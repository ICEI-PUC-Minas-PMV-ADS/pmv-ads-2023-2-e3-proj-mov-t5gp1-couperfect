using FluentResults;

namespace CouperfectServer.Application.Extensions.FluentResultsExtensions;

public class UnhandledError : Error
{
    public UnhandledError() : base("An unknow error has occured")
    {

    }
}
