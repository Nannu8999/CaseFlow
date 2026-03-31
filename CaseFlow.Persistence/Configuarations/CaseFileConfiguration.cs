using CaseFlow.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaseFlow.Persistence.Configuarations;

public class CaseFileConfiguration : IEntityTypeConfiguration<CaseFile>
{
    public void Configure(EntityTypeBuilder<CaseFile> builder)
    {
        builder.ToTable("case_files");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(f => f.CaseId)
            .HasColumnName("case_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(f => f.FileName)
            .HasColumnName("file_name")
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.Property(f => f.FilePath)
            .HasColumnName("file_path")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(f => f.FileSize)
            .HasColumnName("file_size")
            .HasColumnType("bigint");

        builder.Property(f => f.ContentType)
            .HasColumnName("content_type")
            .HasColumnType("varchar(100)");

        builder.Property(f => f.UploadedBy)
            .HasColumnName("uploaded_by")
            .HasColumnType("uuid");

        builder.Property(f => f.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(f => f.Case)
            .WithMany(c => c.Files)
            .HasForeignKey(f => f.CaseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(f => f.Uploader)
            .WithMany()
            .HasForeignKey(f => f.UploadedBy)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(f => f.CaseId).HasDatabaseName("idx_files_case_id");
    }
}
