namespace SocioWeb.Entities;

public class Treatment
{
    public string Medication { get; set; }
    public string Dosage { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Notes { get; set; }
}