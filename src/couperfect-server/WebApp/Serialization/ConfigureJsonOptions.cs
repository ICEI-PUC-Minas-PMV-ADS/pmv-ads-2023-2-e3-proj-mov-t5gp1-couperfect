using CouperfectServer.Application.Extensions.HashIds;
using CouperfectServer.Domain.Extensions.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;

namespace CouperfectServer.WebApp.Serialization;

public class ConfigureJsonOptions : IConfigureOptions<JsonOptions>
{
    public void Configure(JsonOptions options)
    {
        options.SerializerOptions.TypeInfoResolver = PolymorphicTypeResolver.Value;
        options.SerializerOptions.Converters.Add(HashIdJsonConverter.Value);
    }
}