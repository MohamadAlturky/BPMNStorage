using ProjectsManagement.Core.Contributions;

namespace ProjectsManagement.Application.Users;
public interface IUserIdentityPort
{
    Task<int> GetUserIdAsync();
    Task<HashSet<ContributorInfo>> GetUsersAsync(HashSet<int> ids);
}