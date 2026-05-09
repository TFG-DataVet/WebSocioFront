using System.Text.Json.Serialization;

namespace SocioWeb.Entities.Dtos.ClinicApiDto;

public class CompleteSetupDto
{
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

    [JsonPropertyName("ownerDocumentType")]
    public string OwnerDocumentType { get; set; } = "DNI";

    [JsonPropertyName("ownerDocumentNumber")]
    public string OwnerDocumentNumber { get; set; } = string.Empty;

    [JsonPropertyName("ownerAddress")]
    public string OwnerAddress { get; set; } = string.Empty;

    [JsonPropertyName("ownerCity")]
    public string OwnerCity { get; set; } = string.Empty;

    [JsonPropertyName("ownerPostalCode")]
    public string? OwnerPostalCode { get; set; }

    [JsonPropertyName("ownerAvatarUrl")]
    public string? OwnerAvatarUrl { get; set; }

    [JsonPropertyName("ownerSpeciality")]
    public string? OwnerSpeciality { get; set; }
}
