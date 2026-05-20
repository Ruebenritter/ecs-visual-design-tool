using System.Collections.Generic;

namespace Xtype.Models
{
    // The root file for new ECS designs.
    public class Schema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<EntityArchetype> Archetypes { get; set; }
        public List<Component> Components { get; set; }

        // Save visual data for the editor here, so that it can be loaded without parsing the entire file.
        // List<ComponentViewModel> ComponentViews { get; set; }
        // List<EntityArchetypeViewModel> ArchetypeViews { get; set; }
        // ViewModels should contain visual node data like position, color coding etc.
        // Relations can be computed from the archetypes and components.
    }
}
