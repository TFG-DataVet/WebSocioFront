using SocioWeb.Services.AppointmentService;

namespace SocioWeb.ViewModels.Auth;

public class ForgotPasswordVM
{
    private readonly IAuthService _authService;

    public string? Email { get; set; }
    public bool IsSubmitted { get; private set; }
    public bool IsLoading { get; private set; }

    public ForgotPasswordVM(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task OnSubmit()
    {
        if (IsLoading) return;
        IsLoading = true;

        try
        {
            await _authService.ForgotPasswordAsync(Email ?? string.Empty);
        }
        catch
        {
            // Silencioso por seguridad — no revelamos si el email existe
        }
        finally
        {
            IsLoading = false;
            IsSubmitted = true;
        }
    }
}
