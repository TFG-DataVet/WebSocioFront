namespace SocioWeb.Entities.Models.Auth;

public class OnboardingState
{
    public string? ClinicId { get; set; }
    public string? AccessToken { get; set; }

    public void Clear()
    {
        ClinicId = null;
        AccessToken = null;
    }
}