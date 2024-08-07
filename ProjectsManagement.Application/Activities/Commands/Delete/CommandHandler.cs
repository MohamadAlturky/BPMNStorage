using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.Contracts.Activities.Commands.Delete;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Core.Activities.Repositories;

namespace ProjectsManagement.Application.Activities.Commands.Delete;

public class DeleteActivityCommandHandler : ICommandHandler<DeleteActivityCommand>
{
    private readonly IActivityRepositoryPort _activityRepository;
    private readonly ILogger<DeleteActivityCommandHandler> _logger;

    public DeleteActivityCommandHandler(
        IActivityRepositoryPort activityRepository,
        ILogger<DeleteActivityCommandHandler> logger)
    {
        _activityRepository = activityRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            _logger.LogWarning("Invalid activity ID: {ActivityId}", request.Id);
            return Result.Failure(new Error("Activity.InvalidId", "Invalid activity ID."));
        }

        try
        {
            var existingActivity = await _activityRepository.GetByIdAsync(request.Id);
            if (existingActivity == null)
            {
                _logger.LogWarning("Activity not found for deletion. ID: {ActivityId}", request.Id);
                return Result.Failure(new Error("Activity.NotFound", "The activity was not found."));
            }

            // You might want to add additional checks here, e.g., if the activity can be deleted

            await _activityRepository.DeleteAsync(request.Id);
            _logger.LogInformation("Deleted activity with ID: {ActivityId}", request.Id);

            // Optionally, raise a domain event here
            // _domainEventDispatcher.Raise(new ActivityDeletedEvent(request.Id));

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete activity with ID: {ActivityId}", request.Id);
            return Result.Failure(new Error("Activity.DeletionFailed", "Failed to delete the activity."));
        }
    }
}