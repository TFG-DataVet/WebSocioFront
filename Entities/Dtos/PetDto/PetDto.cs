using System.Text.Json.Serialization;
using SocioWeb.Domain.Entities;

namespace SocioWeb.Entities.Dtos.PetDto;

public class PetDto
{
    [JsonPropertyName("clinicId")]
    public string ClinicId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("species")]
    public string Species { get; set; } = string.Empty;

    [JsonPropertyName("breed")]
    public string Breed { get; set; } = string.Empty;

    [JsonPropertyName("sex")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Sex Sex { get; set; }

    // Formato "yyyy-MM-dd" que espera LocalDate del backend
    [JsonPropertyName("dateOfBirth")]
    public string? DateOfBirth { get; set; }

    [JsonPropertyName("chipNumber")]
    public string? ChipNumber { get; set; }

    [JsonPropertyName("avatarUrl")]
    public string? AvatarUrl { get; set; }

    [JsonPropertyName("owner")]
    public PetOwnerDto Owner { get; set; } = new();
}

// DTO del owner embebido — nombres exactos que espera el backend
public class PetOwnerDto
{
    [JsonPropertyName("ownerId")]
    public string OwnerId { get; set; } = string.Empty;

    [JsonPropertyName("ownerName")]
    public string OwnerName { get; set; } = string.Empty;

    [JsonPropertyName("ownerLastName")]
    public string OwnerLastName { get; set; } = string.Empty;

    [JsonPropertyName("ownerPhone")]
    public string OwnerPhone { get; set; } = string.Empty;
}