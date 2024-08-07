using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Contracts.Entities;
using ProjectsManagement.Storage.Adapters.Model;

namespace ProjectsManagement.Core.Invitations;

public class Invitation : AccessControlledEntity
{
    public string Message { get; set; } = null!;
    public DateTime Date { get; set; }

    public int Contributor { get; set; }
    public int Project { get; set; }
    public int InvitationStatus { get; set; }


    public InvitationStatus InvitationStatusNavigation { get; set; } = null!;
    public Project ProjectNavigation { get; set; } = null!;
}
