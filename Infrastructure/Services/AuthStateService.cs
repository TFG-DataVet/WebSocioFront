namespace SocioWeb.Infrastructure.Services;

public class AuthStateService
{
    public string? ClinicId { get; private set; }
    public string? UserId   { get; private set; }
    public string? Email    { get; private set; }
    public string? Role     { get; private set; }

    public string? OnboardingToken { get; set; }

    public bool IsAuthenticated => !string.IsNullOrEmpty(ClinicId);
    public bool SessionChecked  { get; set; } = false;

    public void SetSession(string clinicId, string userId, string email, string? role)
    {
        ClinicId = clinicId;
        UserId   = userId;
        Email    = email;
        Role     = role;
    }

    public void Clear()
    {
        ClinicId       = null;
        UserId         = null;
        Email          = null;
        Role           = null;
        OnboardingToken = null;
        SessionChecked = false;
    }
}
