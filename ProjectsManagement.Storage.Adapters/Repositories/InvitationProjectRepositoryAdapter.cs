using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Core.Invitations;
using ProjectsManagement.Core.Projects;
using ProjectsManagement.Core.Projects.Repositories;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.Storage.Adapters.Context;
using ProjectsManagement.Storage.Adapters.Repositories;

namespace ProjectsManagement.Infrastructure.Repositories;

public class InvitationRepositoryAdapter : BaseRepository<Invitation, InvitationFilter>, IInvitationRepositoryPort
{
    public InvitationRepositoryAdapter(AppDbContext context) : base(context)
    {
    }
    public override async Task<Invitation?> GetByIdAsync(int id)
    {
        return await _context.Invitations
            .Include(p => p.ProjectNavigation)
            .Include(p => p.InvitationStatusNavigation)
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();
    }

    public override async Task<PaginatedResponse<Invitation>> Filter(Action<InvitationFilter> filterAction)
    {
        var filter = new InvitationFilter();
        filterAction(filter);

        var query = _context.Invitations.AsQueryable();

        // Apply filters
        if (filter.Id.HasValue)
            query = query.Where(p => p.Id == filter.Id);


        if (filter.Project.HasValue)
            query = query.Where(p => p.Project== filter.Project);

        if (filter.Contributor.HasValue)
            query = query.Where(p => p.Contributor== filter.Contributor);
        
        if (filter.InvitationStatus.HasValue)
            query = query.Where(p => p.InvitationStatus== filter.InvitationStatus);


        // Date Equals
        if (filter.DateEquals.HasValue)
            query = query.Where(p => p.Date== filter.DateEquals);


        // Date Between
        if (filter.StartDate.HasValue && filter.EndDate.HasValue)
            query = query.Where(p => p.Date >= filter.StartDate && p.Date <= filter.EndDate);



        // Apply ordering
        if (filter.OrderByIdDescending)
            query = query.OrderByDescending(p => p.Id);

        if (filter.OrderByIdAscending)
            query = query.OrderBy(p => p.Id);

        
        if (filter.OrderByContributorDescending)
            query = query.OrderByDescending(p => p.Contributor);

        if (filter.OrderByContributorAscending)
            query = query.OrderBy(p => p.Contributor);


        if (filter.OrderByProjectDescending)
            query = query.OrderByDescending(p => p.Project);

        if (filter.OrderByProjectAscending)
            query = query.OrderBy(p => p.Project);


        if (filter.OrderByDateDescending)
            query = query.OrderByDescending(p => p.Date);

        if (filter.OrderByDateAscending)
            query = query.OrderBy(p => p.Date);


        if (filter.OrderByInvitationStatusDescending)
            query = query.OrderByDescending(p => p.InvitationStatus);

        if (filter.OrderByInvitationStatusAscending)
            query = query.OrderBy(p => p.InvitationStatus);




        // Apply includes
        if (filter.IncludeProject)
            query = query.Include(p => p.ProjectNavigation);

        if (filter.IncludeInvitationStatus)
            query = query.Include(p => p.InvitationStatusNavigation);


        // Apply pagination
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

        return new PaginatedResponse<Invitation>
        {
            TotalCount = totalCount,
            Items = items
        };
    }
}
    
    
