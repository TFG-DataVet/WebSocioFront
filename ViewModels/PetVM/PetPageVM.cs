using System.Text.Json;
using Microsoft.AspNetCore.Components;
using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Domain.Entities;
using SocioWeb.Entities.Dtos;
using SocioWeb.Entities.Dtos.PetDto;

namespace SocioWeb.ViewModels.Pets;

public class PetPageVM : BaseViewModel
{
    private readonly IPetService _service;
    private readonly NavigationManager _nav;

    public PetFormModel FormData { get; set; } = new();
    public bool IsSaving { get; private set; }

    public List<string> SpeciesOptions { get; } = new() { "Perro", "Gato", "Ave", "Conejo", "Reptil", "Otro" };
    public List<string> CurrentBreeds { get; private set; } = new();

    private static readonly Dictionary<string, List<string>> _breedMap = new()
    {
        { "Perro",   new() { "Labrador", "Golden Retriever", "Bulldog", "Pastor Alemán", "Caniche", "Otro" } },
        { "Gato",    new() { "Siamés", "Persa", "Maine Coon", "Bengalí", "Común europeo", "Otro" } },
        { "Ave",     new() { "Loro", "Canario", "Agapornis", "Cacatúa", "Otro" } },
        { "Conejo",  new() { "Enano", "Angora", "Rex", "Otro" } },
        { "Reptil",  new() { "Iguana", "Gecko", "Tortuga", "Otro" } },
        { "Otro",    new() { "Otro" } },
    };

    public PetPageVM(IPetService service, NavigationManager nav)
    {
        _service = service;
        _nav     = nav;
    }

    // ─── CARGA DE MASCOTA PARA EDICIÓN ─────────────────────────────
    public async Task LoadForEditAsync(string id)
    {
        IsLoading = true;
        ClearError();

        try
        {
            var pet = await _service.GetByIdAsync(id);

            if (pet is not null)
            {
                FormData = new PetFormModel
                {
                    ClinicId    = pet.IdClinic,
                    Name        = pet.Name,
                    Species     = pet.Specie,
                    Breed       = pet.Breed,
                    Sex         = pet.Sex,
                    DateOfBirth = pet.BirthDate,
                    ChipNumber  = pet.Chip,
                    AvatarUrl   = pet.AvatarUrl,

                    Owner = new OwnerModel
                    {
                        OwnerId       = pet.Owner?.Id ?? pet.IdOwner ?? string.Empty,
                        OwnerName     = pet.Owner?.Name ?? string.Empty,
                        OwnerLastName = pet.Owner?.LastName ?? string.Empty,
                        OwnerPhone    = pet.Owner?.Phone ?? string.Empty
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

    // ─── CREAR MASCOTA ────────────────────────────────────────────
    public async Task CreateAsync()
    {
        IsSaving = true;
        ClearError();

        try
        {
            var dto = MapToDto();
            await _service.CreateAsync(dto);
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

    // ─── ACTUALIZAR MASCOTA ───────────────────────────────────────
    public async Task UpdateAsync(string id)
    {
        IsSaving = true;
        ClearError();

        try
        {
            var dto = MapToDto();
            await _service.UpdateAsync(id, dto);
        }
        catch (Exception ex)
        {
            SetError($"Error al actualizar la mascota");
        }
        finally
        {
            IsSaving = false;
        }
    }

    // ─── MAPEO A DTO ─────────────────────────────────────────────
    private PetDto MapToDto() => new PetDto
    {
        ClinicId    = FormData.ClinicId ?? string.Empty,
        Name        = FormData.Name ?? string.Empty,
        Species     = FormData.Species ?? string.Empty,
        Breed       = FormData.Breed ?? string.Empty,
        Sex = FormData.Sex ?? Sex.Other,
        DateOfBirth = FormData.DateOfBirth ?? DateTime.UtcNow,
        ChipNumber  = FormData.ChipNumber,
        AvatarUrl   = FormData.AvatarUrl,
        Owner       = new OwnerDto
        {
            Id        = FormData.Owner.OwnerId ?? string.Empty,
            Name      = FormData.Owner.OwnerName ?? string.Empty,
            LastName  = FormData.Owner.OwnerLastName ?? string.Empty,
            Phone     = FormData.Owner.OwnerPhone ?? string.Empty
        }
    };

    // ─── HELPERS ────────────────────────────────────────────────
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
            : string.Empty;

    private static int CalculateAge(DateTime birthday)
    {
        var today = DateTime.Today;
        int age = today.Year - birthday.Year;
        if (birthday.Date > today.AddYears(-age))
            age--;
        return age;
    }

    public void NavigateToNewOwner()
    {
        _nav.NavigateTo("/DuenoFormulario");
    }

    // ─── MODELO DE FORMULARIO ────────────────────────────────────
    public class PetFormModel
    {
        public string? ClinicId { get; set; }
        public string? Name { get; set; }
        public string? Species { get; set; }
        public string? Breed { get; set; }
        public Sex? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ChipNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public OwnerModel Owner { get; set; } = new();
    }

    public class OwnerModel
    {
        public string? OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerLastName { get; set; }
        public string? OwnerPhone { get; set; }
    }
}