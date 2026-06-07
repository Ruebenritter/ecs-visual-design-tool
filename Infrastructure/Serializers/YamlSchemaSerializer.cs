using System;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Xtype.Contracts;
using Xtype.Models;

namespace Xtype.Infrastructure.Serializers;

public class YamlSchemaSerializer : ISchemaSerializer
{
    private static readonly ISerializer Serializer = new SerializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();

    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .Build();

    public async Task<Schema> LoadAsync(string path)
    {
        string yaml = await File.ReadAllTextAsync(path);
        return Deserializer.Deserialize<Schema>(yaml)
            ?? throw new InvalidDataException($"Failed to deserialize schema from '{path}'.");
    }

    public async Task SaveAsync(Schema schema, string path)
    {
        string yaml = Serializer.Serialize(schema);
        await File.WriteAllTextAsync(path, yaml);
    }
}
