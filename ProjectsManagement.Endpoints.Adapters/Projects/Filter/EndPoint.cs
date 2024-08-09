using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using ProjectsManagement.Contracts.Projects.Queries.Filter;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.Representer.Adapters.Actions;
using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.API.Endpoints.Projects;

public class FilterProjectEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/projects/filter", async (FilterProjectRequest request, ILogger<FilterProjectEndpoint> logger, 
            ISender sender) =>
        {
            ProjectFilterBuilder filterBuilder = new();
            var query = new FilterProjectQuery { Filter = filterBuilder.BuildFilter(request.Filter)};
            var result = await sender.Send(query);

            return result.IsSuccess ? Results.Ok(filterBuilder.BuildResponse(result.Value)) : Results.BadRequest(result.Error);
        })
        .WithName("FilterProjects")
        .WithTags("Projects")
        .Produces<PaginatedResponse<Project>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}