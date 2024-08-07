using ProjectsManagement.Core.Contributions;
using ProjectsManagement.SharedKernel.CQRS;

namespace ProjectsManagement.Contracts.ProjectTasks.Queries.GetById;

public class GetContributionMemberByIdQuery : IQuery<ContributionMember>
{
    public int Id { get; set; }
}
