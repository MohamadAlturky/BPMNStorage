using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.Projects.Queries;
using ProjectsManagement.Contracts.Projects.Queries.Filter;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.API.Endpoints.Projects;

public class FilterProjectEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/projects/filter", async (FilterProjectRequest request, ISender sender) =>
        {
            var query = new FilterProjectQuery { Filter = new(r=>r = request.Filter) };
            var result = await sender.Send(query);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithName("FilterProjects")
        .WithTags("Projects")
        .Produces<PaginatedResponse<Project>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}