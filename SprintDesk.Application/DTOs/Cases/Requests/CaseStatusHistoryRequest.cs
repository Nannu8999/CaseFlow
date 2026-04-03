using System.ComponentModel.DataAnnotations;
using CaseFlow.Domain.Enums;

namespace CaseFlow.Application.DTOs.Cases.Requests;

public class CaseStatusHistoryRequest
{
    [Required]
    public Guid CaseId { get; set; }

    [Required]
    public CaseStatus Status { get; set; }

    public Guid? ChangedBy { get; set; }
    public string? Remarks { get; set; }
}
