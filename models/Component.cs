using Xtype.Contracts;

namespace Xtype.Models;

public class Component
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Version { get; set; }

    // Component Factes
    public Lifecycle Lifecycle { get; set; }
    public Cardinality Cardinality { get; set; }
    public Role Role { get; set; }
    public Mutability Mutability { get; set; }
}
