using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddBff()
    .AddRemoteApis();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignOutScheme = "oidc";
    options.DefaultChallengeScheme = "oidc";
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.Name = "__BFF-SPA";
    options.Cookie.SameSite = SameSiteMode.Strict;
}).AddOpenIdConnect("oidc", options =>
{
    options.Authority = "https://localhost:5100";
    options.ClientId = "WebInteractive";
    options.ClientSecret = "DailyPlannerWebInteractiveSecret";
    options.ResponseType = "code";
    options.ResponseMode = "query";

    options.UsePkce = true;
    options.SaveTokens = true;
    options.MapInboundClaims = false;
    options.GetClaimsFromUserInfoEndpoint = true;

    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("daily_planner");
    options.Scope.Add("offline_access");
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseBff();
app.UseAuthorization();
app.MapBffManagementEndpoints();
app.MapControllers()
    .RequireAuthorization()
    .AsBffApiEndpoint();
app.MapFallbackToFile("/index.html");

app.Run();