using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.Representer.Adapters.Actions;

public interface IFilterBuilder<T, TOut>
{
    Action<T> BuildFilter(T filter);
    PaginatedResponse<TOut> BuildResponse(PaginatedResponse<TOut> result);
}


public interface IProjectFilterBuilder : IFilterBuilder<ProjectFilter, Project>
{

}

public class ProjectFilterBuilder : IProjectFilterBuilder
{
    public Action<ProjectFilter> BuildFilter(ProjectFilter filter)
    {
        return new(b =>
        {
            b.Id = filter.Id;
            b.Count = filter.Count;
            b.PaginatedRequest = filter.PaginatedRequest;
            b.ProjectType = filter.ProjectType;

            b.OrderByIdDescending = filter.OrderByIdDescending;
            b.OrderByIdAscending = filter.OrderByIdAscending;
            b.OrderByProjectTypeDescending = filter.OrderByProjectTypeDescending;
            b.OrderByProjectTypeAscending = filter.OrderByProjectTypeAscending;

            b.IncludeActivities = filter.IncludeActivities;
            b.IncludeContributionMembers = filter.IncludeContributionMembers;
            b.IncludeInvitations = filter.IncludeInvitations;
            b.IncludeTasks = filter.IncludeTasks;

            b.IncludeProjectType = filter.IncludeProjectType;
        });
    }

    public PaginatedResponse<Project> BuildResponse(PaginatedResponse<Project> result)
    {
        foreach (var project in result.Items)
        {
            if(project.ProjectTypeNavigation is not null)
            {
                project.ProjectTypeNavigation.Projects = [];
            }

            if (project.Tasks is not null)
            {
                foreach (var task in project.Tasks)
                {
                    task.ProjectNavigation = null;

                    if (task.TaskStatusNavigation is not null)
                    {
                        task.TaskStatusNavigation.Tasks = [];
                    }
                }
            }
            if (project.Activities is not null)
            {
                foreach (var activity in project.Activities)
                {
                    activity.ProjectNavigation = null;

                    if (activity.ActivityResourceTypeNavigation is not null)
                    {
                        activity.ActivityResourceTypeNavigation.Activities = [];
                    }
                    if (activity.ActivityTypeNavigation is not null)
                    {
                        activity.ActivityTypeNavigation.Activities = null;
                    }
                }
            }
            if (project.Invitations is not null)
            {
                foreach (var invitation in project.Invitations)
                {
                    invitation.ProjectNavigation = null;

                    if (invitation.InvitationStatusNavigation is not null)
                    {
                        invitation.InvitationStatusNavigation.Invitations = [];
                    }
                }
            }
            if (project.ContributionMembers is not null)
            {
                foreach (var contributionMember in project.ContributionMembers)
                {
                    contributionMember.ProjectNavigation = null;

                    if (contributionMember.ContributionTypeNavigation is not null)
                    {
                        contributionMember.ContributionTypeNavigation.ContributionMembers = [];
                    }
                }
            }
        }
        return result;
    }

    //public PaginatedResponse<Project> BuildResponse(PaginatedResponse<Project> result)
    //{
    //    List<Project> projects = new List<Project>();
    //    foreach(var project in result.Items)
    //    {
    //        var newProject = new Project();
    //        newProject.Id = project.Id;
    //        newProject.Name = project.Name;
    //        newProject.Description = project.Description;
    //        newProject.ProjectType = project.ProjectType;

    //        if(project.ProjectTypeNavigation is not null)
    //        {

    //            newProject.ProjectTypeNavigation = new()
    //            {
    //                Id = project.ProjectTypeNavigation.Id,
    //                Name = project.ProjectTypeNavigation.Name,
    //            };
    //        }
    //        if(project.Tasks is not null)
    //        {
    //            newProject.Tasks = [];
    //            foreach(var task in project.Tasks)
    //            {
    //                ProjectTask newTask = new()
    //                {
    //                    Id= task.Id,
    //                    Description = task.Description,
    //                    Name = task.Name,
    //                    TaskStatus = task.TaskStatus
    //                };
    //                if(task.TaskStatusNavigation is not null)
    //                {
    //                    newTask.TaskStatusNavigation = new();
    //                    newTask.TaskStatusNavigation.Name = task.TaskStatusNavigation.Name;
    //                    newTask.TaskStatusNavigation.Id = task.TaskStatusNavigation.Id;
    //                }
    //                newProject.Tasks.Add(newTask);
    //            }
    //        }
    //        projects.Add(newProject);
    //    }
    //    return new PaginatedResponse<Project>()
    //    {
    //        TotalCount = result.TotalCount,
    //        Items =projects
    //    };
    //}

}