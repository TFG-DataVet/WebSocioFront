// Servicio local en memoria — reemplazado por AppointmentsApiService (IAppointmentService).
// Se mantiene registrado en Program.cs para no romper otras dependencias posibles.
namespace SocioWeb.Services.AppointmentService;

[Obsolete("Usa IAppointmentService (AppointmentsApiService) en su lugar.")]
public class AppointmentService
{
    public List<Appointment> Appointments { get; } = new();
}
