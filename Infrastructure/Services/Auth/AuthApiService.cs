using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using SocioWeb.Entities.Dtos.Auth;
using SocioWeb.Entities.Models.ApiErrorResponse;
using SocioWeb.Entities.Models.Auth;

namespace SocioWeb.Services.AppointmentService;

public class AuthApiService : IAuthService
{
    private readonly HttpClient _http;
    private readonly OnboardingState _onboardingState;

    public AuthApiService(HttpClient http, OnboardingState onboardingState)
    {
        _http = http;
        _onboardingState = onboardingState;
    }

    // -------------------------------------------------------------------------
    // Paso 1 — Registro
    // Sin cambios respecto a lo que tenías
    // -------------------------------------------------------------------------

    public async Task<RegisterClinic> RegisterInitialAsync(RegisterClinic clinic)
    {
        var response = await _http.PostAsJsonAsync("auth/register", clinic);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<RegisterClinic>();
        }

        await ThrowApiException(response);
        return null;
    }

    // -------------------------------------------------------------------------
    // Paso 2 — Verificación de email
    // Recibe el token temporal en el body y lo guarda en OnboardingState (memoria)
    // -------------------------------------------------------------------------

    public async Task<AuthResponse> VerifyEmailAsync(string token)
    {
        var response = await _http.GetAsync($"auth/verify-email?token={token}");

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<AuthResponse>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Guardamos el token temporal en memoria
            _onboardingState.OnboardingToken = data.AccessToken;
            _onboardingState.ClinicId = data.User?.ClinicId;
            _onboardingState.UserId = data.User?.UserId;

            return data;
        }

        await ThrowApiException(response);
        return null;
    }

    // -------------------------------------------------------------------------
    // Paso 3 — Completar perfil de clínica
    // Envía el token temporal en el header Authorization
    // porque ese token no está en cookie, está en memoria (OnboardingState)
    // -------------------------------------------------------------------------

    public async Task<bool> CompleteClinicProfileAsync(Guid clinicId, FullClinicData clinic)
    {
        // Creamos una request manualmente para poder añadir el header
        var request = new HttpRequestMessage(
            HttpMethod.Patch,
            $"clinic/{clinicId}/complete-setup");

        // Añadimos el token temporal de onboarding en el header
        if (!string.IsNullOrEmpty(_onboardingState.OnboardingToken))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", _onboardingState.OnboardingToken);
        }

        request.Content = JsonContent.Create(clinic);

        var response = await _http.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            // El paso 3 devuelve el JWT definitivo — el backend lo escribe
            // como cookie HttpOnly automáticamente, nosotros no hacemos nada más
            // Limpiamos el estado del onboarding porque ya no lo necesitamos
            _onboardingState.Clear();
            return true;
        }

        await ThrowApiException(response);
        return false;
    }

    // -------------------------------------------------------------------------
    // Reenvío de verificación
    // Sin cambios
    // -------------------------------------------------------------------------

    public async Task ResendEmailToVerify(string email)
    {
        var response = await _http.PostAsJsonAsync(
            "auth/resend-verification", new { email });

        if (response.IsSuccessStatusCode) return;

        await ThrowApiException(response);
    }

    // -------------------------------------------------------------------------
    // Login
    // El backend escribe la cookie — nosotros solo leemos el userInfo del body
    // -------------------------------------------------------------------------

    public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest)
    {
        var response = await _http.PostAsJsonAsync("auth/login", loginRequest);

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<AuthResponse>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Si el usuario aún no completó el onboarding, guardamos
            // el token temporal en memoria igual que en el paso 2
            if (data.NextStep == "COMPLETE_SETUP")
            {
                _onboardingState.OnboardingToken = data.AccessToken;
                _onboardingState.ClinicId = data.User?.ClinicId;
                _onboardingState.UserId = data.User?.UserId;
            }

            // En sesión normal el token ya está en la cookie HttpOnly
            // El body solo tiene el userInfo, que es lo que devolvemos
            return data;
        }

        await ThrowApiException(response);
        return null;
    }

    // -------------------------------------------------------------------------
    // Logout
    // El backend vacía las cookies — nosotros solo hacemos la llamada
    // -------------------------------------------------------------------------

    public async Task LogoutAsync()
    {
        // No necesitamos enviar el refreshToken en el body
        // El backend lo lee directamente de la cookie
        await _http.PostAsync("auth/logout", null);
    }
    
    public async Task<AuthResponse?> GetCurrentUserAsync()
    {
        try
        {
            var response = await _http.GetAsync("auth/me");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AuthResponse>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            // 401 = no hay sesión activa, devolvemos null sin lanzar excepción
            return null;
        }
        catch
        {
            return null;
        }
    }

    // -------------------------------------------------------------------------
    // Refresh
    // El backend rota las cookies automáticamente — el frontend no hace nada especial
    // Llamas a este método cuando una petición devuelva 401
    // -------------------------------------------------------------------------

    public async Task<bool> RefreshTokenAsync()
    {
        var response = await _http.PostAsync("auth/refresh", null);
        return response.IsSuccessStatusCode;
    }

    // -------------------------------------------------------------------------
    // Recuperar contraseña — siempre silencioso (anti-enumeración)
    // -------------------------------------------------------------------------

    public async Task ForgotPasswordAsync(string email)
    {
        await _http.PostAsJsonAsync("auth/forgot-password", new { email });
        // Ignoramos la respuesta intencionalmente
    }

    // -------------------------------------------------------------------------
    // Resetear contraseña — lanza excepción si 400
    // -------------------------------------------------------------------------

    public async Task ResetPasswordAsync(string token, string newPassword)
    {
        var response = await _http.PostAsJsonAsync(
            "auth/reset-password", new { token, newPassword });
        if (!response.IsSuccessStatusCode)
            await ThrowApiException(response);
    }

    // -------------------------------------------------------------------------
    // Helper privado — extrae el error de la respuesta y lanza una excepción
    // -------------------------------------------------------------------------

    private async Task ThrowApiException(HttpResponseMessage response)
    {
        var errorString = await response.Content.ReadAsStringAsync();

        ApiErrorResponse errorObj = null;
        try
        {
            errorObj = JsonSerializer.Deserialize<ApiErrorResponse>(errorString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (JsonException) { }

        if (errorObj != null && !string.IsNullOrEmpty(errorObj.Message))
        {
            throw new Exception(errorObj.Message);
        }

        throw new Exception($"Error desconocido del servidor: {errorString}");
    }
}