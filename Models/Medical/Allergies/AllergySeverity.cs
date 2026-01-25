namespace SocioWeb.Entities;
using System.ComponentModel.DataAnnotations;
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