namespace CaseFlow.Application.DTOs.Cases.Responses;

public class CaseStatusHistoryResponse
{
    public Guid Id { get; set; }
    public Guid CaseId { get; set; }
    public string Status { get; set; } = string.Empty;
    public Guid? ChangedBy { get; set; }
    public string? Remarks { get; set; }
    public DateTime CreatedAt { get; set; }
}
