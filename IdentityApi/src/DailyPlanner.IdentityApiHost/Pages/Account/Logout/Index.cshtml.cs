using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using DailyPlanner.IdentityApiHost.Data.Managers;

namespace DailyPlanner.IdentityApiHost.Pages.Account.Logout;

[AllowAnonymous]
[SecurityHeaders]
public class Index : PageModel
{
    private readonly IEventService _events;
    private readonly DailyPlannerSignInManager _signInManager;
    private readonly IIdentityServerInteractionService _interaction;

    [BindProperty]
    public string LogoutId { get; set; }

    public Index(DailyPlannerSignInManager signInManager, IIdentityServerInteractionService interaction, IEventService events)
    {
        _events = events;
        _interaction = interaction;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> OnGet(string logoutId)
    {
        LogoutId = logoutId;

        var showLogoutPrompt = LogoutOptions.ShowLogoutPrompt;

        if (User?.Identity.IsAuthenticated != true)
        {
            showLogoutPrompt = false;
        }
        else
        {
            var context = await _interaction.GetLogoutContextAsync(LogoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                showLogoutPrompt = false;
            }
        }

        if (showLogoutPrompt == false)
        {
            return await OnPost();
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (User?.Identity.IsAuthenticated == true)
        {
            LogoutId ??= await _interaction.CreateLogoutContextAsync();

            await _signInManager.SignOutAsync();

            await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));

            var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;

            if (idp != null && idp != Duende.IdentityServer.IdentityServerConstants.LocalIdentityProvider)
            {
                if (await HttpContext.GetSchemeSupportsSignOutAsync(idp))
                {
                    string url = Url.Page("/Account/Logout/Loggedout", new { logoutId = LogoutId });

                    return SignOut(new AuthenticationProperties { RedirectUri = url }, idp);
                }
            }
        }

        return RedirectToPage("/Account/Logout/LoggedOut", new { logoutId = LogoutId });
    }
}