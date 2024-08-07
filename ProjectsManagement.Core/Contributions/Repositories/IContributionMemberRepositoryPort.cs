using ProjectsManagement.Core.Contributions;
using ProjectsManagement.SharedKernel.Ports.Storage;

namespace ProjectsManagement.Core.Projects.Repositories;

public interface IContributionMemberRepositoryPort
    : IBaseRepositoryPort<ContributionMember, ContributionMemberFilter>
{ }