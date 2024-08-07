using MediatR;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.SharedKernel.CQRS;
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
            where TCommand : ICommand
{ }

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
            where TCommand : ICommand<TResponse>
{ }