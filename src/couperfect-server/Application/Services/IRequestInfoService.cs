namespace CouperfectServer.Application.Services;

public interface IRequestInfo
{
    public record Info(
        int? RequesterId, 
        string? HubConnectionId, 
        DateTime RecievedAt
    );

    Info RequestInfo { get; set; }
}

public interface IRequestInfoService
{
    Task FillRequestInfo(IRequestInfo requestInfo, CancellationToken cancellationToken = default);
}
