namespace DailyPlanner.IdentityApiHost.Models
{
    public record class RegisterModel
    {
        public string Email { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
        public string PasswordConfirm { get; init; }
    }
}