using SocioWeb.Entities.Dtos.Auth;
using SocioWeb.Entities.Models.Auth;

namespace SocioWeb.Services.AppointmentService;

public interface IAuthService
{
    // Paso 1 
    Task<RegisterClinic>  RegisterInitialAsync(RegisterClinic clinic);
    
    // Paso 2
    Task<AuthResponse> VerifyEmailAsync( string token);
    
    // Paso 3
    Task<bool> CompleteClinicProfileAsync(Guid clinicId, FullClinicData clinic);
    
    Task ResendEmailToVerify(string email);
    
    Task<AuthResponse> LoginAsync(LoginRequest loginRequest);
    
    Task LogoutAsync();
    
    Task<bool> RefreshTokenAsync();

    Task<AuthResponse?> GetCurrentUserAsync();

    Task ForgotPasswordAsync(string email);

    Task ResetPasswordAsync(string token, string newPassword);
}