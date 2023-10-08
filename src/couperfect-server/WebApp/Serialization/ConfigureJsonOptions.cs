using CouperfectServer.Application.Extensions;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using Sqids;

namespace CouperfectServer.WebApp.Serialization;

public class ConfigureJsonOptions : IConfigureOptions<JsonOptions>
{
    private readonly SqidsEncoder<int> sqidsEncoder;

    public ConfigureJsonOptions(SqidsEncoder<int> sqidsEncoder)
    {
        this.sqidsEncoder = sqidsEncoder;
    }

    public void Configure(JsonOptions options)
    {
        options.SerializerOptions.Converters.Add(new HashIdJsonConverter(sqidsEncoder));
    }
}