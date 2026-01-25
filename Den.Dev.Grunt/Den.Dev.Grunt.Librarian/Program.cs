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
            Console.WriteLine("Den.Dev.Grunt Librarian - Halo Infinite API Code Generator");
            Console.WriteLine("Developed by Den Delimarsky. Part of https://gruntapi.com");
            Console.WriteLine();

            // Parse command line arguments
            var options = ParseArguments(args);

            if (options.ShowHelp)
            {
                ShowHelp();
                return 0;
            }

            Console.WriteLine($"Output directory: {options.OutputDirectory}");
            Console.WriteLine($"Response types file: {options.ResponseTypesFile ?? "(none)"}");
            Console.WriteLine($"Dry run: {options.DryRun}");
            Console.WriteLine();

            try
            {
                // Initialize the Halo client to fetch endpoint configuration
                Console.WriteLine("Fetching endpoint configuration...");
                var client = new HaloInfiniteClient(string.Empty, string.Empty);
                var configResult = await client.Configuration.GetApiSettingsContainer();

                if (configResult?.Result?.Endpoints == null)
                {
                    Console.WriteLine("Error: Failed to fetch endpoint configuration.");
                    return 1;
                }

                var container = configResult.Result;
                Console.WriteLine($"Found {container.Endpoints.Count} endpoints.");

                // Initialize services
                var typeResolver = new ResponseTypeResolver(options.ResponseTypesFile);
                if (typeResolver.MappingCount > 0)
                {
                    Console.WriteLine($"Loaded {typeResolver.MappingCount} response type mappings.");
                }

                var endpointParser = new EndpointParser(typeResolver);
                var endpoints = endpointParser.ParseEndpoints(container);
                Console.WriteLine($"Parsed {endpoints.Count} endpoints.");

                // Group endpoints into modules
                var modules = ModuleGrouper.GroupByModule(endpoints);
                Console.WriteLine($"Grouped into {modules.Count} modules.");
                Console.WriteLine();

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
                    Console.WriteLine($"Error: Templates directory not found. Searched in:");
                    Console.WriteLine($"  - {Path.Combine(AppContext.BaseDirectory, "Templates")}");
                    Console.WriteLine($"  - {Path.Combine(Directory.GetCurrentDirectory(), "Templates")}");
                    return 1;
                }

                var templateRenderer = new TemplateRenderer(templateDirectory);

                // Generate code
                var codeGenerator = new CodeGenerator(templateRenderer, options.OutputDirectory, options.DryRun);
                var result = codeGenerator.GenerateModules(modules.Values.OrderBy(m => m.Name));

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

                    return 1;
                }

                if (!options.DryRun)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Generated files written to: {options.OutputDirectory}");
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
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
                }
            }

            return options;
        }

        /// <summary>
        /// Shows help information.
        /// </summary>
        private static void ShowHelp()
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
            Console.WriteLine("Examples:");
            Console.WriteLine("  dotnet run                     Generate to default output directory");
            Console.WriteLine("  dotnet run --output C:\\Code    Generate to custom directory");
            Console.WriteLine("  dotnet run --dry-run           Preview what would be generated");
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
    }
}
