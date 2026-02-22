using NummyWorker;
using NummyWorker.Services;

var builder = WebApplication.CreateBuilder(args);

var apiHost = Environment.GetEnvironmentVariable("NUMMY_API_HOST")
    ?? throw new InvalidOperationException("NUMMY_API_HOST environment variable is not set.");
var apiPort = Environment.GetEnvironmentVariable("NUMMY_API_PORT")
    ?? throw new InvalidOperationException("NUMMY_API_PORT environment variable is not set.");

var baseUrl = $"http://{apiHost}:{apiPort}";

builder.Services.AddHttpClient(NummyConstants.ClientName, config =>
{
    config.BaseAddress = new Uri(baseUrl);
    config.Timeout = TimeSpan.FromSeconds(10);
    config.DefaultRequestHeaders.Clear();
});

builder.Services.AddHealthChecks();

builder.Services.AddHostedService<HealthCheckerService>();

var app = builder.Build();

app.MapHealthChecks("/health");

app.Run();
