using System.Collections.Generic;

namespace Den.Dev.Grunt.Zeta.Models
{
    public class ModuleMetadata
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public object Instance { get; set; } = null!;
        public List<MethodMetadata> Methods { get; set; } = new();

        public override string ToString() => $"{DisplayName} ({Methods.Count} methods)";
    }
}
