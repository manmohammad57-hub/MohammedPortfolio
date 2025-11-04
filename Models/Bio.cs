using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MohammedPortfolio.Models
{
    public class Bio
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        [Required(ErrorMessage = "NameBoi is required")]
        public string NameBoi { get; set; }=String.Empty;

        [Required(ErrorMessage = "Title is required")]
        [StringLength(1500, ErrorMessage = "Bio cannot exceed 1500 characters")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }=String.Empty;



        //-------------------------------
        // About relationship (one-to-one)
        [ForeignKey(nameof(About))]
        public int? AboutId { get; set; }
        public About? About { get; set; }

        // Skill relationship (one-to-one)
        [ForeignKey(nameof(Skill))]
        public int? SkillId { get; set; }
        public Skill? Skill { get; set; }

        // Skill relationship (one-to-one)
        [ForeignKey(nameof(Service))]
        public int? ServiceId { get; set; }
        public Service? Service  { get; set; }


    }
}
