using Duende.Bff;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBff(options =>
    options.ManagementBasePath = "/account").AddRemoteApis();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignOutScheme = "oidc";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "__BFF-SPA";
        options.Cookie.SameSite = SameSiteMode.Strict;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5100";
        options.ClientId = "WebInteractive";
        options.ClientSecret = "DailyPlannerWebInteractiveSecret";

        options.ResponseType = "code";
        options.ResponseMode = "query";

        options.SaveTokens = true;
        options.MapInboundClaims = false;
        options.GetClaimsFromUserInfoEndpoint = true;

        options.Scope.Add("daily_planner");
        options.Scope.Add("offline_access");
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseBff();
app.UseAuthorization();
app.MapBffManagementEndpoints();

app.MapRemoteBffApiEndpoint("/Board", "https://localhost:6100/Board")
    .RequireAccessToken(TokenType.User);
app.MapRemoteBffApiEndpoint("/Column", "https://localhost:6100/Column")
    .RequireAccessToken(TokenType.User);
app.MapRemoteBffApiEndpoint("/Card", "https://localhost:6100/Card")
    .RequireAccessToken(TokenType.User);

app.Run();