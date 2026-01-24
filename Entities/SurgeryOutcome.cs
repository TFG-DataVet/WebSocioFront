namespace SocioWeb.Entities;
using System.ComponentModel.DataAnnotations;
public enum SurgeryOutcome
{
    [Display(Name = "Exitosa")]
    Successful,

    [Display(Name = "Exitosa con complicaciones")]
    SuccessfulWithComplications,

    [Display(Name = "Fallida")]
    Failed,

    [Display(Name = "En progreso")]
    InProgress
}