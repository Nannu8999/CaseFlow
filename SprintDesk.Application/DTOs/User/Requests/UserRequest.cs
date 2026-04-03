using System.ComponentModel.DataAnnotations;
using CaseFlow.Domain.Enums;

namespace CaseFlow.Application.DTOs.User.Requests;

public class UserRequest
{
    [Required]
    [MaxLength(255)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public Guid OrganizationId { get; set; }

    public UserRole Role { get; set; } = UserRole.Employee;
}
