using CouperfectServer.Application.Extensions.FluentResultsExtensions;
using FluentResults;

namespace CouperfectServer.WebApp.Serialization.FluentResultsExtensions;

public interface IResultSerializerBehaviour
{
    Action DetermineAction<TValue>(Result<TValue> result);
    Action DetermineAction(Result result);
    IResult SerializeToHttpResult<TValue>(Result<TValue> result);
    IResult SerializeToHttpResult(Result result);

    public enum Action
    {
        Skip,
        SerializeToHttpResult
    }
}

public static class ResultSerializerBehavioursExtensions
{
    public static IResultSerializerBehaviour DetermineBehaviour<TRequest>(this IEnumerable<IResultSerializerBehaviour> behaviours, Result resultResponse)
        where TRequest : IRequest
    {
        foreach (var behaviour in behaviours)
        {
            if (behaviour.DetermineAction(resultResponse) is IResultSerializerBehaviour.Action.SerializeToHttpResult)
                return behaviour;
        }

        throw new InvalidOperationException("Could not find any behaviour that would serialize the response");
    }

    public static IResultSerializerBehaviour DetermineBehaviour<TRequest, TValue>(this IEnumerable<IResultSerializerBehaviour> behaviours, Result<TValue> resultResponse)
        where TRequest : IRequest<TValue>
    {
        foreach (var behaviour in behaviours)
        {
            if (behaviour.DetermineAction(resultResponse) is IResultSerializerBehaviour.Action.SerializeToHttpResult)
                return behaviour;
        }

        throw new InvalidOperationException("Could not find any behaviour that would serialize the response");
    }
}
