namespace CaseFlow.Application.DTOs.UserSettings.Responses;

public class UserSettingsResponse
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string Theme { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
