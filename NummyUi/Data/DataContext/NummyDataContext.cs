using Microsoft.EntityFrameworkCore;
using NummyUi.Data.Entitites;

namespace NummyUi.Data.DataContext;

internal class NummyDataContext : DbContext
{
    public NummyDataContext(DbContextOptions<NummyDataContext> options) : base(options)
    {
    }

    // Nummy.HttpLogger
    public DbSet<NummyRequestLog> NummyRequestLogs { get; set; }
    public DbSet<NummyResponseLog> NummyResponseLogs { get; set; }
    // Nummy.CodeLogger
    public DbSet<NummyCodeLog> NummyCodeLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }
}