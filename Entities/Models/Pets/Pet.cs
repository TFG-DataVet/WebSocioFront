using System.Text.Json.Serialization;
using SocioWeb.Domain.Entities;
using SocioWeb.Infrastructure.Converters;

namespace SocioWeb.Entities;

public class Pet
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("clinicId")]
    public string IdClinic { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("species")]
    public string? Specie { get; set; }

    [JsonPropertyName("breed")]
    public string? Breed { get; set; }

    [JsonPropertyName("sex")]
    public Sex Sex { get; set; } = Sex.UNKNOWN;

    [JsonPropertyName("dateOfBirth")]
    public string? BirthDate { get; set; }   // string para evitar problemas con LocalDate

    [JsonPropertyName("ageInYears")]
    public int Age { get; set; }

    [JsonPropertyName("chipNumber")]
    public string? Chip { get; set; }

    [JsonPropertyName("avatarUrl")]
    public string? AvatarUrl { get; set; }

    [JsonPropertyName("owner")]
    public PetOwnerInfo? Owner { get; set; }

    // Helpers para acceder al owner fácilmente
    public string IdOwner => Owner?.OwnerId ?? string.Empty;

    [JsonPropertyName("active")]
    public bool IsActive { get; set; } = true;

    [JsonPropertyName("createdAt")]
    [JsonConverter(typeof(FlexibleNullableDateTimeConverter))]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    [JsonConverter(typeof(FlexibleNullableDateTimeConverter))]
    public DateTime? UpdatedAt { get; set; }
}

public class PetOwnerInfo
{
    [JsonPropertyName("ownerId")]
    public string OwnerId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("fullName")]
    public string FullName { get; set; } = string.Empty;
}