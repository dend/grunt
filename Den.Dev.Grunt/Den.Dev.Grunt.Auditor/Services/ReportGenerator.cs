// <copyright file="ReportGenerator.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Den.Dev.Grunt.Auditor.Models;
using Scriban;
using Scriban.Runtime;
using Spectre.Console;

namespace Den.Dev.Grunt.Auditor.Services
{
    /// <summary>
    /// Generates validation reports in various formats.
    /// </summary>
    public class ReportGenerator
    {
        /// <summary>
        /// Displays the validation report to the console using Spectre.Console.
        /// </summary>
        /// <param name="report">The validation report to display.</param>
        public void DisplayConsoleReport(ValidationReport report)
        {
            AnsiConsole.WriteLine();

            // Summary panel
            var summaryTable = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn(new TableColumn("[bold]Metric[/]").Centered())
                .AddColumn(new TableColumn("[bold]Count[/]").Centered());

            summaryTable.AddRow("[green]Passed[/]", report.PassedCount.ToString());
            summaryTable.AddRow("[yellow]Warnings[/]", report.WarningCount.ToString());
            summaryTable.AddRow("[red]Failed[/]", report.FailedCount.ToString());
            summaryTable.AddRow("[dim]Skipped[/]", report.SkippedCount.ToString());
            summaryTable.AddRow("[red]Errors[/]", report.ErrorCount.ToString());
            summaryTable.AddRow("[bold]Total[/]", report.TotalCount.ToString());

            AnsiConsole.Write(new Panel(summaryTable)
                .Header("[bold]API Model Validation Report[/]")
                .Border(BoxBorder.Double)
                .Padding(1, 1));

            AnsiConsole.WriteLine();

            // Results table
            var resultsTable = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn(new TableColumn("[bold]Endpoint[/]").NoWrap())
                .AddColumn(new TableColumn("[bold]Model[/]"))
                .AddColumn(new TableColumn("[bold]Status[/]").Centered())
                .AddColumn(new TableColumn("[bold]Issues[/]"));

            foreach (var result in report.Results.OrderBy(r => r.Status).ThenBy(r => r.EndpointId))
            {
                var statusDisplay = result.Status switch
                {
                    ValidationStatus.Pass => "[green]Pass[/]",
                    ValidationStatus.Warning => "[yellow]Warn[/]",
                    ValidationStatus.Fail => "[red]Fail[/]",
                    ValidationStatus.Skipped => "[dim]Skip[/]",
                    ValidationStatus.Error => "[red]Error[/]",
                    _ => result.Status.ToString(),
                };

                var issuesDisplay = GetIssuesSummary(result);

                resultsTable.AddRow(
                    result.EndpointId,
                    result.ModelType,
                    statusDisplay,
                    issuesDisplay);
            }

            AnsiConsole.Write(resultsTable);
            AnsiConsole.WriteLine();

            // Show details for failures and warnings
            var problemResults = report.Results
                .Where(r => r.Status == ValidationStatus.Fail || r.Status == ValidationStatus.Warning || r.Status == ValidationStatus.Error)
                .ToList();

            if (problemResults.Any())
            {
                AnsiConsole.MarkupLine("[bold]Details:[/]");
                AnsiConsole.WriteLine();

                foreach (var result in problemResults)
                {
                    var color = result.Status == ValidationStatus.Fail || result.Status == ValidationStatus.Error ? "red" : "yellow";
                    AnsiConsole.MarkupLine($"[{color}]{result.EndpointId}[/]");

                    if (!string.IsNullOrEmpty(result.RequestUrl))
                    {
                        AnsiConsole.MarkupLine($"  [dim]Request:[/] {result.RequestMethod ?? "GET"} {Markup.Escape(result.RequestUrl)}");
                    }

                    if (result.RequestHeaders != null && result.RequestHeaders.Count > 0)
                    {
                        AnsiConsole.MarkupLine($"  [dim]Headers:[/]");
                        foreach (var header in result.RequestHeaders.Where(h => !h.Key.Contains("Authorization", StringComparison.OrdinalIgnoreCase)))
                        {
                            AnsiConsole.MarkupLine($"    {Markup.Escape(header.Key)}: {Markup.Escape(header.Value.Length > 50 ? header.Value.Substring(0, 47) + "..." : header.Value)}");
                        }
                    }

                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        AnsiConsole.MarkupLine($"  Error: {Markup.Escape(result.ErrorMessage)}");
                    }

                    // Group similar discrepancies by normalized path pattern
                    var groupedDiscrepancies = result.Discrepancies
                        .GroupBy(d => NormalizePathPattern(d.Path) + "|" + d.Type + "|" + d.Message)
                        .ToList();

                    foreach (var group in groupedDiscrepancies)
                    {
                        var first = group.First();
                        var count = group.Count();

                        if (count > 1)
                        {
                            var normalizedPath = NormalizePathPattern(first.Path);
                            AnsiConsole.MarkupLine($"  [dim]{first.Type}[/] at [cyan]{Markup.Escape(normalizedPath)}[/] [dim]({count} occurrences)[/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine($"  [dim]{first.Type}[/] at [cyan]{Markup.Escape(first.Path)}[/]");
                        }

                        AnsiConsole.MarkupLine($"    {Markup.Escape(first.Message)}");

                        if (!string.IsNullOrEmpty(first.ActualValue))
                        {
                            AnsiConsole.MarkupLine($"    [dim]Actual:[/] {Markup.Escape(first.ActualValue)}");
                        }

                        if (!string.IsNullOrEmpty(first.ExpectedType))
                        {
                            AnsiConsole.MarkupLine($"    [dim]Expected type:[/] {Markup.Escape(first.ExpectedType)}");
                        }
                    }

                    AnsiConsole.WriteLine();
                }
            }
        }

