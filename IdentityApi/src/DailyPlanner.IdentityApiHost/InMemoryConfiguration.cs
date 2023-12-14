using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace DailyPlanner.IdentityApiHost
{
    public static class InMemoryConfiguration
    {
        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("daily_planner", "DailyPlanner")
        };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "WebInteractive",
                ClientSecrets = { new Secret("DailyPlannerWebInteractiveSecret".Sha256()) },

                AllowOfflineAccess = true,
                AllowedGrantTypes = GrantTypes.Code,
                AllowedCorsOrigins =
                {
                    "https://localhost:50100",
                },
                AllowedScopes = new List<string>
                {
                    "daily_planner",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },

                ClientUri = "https://localhost:50100",
                RedirectUris = { "https://localhost:50100/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:50100/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:50100/signout-callback-oidc" },
            }
        };
    }
}