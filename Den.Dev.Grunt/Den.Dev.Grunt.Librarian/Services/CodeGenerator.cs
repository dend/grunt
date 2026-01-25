// <copyright file="CodeGenerator.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using Den.Dev.Grunt.Librarian.Models;

namespace Den.Dev.Grunt.Librarian.Services
{
    /// <summary>
    /// Service that orchestrates the code generation process.
    /// </summary>
    public class CodeGenerator
    {
        private readonly TemplateRenderer templateRenderer;
        private readonly string outputDirectory;
        private readonly bool dryRun;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGenerator"/> class.
        /// </summary>
        /// <param name="templateRenderer">The template renderer to use.</param>
        /// <param name="outputDirectory">The directory to write generated files.</param>
        /// <param name="dryRun">If true, only previews output without writing files.</param>
        public CodeGenerator(TemplateRenderer templateRenderer, string outputDirectory, bool dryRun = false)
        {
            this.templateRenderer = templateRenderer ?? throw new ArgumentNullException(nameof(templateRenderer));
            this.outputDirectory = outputDirectory ?? throw new ArgumentNullException(nameof(outputDirectory));
            this.dryRun = dryRun;
        }

        /// <summary>
        /// Generates code files for all provided modules.
        /// </summary>
        /// <param name="modules">The modules to generate.</param>
        /// <returns>A summary of the generation results.</returns>
        public GenerationResult GenerateModules(IEnumerable<ModuleDefinition> modules)
        {
            var result = new GenerationResult();

            // Ensure output directory exists (unless dry run)
            if (!this.dryRun && !Directory.Exists(this.outputDirectory))
            {
                Directory.CreateDirectory(this.outputDirectory);
            }

            foreach (var module in modules)
            {
                try
                {
                    var code = this.templateRenderer.RenderModule(module);
                    var filePath = Path.Combine(this.outputDirectory, module.FileName);

                    if (this.dryRun)
                    {
                        Console.WriteLine($"[DRY RUN] Would generate: {module.FileName}");
                        Console.WriteLine($"  Methods: {module.Methods.Count}");
                        result.FilesGenerated.Add(module.FileName);
                    }
                    else
                    {
                        File.WriteAllText(filePath, code);
                        Console.WriteLine($"Generated: {module.FileName} ({module.Methods.Count} methods)");
                        result.FilesGenerated.Add(filePath);
                    }

                    result.TotalMethodsGenerated += module.Methods.Count;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error generating {module.FileName}: {ex.Message}");
                    result.Errors.Add($"{module.FileName}: {ex.Message}");
                }
            }

            return result;
        }
    }

    /// <summary>
    /// Contains the results of a code generation run.
    /// </summary>
    public class GenerationResult
    {
        /// <summary>
        /// Gets the list of files that were generated.
        /// </summary>
        public List<string> FilesGenerated { get; } = new();

        /// <summary>
        /// Gets the total number of methods generated.
        /// </summary>
        public int TotalMethodsGenerated { get; set; }

        /// <summary>
        /// Gets any errors that occurred during generation.
        /// </summary>
        public List<string> Errors { get; } = new();

        /// <summary>
        /// Gets a value indicating whether the generation was successful.
        /// </summary>
        public bool Success => this.Errors.Count == 0;
    }
}
