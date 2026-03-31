namespace CaseFlow.Domain.Entity;

public class CaseFile
{
    public Guid Id { get; set; }
    public Guid CaseId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public long? FileSize { get; set; }
    public string? ContentType { get; set; }
    public Guid? UploadedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation
    public Case Case { get; set; } = null!;
    public User? Uploader { get; set; }
}
