using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using SocioWeb.Services.AppointmentService;

namespace SocioWeb.Infrastructure.Auth;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly IAuthService _authService;

    public AuthStateProvider(IAuthService authService)
    {
        _authService = authService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var auth = await _authService.GetCurrentUserAsync();

            if (auth?.User is null)
                return Unauthenticated();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, auth.User.UserId  ?? ""),
                new(ClaimTypes.Name,           auth.User.Email   ?? ""),
                new(ClaimTypes.Role,           auth.User.Role    ?? ""),
                new("ClinicId",                auth.User.ClinicId  ?? ""),
                new("EmployeeId",              auth.User.EmployeeId ?? ""),
            };

            var identity = new ClaimsIdentity(claims, authenticationType: "cookie");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        catch
        {
            return Unauthenticated();
        }
    }

    public void NotifyAuthChanged() =>
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

    private static AuthenticationState Unauthenticated() =>
        new(new ClaimsPrincipal(new ClaimsIdentity()));
}