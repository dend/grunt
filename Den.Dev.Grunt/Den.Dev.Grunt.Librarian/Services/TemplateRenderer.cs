// <copyright file="TemplateRenderer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System;
using System.IO;
using Den.Dev.Grunt.Librarian.Models;
using Scriban;
using Scriban.Runtime;

namespace Den.Dev.Grunt.Librarian.Services
{
    /// <summary>
    /// Service for rendering Scriban templates to generate C# code.
    /// </summary>
    public class TemplateRenderer
    {
        private readonly Template moduleTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateRenderer"/> class.
        /// </summary>
        /// <param name="templateDirectory">The directory containing the Scriban templates.</param>
        public TemplateRenderer(string templateDirectory)
        {
            if (string.IsNullOrEmpty(templateDirectory))
            {
                throw new ArgumentNullException(nameof(templateDirectory));
            }

            var moduleTemplatePath = Path.Combine(templateDirectory, "Module.scriban");

            if (!File.Exists(moduleTemplatePath))
            {
                throw new FileNotFoundException($"Module template not found at: {moduleTemplatePath}");
            }

            var templateContent = File.ReadAllText(moduleTemplatePath);
            this.moduleTemplate = Template.Parse(templateContent);

            if (this.moduleTemplate.HasErrors)
            {
                throw new InvalidOperationException(
                    $"Template parsing errors: {string.Join(", ", this.moduleTemplate.Messages)}");
            }
        }

        /// <summary>
        /// Renders a module definition to C# code.
        /// </summary>
        /// <param name="module">The module definition to render.</param>
        /// <returns>The generated C# code.</returns>
        public string RenderModule(ModuleDefinition module)
        {
            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }

            var scriptObject = new ScriptObject();
            scriptObject.Import(new
            {
                module = new
                {
                    name = module.Name,
                    origin = module.Origin,
                    file_name = module.FileName,
                    methods = ConvertMethods(module),
                },
            });

            var context = new TemplateContext();
            context.PushGlobal(scriptObject);
            context.MemberRenamer = member => member.Name;

            return this.moduleTemplate.Render(context);
        }

        /// <summary>
        /// Converts method definitions to a format suitable for Scriban templates.
        /// </summary>
        private static object[] ConvertMethods(ModuleDefinition module)
        {
            var methods = new object[module.Methods.Count];

            for (int i = 0; i < module.Methods.Count; i++)
            {
                var method = module.Methods[i];
                methods[i] = new
                {
                    endpoint_id = method.EndpointId,
                    name = method.Name,
                    http_method = method.HttpMethod,
                    url_template = method.UrlTemplate,
                    response_type = method.ResponseType,
                    parameter_signature = method.ParameterSignature,
                    use_clearance = method.UseClearance,
                    use_spartan_token = method.UseSpartanToken,
                    needs_review = method.NeedsReview,
                    parameters = ConvertParameters(method),
                };
            }

            return methods;
        }

        /// <summary>
        /// Converts parameters to a format suitable for Scriban templates.
        /// </summary>
        private static object[] ConvertParameters(MethodDefinition method)
        {
            var parameters = new object[method.Parameters.Count];

            for (int i = 0; i < method.Parameters.Count; i++)
            {
                var param = method.Parameters[i];
                parameters[i] = new
                {
                    name = param.Name,
                    type = param.Type,
                    description = param.Description,
                    is_query_parameter = param.IsQueryParameter,
                };
            }

            return parameters;
        }
    }
}
