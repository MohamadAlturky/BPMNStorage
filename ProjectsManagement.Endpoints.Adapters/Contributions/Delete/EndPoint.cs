using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Delete;

namespace ProjectsManagement.API.Endpoints.ContributionMembers;

public class DeleteContributionMemberEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/contribution-members/{id}", async (int id, ISender sender) =>
        {
            var command = new DeleteContributionMemberCommand { Id = id };
            var result = await sender.Send(command);
            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Error);
        })
        .WithName("DeleteContributionMember")
        .WithTags("ContributionMembers")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest);
    }
}