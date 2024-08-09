using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Application.Users;
using ProjectsManagement.Core.Activities;
using ProjectsManagement.Core.Activities.Repositories;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.Storage.Adapters.Context;
using ProjectsManagement.Storage.Adapters.Repositories;

namespace ProjectsManagement.Infrastructure.Repositories
{
    public class ActivityRepositoryAdapter : BaseRepository<Activity, ActivityFilter>, IActivityRepositoryPort
    {
        private readonly IUserIdentityPort _identityPort;
        public ActivityRepositoryAdapter(AppDbContext context, IUserIdentityPort identityPort) : base(context)
        {
            _identityPort = identityPort;
        }

        public override async Task<Activity?> GetByIdAsync(int id)
        {
            return await _context.Activities
                .Include(a => a.ProjectNavigation)
                .Include(a => a.ActivityTypeNavigation)
                .Include(a => a.ActivityResourceTypeNavigation)
                .Include(a => a.ActivityPrecedentPrecedentNavigations)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public override async Task<PaginatedResponse<Activity>> Filter(Action<ActivityFilter> filterAction)
        {
            int contibutor = await _identityPort.GetUserIdAsync();

            var filter = new ActivityFilter();
            filterAction(filter);

            var query = _context.Activities.AsQueryable();


            query  = query.AsSplitQuery()
                .Include(e => e.ProjectNavigation)
                .ThenInclude(p => p.ContributionMembers.Where(c => c.Project == p.Id && c.Contributor == contibutor));

            query = query.Where(e =>e.ProjectNavigation.ContributionMembers.Where(c=>c.Contributor == contibutor).Count() != 0);

            // Apply filters
            if (filter.Id.HasValue)
                query = query.Where(a => a.Id == filter.Id);

            if (filter.Project.HasValue)
                query = query.Where(a => a.Project == filter.Project);

            if (filter.ActivityResourceType.HasValue)
                query = query.Where(a => a.ActivityResourceType == filter.ActivityResourceType);

            if (filter.ActivityType.HasValue)
                query = query.Where(a => a.ActivityType == filter.ActivityType);

            // Date Equals
            if (filter.DateEquals.HasValue)
                query = query.Where(a => a.Date == filter.DateEquals);

            // Date Between
            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
                query = query.Where(a => a.Date >= filter.StartDate && a.Date <= filter.EndDate);

            // Apply ordering
            if (filter.OrderByIdDescending)
                query = query.OrderByDescending(a => a.Id);

            if (filter.OrderByIdAscending)
                query = query.OrderBy(a => a.Id);

            if (filter.OrderByActivityTypeDescending)
                query = query.OrderByDescending(a => a.ActivityType);

            if (filter.OrderByActivityTypeAscending)
                query = query.OrderBy(a => a.ActivityType);

            if (filter.OrderByActivityResourceTypeDescending)
                query = query.OrderByDescending(a => a.ActivityResourceType);

            if (filter.OrderByActivityResourceTypeAscending)
                query = query.OrderBy(a => a.ActivityResourceType);

            if (filter.OrderByProjectDescending)
                query = query.OrderByDescending(a => a.Project);

            if (filter.OrderByProjectAscending)
                query = query.OrderBy(a => a.Project);

            if (filter.OrderByDateDescending)
                query = query.OrderByDescending(a => a.Date);

            if (filter.OrderByDateAscending)
                query = query.OrderBy(a => a.Date);

            // Apply includes
            if (filter.IncludeProject)
                query = query.Include(a => a.ProjectNavigation);

            if (filter.IncludeActivityType)
                query = query.Include(a => a.ActivityTypeNavigation);

            if (filter.IncludeActivityResourceType)
                query = query.Include(a => a.ActivityResourceTypeNavigation);

            if (filter.IncludeActivityPrecedentPrecedent)
                query = query.Include(a => a.ActivityPrecedentPrecedentNavigations);

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

            return new PaginatedResponse<Activity>
            {
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}