using ProjectsManagement.SharedKernel.Contracts.Entities;

namespace ProjectsManagement.Core.Contributions;

public partial class ContributionType : EntityBase
{

    public string Name { get; set; } = null!;

    public virtual ICollection<ContributionMember> ContributionMembers { get; set; }
}
