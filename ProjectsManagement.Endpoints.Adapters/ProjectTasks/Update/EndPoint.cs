using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.ProjectTasks.Commands;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Update;
using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.API.Endpoints.ProjectTasks;

public class UpdateProjectTaskEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/project-tasks/{id}", async (int id, UpdateProjectTaskRequest request, ISender sender) =>
        {
            if (id != request.Id)
            {
                return Results.BadRequest("ID in the route does not match the ID in the request body.");
            }
            var command = new UpdateProjectTaskCommand
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Project = request.Project,
                TaskStatus = request.TaskStatus
            };
            var result = await sender.Send(command);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithName("UpdateProjectTask")
        .WithTags("ProjectTasks")
        .Produces<ProjectTask>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}