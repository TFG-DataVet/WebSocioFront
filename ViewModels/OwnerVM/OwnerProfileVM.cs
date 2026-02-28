using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos;
using SocioWeb.Services.AppointmentService;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            _nav = nav;
        }

        // --- Ejemplo de carga local sin servicio ---
        public async Task LoadAsync(string id)
        {
            try
            {
                IsLoading = true;
                ErrorMessage = null;

                // Simulación de espera
                await Task.Delay(100);

                if (id == "1")
                {
                    Owner = new Owner
                    {
                        Id = "1",
                        Name = "Juan Pérez",
                        Email = "juan@email.com",
                        Phone = "123456789",
                        City = "Madrid",
                        PostalCode = "28001",
                        CreatedAt = DateTime.UtcNow.AddMonths(-6),
                        UpdatedAt = DateTime.UtcNow.AddDays(-1),
                        Pet = new List<Pet>
                        {
                            new Pet
                            {
                                Id = "M1",
                                Name = "Toby",
                                Specie = "Perro",
                                Breed = "Labrador",
                                Sex = Sex.Male,
                                BirthDate = new DateTime(2021,5,20),
                                Weight = 24
                            },
                            new Pet
                            {
                                Id = "M2",
                                Name = "Mia",
                                Specie = "Gato",
                                Breed = "Siames",
                                Sex = Sex.Female,
                                BirthDate = new DateTime(2022,2,10),
                                Weight = 4
                            }
                        }
                    };
                }
                else
                {
                    Owner = new Owner
                    {
                        Id = id,
                        Name = "Desconocido",
                        Email = "-",
                        Phone = "-",
                        City = "-",
                        PostalCode = "-",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Pet = new List<Pet>()
                    };
                }
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
            if (Owner == null) return;

            try
            {
                var dto = new Owner
                {
                    // Aquí mapearías los campos que quieras actualizar
                    Name = Owner.Name,
                    Email = Owner.Email,
                    Phone = Owner.Phone,
                    City = Owner.City,
                    PostalCode = Owner.PostalCode
                };

                // Simulación de guardado
                await Task.Delay(50);
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
}