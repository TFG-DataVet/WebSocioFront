using System.ComponentModel.DataAnnotations;


public class Appointment
{

    public string Id { get; set; }
    public DateTime Date { get; set; }
    public string? Reason { get; set; }
    public string PetName { get; set; }
    public string OwnerName { get; set; }
    public string Phone { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? Text { get; set; }

}