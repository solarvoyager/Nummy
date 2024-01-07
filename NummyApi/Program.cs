using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Mappers;
using NummyApi.Services.Abstract;
using NummyApi.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddAutoMapper(Automapper.GetAutoMapperProfilesFromAllAssemblies().ToArray());

builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<ILogService, LogService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();