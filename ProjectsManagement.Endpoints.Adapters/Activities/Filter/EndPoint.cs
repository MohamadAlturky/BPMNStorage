using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.Activities.Queries.Filter;
using ProjectsManagement.Core.Activities;
using ProjectsManagement.Representer.Adapters.Filters.Activities;
using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.API.Endpoints.Activities;

public class FilterActivityEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/activities/filter", async (FilterActivityRequest request, ISender sender) =>
        {
            ActivityFilterBuilder filterBuilder = new();
            var query = new FilterActivityQuery { Filter = filterBuilder.BuildFilter(request.Filter) };
            var result = await sender.Send(query);

            return result.IsSuccess ? Results.Ok(filterBuilder.BuildResponse(result.Value)) : Results.BadRequest(result.Error);

        })
        .WithName("FilterActivities")
        .WithTags("Activities")
        .Produces<PaginatedResponse<Activity>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}

public class FilterActivityRequest
{
    public ActivityFilter Filter { get; set; }
}