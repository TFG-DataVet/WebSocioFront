namespace SocioWeb.ViewModels.Appointments;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Entities.Dtos.MedicalDto.AppointmentDto;

public class AppointmentProfileViewModel : BaseViewModel
{
    private readonly IAppointmentService _service;

    public Appointment Appointment { get; private set; } = new();
    public bool IsEditing { get; set; }

    public AppointmentProfileViewModel(IAppointmentService service) => _service = service;

    public async Task LoadAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            var found = await _service.GetByIdAsync(id);
            if (found is not null)
            {
                Appointment = found;
            }
            else
            {
                Appointment = new Appointment
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now,
                    Status = "Pendiente",
                    CreatedAt = DateTime.UtcNow
                };
                IsEditing = true;
            }
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

    public async Task SaveAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            Appointment.UpdatedAt = DateTime.UtcNow;
            await _service.UpdateAsync(Appointment.Id, new AppointmentDto());
            IsEditing = false;
        }
        catch (Exception ex)
        {
            SetError($"Error al guardar: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task<bool> DeleteAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            await _service.DeleteAsync(Appointment.Id);
            return true;
        }
        catch (Exception ex)
        {
            SetError($"Error al eliminar: {ex.Message}");
            return false;
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void Cancel() => IsEditing = false;
}