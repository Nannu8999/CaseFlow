using CaseFlow.Domain.Enums;

namespace CaseFlow.Application.DTOs.Cases.Responses;

public class CaseStatusHistoryResponse
{
    public Guid Id { get; set; }
    public Guid CaseId { get; set; }
    public CaseStatus Status { get; set; }
    public Guid? ChangedBy { get; set; }
    public string? Remarks { get; set; }
    public DateTime CreatedAt { get; set; }
}
