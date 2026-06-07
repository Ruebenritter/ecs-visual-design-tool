using System;
using System.IO;
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

    public Schema Load(string path)
    {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<Schema>(json, Settings)
            ?? throw new InvalidDataException($"Failed to deserialize schema from '{path}'.");
    }

    public void Save(Schema schema, string path)
    {
        string json = JsonConvert.SerializeObject(schema, Settings);
        File.WriteAllText(path, json);
    }
}
