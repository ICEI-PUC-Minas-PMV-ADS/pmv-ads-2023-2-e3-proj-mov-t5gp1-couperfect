using CouperfectServer.Application.Extensions.FluentResultsExtensions;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CouperfectServer.Application.Behaviours.ErrorHandling;

public class ErrorHandlingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : MediatR.IRequest<TResponse>
    where TResponse : ResultBase, new()
{
    private readonly ILogger<ErrorHandlingBehaviour<TRequest, TResponse>> logger;

    public ErrorHandlingBehaviour(ILogger<ErrorHandlingBehaviour<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
		try
		{
			return await next();
		}
		catch (Exception e)
		{
            logger.LogError(exception: e, "An unhandled exception has occured");

            return Result.Fail(new UnhandledError()).To<TResponse>();
		}
    }
}
