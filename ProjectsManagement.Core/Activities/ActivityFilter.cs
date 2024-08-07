using ProjectsManagement.SharedKernel.Filters;
using ProjectsManagement.SharedKernel.Pagination;
namespace ProjectsManagement.Core.Activities;

public class ActivityFilter : IFilter
{
    public int? Id { get; set; }
    public int? Count { get; set; }
    public PaginatedRequest? PaginatedRequest { get; set; }


    public int? Project { get; set; }
    public int? ActivityResourceType { get; set; }
    public int? ActivityType { get; set; }

    public DateTime? DateEquals { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public bool OrderByIdDescending { get; set; } = false;
    public bool OrderByIdAscending { get; set; } = false;

    public bool OrderByActivityTypeDescending { get; set; } = false;
    public bool OrderByActivityTypeAscending { get; set; } = false;

    public bool OrderByActivityResourceTypeDescending { get; set; } = false;
    public bool OrderByActivityResourceTypeAscending { get; set; } = false;

    public bool OrderByProjectDescending { get; set; } = false;
    public bool OrderByProjectAscending { get; set; } = false;


    public bool OrderByDateDescending { get; set; } = false;
    public bool OrderByDateAscending { get; set; } = false;


    public bool IncludeProject { get; set; } = false;
    public bool IncludeActivityType { get; set; } = false;
    public bool IncludeActivityResourceType { get; set; } = false;
    public bool IncludeActivityPrecedentPrecedent { get; set; } = false;

}
