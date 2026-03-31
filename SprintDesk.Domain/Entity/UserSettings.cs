namespace CaseFlow.Domain.Entity;

public class UserSettings
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string Theme { get; set; } = "light";
    public DateTime CreatedAt { get; set; }

    // Navigation
    public User? User { get; set; }
}
