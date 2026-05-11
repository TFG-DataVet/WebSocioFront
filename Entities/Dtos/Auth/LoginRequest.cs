namespace SocioWeb.Entities.Dtos.Auth;

public class LoginRequest
{
    public string Email         { get; set; } = string.Empty;
    public string Password      { get; set; } = string.Empty;
}