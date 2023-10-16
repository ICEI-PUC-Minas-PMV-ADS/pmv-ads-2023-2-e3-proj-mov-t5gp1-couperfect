using CouperfectServer.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using CouperfectServer.Application.Extensions.Caching;

namespace CouperfectServer.Application.Services;

public interface IGameRoomService
{
    Task<Dictionary<Guid, GameRoomListing>> GetOpenRooms(CancellationToken cancellationToken = default);
    Task<GameRoom?> GetRoomFromCache(Guid guid, CancellationToken cancellationToken = default);
    Task<Guid> ListNewRoom(GameRoom room, CancellationToken cancellationToken = default);
    Task UpdateListing(GameRoom room, CancellationToken cancellationToken = default);
}

public struct GameRoomListing
{
    public string RoomName { get; set; }
    public int CurrentPlayers { get; set; }
}

public class GameRoomService : IGameRoomService
{
    private const string OpenRoomsCacheKey = "openRooms";
    private readonly IDistributedCache distributedCache;

    public GameRoomService(IDistributedCache distributedCache)
    {
        this.distributedCache = distributedCache;
    }

    public Task<GameRoom?> GetRoomFromCache(Guid guid, CancellationToken cancellationToken = default)
    {
        return distributedCache.GetAsync<GameRoom?>(guid.ToString(), cancellationToken);
    }

    public async Task<Guid> ListNewRoom(GameRoom room, CancellationToken cancellationToken = default)
    {
        await distributedCache.SetAsync(room.Guid.ToString(), room, cancellationToken: cancellationToken);

        var openRooms = await GetOpenRooms(cancellationToken);
        openRooms.Add(room.Guid, new GameRoomListing() { CurrentPlayers = room.Players.Count, RoomName = room.Name });
        await distributedCache.SetAsync(OpenRoomsCacheKey, openRooms, cancellationToken: cancellationToken);

        return room.Guid;
    }

    public async Task UpdateListing(GameRoom room, CancellationToken cancellationToken = default)
    {
        var openRooms = await GetOpenRooms(cancellationToken);
        if (openRooms.TryGetValue(room.Guid, out var listing))
        {
            listing.CurrentPlayers++;
            await distributedCache.SetAsync(OpenRoomsCacheKey, openRooms, cancellationToken: cancellationToken);
        }
    }

    public Task<Dictionary<Guid, GameRoomListing>> GetOpenRooms(CancellationToken cancellationToken = default)
    {
        return distributedCache.GetOrSetAsync(
            OpenRoomsCacheKey,
            entry => new Dictionary<Guid, GameRoomListing>(),
            cancellationToken: cancellationToken
        )!;
    }
}
