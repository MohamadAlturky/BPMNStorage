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

public class GetContributionMemberEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/contribution-members/{id}", async (int id, ISender sender) =>
        {
            var query = new GetContributionMemberByIdQuery { Id = id };
            var result = await sender.Send(query);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithName("GetContributionMemberById")
        .WithTags("ContributionMembers")
        .Produces<ContributionMember>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

    }
}