// <copyright file="SnapshotUpdater.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Spectre.Console;

namespace Den.Dev.Grunt.Auditor.Services
{
    /// <summary>
    /// Updates XML documentation example files with fresh API responses.
    /// </summary>
    public class SnapshotUpdater
    {
        private static readonly Regex XuidPattern = new(@"xuid\(\d+\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex GuidPattern = new(@"[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly string _baseDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotUpdater"/> class.
        /// </summary>
        /// <param name="baseDirectory">Base directory containing APIDocsExamples folder.</param>
        public SnapshotUpdater(string? baseDirectory = null)
        {
            _baseDirectory = baseDirectory ?? FindApiDocsDirectory();
        }

        /// <summary>
        /// Updates an XML example file with a fresh API response.
        /// </summary>
        /// <param name="endpointId">Endpoint identifier (e.g., "Stats_GetMatchStats").</param>
        /// <param name="rawJson">Raw JSON response to store.</param>
        /// <param name="sanitize">Whether to sanitize sensitive data like XUIDs.</param>
        /// <returns>True if the file was updated successfully.</returns>
        public bool UpdateSnapshot(string endpointId, string rawJson, bool sanitize = true)
        {
            if (string.IsNullOrEmpty(rawJson))
            {
                return false;
            }

            var xmlPath = GetXmlPath(endpointId);
            if (string.IsNullOrEmpty(xmlPath))
            {
                AnsiConsole.MarkupLine($"[yellow]Warning:[/] Could not determine XML path for {endpointId}");
                return false;
            }

            try
            {
                // Format the JSON
                var formattedJson = FormatJson(rawJson);

                // Sanitize if requested
                if (sanitize)
                {
                    formattedJson = SanitizeJson(formattedJson);
                }

                // Build the XML content
                var xmlContent = BuildXmlContent(formattedJson);

                // Ensure directory exists
                var directory = Path.GetDirectoryName(xmlPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Write the file
                File.WriteAllText(xmlPath, xmlContent, Encoding.UTF8);

                return true;
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error updating snapshot:[/] {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gets the path to the XML example file for an endpoint.
        /// </summary>
        /// <param name="endpointId">Endpoint identifier.</param>
        /// <returns>Full path to the XML file, or null if not determinable.</returns>
        public string? GetXmlPath(string endpointId)
        {
            if (string.IsNullOrEmpty(_baseDirectory))
            {
                return null;
            }

            return Path.Combine(_baseDirectory, "APIDocsExamples", "HaloInfinite", $"{endpointId}.xml");
        }

        /// <summary>
        /// Checks if a snapshot file exists for the endpoint.
        /// </summary>
        /// <param name="endpointId">Endpoint identifier.</param>
        /// <returns>True if the snapshot file exists.</returns>
        public bool SnapshotExists(string endpointId)
        {
            var path = GetXmlPath(endpointId);
            return path != null && File.Exists(path);
        }

        /// <summary>
        /// Formats JSON with proper indentation.
        /// </summary>
        /// <param name="rawJson">Raw JSON string.</param>
        /// <returns>Formatted JSON string.</returns>
        public string FormatJson(string rawJson)
        {
            try
            {
                using var doc = JsonDocument.Parse(rawJson);
                var options = new JsonSerializerOptions { WriteIndented = true };
                return JsonSerializer.Serialize(doc.RootElement, options);
            }
            catch
            {
                return rawJson;
            }
        }

        /// <summary>
        /// Sanitizes sensitive data in JSON.
        /// </summary>
        /// <param name="json">JSON string to sanitize.</param>
        /// <returns>Sanitized JSON string.</returns>
        public string SanitizeJson(string json)
        {
            // Replace XUIDs with placeholder
            var result = XuidPattern.Replace(json, "xuid(PLAYER_XUID_HERE)");

            // Optionally replace GUIDs that look like match IDs or asset IDs
            // result = GuidPattern.Replace(result, "00000000-0000-0000-0000-000000000000");

            return result;
        }

        /// <summary>
        /// Builds the XML content for an example file.
        /// </summary>
        /// <param name="formattedJson">Formatted JSON content.</param>
        /// <returns>Complete XML content.</returns>
        private string BuildXmlContent(string formattedJson)
        {
            var timestamp = DateTime.UtcNow.ToString("M/d/yyyy");
            var sb = new StringBuilder();

            sb.AppendLine("<example>");
            sb.AppendLine($"\tHere is an example response from the API, as snapshotted on {timestamp}:");
            sb.AppendLine("\t<code>");

            // Indent each line of JSON
            var lines = formattedJson.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                sb.AppendLine($"\t{line}");
            }

            sb.AppendLine("\t</code>");
            sb.AppendLine("</example>");

            return sb.ToString();
        }

        /// <summary>
        /// Finds the APIDocsExamples directory by walking up from the current directory.
        /// </summary>
        private static string FindApiDocsDirectory()
        {
            var current = Directory.GetCurrentDirectory();

            while (!string.IsNullOrEmpty(current))
            {
                var candidate = Path.Combine(current, "Den.Dev.Grunt");
                if (Directory.Exists(candidate))
                {
                    return candidate;
                }

                // Check if we're already in Den.Dev.Grunt
                if (Path.GetFileName(current) == "Den.Dev.Grunt")
                {
                    return current;
                }

                current = Path.GetDirectoryName(current);
            }

            // Default to current directory
            return Directory.GetCurrentDirectory();
        }
    }
}
