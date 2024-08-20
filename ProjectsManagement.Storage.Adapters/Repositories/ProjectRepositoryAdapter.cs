using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Application.Users;
using ProjectsManagement.Core.Contributions;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.Core.Projects.Repositories;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.Storage.Adapters.Context;
using ProjectsManagement.Storage.Adapters.Repositories;

namespace ProjectsManagement.Infrastructure.Repositories;

public class ProjectRepositoryAdapter : BaseRepository<Project, ProjectFilter>, IProjectRepositoryPort
{
    private readonly IUserIdentityPort _identityPort;

    public ProjectRepositoryAdapter(AppDbContext context, IUserIdentityPort identityPort) : base(context)
    {
        _identityPort = identityPort;

    }
    public override async Task<Project?> GetByIdAsync(int id)
    {
        return await _context.Projects
            .Include(p => p.Tasks)
            .Include(p => p.Invitations)
            .Include(p => p.ContributionMembers)
            .Include(p => p.Activities)
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();
    }

    public override async Task<PaginatedResponse<Project>> Filter(Action<ProjectFilter> filter)
    {
        int contributor = await _identityPort.GetUserIdAsync();

        var projectFilter = new ProjectFilter();
        filter(projectFilter);

        var query = _context.Projects.AsQueryable();


        query  = query.AsSplitQuery()
            .Include(p => p.ContributionMembers);

        query = query.Where(p => p.ContributionMembers.Any(cm => cm.Contributor == contributor));

        // Apply filters
        if (projectFilter.Id.HasValue)
            query = query.Where(p => p.Id == projectFilter.Id);


        if (projectFilter.ProjectType.HasValue)
            query = query.Where(p => p.ProjectType == projectFilter.ProjectType);


        // Apply ordering
        //if (projectFilter.OrderByIdDescending)
        query = query.OrderByDescending(p => p.Id);

        if (projectFilter.OrderByIdAscending)
            query = query.OrderBy(p => p.Id);

        if (projectFilter.OrderByProjectTypeDescending)
            query = query.OrderByDescending(p => p.ProjectType);

        if (projectFilter.OrderByProjectTypeAscending)
            query = query.OrderBy(p => p.ProjectType);


        // Apply includes
        if (projectFilter.IncludeActivities)
            query = query.Include(p => p.Activities);

        if (projectFilter.IncludeProjectType)
            query = query.Include(p => p.ProjectTypeNavigation);

        if (projectFilter.IncludeContributionMembers)
            query = query.Include(p => p.ContributionMembers).ThenInclude(f => f.ContributionTypeNavigation);

        if (projectFilter.IncludeInvitations)
            query = query.Include(p => p.Invitations);

        if (projectFilter.IncludeTasks)
            query = query.Include(p => p.Tasks);


        // Apply pagination
        var totalCount = await query.CountAsync();

        if (projectFilter.PaginatedRequest is not null)
        {
            query = query.Skip((projectFilter.PaginatedRequest.PageNumber - 1) * projectFilter.PaginatedRequest.PageSize)
                         .Take(projectFilter.PaginatedRequest.PageSize);
        }
        else if (projectFilter.Count.HasValue)
        {
            query = query.Take(projectFilter.Count.Value);
        }
        //query = query.Include(e => e.ContributionMembers).ThenInclude(e => e.ContributionTypeNavigation);

        var items = await query.ToListAsync();


        HashSet<int> ids = [];
        if (projectFilter.IncludeContributionMembers)
        {
            foreach (var item in items)
            {
                foreach (var member in item.ContributionMembers)
                {
                    ids.Add(member.Contributor);
                }
            }
            if (ids.Count != 0)
            {

                HashSet<ContributorInfo> infos = await _identityPort.GetUsersAsync(ids);

                foreach (var item in items)
                {
                    foreach (var member in item.ContributionMembers)
                    {
                        member.ContributorInfo = infos.FirstOrDefault(e => e.Id == member.Contributor);
                    }
                }
            }
        }


        return new PaginatedResponse<Project>
        {
            TotalCount = totalCount,
            Items = items
        };
    }
}


