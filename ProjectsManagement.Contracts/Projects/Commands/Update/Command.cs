using ProjectsManagement.Core.Activities;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.Contracts.Projects.Commands.Update;


public class UpdateProjectCommand : ICommand<Project>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ProjectType { get; set; }
}