using System.ComponentModel.DataAnnotations;

namespace CaseFlow.Application.DTOs.UserSettings.Requests;

public class UserSettingsRequest
{
    [Required]
    public Guid UserId { get; set; }

    [RegularExpression("light|dark")]
    public string Theme { get; set; } = "light";
}
