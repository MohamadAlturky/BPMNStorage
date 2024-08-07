using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.Core.Tasks.Storage;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.ProjectTasks.Queries.GetById;

namespace ProjectsManagement.Application.ProjectTasks.Queries;

public class GetProjectTaskByIdQueryHandler : IQueryHandler<GetProjectTaskByIdQuery, ProjectTask>
{
    private readonly IProjectTaskRepositoryPort _projectTaskRepository;
    private readonly ILogger<GetProjectTaskByIdQueryHandler> _logger;

    public GetProjectTaskByIdQueryHandler(
        IProjectTaskRepositoryPort projectTaskRepository,
        ILogger<GetProjectTaskByIdQueryHandler> logger)
    {
        _projectTaskRepository = projectTaskRepository;
        _logger = logger;
    }

    public async Task<Result<ProjectTask>> Handle(GetProjectTaskByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            _logger.LogWarning("Invalid project task ID: {TaskId}", request.Id);
            return Result.Failure<ProjectTask>(new Error("ProjectTask.InvalidId", "Invalid project task ID."));
        }

        try
        {
            var projectTask = await _projectTaskRepository.GetByIdAsync(request.Id);

            if (projectTask is null)
            {
                _logger.LogWarning("Project task not found. ID: {TaskId}", request.Id);
                return Result.Failure<ProjectTask>(new Error("ProjectTask.NotFound", "The project task was not found."));
            }

            _logger.LogInformation("Successfully retrieved project task. ID: {TaskId}", request.Id);
            return Result.Success(projectTask);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving project task. ID: {TaskId}", request.Id);
            return Result.Failure<ProjectTask>(new Error("ProjectTask.RetrievalFailed", "Failed to retrieve the project task."));
        }
    }
}