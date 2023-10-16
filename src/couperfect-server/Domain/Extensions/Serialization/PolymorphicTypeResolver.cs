using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace CouperfectServer.Domain.Extensions.Serialization;

public class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver, ISingleton<PolymorphicTypeResolver>
{
    public static PolymorphicTypeResolver Value => new();
    private PolymorphicTypeResolver() { }

    public static Dictionary<Type, ICollection<(Type childType, string discriminator)>> PolyphormicJsonDerivedTypes { get; set; } = new();

    public class PolymorphicMapConfiguration<TBase> where TBase : IJsonDerivedTypeBase
    {
        public ICollection<(Type childType, string discriminator)> ChildDerivedTypes { get; set; } = new HashSet<(Type childType, string discriminator)>();
        public PolymorphicMapConfiguration<TBase> AddChild<TChild>()

            where TChild : IJsonDerivedType<TBase>
        {
            ChildDerivedTypes.Add((typeof(TChild), TChild.Discriminator));
            return this;
        }
    }

    public static void MapJson<TBase>(Action<PolymorphicMapConfiguration<TBase>> configAction)
        where TBase : IJsonDerivedTypeBase
    {
        var tbaseType = typeof(TBase);

        PolyphormicJsonDerivedTypes.TryAdd(tbaseType, new HashSet<(Type childType, string discriminator)>());
        var polymorphicMapConfiguration = new PolymorphicMapConfiguration<TBase>();
        configAction(polymorphicMapConfiguration);
        PolyphormicJsonDerivedTypes[tbaseType] = polymorphicMapConfiguration.ChildDerivedTypes;
    }

    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        var jsonTypeInfo = base.GetTypeInfo(type, options);

        if (PolyphormicJsonDerivedTypes.TryGetValue(jsonTypeInfo.Type, out var jsonDerivedChilds))
        {
            jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
            {
                IgnoreUnrecognizedTypeDiscriminators = false,
                UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
            };

            foreach (var (childType, discriminator) in jsonDerivedChilds)
                jsonTypeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(childType, discriminator));
        }

        return jsonTypeInfo;
    }
}