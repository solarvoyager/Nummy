using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Services.Abstract;

namespace NummyApi.Services.Concrete;

public class DatabaseService(NummyDataContext dataContext) : IDatabaseService
{
    public async Task EnsureCreated()
    {
        await dataContext.Database.EnsureCreatedAsync();
    }

    public async Task<IEnumerable<string>> GetPendingMigrations()
    {
        var pendingMigrations = await dataContext.Database.GetPendingMigrationsAsync();

        return pendingMigrations;
    }

    public async Task ApplyPendingMigrations()
    {
        await dataContext.Database.MigrateAsync();
    }
}