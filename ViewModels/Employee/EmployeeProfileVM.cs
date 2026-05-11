using SocioWeb.Entities.Models.Employee;
using SocioWeb.Services.Exceptions.EmployeeService;
using SocioWeb.ViewModels.Shared;

namespace SocioWeb.ViewModels.Employee;

public class EmployeeProfileVM : BaseViewModel
{
    private readonly IEmployeeService _service;

    // ✅ Usamos Employee directamente, no EmployeeDto
    public Entities.Models.Employee.Employee? Employee { get; private set; }
    public bool IsEditing { get; set; }

    public EmployeeProfileVM(IEmployeeService service) => _service = service;

    public async Task LoadAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            Employee = await _service.GetByIdAsync(id);
            if (Employee is null)
                SetError("Empleado no encontrado");
        }
        catch (Exception ex) { SetError(ex.Message); }
        finally { IsLoading = false; }
    }

    public async Task DeleteAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try { await _service.DeleteAsync(id); }
        catch (Exception ex) { SetError(ex.Message); }
        finally { IsLoading = false; }
    }

    public void Cancel() => IsEditing = false;
}