using Microsoft.EntityFrameworkCore;
using NummyApi.Entitites;
using NummyApi.Entitites.Generic;

namespace NummyApi.DataContext;

public class NummyDataContext(DbContextOptions<NummyDataContext> options) : DbContext(options)
{
    // Nummy.HttpLogger
    public DbSet<RequestLog> RequestLogs { get; set; }
    public DbSet<ResponseLog> ResponseLogs { get; set; }

    // Nummy.CodeLogger & Nummy.ExceptionHandler
    public DbSet<CodeLog> CodeLogs { get; set; }

    // NummyUi
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamUser> TeamUsers { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<ApplicationStack> ApplicationStacks { get; set; }
    public DbSet<TeamApplication> TeamApplications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditProperties();
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //DataSeed.SeedApplicationStacks(modelBuilder);
        // remove unused auditable properties
        //modelBuilder.AddGlobalFilter(nameof(Auditable.IsDeleted), false);
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
                    // var originalValues = entityEntry.OriginalValues.ToObject();
                    // var currentValues = entityEntry.CurrentValues.ToObject();
                    ((Auditable)entityEntry.Entity).CreatedAt = DateTime.Now;
                    /*((Auditable)entityEntry.Entity).CreatedById =
                        utilService.GetUserIdFromToken();*/
                    break;
                case EntityState.Modified:
                {
                    Entry((Auditable)entityEntry.Entity).Property(p => p.CreatedAt).IsModified =
                        false;
                    // remove unused auditable properties
                    //Entry((Auditable)entityEntry.Entity).Property(p => p.CreatedById).IsModified = false;

                    /*if (((Auditable)entityEntry.Entity).IsDeleted)
                    {
                        Entry((Auditable)entityEntry.Entity).Property(p => p.ModifiedBy)
                            .IsModified = false;
                        Entry((Auditable)entityEntry.Entity).Property(p => p.ModifiedAt)
                            .IsModified = false;

                        ((Auditable)entityEntry.Entity).DeletedAt = DateTime.Now;
                        /*((Auditable)entityEntry.Entity).DeletedBy =
                            utilService.GetUserIdFromToken();#1#
                    }
                    else
                    {
                        ((Auditable)entityEntry.Entity).ModifiedAt = DateTime.Now;
                        /*((Auditable)entityEntry.Entity).ModifiedBy =
                            utilService.GetUserIdFromToken();#1#
                    }*/

                    break;
                }
                case EntityState.Detached:
                case EntityState.Unchanged:
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
    }
}