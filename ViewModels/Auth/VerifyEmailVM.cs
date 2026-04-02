using Microsoft.AspNetCore.Components;
using SocioWeb.Entities.Models.Auth;
using SocioWeb.Services.AppointmentService;

namespace SocioWeb.ViewModels.Auth;

public class VerifyEmailVM
{
    private readonly IAuthService _authService;
    private readonly NavigationManager _nav;
    private readonly OnboardingState _state;

    public string? EmailToResend { get; set; }
    
    public bool IsLoading { get; private set; } = true;
    public bool HasError { get; private set; } = false;
    public string? ErrorMessage { get; private set; }

    public VerifyEmailVM(IAuthService authService, NavigationManager nav, OnboardingState state)
    {
        _authService = authService;
        _nav = nav;
        _state = state;
    }

    public async Task InitializeAsync(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            HasError = true;
            IsLoading = false;
            return;
        }

        try
        {
            var result = await _authService.VerifyEmailAsync(token);

            if (result != null)
            {
                _state.AccessToken = result.Access_token;
                _state.ClinicId = result.User.ClinicId;

                _nav.NavigateTo($"/clinic/{_state.ClinicId}/complete-setup");
            }
        }
        catch (Exception ex)
        {
            HasError = true;
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    public async Task ResendEmailAsync()
    {
        if (string.IsNullOrEmpty(EmailToResend))
        {
            HasError = true;
        }
        
        try
        {
            IsLoading = true;
            ErrorMessage = null;
            await _authService.ResendEmailToVerify(EmailToResend);
            
            _nav.NavigateTo("registro-exitoso");
            
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            HasError = true;
        }
        finally
        {
            IsLoading = false;
        }
    }
}