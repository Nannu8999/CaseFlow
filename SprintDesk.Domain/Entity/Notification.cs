namespace CaseFlow.Domain.Entity;

public class Notification
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; }

    // Navigation
    public User User { get; set; } = null!;
}
