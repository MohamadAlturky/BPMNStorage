using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Create;
using ProjectsManagement.Core.Invitations;

namespace ProjectsManagement.API.Endpoints.Invitations;

public class CreateInvitationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/invitations", async (CreateInvitationRequest request, ISender sender) =>
        {
            var command = new CreateInvitationCommand
            {
                Message = request.Message,
                Date = request.Date,
                Contributor = request.Contributor,
                Project = request.Project,
                InvitationStatus = request.InvitationStatus
            };
            var result = await sender.Send(command);
            return result.IsSuccess
                ? Results.Created($"/api/invitations/{result.Value.Id}", result.Value)
                : Results.BadRequest(result.Error);
        })
        .WithName("CreateInvitation")
        .WithTags("Invitations")
        .Produces<Invitation>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
    }
}