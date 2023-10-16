using CouperfectServer.WebApp.Serialization.FluentResultsExtensions;
using CouperfectServer.WebApp.Serialization.FluentResultsExtensions.ResultSerializerBehaviours;

namespace CouperfectServer.WebApp.Serialization;

public static class DependencyInjection
{
    public static IServiceCollection AddCouperfectSerialization(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureJsonOptions>();

        services.AddSingleton<ISSEResultSerializer, SSEResultSerializer>();
        services.AddSingleton<IResultSerializerBehaviour, SuccessResultSerializerBehaviour>();
        services.AddSingleton<IResultSerializerBehaviour, ValidationResultSerializerBehaviour>();
        services.AddSingleton<IResultSerializerBehaviour, UnhandledResultSerializerBehaviour>();
        services.AddSingleton<IResultSerializerBehaviour, ApplicationErrorResultSerializerBehaviour>();

        return services;
    }
}
