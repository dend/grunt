// <copyright file="ConsoleUI.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Den.Dev.Grunt.Librarian.Models;
using Spectre.Console;

namespace Den.Dev.Grunt.Librarian.Services
{
    /// <summary>
    /// Centralized console UI service with rich formatting support.
    /// Automatically falls back to plain text in non-interactive scenarios.
    /// </summary>
    public class ConsoleUI
    {
        private readonly bool isInteractive;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleUI"/> class.
        /// </summary>
        public ConsoleUI()
        {
            this.isInteractive = DetectInteractiveMode();
        }

        /// <summary>
        /// Writes the application header with branding.
        /// </summary>
        public void WriteHeader()
        {
            if (this.isInteractive)
            {
                var content = new Rows(
                    new FigletText("Librarian").Color(Color.Blue),
                    new Markup("[dim]Halo Infinite API Code Generator[/]"),
                    new Markup("[dim]Developed by Den Delimarsky | https://gruntapi.com[/]"));
                AnsiConsole.Write(new Panel(content)
                    .Border(BoxBorder.Rounded)
                    .BorderColor(Color.Blue)
                    .Padding(1, 0));
                AnsiConsole.WriteLine();
            }
            else
            {
                Console.WriteLine("Den.Dev.Grunt Librarian - Halo Infinite API Code Generator");
                Console.WriteLine("Developed by Den Delimarsky. Part of https://gruntapi.com");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Writes an informational message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void WriteInfo(string message)
        {
            if (this.isInteractive)
            {
                AnsiConsole.MarkupLine($"[blue][[*]][/] {EscapeMarkup(message)}");
            }
            else
            {
                Console.WriteLine($"[*] {message}");
            }
        }

        /// <summary>
        /// Writes a success message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void WriteSuccess(string message)
        {
            if (this.isInteractive)
            {
                AnsiConsole.MarkupLine($"[green][[+]][/] {EscapeMarkup(message)}");
            }
            else
            {
                Console.WriteLine($"[+] {message}");
            }
        }

        /// <summary>
        /// Writes a warning message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void WriteWarning(string message)
        {
            if (this.isInteractive)
            {
                AnsiConsole.MarkupLine($"[yellow][[!]][/] {EscapeMarkup(message)}");
            }
            else
            {
                Console.WriteLine($"[!] {message}");
            }
        }

        /// <summary>
        /// Writes an error message.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void WriteError(string message)
        {
            if (this.isInteractive)
            {
                AnsiConsole.MarkupLine($"[red][[X]][/] {EscapeMarkup(message)}");
            }
            else
            {
                Console.WriteLine($"[X] {message}");
            }
        }

        /// <summary>
        /// Executes an async operation with a spinner indicator.
        /// </summary>
        /// <typeparam name="T">The return type of the operation.</typeparam>
        /// <param name="message">The message to display during operation.</param>
        /// <param name="action">The async action to execute.</param>
        /// <returns>The result of the action.</returns>
        public async Task<T> WithSpinnerAsync<T>(string message, Func<Task<T>> action)
        {
            if (this.isInteractive)
            {
                return await AnsiConsole.Status()
                    .Spinner(Spinner.Known.Dots)
                    .SpinnerStyle(Style.Parse("blue"))
                    .StartAsync($"[blue][[*]][/] {EscapeMarkup(message)}", async ctx =>
                    {
                        return await action();
                    });
            }
            else
            {
                Console.WriteLine($"[*] {message}");
                return await action();
            }
        }

        /// <summary>
        /// Processes items with a progress bar.
        /// </summary>
        /// <typeparam name="T">The type of items to process.</typeparam>
        /// <param name="description">The progress description.</param>
        /// <param name="items">The items to process.</param>
        /// <param name="process">The action to run for each item.</param>
        public void WithProgress<T>(string description, IReadOnlyList<T> items, Action<T> process)
        {
            if (this.isInteractive)
            {
                AnsiConsole.Progress()
                    .Columns(new ProgressColumn[]
                    {
                        new TaskDescriptionColumn(),
                        new ProgressBarColumn(),
                        new PercentageColumn(),
                        new SpinnerColumn(),
                    })
                    .Start(ctx =>
                    {
                        var task = ctx.AddTask(description, maxValue: items.Count);

                        foreach (var item in items)
                        {
                            process(item);
                            task.Increment(1);
                        }
                    });
            }
            else
            {
                foreach (var item in items)
                {
                    process(item);
                }
            }
        }

        /// <summary>
        /// Writes the configuration as a formatted table.
        /// </summary>
        /// <param name="outputDirectory">The output directory.</param>
        /// <param name="responseTypesFile">The response types file path.</param>
        /// <param name="dryRun">Whether this is a dry run.</param>
        public void WriteConfiguration(string outputDirectory, string? responseTypesFile, bool dryRun)
        {
            if (this.isInteractive)
            {
                var table = new Table()
                    .Border(TableBorder.Rounded)
                    .AddColumn("[bold]Setting[/]")
                    .AddColumn("[bold]Value[/]");

                table.AddRow("Output Directory", EscapeMarkup(outputDirectory));
                table.AddRow("Response Types", responseTypesFile ?? "[dim](none)[/]");
                table.AddRow("Dry Run", dryRun ? "[yellow]Yes[/]" : "No");

                AnsiConsole.Write(table);
                AnsiConsole.WriteLine();
            }
            else
            {
                Console.WriteLine($"Output directory: {outputDirectory}");
                Console.WriteLine($"Response types file: {responseTypesFile ?? "(none)"}");
                Console.WriteLine($"Dry run: {dryRun}");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Writes the generation summary.
        /// </summary>
        /// <param name="result">The generation result.</param>
        /// <param name="outputDirectory">The output directory.</param>
        /// <param name="dryRun">Whether this was a dry run.</param>
        public void WriteGenerationSummary(GenerationResult result, string outputDirectory, bool dryRun)
        {
            if (this.isInteractive)
            {
                AnsiConsole.WriteLine();

                var table = new Table()
                    .Border(TableBorder.None)
                    .AddColumn("[bold]Metric[/]")
                    .AddColumn("[bold]Value[/]");
                table.AddRow("Files generated", result.FilesGenerated.Count.ToString());
                table.AddRow("Total methods", result.TotalMethodsGenerated.ToString());

                AnsiConsole.Write(new Panel(table)
                    .Header("[bold blue]Generation Summary[/]")
                    .Border(BoxBorder.Rounded)
                    .BorderColor(Color.Blue));

                if (result.Errors.Count > 0)
                {
                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine($"[red]Errors: {result.Errors.Count}[/]");
                    foreach (var error in result.Errors)
                    {
                        AnsiConsole.MarkupLine($"  [red]-[/] {EscapeMarkup(error)}");
                    }
                }
                else if (!dryRun)
                {
                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine($"[green]Generated files written to:[/] {EscapeMarkup(outputDirectory)}");
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("=== Generation Summary ===");
                Console.WriteLine($"Files generated: {result.FilesGenerated.Count}");
                Console.WriteLine($"Total methods: {result.TotalMethodsGenerated}");

                if (result.Errors.Count > 0)
                {
                    Console.WriteLine($"Errors: {result.Errors.Count}");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"  - {error}");
                    }
                }
                else if (!dryRun)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Generated files written to: {outputDirectory}");
                }
            }
        }

        /// <summary>
        /// Writes the coverage report with rich formatting.
        /// </summary>
        /// <param name="report">The coverage report.</param>
        /// <param name="includeImplementationAnalysis">Whether to include implementation analysis.</param>
        public void WriteCoverageReport(CoverageReport report, bool includeImplementationAnalysis)
        {
            if (this.isInteractive)
            {
                AnsiConsole.WriteLine();

                // Summary table
                var summaryTable = new Table()
                    .Border(TableBorder.None)
                    .AddColumn("[bold]Metric[/]")
                    .AddColumn("[bold]Value[/]");

                summaryTable.AddRow("Total endpoints", report.TotalEndpoints.ToString());
                summaryTable.AddRow(
                    "With response type mappings",
                    $"{report.WithResponseTypes} [dim]({report.ResponseTypeCoveragePercent:F1}%)[/]");

                if (includeImplementationAnalysis)
                {
                    summaryTable.AddRow(
                        "Implemented in modules",
                        $"{report.ImplementedInModules} [dim]({report.ImplementationCoveragePercent:F1}%)[/]");
                }

                AnsiConsole.Write(new Panel(summaryTable)
                    .Header("[bold blue]Halo API Coverage Analysis[/]")
                    .Border(BoxBorder.Rounded)
                    .BorderColor(Color.Blue));
                AnsiConsole.WriteLine();

                // Missing response types
                if (report.MissingResponseTypes.Count > 0)
                {
                    AnsiConsole.Write(new Rule($"[yellow]Missing Response Type Mappings ({report.MissingResponseTypes.Count})[/]").RuleStyle("yellow"));
                    WriteGapsTree(report.MissingResponseTypes);
                    AnsiConsole.WriteLine();
                }
                else
                {
                    AnsiConsole.MarkupLine("[green]All endpoints have response type mappings[/]");
                    AnsiConsole.WriteLine();
                }

                // Missing implementations
                if (includeImplementationAnalysis)
                {
                    if (report.MissingImplementations.Count > 0)
                    {
                        AnsiConsole.Write(new Rule($"[yellow]Not Implemented in Modules ({report.MissingImplementations.Count})[/]").RuleStyle("yellow"));
                        WriteGapsTree(report.MissingImplementations);
                        AnsiConsole.WriteLine();
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[green]All endpoints are implemented in modules[/]");
                        AnsiConsole.WriteLine();
                    }

                    // Detailed summary
                    WriteCoverageSummary(report);
                }
            }
            else
            {
                // Fall back to plain text report
                var reportGenerator = new ReportGenerator();
                Console.WriteLine(reportGenerator.GenerateReport(report, includeImplementationAnalysis));
            }
        }

        /// <summary>
        /// Writes the help information.
        /// </summary>
        public void WriteHelp()
        {
            if (this.isInteractive)
            {
                AnsiConsole.Write(new Panel(
                    new Markup("[bold]Den.Dev.Grunt.Librarian[/]\n[dim]Halo Infinite API Code Generator[/]"))
                    .Border(BoxBorder.Rounded)
                    .BorderColor(Color.Blue));

                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[bold]Usage:[/] Den.Dev.Grunt.Librarian [dim][options][/]");
                AnsiConsole.WriteLine();

                // Main options table
                var optionsTable = new Table()
                    .Border(TableBorder.Rounded)
                    .Title("[bold]Options[/]")
                    .AddColumn("[bold]Option[/]")
                    .AddColumn("[bold]Description[/]");

                optionsTable.AddRow("--output, -o [dim]<directory>[/]", "Output directory for generated files\n[dim]Default: ./Output/Generated[/]");
                optionsTable.AddRow("--response-types, -r [dim]<file>[/]", "Path to response-types.json mapping file");
                optionsTable.AddRow("--dry-run, -d", "Preview output without writing files");
                optionsTable.AddRow("--help, -h", "Show this help message");

                AnsiConsole.Write(optionsTable);
                AnsiConsole.WriteLine();

                // Gap analysis options table
                var gapTable = new Table()
                    .Border(TableBorder.Rounded)
                    .Title("[bold]Gap Analysis Options[/]")
                    .AddColumn("[bold]Option[/]")
                    .AddColumn("[bold]Description[/]");

                gapTable.AddRow("--analyze-gaps", "Run gap analysis instead of code generation");
                gapTable.AddRow("--modules-path, -m [dim]<directory>[/]", "Path to module source files for implementation analysis");
                gapTable.AddRow("--output-report [dim]<file>[/]", "Write gap analysis report to file");

                AnsiConsole.Write(gapTable);
                AnsiConsole.WriteLine();

                // Examples
                AnsiConsole.MarkupLine("[bold]Examples:[/]");
                AnsiConsole.MarkupLine("  [blue]dotnet run[/]                     Generate to default output directory");
                AnsiConsole.MarkupLine("  [blue]dotnet run --output C:\\Code[/]    Generate to custom directory");
                AnsiConsole.MarkupLine("  [blue]dotnet run --dry-run[/]           Preview what would be generated");
                AnsiConsole.MarkupLine("  [blue]dotnet run --analyze-gaps[/]      Run gap analysis");
            }
            else
            {
                Console.WriteLine("Usage: Den.Dev.Grunt.Librarian [options]");
                Console.WriteLine();
                Console.WriteLine("Options:");
                Console.WriteLine("  --output, -o <directory>       Output directory for generated files");
                Console.WriteLine("                                 Default: ./Output/Generated");
                Console.WriteLine("  --response-types, -r <file>    Path to response-types.json mapping file");
                Console.WriteLine("  --dry-run, -d                  Preview output without writing files");
                Console.WriteLine("  --help, -h                     Show this help message");
                Console.WriteLine();
                Console.WriteLine("Gap Analysis Options:");
                Console.WriteLine("  --analyze-gaps                 Run gap analysis instead of code generation");
                Console.WriteLine("  --modules-path, -m <directory> Path to module source files for implementation analysis");
                Console.WriteLine("  --output-report <file>         Write gap analysis report to file");
                Console.WriteLine();
                Console.WriteLine("Examples:");
                Console.WriteLine("  dotnet run                     Generate to default output directory");
                Console.WriteLine("  dotnet run --output C:\\Code    Generate to custom directory");
                Console.WriteLine("  dotnet run --dry-run           Preview what would be generated");
                Console.WriteLine("  dotnet run --analyze-gaps      Run gap analysis");
                Console.WriteLine("  dotnet run --analyze-gaps --modules-path ../Den.Dev.Grunt/Core/Modules");
                Console.WriteLine("  dotnet run --analyze-gaps --output-report gaps.txt");
            }
        }

        /// <summary>
        /// Writes a horizontal rule.
        /// </summary>
        /// <param name="title">Optional title for the rule.</param>
        public void WriteRule(string? title = null)
        {
            if (this.isInteractive)
            {
                if (string.IsNullOrEmpty(title))
                {
                    AnsiConsole.Write(new Rule().RuleStyle("dim"));
                }
                else
                {
                    AnsiConsole.Write(new Rule(EscapeMarkup(title)).RuleStyle("dim"));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine(new string('-', 50));
                }
                else
                {
                    Console.WriteLine($"--- {title} ---");
                }
            }
        }

        /// <summary>
        /// Writes an empty line.
        /// </summary>
        public void WriteLine()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Writes the dry run indicator for a module.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="methodCount">The method count.</param>
        public void WriteDryRunModule(string fileName, int methodCount)
        {
            if (this.isInteractive)
            {
                AnsiConsole.MarkupLine($"[yellow][[DRY RUN]][/] Would generate: {EscapeMarkup(fileName)} [dim]({methodCount} methods)[/]");
            }
            else
            {
                Console.WriteLine($"[DRY RUN] Would generate: {fileName}");
                Console.WriteLine($"  Methods: {methodCount}");
            }
        }

        /// <summary>
        /// Writes a generated module notification.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="methodCount">The method count.</param>
        public void WriteGeneratedModule(string fileName, int methodCount)
        {
            if (this.isInteractive)
            {
                AnsiConsole.MarkupLine($"[green][[+]][/] Generated: {EscapeMarkup(fileName)} [dim]({methodCount} methods)[/]");
            }
            else
            {
                Console.WriteLine($"Generated: {fileName} ({methodCount} methods)");
            }
        }

        /// <summary>
        /// Writes a module generation error.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="errorMessage">The error message.</param>
        public void WriteModuleError(string fileName, string errorMessage)
        {
            if (this.isInteractive)
            {
                AnsiConsole.MarkupLine($"[red][[X]][/] Error generating {EscapeMarkup(fileName)}: {EscapeMarkup(errorMessage)}");
            }
            else
            {
                Console.WriteLine($"Error generating {fileName}: {errorMessage}");
            }
        }

        /// <summary>
        /// Writes a fatal error with stack trace.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        public void WriteFatalError(string message, string? stackTrace)
        {
            if (this.isInteractive)
            {
                AnsiConsole.MarkupLine($"[red bold]Fatal error:[/] {EscapeMarkup(message)}");
                if (!string.IsNullOrEmpty(stackTrace))
                {
                    AnsiConsole.MarkupLine($"[dim]{EscapeMarkup(stackTrace)}[/]");
                }
            }
            else
            {
                Console.WriteLine($"Fatal error: {message}");
                if (!string.IsNullOrEmpty(stackTrace))
                {
                    Console.WriteLine(stackTrace);
                }
            }
        }

        /// <summary>
        /// Detects whether the console is in interactive mode.
        /// </summary>
        private static bool DetectInteractiveMode()
        {
            // Check for output redirection
            if (Console.IsOutputRedirected)
            {
                return false;
            }

            // Check for common CI environment variables
            var ciEnvironmentVariables = new[]
            {
                "CI",
                "TF_BUILD",
                "GITHUB_ACTIONS",
                "JENKINS_URL",
                "GITLAB_CI",
                "TRAVIS",
                "CIRCLECI",
                "BUILDKITE",
            };

            foreach (var envVar in ciEnvironmentVariables)
            {
                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable(envVar)))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Escapes Spectre.Console markup characters.
        /// </summary>
        private static string EscapeMarkup(string text)
        {
            return Markup.Escape(text);
        }

        /// <summary>
        /// Writes gaps as a tree structure grouped by module.
        /// </summary>
        private void WriteGapsTree(List<EndpointGap> gaps)
        {
            var tree = new Tree("[dim]Endpoints[/]");

            var groupedByModule = new Dictionary<string, List<EndpointGap>>();
            foreach (var gap in gaps)
            {
                if (!groupedByModule.ContainsKey(gap.ModuleName))
                {
                    groupedByModule[gap.ModuleName] = new List<EndpointGap>();
                }

                groupedByModule[gap.ModuleName].Add(gap);
            }

            foreach (var kvp in groupedByModule)
            {
                var moduleNode = tree.AddNode($"[bold]{EscapeMarkup(kvp.Key)}[/] [dim]({kvp.Value.Count})[/]");
                foreach (var gap in kvp.Value)
                {
                    moduleNode.AddNode($"[dim]{EscapeMarkup(gap.EndpointId)}[/]");
                }
            }

            AnsiConsole.Write(tree);
        }

        /// <summary>
        /// Writes the detailed coverage summary.
        /// </summary>
        private void WriteCoverageSummary(CoverageReport report)
        {
            var missingTypeIds = new HashSet<string>();
            foreach (var gap in report.MissingResponseTypes)
            {
                missingTypeIds.Add(gap.EndpointId);
            }

            var missingImplIds = new HashSet<string>();
            foreach (var gap in report.MissingImplementations)
            {
                missingImplIds.Add(gap.EndpointId);
            }

            int missingTypeOnly = 0;
            int missingImplOnly = 0;
            int missingBoth = 0;

            foreach (var id in missingTypeIds)
            {
                if (missingImplIds.Contains(id))
                {
                    missingBoth++;
                }
                else
                {
                    missingTypeOnly++;
                }
            }

            foreach (var id in missingImplIds)
            {
                if (!missingTypeIds.Contains(id))
                {
                    missingImplOnly++;
                }
            }

            int fullyCovered = report.TotalEndpoints - missingTypeOnly - missingImplOnly - missingBoth;

            var table = new Table()
                .Border(TableBorder.None)
                .AddColumn("Status")
                .AddColumn("Count");

            table.AddRow("[green]Fully covered (type + implementation)[/]", fullyCovered.ToString());
            table.AddRow("[yellow]Missing response type only[/]", missingTypeOnly.ToString());
            table.AddRow("[yellow]Missing implementation only[/]", missingImplOnly.ToString());
            table.AddRow("[red]Missing both[/]", missingBoth.ToString());

            AnsiConsole.Write(new Panel(table)
                .Header("[bold]Summary[/]")
                .Border(BoxBorder.Rounded));
        }
    }
}
