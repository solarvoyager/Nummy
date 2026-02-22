namespace NummyUi.Services.Abstract;

public interface IDatabaseService
{
    Task<IEnumerable<string>> GetPendingMigrations();
    Task<bool> Migrate();
}
