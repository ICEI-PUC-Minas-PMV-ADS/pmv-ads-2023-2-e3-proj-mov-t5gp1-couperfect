using FluentResults;

namespace CouperfectServer.WebApp.Serialization.FluentResultsExtensions.ResultSerializerBehaviours;

public class SuccessResultSerializerBehaviour : IResultSerializerBehaviour
{
    public IResultSerializerBehaviour.Action DetermineAction<TValue>(Result<TValue> result)
    {
        if (result is { IsSuccess: true })
            return IResultSerializerBehaviour.Action.SerializeToHttpResult;
        return IResultSerializerBehaviour.Action.Skip;
    }

    public IResultSerializerBehaviour.Action DetermineAction(Result result)
    {
        if (result is { IsSuccess: true })
            return IResultSerializerBehaviour.Action.SerializeToHttpResult;
        return IResultSerializerBehaviour.Action.Skip;
    }

    public IResult SerializeToHttpResult<TValue>(Result<TValue> result)
    {
        return TypedResults.Ok(result.Value);
    }

    public IResult SerializeToHttpResult(Result result)
    {
        return TypedResults.NoContent();
    }
}
