using ProjectsManagement.Core.Contributions;
using ProjectsManagement.SharedKernel.CQRS;

namespace ProjectsManagement.Contracts.ProjectTasks.Commands.Update;


public class UpdateContributionMemberCommand : ICommand<ContributionMember>
{
    public int Id { get; set; }
    public int Project { get; set; }

    public int Contributor { get; set; }

    public int ContributionType { get; set; }

    public DateTime Date { get; set; }
}