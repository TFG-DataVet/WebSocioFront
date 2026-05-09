using SocioWeb.Entities.Dtos.ClinicApiDto;
using SocioWeb.Infrastructure.Services;
using SocioWeb.Infrastructure.Services.ClinicService;
using SocioWeb.ViewModels.Shared;

namespace SocioWeb.ViewModels.Clinic;

public class ClinicprofileViewModel : BaseViewModel
{
    private readonly IClinicService   _clinicService;
    private readonly AuthStateService _authState;

    public bool IsEditing { get; set; } = false;

    public string Id          { get; set; } = string.Empty;
    public string Name        { get; set; } = string.Empty;
    public string LegalName   { get; set; } = string.Empty;
    public string CifNif      { get; set; } = string.Empty;
    public string Address     { get; set; } = string.Empty;
    public string City        { get; set; } = string.Empty;
    public string PostalCode  { get; set; } = string.Empty;
    public string Phone       { get; set; } = string.Empty;
    public string ContactMail { get; set; } = string.Empty;
    public string LogoUrl     { get; set; } = string.Empty;
    public string AttentionTime { get; set; } = string.Empty;
    public string SubState    { get; set; } = string.Empty;

    // Raw schedule data preserved for save
    private string       _legalType         = "AUTONOMO";
    private List<string> _scheduleOpenDays  = new();
    private string       _scheduleOpenTime  = "09:00:00";
    private string       _scheduleCloseTime = "18:00:00";
    private string?      _scheduleNotes;

    public ClinicprofileViewModel(IClinicService clinicService, AuthStateService authState)
    {
        _clinicService = clinicService;
        _authState     = authState;
    }

    public async Task LoadAsync(string? clinicId = null)
    {
        var id = clinicId ?? _authState.ClinicId;
        if (string.IsNullOrEmpty(id))
        {
            SetError("No hay sesión activa. Por favor, inicia sesión.");
            return;
        }

        IsLoading = true;
        ClearError();
        try
        {
            var clinic = await _clinicService.GetByIdAsync(id);
            MapFromResponse(clinic);
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar la clínica: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void MapFromResponse(ClinicResponseDto clinic)
    {
        Id          = clinic.ClinicId;
        Name        = clinic.ClinicName;
        LegalName   = clinic.LegalName  ?? string.Empty;
        CifNif      = clinic.LegalNumber ?? string.Empty;
        Address     = clinic.Address?.Street ?? string.Empty;
        City        = clinic.Address?.City   ?? string.Empty;
        PostalCode  = clinic.Address?.PostalCode ?? string.Empty;
        Phone       = clinic.Phone      ?? string.Empty;
        ContactMail = clinic.Email      ?? string.Empty;
        LogoUrl     = clinic.LogoUrl    ?? string.Empty;
        SubState    = FormatStatus(clinic.Status);
        _legalType  = clinic.LegalType  ?? "AUTONOMO";

        if (clinic.Schedule != null)
        {
            _scheduleOpenDays  = clinic.Schedule.OpenDays  ?? new();
            _scheduleOpenTime  = clinic.Schedule.OpenTime  ?? "09:00:00";
            _scheduleCloseTime = clinic.Schedule.CloseTime ?? "18:00:00";
            _scheduleNotes     = clinic.Schedule.Notes;
            AttentionTime      = FormatSchedule(clinic.Schedule);
        }
    }

    private static string FormatStatus(string? status) => status switch
    {
        "ACTIVE"        => "Activo",
        "PENDING_SETUP" => "Pendiente de configuración",
        "DEACTIVATED"   => "Desactivado",
        _               => status ?? "—"
    };

    private static string FormatSchedule(ClinicResponseDto.ScheduleDto s)
    {
        var days  = s.OpenDays is { Count: > 0 } ? string.Join(", ", s.OpenDays) : "—";
        var open  = s.OpenTime?.Length  >= 5 ? s.OpenTime[..5]  : "--:--";
        var close = s.CloseTime?.Length >= 5 ? s.CloseTime[..5] : "--:--";
        return $"{days} · {open}–{close}";
    }

    public async Task SaveAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            var dto = new UpdateClinicDto
            {
                ClinicName        = Name,
                LegalName         = LegalName,
                LegalNumber       = CifNif,
                LegalType         = _legalType,
                Address           = Address,
                City              = City,
                CodePostal        = PostalCode,
                Phone             = Phone,
                Email             = ContactMail,
                LogoUrl           = string.IsNullOrWhiteSpace(LogoUrl) ? null : LogoUrl,
                ScheduleOpenDays  = _scheduleOpenDays,
                ScheduleOpenTime  = _scheduleOpenTime,
                ScheduleCloseTime = _scheduleCloseTime,
                ScheduleNotes     = _scheduleNotes
            };

            var updated = await _clinicService.UpdateAsync(Id, dto);
            MapFromResponse(updated);
            IsEditing = false;
        }
        catch (Exception ex)
        {
            SetError($"Error al guardar: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void Cancel() => IsEditing = false;
}
