using DailyPlanner.ApiHost;
using DailyPlanner.Application;
using DailyPlanner.Infrastructure;
using DailyPlanner.ApiHost.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddApiHostServices();
builder.Services.AddHostedService<DailyPlannerWorker>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers()
    .RequireAuthorization("DailyPlannerPolicy");

app.Run();