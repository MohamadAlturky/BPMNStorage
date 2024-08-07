using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.Contracts.Contributions.Queries.Filter;
using ProjectsManagement.Core.Contributions;

namespace ProjectsManagement.API.Endpoints.ContributionMembers;

public class FilterContributionMemberEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
     
        app.MapPost("/api/contribution-members/filter", async (FilterContributionMemberRequest request, ISender sender) =>
        {
            var query = new FilterContributionMemberQuery { Filter = new(r => r = request.Filter) };
            var result = await sender.Send(query);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithName("FilterContributionMembers")
        .WithTags("ContributionMembers")
        .Produces<PaginatedResponse<ContributionMember>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}