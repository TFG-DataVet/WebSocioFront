namespace SocioWeb.Entities.Models.ApiErrorResponse;

public class ApiErrorResponse
{
    public DateTime TimeStamp { get; set; }
    public int Status { get; set; }
    public string Error { get; set; }
    public string Message { get; set; } = "Error desconocido";
    public List<string> Details { get; set; }
    public string Path { get; set; }
}