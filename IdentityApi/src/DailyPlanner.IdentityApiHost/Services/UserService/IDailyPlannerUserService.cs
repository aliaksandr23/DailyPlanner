using Microsoft.AspNetCore.Identity;
using DailyPlanner.IdentityApiHost.Models;
using DailyPlanner.IdentityApiHost.Data.Entities;

namespace DailyPlanner.IdentityApiHost.Services.UserService
{
    public interface IDailyPlannerUserService
    {
        public Task<SignInResult> PasswordSignInAsync(string login, string password,
            bool isPersistent, bool lockoutOnFailure);
        public Task<DailyPlannerUser> GetUserByLoginAsync(string login);
        public Task<IdentityResult> RegisterUserAsync(RegisterModel registerModel);
    }
}