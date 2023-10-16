using CouperfectServer.Application.Contracts.CouperfectDb;
using CouperfectServer.Application.Services;
using CouperfectServer.Domain.Entities;
using CouperfectServer.Domain.Extensions.Serialization;
using FluentResults;

namespace CouperfectServer.Application.UseCases.GameRooms.Join;

public record JoinGameRoomHubRequest(
    Guid? RoomGuid = default,
    JoinGameRoomHubRequest.CreationOptionsClass? CreationOptions = default
) : GameRoomHubRequest, IJsonDerivedType<GameRoomHubRequest>, IRequestInfo
{
    public static string Discriminator => nameof(JoinGameRoomHubRequest);
    public IRequestInfo.Info RequestInfo { get; set; } = default!;

    public record CreationOptionsClass(string RoomName = "Public lobby");
}

public class JoinGameRoomRequestHandler : IRequestHandler<JoinGameRoomHubRequest>
{
    private readonly ICouperfectDbUnitOfWork dbUnitOfWork;
    private readonly IGameRoomService gameRoomService;

    public JoinGameRoomRequestHandler(ICouperfectDbUnitOfWork dbUnitOfWork, IGameRoomService gameRoomService)
    {
        this.dbUnitOfWork = dbUnitOfWork;
        this.gameRoomService = gameRoomService;
    }

    public async Task<Result> Handle(JoinGameRoomHubRequest request, CancellationToken cancellationToken)
    {
        if (request.RequestInfo.RequesterId is null)
            return Result.Fail("Requester not found");

        var requester = await dbUnitOfWork.PlayerRepository.Find(request.RequestInfo.RequesterId!.Value, cancellationToken);

        if (requester is null)
            return Result.Fail("Requester not found");

        var creationOptions = request.CreationOptions ?? (request.RoomGuid is null ? new JoinGameRoomHubRequest.CreationOptionsClass() : null);

        if (creationOptions is not null)
        {
            var newRoom = new GameRoom
            {
                AdminId = requester.Id,
                Name = creationOptions.RoomName,
                Players = new HashSet<(int playerId, string connectionId)> { (requester.Id, request.RequestInfo.HubConnectionId!) }
            };

            await gameRoomService.ListNewRoom(newRoom, cancellationToken);
        }
        else
        {
            var room = await gameRoomService.GetRoomFromCache(request.RoomGuid!.Value, cancellationToken);

            if (room is null)
                return Result.Fail("Room not found");

            // TODO: Adicionar verificacao de banimento
            room.Players.Add((requester.Id, request.RequestInfo.HubConnectionId!));
            await gameRoomService.UpdateListing(room, cancellationToken);
        }

        return Result.Ok();
    }
}
