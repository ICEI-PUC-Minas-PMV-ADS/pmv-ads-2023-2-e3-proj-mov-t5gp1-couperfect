using CouperfectServer.Application.Services;
using FluentResults;
using MediatR;

namespace CouperfectServer.Application.Behaviours.RequesterInfo;

class RequestInfoBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : MediatR.IRequest<TResponse>, IRequestInfo
    where TResponse : ResultBase, new()
{
    private readonly IRequestInfoService requestInfoService;

    public RequestInfoBehaviour(IRequestInfoService requestInfoService)
    {
        this.requestInfoService = requestInfoService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await requestInfoService.FillRequestInfo(request, cancellationToken);
        return await next();
    }
}
