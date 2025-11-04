using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MohammedPortfolio.Models;

namespace MohammedPortfolio.Data.Configurations
{
    public class ToolConfiguration : IEntityTypeConfiguration<Tool>
    {
        public void Configure(EntityTypeBuilder<Tool> builder)
        {
            builder.ToTable("Tool_");

            builder.HasKey(t => t.Id);


            builder.HasOne(t => t.ProjectDetails)
                   .WithMany(pd => pd.Tools)
                   .HasForeignKey(t => t.ProjectDetailsId)
                   .OnDelete(DeleteBehavior.Cascade); // Delete tools when ProjectDetails is deleted
        }
    }
}
