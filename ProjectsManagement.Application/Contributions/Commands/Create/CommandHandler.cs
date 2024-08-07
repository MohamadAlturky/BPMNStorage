using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.Contributions.Commands.Create;
using ProjectsManagement.Core.Contributions;
using ProjectsManagement.Core.Projects.Repositories;

namespace ProjectsManagement.Application.ContributionMembers.Commands.Create;

public class CreateContributionMemberCommandHandler : ICommandHandler<CreateContributionMemberCommand, ContributionMember>
{
    private readonly IContributionMemberRepositoryPort _contributionMemberRepository;
    private readonly ILogger<CreateContributionMemberCommandHandler> _logger;

    public CreateContributionMemberCommandHandler(
        IContributionMemberRepositoryPort contributionMemberRepository,
        ILogger<CreateContributionMemberCommandHandler> logger)
    {
        _contributionMemberRepository = contributionMemberRepository;
        _logger = logger;
    }

    public async Task<Result<ContributionMember>> Handle(CreateContributionMemberCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateCommand(request);

        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Validation failed for CreateContributionMemberCommand: {Error}", validationResult.Error);
            return Result.Failure<ContributionMember>(validationResult.Error);
        }

        var contributionMember = new ContributionMember
        {
            Project = request.Project,
            Contributor = request.Contributor,
            ContributionType = request.ContributionType,
            Date = request.Date
        };

        try
        {
            var createdMember = await _contributionMemberRepository.AddAsync(contributionMember);
            _logger.LogInformation("Created new contribution member with ID: {ContributionMemberId}", createdMember.Id);

            return Result.Success(createdMember);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create contribution member");
            return Result.Failure<ContributionMember>(new Error("ContributionMember.CreationFailed", "Failed to create the contribution member."));
        }
    }

    private Result ValidateCommand(CreateContributionMemberCommand command)
    {
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