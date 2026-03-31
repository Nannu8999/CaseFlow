namespace CaseFlow.Application.DTOs.Cases.Responses;

public class CaseResponse
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public Guid ClientId { get; set; }
    public Guid? AssignedTo { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Priority { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
