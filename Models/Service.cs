using System.ComponentModel.DataAnnotations;

namespace MohammedPortfolio.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }=String.Empty;

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(1500, ErrorMessage = "Description cannot exceed 1500 characters")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }=String.Empty;

        [Display(Name = "Icon URL")]
        public string? IconUrl { get; set; }

        //Navigation property (one-to-one)
        public Bio? Bio { get; set; }
    }
}
