using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.Contracts.Activities.Queries.Filter;


public class FilterProjectTaskQuery : IQuery<PaginatedResponse<ProjectTask>>
{
    public Action<ProjectTaskFilter> Filter { get; set; }
}
