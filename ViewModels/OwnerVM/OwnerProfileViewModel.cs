namespace SocioWeb.ViewModels.Owners;

using System.Text.Json;
using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos;

public class OwnerProfileViewModel : BaseViewModel
{
    private readonly IOwnerService _service;

    public Owner Owner { get; private set; } = new();
    public bool IsEditing { get; set; }

    public OwnerProfileViewModel(IOwnerService service) => _service = service;

    public async Task LoadAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            var found = await _service.GetByIdAsync(id);
            if (found is not null)
                Owner = found;
        }
        catch (Exception ex)
        {
            SetError(await ParseErrorAsync(ex));
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task SaveAsync()
    {
        IsLoading = true;
        ClearError();
        try
        {
            Owner.UpdatedAt = DateTime.UtcNow;
            await _service.UpdateAsync(Owner.Id, new OwnerDto());
            IsEditing = false;
        }
        catch (Exception ex)
        {
            SetError(await ParseErrorAsync(ex));
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task DeleteAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            await _service.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            SetError(await ParseErrorAsync(ex));
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void Cancel() => IsEditing = false;

    // -------------------------------------------------------------------------
    // Parseo de errores del backend
    // -------------------------------------------------------------------------

    /// <summary>
    /// Intenta extraer el mensaje de error estructurado que devuelve el backend
    /// (formato ErrorResponse: { message, details: [{field, message}] }).
    /// Si no puede parsear, devuelve el mensaje genérico de la excepción.
    /// </summary>
    private static Task<string> ParseErrorAsync(Exception ex)
    {
        try
        {
            return Task.FromResult(ExtractMessageFromJson(ex.Message));
        }
        catch
        {
            return Task.FromResult(ex.Message);
        }
    }
    /// <summary>
    /// Dado el JSON de error del backend, extrae el campo "message" y,
    /// si hay "details", los concatena en un string legible.
    /// </summary>
    internal static string ExtractMessageFromJson(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var mainMessage = root.TryGetProperty("message", out var msgProp)
                ? msgProp.GetString() ?? string.Empty
                : string.Empty;

            if (root.TryGetProperty("details", out var details) &&
                details.ValueKind == JsonValueKind.Array)
            {
                var detailMessages = details.EnumerateArray()
                    .Select(d =>
                    {
                        var field   = d.TryGetProperty("field",   out var f) ? f.GetString() : null;
                        var message = d.TryGetProperty("message", out var m) ? m.GetString() : null;
                        return field is not null && message is not null
                            ? $"{field}: {message}"
                            : message ?? string.Empty;
                    })
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .ToList();

                if (detailMessages.Any())
                    return string.Join(" | ", detailMessages);
            }

            return string.IsNullOrWhiteSpace(mainMessage) ? json : mainMessage;
        }
        catch
        {
            // Si el JSON no puede parsearse devolvemos el body crudo
            return json;
        }
    }
}
