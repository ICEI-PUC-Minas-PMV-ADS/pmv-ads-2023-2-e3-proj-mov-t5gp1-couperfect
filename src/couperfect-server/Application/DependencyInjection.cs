using CouperfectServer.Application.Behaviours.ErrorHandling;
using CouperfectServer.Application.Behaviours.RequesterInfo;
using CouperfectServer.Application.Behaviours.RequestValidation;
using CouperfectServer.Application.Services;
using CouperfectServer.Application.UseCases.GameRooms;
using CouperfectServer.Application.UseCases.GameRooms.Join;
using CouperfectServer.Application.UseCases.GameRooms.Leave;
using CouperfectServer.Domain.Extensions.Serialization;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CouperfectServer.Application;

public static class DependencyInjection
{
    static DependencyInjection()
    {
        Result.Setup(setup =>
        {
            setup.ErrorFactory = (message) => new ApplicationError(message);
        });

        // TODO: Search for json maps in assembly
        PolymorphicTypeResolver.MapJson<GameRoomHubRequestBase>(cfg => cfg.AddChild<LeaveGameRoomHubRequest>().AddChild<JoinGameRoomHubRequest>());
        PolymorphicTypeResolver.MapJson<GameRoomHubResponse>(cfg => cfg.AddChild<LeaveGameRoomHubResponse>().AddChild<JoinGameRoomHubResponse>());
    }

    private static readonly Type _thisType = typeof(DependencyInjection);

    public static IServiceCollection AddCouperfectApp(this IServiceCollection services)
    {
        services.AddSingleton<ICryptographyService, CryptographyService>();
        services.AddSingleton<IGameRoomService, GameRoomService>();

        services.AddValidatorsFromAssemblyContaining(_thisType);

        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining(_thisType)
            .AddOpenBehavior(typeof(ErrorHandlingBehaviour<,>))
            .AddOpenBehavior(typeof(RequestInfoBehaviour<,>))
            .AddOpenBehavior(typeof(RequestValidationBehaviour<,>))
        );

        return services;
    }
}
