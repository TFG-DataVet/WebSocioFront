using System.Text.Json.Serialization;

namespace SocioWeb.Entities.Dtos.ClinicApiDto;

public class ClinicResponseDto
{
    [JsonPropertyName("clinicId")]
    public string ClinicId { get; set; } = string.Empty;

    [JsonPropertyName("clinicName")]
    public string ClinicName { get; set; } = string.Empty;

    [JsonPropertyName("legalName")]
    public string? LegalName { get; set; }

    [JsonPropertyName("legalNumber")]
    public string? LegalNumber { get; set; }

    [JsonPropertyName("legalType")]
    public string? LegalType { get; set; }

    [JsonPropertyName("address")]
    public AddressDto? Address { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("logoUrl")]
    public string? LogoUrl { get; set; }

    [JsonPropertyName("schedule")]
    public ScheduleDto? Schedule { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    public class AddressDto
    {
        [JsonPropertyName("street")]
        public string? Street { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("postalCode")]
        public string? PostalCode { get; set; }

        [JsonPropertyName("fullAddress")]
        public string? FullAddress { get; set; }
    }

    public class ScheduleDto
    {
        [JsonPropertyName("openDays")]
        public List<string>? OpenDays { get; set; }

        [JsonPropertyName("openTime")]
        public string? OpenTime { get; set; }

        [JsonPropertyName("closeTime")]
        public string? CloseTime { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }
    }
}
