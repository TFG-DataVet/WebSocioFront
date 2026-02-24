using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos;
using SocioWeb.Services.AppointmentService;

namespace SocioWeb.ViewModels.OwnerVM;
using Microsoft.AspNetCore.Components;

public class OwnerProfileVM
{
    
    private readonly IOwnerService _service;
    private readonly NavigationManager _nav;

    public Owner? Owner { get; private set; }

    public bool IsEditing { get; private set; }
    public bool IsLoading { get; private set; }
    public string? ErrorMessage { get; private set; }

    public OwnerProfileVM(
        IOwnerService service,
        NavigationManager nav)
    {
        _service = service;
        _nav = nav;
    }

    public async Task LoadAsync(string id)
    {
        try
        {
            IsLoading = true;
            ErrorMessage = null;

            Owner = await _service.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void EnableEdit()
    {
        IsEditing = true;
    }

    public void CancelEdit()
    {
        IsEditing = false;
    }

    public async Task SaveAsync()
    {
        if (Owner == null) return;

        try
        {
            var dto = new OwnerDto()
            {
            };

           // await _service.UpdateAsync(Owner.Id, dto);

            IsEditing = false;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    public void GoToPet(string petId)
    {
        _nav.NavigateTo($"/Mascota/{petId}");
    }
}