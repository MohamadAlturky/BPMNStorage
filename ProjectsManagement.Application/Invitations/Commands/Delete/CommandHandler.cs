using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Delete;
using ProjectsManagement.Core.Projects.Repositories;

namespace ProjectsManagement.Application.Invitations.Commands.Delete;

public class DeleteInvitationCommandHandler : ICommandHandler<DeleteInvitationCommand>
{
    private readonly IInvitationRepositoryPort _invitationRepository;
    private readonly ILogger<DeleteInvitationCommandHandler> _logger;

    public DeleteInvitationCommandHandler(
        IInvitationRepositoryPort invitationRepository,
        ILogger<DeleteInvitationCommandHandler> logger)
    {
        _invitationRepository = invitationRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteInvitationCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            _logger.LogWarning("Invalid invitation ID: {InvitationId}", request.Id);
            return Result.Failure(new Error("Invitation.InvalidId", "Invalid invitation ID."));
        }

        try
        {
            var existingInvitation = await _invitationRepository.GetByIdAsync(request.Id);
            if (existingInvitation == null)
            {
                _logger.LogWarning("Invitation not found for deletion. ID: {InvitationId}", request.Id);
                return Result.Failure(new Error("Invitation.NotFound", "The invitation was not found."));
            }

            // You might want to add additional checks here, e.g., if the invitation can be deleted

            await _invitationRepository.DeleteAsync(request.Id);
            _logger.LogInformation("Deleted invitation with ID: {InvitationId}", request.Id);

            // Optionally, raise a domain event here
            // _domainEventDispatcher.Raise(new InvitationDeletedEvent(request.Id));

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete invitation with ID: {InvitationId}", request.Id);
            return Result.Failure(new Error("Invitation.DeletionFailed", "Failed to delete the invitation."));
        }
    }
}