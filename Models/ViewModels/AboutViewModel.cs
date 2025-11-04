namespace MohammedPortfolio.Models.ViewModels
{
    public class AboutViewModel
    {
        public Profile? Profile { get; set; }
        public About? About { get; set; }
        public List<Skill> Skills { get; set; }=new();
    }
}
