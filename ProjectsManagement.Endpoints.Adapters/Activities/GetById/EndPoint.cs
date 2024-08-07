using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.ProjectTasks.Queries.GetById;
using System.Diagnostics;

namespace ProjectsManagement.API.Endpoints.Activities;

public class GetActivityByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/activities/{id}", async (int id, ISender sender) =>
        {
            var query = new GetActivityByIdQuery { Id = id };
            var result = await sender.Send(query);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
        })
        .WithName("GetActivityById")
        .WithTags("Activities")
        .Produces<Activity>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}