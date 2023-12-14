using DailyPlanner.ApiHost;
using DailyPlanner.Application;
using DailyPlanner.Infrastructure;
using DailyPlanner.ApiHost.Middleware;
using DailyPlanner.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddApiHostServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<DailyPlannerDbInitializer>();
        await dbInitializer.InitializeAsync();

        if (args.Contains("/seed"))
        {
            await dbInitializer.SeedAsync();
        }
    }
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers().RequireAuthorization("DailyPlannerPolicy");

app.Run();