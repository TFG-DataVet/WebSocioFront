using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SocioWeb.Entities.Models.Enums;

namespace SocioWeb.Domain.Entities;

public class Clinic
{
    
    public string       ClinicId             { get; set; }
    public string       ClinicName           { get; set; }
    public string       LegalName            { get; set; }
    public string       LegalNumber          { get; set; }
    public LegalType?   LegalType            { get; set; }
    public string       Street               { get; set; }
    public string       City                 { get; set; }
    public string       CodePostal           { get; set; }
    public string       Email                { get; set; }
    public string       LogoUrl              { get; set; }
    public List<string> ScheduleOpenDays     { get; set; }
    public DateTime     ScheduleOpenTime     { get; set; }
    public DateTime     ScheduleCloseTime    { get; set; }
    public string       ScheduleNotes        { get; set; }
    public DateTime     CreatedAt            { get; set; }
    public DateTime?    UpdatedAt            { get; set; }
}