namespace SocioWeb.Domain.Entities;

public class Producto
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string Categoria { get; set; }
    public string Marca { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    
    public bool Activo { get; set; }
    public string Descripcion { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<LogEntry> Historial { get; set; }
}
