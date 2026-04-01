using System.Net;
using System.Net.Http.Json;
using SocioWeb.Entities.Dtos.EmployeeDto;
using SocioWeb.Entities.Models.Employee;

namespace SocioWeb.Services.Exceptions.EmployeeService;

public class EmployeesApiService : IEmployeeService
{
    private readonly HttpClient _http;
    private const string BaseEndpoint = "employee";

    public EmployeesApiService(HttpClient http)
    {
        _http = http;
    }

    // ─── MÉTODO CENTRAL DE ERRORES ───────────────────────────────
    private async Task HandleError(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode) return;

        var body = await response.Content.ReadAsStringAsync();

        // 🔥 Lanzamos el JSON directamente
        throw new Exception(body);
    }

    // ─── GET ALL ────────────────────────────────────────────────
    public async Task<List<Employee>> GetAllAsync()
    {
        var response = await _http.GetAsync(BaseEndpoint);
        await HandleError(response);

        return await response.Content.ReadFromJsonAsync<List<Employee>>() 
               ?? new List<Employee>();
    }

    // ─── GET BY ID ──────────────────────────────────────────────
    public async Task<Employee?> GetByIdAsync(string id)
    {
        var response = await _http.GetAsync($"{BaseEndpoint}/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        await HandleError(response);

        return await response.Content.ReadFromJsonAsync<Employee>();
    }

    // ─── CREATE ─────────────────────────────────────────────────
    public async Task CreateAsync(EmployeeDto dto)
    {
        var response = await _http.PostAsync(
            BaseEndpoint,
            JsonContent.Create(dto)
        );

        await HandleError(response);
    }

    // ─── UPDATE ─────────────────────────────────────────────────
    public async Task UpdateAsync(string id, EmployeeDto dto)
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