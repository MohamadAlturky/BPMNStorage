using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.Projects.Queries;
using ProjectsManagement.Contracts.Projects.Queries.GetById;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.API.Endpoints.Projects;

public class GetProjectByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/projects/{id}", async (int id, ISender sender) =>
        {
            var query = new GetProjectByIdQuery { Id = id };
            var result = await sender.Send(query);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithName("GetProjectById")
        .WithTags("Projects")
        .Produces<Project>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}