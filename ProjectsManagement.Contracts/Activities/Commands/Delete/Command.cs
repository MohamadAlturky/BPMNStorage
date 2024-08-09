using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.AccessControl;
using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.Contracts.Activities.Commands.Delete;

public class DeleteActivityCommand : ICommand
{
    public int Id { get; set; }
    public AccessControlCriteria Criteria()
    {
        return new()
        {
            //Activity = Id
        };
    }
}