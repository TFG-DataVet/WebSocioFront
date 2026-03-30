namespace SocioWeb.Entities.Dtos.PetDto;


public class CreatePetRequestDto
{
    public string ClinicId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Species { get; set; }
    public string? Breed { get; set; }
    public string? Sex { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? ChipNumber { get; set; }
    public string? AvatarUrl { get; set; }

    public OwnerDto Owner { get; set; } = new();

    public class OwnerDto
    {
        public string OwnerId { get; set; } = string.Empty;
        public string? OwnerName { get; set; }
        public string? OwnerLastName { get; set; }
        public string? OwnerPhone { get; set; }
    }
}