namespace SocioWeb.Entities.Dtos.Auth;

public class LoginResponse
{
    public string     AccessToken    { get; set; } = string.Empty;
    public string?    RefreshToken   { get; set; } = string.Empty;
    public string?    TokenType      { get; set; } = string.Empty;
    public int        ExpiresIn      { get; set; }
    public string?    NextStep       { get; set; } = string.Empty;
    public UserInfo   User           { get; set; } = new();
}

public class UserInfo
{
    public string  UserId             { get; set; } = string.Empty;
    public string? EmployeeId         { get; set; }
    public string  ClinicId           { get; set; } = string.Empty;
    public string  Email              { get; set; } = string.Empty;
    public string  Role               { get; set; } = string.Empty;
}