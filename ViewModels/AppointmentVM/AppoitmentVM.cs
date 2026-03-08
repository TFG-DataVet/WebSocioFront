using Microsoft.AspNetCore.Components;

namespace SocioWeb.ViewModels.AppointmentVM;

public class AppoitmentVM
{
    
    // ================= ESTADO =================

    public List<Appointment> Appointments { get; private set; } = new();
    public List<Appointment> FilteredAppointments { get; private set; } = new();

    public DateTime? DateSince { get; set; }
    public DateTime? DateTo { get; set; }
    public string? StatusFilter { get; set; }
    public string SearchFilter { get; set; } = "";

    public List<string> States { get; } =
        new() { "Pendiente", "Confirmada", "Cancelada" };

    // ================= LOAD =================

    public Task LoadAsync()
    {
        Appointments = new List<Appointment>
        {
            new Appointment
            {
                Id = "1",
                PetName = "Firulais",
                OwnerName = "Juan Pérez",
                Phone = "555-1234",
                Date = DateTime.Now.AddDays(1),
                Reason = "Vacunación",
                Status = "Pendiente"
            },
            new Appointment
            {
                Id = "2",
                PetName = "Michi",
                OwnerName = "Ana López",
                Phone = "555-9876",
                Date = DateTime.Now.AddDays(2),
                Reason = "Control general",
                Status = "Confirmada"
            }
        };

        FilteredAppointments = Appointments;

        return Task.CompletedTask;
    }

    // ================= FILTROS =================

    public void ApplyFilters()
    {
        FilteredAppointments = Appointments
            .Where(c =>
                (!DateSince.HasValue || c.Date >= DateSince) &&
                (!DateTo.HasValue || c.Date <= DateTo) &&
                (string.IsNullOrEmpty(StatusFilter) || c.Status == StatusFilter) &&
                (string.IsNullOrEmpty(SearchFilter) ||
                 c.PetName.Contains(SearchFilter, StringComparison.OrdinalIgnoreCase) ||
                 c.OwnerName.Contains(SearchFilter, StringComparison.OrdinalIgnoreCase))
            )
            .ToList();
    }
}