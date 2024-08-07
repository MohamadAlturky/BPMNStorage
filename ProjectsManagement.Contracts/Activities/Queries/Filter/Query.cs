using ProjectsManagement.Core.Activities;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.AccessControl;
using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.Contracts.Activities.Queries.Filter;


public class FilterActivityQuery : IQuery<PaginatedResponse<Activity>>
{
    public Action<ActivityFilter> Filter { get; set; }
    public AccessControlCriteria Criteria()
    {
        ActivityFilter activityFilter = new ActivityFilter();
        Filter(activityFilter);

        return new()
        {
            Project = activityFilter.Project
        };
    }
}
