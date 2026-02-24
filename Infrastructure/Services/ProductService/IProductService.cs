using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos.MedicalDto.ProductDto;

namespace SocioWeb.Services.AppointmentService;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(String id);
    Task CreateAsync(ProductDto dto);
    Task UpdateAsync(String id, ProductDto dto);
    Task DeleteAsync(String id);
}