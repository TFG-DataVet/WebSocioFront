using System.Text.Json.Serialization;

namespace SocioWeb.Entities.Dtos;

public class OwnerDto
{
    [JsonPropertyName("clinicId")]
    public string ClinicId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    // El backend espera "documentId" para el tipo (DNI, NIE...)
    [JsonPropertyName("documentId")]
    public string DocumentId { get; set; } = "DNI";

    [JsonPropertyName("documentNumber")]
    public string DocumentNumber { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("acceptTermsAndCond")]
    public bool AcceptTermsAndCond { get; set; } = true;
}