using ProjectsManagement.Core.Invitations;
using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.Representer.Adapters.Filters.Invitations;

public class InvitationFilterBuilder : IInvitationFilterBuilder
{
    public Action<InvitationFilter> BuildFilter(InvitationFilter filter)
    {
        return new(target =>
        {
            target.Id = filter.Id;
            target.Count = filter.Count;
            target.PaginatedRequest = filter.PaginatedRequest;

            target.Contributor = filter.Contributor;
            target.Project = filter.Project;
            target.By = filter.By;
            target.InvitationStatus = filter.InvitationStatus;

            target.DateEquals = filter.DateEquals;
            target.StartDate = filter.StartDate;
            target.EndDate = filter.EndDate;

            target.OrderByIdDescending = filter.OrderByIdDescending;
            target.OrderByIdAscending = filter.OrderByIdAscending;

            target.OrderByContributorDescending = filter.OrderByContributorDescending;
            target.OrderByContributorAscending = filter.OrderByContributorAscending;

            target.OrderByInvitationStatusDescending = filter.OrderByInvitationStatusDescending;
            target.OrderByInvitationStatusAscending = filter.OrderByInvitationStatusAscending;

            target.OrderByProjectDescending = filter.OrderByProjectDescending;
            target.OrderByProjectAscending = filter.OrderByProjectAscending;

            target.OrderByDateDescending = filter.OrderByDateDescending;
            target.OrderByDateAscending = filter.OrderByDateAscending;

            target.IncludeProject = filter.IncludeProject;
            target.IncludeInvitationStatus = filter.IncludeInvitationStatus;
        });
    }

    public PaginatedResponse<Invitation> BuildResponse(PaginatedResponse<Invitation> result)
    {
        foreach (var invitation in result.Items)
        {
            if (invitation.InvitationStatusNavigation is not null)
            {
                invitation.InvitationStatusNavigation.Invitations= [];
            }

            if (invitation.ProjectNavigation is not null)
            {
                if(invitation.ProjectNavigation.ProjectTypeNavigation is not null)
                {
                    invitation.ProjectNavigation.ProjectTypeNavigation.Projects= [];
                }
                invitation.ProjectNavigation.Invitations = [];
            }
        }
        return result;
    }
}