        /// <summary>
        /// Generates a JSON report file.
        /// </summary>
        /// <param name="report">The validation report.</param>
        /// <param name="outputPath">Path to write the JSON file.</param>
        public void GenerateJsonReport(ValidationReport report, string outputPath)
        {
            var jsonReport = new
            {
                timestamp = report.Timestamp.ToString("O"),
                summary = new
                {
                    passed = report.PassedCount,
                    warnings = report.WarningCount,
                    failed = report.FailedCount,
                    skipped = report.SkippedCount,
                    errors = report.ErrorCount,
                    total = report.TotalCount,
                },
                results = report.Results.Select(r => new
                {
                    endpointId = r.EndpointId,
                    model = r.ModelType,
                    status = r.Status.ToString().ToLowerInvariant(),
                    httpStatusCode = r.HttpStatusCode,
                    errorMessage = r.ErrorMessage,
                    skipReason = r.SkipReason,
                    discrepancies = r.Discrepancies.Select(d => new
                    {
                        type = d.Type.ToString(),
                        path = d.Path,
                        jsonType = d.JsonType,
                        expectedType = d.ExpectedType,
                        message = d.Message,
                        actualValue = d.ActualValue,
                    }),
                }),
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(jsonReport, options);
            File.WriteAllText(outputPath, json);

            AnsiConsole.MarkupLine($"[green]Report saved to:[/] {outputPath}");
        }

        /// <summary>
        /// Generates an HTML report file using the Scriban template.
        /// </summary>
        /// <param name="report">The validation report.</param>
        /// <param name="outputPath">Path to write the HTML file.</param>
        /// <param name="templateDirectory">Directory containing the template files. If null, uses the default Templates directory.</param>
        public void GenerateHtmlReport(ValidationReport report, string outputPath, string? templateDirectory = null)
        {
            // Find template file
            templateDirectory ??= Path.Combine(AppContext.BaseDirectory, "Templates");
            var templatePath = Path.Combine(templateDirectory, "ValidationReport.scriban");

            if (!File.Exists(templatePath))
            {
                AnsiConsole.MarkupLine($"[red]HTML template not found at:[/] {templatePath}");
                return;
            }

            // Load and parse template
            var templateContent = File.ReadAllText(templatePath);
            var template = Template.Parse(templateContent);

            if (template.HasErrors)
            {
                AnsiConsole.MarkupLine($"[red]Template parsing errors:[/]");
                foreach (var msg in template.Messages)
                {
                    AnsiConsole.MarkupLine($"  {msg}");
                }

                return;
            }

            // Build template context
            var scriptObject = new ScriptObject();
            scriptObject.Import(new
            {
                report = new
                {
                    timestamp = report.Timestamp,
                    passed_count = report.PassedCount,
                    warning_count = report.WarningCount,
                    failed_count = report.FailedCount,
                    skipped_count = report.SkippedCount,
                    error_count = report.ErrorCount,
                    total_count = report.TotalCount,
                    results = ConvertResults(report),
                },
            });

            var context = new TemplateContext();
            context.PushGlobal(scriptObject);
            context.MemberRenamer = member => member.Name;

            // Render and save
            var html = template.Render(context);
            File.WriteAllText(outputPath, html);

            AnsiConsole.MarkupLine($"[green]HTML report saved to:[/] {outputPath}");
        }

        /// <summary>
        /// Converts validation results to a format suitable for the Scriban template.
        /// </summary>
        private static object[] ConvertResults(ValidationReport report)
        {
            var results = new object[report.Results.Count];

            for (int i = 0; i < report.Results.Count; i++)
            {
                var r = report.Results[i];
                results[i] = new
                {
                    endpoint_id = r.EndpointId,
                    model_type = r.ModelType,
                    status = r.Status.ToString().ToLowerInvariant(),
                    http_status_code = r.HttpStatusCode,
                    error_message = r.ErrorMessage,
                    skip_reason = r.SkipReason,
                    discrepancies = ConvertDiscrepancies(r),
                };
            }

            return results;
        }

        /// <summary>
        /// Converts discrepancies to a format suitable for the Scriban template.
        /// </summary>
        private static object[] ConvertDiscrepancies(EndpointValidationResult result)
        {
            var discrepancies = new object[result.Discrepancies.Count];

            for (int i = 0; i < result.Discrepancies.Count; i++)
            {
                var d = result.Discrepancies[i];

                // Extract property name from path for suggested fix
                var propertyName = ExtractPropertyName(d.Path);

                discrepancies[i] = new
                {
                    path = d.Path,
                    type = d.Type.ToString(),
                    json_type = d.JsonType,
                    expected_type = d.ExpectedType,
                    message = d.Message,
                    actual_value = d.ActualValue,
                    property_name = propertyName,
                };
            }

            return discrepancies;
        }

        /// <summary>
        /// Extracts a property name from a JSON path for code suggestions.
        /// </summary>
        private static string? ExtractPropertyName(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            // Get the last segment of the path
            var lastDot = path.LastIndexOf('.');
            if (lastDot >= 0 && lastDot < path.Length - 1)
            {
                var segment = path.Substring(lastDot + 1);

                // Remove array indexers
                var bracketIndex = segment.IndexOf('[');
                if (bracketIndex > 0)
                {
                    segment = segment.Substring(0, bracketIndex);
                }

                // Convert to PascalCase if needed
                if (!string.IsNullOrEmpty(segment) && char.IsLower(segment[0]))
                {
                    segment = char.ToUpperInvariant(segment[0]) + segment.Substring(1);
                }

                return segment;
            }

            return null;
        }

        /// <summary>
        /// Truncates a value string for display.
        /// </summary>
        private static string? TruncateValue(string? value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            if (value.Length <= maxLength)
            {
                return value;
            }

            return value.Substring(0, maxLength - 3) + "...";
        }

        /// <summary>
        /// Normalizes a JSON path pattern by replacing array indices with wildcards.
        /// </summary>
        private static string NormalizePathPattern(string path)
        {
            // Replace [0], [1], [123] etc with [*]
            return System.Text.RegularExpressions.Regex.Replace(path, @"\[\d+\]", "[*]");
        }

        /// <summary>
        /// Gets a summary of issues for display.
        /// </summary>
        private static string GetIssuesSummary(EndpointValidationResult result)
        {
            if (!string.IsNullOrEmpty(result.SkipReason))
            {
                return $"[dim]{result.SkipReason}[/]";
            }

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                var msg = result.ErrorMessage;
                if (msg.Length > 40)
                {
                    msg = msg.Substring(0, 37) + "...";
                }

                return $"[red]{msg}[/]";
            }

            if (result.Discrepancies.Count == 0)
            {
                return "-";
            }

            var unexpectedCount = result.Discrepancies.Count(d => d.Type == DiscrepancyType.UnexpectedProperty);
            var typeMismatchCount = result.Discrepancies.Count(d => d.Type == DiscrepancyType.TypeMismatch);
            var nullabilityCount = result.Discrepancies.Count(d => d.Type == DiscrepancyType.NullabilityIssue);
            var otherCount = result.Discrepancies.Count - unexpectedCount - typeMismatchCount - nullabilityCount;

            var parts = new System.Collections.Generic.List<string>();

            if (unexpectedCount > 0)
            {
                parts.Add($"{unexpectedCount} unexpected");
            }

            if (typeMismatchCount > 0)
            {
                parts.Add($"{typeMismatchCount} type mismatch");
            }

            if (nullabilityCount > 0)
            {
                parts.Add($"{nullabilityCount} null issues");
            }

            if (otherCount > 0)
            {
                parts.Add($"{otherCount} other");
            }

            return string.Join(", ", parts);
        }
    }
}
