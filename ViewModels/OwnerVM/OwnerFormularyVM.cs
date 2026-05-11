namespace SocioWeb.ViewModels.Owners;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Entities.Dtos;

public class OwnerFormularyVM : BaseViewModel
{
    private readonly IOwnerService _service;

    public OwnerFormModel FormData { get; set; } = new();
    public bool IsSaving { get; private set; }

    public OwnerFormularyVM(IOwnerService service) => _service = service;

    public async Task LoadForEditAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            var owner = await _service.GetByIdAsync(id);
            if (owner is not null)
            {
                FormData = new OwnerFormModel
                {
                    Name                 = owner.Name,
                    LastName             = owner.LastName,
                    DocumentType         = owner.IdentificationType,
                    DocumentNumber       = owner.IdentificationNumber,
                    Email                = owner.Email,
                    Phone                = owner.Phone,
                    Street               = owner.Street,
                    City                 = owner.City,
                    PostalCode           = owner.PostalCode,
                    Coments              = owner.Coments,
                    Notifications        = owner.AcceptedNotification.ToString()
                };
            }
            else
            {
                SetError("No se encontró el dueño.");
            }
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar datos: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task CreateAsync()
    {
        IsSaving = true;
        ClearError();
        try
        {
            var dto = MapToDto();
            await _service.CreateAsync(dto);
        }
        catch (Exception ex)
        {
            SetError($"Error al registrar el dueño: {ex.Message}");
        }
        finally
        {
            IsSaving = false;
        }
    }

    public async Task UpdateAsync(string id)
    {
        IsSaving = true;
        ClearError();
        try
        {
            var dto = MapToDto();
            await _service.UpdateAsync(id, dto);
        }
        catch (Exception ex)
        {
            SetError($"Error al actualizar el dueño: {ex.Message}");
        }
        finally
        {
            IsSaving = false;
        }
    }

    private OwnerDto MapToDto() => new OwnerDto
    {
        // ⚠️ Necesitas el clinicId real — de momento hardcode hasta tener auth
        ClinicId       = FormData.ClinicId ?? "clinic-default",
        Name           = FormData.Name ?? string.Empty,
        LastName       = FormData.LastName ?? string.Empty,
        DocumentId     = FormData.DocumentType ?? "DNI",
        DocumentNumber = FormData.DocumentNumber ?? string.Empty,
        Phone          = FormData.Phone,
        Email          = FormData.Email ?? string.Empty,
        Address        = FormData.Street ?? string.Empty,
        City           = FormData.City ?? string.Empty,
        PostalCode     = FormData.PostalCode,
        Url            = null,
        AcceptTermsAndCond = true
    };

    public class OwnerFormModel
    {
        public string? ClinicId          { get; set; }
        public string? Name              { get; set; }
        public string? LastName          { get; set; }
        // Tipo documento: DNI, NIE, NIF, PASAPORTE
        public string  DocumentType      { get; set; } = "DNI";
        public string? DocumentNumber    { get; set; }
        public string? Email             { get; set; }
        public string? Phone             { get; set; }
        public string? Street            { get; set; }
        public string? City              { get; set; }
        public string? PostalCode        { get; set; }
        public string? Coments           { get; set; }
        public string  Notifications     { get; set; } = "Ninguna";
    }
}