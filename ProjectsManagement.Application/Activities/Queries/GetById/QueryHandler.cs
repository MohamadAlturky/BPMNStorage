using ProjectsManagement.Core.Activities;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.ProjectTasks.Queries.GetById;
using ProjectsManagement.Core.Activities.Repositories;

namespace ProjectsManagement.Application.Activities.Queries;

public class GetActivityByIdQueryHandler : IQueryHandler<GetActivityByIdQuery, Activity>
{
    private readonly IActivityRepositoryPort _activityRepository;
    private readonly ILogger<GetActivityByIdQueryHandler> _logger;

    public GetActivityByIdQueryHandler(
        IActivityRepositoryPort activityRepository,
        ILogger<GetActivityByIdQueryHandler> logger)
    {
        _activityRepository = activityRepository;
        _logger = logger;
    }

    public async Task<Result<Activity>> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            _logger.LogWarning("Invalid activity ID: {ActivityId}", request.Id);
            return Result.Failure<Activity>(new Error("Activity.InvalidId", "Invalid activity ID."));
        }

        try
        {
            var activity = await _activityRepository.GetByIdAsync(request.Id);

            if (activity is null)
            {
                _logger.LogWarning("Activity not found. ID: {ActivityId}", request.Id);
                return Result.Failure<Activity>(new Error("Activity.NotFound", "The activity was not found."));
            }

            _logger.LogInformation("Successfully retrieved activity. ID: {ActivityId}", request.Id);
            return Result.Success(activity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving activity. ID: {ActivityId}", request.Id);
            return Result.Failure<Activity>(new Error("Activity.RetrievalFailed", "Failed to retrieve the activity."));
        }
    }
}