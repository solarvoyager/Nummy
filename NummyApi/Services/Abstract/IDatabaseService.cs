namespace NummyApi.Services.Abstract;

public interface IDatabaseService
{
    Task<IEnumerable<string>> GetPendingMigrations();
    Task ApplyPendingMigrations();
}