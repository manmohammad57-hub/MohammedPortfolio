using System.ComponentModel.DataAnnotations;

namespace MohammedPortfolio.Models
{
    public class ProjectForm
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(300, ErrorMessage = "Full Name cannot exceed 300 characters")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }=String.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = String.Empty;

        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; } = String.Empty;

        [Required(ErrorMessage = "Project Type is required")]
        [StringLength(150, ErrorMessage = "Project Type cannot exceed 150 characters")]
        [Display(Name = "Project Type")]
        public string ProjectType { get; set; } = String.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = String.Empty;
    }
}
