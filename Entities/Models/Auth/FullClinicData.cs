using System.ComponentModel.DataAnnotations;
using SocioWeb.Entities.Models.Enums;

namespace SocioWeb.Entities.Models.Auth;

public class FullClinicData
{
    [Required]
    public Guid ClinicId { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string LegalName { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string LegalNumber { get; set; }    
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public LegalType? LegalType { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string Address { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string City { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string CodePostal { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string Phone { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string Url { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public List<string> scheduleOpenDays { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public DateTime scheduleOpenTime { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public DateTime scheduleCloseTime { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string scheduleNotes { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public DocumentEnum documentType { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string documentNumber  { get; set; }

    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string OwnerAddress { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string OwnerCity { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string OwnerCodePostal { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public DateTime OwnerHireDate { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string OwnerAvatarUrl { get; set; }
    
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string OwnerSpecialty { get; set; }
    
    public List<LegalTypeItem> LegalTypes { get; set; } = Enum.GetValues(typeof(LegalType))
        .Cast<LegalType>()
        .Select(e => new LegalTypeItem 
        { 
            Name = e.ToString().Replace("_", " "), // Quita los guiones bajos para que se vea bien
            Value = e 
        }).ToList();
    
}

public class LegalTypeItem
{
    public string Name { get; set; }
    public LegalType Value { get; set; }
}