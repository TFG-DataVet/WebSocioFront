using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos;
using SocioWeb.Services.AppointmentService;
using Microsoft.AspNetCore.Components;
using SocioWeb.Entities;

namespace SocioWeb.ViewModels.OwnerVM
{
    public class OwnerProfileVM
    {
        private readonly IOwnerService _service;
        private readonly NavigationManager _nav;

        public Owner? Owner { get; private set; }
        public bool IsEditing { get; private set; }
        public bool IsLoading { get; private set; }
        public string? ErrorMessage { get; private set; }

        public OwnerProfileVM(IOwnerService service, NavigationManager nav)
        {
            _service = service;
            _nav     = nav;
        }

        public async Task LoadAsync(string id)
        {
            IsLoading = true;
            ErrorMessage = null;
            try
            {
                Owner = await _service.GetByIdAsync(id);
                if (Owner is null)
                    ErrorMessage = "No se encontró el dueño.";
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

        public void EnableEdit() => IsEditing = true;
        public void CancelEdit() => IsEditing = false;

        public async Task SaveAsync()
        {
            if (Owner is null) return;
            try
            {
                await _service.UpdateAsync(Owner.Id, new OwnerDto
                {
                    Name     = Owner.Name,
                    LastName = Owner.LastName,
                    Email    = Owner.Email,
                    Phone    = Owner.Phone ?? string.Empty,
                    Address  = Owner.Street,
                    City     = Owner.City,
                    PostalCode = Owner.PostalCode
                });
                IsEditing = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void GoToPet(string petId) => _nav.NavigateTo($"/PerfilMascota/{petId}");
    }
}