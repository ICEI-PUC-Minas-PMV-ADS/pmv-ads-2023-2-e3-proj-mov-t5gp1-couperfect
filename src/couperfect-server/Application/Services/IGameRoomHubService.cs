namespace CouperfectServer.Application.Services;

public interface IGameRoomHubService
{
    Task JoinGroup(
        string connectionId,
        string groupId,
        CancellationToken cancellationToken = default
    );

    Task LeaveGroup(
        string connectionId,
        string groupId,
        CancellationToken cancellationToken = default
    );
}
