using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.Contributions.Queries.Filter;
using ProjectsManagement.Core.Contributions;
using ProjectsManagement.Core.Projects.Repositories;

namespace ProjectsManagement.Application.ContributionMembers.Queries;

public class FilterContributionMemberQueryHandler : IQueryHandler<FilterContributionMemberQuery, PaginatedResponse<ContributionMember>>
{
    private readonly IContributionMemberRepositoryPort _contributionMemberRepository;
    private readonly ILogger<FilterContributionMemberQueryHandler> _logger;

    public FilterContributionMemberQueryHandler(
        IContributionMemberRepositoryPort contributionMemberRepository,
        ILogger<FilterContributionMemberQueryHandler> logger)
    {
        _contributionMemberRepository = contributionMemberRepository;
        _logger = logger;
    }

    public async Task<Result<PaginatedResponse<ContributionMember>>> Handle(FilterContributionMemberQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Filter == null)
            {
                _logger.LogWarning("Filter action is null in FilterContributionMemberQuery");
                return Result.Failure<PaginatedResponse<ContributionMember>>(new Error("ContributionMember.InvalidFilter", "The filter action cannot be null."));
            }

            var paginatedResponse = await _contributionMemberRepository.Filter(request.Filter);

            _logger.LogInformation("Successfully filtered contribution members. Total items: {TotalItems}", paginatedResponse.TotalCount);

            return Result.Success(paginatedResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while filtering contribution members");
            return Result.Failure<PaginatedResponse<ContributionMember>>(new Error("ContributionMember.FilterFailed", "Failed to filter contribution members."));
        }
    }
}