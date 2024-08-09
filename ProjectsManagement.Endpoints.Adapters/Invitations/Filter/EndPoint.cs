using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.Activities.Queries.Filter;
using ProjectsManagement.Core.Invitations;
using ProjectsManagement.Representer.Adapters.Filters.Invitations;
using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.API.Endpoints.Invitations;

public class FilterInvitationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/invitations/filter", async (FilterInvitationRequest request, ISender sender) =>
        {
            InvitationFilterBuilder filterBuilder = new();
            var query = new FilterInvitationQuery { Filter = filterBuilder.BuildFilter(request.Filter) };
            var result = await sender.Send(query);
            return result.IsSuccess ? Results.Ok(filterBuilder.BuildResponse(result.Value)) : Results.BadRequest(result.Error);
        })
        .WithName("FilterInvitations")
        .WithTags("Invitations")
        .Produces<PaginatedResponse<Invitation>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}