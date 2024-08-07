using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Delete;
using ProjectsManagement.Core.Projects.Repositories;

namespace ProjectsManagement.Application.ContributionMembers.Commands.Delete;

public class DeleteContributionMemberCommandHandler : ICommandHandler<DeleteContributionMemberCommand>
{
    private readonly IContributionMemberRepositoryPort _contributionMemberRepository;
    private readonly ILogger<DeleteContributionMemberCommandHandler> _logger;

    public DeleteContributionMemberCommandHandler(
        IContributionMemberRepositoryPort contributionMemberRepository,
        ILogger<DeleteContributionMemberCommandHandler> logger)
    {
        _contributionMemberRepository = contributionMemberRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteContributionMemberCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            _logger.LogWarning("Invalid contribution member ID: {ContributionMemberId}", request.Id);
            return Result.Failure(new Error("ContributionMember.InvalidId", "Invalid contribution member ID."));
        }

        try
        {
            var existingMember = await _contributionMemberRepository.GetByIdAsync(request.Id);
            if (existingMember == null)
            {
                _logger.LogWarning("Contribution member not found for deletion. ID: {ContributionMemberId}", request.Id);
                return Result.Failure(new Error("ContributionMember.NotFound", "The contribution member was not found."));
            }

            // You might want to add additional checks here, e.g., if the contribution member can be deleted

            await _contributionMemberRepository.DeleteAsync(request.Id);
            _logger.LogInformation("Deleted contribution member with ID: {ContributionMemberId}", request.Id);

            // Optionally, raise a domain event here
            // _domainEventDispatcher.Raise(new ContributionMemberDeletedEvent(request.Id));

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete contribution member with ID: {ContributionMemberId}", request.Id);
            return Result.Failure(new Error("ContributionMember.DeletionFailed", "Failed to delete the contribution member."));
        }
    }
}