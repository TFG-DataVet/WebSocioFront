using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos.MedicalDto.ProductDto;

namespace SocioWeb.Services.AppointmentService;

// Mock de desarrollo — en producción se usa ProductsApiService
public class ProductService : IProductService
{
    private readonly List<Product> _products = new();

    public Task<List<Product>> GetAllByClinicAsync(string clinicId, string? category = null)
        => Task.FromResult(_products.Where(p => p.ClinicId == clinicId).ToList());

    public Task<Product?> GetByIdAsync(string id)
        => Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

    public Task<Product> CreateAsync(string clinicId, CreateProductRequest dto)
    {
        var product = new Product
        {
            Id        = Guid.NewGuid().ToString(),
            ClinicId  = clinicId,
            Name      = dto.Name,
            Description = dto.Description,
            Sku       = dto.Sku,
            Barcode   = dto.Barcode,
            Price     = dto.Price,
            TaxRate   = dto.TaxRate,
            Stock     = dto.Stock,
            MinStock  = dto.MinStock,
            IsActive  = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        _products.Add(product);
        return Task.FromResult(product);
    }

    public Task<Product> UpdateAsync(string id, UpdateProductRequest dto)
    {
        var existing = _products.FirstOrDefault(p => p.Id == id)
                       ?? throw new KeyNotFoundException($"Producto {id} no encontrado");
        existing.Name        = dto.Name;
        existing.Description = dto.Description;
        existing.Sku         = dto.Sku;
        existing.Barcode     = dto.Barcode;
        existing.Price       = dto.Price;
        existing.TaxRate     = dto.TaxRate;
        existing.Stock       = dto.Stock;
        existing.MinStock    = dto.MinStock;
        existing.UpdatedAt   = DateTime.UtcNow;
        return Task.FromResult(existing);
    }

    public Task DeleteAsync(string id, string? reason = null)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
            product.IsActive = false;
        return Task.CompletedTask;
    }
}
