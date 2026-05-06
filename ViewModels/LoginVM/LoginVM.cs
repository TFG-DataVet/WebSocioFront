using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SocioWeb.Entities.Dtos.Auth;
using SocioWeb.Infrastructure.Auth;
using SocioWeb.Services.AppointmentService;

namespace SocioWeb.ViewModels.LoginVM;

public class LoginVM
{
    private readonly IAuthService _authService;
    private readonly NavigationManager _nav;
    private readonly AuthStateProvider _authStateProvider;

    public LoginRequest Model { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public LoginVM(IAuthService authService, NavigationManager nav,
        AuthenticationStateProvider authStateProvider)
    {
        _authService = authService;
        _nav = nav;
        _authStateProvider = (AuthStateProvider)authStateProvider;
    }

    public async Task OnSubmit()
    {
        ErrorMessage = null;
        try
        {
            var response = await _authService.LoginAsync(Model);
            if (response == null)
            {
                ErrorMessage = "Error al conectar con el servidor. Inténtalo de nuevo.";
                return;
            }

            if (response.NextStep == "COMPLETE_SETUP")
            {
                _nav.NavigateTo("/onboarding/complete-setup");
                return;
            }

            // Notificamos al sistema de auth que hay sesión nueva
            _authStateProvider.NotifyAuthChanged();
            _nav.NavigateTo("/");
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}