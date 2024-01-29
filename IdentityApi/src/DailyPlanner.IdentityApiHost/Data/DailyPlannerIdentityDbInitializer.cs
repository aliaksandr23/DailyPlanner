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

        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Database.MigrateAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database");
                throw;
            }
        }

        private async Task TrySeedAsync()
        {
            var admin = await _userManager.FindByNameAsync("Admin");
            if (admin is null)
            {
                admin = new DailyPlannerUser
                {
                    Id = "F1DDF39A-41C5-40EA-8310-2D855DC6A192",
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
                    new(JwtClaimTypes.Name, "Admin"),
                    new(JwtClaimTypes.Email, "Admin@mail.com")
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