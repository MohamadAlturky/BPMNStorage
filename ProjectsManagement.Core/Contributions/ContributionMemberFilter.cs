using ProjectsManagement.SharedKernel.Filters;
using ProjectsManagement.SharedKernel.Pagination;
namespace ProjectsManagement.Core.Contributions;

public class ContributionMemberFilter : IFilter
{
    public int? Id { get; set; }
    public int? Count { get; set; }
    public PaginatedRequest? PaginatedRequest { get; set; }


    public int? Project { get; set; }
    public int? Contributor { get; set; }
    public int? ContributionType { get; set; }

    public DateTime? DateEquals { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public bool OrderByIdDescending { get; set; } = false;
    public bool OrderByIdAscending { get; set; } = false;

    public bool OrderByContributorDescending { get; set; } = false;
    public bool OrderByContributorAscending { get; set; } = false;

    public bool OrderByContributionTypeDescending { get; set; } = false;
    public bool OrderByContributionTypeAscending { get; set; } = false;

    public bool OrderByProjectDescending { get; set; } = false;
    public bool OrderByProjectAscending { get; set; } = false;


    public bool OrderByDateDescending { get; set; } = false;
    public bool OrderByDateAscending { get; set; } = false;


    public bool IncludeProject { get; set; } = false;
    public bool IncludeContributionType { get; set; } = false;

}
