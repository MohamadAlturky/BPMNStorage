using ProjectsManagement.Core.Activities;
using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.AccessControl;
using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.Contracts.ProjectTasks.Commands.Update;


public class UpdateProjectTaskCommand : ICommand<ProjectTask>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Project { get; set; }
    public int TaskStatus { get; set; }
    public AccessControlCriteria Criteria()
    {
        return new();
    }
}