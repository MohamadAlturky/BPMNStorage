using ProjectsManagement.Core.Activities;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.Contracts.Activities.Commands.Create;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Core.Activities.Repositories;

namespace ProjectsManagement.Application.Activities.Commands.Create;

public class CreateActivityCommandHandler : ICommandHandler<CreateActivityCommand, Activity>
{
    private readonly IActivityRepositoryPort _activityRepository;
    private readonly ILogger<CreateActivityCommandHandler> _logger;

    public CreateActivityCommandHandler(
        IActivityRepositoryPort activityRepository,
        ILogger<CreateActivityCommandHandler> logger)
    {
        _activityRepository = activityRepository;
        _logger = logger;
    }

    public async Task<Result<Activity>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateCommand(request);

        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Validation failed for CreateActivityCommand: {Error}", validationResult.Error);
            return Result.Failure<Activity>(validationResult.Error);
        }

        var activity = new Activity
        {
            Name = request.Name,
            Description = request.Description,
            Date = request.Date,
            Project = request.Project,
            ActivityType = request.ActivityType,
            ActivityResourceType = request.ActivityResourceType
        };

        try
        {
            var createdActivity = await _activityRepository.AddAsync(activity);
            _logger.LogInformation("Created new activity with ID: {ActivityId}", createdActivity.Id);

            return Result.Success(createdActivity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create activity");
            return Result.Failure<Activity>(new Error("Activity.CreationFailed", "Failed to create the activity."));
        }
    }

    private Result ValidateCommand(CreateActivityCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
        {
            return Result.Failure(new Error("Activity.NameRequired", "The activity name is required."));
        }
        if (string.IsNullOrWhiteSpace(command.Description))
        {
            return Result.Failure(new Error("Activity.DescriptionRequired", "The activity description is required."));
        }
        if (command.Date == default)
        {
            return Result.Failure(new Error("Activity.InvalidDate", "The activity date is required."));
        }
        if (command.Project <= 0)
        {
            return Result.Failure(new Error("Activity.InvalidProject", "Invalid project ID."));
        }
        if (command.ActivityType <= 0)
        {
            return Result.Failure(new Error("Activity.InvalidActivityType", "Invalid activity type ID."));
        }
        if (command.ActivityResourceType <= 0)
        {
            return Result.Failure(new Error("Activity.InvalidActivityResourceType", "Invalid activity resource type ID."));
        }

        return Result.Success();
    }
}