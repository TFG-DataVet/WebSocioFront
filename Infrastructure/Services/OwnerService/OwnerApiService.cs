using System.Net.Http.Json;
using System.Net;
using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos;
using SocioWeb.Services.AppointmentService;

public class OwnersApiService : IOwnerService
{
    private readonly HttpClient _http;
    private const string BaseEndpoint = "owner";

    public OwnersApiService(HttpClient http)
    {
        _http = http;
    }

    // ─── MÉTODO CENTRAL DE ERRORES ───────────────────────────────

    private async Task HandleError(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode) return;

        var body = await response.Content.ReadAsStringAsync();

        // 🔥 IMPORTANTE: lanzamos el JSON directamente
        throw new Exception(body);
    }

    // ─── GET ALL ────────────────────────────────────────────────

    public async Task<List<Owner>> GetAllAsync()
    {
        var response = await _http.GetAsync(BaseEndpoint);
        await HandleError(response);

        return await response.Content.ReadFromJsonAsync<List<Owner>>()
               ?? new List<Owner>();
    }

    // ─── GET BY ID ──────────────────────────────────────────────

    public async Task<Owner?> GetByIdAsync(string id)
    {
        var response = await _http.GetAsync($"{BaseEndpoint}/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        await HandleError(response);

        return await response.Content.ReadFromJsonAsync<Owner>();
    }

    // ─── CREATE ─────────────────────────────────────────────────

    public async Task CreateAsync(OwnerDto dto)
    {
        var response = await _http.PostAsync(
            BaseEndpoint,
            JsonContent.Create(dto)
        );

        await HandleError(response);
    }

    // ─── UPDATE ─────────────────────────────────────────────────

    public async Task UpdateAsync(string id, OwnerDto dto)
    {
        var response = await _http.PutAsync(
            $"{BaseEndpoint}/{id}",
            JsonContent.Create(dto)
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