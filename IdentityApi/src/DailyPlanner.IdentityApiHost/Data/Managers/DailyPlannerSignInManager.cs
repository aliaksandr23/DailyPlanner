using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
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
        {
        }

        public override async Task<SignInResult> PasswordSignInAsync(string login, string password,
            bool isPersistent, bool lockoutOnFailure)
        {
            var emailValidator = new EmailAddressAttribute();

            var user = emailValidator.IsValid(login)
                ? await UserManager.FindByEmailAsync(login)
                : await UserManager.FindByNameAsync(login);

            if (user is null)
            {
                return SignInResult.Failed;
            }

            return await PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }
    }
}