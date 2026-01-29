using System.Collections.Generic;
using System.Linq;
using Den.Dev.Grunt.Zeta.Models;
using Spectre.Console;

#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability doesn't match 'notnull' constraint.
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate.

namespace Den.Dev.Grunt.Zeta.UI.Screens
{
    public class ApiBrowserScreen
    {
        private readonly ExecutionContext _context;
        private readonly ConsoleLayout _layout;
        private string _currentApiName = string.Empty;

        public ApiBrowserScreen(ExecutionContext context, ConsoleLayout layout)
        {
            _context = context;
            _layout = layout;
        }

        public (ModuleMetadata? Module, MethodMetadata? Method) Browse(
            IReadOnlyList<ModuleMetadata> modules,
            string apiName)
        {
            _currentApiName = apiName;

            while (true)
            {
                _layout.ClearContent();
                _layout.SetBreadcrumbs(apiName);

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
                _layout.ClearContent();
                _layout.SetBreadcrumbs(_currentApiName, module.DisplayName);

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
                return $"[cyan]{m.DisplayName}[/] [dim]()[/]";
            }

            var paramNames = string.Join(", ", m.Parameters.Select(p => p.Name));
            return $"[cyan]{m.DisplayName}[/] [dim]({paramNames})[/]";
        }
    }
}
