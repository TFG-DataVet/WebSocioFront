namespace SocioWeb.ViewModels.Appointments;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Domain.Entities;

public class AppointmentListViewModel : BaseViewModel
{
    private readonly IAppointmentService _service;

    public List<Appointment> Appointments { get; private set; } = new();
    public List<Appointment> FilteredAppointments { get; private set; } = new();

    // Filtros
    public DateTime? DateSince { get; set; }
    public DateTime? DateTo { get; set; }
    public string StatusFilter { get; set; } = "";
    public string SearchFilter { get; set; } = "";

    public List<string> States { get; } = new() { "Pendiente", "Confirmada", "Cancelada" };

    public AppointmentListViewModel(IAppointmentService service) => _service = service;

    // ─── CRUD ────────────────────────────────────────────────────────────────

    public async Task LoadAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            Appointments = await _service.GetAllAsync();
            Appointments = Appointments.OrderBy(a => a.Date).ToList();
            FilteredAppointments = new List<Appointment>(Appointments);
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

    public async Task DeleteAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            await _service.DeleteAsync(id);
            Appointments.RemoveAll(a => a.Id == id);
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Error al eliminar la cita: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    // ─── Filtros ──────────────────────────────────────────────────────────────

    public void ApplyFilters()
    {
        FilteredAppointments = Appointments
            .Where(a =>
                (!DateSince.HasValue || a.Date >= DateSince) &&
                (!DateTo.HasValue   || a.Date <= DateTo) &&
                (string.IsNullOrEmpty(StatusFilter) || a.Status.Equals(StatusFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(SearchFilter) ||
                 (a.PetName ?? "").Contains(SearchFilter, StringComparison.OrdinalIgnoreCase) ||
                 (a.OwnerName ?? "").Contains(SearchFilter, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    public void ClearFilters()
    {
        DateSince     = null;
        DateTo       = null;
        StatusFilter = "";
        SearchFilter = "";
        FilteredAppointments = new List<Appointment>(Appointments);
    }
}