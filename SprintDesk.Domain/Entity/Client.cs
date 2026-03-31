namespace CaseFlow.Domain.Entity;

public class Client
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation
    public Organization Organization { get; set; } = null!;
    public ICollection<Case> Cases { get; set; } = new List<Case>();
}
