using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Core.Projects.Repositories;
using ProjectsManagement.Contracts.Projects.Commands.Update;

namespace ProjectsManagement.Application.Projects.Commands.Update;

public class UpdateProjectCommandHandler : ICommandHandler<UpdateProjectCommand, Project>
{
    private readonly IProjectRepositoryPort _projectRepository;
    private readonly ILogger<UpdateProjectCommandHandler> _logger;

    public UpdateProjectCommandHandler(
        IProjectRepositoryPort projectRepository,
        ILogger<UpdateProjectCommandHandler> logger)
    {
        _projectRepository = projectRepository;
        _logger = logger;
    }

    public async Task<Result<Project>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateCommand(request);

        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Validation failed for UpdateProjectCommand: {Error}", validationResult.Error);
            return Result.Failure<Project>(validationResult.Error);
        }

        try
        {
            var existingProject = await _projectRepository.GetByIdAsync(request.Id);
            if (existingProject is null)
            {
                _logger.LogWarning("Project not found for update. ID: {ProjectId}", request.Id);
                return Result.Failure<Project>(new Error("Project.NotFound", "The project was not found."));
            }

            // Update the existing project with new values
            existingProject.Name = request.Name;
            existingProject.Description = request.Description;
            existingProject.ProjectType = request.ProjectType;

            await _projectRepository.UpdateAsync(existingProject);
            _logger.LogInformation("Updated project with ID: {ProjectId}", existingProject.Id);

            // Optionally, raise a domain event here
            // _domainEventDispatcher.Raise(new ProjectUpdatedEvent(updatedProject));

            return Result.Success(existingProject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update project with ID: {ProjectId}", request.Id);
            return Result.Failure<Project>(new Error("Project.UpdateFailed", "Failed to update the project."));
        }
    }

    private Result ValidateCommand(UpdateProjectCommand command)
    {
        if (command.Id <= 0)
        {
            return Result.Failure(new Error("Project.InvalidId", "Invalid project ID."));
        }
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