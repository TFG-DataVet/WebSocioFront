namespace SocioWeb.ViewModels.Medical;

using SocioWeb.ViewModels.Shared;
using SocioWeb.Entities;

public class MedicalRegisterViewModel : BaseViewModel
{
    public Consultation Consultation { get; set; } = new();
    public Vaccine Vaccine { get; set; } = new();
    public Treatment Treatment { get; set; } = new();
    public Surgery Surgery { get; set; } = new();
    public WeightRecord Weight { get; set; } = new();
    public Diagnosis Diagnosis { get; set; } = new();
    public Allergy Allergy { get; set; } = new();
    public DocumentRecord Document { get; set; } = new();

    public MedicalCase SelectedCase { get; set; } = MedicalCase.Consultation;

    public string Title => SelectedCase switch
    {
        MedicalCase.Consultation => "Registro de Consulta",
        MedicalCase.Vaccine      => "Registro de Vacunación",
        MedicalCase.Treatment    => "Registro de Tratamiento",
        MedicalCase.Surgery      => "Registro de Cirugía",
        MedicalCase.Weight       => "Control de Peso",
        MedicalCase.Diagnosis    => "Registro de Diagnóstico",
        MedicalCase.Allergy      => "Registro de Alergias",
        MedicalCase.Document     => "Documentos Clínicos",
        _                        => "Registro Médico"
    };

    // Helpers para campos de texto con listas
    public string SymptomsText
    {
        get => string.Join(", ", Diagnosis.Symptoms ?? new List<string>());
        set => Diagnosis.Symptoms = value?
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .ToList();
    }

    public string RecommendationsText
    {
        get => string.Join(", ", Diagnosis.Recommendations ?? new List<string>());
        set => Diagnosis.Recommendations = value?
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(r => r.Trim())
            .ToList();
    }

    public string ReactionsText
    {
        get => string.Join(", ", Allergy.Reactions ?? new List<string>());
        set => Allergy.Reactions = value?
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(r => r.Trim())
            .ToList();
    }

    public void SelectCase(MedicalCase medicalCase) => SelectedCase = medicalCase;

    public void AddMedication()
    {
        Treatment.Medications ??= new List<TreatmentMedication>();
        Treatment.Medications.Add(new TreatmentMedication());
    }

    public void AddProcedure()
    {
        Surgery.Procedures ??= new List<SurgeryProcedure>();
        Surgery.Procedures.Add(new SurgeryProcedure());
    }

    public void AddPostOpMedication()
    {
        Surgery.PostOpMedications ??= new List<SurgeryMedication>();
        Surgery.PostOpMedications.Add(new SurgeryMedication());
    }

    public void SaveCurrentCase()
    {
        switch (SelectedCase)
        {
            case MedicalCase.Consultation: SaveConsultation(); break;
            case MedicalCase.Vaccine:      SaveVaccine();      break;
            case MedicalCase.Treatment:    SaveTreatment();    break;
            case MedicalCase.Surgery:      SaveSurgery();      break;
            case MedicalCase.Weight:       SaveWeight();       break;
            case MedicalCase.Diagnosis:    SaveDiagnosis();    break;
            case MedicalCase.Allergy:      SaveAllergy();      break;
            case MedicalCase.Document:     SaveDocument();     break;
        }
    }

    // Aquí conectar con los servicios reales en Fase 7
    private void SaveConsultation() => Console.WriteLine("Consulta guardada");
    private void SaveVaccine()      => Console.WriteLine("Vacuna guardada");
    private void SaveTreatment()    => Console.WriteLine("Tratamiento guardado");
    private void SaveSurgery()      => Console.WriteLine("Cirugía guardada");
    private void SaveWeight()       => Console.WriteLine("Peso guardado");
    private void SaveDiagnosis()    => Console.WriteLine("Diagnóstico guardado");
    private void SaveAllergy()      => Console.WriteLine("Alergia guardada");
    private void SaveDocument()     => Console.WriteLine("Documento guardado");
}

public enum MedicalCase
{
    Consultation,
    Vaccine,
    Treatment,
    Surgery,
    Weight,
    Diagnosis,
    Allergy,
    Document
}