using ProjectsManagement.Core.Invitations;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.ProjectTasks.Queries.GetById;
using ProjectsManagement.Core.Projects.Repositories;

namespace ProjectsManagement.Application.Invitations.Queries;

public class GetInvitationByIdQueryHandler : IQueryHandler<GetInvitationByIdQuery, Invitation>
{
    private readonly IInvitationRepositoryPort _invitationRepository;
    private readonly ILogger<GetInvitationByIdQueryHandler> _logger;

    public GetInvitationByIdQueryHandler(
        IInvitationRepositoryPort invitationRepository,
        ILogger<GetInvitationByIdQueryHandler> logger)
    {
        _invitationRepository = invitationRepository;
        _logger = logger;
    }

    public async Task<Result<Invitation>> Handle(GetInvitationByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            _logger.LogWarning("Invalid invitation ID: {InvitationId}", request.Id);
            return Result.Failure<Invitation>(new Error("Invitation.InvalidId", "Invalid invitation ID."));
        }

        try
        {
            var invitation = await _invitationRepository.GetByIdAsync(request.Id);

            if (invitation == null)
            {
                _logger.LogWarning("Invitation not found. ID: {InvitationId}", request.Id);
                return Result.Failure<Invitation>(new Error("Invitation.NotFound", "The invitation was not found."));
            }

            _logger.LogInformation("Successfully retrieved invitation. ID: {InvitationId}", request.Id);
            return Result.Success(invitation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving invitation. ID: {InvitationId}", request.Id);
            return Result.Failure<Invitation>(new Error("Invitation.RetrievalFailed", "Failed to retrieve the invitation."));
        }
    }
}