using ProjectsManagement.Core.Projects;
using ProjectsManagement.SharedKernel.Contracts.Entities;
namespace ProjectsManagement.Core.Contributions;

public class ContributionMember : EntityBase
{
    public int Project { get; set; }

    public int Contributor { get; set; }

    public int ContributionType { get; set; }

    public DateTime Date { get; set; }
    
    public virtual ContributionType ContributionTypeNavigation { get; set; } = null!;

    public virtual Project ProjectNavigation { get; set; } = null!;
}
