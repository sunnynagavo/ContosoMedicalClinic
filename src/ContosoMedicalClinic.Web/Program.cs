using ContosoMedicalClinic.Web.Components;
using ContosoMedicalClinic.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.AccessDeniedPath = "/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

// Register infrastructure services — DAB URL comes from Aspire service discovery
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

// Auth endpoints — cookies must be set via HTTP, not SignalR
app.MapGet("/api/auth/login", async (HttpContext ctx, string email, string name, string role,
    int? patientId, int? staffId) =>
{
    var claims = new List<Claim>
    {
        new(ClaimTypes.Email, email),
        new(ClaimTypes.Name, name),
        new(ClaimTypes.Role, role),
    };
    if (patientId.HasValue)
        claims.Add(new Claim("PatientId", patientId.Value.ToString()));
    if (staffId.HasValue)
        claims.Add(new Claim("StaffId", staffId.Value.ToString()));

    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);

    await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

    // Redirect based on role
    var redirect = role switch
    {
        "Patient" => "/patient/dashboard",
        "Doctor" => "/staff/dashboard",
        "Staff" => "/staff/dashboard",
        "Admin" => "/admin/dashboard",
        _ => "/"
    };
    ctx.Response.Redirect(redirect);
});

app.MapGet("/api/auth/logout", async (HttpContext ctx) =>
{
    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    ctx.Response.Redirect("/");
});

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
