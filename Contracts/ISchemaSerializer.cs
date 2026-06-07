using Xtype.Models;

namespace Xtype.Contracts;

public interface ISchemaSerializer
{
    Schema Load(string path);
    void Save(Schema schema, string path);
}
