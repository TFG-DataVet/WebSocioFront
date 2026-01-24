namespace SocioWeb.Entities;
using System.ComponentModel.DataAnnotations;
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