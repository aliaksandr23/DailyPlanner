using DailyPlanner.IdentityApiHost;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.IdentityApiHost.Data;
using DailyPlanner.IdentityApiHost.Data.Entities;
using DailyPlanner.IdentityApiHost.Data.Managers;
using DailyPlanner.IdentityApiHost.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DailyPlannerIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DailyPlannerIdentityDbConnection"));
});
builder.Services.AddIdentity<DailyPlannerUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<DailyPlannerIdentityDbContext>()
    .AddUserManager<DailyPlannerUserManager>()
    .AddSignInManager<DailyPlannerSignInManager>()
    .AddDefaultTokenProviders();
builder.Services.AddIdentityServer()
    .AddInMemoryClients(InMemoryConfiguration.Clients)
    .AddInMemoryApiScopes(InMemoryConfiguration.ApiScopes)
    .AddInMemoryIdentityResources(InMemoryConfiguration.IdentityResources)
    .AddAspNetIdentity<DailyPlannerUser>();
builder.Services.AddScoped<IDailyPlannerUserService, DailyPlannerUserService>();
builder.Services.AddScoped<DailyPlannerIdentityDbInitializer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<DailyPlannerIdentityDbInitializer>();
        await dbInitializer.TryInitializeAsync();

        if (args.Contains("/seed"))
        {
            await dbInitializer.TrySeedAsync();
        }
    }
}

app.UseHttpsRedirection();
app.UseIdentityServer();
app.UseAuthorization();
app.MapControllers();

app.Run();