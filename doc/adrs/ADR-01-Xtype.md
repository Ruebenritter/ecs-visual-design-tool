Architecture Decision Record 1: The Xtpye tool

# Context

Current game project relies on data oriented design principles and uses a Enitity Component System.
During the design phase i want an overview of all needed components and entities that use them.
The overview should be detailed enough to catch component role issues early before implementation.
A poison component might serve different roles on a character or a weapon.
An overview of all needed entities should help complete the needed components list.

There seems to be no existing tool covering this need.
Tool development needs to be a justified decision between project delay and tool usefulness.

# Decision

Create a lightweight design tool for archetypes (a fixed subset of components linked to an entity that allows human readable categorization: this is a character, weapon, ingredient etc) that combines the data layer of lists and sheets with a user-centered visual editing layer as Figma, Canva and similar tools already offer for user interfaces.
Simple queries should show filtered lists and a graph view should offer two ways for users to get a good understanding of the general scope and relationships of all planned components and archetypes (and maybe their states).
Data layer should be serializable into common human readable structured file formats like json/yaml.
Components are described by facets to allow categorization in multiple useful buckets similar to PRMAT for books.

# Consequences

Stick with Godot C# as familiar framework for a tool prototype instead of starting with new constraints like Rust/Tauri or similar. It already offers graph nodes that might support the graph view. git cli support should be possible for vcs of tool output (schemata).
