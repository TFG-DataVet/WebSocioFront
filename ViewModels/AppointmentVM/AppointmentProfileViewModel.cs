namespace SocioWeb.ViewModels.Appointments;

using SocioWeb.Entities.Dtos.MedicalDto.AppointmentDto;
using SocioWeb.Entities.Models.Appointments;
using SocioWeb.Services.AppointmentService;
using SocioWeb.ViewModels.Shared;

public class AppointmentProfileViewModel : BaseViewModel
{
    private readonly IAppointmentService _service;

    public Appointment? Appointment { get; private set; }
    public bool         IsEditing   { get; set; }

    public AppointmentProfileViewModel(IAppointmentService service) => _service = service;

    public async Task LoadAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            Appointment = await _service.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar la cita: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>Avanza el estado de la cita</summary>
    public async Task UpdateStatusAsync(AppointmentStatus newStatus, string? medicalEmployeeId = null)
    {
        if (Appointment is null) return;
        IsLoading = true;
        ClearError();
        try
        {
            var dto = new UpdateAppointmentStatusDto
            {
                NewStatus         = newStatus,
                MedicalEmployeeId = medicalEmployeeId
            };
            Appointment = await _service.UpdateStatusAsync(Appointment.Id, dto);
            IsEditing = false;
        }
        catch (Exception ex)
        {
            SetError($"Error al actualizar el estado: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task<bool> CancelAsync(string? reason = null)
    {
        if (Appointment is null) return false;
        IsLoading = true;
        ClearError();
        try
        {
            await _service.CancelAsync(Appointment.Id, reason);
            return true;
        }
        catch (Exception ex)
        {
            SetError($"Error al cancelar: {ex.Message}");
            return false;
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void Cancel() => IsEditing = false;
}
