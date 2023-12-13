using Microsoft.AspNetCore.Identity;
using DailyPlanner.IdentityApiHost.Data.Managers;
using DailyPlanner.IdentityApiHost.Data.Entities;
using DailyPlanner.IdentityApiHost.Models;

namespace DailyPlanner.IdentityApiHost.Services.UserService
{
    public class DailyPlannerUserService : IDailyPlannerUserService
    {
        private readonly DailyPlannerUserManager _userManager;
        private readonly DailyPlannerSignInManager _signInManager;

        public DailyPlannerUserService(DailyPlannerUserManager userManager, DailyPlannerSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<DailyPlannerUser> GetUserByLoginAsync(string login)
        {
            return await _userManager.GetUserByLoginAsync(login);
        }

        public async Task<SignInResult> PasswordSignInAsync(string login, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await _signInManager.PasswordSignInAsync(login, password, isPersistent, lockoutOnFailure);
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterModel registerModel)
        {
            var user = new DailyPlannerUser
            {
                Email = registerModel.Email,
                UserName = registerModel.UserName,
            };

            return await _userManager.CreateAsync(user, registerModel.Password);
        }
    }
}