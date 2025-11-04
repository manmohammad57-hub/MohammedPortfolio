using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MohammedPortfolio.Models;

namespace MohammedPortfolio.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Apply all IEntityTypeConfiguration<T> in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            //  Rename ASP.NET Identity tables
            modelBuilder.Entity<IdentityUser>(entity => { entity.ToTable("Users"); });
            modelBuilder.Entity<IdentityRole>(entity => { entity.ToTable("Roles"); });
            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens"); });

            //  Always call base last (so Identity configurations apply properly)
            base.OnModelCreating(modelBuilder);
        }

        //  DbSets for your entities
        public DbSet<About> About_ { get; set; } = default!;
        public DbSet<Bio> Bio_ { get; set; } = default!;
        public DbSet<Skill> Skill_ { get; set; } = default!;
        public DbSet<Profile> Profile_ { get; set; } = default!;
        public DbSet<Project> Project_ { get; set; } = default!;
        public DbSet<Category> Category_ { get; set; } = default!;
        public DbSet<Tool> Tool_ { get; set; } = default!;
        public DbSet<ProjectDetails> ProjectDetails_ { get; set; } = default!;
        public DbSet<ProjectImage> ProjectImage_ { get; set; } = default!;
        public DbSet<Service> Service_ { get; set; } = default!;
        public DbSet<ImplementationStep> ImplementationStep_ { get; set; } = default!;
        public DbSet<ContactForm> ContactForm_ { get; set; } = default!;
        public DbSet<ProjectForm> ProjectForm_ { get; set; } = default!;
    }
}
