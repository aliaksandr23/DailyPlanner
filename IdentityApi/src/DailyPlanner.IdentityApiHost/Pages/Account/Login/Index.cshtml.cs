using Microsoft.AspNetCore.Mvc;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DailyPlanner.IdentityApiHost.Data.Managers;

namespace DailyPlanner.IdentityApiHost.Pages.Account.Login;

[AllowAnonymous]
[SecurityHeaders]
public class Index(
    IEventService events,
    DailyPlannerUserManager userManager,
    DailyPlannerSignInManager signInManager,
    IIdentityServerInteractionService interaction) : PageModel
{
    private readonly IEventService _events = events;
    private readonly DailyPlannerUserManager _userManager = userManager;
    private readonly DailyPlannerSignInManager _signInManager = signInManager;
    private readonly IIdentityServerInteractionService _interaction = interaction;

    public ViewModel View { get; set; }

    [BindProperty]
    public InputModel Input { get; set; }

    public IActionResult OnGet(string returnUrl)
    {
        Input = new InputModel
        {
            ReturnUrl = returnUrl
        };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

        if (Input.Button != "login")
        {
            if (context is not null)
            {
                await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);
            }
            return Redirect(Input.ReturnUrl ?? "~/");
        }

        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, LoginOptions.InvalidCredentialsMessage);
            return Page();
        }

        var user = await _userManager.FindByEmailAsync(Input.Email);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, LoginOptions.InvalidEmailMessage);
            return Page();
        }

        var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberUser, lockoutOnFailure: true);
        if (result.Succeeded)
        {
            await _events.RaiseAsync(new UserLoginSuccessEvent(Input.Email, user.Id, user.UserName, clientId: context?.Client.ClientId));
            return Redirect(Input.ReturnUrl ?? "~/");
        }
        else
        {
            await _events.RaiseAsync(new UserLoginFailureEvent(Input.Email, "invalid credentials", clientId: context?.Client.ClientId));
            ModelState.AddModelError(string.Empty, LoginOptions.InvalidPasswordMessage);
            return Page();
        }
    }
}