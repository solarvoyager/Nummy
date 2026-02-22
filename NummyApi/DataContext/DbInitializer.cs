using Microsoft.EntityFrameworkCore;
using NummyApi.Entitites;
using NummyApi.Helpers;

namespace NummyApi.DataContext;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<NummyDataContext>();

        var migrations = await context.Database.GetPendingMigrationsAsync();
        if (!migrations.Any())
            return;

        await context.Database.MigrateAsync();

        var email = Environment.GetEnvironmentVariable("UI_ADMIN_EMAIL");
        var password = Environment.GetEnvironmentVariable("UI_ADMIN_PASSWORD");

        if (string.IsNullOrEmpty(email))
            throw new InvalidOperationException("UI_ADMIN_EMAIL environment variable is not set.");

        if (string.IsNullOrEmpty(password))
            throw new InvalidOperationException("UI_ADMIN_PASSWORD environment variable is not set.");

        var exists = await context.Users.AnyAsync(u => u.Email == email);
        if (exists)
            return;

        var (hash, salt) = SecurityHelper.GeneratePasswordHash(password);

        context.Users.Add(new User
        {
            Id = new Guid("11111111-1111-1111-1111-111111111111"),
            Name = "Admin",
            Surname = "User",
            Email = email,
            PasswordHash = hash,
            PasswordSalt = salt,
            AvatarColorHex = "#4287f5",
            CreatedAt = new DateTime(2025, 1, 1),
        });

        await context.SaveChangesAsync();
    }
}
