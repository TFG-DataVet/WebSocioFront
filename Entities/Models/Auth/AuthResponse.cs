namespace SocioWeb.Entities.Models.Auth;

public class AuthResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? TokenType { get; set; }
    public long ExpiresIn { get; set; }
    public string? NextStep { get; set; }
    public UserInfo? User { get; set; }
}

public class UserInfo
{
    public string? UserId { get; set; }
    public string? EmployeeId { get; set; }
    public string? ClinicId { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
}