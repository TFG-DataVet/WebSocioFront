using System.Net.Http.Json;
using SocioWeb.Entities.Dtos.AuthDto;

namespace SocioWeb.Infrastructure.Services.Auth;

public class AuthApiService : IAuthService
{
    private readonly HttpClient _http;

    public AuthApiService(HttpClient http) => _http = http;

    public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto dto)
    {
        var response = await _http.PostAsJsonAsync("auth/register", dto);
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadFromJsonAsync<RegisterResponseDto>()
               ?? throw new Exception("Respuesta vacía del servidor");
    }

    public async Task<TokenResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var response = await _http.PostAsJsonAsync("auth/login", dto);
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadFromJsonAsync<TokenResponseDto>()
               ?? throw new Exception("Respuesta vacía del servidor");
    }

    public async Task<TokenResponseDto> VerifyEmailAsync(string token)
    {
        var response = await _http.GetAsync($"auth/verify-email?token={Uri.EscapeDataString(token)}");
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadFromJsonAsync<TokenResponseDto>()
               ?? throw new Exception("Respuesta vacía del servidor");
    }

    public async Task LogoutAsync()
    {
        await _http.PostAsync("auth/logout", null);
    }

    public async Task<TokenResponseDto?> GetMeAsync()
    {
        var response = await _http.GetAsync("auth/me");
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<TokenResponseDto>();
    }
}
