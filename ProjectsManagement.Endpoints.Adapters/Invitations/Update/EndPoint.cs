using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Update;
using ProjectsManagement.Core.Invitations;

namespace ProjectsManagement.API.Endpoints.Invitations;

public class UpdateInvitationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/invitations/{id}", async (int id, UpdateInvitationRequest request, ISender sender) =>
        {
            if (id != request.Id)
            {
                return Results.BadRequest("ID in the route does not match the ID in the request body.");
            }
            var command = new UpdateInvitationCommand
            {
                Id = request.Id,
                InvitationStatus = request.InvitationStatus,
                Contributor = request.Contributor,
                Project = request.Project,
                Message = request.Message,
                Date = request.Date
            };
            var result = await sender.Send(command);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithName("UpdateInvitation")
        .WithTags("Invitations")
        .Produces<Invitation>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}