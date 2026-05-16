namespace SocioWeb.ViewModels.Products;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Services.AppointmentService;
using SocioWeb.Entities.Dtos.MedicalDto.ProductDto;

public class CreateProductViewModel : BaseViewModel
{
    private readonly IProductService _service;

    // ─── Datos generales ──────────────────────────────────────────────────────
    public string  Name        { get; set; } = "";
    public string? Description { get; set; }
    public string? Sku         { get; set; }
    public string? Barcode     { get; set; }
    public decimal Price       { get; set; }
    public decimal TaxRate     { get; set; }
    public int     Stock       { get; set; }
    public int     MinStock    { get; set; }
    public string? Category    { get; set; }

    // ─── Detalles farmacia / clínica ──────────────────────────────────────────
    public string?   ActiveIngredient    { get; set; }
    public string?   Manufacturer        { get; set; }
    public string?   BatchNumber         { get; set; }
    public DateTime? ExpirationDate      { get; set; }
    public bool      PrescriptionRequired { get; set; }
    public string?   StorageConditions   { get; set; }
    public string?   AdministrationRoute { get; set; }

    // ─── Detalles tienda ──────────────────────────────────────────────────────
    public string? Brand            { get; set; }
    public string? Material         { get; set; }
    public int?    WeightGrams      { get; set; }
    public bool    IsVeterinaryDiet { get; set; }

    // ─── Detalles suministros / implantes ─────────────────────────────────────
    public bool    IsSterile       { get; set; }
    public string? ReferenceCode   { get; set; }
    public string? UnitOfMeasure   { get; set; }
    public int?    QuantityPerUnit  { get; set; }

    // ─── Detalles grooming ────────────────────────────────────────────────────
    public int?  DurationMinutes     { get; set; }
    public bool  RequiresAppointment { get; set; }

    public CreateProductViewModel(IProductService service) => _service = service;

    // ─── Validación ───────────────────────────────────────────────────────────

    public string? Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) return "El nombre es obligatorio.";
        if (Category is null)               return "Selecciona una categoría.";
        if (Price < 0)                      return "El precio no puede ser negativo.";
        if (TaxRate < 0)                    return "El IVA no puede ser negativo.";
        if (Stock < 0)                      return "El stock no puede ser negativo.";
        if (MinStock < 0)                   return "El stock mínimo no puede ser negativo.";
        return null;
    }

    // ─── Guardar ──────────────────────────────────────────────────────────────

    public async Task<bool> SaveAsync(string clinicId)
    {
        IsLoading = true;
        ClearError();
        try
        {
            var request = new CreateProductRequest
            {
                Name        = Name,
                Description = Description,
                Sku         = Sku,
                Barcode     = Barcode,
                Price       = Price,
                TaxRate     = TaxRate,
                Stock       = Stock,
                MinStock    = MinStock,
                Details     = BuildDetails(),
            };
            await _service.CreateAsync(clinicId, request);
            return true;
        }
        catch (Exception ex)
        {
            SetError($"Error al crear el producto: {ex.Message}");
            return false;
        }
        finally
        {
            IsLoading = false;
        }
    }

    // ─── Area de la categoría (para mostrar los campos adecuados en la UI) ────

    public string CategoryArea => Category switch
    {
        "MEDICATION" or "VACCINE" or "ANTIPARASITIC" or "SUPPLEMENT" => "PHARMACY",
        "FOOD" or "HYGIENE" or "ACCESSORY" or "TOY"                  => "STORE",
        "MEDICAL_SUPPLY" or "DIAGNOSTIC" or "PROSTHESIS_IMPLANT"     => "CLINIC",
        "GROOMING_SERVICE"                                            => "GROOMING",
        _                                                             => ""
    };

    // ─── Construcción del objeto details ──────────────────────────────────────

    private object BuildDetails() => Category switch
    {
        "MEDICATION" or "VACCINE" or "ANTIPARASITIC" => new
        {
            type                 = Category,
            activeIngredient     = ActiveIngredient,
            manufacturer         = Manufacturer,
            batchNumber          = BatchNumber,
            expirationDate       = ExpirationDate?.ToString("yyyy-MM-dd"),
            prescriptionRequired = (bool?)PrescriptionRequired,
            storageConditions    = StorageConditions,
            administrationRoute  = AdministrationRoute,
        },
        "SUPPLEMENT" => new
        {
            type              = "SUPPLEMENT",
            activeIngredient  = ActiveIngredient,
            manufacturer      = Manufacturer,
            storageConditions = StorageConditions,
        },
        "FOOD" => new
        {
            type             = "FOOD",
            brand            = Brand,
            weightGrams      = WeightGrams,
            isVeterinaryDiet = (bool?)IsVeterinaryDiet,
            storageConditions = StorageConditions,
        },
        "HYGIENE" or "ACCESSORY" or "TOY" => new
        {
            type     = Category,
            brand    = Brand,
            material = Material,
        },
        "MEDICAL_SUPPLY" => new
        {
            type           = "MEDICAL_SUPPLY",
            manufacturer   = Manufacturer,
            batchNumber    = BatchNumber,
            expirationDate = ExpirationDate?.ToString("yyyy-MM-dd"),
            isSterile      = (bool?)IsSterile,
            referenceCode  = ReferenceCode,
            unitOfMeasure  = UnitOfMeasure,
            quantityPerUnit = QuantityPerUnit,
        },
        "DIAGNOSTIC" => new
        {
            type          = "DIAGNOSTIC",
            manufacturer  = Manufacturer,
            referenceCode = ReferenceCode,
        },
        "PROSTHESIS_IMPLANT" => new
        {
            type          = "PROSTHESIS_IMPLANT",
            manufacturer  = Manufacturer,
            material      = Material,
            isSterile     = (bool?)IsSterile,
            referenceCode = ReferenceCode,
        },
        "GROOMING_SERVICE" => new
        {
            type                = "GROOMING_SERVICE",
            brand               = Brand,
            durationMinutes     = DurationMinutes,
            requiresAppointment = (bool?)RequiresAppointment,
        },
        _ => new { type = Category }
    };
}
