using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using SocioWeb.Domain.Entities;

namespace SocioWeb.Entities.Dtos.PetDto;

public class PetDto
{
    // ─── RELACIONES ─────────────────────────────

    [Required]
    [JsonPropertyName("ownerId")]
    public string OwnerId { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("clinicId")]
    public string ClinicId { get; set; } = string.Empty;

    // ─── INFO BÁSICA ────────────────────────────

    [Required]
    [StringLength(100)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("species")]
    public string? Species { get; set; }

    [JsonPropertyName("breed")]
    public string? Breed { get; set; }

    [JsonPropertyName("sex")]
    public Sex Sex { get; set; } = Sex.Other;

    [JsonPropertyName("dateOfBirth")]
    public DateTime DateOfBirth { get; set; }

    [JsonPropertyName("chipNumber")]
    public string? ChipNumber { get; set; }

    [JsonPropertyName("avatarUrl")]
    public string? AvatarUrl { get; set; }

    // ─── CLÍNICO (opcional según backend) ──────

    [JsonPropertyName("weight")]
    public double? Weight { get; set; }

    [JsonPropertyName("clinicalInfo")]
    public string? ClinicalInfo { get; set; }

    [JsonPropertyName("allergies")]
    public bool Allergies { get; set; }

    [JsonPropertyName("whichAllergies")]
    public string? WhichAllergies { get; set; }

    [JsonPropertyName("vaccines")]
    public bool Vaccines { get; set; }

    [JsonPropertyName("whichVaccines")]
    public string? WhichVaccines { get; set; }

    [JsonPropertyName("operations")]
    public bool Operations { get; set; }

    [JsonPropertyName("whichOperations")]
    public string? WhichOperations { get; set; }

    [JsonPropertyName("diseases")]
    public bool Diseases { get; set; }

    [JsonPropertyName("whichDiseases")]
    public string? WhichDiseases { get; set; }

    [JsonPropertyName("food")]
    public bool Food { get; set; }

    // ─── SEGURO ────────────────────────────────

    [JsonPropertyName("insurance")]
    public string? Insurance { get; set; }

    [JsonPropertyName("dateLastContact")]
    public DateTime? DateLastContact { get; set; }
}