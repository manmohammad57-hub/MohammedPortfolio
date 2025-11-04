using System.ComponentModel.DataAnnotations;

namespace MohammedPortfolio.Models
{
    public class ContactForm
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(300, ErrorMessage = "Full Name cannot exceed 300 characters")]
        public string FullName { get; set; }=String.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Subject is required")]
        [StringLength(200, ErrorMessage = "Subject cannot exceed 200 characters")]
        public string Subject { get; set; } = String.Empty;

        [Required(ErrorMessage = "Message is required")]
        [StringLength(2000, ErrorMessage = "Message cannot exceed 2000 characters")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; } = String.Empty;
    }
}
