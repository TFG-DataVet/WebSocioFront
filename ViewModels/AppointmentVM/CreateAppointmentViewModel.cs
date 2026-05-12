namespace SocioWeb.ViewModels.Appointments;

using SocioWeb.Entities.Dtos.MedicalDto.AppointmentDto;
using SocioWeb.Entities.Models.Appointments;
using SocioWeb.Services.AppointmentService;
using SocioWeb.ViewModels.Shared;

public class CreateAppointmentViewModel : BaseViewModel
{
    private readonly IAppointmentService _service;

    public CreateAppointmentDto Form { get; set; } = new()
    {
        ScheduledAt = DateTime.Now.AddHours(1),
        Type        = AppointmentType.RUTINA,
        Source      = AppointmentSource.PANEL
    };

    /// <summary>Cita creada tras guardar con éxito</summary>
    public Appointment? CreatedAppointment { get; private set; }

    public CreateAppointmentViewModel(IAppointmentService service) => _service = service;

    public async Task<bool> SaveAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            CreatedAppointment = await _service.CreateAsync(Form);
            return true;
        }
        catch (Exception ex)
        {
            SetError($"Error al crear la cita: {ex.Message}");
            return false;
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void Reset()
    {
        Form = new CreateAppointmentDto
        {
            ScheduledAt = DateTime.Now.AddHours(1),
            Type        = AppointmentType.RUTINA,
            Source      = AppointmentSource.PANEL
        };
        CreatedAppointment = null;
        ClearError();
    }
}
