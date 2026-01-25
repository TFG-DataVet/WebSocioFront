namespace SocioWeb.Entities;

public class SurgeryMedication
{
    public string Name { get; set; }

    public string Dosage { get; set; }

    public string Frequency { get; set; }

    public int? DurationInDays { get; set; }

    public string Notes { get; set; }
}