using ProjectsManagement.Core.Invitations;
using ProjectsManagement.SharedKernel.Ports.Storage;

namespace ProjectsManagement.Core.Projects.Repositories;

public interface IInvitationRepositoryPort
    : IBaseRepositoryPort<Invitation, InvitationFilter>
{ }