using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos;
using SocioWeb.Entities.Dtos.PetDto;

namespace SocioWeb.ViewModels.Pets;

public class PetPageVM : BaseViewModel
{
    private readonly IPetService   _petService;
    private readonly IOwnerService _ownerService;
    private readonly NavigationManager _nav;
    private readonly AuthenticationStateProvider _authProvider;

    public PetFormModel FormData { get; set; } = new();
    public bool IsSaving { get; private set; }

    // ─── Lista de dueños para el buscador ─────────────────────────────────────
    public List<Owner> Owners { get; private set; } = new();
    public bool LoadingOwners { get; private set; }

    public List<string> SpeciesOptions { get; } = new() { "Perro", "Gato", "Ave", "Conejo", "Reptil", "Otro" };
    public List<string> CurrentBreeds { get; private set; } = new();

    private static readonly Dictionary<string, List<string>> _breedMap = new()
    {
        { "Perro",  new() { "Labrador", "Golden Retriever", "Bulldog", "Pastor Alemán", "Caniche", "Otro" } },
        { "Gato",   new() { "Siamés", "Persa", "Maine Coon", "Bengalí", "Común europeo", "Otro" } },
        { "Ave",    new() { "Loro", "Canario", "Agapornis", "Cacatúa", "Otro" } },
        { "Conejo", new() { "Enano", "Angora", "Rex", "Otro" } },
        { "Reptil", new() { "Iguana", "Gecko", "Tortuga", "Otro" } },
        { "Otro",   new() { "Otro" } },
    };

    public PetPageVM(IPetService petService, IOwnerService ownerService,
                     NavigationManager nav, AuthenticationStateProvider authProvider)
    {
        _petService   = petService;
        _ownerService = ownerService;
        _nav          = nav;
        _authProvider = authProvider;
    }

    // ─── Inicialización: obtiene clinicId y carga dueños ──────────────────────

    public async Task InitAsync()
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        var clinicId  = authState.User.FindFirst("ClinicId")?.Value ?? "";
        FormData.ClinicId = clinicId;
        await LoadOwnersAsync();
    }

    public async Task LoadOwnersAsync()
    {
        LoadingOwners = true;
        try
        {
            Owners = await _ownerService.GetAllAsync();
        }
        catch
        {
            Owners = new();
        }
        finally
        {
            LoadingOwners = false;
        }
    }

    // ─── Selección de dueño desde el buscador ─────────────────────────────────

    public void OnOwnerSelected(string ownerId)
    {
        var owner = Owners.FirstOrDefault(o => o.Id == ownerId);
        if (owner is null) return;

        FormData.Owner = new OwnerModel
        {
            OwnerId       = owner.Id,
            OwnerName     = owner.Name,
            OwnerLastName = owner.LastName,
            OwnerPhone    = owner.Phone ?? "",
        };
    }

    // ─── Carga de mascota para edición ────────────────────────────────────────

    public async Task LoadForEditAsync(string id)
    {
        IsLoading = true;
        ClearError();
        try
        {
            await LoadOwnersAsync();

            var pet = await _petService.GetByIdAsync(id);
            if (pet is not null)
            {
                var authState = await _authProvider.GetAuthenticationStateAsync();
                FormData = new PetFormModel
                {
                    ClinicId    = authState.User.FindFirst("ClinicId")?.Value ?? pet.IdClinic,
                    Name        = pet.Name,
                    Species     = pet.Specie,
                    Breed       = pet.Breed,
                    Sex         = pet.Sex,
                    DateOfBirth = DateTime.TryParse(pet.BirthDate, out var d) ? d : null,
                    ChipNumber  = pet.Chip,
                    AvatarUrl   = pet.AvatarUrl,
                    Owner = new OwnerModel
                    {
                        OwnerId       = pet.Owner?.OwnerId   ?? "",
                        OwnerName     = pet.Owner?.Name      ?? "",
                        OwnerLastName = pet.Owner?.LastName  ?? "",
                        OwnerPhone    = pet.Owner?.Phone     ?? "",
                    }
                };
                OnSpeciesChanged(FormData.Species);
            }
            else
            {
                SetError("No se encontró la mascota.");
            }
        }
        catch (Exception ex)
        {
            SetError($"Error al cargar datos: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    // ─── Crear mascota ────────────────────────────────────────────────────────

    public async Task CreateAsync()
    {
        IsSaving = true;
        ClearError();
        try
        {
            await _petService.CreateAsync(MapToDto());
        }
        catch (Exception ex)
        {
            SetError($"Error al registrar la mascota: {ex.Message}");
        }
        finally
        {
            IsSaving = false;
        }
    }

    // ─── Actualizar mascota ───────────────────────────────────────────────────

    public async Task UpdateAsync(string id)
    {
        IsSaving = true;
        ClearError();
        try
        {
            await _petService.UpdateAsync(id, MapToDto());
        }
        catch (Exception ex)
        {
            SetError($"Error al actualizar la mascota: {ex.Message}");
        }
        finally
        {
            IsSaving = false;
        }
    }

    // ─── Mapeo a DTO ──────────────────────────────────────────────────────────

    private PetDto MapToDto() => new PetDto
    {
        ClinicId    = FormData.ClinicId ?? "",
        Name        = FormData.Name     ?? "",
        Species     = FormData.Species  ?? "",
        Breed       = FormData.Breed    ?? "",
        Sex         = FormData.Sex ?? Sex.UNKNOWN,
        DateOfBirth = FormData.DateOfBirth.HasValue
            ? FormData.DateOfBirth.Value.ToString("yyyy-MM-dd")
            : null,
        ChipNumber  = FormData.ChipNumber,
        AvatarUrl   = FormData.AvatarUrl,
        Owner = new PetOwnerDto
        {
            OwnerId       = FormData.Owner.OwnerId       ?? "",
            OwnerName     = FormData.Owner.OwnerName     ?? "",
            OwnerLastName = FormData.Owner.OwnerLastName ?? "",
            OwnerPhone    = FormData.Owner.OwnerPhone    ?? "",
        }
    };

    // ─── Helpers ──────────────────────────────────────────────────────────────

    public void OnSpeciesChanged(object value)
    {
        if (value is string species && _breedMap.ContainsKey(species))
            CurrentBreeds = _breedMap[species];
        else
            CurrentBreeds = new();

        FormData.Breed = null;
    }

    public string AgeCalculated =>
        FormData.DateOfBirth.HasValue
            ? $"{CalculateAge(FormData.DateOfBirth.Value)} años"
            : "";

    private static int CalculateAge(DateTime birthday)
    {
        var today = DateTime.Today;
        int age   = today.Year - birthday.Year;
        if (birthday.Date > today.AddYears(-age)) age--;
        return age;
    }

    // ─── Modelos de formulario ────────────────────────────────────────────────

    public class PetFormModel
    {
        public string? ClinicId    { get; set; }
        public string? Name        { get; set; }
        public string? Species     { get; set; }
        public string? Breed       { get; set; }
        public Sex?    Sex         { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ChipNumber  { get; set; }
        public string? AvatarUrl   { get; set; }
        public OwnerModel Owner    { get; set; } = new();
    }

    public class OwnerModel
    {
        public string? OwnerId       { get; set; }
        public string? OwnerName     { get; set; }
        public string? OwnerLastName { get; set; }
        public string? OwnerPhone    { get; set; }
    }
}
