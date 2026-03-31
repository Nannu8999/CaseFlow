using System.ComponentModel.DataAnnotations;

namespace CaseFlow.Application.DTOs.Notifications.Requests;

public class NotificationRequest
{
    [Required]
    public Guid UserId { get; set; }

    [MaxLength(255)]
    public string? Title { get; set; }

    public string? Message { get; set; }
}
