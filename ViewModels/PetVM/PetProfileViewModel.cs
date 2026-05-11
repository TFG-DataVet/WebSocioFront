namespace SocioWeb.ViewModels.Pets;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Entities;
using SocioWeb.Entities.Dtos.PetDto;

public class PetProfileViewModel : BaseViewModel
{
    private readonly IPetService _service;

    public Pet Pet { get; private set; } = new();
    public bool IsEditing { get; set; }
    public bool IsEditingHistorial { get; set; }

    public PetProfileViewModel(IPetService service) => _service = service;

    public async Task LoadAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            var found = await _service.GetByIdAsync(id);
            if (found is not null)
                Pet = found;
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar la mascota");
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
            Pet.UpdatedAt = DateTime.UtcNow;
            await _service.UpdateAsync(Pet.Id, new PetDto());
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
    public void SaveHistorical() => IsEditingHistorial = false;
}