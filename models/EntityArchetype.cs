using System.Collections.Generic;

namespace Xtype.Models
{
    public class EntityArchetype
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Component> PermanentComponents { get; set; }
        public List<Component> TemporaryComponents { get; set; }
    }
}
