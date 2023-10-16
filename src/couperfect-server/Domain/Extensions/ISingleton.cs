namespace CouperfectServer.Domain.Extensions;

public interface ISingleton<TValue>
{
    static abstract TValue Value { get; }
}
