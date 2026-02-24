using SocioWeb.Entities;
using SocioWeb.Entities.Dtos.PetDto;

namespace SocioWeb.Services.AppointmentService;

using System.Net.Http.Json;

public class PetsApiService : IPetService
{
    private readonly HttpClient _http;

    public PetsApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Pet>> GetAllAsync()
        => await _http.GetFromJsonAsync<List<Pet>>("api/pets")
           ?? new List<Pet>();

    public async Task<Pet?> GetByIdAsync(String id)
        => await _http.GetFromJsonAsync<Pet>($"api/pets/{id}");

    public async Task CreateAsync(PetDto dto)
        => await _http.PostAsJsonAsync("api/pets", dto);

    public async Task UpdateAsync(String id, PetDto dto)
        => await _http.PutAsJsonAsync($"api/pets/{id}", dto);

    public async Task DeleteAsync(String id)
        => await _http.DeleteAsync($"api/pets/{id}");
}