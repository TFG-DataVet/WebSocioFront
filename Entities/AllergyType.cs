namespace SocioWeb.Entities;
using System.ComponentModel.DataAnnotations;
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