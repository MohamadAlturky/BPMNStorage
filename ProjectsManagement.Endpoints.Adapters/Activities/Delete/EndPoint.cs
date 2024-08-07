using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.Activities.Commands.Delete;

namespace ProjectsManagement.API.Endpoints.Activities;

public class DeleteActivityEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/activities/{id}", async (int id, ISender sender) =>
        {
            var command = new DeleteActivityCommand { Id = id };
            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.NoContent();
        })
        .WithName("DeleteActivity")
        .WithTags("Activities")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest);
    }
}