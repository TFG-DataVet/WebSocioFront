using SocioWeb.Entities.Dtos.MedicalDto.AppointmentDto;

namespace SocioWeb.Services.AppointmentService;



public interface IAppointmentService
{
    Task<List<Appointment>> GetAllAsync();
    Task<Appointment?> GetByIdAsync(int id);
    Task CreateAsync(AppointmentDto dto);
    Task UpdateAsync(int id, AppointmentDto dto);
    Task DeleteAsync(int id);
}