using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.ProjectTasks.Commands;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Delete;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.API.Endpoints.ProjectTasks;

public class DeleteProjectTaskEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/project-tasks/{id}", async (int id, ISender sender) =>
        {
            var command = new DeleteProjectTaskCommand { Id = id };
            var result = await sender.Send(command);
            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Error);
        })
        .WithName("DeleteProjectTask")
        .WithTags("ProjectTasks")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest);
    }
}