using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Delete;
using ProjectsManagement.Core.Tasks.Storage;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;

namespace ProjectsManagement.Application.ProjectTasks.Commands.Delete;

public class DeleteProjectTaskCommandHandler : ICommandHandler<DeleteProjectTaskCommand>
{
    private readonly IProjectTaskRepositoryPort _projectTaskRepository;
    private readonly ILogger<DeleteProjectTaskCommandHandler> _logger;

    public DeleteProjectTaskCommandHandler(
        IProjectTaskRepositoryPort projectTaskRepository,
        ILogger<DeleteProjectTaskCommandHandler> logger)
    {
        _projectTaskRepository = projectTaskRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            _logger.LogWarning("Invalid project task ID: {TaskId}", request.Id);
            return Result.Failure(new Error("ProjectTask.InvalidId", "Invalid project task ID."));
        }

        try
        {
            var existingTask = await _projectTaskRepository.GetByIdAsync(request.Id);
            if (existingTask == null)
            {
                _logger.LogWarning("Project task not found for deletion. ID: {TaskId}", request.Id);
                return Result.Failure(new Error("ProjectTask.NotFound", "The project task was not found."));
            }

            await _projectTaskRepository.DeleteAsync(request.Id);
            _logger.LogInformation("Deleted project task with ID: {TaskId}", request.Id);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete project task with ID: {TaskId}", request.Id);
            return Result.Failure(new Error("ProjectTask.DeletionFailed", "Failed to delete the project task."));
        }
    }
}