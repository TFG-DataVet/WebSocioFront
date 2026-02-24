namespace SocioWeb.Entities;

public class Vaccine
{
    public string VaccineName { get; set; }
    public DateTime? ApplicationDate { get; set; }
    public DateTime? NextDoseDate { get; set; }
    public string BatchNumber { get; set; }
    public string Manufacturer { get; set; }
}
