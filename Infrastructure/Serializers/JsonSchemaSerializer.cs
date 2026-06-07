using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xtype.Contracts;
using Xtype.Models;

namespace Xtype.Infrastructure.Serializers;

public class JsonSchemaSerializer : ISchemaSerializer
{
    private static readonly JsonSerializerSettings Settings = new()
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore,
    };

    public async Task<Schema> LoadAsync(string path)
    {
        string json = await File.ReadAllTextAsync(path);
        return JsonConvert.DeserializeObject<Schema>(json, Settings)
            ?? throw new InvalidDataException($"Failed to deserialize schema from '{path}'.");
    }

    public async Task SaveAsync(Schema schema, string path)
    {
        string json = JsonConvert.SerializeObject(schema, Settings);
        await File.WriteAllTextAsync(path, json);
    }
}
