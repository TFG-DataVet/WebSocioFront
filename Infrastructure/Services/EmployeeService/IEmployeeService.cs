using SocioWeb.Entities.Dtos.EmployeeDto;
using SocioWeb.Entities.Models.Employee;

namespace SocioWeb.Services.Exceptions.EmployeeService;

public interface IEmployeeService
{
    /// <summary>GET /employee — devuelve todos los empleados.</summary>
    Task<List<Employee>> GetAllAsync();

    /// <summary>GET /employee/{id} — devuelve un empleado por ID.</summary>
    Task<Employee?> GetByIdAsync(string id);

    /// <summary>POST /employee — crea un nuevo empleado.</summary>
    Task CreateAsync(EmployeeDto dto);

    /// <summary>PUT /employee/{id} — actualiza un empleado existente.</summary>
    Task UpdateAsync(string id, EmployeeDto dto);

    /// <summary>DELETE /employee/{id} — elimina un empleado por ID.</summary>
    Task DeleteAsync(string id);
}