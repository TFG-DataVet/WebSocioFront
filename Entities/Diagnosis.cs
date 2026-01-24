namespace SocioWeb.Entities;
using System.ComponentModel.DataAnnotations;
public class Diagnosis
{
    public string DiagnosisName { get; set; }
    public DiagnosisCategory Category { get; set; }
    public string Description { get; set; }
    public DiagnosisSeverity Severity { get; set; }
    public DateTime DiagnosedAt { get; set; }
    public bool Chronic { get; set; }
    public bool Contagious { get; set; }
    public List<string> Symptoms { get; set; }
    public List<string> Recommendations { get; set; }
    public bool FollowUpRequired { get; set; }
    public DateTime? FollowUpDate { get; set; }
}

public enum DiagnosisCategory
{
    [Display(Name = "Infecciosa")]
    Infectious,

    [Display(Name = "Parásita")]
    Parasitic,

    [Display(Name = "Genética")]
    Genetic,

    [Display(Name = "Metabólica")]
    Metabolic,

    [Display(Name = "Neurológica")]
    Neurological,

    [Display(Name = "Dermatológica")]
    Dermatological,

    [Display(Name = "Ortopédica")]
    Orthopedic,

    [Display(Name = "Cardiovascular")]
    Cardiovascular,

    [Display(Name = "Respiratoria")]
    Respiratory,

    [Display(Name = "Digestiva")]
    Digestive,

    [Display(Name = "Otra")]
    Other
}

public enum DiagnosisSeverity
{
    [Display(Name = "Leve")]
    Mild,

    [Display(Name = "Moderada")]
    Moderate,

    [Display(Name = "Severa")]
    Severe,

    [Display(Name = "Crítica")]
    Critical
}

