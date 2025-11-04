using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MohammedPortfolio.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Project Title is required")]
        [StringLength(150, ErrorMessage = "Title cannot exceed 150 characters")]
        [Display(Name = "Project Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;

        [Url(ErrorMessage = "Invalid GitHub URL")]
        [Display(Name = "GitHub Repository")]
        public string? GitHubUrl { get; set; }

        [Url(ErrorMessage = "Invalid Live Demo URL")]
        [Display(Name = "Live Demo URL")]
        public string? LiveDemoUrl { get; set; }

        [Display(Name = "Project Image")]
        public byte[]? Image { get; set; }

        // Foreign Key relationship with Category
        [Required(ErrorMessage = "Project Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        // One-to-One relationship with ProjectDetails
        public ProjectDetails? ProjectDetails { get; set; }
    }
}
