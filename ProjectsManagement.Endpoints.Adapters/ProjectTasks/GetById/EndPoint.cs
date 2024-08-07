using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.ProjectTasks.Queries;
using ProjectsManagement.Contracts.ProjectTasks.Queries.GetById;
using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.API.Endpoints.ProjectTasks;

public class GetProjectTaskByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/project-tasks/{id}", async (int id, ISender sender) =>
        {
            var query = new GetProjectTaskByIdQuery { Id = id };
            var result = await sender.Send(query);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithName("GetProjectTaskById")
        .WithTags("ProjectTasks")
        .Produces<ProjectTask>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}