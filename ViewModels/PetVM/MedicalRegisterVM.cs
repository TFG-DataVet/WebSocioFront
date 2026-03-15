using Microsoft.AspNetCore.Components;
using SocioWeb.Entities;

namespace SocioWeb.ViewModels;


    public class MedicalRegisterVM
    {
        private readonly NavigationManager _nav;

        public MedicalRegisterVM(NavigationManager nav)
        {
            _nav = nav;

            Consultation = new();
            Vaccine = new();
            Treatment = new() { Medications = new List<TreatmentMedication>() };
            Surgery = new()
            {
                Procedures = new List<SurgeryProcedure>(),
                PostOpMedications = new List<SurgeryMedication>()
            };
            Weight = new();
            Diagnosis = new();
            Allergy = new();
            Document = new();
        }

        public Consultation Consultation { get; set; }
        public Vaccine Vaccine { get; set; }
        public Treatment Treatment { get; set; }
        public Surgery Surgery { get; set; }
        public WeightRecord Weight { get; set; }
        public Diagnosis Diagnosis { get; set; }
        public Allergy Allergy { get; set; }
        public DocumentRecord Document { get; set; }

        public MedicalCase SelectedCase { get; private set; } = MedicalCase.Consultation;

        public string Title => SelectedCase switch
        {
            MedicalCase.Consultation => "Registro de Consulta",
            MedicalCase.Vaccine => "Registro de Vacunación",
            MedicalCase.Treatment => "Registro de Tratamiento",
            MedicalCase.Surgery => "Registro de Cirugía",
            MedicalCase.Weight => "Control de Peso",
            MedicalCase.Diagnosis => "Registro de Diagnóstico",
            MedicalCase.Allergy => "Registro de Alergias",
            MedicalCase.Document => "Documentos Clínicos",
            _ => "Registro Médico"
        };

        public void SelectCase(MedicalCase medicalCase)
        {
            SelectedCase = medicalCase;
        }

        public async Task SaveAsync()
        {
            switch (SelectedCase)
            {
                case MedicalCase.Consultation:
                    await SaveConsultation();
                    break;
                case MedicalCase.Vaccine:
                    await SaveVaccine();
                    break;
                case MedicalCase.Treatment:
                    await SaveTreatment();
                    break;
                case MedicalCase.Surgery:
                    await SaveSurgery();
                    break;
                case MedicalCase.Weight:
                    await SaveWeight();
                    break;
                case MedicalCase.Diagnosis:
                    await SaveDiagnosis();
                    break;
                case MedicalCase.Allergy:
                    await SaveAllergy();
                    break;
                case MedicalCase.Document:
                    await SaveDocument();
                    break;
            }
        }

        // 🔹 Aquí luego conectas tus services reales
        private Task SaveConsultation() => Task.CompletedTask;
        private Task SaveVaccine() => Task.CompletedTask;
        private Task SaveTreatment() => Task.CompletedTask;
        private Task SaveSurgery() => Task.CompletedTask;
        private Task SaveWeight() => Task.CompletedTask;
        private Task SaveDiagnosis() => Task.CompletedTask;
        private Task SaveAllergy() => Task.CompletedTask;
        private Task SaveDocument() => Task.CompletedTask;

        public void AddMedication()
        {
            Treatment.Medications ??= new();
            Treatment.Medications.Add(new TreatmentMedication());
        }

        public void AddProcedure()
        {
            Surgery.Procedures ??= new();
            Surgery.Procedures.Add(new SurgeryProcedure());
        }

        public void AddPostOpMedication()
        {
            Surgery.PostOpMedications ??= new();
            Surgery.PostOpMedications.Add(new SurgeryMedication());
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
    }
