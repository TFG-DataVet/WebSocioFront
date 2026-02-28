using SocioWeb.Domain.Entities;
using SocioWeb.Entities;

namespace SocioWeb.ViewModels;

public class PetProfileVM
{
    
    public Pet Pet { get; private set; } = new Pet();

    public bool IsEditing { get; private set; }
    public bool IsEditingHistorial { get; private set; }

    public void Load(string id)
    {
        // Simulación (aquí luego llamas a tu API)
        if (id == "M1")
        {
            Pet = new Pet
            {
                Name = "Toby",
                Specie = "Perro",
                Breed = "Labrador",
                Sex = Sex.Male,
                BirthDate = new DateTime(2021, 05, 20),
                Weight = 24,
                Chip = "CHIP123456789",
                WhichDiseases = "Ninguna",
                WhichOperations = "Castrado",
                WhichVacines = "Rabia\nParvovirus",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Owner = new Owner
                {
                    Id = "1",
                    Name = "Juan Pérez",
                    Email = "juan@email.com",
                    Phone = "123456789"
                }
            };
        }
    }

    public void ToggleEdit()
    {
        IsEditing = !IsEditing;
    }

    public void ToggleHistorial()
    {
        IsEditingHistorial = !IsEditingHistorial;
    }

    public void CancelEdit()
    {
        IsEditing = false;
    }

    public void Save()
    {
        Pet.UpdatedAt = DateTime.UtcNow;
        IsEditing = false;

        // Aquí luego llamas a tu API Update
    }

    public void SaveHistorical()
    {
        IsEditingHistorial = false;

        // Aquí luego llamas a tu API Update historial
    }
}