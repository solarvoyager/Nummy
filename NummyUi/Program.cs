using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using NummyUi;
using NummyUi.Services;
using NummyUi.Services.Abstract;
using NummyUi.Session;
using NummyUi.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => options.DetailedErrors = builder.Environment.IsDevelopment());

builder.Services.AddAntDesign();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(sp.GetService<NavigationManager>()!.BaseUri)
});

builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));

// Sessions
builder.Services.AddScoped<IUserSession, UserSession>();

// Services
builder.Services.AddScoped<IChartService, ChartService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IStatisticalService, StatisticalService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IHelperService, HelperService>();

var apiHost = Environment.GetEnvironmentVariable("NUMMY_API_HOST")
    ?? throw new InvalidOperationException("NUMMY_API_HOST environment variable is not set.");
var apiPort = Environment.GetEnvironmentVariable("NUMMY_API_PORT")
    ?? throw new InvalidOperationException("NUMMY_API_PORT environment variable is not set.");

var baseUrl = $"http://{apiHost}:{apiPort}";

builder.Services.AddHttpClient(NummyConstants.ClientName, config =>
{
    config.BaseAddress = new Uri(baseUrl);
    config.DefaultRequestHeaders.Clear();
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
