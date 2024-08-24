using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.ProjectTasks.Commands;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Create;
using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.API.Endpoints.ProjectTasks;

public class CreateProjectTaskEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/project-tasks", async (CreateProjectTaskRequest request, ISender sender) =>
        {
            var command = new CreateProjectTaskCommand
            {
                Name = request.Name,
                Description = request.Description,
                Project = request.Project,
                TaskStatus = request.TaskStatus,
                BasedOn = request.BasedOn
            };
            var result = await sender.Send(command);
            return result.IsSuccess
                ? Results.Created($"/api/project-tasks/{result.Value.Id}", result.Value)
                : Results.BadRequest(result.Error);
        })
        .WithName("CreateProjectTask")
        .WithTags("ProjectTasks")
        .Produces<ProjectTask>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
    }
}