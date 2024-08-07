using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ProjectsManagement.Contracts.Activities.Queries.Filter;
using ProjectsManagement.Core.Activities;
using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.API.Endpoints.Activities;

public class FilterActivityEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/activities/filter", async (FilterActivityRequest request, ISender sender) =>
        {
            var query = new FilterActivityQuery { Filter = new(r=>r = request.Filter) };
            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }
            return Results.Ok(result.Value);
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