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
        => await _http.GetFromJsonAsync<List<Appointment>>("api/appointments")
           ?? new List<Appointment>();

    public async Task<Appointment?> GetByIdAsync(String id)
        => await _http.GetFromJsonAsync<Appointment>($"api/appointments/{id}");

    public async Task CreateAsync(AppointmentDto dto)
        => await _http.PostAsJsonAsync("api/appointments", dto);

    public async Task UpdateAsync(String id, AppointmentDto dto)
        => await _http.PutAsJsonAsync($"api/appointments/{id}", dto);

    public async Task DeleteAsync(String id)
        => await _http.DeleteAsync($"api/appointments/{id}");
}