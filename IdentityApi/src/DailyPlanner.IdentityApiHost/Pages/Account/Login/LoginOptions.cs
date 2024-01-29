namespace DailyPlanner.IdentityApiHost.Pages.Account.Login;

public class LoginOptions
{
    public static bool AllowLocalLogin = true;
    public static bool AllowRememberLogin = true;
    public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);
    public static string InvalidEmailMessage = "User with given email is not found";
    public static string InvalidPasswordMessage = "Invalid password";
    public static string InvalidCredentialsMessage = "Something went wrong. check your credentials and try again";
}