using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.Contracts.Contributions.Commands.Create;
using ProjectsManagement.Contracts.Contributions.Queries.Filter;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Delete;
using ProjectsManagement.Contracts.ProjectTasks.Commands.Update;
using ProjectsManagement.Contracts.ProjectTasks.Queries.GetById;
using ProjectsManagement.Core.Contributions;

namespace ProjectsManagement.API.Endpoints.ContributionMembers;

public class CreateContributionMemberEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/contribution-members", async (CreateContributionMemberRequest request, ISender sender) =>
        {
            var command = new CreateContributionMemberCommand
            {
                Project = request.Project,
                Contributor = request.Contributor,
                ContributionType = request.ContributionType,
                Date = request.Date
            };
            var result = await sender.Send(command);
            return result.IsSuccess
                ? Results.Created($"/api/contribution-members/{result.Value.Id}", result.Value)
                : Results.BadRequest(result.Error);
        })
        .WithName("CreateContributionMember")
        .WithTags("ContributionMembers")
        .Produces<ContributionMember>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
    }
}