using Microsoft.EntityFrameworkCore;
using NummyUi.Data.DataContext;

namespace NummyUi.Services;

public interface IDatabaseService
{
    Task<IEnumerable<string>> GetPendingMigrations();
    Task<bool> Migrate();
}

public class DatabaseService : IDatabaseService
{
    private readonly NummyDataContext _dataContext;

    public DatabaseService(NummyDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IEnumerable<string>> GetPendingMigrations()
    {
        // Ensure the database exists, and create it if not
        // this commented because this is migrating initial migration also
        // don't know about other migrations
        //await _dataContext.Database.EnsureCreatedAsync();

        var pendingMigrations = await _dataContext.Database.GetPendingMigrationsAsync();

        return pendingMigrations;
    }

    public async Task<bool> Migrate()
    {
        await _dataContext.Database.MigrateAsync();

        return true;
    }
}