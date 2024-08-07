using ProjectsManagement.SharedKernel.AccessControl;
using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.Contracts.Projects.Commands.Delete;

public class DeleteProjectCommand : ICommand
{
    public int Id { get; set; }
    public AccessControlCriteria Criteria()
    {
        return new();
    }
}