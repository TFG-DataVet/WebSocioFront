using System.Text.Json.Serialization;

namespace SocioWeb.Entities.Dtos.ClinicApiDto;

public class UpdateClinicDto
{
    [JsonPropertyName("clinicName")]
    public string ClinicName { get; set; } = string.Empty;

    [JsonPropertyName("legalName")]
    public string LegalName { get; set; } = string.Empty;

    [JsonPropertyName("legalNumber")]
    public string LegalNumber { get; set; } = string.Empty;

    [JsonPropertyName("legalType")]
    public string LegalType { get; set; } = "AUTONOMO";

    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("codePostal")]
    public string? CodePostal { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("logoUrl")]
    public string? LogoUrl { get; set; }

    [JsonPropertyName("scheduleOpenDays")]
    public List<string> ScheduleOpenDays { get; set; } = new();

    [JsonPropertyName("scheduleOpenTime")]
    public string ScheduleOpenTime { get; set; } = "09:00:00";

    [JsonPropertyName("scheduleCloseTime")]
    public string ScheduleCloseTime { get; set; } = "18:00:00";

    [JsonPropertyName("scheduleNotes")]
    public string? ScheduleNotes { get; set; }
}
