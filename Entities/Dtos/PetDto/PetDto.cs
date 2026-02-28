using System.ComponentModel.DataAnnotations;
using SocioWeb.Domain.Entities;

namespace SocioWeb.Entities.Dtos.PetDto;

public class PetDto
{
    
    
    public string Id { get; set; }

    // Relaciones
    [Required]
    public string IdOwner { get; set; }
    
    public Owner? Owner { get; set; }

    [Required] 
    public string IdClinic { get; set; }
    
    public Clinic? Clinic { get; set; }

    // Información básica
    [Required]  
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public string? AvatarUrl { get; set; } // Para integrar con mobile

    public string? Specie { get; set; }
    public string? Chip { get; set; }

    // Raza, Edad, Sexo, Fecha de nacimiento
    public string? Breed { get; set; }
    public int Age { get; set; }
    public DateTime BirthDate { get; set; } = DateTime.UtcNow;
    public Sex Sex { get; set; } = Sex.Other;

    // Datos clínicos
    public bool Vacines { get; set; } = false;
    public string? WhichVacines { get; set; }

    public bool Operations { get; set; } = false;
    public string? WhichOperations { get; set; }

    public bool Diseases { get; set; } = false;
    public string? WhichDiseases { get; set; }

    public bool Food { get; set; } = false;

    public double? Weight { get; set; } // Peso en kg

    public string? ClinicalInfo { get; set; } // Información libre: sangre, etc.

    public bool Allergies { get; set; } = false;
    public string? WhichAllergies { get; set; }

    // Consultas y Citas
    public List<Inquiry>? Inquiry { get; set; } = new List<Inquiry>();
    public List<Appointment>? Appointments { get; set; } = new List<Appointment>();

    // Información de seguro
    public string? Insurance { get; set; }
    public DateTime? DateLastContact { get; set; }

    // Auditoría
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
