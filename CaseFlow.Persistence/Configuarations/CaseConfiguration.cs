using CaseFlow.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseFlow.Persistence.Configuarations;

public class CaseConfiguration : IEntityTypeConfiguration<Case>
{
    public void Configure(EntityTypeBuilder<Case> builder)
    {
        builder.ToTable("cases");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.OrganizationId)
            .HasColumnName("organization_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(c => c.ClientId)
            .HasColumnName("client_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(c => c.AssignedTo)
            .HasColumnName("assigned_to")
            .HasColumnType("uuid");

        builder.Property(c => c.Title)
            .HasColumnName("title")
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.Property(c => c.Description)
            .HasColumnName("description")
            .HasColumnType("text");

        builder.Property(c => c.Priority)
            .HasColumnName("priority")
            .HasColumnType("varchar(20)")
            .HasDefaultValue("Medium");

        builder.Property(c => c.Status)
            .HasColumnName("status")
            .HasColumnType("varchar(50)")
            .HasDefaultValue("Open");

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(c => c.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("chk_priority", "priority IN ('Low', 'Medium', 'High')");
            t.HasCheckConstraint("chk_status", "status IN ('Open', 'In Progress', 'Closed')");
        });

        builder.HasOne(c => c.Organization)
            .WithMany(o => o.Cases)
            .HasForeignKey(c => c.OrganizationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Client)
            .WithMany(cl => cl.Cases)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.AssignedUser)
            .WithMany(u => u.AssignedCases)
            .HasForeignKey(c => c.AssignedTo)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(c => c.ClientId).HasDatabaseName("idx_cases_client_id");
        builder.HasIndex(c => c.Status).HasDatabaseName("idx_cases_status");
        builder.HasIndex(c => c.AssignedTo).HasDatabaseName("idx_cases_assigned_to");
    }
}
