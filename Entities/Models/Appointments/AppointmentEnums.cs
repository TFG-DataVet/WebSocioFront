using System.Text.Json.Serialization;

namespace SocioWeb.Entities.Models.Appointments;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AppointmentStatus
{
    RESERVADA,
    CLIENTE_LLEGADO,
    PROXIMO_A_ATENDER,
    EN_CONSULTA,
    FINALIZADA,
    REQUIERE_SEGUIMIENTO,
    CANCELADA
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AppointmentType
{
    RUTINA,
    EXAMEN,
    VACUNAS,
    EMERGENCIA,
    BAÑO,
    CIRUGIA,
    OTRO
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AppointmentSource
{
    PANEL,
    WEB,
    TELEFONO,
    PRESENCIAL
}
