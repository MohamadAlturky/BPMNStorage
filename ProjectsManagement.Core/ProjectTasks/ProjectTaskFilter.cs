using ProjectsManagement.SharedKernel.Filters;
using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.Core.ProjectTasks;
public class ProjectTaskFilter : IFilter
{
    public int? Id { get; set; }
    public int? Count { get; set; }
    public PaginatedRequest? PaginatedRequest { get; set; }

    
    public int? TaskStatus { get; set; }
    public int? Project { get; set; }


    public bool OrderByIdDescending { get; set; } = false;
    public bool OrderByIdAscending { get; set; } = false;
    public bool OrderByProjectDescending { get; set; } = false;
    public bool OrderByProjectAscending { get; set; } = false;
    public bool OrderByTaskStatusDescending { get; set; } = false;
    public bool OrderByTaskStatusAscending { get; set; } = false;

    public bool IncludeProject { get; set; } = false;
    public bool IncludeTaskStatus { get; set; } = false;
}
