using Microsoft.AspNetCore.Components;
using SocioWeb.Entities.Models.Auth;
using SocioWeb.Services.AppointmentService;

namespace SocioWeb.ViewModels;

public class RegisterClinicVM
{
    private readonly IAuthService _authService;
    private readonly NavigationManager _nav;
    
    public RegisterClinic Model { get; set; } = new();
    public bool ISLoading { get; private set; }
    public string? ErrorMessage { get; private set; }
    
    public RegisterClinicVM(IAuthService authService, NavigationManager nav)
    {
        _authService = authService;
        _nav = nav;
    }

    public async Task SaveAsync()
    {
        try
        {
            ISLoading = true;
            ErrorMessage = null;

            var result = await _authService.RegisterInitialAsync(Model);
            
            Console.Write(result);
            if (result != null)
            {
                _nav.NavigateTo("/registro_exitoso");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            ISLoading = false;
        }
    }
}