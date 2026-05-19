using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos.MedicalDto.ProductDto;

namespace SocioWeb.Services.AppointmentService;

public interface IProductService
{
    Task<List<Product>> GetAllByClinicAsync(string clinicId, string? category = null);
    Task<Product?> GetByIdAsync(string id);
    Task<Product> CreateAsync(string clinicId, CreateProductRequest dto);
    Task<Product> UpdateAsync(string id, UpdateProductRequest dto);
    Task DeleteAsync(string id, string? reason = null);
}
