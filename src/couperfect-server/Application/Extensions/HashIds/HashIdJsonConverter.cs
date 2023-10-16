using CouperfectServer.Domain.Extensions;
using Sqids;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CouperfectServer.Application.Extensions.HashIds;

public class HashIdJsonConverter : JsonConverter<HashId>, ISingleton<HashIdJsonConverter>
{
    public static HashIdJsonConverter Value => new(HashId.Encoder);

    private readonly SqidsEncoder<int> sqidsEncoder;
    private HashIdJsonConverter(SqidsEncoder<int> sqidsEncoder)
    {
        this.sqidsEncoder = sqidsEncoder;
    }

    public override HashId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var hashString = reader.GetString()!;
        var id = sqidsEncoder.Decode(hashString)[0];

        return new HashId
        {
            EncodedId = hashString,
            Id = id
        };
    }

    public override void Write(Utf8JsonWriter writer, HashId value, JsonSerializerOptions options)
    {
        var hashedString = sqidsEncoder.Encode(value.Id!.Value);
        writer.WriteStringValue(hashedString);
    }
}
