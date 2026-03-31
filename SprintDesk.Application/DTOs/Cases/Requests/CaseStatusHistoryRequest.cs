using System.ComponentModel.DataAnnotations;

namespace CaseFlow.Application.DTOs.Cases.Requests;

public class CaseStatusHistoryRequest
{
    [Required]
    public Guid CaseId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = string.Empty;

    public Guid? ChangedBy { get; set; }
    public string? Remarks { get; set; }
}
