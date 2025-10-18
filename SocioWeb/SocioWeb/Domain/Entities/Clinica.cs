using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SocioWeb.Domain.Entities;

public class Clinica
{
    // Identificador único de la clínica (PK)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Clinica { get; set; } 

        
        [Required(ErrorMessage = "El nombre de la clínica es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty; 

        // Razón Social (Importante para facturación)
        [Required(ErrorMessage = "La Razón Social es obligatoria.")]
        public string Razon_Social { get; set; } = string.Empty; 

        // Identificación fiscal (CIF/NIF)
        [Required(ErrorMessage = "El CIF/NIF es obligatorio.")]
        [StringLength(20)]
        public string Cif_Nif { get; set; } = string.Empty; 

        // Dirección y Ubicación
        public string? Direccion { get; set; } 
        public string? Ciudad { get; set; } 
        public string? Codigo_Postal { get; set; } 

        // Contacto
        public string? Telefono { get; set; } 
        
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string? Email_Contacto { get; set; } 

        // Branding
        public string? Logo_Url { get; set; } 
        public string? Horario_Atencion { get; set; } 

        // Geolocalización
        [Column(TypeName = "decimal(10, 8)")]
        public decimal? Latitud { get; set; } 
        
        [Column(TypeName = "decimal(11, 8)")]
        public decimal? Longitud { get; set; } 

        // Estado del SaaS (Suscripción)
        public string? Estado_Suscripcion { get; set; } = "Activo"; 

        // Trazabilidad
        [DataType(DataType.Date)]
        public DateTime Fecha_Alta { get; set; } = DateTime.Now; 
    }
