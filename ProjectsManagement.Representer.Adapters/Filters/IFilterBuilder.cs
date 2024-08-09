using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.Representer.Adapters.Actions;

public interface IFilterBuilder<T, TOut>
{
    Action<T> BuildFilter(T filter);
    PaginatedResponse<TOut> BuildResponse(PaginatedResponse<TOut> result);
}
