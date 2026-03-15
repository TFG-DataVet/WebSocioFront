namespace SocioWeb.Services.AppointmentService;

using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos;

public interface IOwnerService
{
    Task<List<Owner>> GetAllAsync();
    Task<Owner?> GetByIdAsync(String id);
    Task CreateAsync(OwnerDto dto);
    Task UpdateAsync(String id, OwnerDto dto);
    Task DeleteAsync(String id);
}