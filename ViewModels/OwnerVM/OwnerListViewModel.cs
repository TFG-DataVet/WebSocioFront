namespace SocioWeb.ViewModels.Owners;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Domain.Entities;

public class OwnerListViewModel : BaseViewModel
{
    private readonly IOwnerService _service;

    public List<Owner> Owners { get; private set; } = new();
    public List<Owner> FilteredOwners { get; private set; } = new();
    public DateTime? DateSince { get; set; }
    public DateTime? DateTo { get; set; }
    public string CityFilter { get; set; } = "";
    public string PostalCodeFilter { get; set; } = "";
    public HashSet<string> VisiblePets { get; } = new();

    public OwnerListViewModel(IOwnerService service) => _service = service;

    public async Task LoadAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            Owners = await _service.GetAllAsync();
            Owners = Owners.OrderBy(d => d.CreatedAt).ToList();
            FilteredOwners = Owners;
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar dueños: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void ApplyFilters()
    {
        FilteredOwners = Owners
            .Where(d =>
                (!DateSince.HasValue || d.CreatedAt >= DateSince) &&
                (!DateTo.HasValue   || d.CreatedAt <= DateTo) &&
                (string.IsNullOrEmpty(CityFilter) ||
                 d.City.Contains(CityFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(PostalCodeFilter) ||
                 d.PostalCode.Contains(PostalCodeFilter, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    public void TogglePets(string ownerId)
    {
        if (!VisiblePets.Add(ownerId))
            VisiblePets.Remove(ownerId);
    }
}