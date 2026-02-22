using NummyUi.Services.Abstract;
using NummyUi.Utils;

namespace NummyUi.Services;

public class DatabaseService(IHttpClientFactory clientFactory) : IDatabaseService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyConstants.ClientName);

    public async Task<IEnumerable<string>> GetPendingMigrations()
    {
        var response = await _client.GetAsync(NummyConstants.GetPendingMigrationsUrl);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        return result ?? [];
    }

    public async Task<bool> Migrate()
    {
        var response = await _client.PutAsync(NummyConstants.ApplyPendingMigrationsUrl, null);
        return response.IsSuccessStatusCode;
    }
}
