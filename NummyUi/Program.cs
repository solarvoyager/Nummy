using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using NummyUi;
using NummyUi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => options.DetailedErrors = true);

builder.Services.AddAntDesign();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(sp.GetService<NavigationManager>()!.BaseUri)
});

builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
builder.Services.AddScoped<IChartService, ChartService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<ILogService, LogService>();

var apiHost = Environment.GetEnvironmentVariable("NUMMY_API_HOST");
var apiPort = Environment.GetEnvironmentVariable("NUMMY_API_PORT");

var baseUrl = $"http://{apiHost}:{apiPort}";

builder.Services.AddHttpClient(NummyContants.ClientName, config =>
{
    config.BaseAddress = new Uri(baseUrl);
    //config.Timeout = new TimeSpan(0, 0, 15);
    config.DefaultRequestHeaders.Clear();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();