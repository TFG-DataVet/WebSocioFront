using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SocioWeb.Domain.Entities;

public class Clinic
{
    // Identificador único de la clínica (PK)
        
        public string Id { get; set; } 

        
        [Required(ErrorMessage = "El nombre de la clínica es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
        public string Name { get; set; } = string.Empty; 

        // Razón Social (Importante para facturación)
        [Required(ErrorMessage = "La Razón Social es obligatoria.")]
        public string Legal_Name { get; set; } = string.Empty; 

        // Identificación fiscal (CIF/NIF)
        [Required(ErrorMessage = "El CIF/NIF es obligatorio.")]
        [StringLength(20)]
        public string Cif_Nif { get; set; } = string.Empty; 

        // Dirección y Ubicación
        public string? Address { get; set; } 
        public string? City { get; set; } 
        public string? Postal_Code { get; set; } 

        // Contacto
        public string? Phone { get; set; } 
        
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string? Contact_Mail { get; set; } 

        // Branding
        public string? Logo_Url { get; set; } 
        public string? Attention_time { get; set; } 
        
        // Estado del SaaS (Suscripción)
        public string? Subscription_State { get; set; } = "Activo"; 
        
    }
