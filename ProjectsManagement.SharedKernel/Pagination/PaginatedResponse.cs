namespace ProjectsManagement.SharedKernel.Pagination;

public class PaginatedResponse<T>
{
    public int TotalCount { get; set; }
    public List<T> Items { get; set; } = new List<T>();
}
