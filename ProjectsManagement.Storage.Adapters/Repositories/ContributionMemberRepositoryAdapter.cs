using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Core.Contributions;
using ProjectsManagement.Core.Projects.Repositories;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.Storage.Adapters.Context;
using ProjectsManagement.Storage.Adapters.Repositories;

namespace ProjectsManagement.Infrastructure.Repositories
{
    public class ContributionMemberRepositoryAdapter : BaseRepository<ContributionMember, ContributionMemberFilter>, IContributionMemberRepositoryPort
    {
        public ContributionMemberRepositoryAdapter(AppDbContext context) : base(context)
        {
        }

        public override async Task<ContributionMember?> GetByIdAsync(int id)
        {
            return await _context.ContributionMembers
                .Include(p => p.ProjectNavigation)
                .Include(p => p.ContributionTypeNavigation)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public override async Task<PaginatedResponse<ContributionMember>> Filter(Action<ContributionMemberFilter> filterAction)
        {
            var filter = new ContributionMemberFilter();
            filterAction(filter);

            var query = _context.ContributionMembers.AsQueryable();

            // Apply filters
            if (filter.Id.HasValue)
                query = query.Where(p => p.Id == filter.Id);

            if (filter.Project.HasValue)
                query = query.Where(p => p.Project == filter.Project);

            if (filter.Contributor.HasValue)
                query = query.Where(p => p.Contributor == filter.Contributor);

            if (filter.ContributionType.HasValue)
                query = query.Where(p => p.ContributionType == filter.ContributionType);

            // Date Equals
            if (filter.DateEquals.HasValue)
                query = query.Where(p => p.Date == filter.DateEquals);

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

            if (filter.OrderByContributionTypeDescending)
                query = query.OrderByDescending(p => p.ContributionType);

            if (filter.OrderByContributionTypeAscending)
                query = query.OrderBy(p => p.ContributionType);

            // Apply includes
            if (filter.IncludeProject)
                query = query.Include(p => p.ProjectNavigation);

            if (filter.IncludeContributionType)
                query = query.Include(p => p.ContributionTypeNavigation);

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

            return new PaginatedResponse<ContributionMember>
            {
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}