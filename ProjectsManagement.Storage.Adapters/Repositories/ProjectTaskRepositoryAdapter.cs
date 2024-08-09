using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Application.Users;
using ProjectsManagement.Core.ProjectTasks;
using ProjectsManagement.Core.Tasks.Storage;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.Storage.Adapters.Context;
using ProjectsManagement.Storage.Adapters.Repositories;

namespace ProjectsManagement.Infrastructure.Repositories;

public class ProjectTaskRepositoryAdapter : BaseRepository<ProjectTask, ProjectTaskFilter>, IProjectTaskRepositoryPort
{
    private readonly IUserIdentityPort _identityPort;

    public ProjectTaskRepositoryAdapter(AppDbContext context, IUserIdentityPort identityPort) : base(context)
    {
        _identityPort = identityPort;

    }

    public override async Task<ProjectTask?> GetByIdAsync(int id)
    {
        return await _context.ProjectTasks
            .Include(pt => pt.ProjectNavigation)
            .Include(pt => pt.TaskStatusNavigation)
            .FirstOrDefaultAsync(pt => pt.Id == id);
    }

    public override async Task<PaginatedResponse<ProjectTask>> Filter(Action<ProjectTaskFilter> filterAction)
    {
        int contibutor = await _identityPort.GetUserIdAsync();

        var filter = new ProjectTaskFilter();
        filterAction(filter);

        var query = _context.ProjectTasks.AsQueryable();


        query  = query.AsSplitQuery()
            .Include(e => e.ProjectNavigation)
            .ThenInclude(p => p.ContributionMembers.Where(c => c.Project == p.Id && c.Contributor == contibutor));

        query = query.Where(e => e.ProjectNavigation.ContributionMembers.Where(c => c.Contributor == contibutor).Count() != 0);

        if (filter.Id.HasValue)
            query = query.Where(pt => pt.Id == filter.Id.Value);

        if (filter.Project.HasValue)
            query = query.Where(pt => pt.Project == filter.Project.Value);

        if (filter.TaskStatus.HasValue)
            query = query.Where(pt => pt.TaskStatus == filter.TaskStatus.Value);

        if (filter.IncludeProject)
            query = query.Include(pt => pt.ProjectNavigation);

        if (filter.IncludeTaskStatus)
            query = query.Include(pt => pt.TaskStatusNavigation);

        // Apply ordering
        if (filter.OrderByIdDescending)
            query = query.OrderByDescending(pt => pt.Id);
        if (filter.OrderByIdAscending)
            query = query.OrderBy(pt => pt.Id);
        if (filter.OrderByProjectDescending)
            query = query.OrderByDescending(pt => pt.Project);
        if (filter.OrderByProjectAscending)
            query = query.OrderBy(pt => pt.Project);
        if (filter.OrderByTaskStatusDescending)
            query = query.OrderByDescending(pt => pt.TaskStatus);
        if (filter.OrderByTaskStatusAscending)
            query = query.OrderBy(pt => pt.TaskStatus);

        var totalCount = await query.CountAsync();

        if (filter.PaginatedRequest is not null)
        {
            query = query.Skip((filter.PaginatedRequest.PageNumber - 1) * filter.PaginatedRequest.PageSize)
                         .Take(filter.PaginatedRequest.PageSize);
        }
        else if (filter.Count.HasValue)
        {
            query = query.Take(filter.Count.Value);
        }

        var items = await query.ToListAsync();

        return new PaginatedResponse<ProjectTask>
        {
            TotalCount = totalCount,
            Items = items
        };
    }

    
}