using SocioWeb.Entities.Dtos.EmployeeDto;
using SocioWeb.Services.Exceptions.EmployeeService;
using SocioWeb.ViewModels.Shared;

namespace SocioWeb.ViewModels.Employee;

public class EmployeeFormVM : BaseViewModel
{
    private readonly IEmployeeService _service;

    public EmployeeFormModel FormData { get; set; } = new();
    public bool IsSaving { get; private set; }

    public EmployeeFormVM(IEmployeeService service) => _service = service;

    public async Task LoadForEditAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            var emp = await _service.GetByIdAsync(id);
            if (emp != null)
            {
                FormData = new EmployeeFormModel
                {
                    
                    FirstName      = emp.FirstName,
                    LastName       = emp.LastName,
                    DocumentType   = emp.DocumentType,
                    DocumentNumber = emp.DocumentNumberValue,
                    Email          = emp.Email,
                    Phone          = emp.Phone,
                    // ✅ Address ahora es objeto anidado
                    Address        = emp.Street,
                    City           = emp.City,
                    PostalCode     = emp.PostalCode,
                    Speciality     = emp.Speciality,
                    LicenseNumber  = emp.LicenseNumber,
                    // ✅ HireDate ahora es string
                    HireDate       = DateTime.TryParse(emp.HireDate, out var d) ? d : (DateTime?)null,
                    Role           = emp.Role,
                    AvatarUrl      = emp.AvatarUrl
                };
            }
            else
            {
                SetError("Empleado no encontrado");
            }
        }
        catch (Exception ex) { SetError(ex.Message); }
        finally { IsLoading = false; }
    }

    public async Task CreateAsync()
    {
        IsSaving = true;
        ClearError();
        try { await _service.CreateAsync(MapToDto()); }
        catch (Exception ex) { SetError(ex.Message); }
        finally { IsSaving = false; }
    }

    public async Task UpdateAsync(string id)
    {
        IsSaving = true;
        ClearError();
        try { await _service.UpdateAsync(id, MapToDto()); }
        catch (Exception ex) { SetError(ex.Message); }
        finally { IsSaving = false; }
    }

    private EmployeeDto MapToDto() => new()
    {
        ClinicId       = "2393ff87-3959-4096-9b31-0d8b6d718f10",
        FirstName      = FormData.FirstName      ?? "",
        LastName       = FormData.LastName       ?? "",
        DocumentType   = FormData.DocumentType   ?? "",
        DocumentNumber = FormData.DocumentNumber ?? "",
        Phone          = FormData.Phone          ?? "",
        Email          = FormData.Email          ?? "",
        Address        = FormData.Address        ?? "",
        City           = FormData.City           ?? "",
        PostalCode     = FormData.PostalCode,
        AvatarUrl      = FormData.AvatarUrl,
        Speciality     = FormData.Speciality,
        LicenseNumber  = FormData.LicenseNumber,
        // ✅ Formato yyyy-MM-dd para Java LocalDate
        HireDate       = FormData.HireDate.HasValue
                            ? FormData.HireDate.Value.ToString("yyyy-MM-dd")
                            : DateTime.UtcNow.ToString("yyyy-MM-dd"),
        Role           = FormData.Role ?? ""
    };

    public class EmployeeFormModel
    {
        public string?   FirstName      { get; set; }
        public string?   LastName       { get; set; }
        public string?   DocumentType   { get; set; }
        public string?   DocumentNumber { get; set; }
        public string?   Phone          { get; set; }
        public string?   Email          { get; set; }
        public string?   Address        { get; set; }
        public string?   City           { get; set; }
        public string?   PostalCode     { get; set; }
        public string?   AvatarUrl      { get; set; }
        public string?   Speciality     { get; set; }
        public string?   LicenseNumber  { get; set; }
        public DateTime? HireDate       { get; set; }
        public string?   Role           { get; set; }
    }
}