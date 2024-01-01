using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using NummyUi.Data.DataContext;
using NummyUi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
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

//docker run -d --name nummy_postgres -e POSTGRES_USER=nummyuser -e POSTGRES_PASSWORD=nummypassword -p 5432:5432 postgres:latest
//var connectionString = "Host=localhost;Database=nummydatabase;Username=nummyuser;Password=nummypassword";
var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");

builder.Services.AddDbContext<NummyDataContext>(dbOptions => dbOptions.UseNpgsql(connectionString));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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