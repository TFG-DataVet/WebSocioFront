using SocioWeb.Entities;
using SocioWeb.Services.AppointmentService;

namespace SocioWeb.ViewModels;


public class PetsMenuVM
{
    private readonly IPetService _petService;

    public List<Pet> Pets { get; private set; } = new();
    public List<Pet> FilteredPets { get; private set; } = new();

    public string NameFilter { get; set; } = string.Empty;
    public string SpecieFilter { get; set; } = string.Empty;

    public HashSet<string> VisibleOwners { get; } = new();

    public PetsMenuVM(IPetService petService)
    {
        _petService = petService;
    }

    public async Task LoadAsync()
    {
        Pets = await _petService.GetAllAsync();
        FilteredPets = Pets;
    }

    public void ApplyFilters()
    {
        FilteredPets = Pets
            .Where(m =>
                (string.IsNullOrWhiteSpace(NameFilter) ||
                 m.Name.Contains(NameFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(SpecieFilter) ||
                 m.Specie.Contains(SpecieFilter, StringComparison.OrdinalIgnoreCase))
            )
            .ToList();
    }

    public void ToggleOwner(string petId)
    {
        if (VisibleOwners.Contains(petId))
            VisibleOwners.Remove(petId);
        else
            VisibleOwners.Add(petId);
    }
}