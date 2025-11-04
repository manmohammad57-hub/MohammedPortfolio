using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MohammedPortfolio.Models;

namespace MohammedPortfolio.Data.Configurations
{
    public class AboutConfiguration : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> builder)
        {
            // Table name (optional, EF Core can infer it)
            builder.ToTable("About_");

            // Primary key
            builder.HasKey(a => a.Id);

            // Relationship: One-to-one with Bio
            builder.HasOne(a => a.Bio)
                   .WithOne(b => b.About)
                   .HasForeignKey<Bio>(b => b.AboutId);
        }
        
    }
}
