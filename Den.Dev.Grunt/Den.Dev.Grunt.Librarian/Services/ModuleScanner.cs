// <copyright file="ModuleScanner.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Den.Dev.Grunt.Librarian.Services
{
    /// <summary>
    /// Service for scanning module source files to extract implemented method names.
    /// </summary>
    public class ModuleScanner
    {
        // Regex pattern to match public async method declarations
        // Matches: public async Task<...> MethodName(
        private static readonly Regex MethodPattern = new(
            @"public\s+async\s+Task<.+>\s+(\w+)\s*\(",
            RegexOptions.Compiled);

        // Pattern to extract module name from class declaration
        // Matches: class StatsModule : ModuleBase
        private static readonly Regex ClassPattern = new(
            @"class\s+(\w+)Module\s*:",
            RegexOptions.Compiled);

        /// <summary>
        /// Scans module files in the specified directory and returns implemented method names grouped by module.
        /// </summary>
        /// <param name="modulesPath">Path to the directory containing module .cs files.</param>
        /// <returns>Dictionary mapping module names to sets of implemented method names.</returns>
        public Dictionary<string, HashSet<string>> ScanModules(string modulesPath)
        {
            var result = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

            if (!Directory.Exists(modulesPath))
            {
                Console.WriteLine($"Warning: Modules path does not exist: {modulesPath}");
                return result;
            }

            var moduleFiles = Directory.GetFiles(modulesPath, "*Module.cs", SearchOption.TopDirectoryOnly);

            foreach (var file in moduleFiles)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);

                // Skip base classes and generated files
                if (fileName.Equals("ModuleBase", StringComparison.OrdinalIgnoreCase) ||
                    fileName.EndsWith(".Generated", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                try
                {
                    var content = File.ReadAllText(file);
                    var moduleName = ExtractModuleName(content, fileName);
                    var methods = ExtractMethodNames(content);

                    if (!string.IsNullOrEmpty(moduleName) && methods.Count > 0)
                    {
                        if (!result.ContainsKey(moduleName))
                        {
                            result[moduleName] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        }

                        foreach (var method in methods)
                        {
                            result[moduleName].Add(method);
                        }
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Warning: Failed to read module file {file}: {ex.Message}");
                }
            }

            return result;
        }

        /// <summary>
        /// Extracts the module name from class declaration or file name.
        /// </summary>
        private static string ExtractModuleName(string content, string fileName)
        {
            var match = ClassPattern.Match(content);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            // Fallback: derive from file name (e.g., "StatsModule" -> "Stats")
            if (fileName.EndsWith("Module", StringComparison.OrdinalIgnoreCase))
            {
                return fileName.Substring(0, fileName.Length - "Module".Length);
            }

            return fileName;
        }

        /// <summary>
        /// Extracts all public async method names from the source content.
        /// </summary>
        private static HashSet<string> ExtractMethodNames(string content)
        {
            var methods = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (Match match in MethodPattern.Matches(content))
            {
                methods.Add(match.Groups[1].Value);
            }

            return methods;
        }

        /// <summary>
        /// Checks if a specific method name exists in the scanned modules.
        /// </summary>
        /// <param name="scannedModules">The dictionary of scanned modules from ScanModules.</param>
        /// <param name="moduleName">The module name to check.</param>
        /// <param name="methodName">The method name to check.</param>
        /// <returns>True if the method is implemented in the module.</returns>
        public static bool HasImplementation(
            Dictionary<string, HashSet<string>> scannedModules,
            string moduleName,
            string methodName)
        {
            if (scannedModules.TryGetValue(moduleName, out var methods))
            {
                return methods.Contains(methodName);
            }

            return false;
        }
    }
}
