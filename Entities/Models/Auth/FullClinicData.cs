using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SocioWeb.Entities.Models.Enums;

namespace SocioWeb.Entities.Models.Auth;

public class FullClinicData
{
    [Required]
    public Guid ClinicId { get; set; }

    [Required(ErrorMessage = "El nombre legal es obligatorio")]
    public string LegalName { get; set; }

    [Required(ErrorMessage = "El número legal es obligatorio")]
    public string LegalNumber { get; set; }

    [Required(ErrorMessage = "El tipo legal es obligatorio")]
    public LegalType? LegalType { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria")]
    public string Address { get; set; }

    [Required(ErrorMessage = "La ciudad es obligatoria")]
    public string City { get; set; }

    [Required(ErrorMessage = "El código postal es obligatorio")]
    public string CodePostal { get; set; }

    [Required(ErrorMessage = "El teléfono de la clínica es obligatorio")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "El email de la clínica es obligatorio")]
    public string Email { get; set; }

    [Required(ErrorMessage = "La URL de la clínica es obligatoria")]
    public string Url { get; set; }

    [Required(ErrorMessage = "Los días de apertura son obligatorios")]
    public List<string> ScheduleOpenDays { get; set; }

    [Required(ErrorMessage = "El horario de apertura es obligatorio")]
    public DateTime ScheduleOpenTime { get; set; }

    [Required(ErrorMessage = "El horario de cierre es obligatorio")]
    public DateTime ScheduleCloseTime { get; set; }

    [Required(ErrorMessage = "Las notas de horario son obligatorias")]
    public string ScheduleNotes { get; set; }

    [Required(ErrorMessage = "El tipo de documento del responsable es obligatorio")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DocumentType? OwnerDocumentType { get; set; }

    [Required(ErrorMessage = "El número de documento del responsable es obligatorio")]
    public string OwnerDocumentNumber { get; set; }

    [Required(ErrorMessage = "La dirección del responsable es obligatoria")]
    public string OwnerAddress { get; set; }

    [Required(ErrorMessage = "La ciudad del responsable es obligatoria")]
    public string OwnerCity { get; set; }

    [Required(ErrorMessage = "El código postal del responsable es obligatorio")]
    public string OwnerCodePostal { get; set; }

    [Required(ErrorMessage = "El avatar del responsable es obligatorio")]
    public string OwnerAvatarUrl { get; set; }

    // Para LegalType
    public List<DropDownItem<LegalType?>> LegalTypes { get; set; } = Enum.GetValues(typeof(LegalType))
        .Cast<LegalType>()
        .Select(e => new DropDownItem<LegalType?>
        {
            Name = e.ToString().Replace("_", " "),
            Value = e
        }).ToList();

// Para DocumentType
    public List<DropDownItem<DocumentType?>> DocumentTypes { get; set; } = Enum.GetValues(typeof(DocumentType))
        .Cast<DocumentType>()
        .Select(e => new DropDownItem<DocumentType?>
        {
            Name = e.ToString(),
            Value = e
        }).ToList();
}

public class DropDownItem<T>
{
    public string Name { get; set; }
    public T Value { get; set; }
}
