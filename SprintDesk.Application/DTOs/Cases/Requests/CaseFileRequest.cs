using System.ComponentModel.DataAnnotations;

namespace CaseFlow.Application.DTOs.Cases.Requests;

public class CaseFileRequest
{
    [Required]
    public Guid CaseId { get; set; }

    [Required]
    [MaxLength(255)]
    public string FileName { get; set; } = string.Empty;

    [Required]
    public string FilePath { get; set; } = string.Empty;

    public long? FileSize { get; set; }

    [MaxLength(100)]
    public string? ContentType { get; set; }

    public Guid? UploadedBy { get; set; }
}
