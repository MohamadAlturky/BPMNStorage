namespace ProjectsManagement.Application.Users;
public interface IUserIdentityPort
{
    Task<int> GetUserIdAsync();
}