namespace SocioWeb.ViewModels.Products;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos.MedicalDto.ProductDto;

public class ProductProfileViewModel : BaseViewModel
{
    private readonly IProductService _service;

    public Product Product { get; private set; } = new();
    public bool IsEditing { get; set; }

    public ProductProfileViewModel(IProductService service) => _service = service;

    public async Task LoadAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            var found = await _service.GetByIdAsync(id);
            if (found is not null)
                Product = found;
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar el producto: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task SaveAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            var request = new UpdateProductRequest
            {
                Name        = Product.Name,
                Description = Product.Description,
                Sku         = Product.Sku,
                Barcode     = Product.Barcode,
                Price       = Product.Price,
                TaxRate     = Product.TaxRate,
                Stock       = Product.Stock,
                MinStock    = Product.MinStock,
                Details     = Product.Details,
            };
            var updated = await _service.UpdateAsync(Product.Id, request);
            Product = updated;
            IsEditing = false;
        }
        catch (Exception ex)
        {
            SetError($"Error al guardar: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void Cancel() => IsEditing = false;
}
