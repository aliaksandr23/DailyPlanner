using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace DailyPlanner.IdentityApiHost
{
    public static class InMemoryConfiguration
    {
        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new("daily_planner", "DailyPlanner")
        };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new() {
                ClientId = "WebInteractive",
                ClientSecrets = { new Secret("DailyPlannerWebInteractiveSecret".Sha256()) },

                AllowOfflineAccess = true,
                AllowedGrantTypes = GrantTypes.Code,

                AllowedScopes = new List<string>
                {
                    "daily_planner",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                },

                ClientUri = "https://localhost:5173",
                RedirectUris = { "https://localhost:5173/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:5173/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:5173/signout-callback-oidc" },
            }
        };
    }
}