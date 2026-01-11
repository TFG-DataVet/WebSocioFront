using System.ComponentModel.DataAnnotations;

namespace SocioWeb.Domain.Entities;

public class Cita
{

    public string Id { get; set; }
    public DateTime Fecha { get; set; }
    public string? Motivo { get; set; }
}