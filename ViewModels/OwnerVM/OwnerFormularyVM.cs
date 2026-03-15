using Microsoft.AspNetCore.Components;
using SocioWeb.Domain.Entities;
using SocioWeb.Services.AppointmentService;

namespace SocioWeb.ViewModels.OwnerVM;

public class OwnerFormularyVM
{
    
        private readonly NavigationManager _nav;
        private readonly IOwnerService _ownerService;

        public OwnerFormularyVM(NavigationManager nav, IOwnerService ownerService)
        {
            _nav = nav;
            _ownerService = ownerService;
            Owner = new Owner();
        }

        public Owner Owner { get; set; }

        public bool IsSaving { get; private set; }
        public string? ErrorMessage { get; private set; }

        public async Task SaveAsync()
        {
            try
            {
                IsSaving = true;
                ErrorMessage = null;

                // Guardar en base de datos via servicio
                //await _ownerService.CreateAsync();

                // Redirigir a confirmación
                _nav.NavigateTo("/confirmacion");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsSaving = false;
            }
        }

        public void NavigateToNewPet()
        {
            _nav.NavigateTo("/create-pet");
        }
    }
