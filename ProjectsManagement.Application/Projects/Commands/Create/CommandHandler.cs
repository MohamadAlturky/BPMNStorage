using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Core.Projects.Repositories;
using ProjectsManagement.Contracts.Projects.Commands.Create;

namespace ProjectsManagement.Application.Projects.Commands.Create;

public class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand, Project>
{
    private readonly IProjectRepositoryPort _projectRepository;
    private readonly ILogger<CreateProjectCommandHandler> _logger;

    public CreateProjectCommandHandler(
        IProjectRepositoryPort projectRepository,
        ILogger<CreateProjectCommandHandler> logger)
    {
        _projectRepository = projectRepository;
        _logger = logger;
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