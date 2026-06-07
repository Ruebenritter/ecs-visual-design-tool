using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xtype.Contracts;
using Xtype.Infrastructure.Serializers;
using Xtype.Models;

namespace Xtype.Infrastructure;

public sealed class DataRepository
{
    // Singleton
    private static readonly Lazy<DataRepository> _instance =
        new(() => new DataRepository(), LazyThreadSafetyMode.ExecutionAndPublication);

    public static DataRepository Instance => _instance.Value;

    // Busy guard — allows only one concurrent operation
    private readonly SemaphoreSlim _lock = new(1, 1);

    private readonly Dictionary<string, ISchemaSerializer> _serializers;

    private DataRepository()
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

    public async Task<OperationResult<Schema>> LoadAsync(string path)
    {
        if (!_lock.Wait(0))
            return OperationResult<Schema>.Failure("A file operation is already in progress.");

        try
        {
            var serializer = ResolveSerializer(path);
            if (serializer is null)
                return OperationResult<Schema>.Failure(
                    $"Unsupported file extension '{Path.GetExtension(path)}'. Supported: {string.Join(", ", _serializers.Keys)}.");

            var schema = await serializer.LoadAsync(path);
            return OperationResult<Schema>.Success(schema);
        }
        catch (Exception ex)
        {
            return OperationResult<Schema>.Failure(ex.Message, (System.Exception)ex);
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task<OperationResult> SaveAsync(Schema schema, string path)
    {
        if (!_lock.Wait(0))
            return OperationResult.Failure("A file operation is already in progress.");

        try
        {
            var serializer = ResolveSerializer(path);
            if (serializer is null)
                return OperationResult.Failure(
                    $"Unsupported file extension '{Path.GetExtension(path)}'. Supported: {string.Join(", ", _serializers.Keys)}.");

            await serializer.SaveAsync(schema, path);
            return OperationResult.Success();
        }
        catch (Exception ex)
        {
            return OperationResult.Failure(ex.Message, (System.Exception)ex);
        }
        finally
        {
            _lock.Release();
        }
    }

    private ISchemaSerializer? ResolveSerializer(string path)
    {
        string ext = Path.GetExtension(path);
        _serializers.TryGetValue(ext, out var serializer);
        return serializer;
    }
}
