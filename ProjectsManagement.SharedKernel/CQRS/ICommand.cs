using MediatR;
using ProjectsManagement.SharedKernel.Results;

namespace ProjectsManagement.SharedKernel.CQRS;


public interface ICommand : IRequest<Result> { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }