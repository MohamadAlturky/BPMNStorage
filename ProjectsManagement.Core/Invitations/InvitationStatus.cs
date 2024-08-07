using ProjectsManagement.Core.Invitations;
using ProjectsManagement.SharedKernel.Contracts.Entities;

namespace ProjectsManagement.Storage.Adapters.Model;

public class InvitationStatus : EntityBase
{
    public string Name { get; set; } = null!;

    public ICollection<Invitation> Invitations { get; set; }
}
