using FluentResults;

namespace CouperfectServer.Application.Extensions.FluentResultsExtensions;

public interface IRequest<TResponse> : MediatR.IRequest<Result<TResponse>> { }
public interface IRequest : MediatR.IRequest<Result> { }

public interface IStreamRequest<TResponse> : MediatR.IStreamRequest<Result<TResponse>> { }
public interface IStreamRequest : MediatR.IStreamRequest<Result> { }

public interface IRequestHandler<TRequest, TResponse> : MediatR.IRequestHandler<TRequest, Result<TResponse>> where TRequest : IRequest<TResponse> { }
public interface IRequestHandler<TRequest> : MediatR.IRequestHandler<TRequest, Result> where TRequest : IRequest { }