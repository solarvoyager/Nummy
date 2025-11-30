using System.Collections.Concurrent;
using NummyShared.DTOs.Domain;

namespace NummyWorker.Services;

public class HealthCheckerService(
    ILogger<HealthCheckerService> logger,
    IServiceScopeFactory scopeFactory,
    IHttpClientFactory httpClientFactory
)
    : BackgroundService
{
    // How often to run the check
    private static readonly TimeSpan Interval = TimeSpan.FromSeconds(7);

    // HTTP timeout per request
    private static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(5);

    // Max concurrent HTTP requests
    private const int MaxConcurrency = 10;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("HealthCheckerService started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CheckAllUrls(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while checking URLs.");
            }

            try
            {
                await Task.Delay(Interval, stoppingToken);
            }
            catch (TaskCanceledException)
            {
                // shutting down
            }
        }

        logger.LogInformation("HealthCheckerService stopping.");
    }

    private async Task CheckAllUrls(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting URL health check cycle...");

        var client = httpClientFactory.CreateClient(NummyConstants.ClientName);

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(NummyConstants.GetHealthCheckerUrlsUrl, UriKind.Relative)
        };

        var response = await client.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var urlsToCheck = await response.Content
            .ReadFromJsonAsync<List<ApplicationHealthCheckerUrlToListDto>>(cancellationToken: cancellationToken);

        if (urlsToCheck == null || urlsToCheck.Count == 0)
            logger.LogInformation("No urls found...");

        // Use a semaphore to limit concurrency
        using var semaphore = new SemaphoreSlim(MaxConcurrency);

        // Thread-safe collection to store results
        var results = new ConcurrentBag<ApplicationIsHealthyToUpdateDto>();

        var tasks = new List<Task>();

        foreach (var item in urlsToCheck!)
        {
            await semaphore.WaitAsync(cancellationToken);

            tasks.Add(CheckSingleUrl(item, client, semaphore, results, cancellationToken));
        }

        await Task.WhenAll(tasks);

        var request2 = new HttpRequestMessage
        {
            Content = JsonContent.Create(results.ToList()),
            Method = HttpMethod.Put,
            RequestUri = new Uri(NummyConstants.UpdateHealthCheckerUrlsUrl, UriKind.Relative)
        };

        var response2 = await client.SendAsync(request2, cancellationToken);
        response.EnsureSuccessStatusCode();

        logger.LogInformation("URL health check cycle completed.");
    }

    private async Task CheckSingleUrl(
        ApplicationHealthCheckerUrlToListDto item,
        HttpClient client,
        SemaphoreSlim semaphore,
        ConcurrentBag<ApplicationIsHealthyToUpdateDto> results,
        CancellationToken cancellationToken)
    {
        await semaphore.WaitAsync(cancellationToken);

        try
        {
            var isHealthy = await CheckUrlAsync(client, item.HealthCheckerUrl, cancellationToken);
            results.Add(new ApplicationIsHealthyToUpdateDto
            (
                item.ApplicationId,
                isHealthy
            ));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error checking URL {Url}", item.HealthCheckerUrl);
            results.Add(new ApplicationIsHealthyToUpdateDto
            (
                item.ApplicationId,
                false
            ));
        }
        finally
        {
            semaphore.Release();
        }
    }

    private async Task<bool> CheckUrlAsync(HttpClient client, string url, CancellationToken cancellationToken)
    {
        try
        {
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            linkedCts.CancelAfter(RequestTimeout);

            var response = await client.GetAsync(url, linkedCts.Token);
            return response.IsSuccessStatusCode;
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            logger.LogWarning("Request to {Url} timed out.", url);
            return false;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Request to {Url} failed.", url);
            return false;
        }
    }
}