namespace SocioWeb.ViewModels.Products;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Domain.Entities;

public class ProductListViewModel : BaseViewModel
{
    private readonly IProductService _service;

    public List<Product> Products { get; private set; } = new();
    public List<Product> FilteredProducts { get; private set; } = new();

    // ─── FILTROS ─────────────────────────────────────────────────────────────
    public DateTime? DateSince { get; set; }
    public DateTime? DateTo { get; set; }
    public string CategoryFilter { get; set; } = "";
    public string SkuFilter { get; set; } = "";
    public int? MinimumStock { get; set; }

    public ProductListViewModel(IProductService service) => _service = service;

    // ─── CRUD ─────────────────────────────────────────────────────────────────

    public async Task LoadAsync(string clinicId)
    {
        IsLoading = true;
        ClearError();
        try
        {
            Products = await _service.GetAllByClinicAsync(clinicId);
            Products = Products.OrderBy(p => p.CreatedAt).ToList();
            FilteredProducts = new List<Product>(Products);
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

    public async Task DeleteAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            await _service.DeleteAsync(id);
            Products.RemoveAll(p => p.Id == id);
            ApplyFilters();
        }
        catch (Exception ex)
        {
            SetError($"Error al eliminar el producto: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    // ─── FILTROS ──────────────────────────────────────────────────────────────

    public void ApplyFilters()
    {
        FilteredProducts = Products
            .Where(p =>
                (!DateSince.HasValue || p.CreatedAt >= DateSince) &&
                (!DateTo.HasValue   || p.CreatedAt <= DateTo) &&
                (string.IsNullOrEmpty(CategoryFilter) ||
                 (p.Category ?? "").Contains(CategoryFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(SkuFilter) ||
                 (p.Sku ?? "").Contains(SkuFilter, StringComparison.OrdinalIgnoreCase)) &&
                (!MinimumStock.HasValue || p.Stock >= MinimumStock))
            .ToList();
    }

    public void ClearFilters()
    {
        DateSince      = null;
        DateTo         = null;
        CategoryFilter = "";
        SkuFilter      = "";
        MinimumStock   = null;
        FilteredProducts = new List<Product>(Products);
    }
}
