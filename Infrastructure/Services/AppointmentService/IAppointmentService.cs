using SocioWeb.Entities.Dtos.MedicalDto.AppointmentDto;
using SocioWeb.Entities.Models.Appointments;

namespace SocioWeb.Services.AppointmentService;

public interface IAppointmentService
{
    /// <summary>GET /appointments — lista con filtros opcionales</summary>
    Task<List<Appointment>> GetAllAsync(
        DateOnly?            date     = null,
        AppointmentStatus?   status   = null,
        AppointmentType?     type     = null,
        string?              ownerId  = null);

    /// <summary>GET /appointments/{id}</summary>
    Task<Appointment?> GetByIdAsync(string id);

    /// <summary>POST /appointments</summary>
    Task<Appointment> CreateAsync(CreateAppointmentDto dto);

    /// <summary>PATCH /appointments/{id}/status</summary>
    Task<Appointment> UpdateStatusAsync(string id, UpdateAppointmentStatusDto dto);

    /// <summary>DELETE /appointments/{id}</summary>
    Task CancelAsync(string id, string? reason = null);
}
