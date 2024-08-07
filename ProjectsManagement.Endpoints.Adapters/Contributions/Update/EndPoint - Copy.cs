//using Carter;
//using MediatR;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing;
//using ProjectsManagement.SharedKernel.Pagination;
//using ProjectsManagement.Contracts.Contributions.Commands.Create;
//using ProjectsManagement.Contracts.Contributions.Queries.Filter;
//using ProjectsManagement.Contracts.ProjectTasks.Commands.Delete;
//using ProjectsManagement.Contracts.ProjectTasks.Commands.Update;
//using ProjectsManagement.Contracts.ProjectTasks.Queries.GetById;
//using ProjectsManagement.Core.Contributions;

//namespace ProjectsManagement.API.Endpoints.ContributionMembers;

//public class CreateContributionMemberEndpoints : ICarterModule
//{
//    public void AddRoutes(IEndpointRouteBuilder app)
//    {
//        app.MapGet("/api/contribution-members/{id}", async (int id, ISender sender) =>
//        {
//            var query = new GetContributionMemberByIdQuery { Id = id };
//            var result = await sender.Send(query);
//            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
//        })
//        .WithName("GetContributionMemberById")
//        .WithTags("ContributionMembers")
//        .Produces<ContributionMember>(StatusCodes.Status200OK)
//        .Produces(StatusCodes.Status400BadRequest);

//        app.MapPost("/api/contribution-members/filter", async (FilterContributionMemberRequest request, ISender sender) =>
//        {
//            var query = new FilterContributionMemberQuery { Filter = new(() => request.Filter) };
//            var result = await sender.Send(query);
//            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
//        })
//        .WithName("FilterContributionMembers")
//        .WithTags("ContributionMembers")
//        .Produces<PaginatedResponse<ContributionMember>>(StatusCodes.Status200OK)
//        .Produces(StatusCodes.Status400BadRequest);

//        app.MapPut("/api/contribution-members/{id}", async (int id, UpdateContributionMemberRequest request, ISender sender) =>
//        {
//            if (id != request.Id)
//            {
//                return Results.BadRequest("ID in the route does not match the ID in the request body.");
//            }
//            var command = new UpdateContributionMemberCommand
//            {
//                Id = request.Id,
//                Project = request.Project,
//                Contributor = request.Contributor,
//                ContributionType = request.ContributionType,
//                Date = request.Date
//            };
//            var result = await sender.Send(command);
//            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
//        })
//        .WithName("UpdateContributionMember")
//        .WithTags("ContributionMembers")
//        .Produces<ContributionMember>(StatusCodes.Status200OK)
//        .Produces(StatusCodes.Status400BadRequest);

//        app.MapDelete("/api/contribution-members/{id}", async (int id, ISender sender) =>
//        {
//            var command = new DeleteContributionMemberCommand { Id = id };
//            var result = await sender.Send(command);
//            return result.IsSuccess ? Results.NoContent() : Results.BadRequest(result.Error);
//        })
//        .WithName("DeleteContributionMember")
//        .WithTags("ContributionMembers")
//        .Produces(StatusCodes.Status204NoContent)
//        .Produces(StatusCodes.Status400BadRequest);

//        app.MapPost("/api/contribution-members", async (CreateContributionMemberRequest request, ISender sender) =>
//        {
//            var command = new CreateContributionMemberCommand
//            {
//                Project = request.Project,
//                Contributor = request.Contributor,
//                ContributionType = request.ContributionType,
//                Date = request.Date
//            };
//            var result = await sender.Send(command);
//            return result.IsSuccess
//                ? Results.Created($"/api/contribution-members/{result.Value.Id}", result.Value)
//                : Results.BadRequest(result.Error);
//        })
//        .WithName("CreateContributionMember")
//        .WithTags("ContributionMembers")
//        .Produces<ContributionMember>(StatusCodes.Status201Created)
//        .Produces(StatusCodes.Status400BadRequest);
//    }
//}