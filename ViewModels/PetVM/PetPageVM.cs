using System.Text.Json;
using Microsoft.AspNetCore.Components;
using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Entities.Dtos.PetDto;
using SocioWeb.Domain.Entities;

namespace SocioWeb.ViewModels.Pets;

public class PetPageVM : BaseViewModel
{
    private readonly IPetService _service;
    private readonly NavigationManager _nav;

    public PetFormModel FormData { get; set; } = new();
    public bool IsSaving { get; private set; }

    public List<string> SpeciesOptions { get; } = new() { "Perro", "Gato", "Ave", "Conejo", "Reptil", "Otro" };
    public List<string> SexOptions { get; } = new() { "Macho", "Hembra", "Otro" };
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

    // ─── Carga datos de la mascota para edición ────────────────────────────────
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
                    Name      = pet.Name,
                    Specie    = pet.Specie,
                    Breed     = pet.Breed,
                    Sex       = pet.Sex,
                    BirthDate = pet.BirthDate,
                    Weight    = pet.Weight,
                    Chip      = pet.Chip,
                    ClinicalInfo = pet.ClinicalInfo
                };

                OnSpeciesChanged(FormData.Specie);
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

    // ─── Crear nueva mascota ───────────────────────────────────────────────────
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

    // ─── Actualizar mascota existente ──────────────────────────────────────────
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
            SetError($"Error al actualizar la mascota: {ex.Message}");
        }
        finally
        {
            IsSaving = false;
        }
    }

    // ─── Mapeo a DTO ───────────────────────────────────────────────────────────
    private PetDto MapToDto() => new PetDto
    {
        Name        = FormData.Name ?? string.Empty,
        Species     = FormData.Specie ?? string.Empty,
        Breed       = FormData.Breed ?? string.Empty,
        Sex         = FormData.Sex ?? Sex.Male, // si es nulo, usar un valor por defecto
        DateOfBirth = FormData.BirthDate,
        Weight      = FormData.Weight,
        ChipNumber  = FormData.Chip ?? string.Empty,
        ClinicalInfo = FormData.ClinicalInfo ?? string.Empty
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
        FormData.BirthDate != default
            ? $"{CalculateAge(FormData.BirthDate)} años"
            : string.Empty;

    private static int CalculateAge(DateTime birthday)
    {
        var today = DateTime.Today;
        int age   = today.Year - birthday.Year;
        if (birthday.Date > today.AddYears(-age)) age--;
        return age;
    }

    public void NavigateToNewOwner()
    {
        _nav.NavigateTo("/DuenoFormulario");
    }

    // ─── Modelo de formulario ──────────────────────────────────────────────────
    public class PetFormModel
    {
        public string? Name { get; set; }
        public string? Specie { get; set; }
        public string? Breed { get; set; }
        public Sex? Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public double? Weight { get; set; }
        public string? Chip { get; set; }
        public string? ClinicalInfo { get; set; }
    }
}