using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.Results;
using MediatR;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Update;
using ProjectsManagement.Core.Tasks.Storage;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;

namespace ProjectsManagement.Application.ProjectTasks.Commands.Update;

public class UpdateProjectTaskCommandHandler : ICommandHandler<UpdateProjectTaskCommand, ProjectTask>
{
    private readonly IProjectTaskRepositoryPort _projectTaskRepository;
    private readonly ILogger<UpdateProjectTaskCommandHandler> _logger;

    public UpdateProjectTaskCommandHandler(
        IProjectTaskRepositoryPort projectTaskRepository,
        ILogger<UpdateProjectTaskCommandHandler> logger)
    {
        _projectTaskRepository = projectTaskRepository;
        _logger = logger;
    }

    public async Task<Result<ProjectTask>> Handle(UpdateProjectTaskCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateCommand(request);

        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Validation failed for UpdateProjectTaskCommand: {Error}", validationResult.Error);
            return Result.Failure<ProjectTask>(validationResult.Error);
        }

        try
        {
            var existingTask = await _projectTaskRepository.GetByIdAsync(request.Id);
            if (existingTask is null)
            {
                _logger.LogWarning("Project task not found for update. ID: {TaskId}", request.Id);
                return Result.Failure<ProjectTask>(new Error("ProjectTask.NotFound", "The project task was not found."));
            }

            existingTask.Name = request.Name;
            existingTask.Description = request.Description;
            existingTask.Project = request.Project;
            existingTask.TaskStatus = request.TaskStatus;

            await _projectTaskRepository.UpdateAsync(existingTask);
            _logger.LogInformation("Updated project task with ID: {TaskId}", existingTask.Id);


            return Result.Success(existingTask);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update project task with ID: {TaskId}", request.Id);
            return Result.Failure<ProjectTask>(new Error("ProjectTask.UpdateFailed", "Failed to update the project task."));
        }
    }

    private Result ValidateCommand(UpdateProjectTaskCommand command)
    {
        if (command.Id <= 0)
        {
            return Result.Failure(new Error("ProjectTask.InvalidId", "Invalid project task ID."));
        }
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result.Failure(new Error("ProjectTask.NameRequired", "The project task name is required."));
        }
        if (string.IsNullOrWhiteSpace(command.Description))
        {
            return Result.Failure(new Error("ProjectTask.DescriptionRequired", "The project task description is required."));
        }
        if (command.Project <= 0)
        {
            return Result.Failure(new Error("ProjectTask.InvalidProject", "Invalid project ID."));
        }
        if (command.TaskStatus <= 0)
        {
            return Result.Failure(new Error("ProjectTask.InvalidStatus", "Invalid task status ID."));
        }

        return Result.Success();
    }
}