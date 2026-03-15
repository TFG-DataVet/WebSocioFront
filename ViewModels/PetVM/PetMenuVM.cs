using SocioWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocioWeb.Domain.Entities;

namespace SocioWeb.ViewModels
{
    public class PetsMenuVM
    {
        // Lista completa de mascotas
        public List<Pet> Pets { get; private set; } = new();
        // Lista filtrada
        public List<Pet> FilteredPets { get; private set; } = new();

        // Filtros
        public string NameFilter { get; set; } = string.Empty;
        public string SpecieFilter { get; set; } = string.Empty;

        // Control de visibilidad de dueño
        public HashSet<string> VisibleOwners { get; } = new();

        public PetsMenuVM() { }

        // Carga inicial de datos de ejemplo
        public Task LoadAsync()
        {
            Pets = new List<Pet>
            {
                new Pet
                {
                    Id = "P1",
                    Name = "Toby",
                    Specie = "Perro",
                    Breed = "Labrador",
                    Sex = Sex.Male,
                    BirthDate = new DateTime(2021, 5, 20),
                    Weight = 24,
                    Owner = new Owner { Id = "O1", Name = "Juan Pérez" }
                },
                new Pet
                {
                    Id = "P2",
                    Name = "Mimi",
                    Specie = "Gato",
                    Breed = "Siamés",
                    Sex = Sex.Female,
                    BirthDate = new DateTime(2022, 3, 15),
                    Weight = 5,
                    Owner = new Owner { Id = "O2", Name = "Ana Gómez" }
                },
                new Pet
                {
                    Id = "P3",
                    Name = "Rocky",
                    Specie = "Perro",
                    Breed = "Golden Retriever",
                    Sex = Sex.Male,
                    BirthDate = new DateTime(2020, 8, 10),
                    Weight = 30,
                    Owner = new Owner { Id = "O3", Name = "Carlos Ruiz" }
                }
            };

            FilteredPets = Pets;
            return Task.CompletedTask;
        }

        // Aplica filtros de nombre y especie
        public void ApplyFilters()
        {
            FilteredPets = Pets
                .Where(p =>
                    (string.IsNullOrWhiteSpace(NameFilter) ||
                     p.Name.Contains(NameFilter, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(SpecieFilter) ||
                     p.Specie.Contains(SpecieFilter, StringComparison.OrdinalIgnoreCase))
                )
                .ToList();
        }

        // Mostrar/ocultar dueño
        public void ToggleOwner(string petId)
        {
            if (VisibleOwners.Contains(petId))
                VisibleOwners.Remove(petId);
            else
                VisibleOwners.Add(petId);
        }
    }
}