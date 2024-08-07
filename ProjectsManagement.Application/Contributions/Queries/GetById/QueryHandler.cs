using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.ProjectTasks.Queries.GetById;
using ProjectsManagement.Core.Contributions;
using ProjectsManagement.Core.Projects.Repositories;

namespace ProjectsManagement.Application.ContributionMembers.Queries;

public class GetContributionMemberByIdQueryHandler : IQueryHandler<GetContributionMemberByIdQuery, ContributionMember>
{
    private readonly IContributionMemberRepositoryPort _contributionMemberRepository;
    private readonly ILogger<GetContributionMemberByIdQueryHandler> _logger;

    public GetContributionMemberByIdQueryHandler(
        IContributionMemberRepositoryPort contributionMemberRepository,
        ILogger<GetContributionMemberByIdQueryHandler> logger)
    {
        _contributionMemberRepository = contributionMemberRepository;
        _logger = logger;
    }

    public async Task<Result<ContributionMember>> Handle(GetContributionMemberByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            _logger.LogWarning("Invalid contribution member ID: {ContributionMemberId}", request.Id);
            return Result.Failure<ContributionMember>(new Error("ContributionMember.InvalidId", "Invalid contribution member ID."));
        }

        try
        {
            var contributionMember = await _contributionMemberRepository.GetByIdAsync(request.Id);

            if (contributionMember == null)
            {
                _logger.LogWarning("Contribution member not found. ID: {ContributionMemberId}", request.Id);
                return Result.Failure<ContributionMember>(new Error("ContributionMember.NotFound", "The contribution member was not found."));
            }

            _logger.LogInformation("Successfully retrieved contribution member. ID: {ContributionMemberId}", request.Id);
            return Result.Success(contributionMember);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving contribution member. ID: {ContributionMemberId}", request.Id);
            return Result.Failure<ContributionMember>(new Error("ContributionMember.RetrievalFailed", "Failed to retrieve the contribution member."));
        }
    }
}