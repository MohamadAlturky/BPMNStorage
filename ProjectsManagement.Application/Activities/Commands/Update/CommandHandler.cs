using ProjectsManagement.Core.Activities;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Update;
using ProjectsManagement.Core.Activities.Repositories;

namespace ProjectsManagement.Application.Activities.Commands.Update;

public class UpdateActivityCommandHandler : ICommandHandler<UpdateActivityCommand, Activity>
{
    private readonly IActivityRepositoryPort _activityRepository;
    private readonly ILogger<UpdateActivityCommandHandler> _logger;

    public UpdateActivityCommandHandler(
        IActivityRepositoryPort activityRepository,
        ILogger<UpdateActivityCommandHandler> logger)
    {
        _activityRepository = activityRepository;
        _logger = logger;
    }

    public async Task<Result<Activity>> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        var validationResult = ValidateCommand(request);

        if (validationResult.IsFailure)
        {
            _logger.LogWarning("Validation failed for UpdateActivityCommand: {Error}", validationResult.Error);
            return Result.Failure<Activity>(validationResult.Error);
        }

        try
        {
            var existingActivity = await _activityRepository.GetByIdAsync(request.Id);
            if (existingActivity == null)
            {
                _logger.LogWarning("Activity not found for update. ID: {ActivityId}", request.Id);
                return Result.Failure<Activity>(new Error("Activity.NotFound", "The activity was not found."));
            }

            // Update the existing activity with new values
            existingActivity.Name = request.Name;
            existingActivity.Description = request.Description;
            existingActivity.Date = request.Date;
            existingActivity.Project = request.Project;
            existingActivity.ActivityType = request.ActivityType;
            existingActivity.ActivityResourceType = request.ActivityResourceType;

            await _activityRepository.UpdateAsync(existingActivity);
            _logger.LogInformation("Updated activity with ID: {ActivityId}", existingActivity.Id);

            return Result.Success(existingActivity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update activity with ID: {ActivityId}", request.Id);
            return Result.Failure<Activity>(new Error("Activity.UpdateFailed", "Failed to update the activity."));
        }
    }

    private Result ValidateCommand(UpdateActivityCommand command)
    {
        if (command.Id <= 0)
        {
            return Result.Failure(new Error("Activity.InvalidId", "Invalid activity ID."));
        }
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