// <copyright file="HttpMethodInferrer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Den.Dev.Grunt.Librarian.Services
{
    /// <summary>
    /// Service for inferring HTTP methods from endpoint names.
    /// </summary>
    public static class HttpMethodInferrer
    {
        /// <summary>
        /// Prefix patterns that indicate specific HTTP methods.
        /// Order matters - more specific patterns should come first.
        /// </summary>
        private static readonly List<(string Prefix, HttpMethod Method)> PrefixRules = new()
        {
            ("Get", HttpMethod.Get),
            ("Post", HttpMethod.Post),
            ("Put", HttpMethod.Put),
            ("Delete", HttpMethod.Delete),
            ("Patch", HttpMethod.Patch),
            ("Upload", HttpMethod.Post),
            ("Create", HttpMethod.Post),
            ("Update", HttpMethod.Put),
            ("Set", HttpMethod.Put),
            ("Add", HttpMethod.Post),
            ("Remove", HttpMethod.Delete),
            ("Submit", HttpMethod.Post),
            ("Execute", HttpMethod.Post),
            ("Trigger", HttpMethod.Post),
            ("Start", HttpMethod.Post),
            ("Stop", HttpMethod.Post),
            ("Cancel", HttpMethod.Post),
            ("Approve", HttpMethod.Post),
            ("Reject", HttpMethod.Post),
            ("Search", HttpMethod.Get),
            ("List", HttpMethod.Get),
            ("Find", HttpMethod.Get),
            ("Fetch", HttpMethod.Get),
            ("Query", HttpMethod.Get),
        };

        /// <summary>
        /// Suffix patterns that indicate specific HTTP methods (for ambiguous cases).
        /// </summary>
        private static readonly List<(string Suffix, HttpMethod Method)> SuffixRules = new()
        {
            ("Transaction", HttpMethod.Post),
            ("Report", HttpMethod.Post),
            ("Request", HttpMethod.Post),
        };

        /// <summary>
        /// Infers the HTTP method for a given method name.
        /// </summary>
        /// <param name="methodName">The method name (e.g., "GetActiveBoosts", "AiCoreCustomization").</param>
        /// <returns>A tuple containing the inferred HTTP method and whether manual review is needed.</returns>
        public static (HttpMethod Method, bool NeedsReview) InferMethod(string methodName)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                return (HttpMethod.Get, true);
            }

            // Check prefix rules first
            foreach (var (prefix, method) in PrefixRules)
            {
                if (methodName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    return (method, false);
                }
            }

            // Check suffix rules for ambiguous cases
            foreach (var (suffix, method) in SuffixRules)
            {
                if (methodName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
                {
                    return (method, false);
                }
            }

            // Default to GET with review flag for methods that don't match patterns
            return (HttpMethod.Get, true);
        }

        /// <summary>
        /// Gets the PascalCase method name for use in generated code.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <returns>The method name in PascalCase (e.g., "Get", "Post").</returns>
        public static string GetMethodName(HttpMethod method)
        {
            return method.Method switch
            {
                "GET" => "Get",
                "POST" => "Post",
                "PUT" => "Put",
                "DELETE" => "Delete",
                "PATCH" => "Patch",
                _ => "Get",
            };
        }
    }
}
