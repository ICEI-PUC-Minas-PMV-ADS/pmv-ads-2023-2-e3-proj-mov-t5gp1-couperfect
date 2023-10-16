using CouperfectServer.Domain.Extensions.Serialization;

namespace CouperfectServer.Application.UseCases.GameRooms;

public record GameRoomHubRequest : IRequest, IJsonDerivedTypeBase { }
