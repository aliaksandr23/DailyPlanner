namespace DailyPlanner.IdentityApiHost.Models
{
    public class LoginModel
    {
        public string Login { get; init; }
        public string Password { get; init; }
        public bool RememberMe { get; init; }
        public string ReturnUrl { get; init; }
    }
}