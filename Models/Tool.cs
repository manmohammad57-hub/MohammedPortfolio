using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MohammedPortfolio.Models
{
    public class Tool
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tool Name is required")]
        [StringLength(150, ErrorMessage = "Tool Name cannot exceed 150 characters")]
        public string ToolName { get; set; } = string.Empty;

        // Foreign key to ProjectDetails
        [Required]
        [ForeignKey(nameof(ProjectDetails))]
        public int ProjectDetailsId { get; set; }

        public ProjectDetails? ProjectDetails { get; set; }
    }
}
