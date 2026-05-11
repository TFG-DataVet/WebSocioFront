using System.Text.Json.Serialization;

namespace SocioWeb.Entities.Dtos.AuthDto;

public class TokenResponseDto
{
    [JsonPropertyName("accessToken")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("refreshToken")]
    public string? RefreshToken { get; set; }

    [JsonPropertyName("tokenType")]
    public string? TokenType { get; set; }

    [JsonPropertyName("expiresIn")]
    public long ExpiresIn { get; set; }

    [JsonPropertyName("user")]
    public UserInfoDto? User { get; set; }

    [JsonPropertyName("nextStep")]
    public string? NextStep { get; set; }

    public class UserInfoDto
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; } = string.Empty;

        [JsonPropertyName("employeeId")]
        public string? EmployeeId { get; set; }

        [JsonPropertyName("clinicId")]
        public string ClinicId { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("role")]
        public string? Role { get; set; }
    }
}
