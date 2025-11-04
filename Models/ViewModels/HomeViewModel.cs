namespace MohammedPortfolio.Models.ViewModels
{
    public class HomeViewModel
    {
        public Profile? Profile { get; set; }
        public Bio? Bio { get; set; }
        public List<Category> Categories  { get; set; } = new();


    }
}
