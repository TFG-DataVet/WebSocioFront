using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos.MedicalDto.ProductDto;

namespace SocioWeb.Services.AppointmentService;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task CreateAsync(ProductDto dto);
    Task UpdateAsync(int id, ProductDto dto);
    Task DeleteAsync(int id);
}