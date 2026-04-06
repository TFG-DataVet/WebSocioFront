namespace SocioWeb.Services.AppointmentService;

using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos;

public interface IOwnerService
{
    /// <summary>GET /owner — devuelve todos los dueños.</summary>
    Task<List<Owner>> GetAllAsync();

    /// <summary>GET /owner/{id} — devuelve un dueño por ID.</summary>
    Task<Owner?> GetByIdAsync(string id);

    /// <summary>POST /owner — crea un nuevo dueño.</summary>
    Task CreateAsync(OwnerDto dto);

    /// <summary>PUT /owner/{id} — actualiza un dueño existente.</summary>
    Task UpdateAsync(string id, OwnerDto dto);

    /// <summary>DELETE /owner/{id} — elimina un dueño por ID.</summary>
    Task DeleteAsync(string id);
}