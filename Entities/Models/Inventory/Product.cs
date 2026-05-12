using System.Text.Json.Serialization;
using SocioWeb.Infrastructure.Converters;

namespace SocioWeb.Domain.Entities;

public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
    public String Stock { get; set; }
    
    public bool Active { get; set; }
    public string Description { get; set; }
    [JsonConverter(typeof(FlexibleNullableDateTimeConverter))]
    public DateTime? CreatedAt { get; set; }
    [JsonConverter(typeof(FlexibleNullableDateTimeConverter))]
    public DateTime? UpdatedAt { get; set; }
    public List<LogEntry> Historical { get; set; }
    
    public string ImageUrl { get; set; }
    
}
