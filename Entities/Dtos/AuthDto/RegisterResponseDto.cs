using System.Text.Json.Serialization;

namespace SocioWeb.Entities.Dtos.AuthDto;

public class RegisterResponseDto
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("clinicId")]
    public string ClinicId { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
