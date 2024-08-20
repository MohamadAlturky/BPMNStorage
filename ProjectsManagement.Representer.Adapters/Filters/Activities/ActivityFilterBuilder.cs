using ProjectsManagement.Core.Activities;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.Representer.Adapters.Filters.Activities;

public class ActivityFilterBuilder : IActivityFilterBuilder
{
    public Action<ActivityFilter> BuildFilter(ActivityFilter filter)
    {
        return new(target =>
        {

            target.Id = filter.Id;
            target.Count = filter.Count;
            target.PaginatedRequest = filter.PaginatedRequest;

            target.Project = filter.Project;
            target.ActivityResourceType = filter.ActivityResourceType;
            target.ActivityType = filter.ActivityType;

            target.DateEquals = filter.DateEquals;
            target.StartDate = filter.StartDate;
            target.EndDate = filter.EndDate;

            target.OrderByIdDescending = filter.OrderByIdDescending;
            target.OrderByIdAscending = filter.OrderByIdAscending;

            target.OrderByActivityTypeDescending = filter.OrderByActivityTypeDescending;
            target.OrderByActivityTypeAscending = filter.OrderByActivityTypeAscending;

            target.OrderByActivityResourceTypeDescending = filter.OrderByActivityResourceTypeDescending;
            target.OrderByActivityResourceTypeAscending = filter.OrderByActivityResourceTypeAscending;

            target.OrderByProjectDescending = filter.OrderByProjectDescending;
            target.OrderByProjectAscending = filter.OrderByProjectAscending;

            target.OrderByDateDescending = filter.OrderByDateDescending;
            target.OrderByDateAscending = filter.OrderByDateAscending;

            target.IncludeProject = filter.IncludeProject;
            target.IncludeActivityType = filter.IncludeActivityType;
            target.IncludeActivityResourceType = filter.IncludeActivityResourceType;
            target.IncludeActivityPrecedentPrecedent = filter.IncludeActivityPrecedentPrecedent;
        });
    }

    public PaginatedResponse<Activity> BuildResponse(PaginatedResponse<Activity> result)
    {
        foreach (var activity in result.Items)
        {
            if(activity.ProjectNavigation is not null)
            {
                activity.ProjectNavigation.Activities = [];
            }
            if (activity.ActivityTypeNavigation is not null)
            {
                activity.ActivityTypeNavigation.Activities = [];
            }
            if (activity.ActivityResourceTypeNavigation is not null)
            {
                activity.ActivityResourceTypeNavigation.Activities = [];
            }
            if (activity.ActivityPrecedentActivityNavigations is not null)
            {
                activity.ActivityPrecedentActivityNavigations
                    = activity.ActivityPrecedentActivityNavigations.ToList().Select(e => new ActivityPrecedent()
                    {
                        Activity = e.Activity,
                        Precedent = e.Precedent,
                        Id = e.Id
                    }).ToList();
            }
            if (activity.ActivityPrecedentPrecedentNavigations is not null)
            {
                activity.ActivityPrecedentPrecedentNavigations
                    = activity.ActivityPrecedentPrecedentNavigations.ToList().Select(e => new ActivityPrecedent()
                    {
                        Activity = e.Activity,
                        Precedent = e.Precedent,
                        Id = e.Id
                    }).ToList();
            }
        }
        return result;

    }
}