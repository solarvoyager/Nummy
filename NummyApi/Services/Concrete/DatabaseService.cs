using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Services.Abstract;

namespace NummyApi.Services.Concrete;

public class DatabaseService(NummyDataContext dataContext) : IDatabaseService
{
    public async Task EnsureCreated(CancellationToken cancellationToken = default)
    {
        await dataContext.Database.EnsureCreatedAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>> GetPendingMigrations(CancellationToken cancellationToken = default)
    {
        return await dataContext.Database.GetPendingMigrationsAsync(cancellationToken);
    }

    public async Task ApplyPendingMigrations(CancellationToken cancellationToken = default)
    {
        await dataContext.Database.MigrateAsync(cancellationToken);
    }
}
