using CaseFlow.Domain.Enums;

namespace CaseFlow.Domain.Entity;

public class CaseStatusHistory
{
    public Guid Id { get; set; }
    public Guid CaseId { get; set; }
    public CaseStatus Status { get; set; }
    public Guid? ChangedBy { get; set; }
    public string? Remarks { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation
    public Case Case { get; set; } = null!;
    public User? ChangedByUser { get; set; }
}
