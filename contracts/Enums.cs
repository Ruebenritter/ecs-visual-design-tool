namespace Xtype.Contracts;

public enum Lifecycle
{
    Static,
    Dynamic,
    Transient,
}

public enum Cardinality
{
    One,
    Many,
    SingletonGlobal,
}

public enum Role
{
    Capability,
    State,
    Identity,
    Relationship,
}

public enum Mutability
{
    Mutable,
    ReadOnly,
    AppendOnly,
}

public enum VisualNodeType
{
    Unknown,
    Component,
    Entity,
    System,
}
