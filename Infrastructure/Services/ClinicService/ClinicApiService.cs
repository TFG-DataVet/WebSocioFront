using System.Net.Http.Headers;
using System.Net.Http.Json;
using SocioWeb.Entities.Dtos.AuthDto;
using SocioWeb.Entities.Dtos.ClinicApiDto;

namespace SocioWeb.Infrastructure.Services.ClinicService;

public class ClinicApiService : IClinicService
{
    private readonly HttpClient _http;

    public ClinicApiService(HttpClient http) => _http = http;

    public async Task<ClinicResponseDto> GetByIdAsync(string clinicId)
    {
        var response = await _http.GetAsync($"clinic/{clinicId}");
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadFromJsonAsync<ClinicResponseDto>()
               ?? throw new Exception("Respuesta vacía del servidor");
    }

    public async Task<ClinicResponseDto> UpdateAsync(string clinicId, UpdateClinicDto dto)
    {
        var response = await _http.PutAsJsonAsync($"clinic/{clinicId}", dto);
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadFromJsonAsync<ClinicResponseDto>()
               ?? throw new Exception("Respuesta vacía del servidor");
    }

    public async Task<TokenResponseDto> CompleteSetupAsync(string clinicId, CompleteSetupDto dto, string onboardingToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, $"clinic/{clinicId}/complete-setup")
        {
            Content = JsonContent.Create(dto)
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", onboardingToken);

        var response = await _http.SendAsync(request);
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadFromJsonAsync<TokenResponseDto>()
               ?? throw new Exception("Respuesta vacía del servidor");
    }
}
