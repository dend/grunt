using System;
using System.Reflection;

namespace Den.Dev.Grunt.Zeta.Models
{
    public class MethodMetadata
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public MethodInfo Method { get; set; } = null!;
        public ParameterInfo[] Parameters { get; set; } = Array.Empty<ParameterInfo>();
        public Type ReturnType { get; set; } = typeof(void);

        public override string ToString() => DisplayName;
    }
}
