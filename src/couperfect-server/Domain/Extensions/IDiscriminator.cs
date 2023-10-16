namespace CouperfectServer.Domain.Extensions;

public interface IDiscriminator
{
    static abstract string Discriminator { get; }
}