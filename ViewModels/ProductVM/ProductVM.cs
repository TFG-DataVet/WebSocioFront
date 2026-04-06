using SocioWeb.Domain.Entities;

namespace SocioWeb.ViewModels;

public class ProductVM
{
    
    public List<Product> Products { get; private set; } = new();
    public List<Product> FilteredProducts { get; private set; } = new();

    public DateTime? DateSince { get; set; }
    public DateTime? DateTo { get; set; }
    public string CategoryFilter { get; set; } = string.Empty;
    public string BrandFilter { get; set; } = string.Empty;
    public int? MinimumStock { get; set; }

    public void Load()
    {
        // Datos simulados, reemplaza con tu API real
        Products = new List<Product>
        {
            new Product
            {
                Id = "1",
                Name = "Alimento Premium Perro",
                Category = "Alimentos",
                Brand = "DogPlus",
                Price = 29.99m,
                Stock = 15.ToString(),
                Description = "Bolsa 10kg alimento balanceado",
                CreatedAt = new DateTime(2023, 1, 10)
            },
            new Product
            {
                Id = "2",
                Name = "Arena para Gato",
                Category = "Higiene",
                Brand = "CatClean",
                Price = 12.50m,
                Stock = 5.ToString(),
                Description = "Arena aglomerante",
                CreatedAt = new DateTime(2024, 2, 5)
            }
        };

        Products = Products.OrderBy(p => p.CreatedAt).ToList();
        FilteredProducts = Products;
    }

    public void ApplyFilters()
    {
        FilteredProducts = Products
            .Where(p =>
                (!DateSince.HasValue || p.CreatedAt >= DateSince) &&
                (!DateTo.HasValue || p.CreatedAt <= DateTo) &&
                (string.IsNullOrEmpty(CategoryFilter) ||
                 p.Category.Contains(CategoryFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(BrandFilter) ||
                 p.Brand.Contains(BrandFilter, StringComparison.OrdinalIgnoreCase)) &&
                (!MinimumStock.HasValue || int.Parse(p.Stock) >= MinimumStock)
            )
            .ToList();
    }
}