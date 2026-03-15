namespace SocioWeb.ViewModels.Owners;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos;

public class OwnerProfileViewModel : BaseViewModel
{
    private readonly IOwnerService _service;

    public Owner Owner { get; private set; } = new();
    public bool IsEditing { get; set; }

    public OwnerProfileViewModel(IOwnerService service) => _service = service;

    public async Task LoadAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            var found = await _service.GetByIdAsync(id);
            if (found is not null)
                Owner = found;
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar el dueño: {ex.Message}");
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
            Owner.UpdatedAt = DateTime.UtcNow;
            await _service.UpdateAsync(Owner.Id, new OwnerDto());
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