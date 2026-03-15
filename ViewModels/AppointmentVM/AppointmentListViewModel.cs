namespace SocioWeb.ViewModels.Appointments;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;

public class AppointmentListViewModel : BaseViewModel
{
    private readonly IAppointmentService _service;

    public List<Appointment> Appointments { get; private set; } = new();
    public List<Appointment> FilteredAppointments { get; private set; } = new();

    public DateTime? DateSince { get; set; }
    public DateTime? DateTo { get; set; }
    public string StatusFilter { get; set; } = "";
    public string SearchFilter { get; set; } = "";
    public List<string> States { get; } = new() { "Pendiente", "Confirmada", "Cancelada" };

    public AppointmentListViewModel(IAppointmentService service) => _service = service;

    public async Task LoadAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            Appointments = await _service.GetAllAsync();
            FilteredAppointments = Appointments;
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar citas: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void ApplyFilters()
    {
        FilteredAppointments = Appointments
            .Where(c =>
                (!DateSince.HasValue || c.Date >= DateSince) &&
                (!DateTo.HasValue   || c.Date <= DateTo) &&
                (string.IsNullOrEmpty(StatusFilter) || c.Status == StatusFilter) &&
                (string.IsNullOrEmpty(SearchFilter) ||
                 c.PetName.Contains(SearchFilter, StringComparison.OrdinalIgnoreCase) ||
                 c.OwnerName.Contains(SearchFilter, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }
}