using CouperfectServer.Application.Services;
using CouperfectServer.Domain.Extensions;
using FluentResults;

namespace CouperfectServer.Application.UseCases.GameRooms.Query;

// TODO: Adicionar filtragens como apenas salas abertas ou nomes
public class QueryGameRoomsRequest : IQueryRequest<QueryGameRoomsResponse>, ISingleton<QueryGameRoomsRequest>
{
    private QueryGameRoomsRequest() { }
    public static QueryGameRoomsRequest Value => new();
}

public record QueryGameRoomsResponse(Guid RoomGuid, string RoomName, int CurrentPlayers);

public class QueryGameRoomsRequestHandler : MediatR.IRequestHandler<QueryGameRoomsRequest, Result<IEnumerable<QueryGameRoomsResponse>>>
{
    private readonly IGameRoomService gameRoomService;

    public QueryGameRoomsRequestHandler(IGameRoomService gameRoomService)
    {
        this.gameRoomService = gameRoomService;
    }

    public async Task<Result<IEnumerable<QueryGameRoomsResponse>>> Handle(QueryGameRoomsRequest request, CancellationToken cancellationToken)
    {
        var openRooms = await gameRoomService.GetOpenRooms(cancellationToken);
        return openRooms.Select(o => new QueryGameRoomsResponse(o.Key, o.Value.RoomName, o.Value.CurrentPlayers)).ToResult();
    }
}
