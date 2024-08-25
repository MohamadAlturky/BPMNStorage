using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Application.Users;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Create;
using ProjectsManagement.Core.Constants;
using ProjectsManagement.Core.Invitations;
using System.Reflection.Metadata;

namespace ProjectsManagement.API.Endpoints.Invitations;

public class CreateInvitationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/invitations", async (CreateInvitationRequest request,IUserIdentityPort userIdentityPort, ISender sender) =>
        {
            var command = new CreateInvitationCommand
            {
                Message = request.Message,
                Date = DateTime.UtcNow,
                By = await userIdentityPort.GetUserIdAsync(),
                Contributor = request.Contributor,
                Project = request.Project,
                InvitationStatus = ConstantsProvider.PENDING.Id
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