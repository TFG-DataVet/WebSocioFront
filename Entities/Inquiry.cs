using System.ComponentModel.DataAnnotations;

namespace SocioWeb.Domain.Entities;

public class Inquiry
{
    
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
}