using ProjectsManagement.Core.Invitations;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Create;
using ProjectsManagement.Core.Projects.Repositories;

namespace ProjectsManagement.Application.Invitations.Commands.Create;

public class CreateInvitationCommandHandler : ICommandHandler<CreateInvitationCommand, Invitation>
{
    private readonly IInvitationRepositoryPort _invitationRepository;
    private readonly ILogger<CreateInvitationCommandHandler> _logger;

    public CreateInvitationCommandHandler(
        IInvitationRepositoryPort invitationRepository,
        ILogger<CreateInvitationCommandHandler> logger)
    {
        _invitationRepository = invitationRepository;
        _logger = logger;
    }

    public async Task<Result<Invitation>> Handle(CreateInvitationCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateCommand(request);

        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Validation failed for CreateInvitationCommand: {Error}", validationResult.Error);
            return Result.Failure<Invitation>(validationResult.Error);
        }

        var invitation = new Invitation
        {
            Message = request.Message,
            Date = request.Date,
            Contributor = request.Contributor,
            Project = request.Project,
            By=request.By,
            InvitationStatus = request.InvitationStatus
        };

        try
        {
            var createdInvitation = await _invitationRepository.AddAsync(invitation);
            _logger.LogInformation("Created new invitation with ID: {InvitationId}", createdInvitation.Id);

            // Optionally, raise a domain event here
            // _domainEventDispatcher.Raise(new InvitationCreatedEvent(createdInvitation));

            return Result.Success(createdInvitation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create invitation");
            return Result.Failure<Invitation>(new Error("Invitation.CreationFailed", "Failed to create the invitation."));
        }
    }

    private Result ValidateCommand(CreateInvitationCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Message))
        {
            return Result.Failure(new Error("Invitation.MessageRequired", "The invitation message is required."));
        }
        if (command.Date == default)
        {
            return Result.Failure(new Error("Invitation.InvalidDate", "The invitation date is required."));
        }
        if (command.Contributor <= 0)
        {
            return Result.Failure(new Error("Invitation.InvalidContributor", "Invalid contributor ID."));
        }
        if (command.Project <= 0)
        {
            return Result.Failure(new Error("Invitation.InvalidProject", "Invalid project ID."));
        }
        if (command.InvitationStatus <= 0)
        {
            return Result.Failure(new Error("Invitation.InvalidStatus", "Invalid invitation status ID."));
        }

        return Result.Success();
    }
}