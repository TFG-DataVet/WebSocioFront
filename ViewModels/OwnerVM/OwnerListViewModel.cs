namespace SocioWeb.ViewModels.Owners;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Domain.Entities;

public class OwnerListViewModel : BaseViewModel
{
    private readonly IOwnerService _service;

    public List<Owner> Owners { get; private set; } = new();
    public List<Owner> FilteredOwners { get; private set; } = new();

    // Filtros
    public DateTime? DateSince { get; set; }
    public DateTime? DateTo { get; set; }
    public string CityFilter { get; set; } = "";
    public string PostalCodeFilter { get; set; } = "";
    public string SearchFilter { get; set; } = "";
    public HashSet<string> VisiblePets { get; } = new();

    public OwnerListViewModel(IOwnerService service) => _service = service;

    // ─── CRUD ─────────────────────────────────────────────────────────────────

    /// <summary>GET /owner — carga todos los dueños.</summary>
    public async Task LoadAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            Owners = await _service.GetAllAsync();
            Owners = Owners.OrderBy(d => d.CreatedAt).ToList();
            FilteredOwners = new List<Owner>(Owners);
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar dueños");
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>DELETE /owner/{id} — elimina un dueño por ID.</summary>
    public async Task DeleteAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            await _service.DeleteAsync(id);
            Owners.RemoveAll(o => o.Id == id);
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Error al eliminar el dueño: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    // ─── Filtros ──────────────────────────────────────────────────────────────

    public void ApplyFilters()
    {
        FilteredOwners = Owners
            .Where(d =>
                (!DateSince.HasValue || d.CreatedAt >= DateSince) &&
                (!DateTo.HasValue   || d.CreatedAt <= DateTo) &&
                (string.IsNullOrEmpty(CityFilter) ||
                 (d.City ?? "").Contains(CityFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(PostalCodeFilter) ||
                 (d.PostalCode ?? "").Contains(PostalCodeFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(SearchFilter) ||
                 ($"{d.Name} {d.LastName}").Contains(SearchFilter, StringComparison.OrdinalIgnoreCase) ||
                 (d.Email ?? "").Contains(SearchFilter, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    public void ClearFilters()
    {
        DateSince        = null;
        DateTo           = null;
        CityFilter       = "";
        PostalCodeFilter = "";
        SearchFilter     = "";
        FilteredOwners   = new List<Owner>(Owners);
    }

    public void TogglePets(string ownerId)
    {
        if (!VisiblePets.Add(ownerId))
            VisiblePets.Remove(ownerId);
    }
}
