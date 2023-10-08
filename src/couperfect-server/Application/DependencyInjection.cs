using CouperfectServer.Application.Behaviours.ErrorHandling;
using CouperfectServer.Application.Behaviours.RequestValidation;
using CouperfectServer.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CouperfectServer.Application;

public static class DependencyInjection
{
    private static readonly Type _thisType = typeof(DependencyInjection);

    public static IServiceCollection AddCouperfectApp(this IServiceCollection services)
    {
        services.AddSingleton<ICryptographyService, CryptographyService>();

        services.AddValidatorsFromAssemblyContaining(_thisType);

        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining(_thisType)
            .AddOpenBehavior(typeof(ErrorHandlingBehaviour<,>))
            .AddOpenBehavior(typeof(RequestValidationBehaviour<,>))
        );

        return services;
    }
}
