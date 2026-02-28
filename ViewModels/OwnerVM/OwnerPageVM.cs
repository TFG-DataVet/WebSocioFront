using SocioWeb.Domain.Entities;

using SocioWeb.Entities;

namespace SocioWeb.ViewModels.OwnerVM
{
    public class OwnerPageVM
    {
        public List<Owner> Owners { get; private set; } = new();
        public List<Owner> FilteredOwners { get; private set; } = new();

        // Filtros
        public DateTime? DateSince { get; set; }
        public DateTime? DateTo { get; set; }
        public string CityFilter { get; set; } = string.Empty;
        public string PostalCodeFilter { get; set; } = string.Empty;

        // UI
        public HashSet<string> VisiblePets { get; } = new();
        public bool IsLoading { get; private set; }
        public string? ErrorMessage { get; private set; }

        public OwnerPageVM() { }

        public Task LoadAsync()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = null;

                // --- DATOS DE EJEMPLO ---
                Owners = new List<Owner>
                {
                    new Owner
                    {
                        Id = "O1",
                        Name = "Juan Pérez",
                        City = "Madrid",
                        PostalCode = "28001",
                        CreatedAt = new DateTime(2022, 1, 10),
                        Pet = new List<Pet>
                        {
                            new Pet { Id = "P1", Name = "Toby", Specie = "Perro", Breed="Labrador" },
                            new Pet { Id = "P2", Name = "Mimi", Specie = "Gato", Breed="Siamés" }
                        }
                    },
                    new Owner
                    {
                        Id = "O2",
                        Name = "Ana Gómez",
                        City = "Barcelona",
                        PostalCode = "08001",
                        CreatedAt = new DateTime(2023, 3, 15),
                        Pet = new List<Pet>
                        {
                            new Pet { Id = "P3", Name = "Rocky", Specie = "Perro", Breed="Golden Retriever" }
                        }
                    },
                    new Owner
                    {
                        Id = "O3",
                        Name = "Carlos Ruiz",
                        City = "Valencia",
                        PostalCode = "46001",
                        CreatedAt = new DateTime(2021, 7, 5),
                        Pet = new List<Pet>()
                    }
                };

                Owners = Owners.OrderBy(o => o.CreatedAt).ToList();
                FilteredOwners = Owners;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsLoading = false;
            }

            return Task.CompletedTask;
        }

        public void ApplyFilters()
        {
            FilteredOwners = Owners
                .Where(o =>
                    (!DateSince.HasValue || o.CreatedAt >= DateSince.Value) &&
                    (!DateTo.HasValue || o.CreatedAt <= DateTo.Value) &&
                    (string.IsNullOrWhiteSpace(CityFilter) ||
                     o.City.Contains(CityFilter, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(PostalCodeFilter) ||
                     o.PostalCode.Contains(PostalCodeFilter, StringComparison.OrdinalIgnoreCase))
                )
                .ToList();
        }

        public void TogglePets(string ownerId)
        {
            if (VisiblePets.Contains(ownerId))
                VisiblePets.Remove(ownerId);
            else
                VisiblePets.Add(ownerId);
        }
    }
}