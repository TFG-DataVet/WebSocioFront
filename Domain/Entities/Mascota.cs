using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocioWeb.Domain.Entities;

public class Mascota
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdMascota { get; set; }

    // Relaciones
    [Required]
    public long IdDueno { get; set; }
    [ForeignKey("IdDueno")]
    public Dueno? Dueno { get; set; }

    [Required]
    public long IdClinic { get; set; }
    [ForeignKey("IdClinic")]
    public Clinica? Clinic { get; set; }

    // Información básica
    [Required]  
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    public string? AvatarUrl { get; set; } // Para integrar con mobile

    public string? Especie { get; set; }
    public string? ChipIdentificatorio { get; set; }

    // Raza, Edad, Sexo, Fecha de nacimiento
    public string? Raza { get; set; }
    public int Edad { get; set; }
    public DateTime FechaDeNacimiento { get; set; } = DateTime.UtcNow;
    public Sexo Sexo { get; set; } = Sexo.Otro;

    // Datos clínicos
    public bool Vacunas { get; set; } = false;
    public string? CualesVacunas { get; set; }

    public bool Operaciones { get; set; } = false;
    public string? CualesOperaciones { get; set; }

    public bool Enfermedades { get; set; } = false;
    public string? CualesEnfermedades { get; set; }

    public bool Comida { get; set; } = false;
    public string? MarcaComida { get; set; }

    public double? Peso { get; set; } // Peso en kg

    public string? DatosClinicos { get; set; } // Información libre: sangre, etc.

    public bool Alergias { get; set; } = false;
    public string? TipoAlergias { get; set; }

    // Consultas y Citas
    public List<Consulta>? Consultas { get; set; } = new List<Consulta>();
    public List<Cita>? Citas { get; set; } = new List<Cita>();

    // Información de seguro
    public string? NombreSeguro { get; set; }
    public string? NumeroPoliza { get; set; }
    public DateTime? FechaUltimoContacto { get; set; }

    // Auditoría
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}