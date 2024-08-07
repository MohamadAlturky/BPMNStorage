using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Update;
using ProjectsManagement.Core.Activities;

namespace ProjectsManagement.API.Endpoints.Activities;

public class UpdateActivityEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/activities/{id}", async (int id, UpdateActivityRequest request, ISender sender) =>
        {
            if (id != request.Id)
            {
                return Results.BadRequest("ID in the route does not match the ID in the request body.");
            }

            var command = new UpdateActivityCommand
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Date = request.Date,
                Project = request.Project,
                ActivityType = request.ActivityType,
                ActivityResourceType = request.ActivityResourceType
            };

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value);
        })
        .WithName("UpdateActivity")
        .WithTags("Activities")
        .Produces<Activity>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}

public class UpdateActivityRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public int Project { get; set; }
    public int ActivityType { get; set; }
    public int ActivityResourceType { get; set; }
}