using ProjectsManagement.Core.Invitations;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Update;
using ProjectsManagement.Core.Projects.Repositories;

namespace ProjectsManagement.Application.Invitations.Commands.Update;

public class UpdateInvitationCommandHandler : ICommandHandler<UpdateInvitationCommand, Invitation>
{
    private readonly IInvitationRepositoryPort _invitationRepository;
    private readonly ILogger<UpdateInvitationCommandHandler> _logger;

    public UpdateInvitationCommandHandler(
        IInvitationRepositoryPort invitationRepository,
        ILogger<UpdateInvitationCommandHandler> logger)
    {
        _invitationRepository = invitationRepository;
        _logger = logger;
    }

    public async Task<Result<Invitation>> Handle(UpdateInvitationCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateCommand(request);

        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Validation failed for UpdateInvitationCommand: {Error}", validationResult.Error);
            return Result.Failure<Invitation>(validationResult.Error);
        }

        try
        {
            var existingInvitation = await _invitationRepository.GetByIdAsync(request.Id);
            if (existingInvitation is null)
            {
                _logger.LogWarning("Invitation not found for update. ID: {InvitationId}", request.Id);
                return Result.Failure<Invitation>(new Error("Invitation.NotFound", "The invitation was not found."));
            }

            // Update the existing invitation with new values
            existingInvitation.InvitationStatus = request.InvitationStatus;
            existingInvitation.Contributor = request.Contributor;
            existingInvitation.Project = request.Project;
            existingInvitation.Message = request.Message;
            existingInvitation.Date = request.Date;

            await _invitationRepository.UpdateAsync(existingInvitation);
            _logger.LogInformation("Updated invitation with ID: {InvitationId}", existingInvitation.Id);

            // Optionally, raise a domain event here
            // _domainEventDispatcher.Raise(new InvitationUpdatedEvent(updatedInvitation));

            return Result.Success(existingInvitation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update invitation with ID: {InvitationId}", request.Id);
            return Result.Failure<Invitation>(new Error("Invitation.UpdateFailed", "Failed to update the invitation."));
        }
    }

    private Result ValidateCommand(UpdateInvitationCommand command)
    {
        if (command.Id <= 0)
        {
            return Result.Failure(new Error("Invitation.InvalidId", "Invalid invitation ID."));
        }
        if (command.InvitationStatus <= 0)
        {
            return Result.Failure(new Error("Invitation.InvalidStatus", "Invalid invitation status ID."));
        }
        if (command.Contributor <= 0)
        {
            return Result.Failure(new Error("Invitation.InvalidContributor", "Invalid contributor ID."));
        }
        if (command.Project <= 0)
        {
            return Result.Failure(new Error("Invitation.InvalidProject", "Invalid project ID."));
        }
        if (string.IsNullOrWhiteSpace(command.Message))
        {
            return Result.Failure(new Error("Invitation.MessageRequired", "The invitation message is required."));
        }
        if (command.Date == default)
        {
            return Result.Failure(new Error("Invitation.InvalidDate", "The invitation date is required."));
        }

        return Result.Success();
    }
}