using CouperfectServer.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using CouperfectServer.Application.Extensions.Caching;

namespace CouperfectServer.Application.Services;

public interface IGameRoomService
{
    Task<Dictionary<Guid, GameRoomListing>> GetOpenRooms(CancellationToken cancellationToken = default);
    Task<GameRoom?> GetRoomFromCache(Guid guid, CancellationToken cancellationToken = default);
    Task<Guid> SaveFreshRoom(GameRoom room, CancellationToken cancellationToken = default);
    Task Update(GameRoom freshRoom, CancellationToken cancellationToken = default);
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

    public async Task<Guid> SaveFreshRoom(GameRoom room, CancellationToken cancellationToken = default)
    {
        await SetRoomInCache(room, cancellationToken);

        var openRooms = await GetOpenRooms(cancellationToken);
        openRooms.Add(room.Guid, new GameRoomListing() { CurrentPlayers = room.Players.Count, RoomName = room.Name });
        await distributedCache.SetAsync(OpenRoomsCacheKey, openRooms, cancellationToken: cancellationToken);

        return room.Guid;
    }

    public async Task Update(GameRoom freshRoom, CancellationToken cancellationToken = default)
    {
        var openRooms = await GetOpenRooms(cancellationToken);

        if (openRooms.TryGetValue(freshRoom.Guid, out var listing))
        {
            listing.RoomName = freshRoom.Name;
            listing.CurrentPlayers = freshRoom.Players.Count;
            if (freshRoom.Players.Count < 1)
                openRooms.Remove(freshRoom.Guid);
            await distributedCache.SetAsync(OpenRoomsCacheKey, openRooms, cancellationToken: cancellationToken);
        }

        await SetRoomInCache(freshRoom, cancellationToken);
    }

    public Task<Dictionary<Guid, GameRoomListing>> GetOpenRooms(CancellationToken cancellationToken = default)
    {
        return distributedCache.GetOrSetAsync(
            OpenRoomsCacheKey,
            entry => new Dictionary<Guid, GameRoomListing>(),
            cancellationToken: cancellationToken
        )!;
    }

    public Task<GameRoom?> GetRoomFromCache(Guid guid, CancellationToken cancellationToken = default)
    {
        return distributedCache.GetAsync<GameRoom?>(guid.ToString(), cancellationToken);
    }

    private Task SetRoomInCache(GameRoom freshRoom, CancellationToken cancellationToken = default)
    {
        if(freshRoom.Players.Count < 1)
            return distributedCache.RemoveAsync(freshRoom.Guid.ToString(), cancellationToken);
        return distributedCache.SetAsync(freshRoom.Guid.ToString(), freshRoom, cancellationToken: cancellationToken);
    }
}
