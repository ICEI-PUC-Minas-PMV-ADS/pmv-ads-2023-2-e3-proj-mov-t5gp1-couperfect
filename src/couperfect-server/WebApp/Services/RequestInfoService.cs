using CouperfectServer.Application.Services;
using CouperfectServer.Domain.Services;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace CouperfectServer.WebApp.Services;

public class RequestInfoService : IRequestInfoService
{
    private ClaimsPrincipal? _user = default;
    private HubCallerContext? _hubCallerContext = default;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IDateTimeService dateTimeService;

    public RequestInfoService(IHttpContextAccessor httpContextAccessor, IDateTimeService dateTimeService)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.dateTimeService = dateTimeService;
    }

    public void SetSignalRConnectionInfo(HubCallerContext? callerContext)
    {
        _hubCallerContext = callerContext;
    }

    public Task FillRequestInfo(IRequestInfo requestInfo, CancellationToken cancellationToken = default)
    {
        _user ??= _hubCallerContext is null ? httpContextAccessor.HttpContext?.User : _hubCallerContext.User;

        var requesterIdClaim = _user?.FindFirst(ClaimTypes.NameIdentifier);
        int? requesterId = requesterIdClaim?.Value is null ? null : int.Parse(requesterIdClaim.Value);
        requestInfo.RequestInfo ??= new IRequestInfo.Info(requesterId, _hubCallerContext?.ConnectionId, dateTimeService.Now);

        return Task.CompletedTask;
    }
}
