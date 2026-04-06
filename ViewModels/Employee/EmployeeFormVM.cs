using SocioWeb.Entities.Dtos.EmployeeDto;
using SocioWeb.Services.Exceptions.EmployeeService;
using SocioWeb.ViewModels.Shared;

namespace SocioWeb.ViewModels.Employee;

public class EmployeeFormVM : BaseViewModel
{
    private readonly IEmployeeService _service;

    public EmployeeFormModel FormData { get; set; } = new();
    public bool IsSaving { get; private set; }

    public EmployeeFormVM(IEmployeeService service)
        => _service = service;

    // ─────────────────────────────────────────
    // LOAD EDIT
    // ─────────────────────────────────────────
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
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    DocumentType = emp.DocumentType,
                    DocumentNumber = emp.DocumentNumber,
                    Email = emp.Email,
                    Phone = emp.Phone,
                    Address = emp.Address,
                    City = emp.City,
                    PostalCode = emp.PostalCode,
                    Speciality = emp.Speciality,
                    LicenseNumber = emp.LicenseNumber,
                    HireDate = emp.HireDate,
                    Role = emp.Role,
                    AvatarUrl = emp.AvatarUrl
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

    // ─────────────────────────────────────────
    // CREATE
    // ─────────────────────────────────────────
    public async Task CreateAsync()
    {
        IsSaving = true;
        ClearError();

        try
        {
            await _service.CreateAsync(MapToDto());
        }
        catch (Exception ex)
        {
            SetError(ex.Message);
        }
        finally
        {
            IsSaving = false;
        }
    }

    // ─────────────────────────────────────────
    // UPDATE
    // ─────────────────────────────────────────
    public async Task UpdateAsync(string id)
    {
        IsSaving = true;
        ClearError();

        try
        {
            await _service.UpdateAsync(id, MapToDto());
        }
        catch (Exception ex)
        {
            SetError(ex.Message);
        }
        finally
        {
            IsSaving = false;
        }
    }

    // ─────────────────────────────────────────
    // MAP DTO
    // ─────────────────────────────────────────
    private EmployeeDto MapToDto() => new()
    {
        FirstName = FormData.FirstName ?? "",
        LastName = FormData.LastName ?? "",
        DocumentType = FormData.DocumentType ?? "",
        DocumentNumber = FormData.DocumentNumber ?? "",
        Phone = FormData.Phone ?? "",
        Email = FormData.Email ?? "",
        Address = FormData.Address ?? "",
        City = FormData.City ?? "",
        PostalCode = FormData.PostalCode,
        AvatarUrl = FormData.AvatarUrl,
        Speciality = FormData.Speciality,
        LicenseNumber = FormData.LicenseNumber,
        HireDate = FormData.HireDate ?? DateTime.UtcNow,
        Role = FormData.Role ?? ""
    };

    // ─────────────────────────────────────────
    // MODEL
    // ─────────────────────────────────────────
    public class EmployeeFormModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? DocumentType { get; set; }
        public string? DocumentNumber { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }

        public string? AvatarUrl { get; set; }

        public string? Speciality { get; set; }
        public string? LicenseNumber { get; set; }

        public DateTime? HireDate { get; set; }
        public string? Role { get; set; }
    }
}