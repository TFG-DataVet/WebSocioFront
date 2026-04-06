using SocioWeb.Domain.Entities;

namespace SocioWeb.Entities.Dtos.PetDto;

public class CreatePetRequest
{
    public string ClinicId { get; set; } = "";
    public string Name { get; set; } = "";
    public string Species { get; set; } = "";
    public string Breed { get; set; } = "";
    public Sex Sex { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public string? ChipNumber { get; set; }
    public string? AvatarUrl { get; set; }

    public OwnerRequest Owner { get; set; } = new();
}