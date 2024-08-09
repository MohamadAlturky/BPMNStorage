using ProjectsManagement.SharedKernel.AccessControl;

namespace ProjectsManagement.Application.AccessControl;

public interface IAccessController
{
    Task<bool> HasAccess(AccessControlCriteria criteria);
}
