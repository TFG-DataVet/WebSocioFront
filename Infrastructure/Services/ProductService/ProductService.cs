using SocioWeb.Domain.Entities;

namespace SocioWeb.Services.AppointmentService;


public class ProductService : IProductService
{
    private readonly List<Product> _products = new();

    public ProductService()
    {
        // Ejemplo hardcode de productos
        _products.Add(new Product
        {
            Id = "P1",
            Name = "Alimento Premium Perro",
            Category = "Alimentos",
            Brand = "DogPlus",
            Price = 29.99m,
            Stock = 15.ToString(),
            Active = true,
            Description = "Bolsa 10kg alimento balanceado",
            CreatedAt = DateTime.UtcNow.AddMonths(-3),
            UpdatedAt = DateTime.UtcNow,
            Historical = new List<LogEntry>
            {
                new LogEntry { Date = DateTime.UtcNow.AddDays(-10), Description = "Producto creado" },
                new LogEntry { Date = DateTime.UtcNow.AddDays(-2), Description = "Actualización de stock" }
            }
        });
    }

    public Task<List<Product>> GetAllAsync() => Task.FromResult(_products.ToList());

    public Task<Product?> GetByIdAsync(string id) => Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

    public Task CreateAsync(Product product)
    {
        product.Id = Guid.NewGuid().ToString();
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;
        _products.Add(product);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(string id, Product product)
    {
        var existing = _products.FirstOrDefault(p => p.Id == id);
        if (existing != null)
        {
            existing.Name = product.Name;
            existing.Category = product.Category;
            existing.Brand = product.Brand;
            existing.Price = product.Price;
            existing.Stock = product.Stock;
            existing.Active = product.Active;
            existing.Description = product.Description;
            existing.UpdatedAt = DateTime.UtcNow;

            if (product.Historical != null)
            {
                existing.Historical = product.Historical;
            }
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(string id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
            _products.Remove(product);
        return Task.CompletedTask;
    }
}