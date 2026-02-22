namespace NummyApi.Services.Abstract;

public interface IDatabaseService
{
    Task EnsureCreated(CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> GetPendingMigrations(CancellationToken cancellationToken = default);
    Task ApplyPendingMigrations(CancellationToken cancellationToken = default);
}
