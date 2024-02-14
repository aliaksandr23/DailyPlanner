using DailyPlanner.IdentityApiHost;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.IdentityApiHost.Data;
using DailyPlanner.IdentityApiHost.Data.Entities;
using DailyPlanner.IdentityApiHost.Data.Managers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<DailyPlannerIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DailyPlannerIdentityDbConnection"));
});

builder.Services
    .AddIdentity<DailyPlannerUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<DailyPlannerIdentityDbContext>()
    .AddUserManager<DailyPlannerUserManager>()
    .AddSignInManager<DailyPlannerSignInManager>()
    .AddDefaultTokenProviders();

builder.Services
    .AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
    })
    .AddInMemoryClients(InMemoryConfiguration.Clients)
    .AddInMemoryApiScopes(InMemoryConfiguration.ApiScopes)
    .AddInMemoryIdentityResources(InMemoryConfiguration.IdentityResources)
    .AddAspNetIdentity<DailyPlannerUser>();

builder.Services.AddScoped<DailyPlannerIdentityDbInitializer>();
builder.Services.AddHostedService<DailyPlannerIdentityWorker>();

var app = builder.Build();

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseIdentityServer();
app.UseAuthorization();
app.MapRazorPages()
    .RequireAuthorization();

app.Run();