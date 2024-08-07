using Microsoft.EntityFrameworkCore;
using ProjectsManagement.SharedKernel.Pagination;
using ProjectsManagement.SharedKernel.Ports.Storage;
using ProjectsManagement.Storage.Adapters.Context;

namespace ProjectsManagement.Storage.Adapters.Repositories;

public abstract class BaseRepository<TEntity, TFilter> : IBaseRepositoryPort<TEntity, TFilter>
    where TEntity : class
    where TFilter : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public abstract Task<PaginatedResponse<TEntity>> Filter(Action<TFilter> filter);

    protected IQueryable<TEntity> ApplyPagination(IQueryable<TEntity> query, PaginatedRequest? request, int? count)
    {
        if (request != null)
        {
            query = query.Skip((request.PageNumber - 1) * request.PageSize)
                         .Take(request.PageSize);
        }
        else if (count.HasValue)
        {
            query = query.Take(count.Value);
        }

        return query;
    }
}