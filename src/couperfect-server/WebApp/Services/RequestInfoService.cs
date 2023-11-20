using CouperfectServer.Application.Services;
using CouperfectServer.Domain.Services;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace CouperfectServer.WebApp.Services;

public class RequestInfoService : IRequestInfoService
{
    public IRequestInfo.Info? RequestInfo { get; private set; }
    private HubCallerContext? _hubCallerContext = default;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IDateTimeService dateTimeService;

    public RequestInfoService(IHttpContextAccessor httpContextAccessor, IDateTimeService dateTimeService)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.dateTimeService = dateTimeService;
    }

    public void SetRoomGuid(Guid roomGuid)
    {
        if (RequestInfo is null)
            RequestInfo = new IRequestInfo.Info(null, null, roomGuid, dateTimeService.Now);
        else
            RequestInfo = new IRequestInfo.Info(RequestInfo.RequesterId, RequestInfo.HubConnectionId, roomGuid, dateTimeService.Now);
    }

    public void SetSignalRConnectionInfo(HubCallerContext? callerContext)
    {
        _hubCallerContext = callerContext;
    }

    public Task<IRequestInfo.Info> SetRequestInfo(CancellationToken cancellationToken = default)
    {
        if (RequestInfo is not null)
            return Task.FromResult(RequestInfo);

        var userClaims = _hubCallerContext is null ? httpContextAccessor.HttpContext?.User : _hubCallerContext.User;
        var requesterIdClaim = userClaims?.FindFirst(ClaimTypes.NameIdentifier);
        int? requesterId = requesterIdClaim?.Value is null ? null : int.Parse(requesterIdClaim.Value);

        var roomGuidClaim = userClaims?.FindFirst(ClaimTypes.GroupSid);

        Guid? roomGuid = default;

        if (Guid.TryParse(roomGuidClaim?.Value, out var parsedGuid))
            roomGuid = parsedGuid;

        var info = new IRequestInfo.Info(requesterId, _hubCallerContext?.ConnectionId, roomGuid, dateTimeService.Now);
        RequestInfo = info;

        return Task.FromResult(RequestInfo);
    }
}
