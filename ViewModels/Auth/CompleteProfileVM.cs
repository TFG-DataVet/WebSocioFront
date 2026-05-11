using Microsoft.AspNetCore.Components;
using SocioWeb.Entities.Models.Auth;
using SocioWeb.Services.AppointmentService;

namespace SocioWeb.ViewModels.Auth;

public class CompleteProfileVM
{
    private readonly IAuthService _authService;
    private readonly NavigationManager _nav;
    private readonly OnboardingState _onboardingState;

    public FullClinicData Model { get; set; } = new();
    public string? ErrorMessage { get; set; }

    public CompleteProfileVM(
        IAuthService authService,
        NavigationManager nav,
        OnboardingState onboardingState)
    {
        _authService = authService;
        _nav = nav;
        _onboardingState = onboardingState;
    }

    public async Task OnSubmit()
    {
        ErrorMessage = null;

        if (string.IsNullOrEmpty(_onboardingState.OnboardingToken))
        {
            ErrorMessage = "El token de onboarding está vacío. Por favor, vuelve a verificar tu correo.";
            return;
        }

        if (string.IsNullOrEmpty(_onboardingState.ClinicId))
        {
            ErrorMessage = "No se encontró el identificador de la clínica. Por favor, vuelve a iniciar el proceso.";
            return;
        }

        var clinicId = Guid.Parse(_onboardingState.ClinicId!);
        var success = await _authService.CompleteClinicProfileAsync(clinicId, Model);

        if (success)
        {
            _nav.NavigateTo("/");
        }
    }
}
