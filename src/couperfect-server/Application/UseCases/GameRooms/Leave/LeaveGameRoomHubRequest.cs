using CouperfectServer.Application.Contracts.CouperfectDb;
using CouperfectServer.Application.Services;
using CouperfectServer.Domain.Extensions.Serialization;
using FluentResults;

namespace CouperfectServer.Application.UseCases.GameRooms.Leave;

public record LeaveGameRoomHubRequest : GameRoomHubRequest<LeaveGameRoomHubResponse>, IJsonDerivedType<GameRoomHubRequestBase>, IRequestInfo
{
    public static string Discriminator => nameof(LeaveGameRoomHubRequest);

    public IRequestInfo.Info RequestInfo { get; set; } = default!;
}

public record LeaveGameRoomHubResponse(string NewToken) : GameRoomHubResponse, IJsonDerivedType<GameRoomHubResponse>
{
    public static string Discriminator => nameof(LeaveGameRoomHubResponse);
}

public class LeaveGameRoomHubRequestHandler : IGameRoomRequestHandler<LeaveGameRoomHubRequest, LeaveGameRoomHubResponse>
{
    private readonly ICouperfectDbUnitOfWork dbUnitOfWork;
    private readonly IGameRoomHubService gameRoomHubService;
    private readonly IGameRoomService gameRoomService;
    private readonly ITokenService tokenService;

    public LeaveGameRoomHubRequestHandler(
        ICouperfectDbUnitOfWork dbUnitOfWork,
        IGameRoomHubService gameRoomHubService,
        IGameRoomService gameRoomService,
        ITokenService tokenService
    )
    {
        this.dbUnitOfWork = dbUnitOfWork;
        this.gameRoomHubService = gameRoomHubService;
        this.gameRoomService = gameRoomService;
        this.tokenService = tokenService;
    }

    public async Task<Result<GameRoomHubResponse>> Handle(LeaveGameRoomHubRequest request, CancellationToken cancellationToken)
    {
        if (request.RequestInfo.RequesterId is null)
            return Result.Fail("Requester not found");

        var requester = await dbUnitOfWork.PlayerRepository.Find(request.RequestInfo.RequesterId!.Value, cancellationToken);

        if (requester is null)
            return Result.Fail("Requester not found");

        var room = await gameRoomService.GetRoomFromCache(requester.CurrentRoom!.Value, cancellationToken);

        await gameRoomHubService.LeaveGroup(request.RequestInfo!.HubConnectionId!, requester.CurrentRoom!.ToString()!, cancellationToken);

        requester.CurrentRoom = null;
        dbUnitOfWork.PlayerRepository.Update(requester);
        await dbUnitOfWork.SaveChangesAsync(cancellationToken);

        if (room is null)
            return Result.Fail("Room not found");

        room.Players.RemoveWhere(p => p.PlayerId == requester.Id);
        await gameRoomService.Update(room, cancellationToken);

        var newToken = tokenService.GetToken(requester);

        return new LeaveGameRoomHubResponse(newToken);
    }
}
