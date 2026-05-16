using SocioWeb.Domain.Entities;

namespace SocioWeb.ViewModels;

// Clase de compatibilidad — no registrada en DI
public class ProductVM
{
    public List<Product> Products { get; private set; } = new();
    public List<Product> FilteredProducts { get; private set; } = new();

    public DateTime? DateSince { get; set; }
    public DateTime? DateTo { get; set; }
    public string CategoryFilter { get; set; } = string.Empty;
    public string SkuFilter { get; set; } = string.Empty;
    public int? MinimumStock { get; set; }

    public void ApplyFilters()
    {
        FilteredProducts = Products
            .Where(p =>
                (!DateSince.HasValue || p.CreatedAt >= DateSince) &&
                (!DateTo.HasValue || p.CreatedAt <= DateTo) &&
                (string.IsNullOrEmpty(CategoryFilter) ||
                 (p.Category ?? "").Contains(CategoryFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(SkuFilter) ||
                 (p.Sku ?? "").Contains(SkuFilter, StringComparison.OrdinalIgnoreCase)) &&
                (!MinimumStock.HasValue || p.Stock >= MinimumStock)
            )
            .ToList();
    }
}
