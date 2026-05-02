using SocioWeb.Entities;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Domain.Entities;

namespace SocioWeb.ViewModels
{
    public class PetsMenuVM
    {
        private readonly IPetService _service;

        public List<Pet> Pets { get; private set; } = new();
        public List<Pet> FilteredPets { get; private set; } = new();

        public string NameFilter { get; set; } = string.Empty;
        public string SpecieFilter { get; set; } = string.Empty;
        public string ErrorMessage { get; private set; } = string.Empty;
        public bool IsLoading { get; private set; }

        public HashSet<string> VisibleOwners { get; } = new();

        public PetsMenuVM(IPetService service)
        {
            _service = service;
        }

        // ─── CARGA REAL DESDE EL BACKEND ─────────────────────────
        public async Task LoadAsync()
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                Pets = await _service.GetAllAsync();
                FilteredPets = Pets;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar mascotas: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        // ─── FILTROS ──────────────────────────────────────────────
        public void ApplyFilters()
        {
            FilteredPets = Pets
                .Where(p =>
                    (string.IsNullOrWhiteSpace(NameFilter) ||
                     p.Name.Contains(NameFilter, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrWhiteSpace(SpecieFilter) ||
                     (p.Specie ?? "").Contains(SpecieFilter, StringComparison.OrdinalIgnoreCase))
                )
                .ToList();
        }

        // ─── TOGGLE DUEÑO ─────────────────────────────────────────
        public void ToggleOwner(string petId)
        {
            if (VisibleOwners.Contains(petId))
                VisibleOwners.Remove(petId);
            else
                VisibleOwners.Add(petId);
        }
    }
}