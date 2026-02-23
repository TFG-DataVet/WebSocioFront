using SocioWeb.Domain.Entities;
using SocioWeb.Services.AppointmentService;

namespace SocioWeb.ViewModels.OwnerVM;

public class OwnerPageVM
{
    
    private readonly IOwnerService _ownerService;

    public List<Owner> Owners { get; private set; } = new();
    public List<Owner> FilteredOwners { get; private set; } = new();

    // Filtros
    public DateTime? DateSince { get; set; }
    public DateTime? DateTo { get; set; }
    public string CityFilter { get; set; } = string.Empty;
    public string PostalCodeFilter { get; set; } = string.Empty;

    // Estado UI
    public HashSet<string> VisiblePets { get; } = new();

    public bool IsLoading { get; private set; }
    public string? ErrorMessage { get; private set; }

    public OwnerPageVM(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    public async Task LoadAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = null;

            Owners = await _ownerService.GetAllAsync();
            Owners = Owners.OrderBy(d => d.CreatedAt).ToList();

            FilteredOwners = Owners;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
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
                (!DateSince.HasValue || d.CreatedAt >= DateSince.Value) &&
                (!DateTo.HasValue || d.CreatedAt <= DateTo.Value) &&
                (string.IsNullOrWhiteSpace(CityFilter) ||
                 d.City.Contains(CityFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(PostalCodeFilter) ||
                 d.PostalCode.Contains(PostalCodeFilter, StringComparison.OrdinalIgnoreCase))
            )
            .ToList();
    }

    public void TogglePets(string ownerId)
    {
        if (VisiblePets.Contains(ownerId))
            VisiblePets.Remove(ownerId);
        else
            VisiblePets.Add(ownerId);
    }
}