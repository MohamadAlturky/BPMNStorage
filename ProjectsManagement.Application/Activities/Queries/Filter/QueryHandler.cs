using ProjectsManagement.Core.Activities;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.Activities.Queries.Filter;
using ProjectsManagement.Core.Activities.Repositories;

namespace ProjectsManagement.Application.Activities.Queries;

public class FilterActivityQueryHandler : IQueryHandler<FilterActivityQuery, PaginatedResponse<Activity>>
{
    private readonly IActivityRepositoryPort _activityRepository;
    private readonly ILogger<FilterActivityQueryHandler> _logger;

    public FilterActivityQueryHandler(
        IActivityRepositoryPort activityRepository,
        ILogger<FilterActivityQueryHandler> logger)
    {
        _activityRepository = activityRepository;
        _logger = logger;
    }

    public async Task<Result<PaginatedResponse<Activity>>> Handle(FilterActivityQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Filter is null)
            {
                _logger.LogWarning("Filter action is null in FilterActivityQuery");
                return Result.Failure<PaginatedResponse<Activity>>(new Error("Activity.InvalidFilter", "The filter action cannot be null."));
            }

            var paginatedResponse = await _activityRepository.Filter(request.Filter);

            _logger.LogInformation("Successfully filtered activities. Total items: {TotalItems}", paginatedResponse.TotalCount);

            return Result.Success(paginatedResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while filtering activities");
            return Result.Failure<PaginatedResponse<Activity>>(new Error("Activity.FilterFailed", "Failed to filter activities."));
        }
    }
}