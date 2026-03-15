namespace SocioWeb.ViewModels.Shared;

public abstract class BaseViewModel
{
    public bool IsLoading { get; set; }
    public string? ErrorMessage { get; set; }
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

    protected void SetError(string message) => ErrorMessage = message;
    protected void ClearError() => ErrorMessage = null;
}