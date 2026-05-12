using System.Text.Json.Serialization;
using SocioWeb.Entities.Models.Appointments;
using SocioWeb.Infrastructure.Converters;

public class Appointment
{
    public string            Id                 { get; set; } = string.Empty;
    public string            ClinicId           { get; set; } = string.Empty;
    public bool              Emergency          { get; set; }
    public AppointmentType   Type               { get; set; }
    public AppointmentStatus Status             { get; set; }
    [JsonConverter(typeof(FlexibleDateTimeConverter))]
    public DateTime          ScheduledAt        { get; set; }

    // Propietario
    public string?  OwnerId    { get; set; }
    public string?  OwnerName  { get; set; }
    public string?  OwnerEmail { get; set; }
    public string?  OwnerPhone { get; set; }

    // Mascota (snapshot)
    public PetSnapshot? Pet { get; set; }

    // Empleados
    public string? CreationEmployeeId { get; set; }
    public string? MedicalEmployeeId  { get; set; }

    // Extras
    public string?       Notes      { get; set; }
    public List<string>? ProductIds { get; set; }
    public AppointmentSource Source { get; set; }

    [JsonConverter(typeof(FlexibleNullableDateTimeConverter))]
    public DateTime? CreatedAt { get; set; }
    [JsonConverter(typeof(FlexibleNullableDateTimeConverter))]
    public DateTime? UpdatedAt { get; set; }

    // Helpers para el calendario / scheduler
    [JsonIgnore]
    public DateTime Start => ScheduledAt;
    [JsonIgnore]
    public DateTime End   => ScheduledAt.AddMinutes(30);
    [JsonIgnore]
    public string   Text  => $"{OwnerName ?? "—"} · {Pet?.Name ?? "—"}";
}

public class PetSnapshot
{
    public string? PetId   { get; set; }
    public string? Name    { get; set; }
    public string? Species { get; set; }
}
