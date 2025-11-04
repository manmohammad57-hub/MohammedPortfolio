using System.ComponentModel.DataAnnotations;

namespace MohammedPortfolio.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Skill Name is required")]
        [DataType(DataType.Text)]
        [MaxLength(100, ErrorMessage = "Skill Name cannot exceed 100 characters")]
        public string SkillName { get; set; }=String.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }=String.Empty;

        [Required(ErrorMessage = "Level is required")]
        [Range(1, 100, ErrorMessage = "Level must be between 1 and 100")]
        public int Level { get; set; }


        //Navigation property (one-to-one)
        public Bio? Bio { get; set; }

    }
}
