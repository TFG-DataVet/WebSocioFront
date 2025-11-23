using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocioWeb.Domain.Entities;

public class Dueno
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    // Relación con Clinica
    [Required(ErrorMessage = "La clínica es obligatoria.")]
    public long IdClinic { get; set; }

    [ForeignKey("IdClinic")]
    public Clinica? Clinic { get; set; }

    // Relación con Mascotas
    public List<long>? IdMascotas { get; set; } = new List<long>();

    // Datos personales
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(50, ErrorMessage = "El apellido no puede exceder 50 caracteres.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "Formato de email inválido.")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Formato de teléfono inválido.")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string PasswordHash { get; set; } = string.Empty;

    // Estado de la app y notificaciones
    public StatusApp StatusApp { get; set; } = StatusApp.Activo;
    public AcceptedNotification AcceptedNotification { get; set; } = AcceptedNotification.Ninguna;

    // Auditoría
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}