using CouperfectServer.Application.UseCases.Players.SingIn;
using CouperfectServer.Application.UseCases.Players.SingUp;
using CouperfectServer.WebApp.Serialization.FluentResultsExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CouperfectServer.WebApp;

public static class Endpoints
{
    public static void MapEndpoints(WebApplication app)
    {
        app.MapPost(
            "/api/players",
            Endpoint<PlayerSignUpRequest, PlayerSignUpResponse>()
        )
        .Produces<PlayerSignUpResponse>(200)
        .Produces<ValidationProblemDetails>(400)
        .Produces<ProblemDetails>(403)
        .Produces<ProblemDetails>(500)
        .AllowAnonymous()
        .WithName("PlayerSignUp")
        .WithOpenApi();

        app.MapPost(
            "/api/players/singin",
            Endpoint<PlayerSignInRequest, PlayerSignInResponse>()
        )
        .Produces<PlayerSignInResponse>(200)
        .Produces<ValidationProblemDetails>(400)
        .Produces<ProblemDetails>(403)
        .Produces<ProblemDetails>(500)
        .AllowAnonymous()
        .WithName("PlayerSignIn")
        .WithOpenApi();
    }

    private static Func<TRequest, IServiceScopeFactory, CancellationToken, Task<IResult>> Endpoint<TRequest, TValue>()
        where TRequest : Application.Extensions.FluentResultsExtensions.IRequest<TValue>
    {
        return async ([FromBody] TRequest request, [FromServices] IServiceScopeFactory scopeFactory, CancellationToken cancellationToken) =>
        {
            using var scope = scopeFactory.CreateScope();
            var sender = scope.ServiceProvider.GetRequiredService<ISender>();
            var behaviours = scope.ServiceProvider.GetServices<IResultSerializerBehaviour>();
            var resultResponse = await sender.Send(request, cancellationToken);
            var selectedBehaviour = behaviours.DetermineBehaviour<TRequest, TValue>(resultResponse);
            return selectedBehaviour.SerializeToHttpResult(resultResponse);
        };
    }
}
