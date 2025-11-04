using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MohammedPortfolio.Models;

namespace MohammedPortfolio.Data.Configurations
{
    public class BioConfiguration : IEntityTypeConfiguration<Bio>
    {
        public void Configure(EntityTypeBuilder<Bio> builder)
        {
            builder.ToTable("Bio_");

            // Primary Key
            builder.HasKey(b => b.Id);

            // Properties
            builder.Property(b => b.NameBoi)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(b => b.Content)
                   .IsRequired()
                   .HasMaxLength(1500);

            // Relationships (one-to-one)
            builder.HasOne(b => b.About)
                   .WithOne()
                   .HasForeignKey<Bio>(b => b.AboutId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Skill)
                   .WithOne()
                   .HasForeignKey<Bio>(b => b.SkillId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Service)
                   .WithOne()
                   .HasForeignKey<Bio>(b => b.ServiceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
