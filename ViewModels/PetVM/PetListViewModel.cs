using SocioWeb.Entities;

namespace SocioWeb.ViewModels.Pets;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Domain.Entities;

public class PetListViewModel : BaseViewModel
{
    private readonly IPetService _service;

    public List<Pet> Pets { get; private set; } = new();
    public List<Pet> FilteredPets { get; private set; } = new();

    // ─── Filtros ─────────────────────────────────────────────
    public DateTime? DateSince { get; set; }
    public DateTime? DateTo { get; set; }
    public string NameFilter { get; set; } = "";
    public string SpecieFilter { get; set; } = "";
    public string BreedFilter { get; set; } = "";

    public HashSet<string> VisibleOwners { get; } = new();

    public PetListViewModel(IPetService service)
    {
        _service = service;
    }

    // ─── CRUD ────────────────────────────────────────────────

    /// <summary>GET /pet — carga todas las mascotas</summary>
    public async Task LoadAsync()
    {
        IsLoading = true;
        ClearError();

        try
        {
            Pets = await _service.GetAllAsync();
            Pets = Pets.OrderBy(p => p.CreatedAt).ToList();
            FilteredPets = new List<Pet>(Pets);
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar mascotas");
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>DELETE /pet/{id}</summary>
    public async Task DeleteAsync(string id)
    {
        IsLoading = true;
        ClearError();

        try
        {
            await _service.DeleteAsync(id);
            Pets.RemoveAll(p => p.Id == id);
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Error al eliminar la mascota: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    // ─── Filtros ─────────────────────────────────────────────

    public void ApplyFilters()
    {
        FilteredPets = Pets
            .Where(p =>
                (!DateSince.HasValue || p.CreatedAt >= DateSince) &&
                (!DateTo.HasValue   || p.CreatedAt <= DateTo) &&
                (string.IsNullOrEmpty(NameFilter) ||
                    (p.Name ?? "").Contains(NameFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(SpecieFilter) ||
                    (p.Specie ?? "").Contains(SpecieFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(BreedFilter) ||
                    (p.Breed ?? "").Contains(BreedFilter, StringComparison.OrdinalIgnoreCase))
            )
            .ToList();
    }

    public void ClearFilters()
    {
        DateSince    = null;
        DateTo       = null;
        NameFilter   = "";
        SpecieFilter = "";
        BreedFilter  = "";

        FilteredPets = new List<Pet>(Pets);
    }

    // ─── UI ──────────────────────────────────────────────────

    public void ToggleOwner(string petId)
    {
        if (!VisibleOwners.Add(petId))
            VisibleOwners.Remove(petId);
    }
}