namespace SocioWeb.ViewModels.Appointments;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Entities.Dtos.MedicalDto.AppointmentDto;

public class CreateAppointmentViewModel : BaseViewModel
{
    private readonly IAppointmentService _service;

    public Appointment Appointment { get; set; } = new()
    {
        Id = Guid.NewGuid().ToString(),
        Date = DateTime.Now,
        Status = "Pendiente",
        CreatedAt = DateTime.UtcNow
    };

    public CreateAppointmentViewModel(IAppointmentService service) => _service = service;

    public async Task SaveAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            await _service.CreateAsync(new AppointmentDto());
        }
        catch (Exception ex)
        {
            SetError($"Error al crear la cita: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
}