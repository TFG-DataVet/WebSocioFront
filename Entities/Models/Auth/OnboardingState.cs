namespace SocioWeb.Entities.Models.Auth;

public class OnboardingState
{
    public string? ClinicId { get; set; }
    public string? UserId { get; set; }

    public string? OnboardingToken { get; set; }

    public void Clear()
    {
        ClinicId = null;
        UserId = null;
        OnboardingToken = null;
    }
}