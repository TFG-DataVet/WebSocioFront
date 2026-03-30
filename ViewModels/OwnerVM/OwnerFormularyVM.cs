using System.Text.Json;

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

    // ─── GET /owner/{id} — carga datos para edición ───────────────────────────

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
                    IdentificationNumber = owner.IdentificationNumber,
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

    // ─── POST /owner — crea un nuevo dueño ────────────────────────────────────

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

    // ─── PUT /owner/{id} — actualiza un dueño existente ───────────────────────

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

    // ─── Helpers ──────────────────────────────────────────────────────────────

    private OwnerDto MapToDto() => new OwnerDto
    {
        Name = FormData.Name ?? string.Empty,
        LastName = FormData.LastName ?? string.Empty,
        Dni = FormData.IdentificationNumber ?? string.Empty, // Mapeo de ID a DNI
        Phone = FormData.Phone,
        Email = FormData.Email ?? string.Empty,
        Address = FormData.Street ?? string.Empty, // Ojo aquí con la estructura
        City = FormData.City ?? string.Empty,
        PostalCode = FormData.PostalCode
    };

    // ─── Form model ───────────────────────────────────────────────────────────

    public class OwnerFormModel
    {
        public string? Name                 { get; set; }
        public string? LastName             { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Email                { get; set; }
        public string? Phone                { get; set; }
        public string? Password             { get; set; }
        public string? Street               { get; set; }
        public string? City                 { get; set; }
        public string? PostalCode           { get; set; }
        public string? Coments              { get; set; }
        public string  Notifications        { get; set; } = "Ninguna";
    }
    
    
}
