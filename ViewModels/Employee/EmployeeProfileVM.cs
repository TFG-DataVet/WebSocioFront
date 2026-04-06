using SocioWeb.Entities.Dtos.EmployeeDto;
using SocioWeb.Services.Exceptions.EmployeeService;
using SocioWeb.ViewModels.Shared;

namespace SocioWeb.ViewModels.Employee;

public class EmployeeProfileVM : BaseViewModel
{
    private readonly IEmployeeService _service;

    public EmployeeDto Employee { get; set; } = new();

    public EmployeeProfileVM(IEmployeeService service)
        => _service = service;

    // ─────────────────────────────
    // LOAD
    // ─────────────────────────────
    public async Task LoadAsync(string id)
    {
        IsLoading = true;
        ClearError();

        try
        {
            var emp = await _service.GetByIdAsync(id);

            if (emp != null)
            {
                Employee = new EmployeeDto
                {
                    id = emp.Id,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    DocumentType = emp.DocumentType,
                    DocumentNumber = emp.DocumentNumber,
                    Phone = emp.Phone,
                    Email = emp.Email,
                    Address = emp.Address,
                    City = emp.City,
                    PostalCode = emp.PostalCode,
                    AvatarUrl = emp.AvatarUrl,
                    Speciality = emp.Speciality,
                    LicenseNumber = emp.LicenseNumber,
                    HireDate = emp.HireDate,
                    Role = emp.Role
                };
            }
            else
            {
                SetError("Empleado no encontrado");
            }
        }
        catch (Exception ex)
        {
            SetError(ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }

    // ─────────────────────────────
    // DELETE
    // ─────────────────────────────
    public async Task DeleteAsync(string id)
    {
        IsLoading = true;
        ClearError();

        try
        {
            await _service.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            SetError(ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }
}