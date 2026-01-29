// <copyright file="EndpointParser.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Den.Dev.Grunt.Librarian.Models;
using Den.Dev.Grunt.Models.ApiIngress;

namespace Den.Dev.Grunt.Librarian.Services
{
    /// <summary>
    /// Service for parsing endpoint configuration data into structured metadata.
    /// </summary>
    public class EndpointParser
    {
        private static readonly Regex ParameterRegex = new(@"\{([^}]+)\}", RegexOptions.Compiled);

        private readonly ResponseTypeResolver typeResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointParser"/> class.
        /// </summary>
        /// <param name="typeResolver">The response type resolver to use.</param>
        public EndpointParser(ResponseTypeResolver typeResolver)
        {
            this.typeResolver = typeResolver ?? throw new ArgumentNullException(nameof(typeResolver));
        }

        /// <summary>
        /// Parses all endpoints from a configuration container.
        /// </summary>
        /// <param name="container">The configuration container with endpoints and authorities.</param>
        /// <returns>A list of parsed endpoint metadata.</returns>
        public List<EndpointMetadata> ParseEndpoints(Configuration container)
        {
            if (container?.Endpoints == null)
            {
                return new List<EndpointMetadata>();
            }

            var results = new List<EndpointMetadata>();

            foreach (var endpoint in container.Endpoints)
            {
                var metadata = this.ParseEndpoint(endpoint.Key, endpoint.Value, container.Authorities);
                if (metadata != null)
                {
                    results.Add(metadata);
                }
            }

            return results.OrderBy(e => e.EndpointId).ToList();
        }

        /// <summary>
        /// Parses a single endpoint into metadata.
        /// </summary>
        /// <param name="endpointId">The endpoint identifier.</param>
        /// <param name="uriReference">The URI reference data.</param>
        /// <param name="authorities">The available authorities.</param>
        /// <returns>The parsed endpoint metadata, or null if parsing fails.</returns>
        private EndpointMetadata? ParseEndpoint(
            string endpointId,
            OnlineUriReference uriReference,
            Dictionary<string, Authority>? authorities)
        {
            if (string.IsNullOrEmpty(endpointId) || uriReference == null)
            {
                return null;
            }

            var (moduleName, _) = ModuleGrouper.GetModuleInfo(endpointId);
            var methodName = ModuleGrouper.GetMethodName(endpointId);

            // Determine if Spartan token is required based on authority
            var requiresSpartanToken = true;
            if (authorities != null && !string.IsNullOrEmpty(uriReference.AuthorityId))
            {
                if (authorities.TryGetValue(uriReference.AuthorityId, out var authority))
                {
                    requiresSpartanToken = authority.AuthenticationMethods != null
                        && authority.AuthenticationMethods.Contains(AuthenticationMethod.SpartanTokenV4);
                }
            }

            // Infer HTTP method
            var (httpMethod, needsReview) = HttpMethodInferrer.InferMethod(methodName);

            // Extract parameters
            var parameters = this.ExtractParameters(
                uriReference.Path ?? string.Empty,
                uriReference.QueryString ?? string.Empty);

            // Sanitize paths (some endpoints have malformed data like extra closing braces)
            var sanitizedPath = SanitizePath(uriReference.Path ?? string.Empty);
            var sanitizedQueryString = SanitizePath(uriReference.QueryString ?? string.Empty);

            return new EndpointMetadata
            {
                EndpointId = endpointId,
                ModuleName = moduleName,
                MethodName = methodName,
                Path = sanitizedPath,
                QueryString = sanitizedQueryString,
                ClearanceAware = uriReference.ClearanceAware ?? false,
                RequiresSpartanToken = requiresSpartanToken,
                AuthorityId = uriReference.AuthorityId ?? string.Empty,
                InferredMethod = httpMethod,
                NeedsMethodReview = needsReview,
                Parameters = parameters,
                ResponseTypeName = this.typeResolver.ResolveType(endpointId),
            };
        }

        /// <summary>
        /// Extracts parameter information from path and query string.
        /// </summary>
        /// <param name="path">The URL path.</param>
        /// <param name="queryString">The query string.</param>
        /// <returns>A list of extracted parameters.</returns>
        private List<ParameterInfo> ExtractParameters(string path, string queryString)
        {
            var parameters = new List<ParameterInfo>();
            var seenNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Extract from path
            var pathMatches = ParameterRegex.Matches(path);
            foreach (Match match in pathMatches)
            {
                var paramName = match.Groups[1].Value;
                if (!seenNames.Contains(paramName))
                {
                    seenNames.Add(paramName);
                    parameters.Add(new ParameterInfo
                    {
                        Name = paramName,
                        Type = "string",
                        Description = GenerateParameterDescription(paramName),
                        IsQueryParameter = false,
                    });
                }
            }

            // Extract from query string
            var queryMatches = ParameterRegex.Matches(queryString);
            foreach (Match match in queryMatches)
            {
                var paramName = match.Groups[1].Value;
                if (!seenNames.Contains(paramName))
                {
                    seenNames.Add(paramName);
                    parameters.Add(new ParameterInfo
                    {
                        Name = paramName,
                        Type = "string",
                        Description = GenerateParameterDescription(paramName),
                        IsQueryParameter = true,
                    });
                }
            }

            return parameters;
        }

        /// <summary>
        /// Generates a description for a parameter based on its name.
        /// </summary>
        /// <param name="paramName">The parameter name.</param>
        /// <returns>A generated description.</returns>
        private static string GenerateParameterDescription(string paramName)
        {
            return paramName.ToLowerInvariant() switch
            {
                "player" => "The player identifier in the format \"xuid(XUID_VALUE)\".",
                "coreid" => "The unique core identifier.",
                "flightid" => "The flight identifier for the request.",
                "clearanceid" => "The clearance identifier.",
                "targetlist" => "A comma-separated list of target identifiers.",
                "matchid" => "The unique match identifier.",
                "assetid" => "The unique asset identifier.",
                "versionid" => "The version identifier.",
                "gamertag" => "The player's Xbox Live gamertag.",
                "storeid" => "The store identifier.",
                "rewardid" => "The reward identifier.",
                "trackid" => "The reward track identifier.",
                _ => $"The {SplitCamelCase(paramName).ToLowerInvariant()} value.",
            };
        }

        /// <summary>
        /// Splits a camelCase or PascalCase string into words.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The string with spaces between words.</returns>
        private static string SplitCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");
        }

        /// <summary>
        /// Sanitizes a URL path by fixing common malformed patterns from the API data.
        /// </summary>
        /// <param name="path">The path to sanitize.</param>
        /// <returns>The sanitized path.</returns>
        private static string SanitizePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            // Fix doubled closing braces (e.g., "{versionId}}" -> "{versionId}")
            var sanitized = Regex.Replace(path, @"\}+", "}");

            // Fix doubled opening braces
            sanitized = Regex.Replace(sanitized, @"\{+", "{");

            return sanitized;
        }
    }
}
