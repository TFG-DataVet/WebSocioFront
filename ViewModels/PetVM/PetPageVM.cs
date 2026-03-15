using SocioWeb.Domain.Entities;
using SocioWeb.Entities;
using SocioWeb.Entities.Dtos.PetDto;
using SocioWeb.Services.AppointmentService;

namespace SocioWeb.ViewModels;

public class PetPageVM
{
    

    private readonly IPetService _petService;

    

    // =======================
    // FORM DATA
    // =======================

    public RegisterPetForm FormData { get; set; } = new();

    // =======================
    // DROPDOWNS
    // =======================

    public List<string> SpeciesOptions { get; } = new()
    {
        "Perro", "Gato", "Ave"
    };

    public List<string> SexOptions { get; } = new()
    {
        "Macho", "Hembra"
    };

    public List<string> CurrentBreeds { get; private set; } = new();

    private Dictionary<string, List<string>> BreedOptions = new()
    {
        { "Perro", new() { "Golden Retriever", "Labrador", "Bulldog" } },
        { "Gato", new() { "Siamés", "Persa", "Maine Coon" } },
        { "Ave", new() { "Loro", "Canario", "Águila" } }
    };

    public void OnSpeciesChanged(object value)
    {
        if (value is string species && BreedOptions.ContainsKey(species))
            CurrentBreeds = BreedOptions[species];
        else
            CurrentBreeds = new();

        FormData.Breed = null;
    }

    // =======================
    // AGE CALCULATION
    // =======================

    public string AgeCalculated =>
        FormData.Birthday.HasValue
            ? $"{CalculateAge(FormData.Birthday.Value)} años"
            : string.Empty;

    private int CalculateAge(DateTime birthday)
    {
        var today = DateTime.Today;
        int age = today.Year - birthday.Year;
        if (birthday.Date > today.AddYears(-age)) age--;
        return age;
    }

    // =======================
    // SAVE
    // =======================

    public async Task SaveAsync()
    {
        var pet = new PetDto()
        {
            Name = FormData.NamePet!,
            Specie = FormData.Species!,
            Breed = FormData.Breed!,

            // ENUM conversion
            Sex = Enum.Parse<Sex>(FormData.Sex!, true),

            // double conversion
            Weight = double.TryParse(FormData.Weight, out var w) ? w : 0,

            // DateTime? → DateTime
            BirthDate = FormData.Birthday ?? DateTime.Today
        };

        await _petService.CreateAsync(pet);
    }

    // =======================
    // FORM MODEL
    // =======================

    public class RegisterPetForm
    {
        public string? NamePet { get; set; }
        public string? Species { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Breed { get; set; }
        public string? Sex { get; set; }
        public string? Weight { get; set; }
    }
}