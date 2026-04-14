using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations;

namespace CaseFlow.Application.DTOs.Client.Requests;

public class ClientRequest
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [EmailAddress]
    [MaxLength(255)]
    public string? Email { get; set; }

    [MaxLength(50)]
    public string? Phone { get; set; }

    public string? Address { get; set; }
}
