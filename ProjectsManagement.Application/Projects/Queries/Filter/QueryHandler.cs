using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.Projects.Queries.Filter;
using ProjectsManagement.Core.Projects.Repositories;

namespace ProjectsManagement.Application.Projects.Queries.Filter;

public class FilterProjectQueryHandler : IQueryHandler<FilterProjectQuery, PaginatedResponse<Project>>
{
    private readonly IProjectRepositoryPort _projectRepository;
    private readonly ILogger<FilterProjectQueryHandler> _logger;

    public FilterProjectQueryHandler(
        IProjectRepositoryPort projectRepository,
        ILogger<FilterProjectQueryHandler> logger)
    {
        _projectRepository = projectRepository;
        _logger = logger;
    }

    public async Task<Result<PaginatedResponse<Project>>> Handle(FilterProjectQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Filter == null)
            {
                _logger.LogWarning("Filter action is null in FilterProjectQuery");
                return Result.Failure<PaginatedResponse<Project>>(new Error("Project.InvalidFilter", "The filter action cannot be null."));
            }

            var paginatedResponse = await _projectRepository.Filter(request.Filter);

            _logger.LogInformation("Successfully filtered projects. Total items: {TotalItems}", paginatedResponse.TotalItems);

            return Result.Success(paginatedResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while filtering projects");
            return Result.Failure<PaginatedResponse<Project>>(new Error("Project.FilterFailed", "Failed to filter projects."));
        }
    }
}