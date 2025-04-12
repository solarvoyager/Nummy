using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Mappers;
using NummyApi.Services.Abstract;
using NummyApi.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:8082"); //,"https://*.8083"

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
var db = Environment.GetEnvironmentVariable("POSTGRES_DB");
var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
var pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

var connectionString =
    $"Host={host};Port=5432;Database={db};Username={user};Password={pass};IncludeErrorDetail=true;";

// example for testing:
// docker run --name postgres-container -e POSTGRES_PASSWORD=nummypassword -e POSTGRES_USER=nummyuser -p 5432:5432 -d postgres:latest
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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();