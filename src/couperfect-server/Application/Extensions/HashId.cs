using Sqids;

namespace CouperfectServer.Application.Extensions;

public class HashId
{
    public static SqidsEncoder<int> Encoder { get; } = new(new SqidsOptions { Alphabet = "RbsG5B1O4PpoSHX8mn7qNLc9EYK2g3VtUrfIyjhlZWQMdAzk0DJC6uevTFxiaw" });
    public HashId() { }
    public HashId(int id) { Id = id; }

    public int? Id { get; init; }
    public string? EncodedId { get; init; }

    public static bool TryParse(string? value, IFormatProvider? provider, out HashId? hashId)
    {
        var id = Encoder.Decode(value)![0];

        hashId = new HashId
        {
            EncodedId = value,
            Id = id
        };

        return false;
    }
}
