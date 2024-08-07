using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.Core.Projects.Repositories;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.Storage.Adapters.Context;
using ProjectsManagement.Storage.Adapters.Repositories;

namespace ProjectsManagement.Infrastructure.Repositories;

public class ProjectRepositoryAdapter : BaseRepository<Project, ProjectFilter>, IProjectRepositoryPort
{
    public ProjectRepositoryAdapter(AppDbContext context) : base(context)
    {
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
        var projectFilter = new ProjectFilter();
        filter(projectFilter);

        var query = _context.Projects.AsQueryable();

        // Apply filters
        if (projectFilter.Id.HasValue)
            query = query.Where(p => p.Id == projectFilter.Id);
        

        if (projectFilter.ProjectType.HasValue)
            query = query.Where(p => p.ProjectType == projectFilter.ProjectType);
        

        // Apply ordering
        if (projectFilter.OrderByIdDescending)
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
        
        if (projectFilter.IncludeContributionMembers)
            query = query.Include(p => p.ContributionMembers);
        
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

        var items = await query.ToListAsync();

        return new PaginatedResponse<Project>
        {
            TotalCount = totalCount,
            Items = items
        };
    }
}
    
    
