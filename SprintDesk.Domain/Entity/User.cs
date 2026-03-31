namespace CaseFlow.Domain.Entity;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; } = null!;
    public string Role { get; set; } = "Employee";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation
    public ICollection<Case> AssignedCases { get; set; } = new List<Case>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public UserSettings? Settings { get; set; }
}
