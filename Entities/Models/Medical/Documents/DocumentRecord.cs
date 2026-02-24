namespace SocioWeb.Entities;

public class DocumentRecord
{
    public string DocumentName { get; set; }
    public string DocumentType { get; set; }
    public string FileUrl { get; set; }
    public string MimeType { get; set; }
    public DateTime UploadedAt { get; set; }
    public string UploadedBy { get; set; }
    public string Description { get; set; }
    public long FileSizeInBytes { get; set; }
    public bool Confidential { get; set; }
    public string Checksum { get; set; }
}