using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.Pagination;
using MediatR;
using ProjectsManagement.Contracts.ProjectTasks.Queries;
using ProjectsManagement.Core.Tasks.Storage;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.Activities.Queries.Filter;

namespace ProjectsManagement.Application.ProjectTasks.Queries;

public class FilterProjectTaskQueryHandler : IQueryHandler<FilterProjectTaskQuery, PaginatedResponse<ProjectTask>>
{
    private readonly IProjectTaskRepositoryPort _projectTaskRepository;
    private readonly ILogger<FilterProjectTaskQueryHandler> _logger;

    public FilterProjectTaskQueryHandler(
        IProjectTaskRepositoryPort projectTaskRepository,
        ILogger<FilterProjectTaskQueryHandler> logger)
    {
        _projectTaskRepository = projectTaskRepository;
        _logger = logger;
    }

    public async Task<Result<PaginatedResponse<ProjectTask>>> Handle(FilterProjectTaskQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Filter is null)
            {
                _logger.LogWarning("Filter action is null in FilterProjectTaskQuery");
                return Result.Failure<PaginatedResponse<ProjectTask>>(new Error("ProjectTask.InvalidFilter", "The filter action cannot be null."));
            }

            var paginatedResponse = await _projectTaskRepository.Filter(request.Filter);

            _logger.LogInformation("Successfully filtered project tasks. Total items: {TotalItems}", paginatedResponse.TotalCount);

            return Result.Success(paginatedResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while filtering project tasks");
            return Result.Failure<PaginatedResponse<ProjectTask>>(new Error("ProjectTask.FilterFailed", "Failed to filter project tasks."));
        }
    }
}