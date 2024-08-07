using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Core.Projects.Repositories;
using ProjectsManagement.Contracts.Projects.Commands.Delete;

namespace ProjectsManagement.Application.Projects.Commands.Delete;

public class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand>
{
    private readonly IProjectRepositoryPort _projectRepository;
    private readonly ILogger<DeleteProjectCommandHandler> _logger;

    public DeleteProjectCommandHandler(
        IProjectRepositoryPort projectRepository,
        ILogger<DeleteProjectCommandHandler> logger)
    {
        _projectRepository = projectRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            _logger.LogWarning("Invalid project ID: {ProjectId}", request.Id);
            return Result.Failure(new Error("Project.InvalidId", "Invalid project ID."));
        }

        try
        {
            var existingProject = await _projectRepository.GetByIdAsync(request.Id);
            if (existingProject is null)
            {
                _logger.LogWarning("Project not found for deletion. ID: {ProjectId}", request.Id);
                return Result.Failure(new Error("Project.NotFound", "The project was not found."));
            }


            await _projectRepository.DeleteAsync(request.Id);
            _logger.LogInformation("Deleted project with ID: {ProjectId}", request.Id);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete project with ID: {ProjectId}", request.Id);
            return Result.Failure(new Error("Project.DeletionFailed", "Failed to delete the project."));
        }
    }
}