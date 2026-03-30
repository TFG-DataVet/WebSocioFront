using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using SocioWeb.Entities;

namespace SocioWeb.Domain.Entities;

public class Owner
{
    [JsonPropertyName("ownerId")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("clinicId")]
    public string IdClinic { get; set; } = string.Empty;

    [JsonPropertyName("firstName")]
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    [Required(ErrorMessage = "El apellido es obligatorio.")]
    public string LastName { get; set; } = string.Empty;
    
    [JsonPropertyName("documentType")]
    [Required(ErrorMessage = "El número de documento es obligatorio.")]
    public string IdentificationType { get; set; } = string.Empty;

    [JsonPropertyName("documentNumber")]
    [Required(ErrorMessage = "El número de documento es obligatorio.")]
    public string IdentificationNumber { get; set; } = string.Empty;

    
    [JsonPropertyName("email")]
    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "Formato de email inválido.")]
    public string Email { get; set; } = string.Empty;

    // Nota: Java usa un objeto Phone, pero si recibes el string directamente:
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    // Sincronización con el objeto Address de Java
    [JsonPropertyName("Street")]
    public string Street { get; set; } = string.Empty;

    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; } = string.Empty;

    [JsonPropertyName("avatarUrl")]
    public string? Url { get; set; }

    [JsonPropertyName("active")]
    public bool IsActive { get; set; } = true;

    // Mapeo de Enums (asegúrate de que coincidan con los valores de Java)
    public StatusApp StatusApp => IsActive ? StatusApp.Active : StatusApp.Inactive;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonPropertyName("updateAt")]
    public DateTime? UpdatedAt { get; set; }

    // Listas anidadas
    public List<Pet> Pet { get; set; } = new();
    public List<LogEntry> Historic { get; set; } = new();
    
    public string Coments { get; set; } = string.Empty;
    public AcceptedNotification AcceptedNotification { get; set; } = AcceptedNotification.Ninguna;
    
    
    [JsonPropertyName("acceptTermsAndCond")]
    [Required(ErrorMessage = "El número de documento es obligatorio.")]
    public bool acceptTermsAndCond {get; set;}
}