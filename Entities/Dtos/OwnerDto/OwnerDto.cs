using SocioWeb.Domain.Entities;

namespace SocioWeb.Entities.Dtos;
using System.ComponentModel.DataAnnotations;

public class OwnerDto
{
    
    public string Id { get; set; }
    
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [MaxLength(50, ErrorMessage = "El apellido no puede exceder 50 caracteres")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El DNI es obligatorio")]
    [MaxLength(10, ErrorMessage = "El DNI no puede exceder 10 caracteres")]
    public string Dni { get; set; } = string.Empty;

    [RegularExpression(@"^[+]?[0-9\s\-\(\)]{7,15}$", ErrorMessage = "Formato de teléfono inválido")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    [MaxLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La dirección es obligatoria")]
    [MaxLength(200, ErrorMessage = "La dirección no puede exceder 200 caracteres")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "La ciudad es obligatoria")]
    [MaxLength(50, ErrorMessage = "La ciudad no puede exceder 50 caracteres")]
    public string City { get; set; } = string.Empty;

    [MaxLength(10, ErrorMessage = "El código postal no puede exceder 10 caracteres")]
    public string? PostalCode { get; set; }

    public string? Url { get; set; }
}



