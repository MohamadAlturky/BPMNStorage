using ProjectsManagement.Core.Contributions;
using ProjectsManagement.SharedKernel.CQRS;

namespace ProjectsManagement.Contracts.Contributions.Commands.Create;

public class CreateContributionMemberCommand : ICommand<ContributionMember>
{
    public int Project { get; set; }

    public int Contributor { get; set; }

    public int ContributionType { get; set; }

    public DateTime Date { get; set; }
}