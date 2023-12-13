using DailyPlanner.ApiHost.Services;
using DailyPlanner.ApiHost.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DailyPlanner.Infrastructure.Services.CurrentUser;

namespace DailyPlanner.ApiHost
{
    public static class ApiHostDI
    {
        public static IServiceCollection AddApiHostServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddHttpContextAccessor();
            services.AddTransient<IMiddleware, ExceptionHandlingMiddleware>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "https://localhost:5100";
                    options.TokenValidationParameters.ValidateAudience = true;
                });
            services.AddAuthorization(options =>
                options.AddPolicy("DailyPlannerPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "DailyPlannerScope");
                })
            );
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}