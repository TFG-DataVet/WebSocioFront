using SocioWeb.ViewModels.Shared;

namespace SocioWeb.ViewModels.Clinic;

public class ClinicprofileViewModel : BaseViewModel
{
    // ── Estado de UI ────────────────────────────────────────────────
    public bool IsEditing { get; set; } = false;

    // ── Datos de la clínica (enlazados al formulario) ────────────────
    public string Id           { get; set; } = string.Empty;
    public string Name         { get; set; } = string.Empty;
    public string LegalName    { get; set; } = string.Empty;
    public string CifNif       { get; set; } = string.Empty;
    public string Address      { get; set; } = string.Empty;
    public string City         { get; set; } = string.Empty;
    public string PostalCode   { get; set; } = string.Empty;
    public string Phone        { get; set; } = string.Empty;
    public string ContactMail  { get; set; } = string.Empty;
    public string LogoUrl      { get; set; } = string.Empty;
    public string AttentionTime{ get; set; } = string.Empty;
    public string SubState     { get; set; } = string.Empty;

    // ── Carga simulada ──────────────────────────────────────────────
    public async Task LoadAsync(string clinicId)
    {
        IsLoading = true;
        ClearError();
        try
        {
            await Task.Delay(80); // simula llamada a API

            Id            = clinicId;
            Name          = "Clínica PetCare";
            LegalName     = "PetCare Veterinaria S.L.";
            CifNif        = "B-12345678";
            Address       = "Calle Gran Vía, 42";
            City          = "Madrid";
            PostalCode    = "28013";
            Phone         = "+34 91 123 45 67";
            ContactMail   = "info@petcare.es";
            LogoUrl       = "";
            AttentionTime = "Lunes–Viernes 9:00–20:00 · Sábados 9:00–14:00";
            SubState      = "Activo";
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar la clínica");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task SaveAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            await Task.Delay(120); // simula PUT a API
            // TODO: await _clinicService.UpdateAsync(Id, BuildDto());
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
