using ProjectsManagement.SharedKernel.Filters;
using ProjectsManagement.SharedKernel.Pagination;

namespace ProjectsManagement.Core.Projects;

public  class ProjectFilter : IFilter
{
    public int? Id { get; set; }
    public int? Count { get; set; }
    public PaginatedRequest? PaginatedRequest { get; set; }


    public int? ProjectType { get; set; }


    public bool OrderByIdDescending { get; set; } = false;
    public bool OrderByIdAscending { get; set; } = false;
    public bool OrderByProjectTypeDescending { get; set; } = false;
    public bool OrderByProjectTypeAscending { get; set; } = false;



    public bool IncludeActivities { get; set; } = false;
    public bool IncludeContributionMembers { get; set; } = false;
    public bool IncludeInvitations { get; set; } = false;
    public bool IncludeTasks { get; set; } = false;
    public bool IncludeProjectType { get; set; } = false;

}
