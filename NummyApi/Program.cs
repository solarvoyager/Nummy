using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Mappers;
using NummyApi.Middleware;
using NummyApi.Services.Abstract;
using NummyApi.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:8082");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var host = Environment.GetEnvironmentVariable("POSTGRES_HOST")
    ?? throw new InvalidOperationException("POSTGRES_HOST environment variable is not set.");
var port = Environment.GetEnvironmentVariable("POSTGRES_PORT")
    ?? throw new InvalidOperationException("POSTGRES_PORT environment variable is not set.");
var db = Environment.GetEnvironmentVariable("POSTGRES_DB")
    ?? throw new InvalidOperationException("POSTGRES_DB environment variable is not set.");
var user = Environment.GetEnvironmentVariable("POSTGRES_USER")
    ?? throw new InvalidOperationException("POSTGRES_USER environment variable is not set.");
var pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")
    ?? throw new InvalidOperationException("POSTGRES_PASSWORD environment variable is not set.");

var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={pass};";

builder.Services.AddDbContext<NummyDataContext>(options =>
{
    options.UseNpgsql(connectionString);
    if (builder.Environment.IsDevelopment())
    {
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
    }
});

builder.Services.AddHealthChecks()
    .AddDbContextCheck<NummyDataContext>();

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

await DbInitializer.InitializeAsync(app.Services);

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
