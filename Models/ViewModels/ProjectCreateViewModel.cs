namespace MohammedPortfolio.Models.ViewModels
{
    public class ProjectCreateViewModel
    {
        //  Project Information
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? GitHubUrl { get; set; }

        public string? LiveDemoUrl { get; set; }

        public int CategoryId { get; set; }

        public IFormFile? ImageFile { get; set; }

        //  Project Details
        public string? Overview { get; set; }

        //  Tools
        public List<string> Tools { get; set; } = new();
        public List<Category> Categories { get; set; } = new();

        //  Additional Images
        public List<IFormFile> AdditionalImages { get; set; } = new();
    }
}
