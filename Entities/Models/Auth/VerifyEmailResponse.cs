namespace SocioWeb.Entities.Models.Auth;

public class VerifyEmailResponse
{
    public string Access_token { get; set; }
    public string RefreshToken { get; set; }
    public string Token_type { get; set; }
    public string Expires_in { get; set; }
    public User User { get; set; }
}

public class User
{
    public string UserId { get; set; }
    public string EmployeeId { get; set; }
    public string ClinicId { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}