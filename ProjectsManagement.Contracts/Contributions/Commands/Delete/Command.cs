using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.Contracts.ProjectTasks.Commands.Delete;

public class DeleteContributionMemberCommand : ICommand
{
    public int Id { get; set; }
}