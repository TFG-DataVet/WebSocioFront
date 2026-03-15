namespace SocioWeb.ViewModels.Products;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Domain.Entities;

public class ProductListViewModel : BaseViewModel
{
    private readonly IProductService _service;

    public List<Product> Products { get; private set; } = new();
    public List<Product> FilteredProducts { get; private set; } = new();
    public DateTime? DateSince { get; set; }
    public DateTime? DateTo { get; set; }
    public string CategoryFilter { get; set; } = "";
    public string BrandFilter { get; set; } = "";
    public int? MinimumStock { get; set; }

    public ProductListViewModel(IProductService service) => _service = service;

    public async Task LoadAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            Products = await _service.GetAllAsync();
            Products = Products.OrderBy(p => p.CreatedAt).ToList();
            FilteredProducts = Products;
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar productos: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void ApplyFilters()
    {
        FilteredProducts = Products
            .Where(p =>
                (!DateSince.HasValue || p.CreatedAt >= DateSince) &&
                (!DateTo.HasValue   || p.CreatedAt <= DateTo) &&
                (string.IsNullOrEmpty(CategoryFilter) ||
                 p.Category.Contains(CategoryFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(BrandFilter) ||
                 p.Brand.Contains(BrandFilter, StringComparison.OrdinalIgnoreCase)) &&
                (!MinimumStock.HasValue || p.Stock >= MinimumStock))
            .ToList();
    }
}