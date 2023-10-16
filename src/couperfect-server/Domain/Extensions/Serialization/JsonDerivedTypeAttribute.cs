using System.Text.Json.Serialization;

namespace CouperfectServer.Domain.Extensions.Serialization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
public sealed class JsonDerivedTypeAttribute<T> : JsonDerivedTypeAttribute where T : IDiscriminator
{
    public JsonDerivedTypeAttribute() : base(typeof(T), T.Discriminator) { }
}
