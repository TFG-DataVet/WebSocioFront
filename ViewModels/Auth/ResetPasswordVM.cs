using Microsoft.AspNetCore.Components;
using SocioWeb.Services.AppointmentService;

namespace SocioWeb.ViewModels.Auth;

public class ResetPasswordVM
{
    private readonly IAuthService _authService;
    private readonly NavigationManager _nav;

    private string? _token;

    public string? NewPassword { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? ErrorMessage { get; private set; }
    public bool IsSuccess { get; private set; }
    public bool IsLoading { get; private set; }

    public ResetPasswordVM(IAuthService authService, NavigationManager nav)
    {
        _authService = authService;
        _nav = nav;
    }

    public void Initialize(string? token)
    {
        _token = token;
    }

    public async Task OnSubmit()
    {
        ErrorMessage = null;

        if (NewPassword != ConfirmPassword)
        {
            ErrorMessage = "Las contraseñas no coinciden.";
            return;
        }

        if (string.IsNullOrEmpty(_token))
        {
            ErrorMessage = "El enlace no es válido.";
            return;
        }

        IsLoading = true;
        try
        {
            await _authService.ResetPasswordAsync(_token, NewPassword!);
            IsSuccess = true;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }
}
