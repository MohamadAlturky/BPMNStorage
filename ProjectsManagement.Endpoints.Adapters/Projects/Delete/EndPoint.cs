using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.Projects.Commands;
using ProjectsManagement.Contracts.Projects.Commands.Delete;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.API.Endpoints.Projects;

public class DeleteProjectEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/projects/{id}", async (int id, ISender sender) =>
        {
            var command = new DeleteProjectCommand { Id = id };
            var result = await sender.Send(command);
            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Error);
        })
        .WithName("DeleteProject")
        .WithTags("Projects")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest);
    }
}