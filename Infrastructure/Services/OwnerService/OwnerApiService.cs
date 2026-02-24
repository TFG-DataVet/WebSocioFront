using SocioWeb.Entities.Dtos;

namespace SocioWeb.Services.AppointmentService;

using System.Net.Http.Json;
using SocioWeb.Domain.Entities;

public class OwnersApiService : IOwnerService
{
    private readonly HttpClient _http;

    public OwnersApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Owner>> GetAllAsync()
        => await _http.GetFromJsonAsync<List<Owner>>("api/owners")
           ?? new List<Owner>();

    public async Task<Owner?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<Owner>($"api/owners/{id}");

    public async Task CreateAsync(OwnerDto dto)
        => await _http.PostAsJsonAsync("api/owners", dto);

    public async Task UpdateAsync(int id, OwnerDto dto)
        => await _http.PutAsJsonAsync($"api/owners/{id}", dto);

    public async Task DeleteAsync(int id)
        => await _http.DeleteAsync($"api/owners/{id}");
}