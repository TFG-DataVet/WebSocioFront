

using System.Net.Http.Json;
using SocioWeb.Domain.Entities;

namespace SocioWeb.Services.AppointmentService
{
    public class OwnerService
    {
        private readonly HttpClient _http;

        public OwnerService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Owner>> GetOwnersAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<Owner>>("api/owners");

                if (result == null)
                    return new List<Owner>();

                return result.OrderBy(o => o.CreatedAt).ToList();
            }
            catch
            {
                return new List<Owner>();
            }
        }
    }
}