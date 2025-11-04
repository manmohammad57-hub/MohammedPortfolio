using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MohammedPortfolio.Models
{
    public class ProjectImage
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Project image is required")]
        [Display(Name = "Project Image")]
        public byte[] Image { get; set; } = Array.Empty<byte>();

        // Foreign Key
        [Required]
        [ForeignKey(nameof(ProjectDetails))]
        public int ProjectDetailsId { get; set; }

        public ProjectDetails? ProjectDetails { get; set; }
    }
}
