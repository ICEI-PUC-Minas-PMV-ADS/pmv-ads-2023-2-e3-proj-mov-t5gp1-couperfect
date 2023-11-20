using CouperfectServer.Domain.Extensions.Serialization;

namespace CouperfectServer.Application.UseCases.GameRooms;

public record GameRoomHubRequestBase : IJsonDerivedTypeBase { }
public record GameRoomHubRequest : GameRoomHubRequestBase, IRequest { }
public record GameRoomHubRequest<TResponse> : GameRoomHubRequestBase, IRequest<GameRoomHubResponse>;
public record GameRoomHubResponse : IJsonDerivedTypeBase { }
public interface IGameRoomRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, GameRoomHubResponse>
    where TRequest : GameRoomHubRequest<TResponse>
    where TResponse : GameRoomHubResponse
{ }

public interface IGameRoomRequestHandler<TRequest> : IRequestHandler<TRequest>
    where TRequest : GameRoomHubRequest
{ }