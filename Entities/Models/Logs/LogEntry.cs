namespace SocioWeb.Domain.Entities;

public class LogEntry
{
    public DateTime Date { get; set; }             // Momento en que ocurrió la acción
    public string Description { get; set; } = "";   // Descripción breve de la acción
    public string? User { get; set; }            // Opcional: quién realizó la acción
    public string? Type { get; set; }               // Opcional: tipo de acción, ej. "Creación", "Actualización"
}
