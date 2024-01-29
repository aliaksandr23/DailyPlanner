using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using DailyPlanner.IdentityApiHost.Data.Entities;

namespace DailyPlanner.IdentityApiHost.Data.Managers
{
    public class DailyPlannerUserManager : UserManager<DailyPlannerUser>
    {
        public DailyPlannerUserManager(
            IUserStore<DailyPlannerUser> store,
            IServiceProvider services,
            ILogger<UserManager<DailyPlannerUser>> logger,
            IPasswordHasher<DailyPlannerUser> passwordHasher,
            IOptions<IdentityOptions> optionsAccessor,
            IEnumerable<IUserValidator<DailyPlannerUser>> userValidators,
            IEnumerable<IPasswordValidator<DailyPlannerUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors)
            : base(store, optionsAccessor, passwordHasher, userValidators,
                  passwordValidators, keyNormalizer, errors, services, logger)
        { }
    }
}