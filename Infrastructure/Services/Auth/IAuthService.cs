using SocioWeb.Entities.Models.Auth;

namespace SocioWeb.Services.AppointmentService;

public interface IAuthService
{
    // Paso 1 
    Task<RegisterClinic>  RegisterInitialAsync(RegisterClinic clinic);
    
    // Paso 2
    Task<VerifyEmailResponse> VerifyEmailAsync( string token);
    
    // Paso 3
    Task<bool> CompleteClinicProfileAsync(Guid clinicId, FullClinicData clinic);
    
    Task ResendEmailToVerify(string email);
}