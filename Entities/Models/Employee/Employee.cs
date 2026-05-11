using System.Text.Json.Serialization;

namespace SocioWeb.Entities.Models.Employee;

public class Employee
{
    [JsonPropertyName("employeeId")]   // ✅ el backend devuelve "employeeId"
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("userId")]
    public string? UserId { get; set; }

    [JsonPropertyName("clinicId")]
    public string? ClinicId { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("documentNumber")]
    public DocumentNumberDto? DocumentNumber { get; set; }

    public string? DocumentType        => DocumentNumber?.DocumentType;
    public string? DocumentNumberValue => DocumentNumber?.Number;

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("address")]
    public EmployeeAddressDto? Address { get; set; }

    public string Street     => Address?.Street     ?? string.Empty;
    public string City       => Address?.City       ?? string.Empty;
    public string PostalCode => Address?.PostalCode ?? string.Empty;

    [JsonPropertyName("avatarUrl")]
    public string? AvatarUrl { get; set; }

    [JsonPropertyName("speciality")]
    public string? Speciality { get; set; }

    [JsonPropertyName("licenseNumber")]
    public string? LicenseNumber { get; set; }

    [JsonPropertyName("hireDate")]
    public string? HireDate { get; set; }

    [JsonPropertyName("role")]
    public string? Role { get; set; }

    [JsonPropertyName("active")]
    public bool IsActive { get; set; } = true;

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; } = string.Empty;
}

public class DocumentNumberDto
{
    [JsonPropertyName("documentType")]
    public string? DocumentType { get; set; }

    [JsonPropertyName("documentNumber")]
    public string? Number { get; set; }
}

public class EmployeeAddressDto
{
    [JsonPropertyName("street")]
    public string Street { get; set; } = string.Empty;

    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; } = string.Empty;
}