using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Core.Projects.Repositories;
using ProjectsManagement.Contracts.Projects.Commands.Create;
using ProjectsManagement.Application.Users;
using ProjectsManagement.Core.Contributions;
using ProjectsManagement.Core.Constants;

namespace ProjectsManagement.Application.Projects.Commands.Create;

public class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand, Project>
{
    private readonly IProjectRepositoryPort _projectRepository;
    private readonly IUserIdentityPort _identityPort;
    private readonly IContributionMemberRepositoryPort _memberRepositoryPort;
    private readonly ILogger<CreateProjectCommandHandler> _logger;

    public CreateProjectCommandHandler(IProjectRepositoryPort projectRepository, IUserIdentityPort identityPort, 
        IContributionMemberRepositoryPort memberRepositoryPort, ILogger<CreateProjectCommandHandler> logger)
    {
        _projectRepository=projectRepository;
        _identityPort=identityPort;
        _memberRepositoryPort=memberRepositoryPort;
        _logger=logger;
    }

    public async Task<Result<Project>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateCommand(request);

        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Validation failed for CreateProjectCommand: {Error}", validationResult.Error);
            return Result.Failure<Project>(validationResult.Error);
        }

        var project = new Project
        {
            Name = request.Name,
            Description = request.Description,
            ProjectType = request.ProjectType
        };

        try
        {
            var createdProject = await _projectRepository.AddAsync(project);
            ContributionMember member = new() 
            {
                ContributionType = ConstantsProvider.OWNER.Id,
                Date = DateTime.UtcNow,
                Contributor = await _identityPort.GetUserIdAsync(),
                Project = createdProject.Id
            };
            await _memberRepositoryPort.AddAsync(member);
            _logger.LogInformation("Created new project with ID: {ProjectId}", createdProject.Id);


            return Result.Success(createdProject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create project");
            return Result.Failure<Project>(new Error("Project.CreationFailed", "Failed to create the project."));
        }
    }

    private Result ValidateCommand(CreateProjectCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result.Failure(new Error("Project.NameRequired", "The project name is required."));
        }
        if (string.IsNullOrWhiteSpace(command.Description))
        {
            return Result.Failure(new Error("Project.DescriptionRequired", "The project description is required."));
        }
        if (command.ProjectType <= 0)
        {
            return Result.Failure(new Error("Project.InvalidProjectType", "Invalid project type ID."));
        }

        return Result.Success();
    }
}