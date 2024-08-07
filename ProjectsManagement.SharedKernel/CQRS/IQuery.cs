using ProjectsManagement.SharedKernel.Results;
using MediatR;

namespace ProjectsManagement.SharedKernel.CQRS;


public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
