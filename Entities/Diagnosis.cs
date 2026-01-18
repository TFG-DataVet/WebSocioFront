namespace SocioWeb.Entities;

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
    Infectious,
    Parasitic,
    Genetic,
    Metabolic,
    Neurological,
    Dermatological,
    Orthopedic,
    Cardiovascular,
    Respiratory,
    Digestive,
    Other
}

public enum DiagnosisSeverity
{
    Mild,
    Moderate,
    Severe,
    Critical
}

