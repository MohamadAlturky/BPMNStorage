using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Results;
using ProjectsManagement.SharedKernel.CQRS;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.Projects.Queries.GetById;
using ProjectsManagement.Core.Projects.Repositories;

namespace ProjectsManagement.Application.Projects.Queries;

public class GetProjectByIdQueryHandler : IQueryHandler<GetProjectByIdQuery, Project>
{
    private readonly IProjectRepositoryPort _projectRepository;
    private readonly ILogger<GetProjectByIdQueryHandler> _logger;

    public GetProjectByIdQueryHandler(
        IProjectRepositoryPort projectRepository,
        ILogger<GetProjectByIdQueryHandler> logger)
    {
        _projectRepository = projectRepository;
        _logger = logger;
    }

    public async Task<Result<Project>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            _logger.LogWarning("Invalid project ID: {ProjectId}", request.Id);
            return Result.Failure<Project>(new Error("Project.InvalidId", "Invalid project ID."));
        }

        try
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            if (project is null)
            {
                _logger.LogWarning("Project not found. ID: {ProjectId}", request.Id);
                return Result.Failure<Project>(new Error("Project.NotFound", "The project was not found."));
            }

            _logger.LogInformation("Successfully retrieved project. ID: {ProjectId}", request.Id);
            return Result.Success(project);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving project. ID: {ProjectId}", request.Id);
            return Result.Failure<Project>(new Error("Project.RetrievalFailed", "Failed to retrieve the project."));
        }
    }
}