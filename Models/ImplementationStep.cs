using System.ComponentModel.DataAnnotations;

namespace MohammedPortfolio.Models
{
    public class ImplementationStep
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Step Title is required")]
        [MaxLength(200, ErrorMessage = "Step Title cannot exceed 200 characters")]
        [Display(Name = "Step Title")]
        public string StepTitle { get; set; }=String.Empty;

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = String.Empty;

        [MaxLength(50, ErrorMessage = "Duration cannot exceed 50 characters")]
        [Display(Name = "Duration (e.g., Week 1–2)")]
        public string Duration { get; set; } = String.Empty;

        [Required(ErrorMessage = "Step Number is required")]
        [Range(1, 100, ErrorMessage = "Step Number must be between 1 and 100")]
        [Display(Name = "Step Number")]
        public int StepNumber { get; set; }
    }
}
