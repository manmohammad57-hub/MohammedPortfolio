using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MohammedPortfolio.Models;

namespace MohammedPortfolio.Data.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Service_");

            // Primary key
            builder.HasKey(a => a.Id);

            // Relationship: One-to-one with Bio
            builder.HasOne(a => a.Bio)
                   .WithOne(b => b.Service)
                   .HasForeignKey<Bio>(b => b.ServiceId);
        }
    }
}
