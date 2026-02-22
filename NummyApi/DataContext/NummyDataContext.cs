using Microsoft.EntityFrameworkCore;
using NummyApi.Entitites;
using NummyApi.Entitites.Generic;

namespace NummyApi.DataContext;

public class NummyDataContext(DbContextOptions<NummyDataContext> options) : DbContext(options)
{
    // Nummy.HttpLogger
    public DbSet<RequestLog> RequestLogs { get; set; }
    public DbSet<ResponseLog> ResponseLogs { get; set; }
    public DbSet<Header> Headers { get; set; }

    // Nummy.CodeLogger & Nummy.ExceptionHandler
    public DbSet<CodeLog> CodeLogs { get; set; }

    // NummyUi
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamUser> TeamUsers { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<ApplicationStack> ApplicationStacks { get; set; }
    public DbSet<TeamApplication> TeamApplications { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditProperties();
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DataSeed.SeedApplicationStacks(modelBuilder);
    }

    private void SetAuditProperties()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: Auditable, State: EntityState.Added or EntityState.Modified });

        foreach (var entityEntry in entries)
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    ((Auditable)entityEntry.Entity).CreatedAt = DateTimeOffset.Now;
                    break;
                case EntityState.Modified:
                    Entry((Auditable)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
    }
}
