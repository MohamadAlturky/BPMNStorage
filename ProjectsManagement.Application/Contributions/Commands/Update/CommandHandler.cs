using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Update;
using ProjectsManagement.Core.Contributions;
using ProjectsManagement.Core.Projects.Repositories;

namespace ProjectsManagement.Application.ContributionMembers.Commands.Update;

public class UpdateContributionMemberCommandHandler : ICommandHandler<UpdateContributionMemberCommand, ContributionMember>
{
    private readonly IContributionMemberRepositoryPort _contributionMemberRepository;
    private readonly ILogger<UpdateContributionMemberCommandHandler> _logger;

    public UpdateContributionMemberCommandHandler(
        IContributionMemberRepositoryPort contributionMemberRepository,
        ILogger<UpdateContributionMemberCommandHandler> logger)
    {
        _contributionMemberRepository = contributionMemberRepository;
        _logger = logger;
    }

    public async Task<Result<ContributionMember>> Handle(UpdateContributionMemberCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateCommand(request);

        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Validation failed for UpdateContributionMemberCommand: {Error}", validationResult.Error);
            return Result.Failure<ContributionMember>(validationResult.Error);
        }

        try
        {
            var existingMember = await _contributionMemberRepository.GetByIdAsync(request.Id);
            if (existingMember == null)
            {
                _logger.LogWarning("Contribution member not found for update. ID: {ContributionMemberId}", request.Id);
                return Result.Failure<ContributionMember>(new Error("ContributionMember.NotFound", "The contribution member was not found."));
            }

            // Update the existing contribution member with new values
            existingMember.Project = request.Project;
            existingMember.Contributor = request.Contributor;
            existingMember.ContributionType = request.ContributionType;
            existingMember.Date = request.Date;

            await _contributionMemberRepository.UpdateAsync(existingMember);
            _logger.LogInformation("Updated contribution member with ID: {ContributionMemberId}", existingMember.Id);

            // Optionally, raise a domain event here
            // _domainEventDispatcher.Raise(new ContributionMemberUpdatedEvent(updatedMember));

            return Result.Success(existingMember);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update contribution member with ID: {ContributionMemberId}", request.Id);
            return Result.Failure<ContributionMember>(new Error("ContributionMember.UpdateFailed", "Failed to update the contribution member."));
        }
    }

    private Result ValidateCommand(UpdateContributionMemberCommand command)
    {
        if (command.Id <= 0)
        {
            return Result.Failure(new Error("ContributionMember.InvalidId", "Invalid contribution member ID."));
        }
        if (command.Project <= 0)
        {
            return Result.Failure(new Error("ContributionMember.InvalidProject", "Invalid project ID."));
        }
        if (command.Contributor <= 0)
        {
            return Result.Failure(new Error("ContributionMember.InvalidContributor", "Invalid contributor ID."));
        }
        if (command.ContributionType <= 0)
        {
            return Result.Failure(new Error("ContributionMember.InvalidContributionType", "Invalid contribution type ID."));
        }
        if (command.Date == default)
        {
            return Result.Failure(new Error("ContributionMember.InvalidDate", "The contribution date is required."));
        }

        return Result.Success();
    }
}