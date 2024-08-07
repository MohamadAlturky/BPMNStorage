using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.Projects.Commands;
using ProjectsManagement.Contracts.Projects.Commands.Update;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.API.Endpoints.Projects;

public class UpdateProjectEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/projects/{id}", async (int id, UpdateProjectRequest request, ISender sender) =>
        {
            if (id != request.Id)
            {
                return Results.BadRequest("ID in the route does not match the ID in the request body.");
            }
            var command = new UpdateProjectCommand
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                ProjectType = request.ProjectType
            };
            var result = await sender.Send(command);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithName("UpdateProject")
        .WithTags("Projects")
        .Produces<Project>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}