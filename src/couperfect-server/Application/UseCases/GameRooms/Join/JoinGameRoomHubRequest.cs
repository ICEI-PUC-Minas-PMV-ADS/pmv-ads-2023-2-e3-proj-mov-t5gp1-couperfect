using CouperfectServer.Application.Contracts.CouperfectDb;
using CouperfectServer.Application.Services;
using CouperfectServer.Domain.Entities;
using CouperfectServer.Domain.Extensions.Serialization;
using FluentResults;

namespace CouperfectServer.Application.UseCases.GameRooms.Join;

public record JoinGameRoomHubRequest(
    Guid? RoomGuid = default,
    JoinGameRoomHubRequest.CreationOptionsClass? CreationOptions = default
) : GameRoomHubRequest<JoinGameRoomHubResponse>, IJsonDerivedType<GameRoomHubRequestBase>, IRequestInfo
{
    public static string Discriminator => nameof(JoinGameRoomHubRequest);
    public IRequestInfo.Info RequestInfo { get; set; } = default!;

    public record CreationOptionsClass(string RoomName = "Public lobby");
}

public record JoinGameRoomHubResponse(string NewToken) : GameRoomHubResponse, IJsonDerivedType<GameRoomHubResponse>
{
    public static string Discriminator => nameof(JoinGameRoomHubResponse);
}

public class JoinGameRoomRequestHandler : IGameRoomRequestHandler<JoinGameRoomHubRequest, JoinGameRoomHubResponse>
{
    private readonly ICouperfectDbUnitOfWork dbUnitOfWork;
    private readonly IGameRoomService gameRoomService;
    private readonly IGameRoomHubService gameRoomHubService;
    private readonly ITokenService tokenService;
    private readonly IRequestInfoService requestInfoService;

    public JoinGameRoomRequestHandler(
        ICouperfectDbUnitOfWork dbUnitOfWork,
        IGameRoomService gameRoomService,
        IGameRoomHubService gameRoomHubService,
        ITokenService tokenService,
        IRequestInfoService requestInfoService
    )
    {
        this.dbUnitOfWork = dbUnitOfWork;
        this.gameRoomService = gameRoomService;
        this.gameRoomHubService = gameRoomHubService;
        this.tokenService = tokenService;
        this.requestInfoService = requestInfoService;
    }

    public async Task<Result<GameRoomHubResponse>> Handle(JoinGameRoomHubRequest request, CancellationToken cancellationToken)
    {
        if (request.RequestInfo.RequesterId is null)
            return Result.Fail("Requester not found");

        var requester = await dbUnitOfWork.PlayerRepository.Find(request.RequestInfo.RequesterId!.Value, cancellationToken);

        if (requester is null)
            return Result.Fail("Requester not found");

        //if (requester.CurrentRoom is not null)
        //    return Result.Fail("Requester already on a room");

        var creationOptions = request.CreationOptions ?? (request.RoomGuid is null ? new JoinGameRoomHubRequest.CreationOptionsClass() : null);

        Guid? roomGuid = request.RoomGuid;

        if (creationOptions is not null && roomGuid is null)
        {
            roomGuid = await CreateNewRoom(request, requester, creationOptions, cancellationToken);
        }
        else
        {
            var room = await gameRoomService.GetRoomFromCache(request.RoomGuid!.Value, cancellationToken);

            if (room is null)
                return Result.Fail("Room not found");

            // TODO: Adicionar verificacao de banimento e se o player ja entrou na sala
            room.Players.Add(new GameRoom.PlayerInfo(requester.Id, request.RequestInfo.HubConnectionId!));
            await gameRoomService.Update(room, cancellationToken);
        }

        requester.CurrentRoom = roomGuid;
        dbUnitOfWork.PlayerRepository.Update(requester);
        await dbUnitOfWork.SaveChangesAsync(cancellationToken);

        var roomGuidStr = roomGuid!.Value.ToString()!;

        requestInfoService.SetRoomGuid(roomGuid!.Value);

        await gameRoomHubService.JoinGroup(
            request.RequestInfo.HubConnectionId!,
            roomGuidStr,
            cancellationToken
        );

        var newToken = tokenService.GetToken(requester);

        return new JoinGameRoomHubResponse(newToken);
    }

    private async Task<Guid?> CreateNewRoom(JoinGameRoomHubRequest request, Player requester, JoinGameRoomHubRequest.CreationOptionsClass creationOptions, CancellationToken cancellationToken)
    {
        var newRoom = new GameRoom
        {
            AdminId = requester.Id,
            Name = creationOptions.RoomName,
            Players = new HashSet<GameRoom.PlayerInfo> { new GameRoom.PlayerInfo(requester.Id, request.RequestInfo.HubConnectionId!) }
        };

        await gameRoomService.SaveFreshRoom(newRoom, cancellationToken);

        return newRoom.Guid;
    }
}
