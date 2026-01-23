using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Den.Dev.Grunt.Core;
using Den.Dev.Grunt.Zeta.Models;

namespace Den.Dev.Grunt.Zeta.Registry
{
    public class ApiMethodRegistry
    {
        private readonly List<ModuleMetadata> _haloModules = new();
        private readonly List<ModuleMetadata> _waypointModules = new();

        public IReadOnlyList<ModuleMetadata> HaloModules => _haloModules;
        public IReadOnlyList<ModuleMetadata> WaypointModules => _waypointModules;

        public void DiscoverMethods(HaloInfiniteClient haloClient, WaypointClient waypointClient)
        {
            _haloModules.Clear();
            _waypointModules.Clear();

            DiscoverHaloModules(haloClient);
            DiscoverWaypointMethods(waypointClient);
        }

        private void DiscoverHaloModules(HaloInfiniteClient client)
        {
            var moduleProperties = typeof(HaloInfiniteClient)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType.Name.EndsWith("Module"));

            foreach (var prop in moduleProperties)
            {
                var moduleInstance = prop.GetValue(client);
                if (moduleInstance == null) continue;

                var module = new ModuleMetadata
                {
                    Name = prop.Name,
                    DisplayName = FormatModuleName(prop.Name),
                    Instance = moduleInstance
                };

                DiscoverModuleMethods(module, moduleInstance.GetType());
                if (module.Methods.Count > 0)
                {
                    _haloModules.Add(module);
                }
            }

            // Add the GetApiSettingsContainer method from the client itself
            var clientModule = new ModuleMetadata
            {
                Name = "Utility",
                DisplayName = "Utility",
                Instance = client
            };

            var settingsMethod = typeof(HaloInfiniteClient).GetMethod("GetApiSettingsContainer");
            if (settingsMethod != null)
            {
                clientModule.Methods.Add(CreateMethodMetadata(settingsMethod));
            }

            if (clientModule.Methods.Count > 0)
            {
                _haloModules.Add(clientModule);
            }

            _haloModules.Sort((a, b) => string.Compare(a.DisplayName, b.DisplayName, StringComparison.Ordinal));
        }

        private void DiscoverWaypointMethods(WaypointClient client)
        {
            var waypointModule = new ModuleMetadata
            {
                Name = "Waypoint",
                DisplayName = "Waypoint",
                Instance = client
            };

            DiscoverModuleMethods(waypointModule, typeof(WaypointClient));
            if (waypointModule.Methods.Count > 0)
            {
                _waypointModules.Add(waypointModule);
            }
        }

        private void DiscoverModuleMethods(ModuleMetadata module, Type moduleType)
        {
            var methods = moduleType
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(m => !m.IsSpecialName && m.ReturnType.IsGenericType &&
                            m.ReturnType.GetGenericTypeDefinition() == typeof(Task<>));

            foreach (var method in methods)
            {
                module.Methods.Add(CreateMethodMetadata(method));
            }

            module.Methods.Sort((a, b) => string.Compare(a.DisplayName, b.DisplayName, StringComparison.Ordinal));
        }

        private MethodMetadata CreateMethodMetadata(MethodInfo method)
        {
            return new MethodMetadata
            {
                Name = method.Name,
                DisplayName = FormatMethodName(method.Name),
                Method = method,
                Parameters = method.GetParameters(),
                ReturnType = method.ReturnType
            };
        }

        private static string FormatModuleName(string name)
        {
            // Remove "Module" suffix if present
            if (name.EndsWith("Module"))
            {
                name = name.Substring(0, name.Length - 6);
            }
            return AddSpacesToPascalCase(name);
        }

        private static string FormatMethodName(string name)
        {
            return AddSpacesToPascalCase(name);
        }

        private static string AddSpacesToPascalCase(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            return Regex.Replace(text, "([a-z])([A-Z])", "$1 $2");
        }
    }
}
