using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.SharedKernel.Filters;

public interface IFilter
{
    int? Id { get; set; }
    int? Count { get; set; }
    PaginatedRequest? PaginatedRequest { get; set; }
}
