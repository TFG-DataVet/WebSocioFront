namespace SocioWeb.Entities;
using System.ComponentModel.DataAnnotations;
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
    [Display(Name = "Leve")]
    Mild,

    [Display(Name = "Moderada")]
    Moderate,

    [Display(Name = "Severa")]
    Severe,

    [Display(Name = "Anafilaxia")]
    Anaphylaxis
}

public enum AllergyType
{
    [Display(Name = "Alimentaria")]
    Food,

    [Display(Name = "Medicamentos")]
    Medication,

    [Display(Name = "Ambiental")]
    Environmental,

    [Display(Name = "Parásitos")]
    Parasite,

    [Display(Name = "Otra")]
    Other
}

