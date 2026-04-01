using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using SocioWeb.Domain.Entities;

namespace SocioWeb.Entities.Dtos.PetDto;

public class PetDto
{
    [Required]
    [JsonPropertyName("clinicId")]
    public string ClinicId { get; set; } = string.Empty;

    [Required] [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("species")]
    public string Species { get; set; } = string.Empty;

    [Required] [JsonPropertyName("breed")] public string Breed { get; set; } = string.Empty;

    [Required] [JsonPropertyName("sex")] public Sex Sex { get; set; }

    [JsonPropertyName("dateOfBirth")] public DateTime? DateOfBirth { get; set; }

    [JsonPropertyName("chipNumber")] public string? ChipNumber { get; set; }

    [JsonPropertyName("avatarUrl")] public string? AvatarUrl { get; set; }

    [Required] [JsonPropertyName("owner")] public OwnerDto Owner { get; set; } = new();
}