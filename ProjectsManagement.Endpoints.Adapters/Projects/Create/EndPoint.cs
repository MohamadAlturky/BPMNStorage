using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.Projects.Commands;
using ProjectsManagement.Contracts.Projects.Commands.Create;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.API.Endpoints.Projects;

public class CreateProjectEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/projects", async (CreateProjectRequest request, ISender sender) =>
        {
            var command = new CreateProjectCommand
            {
                Name = request.Name,
                Description = request.Description,
                ProjectType = request.ProjectType
            };
            var result = await sender.Send(command);
            return result.IsSuccess
                ? Results.Created($"/api/projects/{result.Value.Id}", new
                {
                    Id = result.Value.Id,
                    Name = result.Value.Name
                })
                : Results.BadRequest(result.Error);
        })
        .WithName("CreateProject")
        .WithTags("Projects")
        .Produces<Project>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
    }
}