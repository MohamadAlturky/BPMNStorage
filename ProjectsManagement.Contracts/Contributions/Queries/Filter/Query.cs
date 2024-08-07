using ProjectsManagement.Core.Contributions;
using ProjectsManagement.SharedKernel.AccessControl;
using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.Contracts.Contributions.Queries.Filter;


public class FilterContributionMemberQuery : IQuery<PaginatedResponse<ContributionMember>>
{
    public Action<ContributionMemberFilter> Filter { get; set; }
    public AccessControlCriteria Criteria()
    {
        return new();
    }
}
