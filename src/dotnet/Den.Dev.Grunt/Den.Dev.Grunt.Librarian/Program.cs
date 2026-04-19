// <copyright file="Program.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Den.Dev.Grunt.Core;
using Den.Dev.Grunt.Librarian.Services;

namespace Den.Dev.Grunt.Librarian
{
    /// <summary>
    /// Main entry point for the Librarian API code generator.
    /// </summary>
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var ui = new ConsoleUI();
            ui.WriteHeader();

            // Parse command line arguments
            var options = ParseArguments(args);

            if (options.ShowHelp)
            {
                ui.WriteHelp();
                return 0;
            }

            // Gap analysis mode
            if (options.AnalyzeGaps)
            {
                return await RunGapAnalysis(options, ui);
            }

            ui.WriteConfiguration(options.OutputDirectory, options.ResponseTypesFile, options.DryRun);

            try
            {
                // Initialize the Halo client to fetch endpoint configuration
                var client = new HaloInfiniteClient(string.Empty, string.Empty);
                var configResult = await ui.WithSpinnerAsync("Fetching endpoint configuration...", async () =>
                {
                    return await client.Configuration.GetApiSettingsContainerAsync();
                });

                if (configResult?.Result?.Endpoints == null)
                {
                    ui.WriteError("Failed to fetch endpoint configuration.");
                    return 1;
                }

                var container = configResult.Result;
                ui.WriteSuccess($"Found {container.Endpoints.Count} endpoints.");

                // Initialize services
                var typeResolver = new ResponseTypeResolver(options.ResponseTypesFile);
                if (typeResolver.MappingCount > 0)
                {
                    ui.WriteSuccess($"Loaded {typeResolver.MappingCount} response type mappings.");
                }

                var endpointParser = new EndpointParser(typeResolver);
                var endpoints = endpointParser.ParseEndpoints(container);
                ui.WriteSuccess($"Parsed {endpoints.Count} endpoints.");

                // Group endpoints into modules
                var modules = ModuleGrouper.GroupByModule(endpoints);
                ui.WriteSuccess($"Grouped into {modules.Count} modules.");
                ui.WriteLine();

                // Initialize template renderer
                var templateDirectory = Path.Combine(AppContext.BaseDirectory, "Templates");

                // If running from project directory, look for Templates there
                if (!Directory.Exists(templateDirectory))
                {
                    templateDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Templates");
                }

                // Also check relative to the assembly location
                if (!Directory.Exists(templateDirectory))
                {
                    var assemblyDir = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                    if (!string.IsNullOrEmpty(assemblyDir))
                    {
                        templateDirectory = Path.Combine(assemblyDir, "Templates");
                    }
                }

                if (!Directory.Exists(templateDirectory))
                {
                    ui.WriteError("Templates directory not found. Searched in:");
                    ui.WriteError($"  - {Path.Combine(AppContext.BaseDirectory, "Templates")}");
                    ui.WriteError($"  - {Path.Combine(Directory.GetCurrentDirectory(), "Templates")}");
                    return 1;
                }

                var templateRenderer = new TemplateRenderer(templateDirectory);

                // Generate code
                var codeGenerator = new CodeGenerator(templateRenderer, options.OutputDirectory, options.DryRun, ui);
                var modulesList = modules.Values.OrderBy(m => m.Name).ToList();
                var result = codeGenerator.GenerateModules(modulesList);

                ui.WriteGenerationSummary(result, options.OutputDirectory, options.DryRun);

                return result.Success ? 0 : 1;
            }
            catch (Exception ex)
            {
                ui.WriteFatalError(ex.Message, ex.StackTrace);
                return 1;
            }
        }

        /// <summary>
        /// Runs the gap analysis mode.
        /// </summary>
        private static async Task<int> RunGapAnalysis(CommandLineOptions options, ConsoleUI ui)
        {
            ui.WriteInfo("Running gap analysis...");
            ui.WriteLine();

            try
            {
                // Initialize the Halo client to fetch endpoint configuration
                var client = new HaloInfiniteClient(string.Empty, string.Empty);
                var configResult = await ui.WithSpinnerAsync("Fetching endpoint configuration...", async () =>
                {
                    return await client.Configuration.GetApiSettingsContainerAsync();
                });

                if (configResult?.Result?.Endpoints == null)
                {
                    ui.WriteError("Failed to fetch endpoint configuration.");
                    return 1;
                }

                var container = configResult.Result;
                ui.WriteSuccess($"Found {container.Endpoints.Count} endpoints.");

                // Initialize services
                var typeResolver = new ResponseTypeResolver(options.ResponseTypesFile);
                if (typeResolver.MappingCount > 0)
                {
                    ui.WriteSuccess($"Loaded {typeResolver.MappingCount} response type mappings.");
                }

                var endpointParser = new EndpointParser(typeResolver);
                var endpoints = endpointParser.ParseEndpoints(container);
                ui.WriteSuccess($"Parsed {endpoints.Count} endpoints.");
                ui.WriteLine();

                // Run coverage analysis
                var analyzer = new CoverageAnalyzer(typeResolver);
                var report = analyzer.Analyze(endpoints, options.ModulesPath);

                // Generate and output report
                var reportGenerator = new ReportGenerator();
                bool includeImplementationAnalysis = !string.IsNullOrEmpty(options.ModulesPath);

                if (!string.IsNullOrEmpty(options.OutputReport))
                {
                    reportGenerator.WriteToFile(report, options.OutputReport, includeImplementationAnalysis);
                    ui.WriteSuccess($"Report written to: {options.OutputReport}");
                }
                else
                {
                    ui.WriteCoverageReport(report, includeImplementationAnalysis);
                }

                return 0;
            }
            catch (Exception ex)
            {
                ui.WriteFatalError(ex.Message, ex.StackTrace);
                return 1;
            }
        }

        /// <summary>
        /// Parses command line arguments into options.
        /// </summary>
        private static CommandLineOptions ParseArguments(string[] args)
        {
            var options = new CommandLineOptions();

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLowerInvariant())
                {
                    case "--help":
                    case "-h":
                    case "/?":
                        options.ShowHelp = true;
                        break;

                    case "--output":
                    case "-o":
                        if (i + 1 < args.Length)
                        {
                            options.OutputDirectory = args[++i];
                        }

                        break;

                    case "--response-types":
                    case "-r":
                        if (i + 1 < args.Length)
                        {
                            options.ResponseTypesFile = args[++i];
                        }

                        break;

                    case "--dry-run":
                    case "-d":
                        options.DryRun = true;
                        break;

                    case "--analyze-gaps":
                        options.AnalyzeGaps = true;
                        break;

                    case "--modules-path":
                    case "-m":
                        if (i + 1 < args.Length)
                        {
                            options.ModulesPath = args[++i];
                        }

                        break;

                    case "--output-report":
                        if (i + 1 < args.Length)
                        {
                            options.OutputReport = args[++i];
                        }

                        break;
                }
            }

            return options;
        }
    }

    /// <summary>
    /// Command line options for the generator.
    /// </summary>
    internal class CommandLineOptions
    {
        /// <summary>
        /// Gets or sets the output directory for generated files.
        /// </summary>
        public string OutputDirectory { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "Output", "Generated");

        /// <summary>
        /// Gets or sets the path to the response types mapping file.
        /// </summary>
        public string? ResponseTypesFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is a dry run.
        /// </summary>
        public bool DryRun { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show help.
        /// </summary>
        public bool ShowHelp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to run gap analysis.
        /// </summary>
        public bool AnalyzeGaps { get; set; }

        /// <summary>
        /// Gets or sets the path to module source files for implementation analysis.
        /// </summary>
        public string? ModulesPath { get; set; }

        /// <summary>
        /// Gets or sets the output file path for the gap analysis report.
        /// </summary>
        public string? OutputReport { get; set; }
    }
}
