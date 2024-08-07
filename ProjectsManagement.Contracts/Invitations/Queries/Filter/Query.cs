using ProjectsManagement.Core.Invitations;
using ProjectsManagement.SharedKernel.AccessControl;
using ProjectsManagement.SharedKernel.CQRS;
using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.Contracts.Activities.Queries.Filter;


public class FilterInvitationQuery : IQuery<PaginatedResponse<Invitation>>
{
    public Action<InvitationFilter> Filter { get; set; }
    public AccessControlCriteria Criteria()
    {
        return new();
    }
}
