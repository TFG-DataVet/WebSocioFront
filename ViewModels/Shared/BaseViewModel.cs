namespace SocioWeb.ViewModels.Shared;

public abstract class BaseViewModel
{
    public bool IsLoading { get; set; }
    public string? ErrorMessage { get; set; }
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

    protected void SetError(string message) => ErrorMessage = message;
    protected void ClearError() => ErrorMessage = null;

    /// <summary>
    /// Versión pública para enlazar desde la vista (p.ej. botón de cerrar alerta).
    /// </summary>
    public void ClearErrorPublic() => ErrorMessage = null;
}