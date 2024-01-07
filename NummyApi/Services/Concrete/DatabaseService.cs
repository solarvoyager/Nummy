using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.Services.Abstract;

namespace NummyApi.Services.Concrete;

public class DatabaseService(DataContext.DataContext dataContext, IMapper mapper) : IDatabaseService
{
    public async Task<IEnumerable<string>> GetPendingMigrations()
    {
        // Ensure the database exists, and create it if not
        // this commented because this is migrating initial migration also
        // don't know about other migrations
        // await _dataContext.Database.EnsureCreatedAsync();

        var pendingMigrations = await dataContext.Database.GetPendingMigrationsAsync();

        return pendingMigrations;
    }

    public async Task ApplyPendingMigrations()
    {
        await dataContext.Database.MigrateAsync();
    }
}