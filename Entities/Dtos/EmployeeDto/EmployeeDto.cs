namespace SocioWeb.Entities.Dtos.EmployeeDto;

using System;
using System.ComponentModel.DataAnnotations;


    public class EmployeeDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [MaxLength(100, ErrorMessage = "El apellido no puede exceder 100 caracteres")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        public string DocumentType { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de documento es obligatorio")]
        [MaxLength(20, ErrorMessage = "El número de documento no puede exceder 20 caracteres")]
        public string DocumentNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression(@"^[+]?[0-9\s\-\(\)]{7,15}$", ErrorMessage = "Formato de teléfono inválido")]
        public string Phone { get; set; } = string.Empty;

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

        [MaxLength(255, ErrorMessage = "La URL del avatar no puede exceder 255 caracteres")]
        public string? AvatarUrl { get; set; }

        [MaxLength(100, ErrorMessage = "La especialidad no puede exceder 100 caracteres")]
        public string? Speciality { get; set; }

        // Obligatorio solo si Role == "CLINIC_VETERINARIAN"
        public string? LicenseNumber { get; set; }

        [Required(ErrorMessage = "La fecha de contratación es obligatoria")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        public string Role { get; set; } = string.Empty;
        
        public string id { get; set; } = string.Empty;
    }
