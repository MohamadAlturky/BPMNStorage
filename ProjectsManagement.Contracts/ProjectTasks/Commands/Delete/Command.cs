using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.Contracts.ProjectTasks.Commands.Delete;

public class DeleteProjectTaskCommand : ICommand
{
    public int Id { get; set; }
}