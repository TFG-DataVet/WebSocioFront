using Microsoft.AspNetCore.Components;
using SocioWeb.Entities.Models.Auth;
using SocioWeb.Services.AppointmentService;

public class CompleteProfileVM
{
    private readonly IAuthService _authService;
    private readonly NavigationManager _nav;
    public FullClinicData Model { get; set; } = new();
    public Guid ClinicId { get; set; } // Lo llenaremos desde la URL o LocalStorage

    public async Task OnSubmit()
    {
        var success = await _authService.CompleteClinicProfileAsync(ClinicId, Model);
        
        if (success) {
            _nav.NavigateTo("/dashboard");
        }
    }
}