using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MohammedPortfolio.Models;

namespace MohammedPortfolio.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project_");

            // Primary key
            builder.HasKey(p => p.Id);

            // Property configurations
            builder.Property(p => p.Title)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(p => p.Description)
                   .IsRequired()
                   .HasMaxLength(1000);

            // Relationship: One-to-one (Project ↔ ProjectDetails)
            builder.HasOne(p => p.ProjectDetails)
                   .WithOne(pd => pd.Project)
                   .HasForeignKey<ProjectDetails>(pd => pd.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade); 

            // Relationship: Many-to-one (Project ↔ Category)
            builder.HasOne(p => p.Category)
                   .WithMany()
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
