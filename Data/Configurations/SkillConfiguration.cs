using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MohammedPortfolio.Models;

namespace MohammedPortfolio.Data.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable("Skill_");

            // Primary key
            builder.HasKey(a => a.Id);

            // Relationship: One-to-one with Bio
            builder.HasOne(a => a.Bio)
                   .WithOne(b => b.Skill)
                   .HasForeignKey<Bio>(b => b.SkillId);
        }
    }
}
