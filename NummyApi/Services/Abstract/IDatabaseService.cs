namespace NummyApi.Services.Abstract;

public interface IDatabaseService
{
    Task EnsureCreated();
    Task<IEnumerable<string>> GetPendingMigrations();
    Task ApplyPendingMigrations();
}