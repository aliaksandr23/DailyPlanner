using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using DailyPlanner.IdentityApiHost.Data.Entities;

namespace DailyPlanner.IdentityApiHost.Data.Managers
{
    public class DailyPlannerSignInManager : SignInManager<DailyPlannerUser>
    {
        public DailyPlannerSignInManager(
            UserManager<DailyPlannerUser> userManager,
            IHttpContextAccessor contextAccessor,
            ILogger<SignInManager<DailyPlannerUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<DailyPlannerUser> confirmation,
            IOptions<IdentityOptions> optionsAccessor,
            IUserClaimsPrincipalFactory<DailyPlannerUser> claimsFactory)
            : base(userManager, contextAccessor, claimsFactory,
                  optionsAccessor, logger, schemes, confirmation)
        { } 
    }
}