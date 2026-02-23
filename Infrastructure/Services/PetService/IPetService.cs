using SocioWeb.Entities;
using SocioWeb.Entities.Dtos.PetDto;

namespace SocioWeb.Services.AppointmentService;

public interface IPetService
{
    Task<List<Pet>> GetAllAsync();
    Task<Pet?> GetByIdAsync(int id);
    Task CreateAsync(PetDto dto);
    Task UpdateAsync(int id, PetDto dto);
    Task DeleteAsync(int id);
}