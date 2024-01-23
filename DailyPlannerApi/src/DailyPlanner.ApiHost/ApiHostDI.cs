using DailyPlanner.ApiHost.Middleware;
using DailyPlanner.ApiHost.Services.User;
using DailyPlanner.Infrastructure.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DailyPlanner.ApiHost
{
    public static class ApiHostDI
    {
        public static IServiceCollection AddApiHostServices(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddHttpContextAccessor();
            services.AddTransient<ExceptionHandlingMiddleware>();
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
                    policy.RequireClaim("scope", "daily_planner");
                })
            );
            if (environment.IsDevelopment())
                services.AddScoped<IUserService, DevUserService>();
            else
                services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}