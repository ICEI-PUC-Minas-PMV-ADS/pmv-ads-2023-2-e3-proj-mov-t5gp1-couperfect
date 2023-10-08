using FluentResults;

namespace CouperfectServer.Application.Behaviours.RequestValidation;

public class RequestValidationError : Error
{
    public required Dictionary<string, string[]> FieldReasonDictionary { get; set; }
}
