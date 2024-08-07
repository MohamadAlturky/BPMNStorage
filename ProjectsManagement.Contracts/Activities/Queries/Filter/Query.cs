using ProjectsManagement.Core.Activities;
using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.Contracts.Activities.Queries.Filter;


public class FilterActivityQuery : IQuery<PaginatedResponse<Activity>>
{
    public Action<ActivityFilter> Filter { get; set; }
}
