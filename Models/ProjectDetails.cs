using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MohammedPortfolio.Models
{
    public class ProjectDetails
    {
        [Key]
        public int Id { get; set; }

        [StringLength(1500, ErrorMessage = "Overview cannot exceed 1500 characters")]
        [DataType(DataType.MultilineText)]
        public string Overview { get; set; } = string.Empty;

        // Foreign Key to Project (One-to-One)
        [Required]
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        public Project? Project { get; set; }

        // One ProjectDetails → Many ProjectImages
        public ICollection<ProjectImage> Images { get; set; } = new List<ProjectImage>();

        // One ProjectDetails → Many Tools
        public ICollection<Tool> Tools { get; set; } = new List<Tool>();
    }
}
