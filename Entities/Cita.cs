using System.ComponentModel.DataAnnotations;

namespace SocioWeb.Domain.Entities;

public class Cita
{

    public long IdCita { get; set; }
    public DateTime Fecha { get; set; }
    public string? Motivo { get; set; }
}