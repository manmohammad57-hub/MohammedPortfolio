using System.ComponentModel.DataAnnotations;

namespace MohammedPortfolio.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Type is required")]
        [MaxLength(100, ErrorMessage = "Category Type cannot exceed 100 characters")]
        [Display(Name = "Category Type")]
        public string CategoryName  { get; set; } = String.Empty;

        // Relationship: One category → Many projects
        public ICollection<Project>? Projects { get; set; }
    }
}
