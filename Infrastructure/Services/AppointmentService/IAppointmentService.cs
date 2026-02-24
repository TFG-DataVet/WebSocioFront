using SocioWeb.Entities.Dtos.MedicalDto.AppointmentDto;

namespace SocioWeb.Services.AppointmentService;



public interface IAppointmentService
{
    Task<List<Appointment>> GetAllAsync();
    Task<Appointment?> GetByIdAsync(String id);
    Task CreateAsync(AppointmentDto dto);
    Task UpdateAsync(String id, AppointmentDto dto);
    Task DeleteAsync(String id);
}