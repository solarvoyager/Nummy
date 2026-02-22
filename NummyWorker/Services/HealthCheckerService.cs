using System.Collections.Concurrent;
using NummyShared.DTOs.Domain;

namespace NummyWorker.Services;

public class HealthCheckerService(
    ILogger<HealthCheckerService> logger,
    IHttpClientFactory httpClientFactory
)
    : BackgroundService
{
    // How often to run the check
    private static readonly TimeSpan Interval = TimeSpan.FromSeconds(7);

    // HTTP timeout per individual health-check request
    private static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(5);

    // Max concurrent HTTP requests
    private const int MaxConcurrency = 10;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("HealthCheckerService started.");

        // Create the client once and reuse it across all cycles.
        var client = httpClientFactory.CreateClient(NummyConstants.ClientName);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CheckAllUrls(client, stoppingToken);
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

    private async Task CheckAllUrls(HttpClient client, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting URL health check cycle...");

        var urlsToCheck = await client.GetFromJsonAsync<List<ApplicationHealthCheckerUrlToListDto>>(
            NummyConstants.GetHealthCheckerUrlsUrl, cancellationToken);

        if (urlsToCheck == null || urlsToCheck.Count == 0)
        {
            logger.LogInformation("No URLs to check. Skipping cycle.");
            return;
        }

        using var semaphore = new SemaphoreSlim(MaxConcurrency);
        var results = new ConcurrentBag<ApplicationIsHealthyToUpdateDto>();
        var tasks = new List<Task>();

        foreach (var item in urlsToCheck)
        {
            // Semaphore is acquired solely inside CheckSingleUrl to avoid double-acquire deadlock.
            tasks.Add(CheckSingleUrl(item, client, semaphore, results, cancellationToken));
        }

        await Task.WhenAll(tasks);

        await client.PutAsJsonAsync(NummyConstants.UpdateHealthCheckerUrlsUrl, results.ToList(), cancellationToken);

        var healthy = results.Count(r => r.IsHealthy);
        logger.LogInformation("URL health check cycle completed: {Healthy}/{Total} URLs healthy.", healthy, results.Count);
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
            results.Add(new ApplicationIsHealthyToUpdateDto(item.ApplicationId, isHealthy));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error checking URL {Url}", item.HealthCheckerUrl);
            results.Add(new ApplicationIsHealthyToUpdateDto(item.ApplicationId, false));
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
