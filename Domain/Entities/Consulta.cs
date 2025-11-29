using System.ComponentModel.DataAnnotations;

namespace SocioWeb.Domain.Entities;

public class Consulta
{
    [Key]
    public long IdConsulta { get; set; }
    public DateTime Fecha { get; set; }
    public string? Motivo { get; set; }
    public string? Notas { get; set; }
}