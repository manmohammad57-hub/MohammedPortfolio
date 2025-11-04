using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MohammedPortfolio.Models;

namespace MohammedPortfolio.Data.Configurations
{
    public class ProjectImageConfiguration : IEntityTypeConfiguration<ProjectImage>
    {
        public void Configure(EntityTypeBuilder<ProjectImage> builder)
        {
            builder.ToTable("ProjectImage_");

            builder.HasKey(pi => pi.Id);


            builder.HasOne(pi => pi.ProjectDetails)
                   .WithMany(pd => pd.Images)
                   .HasForeignKey(pi => pi.ProjectDetailsId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
