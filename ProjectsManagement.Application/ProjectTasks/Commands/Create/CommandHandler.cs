using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.Results;
using MediatR;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Create;
using ProjectsManagement.Core.Tasks.Storage;
using ProjectsManagement.SharedKernel.CQRS;

namespace ProjectsManagement.Application.ProjectTasks.Commands.Create;

public class CreateProjectTaskCommandHandler : ICommandHandler<CreateProjectTaskCommand, ProjectTask>
{
    private readonly IProjectTaskRepositoryPort _projectTaskRepository;

    public CreateProjectTaskCommandHandler(IProjectTaskRepositoryPort projectTaskRepository)
    {
        _projectTaskRepository = projectTaskRepository;
    }

    public async Task<Result<ProjectTask>> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateCommand(request);

        if (validationResult.IsFailure)
        {
            return Result.Failure<ProjectTask>(validationResult.Error);
        }

        var projectTask = new ProjectTask
        {
            Name = request.Name,
            Description = request.Description,
            Project = request.Project,
            TaskStatus = request.TaskStatus
        };

        try
        {
            var createdTask = await _projectTaskRepository.AddAsync(projectTask);

            return Result.Success(createdTask);
        }
        catch (Exception)
        {
            return Result.Failure<ProjectTask>(new Error("ProjectTask.CreationFailed", "Failed to create the project task."));
        }
    }

    private Result ValidateCommand(CreateProjectTaskCommand command)
    {
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