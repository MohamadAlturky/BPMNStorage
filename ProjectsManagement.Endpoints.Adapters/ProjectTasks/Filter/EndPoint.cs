using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.Activities.Queries.Filter;
using ProjectsManagement.Contracts.ProjectTasks.Queries;
using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.API.Endpoints.ProjectTasks;

public class FilterProjectTaskEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/project-tasks/filter", async (FilterProjectTaskRequest request, ISender sender) =>
        {
            var query = new FilterProjectTaskQuery { Filter = new(r=>r = request.Filter) };
            var result = await sender.Send(query);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithName("FilterProjectTasks")
        .WithTags("ProjectTasks")
        .Produces<PaginatedResponse<ProjectTask>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}