using NummyWorker;
using NummyWorker.Services;

var builder = WebApplication.CreateBuilder(args);

// HttpClientFactory

var apiHost = Environment.GetEnvironmentVariable("NUMMY_API_HOST");
var apiPort = Environment.GetEnvironmentVariable("NUMMY_API_PORT");

var baseUrl = $"http://{apiHost}:{apiPort}";

builder.Services.AddHttpClient(NummyConstants.ClientName, config =>
{
    config.BaseAddress = new Uri(baseUrl);
    //config.Timeout = new TimeSpan(0, 0, 15);
    config.DefaultRequestHeaders.Clear();
});

// Background worker
builder.Services.AddHostedService<HealthCheckerService>();

var app = builder.Build();

app.Run();