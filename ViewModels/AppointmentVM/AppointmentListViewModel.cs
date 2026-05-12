namespace SocioWeb.ViewModels.Appointments;

using SocioWeb.Entities.Models.Appointments;
using SocioWeb.Services.AppointmentService;
using SocioWeb.ViewModels.Shared;

public class AppointmentListViewModel : BaseViewModel
{
    private readonly IAppointmentService _service;

    public List<Appointment> Appointments         { get; private set; } = new();
    public List<Appointment> FilteredAppointments { get; private set; } = new();

    // Filtros
    public DateOnly?           DateFilter    { get; set; }
    public AppointmentStatus?  StatusFilter  { get; set; }
    public AppointmentType?    TypeFilter    { get; set; }
    public string              SearchFilter  { get; set; } = string.Empty;

    public AppointmentListViewModel(IAppointmentService service) => _service = service;

    // ─── CRUD ────────────────────────────────────────────────────────────────

    public async Task LoadAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            Appointments = await _service.GetAllAsync(DateFilter, StatusFilter, TypeFilter);
            Appointments = Appointments.OrderBy(a => a.ScheduledAt).ToList();
            ApplyLocalFilter();
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

    public async Task CancelAsync(string id, string? reason = null)
    {
        IsLoading = true;
        ClearError();
        try
        {
            await _service.CancelAsync(id, reason);
            Appointments.RemoveAll(a => a.Id == id);
            ApplyLocalFilter();
        }
        catch (Exception ex)
        {
            SetError($"Error al cancelar la cita: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    // ─── Filtros locales ──────────────────────────────────────────────────────

    public void ApplyLocalFilter()
    {
        FilteredAppointments = Appointments
            .Where(a =>
                string.IsNullOrEmpty(SearchFilter) ||
                (a.OwnerName ?? "").Contains(SearchFilter, StringComparison.OrdinalIgnoreCase) ||
                (a.Pet?.Name ?? "").Contains(SearchFilter, StringComparison.OrdinalIgnoreCase) ||
                (a.OwnerPhone ?? "").Contains(SearchFilter, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void ClearFilters()
    {
        DateFilter   = null;
        StatusFilter = null;
        TypeFilter   = null;
        SearchFilter = string.Empty;
        FilteredAppointments = new List<Appointment>(Appointments);
    }
}
