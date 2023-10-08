using CouperfectServer.Application.Extensions.FluentResultsExtensions;
using FluentResults;

namespace CouperfectServer.WebApp.Serialization.FluentResultsExtensions.ResultSerializerBehaviours;

public class UnhandledResultSerializerBehaviour : IResultSerializerBehaviour
{
    public IResultSerializerBehaviour.Action DetermineAction<TValue>(Result<TValue> result)
    {
        if (result.Errors.Any(e => e is UnhandledError))
            return IResultSerializerBehaviour.Action.SerializeToHttpResult;
        return IResultSerializerBehaviour.Action.Skip;
    }

    public IResultSerializerBehaviour.Action DetermineAction(Result result)
    {
        if (result.Errors.Any(e => e is UnhandledError))
            return IResultSerializerBehaviour.Action.SerializeToHttpResult;
        return IResultSerializerBehaviour.Action.Skip;
    }

    public IResult SerializeToHttpResult<TValue>(Result<TValue> result)
    {
        var error = result.Errors.FirstOrDefault(e => e is UnhandledError) as UnhandledError;

        return TypedResults.Problem(error!.Message);
    }

    public IResult SerializeToHttpResult(Result result)
    {
        var error = result.Errors.FirstOrDefault(e => e is UnhandledError) as UnhandledError;

        return TypedResults.Problem(error!.Message);
    }
}
