using IdentityModel;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.IdentityApiHost.Data.Managers;
using DailyPlanner.IdentityApiHost.Data.Entities;

namespace DailyPlanner.IdentityApiHost.Data
{
    public class DailyPlannerIdentityDbInitializer
    {
        private readonly DailyPlannerUserManager _userManager;
        private readonly DailyPlannerIdentityDbContext _context;
        private readonly ILogger<DailyPlannerIdentityDbInitializer> _logger;

        public DailyPlannerIdentityDbInitializer(DailyPlannerUserManager userManager,
            ILogger<DailyPlannerIdentityDbInitializer> logger, DailyPlannerIdentityDbContext context)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task TryInitializeAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while initializing the database {ex.Message}");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            try
            {
                await SeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while seeding the database {ex.Message}");
                throw;
            }
        }

        private async Task SeedAsync()
        {
            var admin = await _userManager.FindByNameAsync("Admin");
            if (admin is null)
            {
                admin = new DailyPlannerUser
                {
                    UserName = "Admin",
                    Email = "Admin@mail.com",
                    EmailConfirmed = true,
                };

                var result = await _userManager.CreateAsync(admin, "Pa$$word123");

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = await _userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "Alexander"),
                    new Claim(JwtClaimTypes.Email, "Admin@mail.com")
                });

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                _logger.LogInformation("Admin user was successfully created");
            }
        }
    }
}