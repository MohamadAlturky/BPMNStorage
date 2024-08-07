using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Update;
using ProjectsManagement.Core.Contributions;

namespace ProjectsManagement.API.Endpoints.ContributionMembers;

public class UpdateContributionMemberEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/contribution-members/{id}", async (int id, UpdateContributionMemberRequest request, ISender sender) =>
        {
            if (id != request.Id)
            {
                return Results.BadRequest("ID in the route does not match the ID in the request body.");
            }
            var command = new UpdateContributionMemberCommand
            {
                Id = request.Id,
                Project = request.Project,
                Contributor = request.Contributor,
                ContributionType = request.ContributionType,
                Date = request.Date
            };
            var result = await sender.Send(command);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithName("UpdateContributionMember")
        .WithTags("ContributionMembers")
        .Produces<ContributionMember>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}