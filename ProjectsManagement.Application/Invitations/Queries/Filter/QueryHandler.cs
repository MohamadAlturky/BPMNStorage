using ProjectsManagement.Core.Invitations;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.Activities.Queries.Filter;
using ProjectsManagement.Core.Projects.Repositories;

namespace ProjectsManagement.Application.Invitations.Queries;

public class FilterInvitationQueryHandler : IQueryHandler<FilterInvitationQuery, PaginatedResponse<Invitation>>
{
    private readonly IInvitationRepositoryPort _invitationRepository;
    private readonly ILogger<FilterInvitationQueryHandler> _logger;

    public FilterInvitationQueryHandler(
        IInvitationRepositoryPort invitationRepository,
        ILogger<FilterInvitationQueryHandler> logger)
    {
        _invitationRepository = invitationRepository;
        _logger = logger;
    }

    public async Task<Result<PaginatedResponse<Invitation>>> Handle(FilterInvitationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Filter == null)
            {
                _logger.LogWarning("Filter action is null in FilterInvitationQuery");
                return Result.Failure<PaginatedResponse<Invitation>>(new Error("Invitation.InvalidFilter", "The filter action cannot be null."));
            }

            var paginatedResponse = await _invitationRepository.Filter(request.Filter);

            _logger.LogInformation("Successfully filtered invitations. Total items: {TotalItems}", paginatedResponse.TotalCount);

            return Result.Success(paginatedResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while filtering invitations");
            return Result.Failure<PaginatedResponse<Invitation>>(new Error("Invitation.FilterFailed", "Failed to filter invitations."));
        }
    }
}