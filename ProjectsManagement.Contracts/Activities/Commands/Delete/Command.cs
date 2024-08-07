using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.Contracts.Activities.Commands.Delete;

public class DeleteActivityCommand : ICommand
{
    public int Id { get; set; }
}