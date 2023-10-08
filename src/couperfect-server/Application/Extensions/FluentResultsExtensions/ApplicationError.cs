using FluentResults;

namespace CouperfectServer.Application.Extensions.FluentResultsExtensions;

public class ApplicationError : Error 
{
	public ApplicationError(string message) : base(message)
	{
			
	}
}
