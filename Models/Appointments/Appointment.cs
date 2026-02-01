using System.ComponentModel.DataAnnotations;

namespace SocioWeb.Domain.Entities;

public class Appointment
{

    public string Id { get; set; }
    public DateTime Date { get; set; }
    public string? Reason { get; set; }
    public string PetName { get; set; }
    public string OwnerName { get; set; }
    public string Phone { get; set; }
    public string Status { get; set; }
}