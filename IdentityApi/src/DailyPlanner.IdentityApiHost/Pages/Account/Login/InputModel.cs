using System.ComponentModel.DataAnnotations;

namespace DailyPlanner.IdentityApiHost.Pages.Account.Login;

public class InputModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public bool RememberUser { get; set; }
    public string ReturnUrl { get; set; }
    public string Button { get; set; }
}