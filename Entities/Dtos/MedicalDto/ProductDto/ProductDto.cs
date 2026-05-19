using System.Text.Json;

namespace SocioWeb.Entities.Dtos.MedicalDto.ProductDto;

public class CreateProductRequest
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public string? Sku { get; set; }
    public string? Barcode { get; set; }
    public decimal Price { get; set; }
    public decimal TaxRate { get; set; }
    public int Stock { get; set; }
    public int MinStock { get; set; }
    public object Details { get; set; } = new { type = "ACCESSORY" };
}

public class UpdateProductRequest
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public string? Sku { get; set; }
    public string? Barcode { get; set; }
    public decimal Price { get; set; }
    public decimal TaxRate { get; set; }
    public int Stock { get; set; }
    public int MinStock { get; set; }
    public JsonElement? Details { get; set; }
}
