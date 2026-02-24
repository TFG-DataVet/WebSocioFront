namespace SocioWeb.Services.AppointmentService;

using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos;

public interface IOwnerService
{
    Task<List<Owner>> GetAllAsync();
    Task<Owner?> GetByIdAsync(int id);
    Task CreateAsync(OwnerDto dto);
    Task UpdateAsync(int id, OwnerDto dto);
    Task DeleteAsync(int id);
}