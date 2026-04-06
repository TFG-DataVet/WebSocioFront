using System.Net.Http.Json;
using System.Net;
using SocioWeb.Entities;
using SocioWeb.Entities.Dtos.PetDto;
using SocioWeb.Services.AppointmentService;

public class PetsApiService : IPetService
{
    private readonly HttpClient _http;
    private const string BaseEndpoint = "pet"; // 👈 igual que owner

    public PetsApiService(HttpClient http)
    {
        _http = http;
    }

    // ─── MANEJO DE ERRORES ─────────────────────────────

    private async Task HandleError(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode) return;

        var body = await response.Content.ReadAsStringAsync();

        // Igual que Owner
        throw new Exception(body);
    }

    // ─── GET ALL ──────────────────────────────────────

    public async Task<List<Pet>> GetAllAsync()
    {
        var response = await _http.GetAsync(BaseEndpoint);
        await HandleError(response);

        return await response.Content.ReadFromJsonAsync<List<Pet>>()
               ?? new List<Pet>();
    }

    // ─── GET BY ID ────────────────────────────────────

    public async Task<Pet?> GetByIdAsync(string id)
    {
        var response = await _http.GetAsync($"{BaseEndpoint}/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        await HandleError(response);

        return await response.Content.ReadFromJsonAsync<Pet>();
    }

    // ─── CREATE ───────────────────────────────────────

    public async Task CreateAsync(PetDto dto)
    {
        var response = await _http.PostAsync(
            BaseEndpoint,
            JsonContent.Create(dto)
        );

        await HandleError(response);
    }

    // ─── UPDATE ───────────────────────────────────────

    public async Task UpdateAsync(string id, PetDto dto)
    {
        var response = await _http.PutAsync(
            $"{BaseEndpoint}/{id}",
            JsonContent.Create(dto)
        );

        await HandleError(response);
    }

    // ─── DELETE ───────────────────────────────────────

    public async Task DeleteAsync(string id)
    {
        var response = await _http.DeleteAsync($"{BaseEndpoint}/{id}");
        await HandleError(response);
    }
}