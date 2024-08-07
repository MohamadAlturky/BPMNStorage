using ProjectsManagement.SharedKernel.Results;
using MediatR;
using ProjectsManagement.SharedKernel.AccessControl;

namespace ProjectsManagement.SharedKernel.CQRS;


public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    AccessControlCriteria Criteria();

}
