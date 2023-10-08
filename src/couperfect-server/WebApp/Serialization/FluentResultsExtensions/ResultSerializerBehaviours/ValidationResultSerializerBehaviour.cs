using CouperfectServer.Application.Behaviours.RequestValidation;
using FluentResults;

namespace CouperfectServer.WebApp.Serialization.FluentResultsExtensions.ResultSerializerBehaviours;

public class ValidationResultSerializerBehaviour : IResultSerializerBehaviour
{
    public IResultSerializerBehaviour.Action DetermineAction<TValue>(Result<TValue> result)
    {
        if (result.Errors.Any(e=>e is RequestValidationError))
            return IResultSerializerBehaviour.Action.SerializeToHttpResult;
        return IResultSerializerBehaviour.Action.Skip;
    }

    public IResultSerializerBehaviour.Action DetermineAction(Result result)
    {
        if (result.Errors.Any(e => e is RequestValidationError))
            return IResultSerializerBehaviour.Action.SerializeToHttpResult;
        return IResultSerializerBehaviour.Action.Skip;
    }

    public IResult SerializeToHttpResult<TValue>(Result<TValue> result)
    {
        var requestValidationError = result.Errors.FirstOrDefault(e => e is RequestValidationError) as RequestValidationError;

        return TypedResults.ValidationProblem(requestValidationError!.FieldReasonDictionary);
    }

    public IResult SerializeToHttpResult(Result result)
    {
        var requestValidationError = result.Errors.FirstOrDefault(e => e is RequestValidationError) as RequestValidationError;

        return TypedResults.ValidationProblem(requestValidationError!.FieldReasonDictionary);
    }
}
