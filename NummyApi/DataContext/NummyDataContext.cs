using Microsoft.EntityFrameworkCore;
using NummyApi.Entitites;
using NummyApi.Entitites.Generic;

namespace NummyApi.DataContext;

public class NummyDataContext : DbContext
{
    public NummyDataContext(DbContextOptions<NummyDataContext> options) : base(options)
    {
    }

    // Nummy.HttpLogger
    public DbSet<RequestLog> NummyRequestLogs { get; set; }

    public DbSet<ResponseLog> NummyResponseLogs { get; set; }

    // Nummy.CodeLogger
    public DbSet<CodeLog> NummyCodeLogs { get; set; }

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
        modelBuilder.AddGlobalFilter(nameof(Auditable.IsDeleted), false);
    }

    private void SetAuditProperties()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Auditable && e.State is EntityState.Added or EntityState.Modified);

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
                    Entry((Auditable)entityEntry.Entity).Property(p => p.CreatedById).IsModified =
                        false;

                    if (((Auditable)entityEntry.Entity).IsDeleted)
                    {
                        Entry((Auditable)entityEntry.Entity).Property(p => p.ModifiedBy)
                            .IsModified = false;
                        Entry((Auditable)entityEntry.Entity).Property(p => p.ModifiedAt)
                            .IsModified = false;

                        ((Auditable)entityEntry.Entity).DeletedAt = DateTime.Now;
                        /*((Auditable)entityEntry.Entity).DeletedBy =
                            utilService.GetUserIdFromToken();*/
                    }
                    else
                    {
                        ((Auditable)entityEntry.Entity).ModifiedAt = DateTime.Now;
                        /*((Auditable)entityEntry.Entity).ModifiedBy =
                            utilService.GetUserIdFromToken();*/
                    }

                    break;
                }
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
    }
}