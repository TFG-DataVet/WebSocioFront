namespace SocioWeb.Entities;

public class Allergy
{
    public string Allergen { get; set; }
    public AllergyType Type { get; set; }
    public AllergySeverity Severity { get; set; }
    public List<string> Reactions { get; set; }
    public bool LifeThreatening { get; set; }
    public DateTime IdentifiedAt { get; set; }
    
    public string Notes { get; set; }
}

public enum AllergySeverity
{
    Mild,
    Moderate,
    Severe,
    Anaphylaxis
}

public enum AllergyType
{
    Food,
    Medication,
    Environmental,
    Parasite,
    Other
}
