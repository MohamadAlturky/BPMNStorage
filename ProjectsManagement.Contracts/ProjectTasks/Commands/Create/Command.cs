using ProjectsManagement.Core.Activities;
using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.AccessControl;
using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.Contracts.ProjectTasks.Commands.Create;

public class CreateProjectTaskCommand : ICommand<ProjectTask>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;


    public int Project { get; set; }
    public int TaskStatus { get; set; }
    public AccessControlCriteria Criteria()
    {
        return new();
    }
}