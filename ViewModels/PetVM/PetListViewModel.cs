namespace SocioWeb.ViewModels.Pets;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Entities;

public class PetListViewModel : BaseViewModel
{
    private readonly IPetService _service;

    public List<Pet> Pets { get; private set; } = new();
    public List<Pet> FilteredPets { get; private set; } = new();
    public string NameFilter { get; set; } = "";
    public string SpecieFilter { get; set; } = "";
    public HashSet<string> VisibleOwners { get; } = new();

    public PetListViewModel(IPetService service) => _service = service;

    public async Task LoadAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            Pets = await _service.GetAllAsync();
            FilteredPets = Pets;
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar mascotas: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void ApplyFilters()
    {
        FilteredPets = Pets
            .Where(m =>
                (string.IsNullOrEmpty(NameFilter) ||
                 m.Name.Contains(NameFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(SpecieFilter) ||
                 m.Specie.Contains(SpecieFilter, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    public void ToggleOwner(string petId)
    {
        if (!VisibleOwners.Add(petId))
            VisibleOwners.Remove(petId);
    }
}