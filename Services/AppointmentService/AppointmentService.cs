namespace SocioWeb.Services.AppointmentService;

using System;
using System.Collections.Generic;

public class AppointmentService
{
    public List<Appointment> Appointments { get; } = new List<Appointment>();

    public AppointmentService()
    {
        // Citas de ejemplo
        Appointments.Add(new Appointment
        {
            Id = "1",
            PetName = "Firulais",
            OwnerName = "Juan Pérez",
            Phone = "555-1234",
            Start = DateTime.Today.AddHours(10),
            End = DateTime.Today.AddHours(11),
            Reason = "Vacunación",
            Status = "Pendiente"
        });

        Appointments.Add(new Appointment
        {
            Id = "2",
            PetName = "Michi",
            OwnerName = "Ana López",
            Phone = "555-9876",
            Start = DateTime.Today.AddDays(1).AddHours(16),
            End = DateTime.Today.AddDays(1).AddHours(17),
            Reason = "Control general",
            Status = "Confirmada"
        });
    }
    public void Add(Appointment appointment)
    {
        Appointments.Add(appointment);
    }
    
    public void Create(Appointment appoint)
    {
        var appointment = new Appointment
        {
            Id = Guid.NewGuid().ToString(),
            PetName = appoint.PetName,
            OwnerName = appoint.OwnerName,
            Phone = appoint.Phone,
            Start = appoint.Date,
            End = appoint.Date.AddHours(1),
            Reason = appoint.Reason,
            Status = "Pendiente",
            CreatedAt = DateTime.UtcNow
        };
        Add(appointment);
    }
}


