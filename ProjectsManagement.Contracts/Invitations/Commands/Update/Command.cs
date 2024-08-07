using ProjectsManagement.Core.Activities;
using ProjectsManagement.Core.Invitations;
using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.Contracts.ProjectTasks.Commands.Update;


public class UpdateInvitationCommand : ICommand<Invitation>
{
    public int Id { get; set; }
    public int InvitationStatus { get; set; }
    public int Contributor { get; set; }
    public int Project { get; set; }

    public string Message { get; set; } = null!;
    public DateTime Date { get; set; }
}