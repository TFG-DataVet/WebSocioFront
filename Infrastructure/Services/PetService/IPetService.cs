using SocioWeb.Entities;
using SocioWeb.Entities.Dtos.PetDto;

namespace SocioWeb.Services.AppointmentService;

public interface IPetService
{
    Task<List<Pet>> GetAllAsync();
    Task<Pet?> GetByIdAsync(String id);
    Task CreateAsync(PetDto dto);
    Task UpdateAsync(String id, PetDto dto);
    Task DeleteAsync(String id);
}