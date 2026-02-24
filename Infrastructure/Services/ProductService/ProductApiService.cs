using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos.MedicalDto.ProductDto;

namespace SocioWeb.Services.AppointmentService;

using System.Net.Http.Json;

public class ProductsApiService : IProductService
{
    private readonly HttpClient _http;

    public ProductsApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Product>> GetAllAsync()
        => await _http.GetFromJsonAsync<List<Product>>("api/products")
           ?? new List<Product>();

    public async Task<Product?> GetByIdAsync(String id)
        => await _http.GetFromJsonAsync<Product>($"api/products/{id}");

    public async Task CreateAsync(ProductDto dto)
        => await _http.PostAsJsonAsync("api/products", dto);

    public async Task UpdateAsync(String id, ProductDto dto)
        => await _http.PutAsJsonAsync($"api/products/{id}", dto);

    public async Task DeleteAsync(String id)
        => await _http.DeleteAsync($"api/products/{id}");
}