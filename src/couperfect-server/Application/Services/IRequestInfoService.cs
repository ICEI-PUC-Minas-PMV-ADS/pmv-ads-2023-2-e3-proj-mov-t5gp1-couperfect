namespace CouperfectServer.Application.Services;

public interface IRequestInfo
{
    public record Info(
        int? RequesterId,
        string? HubConnectionId,
        Guid? RoomId,
        DateTime RecievedAt
    );

    Info RequestInfo { get; set; }
}

public interface IRequestInfoService
{
    IRequestInfo.Info? RequestInfo { get; }
    void SetRoomGuid(Guid roomGuid);
    Task<IRequestInfo.Info> SetRequestInfo(CancellationToken cancellationToken = default);
}
