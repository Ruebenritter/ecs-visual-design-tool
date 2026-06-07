using System;
using System.Collections.Generic;
using System.IO;
using Xtype.Contracts;
using Xtype.Infrastructure.Serializers;
using Xtype.Models;

namespace Xtype.Infrastructure;

public class DataRepository
{
    private readonly Dictionary<string, ISchemaSerializer> _serializers;

    public DataRepository()
    {
        var json = new JsonSchemaSerializer();
        var yaml = new YamlSchemaSerializer();

        _serializers = new Dictionary<string, ISchemaSerializer>(StringComparer.OrdinalIgnoreCase)
        {
            [".json"] = json,
            [".yaml"] = yaml,
            [".yml"]  = yaml,
        };
    }

    public Schema Load(string path)
    {
        var serializer = ResolveSerializer(path);
        return serializer.Load(path);
    }

    public void Save(Schema schema, string path)
    {
        var serializer = ResolveSerializer(path);
        serializer.Save(schema, path);
    }

    private ISchemaSerializer ResolveSerializer(string path)
    {
        string ext = Path.GetExtension(path);

        if (!_serializers.TryGetValue(ext, out var serializer))
            throw new NotSupportedException(
                $"No serializer registered for file extension '{ext}'. Supported: {string.Join(", ", _serializers.Keys)}.");

        return serializer;
    }
}
