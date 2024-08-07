using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.SharedKernel.Ports.Storage;

public interface IBaseRepositoryPort<TEntity, TFilter> where TEntity : class where TFilter : class
{
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(int id);
    Task<TEntity?> GetByIdAsync(int id);
    Task<PaginatedResponse<TEntity>> Filter(Action<TFilter> filterAction);
}