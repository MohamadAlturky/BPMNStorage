using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.Activities.Commands.Create;
using ProjectsManagement.Core.Activities;

namespace ProjectsManagement.API.Endpoints.Activities;

public class CreateActivityEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/activities", async (CreateActivityRequest request, ISender sender) =>
        {
            var command = new CreateActivityCommand
            {
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

            return Results.Created($"/api/activities/{result.Value.Id}", result.Value);
        })
        .WithName("CreateActivity")
        .WithTags("Activities")
        .Produces<Activity>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
public class CreateActivityRequest
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public int Project { get; set; }
    public int ActivityType { get; set; }
    public int ActivityResourceType { get; set; }
}