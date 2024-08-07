using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.ProjectTasks.Queries.GetById;
using ProjectsManagement.Core.Invitations;

namespace ProjectsManagement.API.Endpoints.Invitations;

public class GetInvitationByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/invitations/{id}", async (int id, ISender sender) =>
        {
            var query = new GetInvitationByIdQuery { Id = id };
            var result = await sender.Send(query);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithName("GetInvitationById")
        .WithTags("Invitations")
        .Produces<Invitation>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}