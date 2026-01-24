namespace SocioWeb.Entities;
using System.ComponentModel.DataAnnotations;
public class Surgery
{
    public string SurgeryName { get; set; }

    public SurgeryType SurgeryType { get; set; }

    public DateTime? SurgeryDate { get; set; }

    public SurgeryOutcome Outcome { get; set; }

    public List<SurgeryProcedure> Procedures { get; set; }

    public string AnesthesiaType { get; set; }

    public List<SurgeryMedication> PostOpMedications { get; set; }

    public bool HospitalizationRequired { get; set; }

    public int? HospitalizationDays { get; set; }

    public bool FollowUpRequired { get; set; }

    public DateTime? FollowUpDate { get; set; }
}

public enum SurgeryType
{
    [Display(Name = "Preventiva")]
    Preventive,

    [Display(Name = "Correctiva")]
    Corrective,

    [Display(Name = "Emergencia")]
    Emergency,

    [Display(Name = "Diagnóstica")]
    Diagnostic,

    [Display(Name = "Reconstructiva")]
    Reconstructive
}

public enum SurgeryOutcome
{
    [Display(Name = "Exitosa")]
    Successful,

    [Display(Name = "Exitosa con complicaciones")]
    SuccessfulWithComplications,

    [Display(Name = "Fallida")]
    Failed,

    [Display(Name = "En progreso")]
    InProgress
}


public class SurgeryProcedure
{
    public string name { get; set; }
    public string description { get; set; }
}

public class SurgeryMedication
{
    public string Name { get; set; }

    public string Dosage { get; set; }

    public string Frequency { get; set; }

    public int? DurationInDays { get; set; }

    public string Notes { get; set; }
}