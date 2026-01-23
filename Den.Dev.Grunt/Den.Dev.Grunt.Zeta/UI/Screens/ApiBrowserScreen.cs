using System.Collections.Generic;
using System.Linq;
using Den.Dev.Grunt.Zeta.Models;
using Den.Dev.Grunt.Zeta.UI.Components;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.UI.Screens
{
    public class ApiBrowserScreen
    {
        private readonly ExecutionContext _context;

        public ApiBrowserScreen(ExecutionContext context)
        {
            _context = context;
        }

        public (ModuleMetadata? Module, MethodMetadata? Method) Browse(
            IReadOnlyList<ModuleMetadata> modules,
            string apiName)
        {
            while (true)
            {
                Header.Render(_context, apiName);

                var prompt = new SelectionPrompt<ModuleMetadata?>()
                    .PageSize(Theme.DefaultPageSize)
                    .WrapAround(true)
                    .HighlightStyle(Theme.Highlight)
                    .EnableSearch()
                    .UseConverter(m => m == null
                        ? "[dim]← Back[/]"
                        : $"[white]{m.DisplayName}[/] [dim]({m.Methods.Count})[/]")
                    .AddChoices(modules.Cast<ModuleMetadata?>().Append(null));

                var selection = AnsiConsole.Prompt(prompt);

                if (selection == null)
                {
                    return (null, null);
                }

                var method = SelectMethod(selection);
                if (method != null)
                {
                    return (selection, method);
                }
            }
        }

        private MethodMetadata? SelectMethod(ModuleMetadata module)
        {
            while (true)
            {
                Header.Render(_context, module.DisplayName);

                var prompt = new SelectionPrompt<MethodMetadata?>()
                    .PageSize(Theme.LargePageSize)
                    .WrapAround(true)
                    .HighlightStyle(Theme.Highlight)
                    .EnableSearch()
                    .UseConverter(m => m == null
                        ? "[dim]← Back[/]"
                        : FormatMethod(m))
                    .AddChoices(module.Methods.Cast<MethodMetadata?>().Append(null));

                var selection = AnsiConsole.Prompt(prompt);

                if (selection == null)
                {
                    return null;
                }

                return selection;
            }
        }

        private static string FormatMethod(MethodMetadata m)
        {
            if (m.Parameters.Length == 0)
            {
                return $"[cyan]{m.DisplayName}[/][dim]()[/]";
            }

            var paramNames = string.Join(", ", m.Parameters.Select(p => p.Name));
            return $"[cyan]{m.DisplayName}[/][dim]({paramNames})[/]";
        }
    }
}
