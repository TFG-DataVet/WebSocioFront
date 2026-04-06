using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using SocioWeb.Domain.Entities;

namespace SocioWeb.Entities.Dtos.PetDto;

public class PetDto
{
    public string ClinicId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    
    public string Species { get; set; } = string.Empty;

    public string Breed { get; set; } = string.Empty;

    public Sex Sex { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? ChipNumber { get; set; }

    public string? AvatarUrl { get; set; }

    public OwnerDto Owner { get; set; } = new();
}