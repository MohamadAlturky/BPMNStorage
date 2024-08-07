using MediatR;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.SharedKernel.CQRS;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
{ }