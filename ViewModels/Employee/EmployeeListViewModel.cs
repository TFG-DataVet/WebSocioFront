using SocioWeb.Services.Exceptions.EmployeeService;

namespace SocioWeb.ViewModels.Employee;

using SocioWeb.ViewModels.Shared;

using SocioWeb.Domain.Entities;

public class EmployeeListViewModel : BaseViewModel
{
    private readonly IEmployeeService _service;

    public List<Entities.Models.Employee.Employee> Employees { get; private set; } = new();
    public List<Entities.Models.Employee.Employee> FilteredEmployees { get; private set; } = new();

    // Filtros
    public DateTime? DateSince { get; set; }
    public DateTime? DateTo { get; set; }
    public string CityFilter { get; set; } = "";
    public string PostalCodeFilter { get; set; } = "";
    public string SearchFilter { get; set; } = "";

    public EmployeeListViewModel(IEmployeeService service) => _service = service;

    // ─── CRUD ─────────────────────────────────────────────────────────────────

    /// <summary>GET /employee — carga todos los empleados.</summary>
    public async Task LoadAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            Employees = await _service.GetAllAsync();
            Employees = Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToList();
            FilteredEmployees = new List<Entities.Models.Employee.Employee>(Employees);
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar empleados: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>DELETE /employee/{id} — elimina un empleado por ID.</summary>
    public async Task DeleteAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            await _service.DeleteAsync(id);
            Employees.RemoveAll(e => e.Id == id);
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Error al eliminar el empleado: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    // ─── Filtros ──────────────────────────────────────────────────────────────

    public void ApplyFilters()
    {
        FilteredEmployees = Employees
            .Where(e =>
                (!DateSince.HasValue || e.HireDate >= DateSince) &&
                (!DateTo.HasValue   || e.HireDate <= DateTo) &&
                (string.IsNullOrEmpty(CityFilter) ||
                 (e.City ?? "").Contains(CityFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(PostalCodeFilter) ||
                 (e.PostalCode ?? "").Contains(PostalCodeFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(SearchFilter) ||
                 ($"{e.FirstName} {e.LastName}").Contains(SearchFilter, StringComparison.OrdinalIgnoreCase) ||
                 (e.Email ?? "").Contains(SearchFilter, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    public void ClearFilters()
    {
        DateSince       = null;
        DateTo          = null;
        CityFilter      = "";
        PostalCodeFilter= "";
        SearchFilter    = "";
        FilteredEmployees = new List<Entities.Models.Employee.Employee>(Employees);
    }
}