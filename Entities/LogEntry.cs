namespace SocioWeb.Domain.Entities;

public class LogEntry
{
    public DateTime Fecha { get; set; }             // Momento en que ocurrió la acción
    public string Descripcion { get; set; } = "";   // Descripción breve de la acción
    public string? Usuario { get; set; }            // Opcional: quién realizó la acción
    public string? Tipo { get; set; }               // Opcional: tipo de acción, ej. "Creación", "Actualización"
}
