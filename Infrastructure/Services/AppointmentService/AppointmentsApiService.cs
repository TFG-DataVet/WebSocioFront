using SocioWeb.Entities.Dtos.MedicalDto.AppointmentDto;

namespace SocioWeb.Services.AppointmentService;

using System.Net.Http.Json;

public class AppointmentsApiService : IAppointmentService
{
    private readonly HttpClient _http;

    public AppointmentsApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        try
        {
            return await _http.GetFromJsonAsync<List<Appointment>>("appointments")
                   ?? new List<Appointment>();
        }
        catch { return new List<Appointment>(); }
    }

    public async Task<Appointment?> GetByIdAsync(String id)
    {
        try { return await _http.GetFromJsonAsync<Appointment>($"appointments/{id}"); }
        catch { return null; }
    }

    public async Task CreateAsync(AppointmentDto dto)
        => await _http.PostAsJsonAsync("appointments", dto);

    public async Task UpdateAsync(String id, AppointmentDto dto)
        => await _http.PutAsJsonAsync($"appointments/{id}", dto);

    public async Task DeleteAsync(String id)
        => await _http.DeleteAsync($"appointments/{id}");
}