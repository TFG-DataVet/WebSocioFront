using System.Net.Http.Json;
using System.Text.Json;
using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos.MedicalDto.ProductDto;

namespace SocioWeb.Services.AppointmentService;

public class ProductsApiService : IProductService
{
    private readonly HttpClient _http;

    // Case-insensitive para que [JsonPropertyName] y nombres camelCase del back mapeen siempre
    private static readonly JsonSerializerOptions _jsonOpts = new(JsonSerializerDefaults.Web);

    public ProductsApiService(HttpClient http) => _http = http;

    // GET /product/clinic/{clinicId}?category=
    public async Task<List<Product>> GetAllByClinicAsync(string clinicId, string? category = null)
    {
        var url = $"product/clinic/{Uri.EscapeDataString(clinicId)}";
        if (!string.IsNullOrEmpty(category))
            url += $"?category={Uri.EscapeDataString(category)}";
        return await _http.GetFromJsonAsync<List<Product>>(url, _jsonOpts) ?? new List<Product>();
    }

    // GET /product/{id}
    public async Task<Product?> GetByIdAsync(string id)
        => await _http.GetFromJsonAsync<Product>($"product/{id}", _jsonOpts);

    // POST /product/clinic/{clinicId}
    public async Task<Product> CreateAsync(string clinicId, CreateProductRequest dto)
    {
        var response = await _http.PostAsJsonAsync($"product/clinic/{Uri.EscapeDataString(clinicId)}", dto);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Product>(_jsonOpts))!;
    }

    // PUT /product/{id}
    public async Task<Product> UpdateAsync(string id, UpdateProductRequest dto)
    {
        var response = await _http.PutAsJsonAsync($"product/{id}", dto);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Product>(_jsonOpts))!;
    }

    // DELETE /product/{id}
    public async Task DeleteAsync(string id, string? reason = null)
    {
        HttpResponseMessage response;
        if (reason is not null)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"product/{id}")
            {
                Content = JsonContent.Create(new { reason })
            };
            response = await _http.SendAsync(request);
        }
        else
        {
            response = await _http.DeleteAsync($"product/{id}");
        }
        response.EnsureSuccessStatusCode();
    }
}
