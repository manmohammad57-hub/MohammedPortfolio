namespace MohammedPortfolio.Models.ViewModels
{
    public class ServiceViewModel
    {
        public List<Service> Services { get; set; } = new();
        public List<ImplementationStep> ImplementationSteps { get; set; } = new();
        public List<ProjectForm> ProjectForms { get; set; } = new();

        // This represents a single form, so mark it nullable unless it's always expected
        public ProjectForm? ProjectForm { get; set; }
    }
}
