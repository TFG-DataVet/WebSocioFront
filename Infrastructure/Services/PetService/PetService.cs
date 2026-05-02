using System.Net;
using System.Net.Http.Json;
using SocioWeb.Domain.Entities;
using SocioWeb.Entities;
using SocioWeb.Entities.Dtos.PetDto;

namespace SocioWeb.Services.AppointmentService;

public class PetService : IPetService
{
    private readonly HttpClient _http;
    private const string BaseEndpoint = "pet";

    public PetService(HttpClient http)
    {
        _http = http;
    }

    // ─── MÉTODO CENTRAL DE ERRORES ───────────────────────────────

    private async Task HandleError(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode) return;

        var body = await response.Content.ReadAsStringAsync();

        throw new Exception(body);
    }

    // ─── GET ALL ────────────────────────────────────────────────

    public async Task<List<Pet>> GetAllAsync()
    {
        var response = await _http.GetAsync(BaseEndpoint);
        await HandleError(response);

        return await response.Content.ReadFromJsonAsync<List<Pet>>()
               ?? new List<Pet>();
    }

    // ─── GET BY ID ──────────────────────────────────────────────

    public async Task<Pet?> GetByIdAsync(string id)
    {
        var response = await _http.GetAsync($"{BaseEndpoint}/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        await HandleError(response);

        return await response.Content.ReadFromJsonAsync<Pet>();
    }

    // ─── CREATE ─────────────────────────────────────────────────

    public async Task CreateAsync(PetDto dto)
    {
        var response = await _http.PostAsync(
            BaseEndpoint,
            JsonContent.Create(dto)
        );

        await HandleError(response);
    }

    // ─── UPDATE ─────────────────────────────────────────────────

    public async Task UpdateAsync(string id, PetDto dto)
    {
        // Si tu backend solo acepta name + avatarUrl:
        var updateRequest = new
        {
            name = dto.Name,
            avatarUrl = dto.AvatarUrl
        };

        var response = await _http.PutAsync(
            $"{BaseEndpoint}/{id}",
            JsonContent.Create(updateRequest)
        );

        await HandleError(response);
    }

    // ─── DELETE ─────────────────────────────────────────────────

    public async Task DeleteAsync(string id)
    {
        var response = await _http.DeleteAsync($"{BaseEndpoint}/{id}");
        await HandleError(response);
    }
}