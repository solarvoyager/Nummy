namespace NummyUi.Services;

public interface IDatabaseService
{
    Task<IEnumerable<string>> GetPendingMigrations();
    Task<bool> Migrate();
}

public class DatabaseService(IHttpClientFactory clientFactory) : IDatabaseService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyContants.ClientName);

    public async Task<IEnumerable<string>> GetPendingMigrations()
    {
        var response = await _client.GetFromJsonAsync<IEnumerable<string>>(NummyContants.GetPendingMigrationsUrl);
        
        return response?? Array.Empty<string>();
    }

    public async Task<bool> Migrate()
    {
        var response = await _client.PutAsync(NummyContants.ApplyPendingMigrationsUrl, null);

        return response.IsSuccessStatusCode;
    }
}