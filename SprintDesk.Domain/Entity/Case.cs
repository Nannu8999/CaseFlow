using CaseFlow.Domain.Enums;

namespace CaseFlow.Domain.Entity;

public class Case
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public Guid ClientId { get; set; }
    public Guid? AssignedTo { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public CasePriority Priority { get; set; } = CasePriority.Medium;
    public CaseStatus Status { get; set; } = CaseStatus.Open;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation
    public Organization Organization { get; set; } = null!;
    public Client Client { get; set; } = null!;
    public User? AssignedUser { get; set; }
    public ICollection<CaseFile> Files { get; set; } = new List<CaseFile>();
    public ICollection<CaseStatusHistory> StatusHistory { get; set; } = new List<CaseStatusHistory>();
}
