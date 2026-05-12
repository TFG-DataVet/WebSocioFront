using System.Net.Http.Json;
using SocioWeb.Entities.Dtos.MedicalDto.AppointmentDto;
using SocioWeb.Entities.Models.Appointments;

namespace SocioWeb.Services.AppointmentService;

public class AppointmentsApiService : IAppointmentService
{
    private readonly HttpClient _http;

    public AppointmentsApiService(HttpClient http) => _http = http;

    // GET /appointments?date=&status=&type=&ownerId=
    public async Task<List<Appointment>> GetAllAsync(
        DateOnly?          date    = null,
        AppointmentStatus? status  = null,
        AppointmentType?   type    = null,
        string?            ownerId = null)
    {
        var query = BuildQuery(date, status, type, ownerId);
        return await _http.GetFromJsonAsync<List<Appointment>>($"appointments{query}")
               ?? new List<Appointment>();
    }

    // GET /appointments/{id}
    public async Task<Appointment?> GetByIdAsync(string id)
        => await _http.GetFromJsonAsync<Appointment>($"appointments/{id}");

    // POST /appointments
    public async Task<Appointment> CreateAsync(CreateAppointmentDto dto)
    {
        // Java LocalDateTime no admite timezone offset.
        // Forzamos Unspecified para que System.Text.Json serialice sin "+02:00".
        dto.ScheduledAt = DateTime.SpecifyKind(dto.ScheduledAt, DateTimeKind.Unspecified);

        var response = await _http.PostAsJsonAsync("appointments", dto);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Appointment>())!;
    }

    // PATCH /appointments/{id}/status
    public async Task<Appointment> UpdateStatusAsync(string id, UpdateAppointmentStatusDto dto)
    {
        var response = await _http.PatchAsJsonAsync($"appointments/{id}/status", dto);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Appointment>())!;
    }

    // DELETE /appointments/{id}
    public async Task CancelAsync(string id, string? reason = null)
    {
        HttpResponseMessage response;
        if (reason is not null)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"appointments/{id}")
            {
                Content = JsonContent.Create(new CancelAppointmentDto { Reason = reason })
            };
            response = await _http.SendAsync(request);
        }
        else
        {
            response = await _http.DeleteAsync($"appointments/{id}");
        }
        response.EnsureSuccessStatusCode();
    }

    // -------------------------------------------------------------------------
    private static string BuildQuery(
        DateOnly?          date,
        AppointmentStatus? status,
        AppointmentType?   type,
        string?            ownerId)
    {
        var parts = new List<string>();
        if (date    is not null) parts.Add($"date={date:yyyy-MM-dd}");
        if (status  is not null) parts.Add($"status={status}");
        if (type    is not null) parts.Add($"type={type}");
        if (ownerId is not null) parts.Add($"ownerId={Uri.EscapeDataString(ownerId)}");
        return parts.Count > 0 ? "?" + string.Join("&", parts) : string.Empty;
    }
}
