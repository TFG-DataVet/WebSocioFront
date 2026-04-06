namespace SocioWeb.Services.Exceptions;

/// <summary>
/// Excepción que transporta el body JSON de error que devuelve el backend.
/// Permite que los ViewModels lean el mensaje estructurado del servidor.
/// </summary>
public class ApiException : Exception
{
    public int StatusCode { get; }
    public string ResponseBody { get; }

    public ApiException(int statusCode, string responseBody)
        : base($"API error {statusCode}: {responseBody}")
    {
        StatusCode   = statusCode;
        ResponseBody = responseBody;
    }
}