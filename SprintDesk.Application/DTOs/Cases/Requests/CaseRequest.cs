using System.ComponentModel.DataAnnotations;

namespace CaseFlow.Application.DTOs.Cases.Requests;

public class CaseRequest
{
    [Required]
    public Guid OrganizationId { get; set; }

    [Required]
    public Guid ClientId { get; set; }

    public Guid? AssignedTo { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [RegularExpression("Low|Medium|High")]
    public string Priority { get; set; } = "Medium";

    [RegularExpression("Open|In Progress|Closed")]
    public string Status { get; set; } = "Open";
}
