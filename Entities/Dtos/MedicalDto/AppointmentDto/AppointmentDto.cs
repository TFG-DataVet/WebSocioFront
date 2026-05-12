using SocioWeb.Entities.Models.Appointments;

namespace SocioWeb.Entities.Dtos.MedicalDto.AppointmentDto;

/// <summary>
/// Payload para POST /appointments
/// </summary>
public class CreateAppointmentDto
{
    public bool   Emergency   { get; set; }
    public AppointmentType Type { get; set; }

    /// <summary>ISO 8601 — debe ser en el futuro</summary>
    public DateTime ScheduledAt { get; set; }

    // Propietario
    public string? OwnerId    { get; set; }
    public string? OwnerName  { get; set; }
    public string? OwnerEmail { get; set; }
    public string? OwnerPhone { get; set; }

    // Mascota
    public string? PetId      { get; set; }
    public string? PetName    { get; set; }
    public string? PetSpecies { get; set; }

    // Empleados
    public string  CreationEmployeeId { get; set; } = string.Empty;
    public string? MedicalEmployeeId  { get; set; }

    // Opcionales
    public string?       Notes      { get; set; }
    public List<string>? ProductIds { get; set; }
    public AppointmentSource Source { get; set; } = AppointmentSource.PANEL;
}

/// <summary>
/// Payload para PATCH /appointments/{id}/status
/// </summary>
public class UpdateAppointmentStatusDto
{
    public AppointmentStatus NewStatus        { get; set; }
    public string?           MedicalEmployeeId { get; set; }
}

/// <summary>
/// Payload para DELETE /appointments/{id}  (body opcional)
/// </summary>
public class CancelAppointmentDto
{
    public string? Reason { get; set; }
}

// Alias de compatibilidad para código antiguo
public class AppointmentDto : CreateAppointmentDto { }
