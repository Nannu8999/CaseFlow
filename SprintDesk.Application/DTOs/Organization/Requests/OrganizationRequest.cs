using System.ComponentModel.DataAnnotations;

namespace CaseFlow.Application.DTOs.Organization.Requests;

public class OrganizationRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
}
