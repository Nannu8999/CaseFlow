using CaseFlow.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Organization> Organizations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Case> Cases { get; set; }
    public DbSet<CaseFile> CaseFiles { get; set; }
    public DbSet<CaseStatusHistory> CaseStatusHistories { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
