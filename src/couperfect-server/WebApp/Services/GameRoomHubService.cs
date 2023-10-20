using CouperfectServer.Application.Services;
using Microsoft.AspNetCore.SignalR;

namespace CouperfectServer.WebApp.Services;

public class GameRoomHubService : IGameRoomHubService
{
    private readonly IHubContext<GameRoomHub, GameRoomHub.IGameRoomHubClient> hubContext;

    public GameRoomHubService(IHubContext<GameRoomHub, GameRoomHub.IGameRoomHubClient> hubContext)
    {
        this.hubContext = hubContext;
    }

    public Task JoinGroup(string connectionId, string groupId, CancellationToken cancellationToken = default)
    {
        return hubContext.Groups.AddToGroupAsync(connectionId, groupId, cancellationToken);
    }

    public Task LeaveGroup(string connectionId, string groupId, CancellationToken cancellationToken = default)
    {
        return hubContext.Groups.RemoveFromGroupAsync(connectionId, groupId, cancellationToken);
    }
}
