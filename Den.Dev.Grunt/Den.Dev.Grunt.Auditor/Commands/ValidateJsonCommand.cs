// <copyright file="ValidateJsonCommand.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.CommandLine;
using System.IO;
using System.Linq;
using Den.Dev.Grunt.Auditor.Models;
using Den.Dev.Grunt.Auditor.Services;
using Spectre.Console;

namespace Den.Dev.Grunt.Auditor.Commands
{
    /// <summary>
    /// Command that validates a JSON file against a specific model (offline).
    /// </summary>
    public class ValidateJsonCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateJsonCommand"/> class.
        /// </summary>
        public ValidateJsonCommand()
            : base("validate-json", "Validate a JSON file against a model (offline)")
        {
            var typeOption = new Option<string>(
                aliases: new[] { "-m", "--model" },
                description: "The C# model type name to validate against")
            {
                IsRequired = true,
            };

            var inputOption = new Option<string>(
                aliases: new[] { "-i", "--input" },
                description: "Path to the JSON file to validate")
            {
                IsRequired = true,
            };

            AddOption(typeOption);
            AddOption(inputOption);

            this.SetHandler(Execute, typeOption, inputOption);
        }

        private void Execute(string modelType, string inputPath)
        {
            AnsiConsole.Write(new FigletText("Auditor").Color(Color.Cyan1));
            AnsiConsole.MarkupLine("[dim]Offline JSON Validation[/]");
            AnsiConsole.WriteLine();

            // Check file exists
            if (!File.Exists(inputPath))
            {
                AnsiConsole.MarkupLine($"[red]File not found:[/] {inputPath}");
                return;
            }

            // Read JSON
            string json;
            try
            {
                json = File.ReadAllText(inputPath);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error reading file:[/] {ex.Message}");
                return;
            }

            // Validate
            var validator = new ResponseValidator();
            var resolvedType = validator.ResolveModelType(modelType);

            if (resolvedType == null)
            {
                AnsiConsole.MarkupLine($"[red]Model type not found:[/] {modelType}");
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[dim]Available model types are in the Den.Dev.Grunt.Models.HaloInfinite namespace.[/]");
                return;
            }

            AnsiConsole.MarkupLine($"[cyan]Model:[/] {resolvedType.FullName}");
            AnsiConsole.MarkupLine($"[cyan]Input:[/] {inputPath}");
            AnsiConsole.WriteLine();

            var discrepancies = validator.Validate(json, resolvedType);

            if (discrepancies.Count == 0)
            {
                AnsiConsole.MarkupLine("[green]Validation passed![/] No discrepancies found.");
                return;
            }

            // Group discrepancies by type
            var byType = discrepancies.GroupBy(d => d.Type).OrderBy(g => g.Key);

            AnsiConsole.MarkupLine($"[yellow]Found {discrepancies.Count} discrepancies:[/]");
            AnsiConsole.WriteLine();

            foreach (var group in byType)
            {
                var color = group.Key switch
                {
                    DiscrepancyType.UnexpectedProperty => "yellow",
                    DiscrepancyType.TypeMismatch => "red",
                    DiscrepancyType.DeserializationFailure => "red",
                    DiscrepancyType.NullabilityIssue => "yellow",
                    _ => "white",
                };

                AnsiConsole.MarkupLine($"[bold {color}]{group.Key}[/] ({group.Count()}):");

                foreach (var d in group.Take(20))
                {
                    AnsiConsole.MarkupLine($"  [cyan]{d.Path}[/]");
                    AnsiConsole.MarkupLine($"    {d.Message}");

                    if (!string.IsNullOrEmpty(d.ActualValue))
                    {
                        AnsiConsole.MarkupLine($"    Value: [dim]{d.ActualValue}[/]");
                    }
                }

                if (group.Count() > 20)
                {
                    AnsiConsole.MarkupLine($"  [dim]... and {group.Count() - 20} more[/]");
                }

                AnsiConsole.WriteLine();
            }

            // Summary
            var hasFailures = discrepancies.Any(d =>
                d.Type == DiscrepancyType.TypeMismatch ||
                d.Type == DiscrepancyType.DeserializationFailure);

            if (hasFailures)
            {
                AnsiConsole.MarkupLine("[red]Validation failed.[/] Model needs to be updated.");
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Validation passed with warnings.[/] Consider adding missing properties to the model.");
            }
        }
    }
}
