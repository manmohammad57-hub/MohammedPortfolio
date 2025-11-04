using System.ComponentModel.DataAnnotations;

namespace MohammedPortfolio.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Tagline cannot exceed 100 characters")]
        public string Tagline { get; set; }=String.Empty;

        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.MultilineText)]
        [StringLength(1500, ErrorMessage = "Description cannot exceed 1500 characters")]
        public string Description { get; set; } = String.Empty;

        [Display(Name = "Resume PDF")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "pdf", ErrorMessage = "Please upload a valid PDF file")]
        public byte[]? ResumePdf { get; set; }

        public byte[]? Aboutimage { get; set; }

        //Navigation property (one-to-one)
        public Bio? Bio { get; set; }


    }
}
