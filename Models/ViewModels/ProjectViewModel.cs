namespace MohammedPortfolio.Models.ViewModels
{
    public class ProjectViewModel
    {
        public List<Project> Projects { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<ProjectDetails> ProjectDetails { get; set; } = new();
        public List<ProjectImage> ProjectImages { get; set; } = new();
        public List<Tool> Tools { get; set; } = new();
    }
}
