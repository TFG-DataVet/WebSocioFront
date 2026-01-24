namespace SocioWeb.Entities;
using System.ComponentModel.DataAnnotations;
public enum SurgeryType
{
    [Display(Name = "Preventiva")]
    Preventive,

    [Display(Name = "Correctiva")]
    Corrective,

    [Display(Name = "Emergencia")]
    Emergency,

    [Display(Name = "Diagnóstica")]
    Diagnostic,

    [Display(Name = "Reconstructiva")]
    Reconstructive
}