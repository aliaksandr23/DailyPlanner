using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.IdentityApiHost.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DailyPlanner.IdentityApiHost.Data
{
    public class DailyPlannerIdentityDbContext : IdentityDbContext<DailyPlannerUser>
    {
        private const string TablePrefix = "DailyPlanner.";

        public DailyPlannerIdentityDbContext(DbContextOptions<DailyPlannerIdentityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<IdentityRole>(b => b.ToTable($"{TablePrefix}Roles"));
            builder.Entity<DailyPlannerUser>(b => b.ToTable($"{TablePrefix}Users"));
            builder.Entity<IdentityUserRole<string>>(b => b.ToTable($"{TablePrefix}UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(b => b.ToTable($"{TablePrefix}UserClaims"));
            builder.Entity<IdentityUserLogin<string>>(b => b.ToTable($"{TablePrefix}UserLogins"));
            builder.Entity<IdentityUserToken<string>>(b => b.ToTable($"{TablePrefix}UserTokens"));
            builder.Entity<IdentityRoleClaim<string>>(b => b.ToTable($"{TablePrefix}RoleClaims"));
        }
    }
}