namespace SocioWeb.Services.Exceptions;

/// <summary>
/// Extensiones sobre HttpResponseMessage para lanzar ApiException
/// con el body completo cuando el servidor responde con error.
/// </summary>
public static class HttpResponseExtensions
{
    /// <summary>
    /// Si la respuesta no es exitosa, lee el body y lanza ApiException.
    /// Úsalo en lugar de EnsureSuccessStatusCode().
    /// </summary>
    public static async Task EnsureSuccessOrThrowAsync(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode) return;

        var body = await response.Content.ReadAsStringAsync();
        throw new ApiException((int)response.StatusCode, body);
    }
}