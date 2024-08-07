using MediatR;
using ProjectsManagement.SharedKernel.AccessControl;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.SharedKernel.CQRS;


public interface ICommand : IRequest<Result> 
{
    AccessControlCriteria Criteria();
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>> 
{
    AccessControlCriteria Criteria();
}