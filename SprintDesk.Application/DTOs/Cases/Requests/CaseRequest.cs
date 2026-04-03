using System.ComponentModel.DataAnnotations;
using CaseFlow.Domain.Enums;

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

    public CasePriority Priority { get; set; } = CasePriority.Medium;

    public CaseStatus Status { get; set; } = CaseStatus.Open;
}
