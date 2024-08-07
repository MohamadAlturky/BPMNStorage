using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.AccessControl;
using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.Contracts.Projects.Queries.Filter;


public class FilterProjectQuery : IQuery<PaginatedResponse<Project>>
{
    public Action<ProjectFilter> Filter { get; set; }
    public AccessControlCriteria Criteria()
    {
        return new();
    }
}
