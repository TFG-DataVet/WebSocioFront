using SocioWeb.Services.Exceptions.EmployeeService;
using SocioWeb.ViewModels.Shared;
namespace SocioWeb.ViewModels.Employee;

public class EmployeeListViewModel : BaseViewModel
{
    private readonly IEmployeeService _service;

    public List<Entities.Models.Employee.Employee> Employees         { get; private set; } = new();
    public List<Entities.Models.Employee.Employee> FilteredEmployees { get; private set; } = new();

    public DateTime? DateSince        { get; set; }
    public DateTime? DateTo           { get; set; }
    public string    CityFilter       { get; set; } = "";
    public string    PostalCodeFilter { get; set; } = "";
    public string    SearchFilter     { get; set; } = "";

    public EmployeeListViewModel(IEmployeeService service) => _service = service;

    public async Task LoadAsync(string? clinicId = null)
    {
        IsLoading = true;
        ClearError();
        try
        {
            Employees = await _service.GetAllAsync(clinicId);
            Employees = Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToList();
            FilteredEmployees = new List<Entities.Models.Employee.Employee>(Employees);
        }
        catch (Exception ex) { SetError($"Error al cargar empleados: {ex.Message}"); }
        finally { IsLoading = false; }
    }

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
        catch (Exception ex) { SetError($"Error al eliminar: {ex.Message}"); }
        finally { IsLoading = false; }
    }

    public void ApplyFilters()
    {
        FilteredEmployees = Employees.Where(e =>
        {
            // ✅ HireDate es string — parseamos para comparar
            DateTime? hireDate = DateTime.TryParse(e.HireDate, out var d) ? d : null;

            return
                (!DateSince.HasValue || (hireDate.HasValue && hireDate >= DateSince)) &&
                (!DateTo.HasValue    || (hireDate.HasValue && hireDate <= DateTo))    &&
                (string.IsNullOrEmpty(CityFilter)       || e.City.Contains(CityFilter, StringComparison.OrdinalIgnoreCase))       &&
                (string.IsNullOrEmpty(PostalCodeFilter) || e.PostalCode.Contains(PostalCodeFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(SearchFilter)     ||
                    e.FullName.Contains(SearchFilter, StringComparison.OrdinalIgnoreCase) ||
                    (e.Email ?? "").Contains(SearchFilter, StringComparison.OrdinalIgnoreCase));
        }).ToList();
    }

    public void ClearFilters()
    {
        DateSince = null; DateTo = null;
        CityFilter = ""; PostalCodeFilter = ""; SearchFilter = "";
        FilteredEmployees = new List<Entities.Models.Employee.Employee>(Employees);
    }
}