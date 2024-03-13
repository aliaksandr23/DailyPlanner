using Microsoft.EntityFrameworkCore;
using DailyPlanner.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using DailyPlanner.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using DailyPlanner.Application.Common.Repositories;
using DailyPlanner.Infrastructure.Data.Interceptors;
using DailyPlanner.Infrastructure.Services.DateAndTime;

namespace DailyPlanner.Infrastructure;

public static class PersistenceDI
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddScoped<DailyPlannerDbInitializer>();
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();
        services.AddDbContext<DailyPlannerDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DailyPlannerDbConnection"),
                builder => builder.MigrationsAssembly(typeof(DailyPlannerDbContext).Assembly.FullName));
            options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
        });
        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IBoardRepository, BoardRepository>();
        services.AddScoped<IColumnRepository, ColumnRepository>();
    }
}