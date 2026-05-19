using System.Text.Json;
using System.Text.Json.Serialization;
using SocioWeb.Infrastructure.Converters;

namespace SocioWeb.Domain.Entities;

public class Product
{
    [JsonPropertyName("productId")]
    public string Id { get; set; } = "";
    public string ClinicId { get; set; } = "";
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Sku { get; set; }
    public string? Barcode { get; set; }
    public decimal Price { get; set; }
    public decimal TaxRate { get; set; }
    public int Stock { get; set; }
    public int MinStock { get; set; }
    public bool IsActive { get; set; }
    [JsonConverter(typeof(FlexibleNullableDateTimeConverter))]
    public DateTime? CreatedAt { get; set; }
    [JsonConverter(typeof(FlexibleNullableDateTimeConverter))]
    public DateTime? UpdatedAt { get; set; }
    public JsonElement? Details { get; set; }
}
