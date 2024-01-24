using DailyPlanner.ApiHost.Middleware;
using DailyPlanner.ApiHost.Services.User;
using DailyPlanner.Infrastructure.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DailyPlanner.ApiHost
{
    public static class ApiHostDI
    {
        public static IServiceCollection AddApiHostServices(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddEndpointsApiExplorer();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.MapInboundClaims = false;
                    options.Authority = "https://localhost:5100";
                    options.TokenValidationParameters.ValidateAudience = false;
                });

            services.AddAuthorizationBuilder()
                .AddPolicy("DailyPlannerPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser().RequireClaim("sub");
                    policy.RequireClaim("scope", "daily_planner");
                });
            return services;
        }
    }
}