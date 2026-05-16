using SocioWeb.Domain.Entities;

namespace SocioWeb.ViewModels;

// Clase de compatibilidad — no registrada en DI
public class ProductProfileVM
{
    public Product Product { get; private set; } = new();
    public bool IsEditing { get; private set; } = false;

    public void ToggleEdit() => IsEditing = !IsEditing;
    public void CancelEdit() => IsEditing = false;
}
