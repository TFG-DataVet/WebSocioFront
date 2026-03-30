using SocioWeb.Domain.Entities;
using SocioWeb.Entities;
using SocioWeb.Entities.Dtos.PetDto;

namespace SocioWeb.Services.AppointmentService;



public class PetService : IPetService
{
    private readonly List<Pet> _pets = new();

    public PetService()
    {
        // Datos de ejemplo
        _pets.Add(new Pet
        {
            Id = "M1",
            Name = "Toby",
            Specie = "Perro",
            Breed = "Labrador",
            Sex = Sex.Male,
            BirthDate = new DateTime(2021, 5, 20),
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
        });
    }

    public Task<List<Pet>> GetAllAsync()
    {
        return Task.FromResult(_pets.ToList());
    }

    public Task<Pet?> GetByIdAsync(string id)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(pet);
    }

    public Task CreateAsync(PetDto dto)
    {
        var pet = new Pet
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Specie = dto.Species,
            Breed = dto.Breed,
            Sex = dto.Sex,
            Weight = dto.Weight,
            BirthDate = dto.DateOfBirth,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _pets.Add(pet);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(string id, PetDto dto)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == id);
        if (pet != null)
        {
            pet.Name = dto.Name;
            pet.Specie = dto.Species;
            pet.Breed = dto.Breed;
            pet.Sex = dto.Sex;
            pet.Weight = dto.Weight;
            pet.BirthDate = dto.DateOfBirth;
            pet.UpdatedAt = DateTime.UtcNow;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(string id)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == id);
        if (pet != null)
            _pets.Remove(pet);
        return Task.CompletedTask;
    }
}