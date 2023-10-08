using FluentResults;
using System.Text.Json;

namespace CouperfectServer.WebApp.Serialization.FluentResultsExtensions;

public class SSEResultSerializer : ISSEResultSerializer
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public SSEResultSerializer(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task SendSSEStream(IAsyncEnumerable<Result> responseResultStream, CancellationToken cancellationToken = default)
    {
        httpContextAccessor.HttpContext!.Response.Headers.Add("Content-Type", "text/event-stream");
        httpContextAccessor.HttpContext!.Response.Headers.Add("Cache-Control", "no-cache");
        httpContextAccessor.HttpContext!.Response.Headers.Add("Connection", "keep-alive");

        await foreach (var responseResult in responseResultStream)
        {
            var jsonResult = JsonSerializer.Serialize(responseResult);
            await httpContextAccessor.HttpContext!.Response.WriteAsync($"data: {jsonResult}\r\r", cancellationToken: cancellationToken);
            await httpContextAccessor.HttpContext!.Response.Body.FlushAsync(cancellationToken);
        }
    }

    public async Task SendSSEStream<TValue>(IAsyncEnumerable<Result<TValue>> responseResultStream, CancellationToken cancellationToken = default)
    {
        httpContextAccessor.HttpContext!.Response.Headers.Add("Content-Type", "text/event-stream");
        httpContextAccessor.HttpContext!.Response.Headers.Add("Cache-Control", "no-cache");
        httpContextAccessor.HttpContext!.Response.Headers.Add("Connection", "keep-alive");

        await foreach (var responseResult in responseResultStream)
        {
            var jsonResult = JsonSerializer.Serialize(responseResult);
            await httpContextAccessor.HttpContext!.Response.WriteAsync($"data: {jsonResult}\r\r", cancellationToken: cancellationToken);
            await httpContextAccessor.HttpContext!.Response.Body.FlushAsync(cancellationToken);
        }
    }
}

public interface ISSEResultSerializer
{
    Task SendSSEStream(IAsyncEnumerable<Result> responseResultStream, CancellationToken cancellationToken = default);
    Task SendSSEStream<TValue>(IAsyncEnumerable<Result<TValue>> responseResultStream, CancellationToken cancellationToken = default);
}