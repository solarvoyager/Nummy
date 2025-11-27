using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Entitites;
using NummyApi.Helpers;
using NummyApi.Mappers;
using NummyApi.Services.Abstract;
using NummyApi.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:8082"); //,"https://*.8083"

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
var port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
var db = Environment.GetEnvironmentVariable("POSTGRES_DB");
var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
var pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={pass};IncludeErrorDetail=true;";
//$"Host=localhost;Port=5432;Database=nummydatabase;Username=nummyuser;Password=nummypassword;IncludeErrorDetail=true;";

// example for testing:
// docker run --name postgres-container -e POSTGRES_PASSWORD=nummy_password -e POSTGRES_USER=nummy_user -p 5433:5432 -d postgres:latest
builder.Services.AddDbContext<NummyDataContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddAutoMapper(Automapper.GetAutoMapperProfilesFromAllAssemblies().ToArray());

builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<ICodeLogService, CodeLogService>();
builder.Services.AddScoped<IRequestLogService, RequestLogService>();
builder.Services.AddScoped<IResponseLogService, ResponseLogService>();
builder.Services.AddScoped<IStatisticalService, StatisticalService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

var app = builder.Build();

// Seed on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<NummyDataContext>();
    await context.Database.MigrateAsync(); // applies migrations

    var email = Environment.GetEnvironmentVariable("UI_SUPER_ADMIN_EMAIL");
    var password = Environment.GetEnvironmentVariable("UI_SUPER_ADMIN_PASSWORD");

    var (hash, salt) = SecurityHelper.GeneratePasswordHash(password!);

    var exists = await context.Users.AnyAsync(u => u.Email == email);
    if (!exists)
    {
        context.Users.Add(new User
        {
            Id = new Guid("11111111-1111-1111-1111-111111111111"),
            Name = "Super",
            Surname = "Admin",
            Email = email!,
            PasswordHash = hash,
            PasswordSalt = salt,
            AvatarColorHex = "#4287f5",
            CreatedAt = new DateTime(2025, 1, 1),
        });
        await context.SaveChangesAsync();
    }
}


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();