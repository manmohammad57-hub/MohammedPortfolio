using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MohammedPortfolio.Models;

namespace MohammedPortfolio.Data.Configurations
{
    public class ProjectDetailsConfiguration : IEntityTypeConfiguration<ProjectDetails>
    {
        public void Configure(EntityTypeBuilder<ProjectDetails> builder)
        {
            builder.ToTable("ProjectDetails_");

            // Primary key
            builder.HasKey(pd => pd.Id);

            // One-to-one relationship: Project ↔ ProjectDetails
            builder.HasOne(pd => pd.Project)
                   .WithOne(p => p.ProjectDetails)
                   .HasForeignKey<ProjectDetails>(pd => pd.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);

            // One-to-many: ProjectDetails → ProjectImages
            builder.HasMany(pd => pd.Images)
                   .WithOne(img => img.ProjectDetails)
                   .HasForeignKey(img => img.ProjectDetailsId)
                   .OnDelete(DeleteBehavior.Cascade);

            // One-to-many: ProjectDetails → Tools
            builder.HasMany(pd => pd.Tools)
                   .WithOne(t => t.ProjectDetails)
                   .HasForeignKey(t => t.ProjectDetailsId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
