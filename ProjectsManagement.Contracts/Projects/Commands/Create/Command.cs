using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.CQRS;

namespace ProjectsManagement.Contracts.Projects.Commands.Create;

public class CreateProjectCommand : ICommand<Project>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public int ProjectType { get; set; }
}