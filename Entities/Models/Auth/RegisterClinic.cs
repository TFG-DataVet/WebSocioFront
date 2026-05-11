using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocioWeb.Entities.Models.Auth;

public class RegisterClinic
{
    [Required(ErrorMessage = "El nombre de la clinica es obligatorio")]
    public string ClinicName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre del registtrante es obligatorio")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]    // ← añadir
    [Required(ErrorMessage = "El apellido del registrante es obligatorio")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El e-mail es obligatorio")]
    [EmailAddress(ErrorMessage = "La direccion no valida con el e-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "El numero de telefono del registrante es obligatorio")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [MinLength(8, ErrorMessage = "Minimo 8 caracteres")]
    public string Password { get; set; } = string.Empty;
}