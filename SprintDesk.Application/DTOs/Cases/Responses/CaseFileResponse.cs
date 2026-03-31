namespace CaseFlow.Application.DTOs.Cases.Responses;

public class CaseFileResponse
{
    public Guid Id { get; set; }
    public Guid CaseId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public long? FileSize { get; set; }
    public string? ContentType { get; set; }
    public Guid? UploadedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
