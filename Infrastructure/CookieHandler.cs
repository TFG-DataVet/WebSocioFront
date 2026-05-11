using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace SocioWeb.Infrastructure;

public class CookieHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Añadir credenciales a cada petición para que
        // el navegador adjunte y acepte las cookies
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        
        return await base.SendAsync(request, cancellationToken);
    }
}