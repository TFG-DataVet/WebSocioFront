using SocioWeb.Domain.Entities;

namespace SocioWeb.ViewModels;

public class ProductProfileVM
{

        public Product Product { get; private set; } = new();

        public bool IsEditing { get; private set; } = false;

        public async Task LoadAsync(string productId)
        {
            // --- EJEMPLO HARDCODE ---
            // Aquí iría la llamada real al servicio
            await Task.Delay(50);

            Product = new Product
            {
                Id = productId,
                Name = "Alimento Premium Perro",
                Category = "Alimentos",
                Brand = "DogPlus",
                Price = 29.99m,
                Stock = 15.ToString(),
                Active = true,
                Description = "Bolsa 10kg alimento balanceado",
                CreatedAt = DateTime.UtcNow.AddMonths(-3),
                UpdatedAt = DateTime.UtcNow,
                Historical = new List<LogEntry>
                {
                    new LogEntry { Date = DateTime.UtcNow.AddDays(-10), Description = "Producto creado" },
                    new LogEntry { Date = DateTime.UtcNow.AddDays(-2), Description = "Actualización de stock" }
                }
            };
        }

        public void ToggleEdit() => IsEditing = !IsEditing;

        public void Save()
        {
            Product.UpdatedAt = DateTime.UtcNow;
            IsEditing = false;
            // Aquí podrías llamar a un servicio para guardar los cambios
        }

        public void CancelEdit() => IsEditing = false;
    }
