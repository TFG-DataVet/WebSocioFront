using System.Net.Http.Json;
using System.Text.Json;
using SocioWeb.Entities.Models.ApiErrorResponse;
using SocioWeb.Entities.Models.Auth;

namespace SocioWeb.Services.AppointmentService;

public class AuthApiService : IAuthService
{

    private readonly HttpClient _http;

    public AuthApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<RegisterClinic> RegisterInitialAsync(RegisterClinic clinic)
    {
        var response = await _http.PostAsJsonAsync("auth/register", clinic);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<RegisterClinic>();
        }
        
        var errorString = await response.Content.ReadAsStringAsync();
        
        ApiErrorResponse errorObj = null;
        try
        {
            errorObj = JsonSerializer.Deserialize<ApiErrorResponse>(errorString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (JsonException)
        {
            
        }

        if (errorObj != null && !string.IsNullOrEmpty(errorObj.Message))
        {
            throw new Exception(errorObj.Message);
        }
        else
        {
            throw new Exception($"Error desconociddo del servidor: {errorString}");
        }
    }
    
    public async Task<VerifyEmailResponse> VerifyEmailAsync(string token)
    {
        var response = await _http.GetAsync($"auth/verify-email?token={token}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<VerifyEmailResponse>();
        }
        
        var errorString = await response.Content.ReadAsStringAsync();
        
        ApiErrorResponse errorObj = null;

        try
        {
            errorObj = JsonSerializer.Deserialize<ApiErrorResponse>(errorString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (JsonException)
        {
            
        }

        if (errorObj != null && !string.IsNullOrEmpty(errorObj.Message))
        {
            throw new Exception(errorObj.Message);
        }
        else
        {
            throw new Exception($"Error desconociddo del servidor: {errorString}");
        }
    }

    public async Task<bool> CompleteClinicProfileAsync(Guid clinicId, FullClinicData clinic)
    {
        var response = await _http.PatchAsJsonAsync($"clinic/{clinicId}/complete-setup", clinic);
        Console.Write(response);
        return response.IsSuccessStatusCode;
    }

    public async Task ResendEmailToVerify(string email)
    {
        var response = await _http.PostAsJsonAsync("auth/resend-verification", new { email });
        
        if (response.IsSuccessStatusCode)
        {
            return;
        }
        
        var errorString = await response.Content.ReadAsStringAsync();
        
        ApiErrorResponse errorObj = null;

        try
        {
            errorObj = JsonSerializer.Deserialize<ApiErrorResponse>(errorString,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        catch (JsonException)
        {
            
        }
        
        if (errorObj != null && !string.IsNullOrEmpty(errorObj.Message))
        {
            throw new Exception(errorObj.Message);
        }
        else
        {
            throw new Exception($"Error desconociddo del servidor: {errorString}");
        }
    }

}