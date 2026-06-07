using System.Threading.Tasks;
using Xtype.Models;

namespace Xtype.Contracts;

public interface ISchemaSerializer
{
    Task<Schema> LoadAsync(string path);
    Task SaveAsync(Schema schema, string path);
}
