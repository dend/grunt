using System;
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

                var choices = modules
                    .Select(m => $"{m.DisplayName} [dim]({m.Methods.Count})[/]")
                    .Append("[dim]Back[/]")
                    .ToList();

                var prompt = new SelectionPrompt<string>()
                    .PageSize(15)
                    .WrapAround(true)
                    .HighlightStyle(new Style(Color.Cyan1))
                    .EnableSearch()
                    .SearchPlaceholderText("[dim]Type to search...[/]")
                    .AddChoices(choices);

                var selection = AnsiConsole.Prompt(prompt);

                if (selection == "[dim]Back[/]")
                {
                    return (null, null);
                }

                var moduleName = selection.Split(" [dim]")[0];
                var selectedModule = modules.FirstOrDefault(m => m.DisplayName == moduleName);

                if (selectedModule == null)
                {
                    continue;
                }

                var method = SelectMethod(selectedModule);
                if (method != null)
                {
                    return (selectedModule, method);
                }
            }
        }

        private MethodMetadata? SelectMethod(ModuleMetadata module)
        {
            while (true)
            {
                Header.Render(_context, module.DisplayName);

                var choices = module.Methods
                    .Select(m =>
                    {
                        var paramInfo = m.Parameters.Length == 0
                            ? "[dim]()[/]"
                            : $"[dim]({string.Join(", ", m.Parameters.Select(p => p.Name))})[/]";
                        return $"{m.DisplayName}{paramInfo}";
                    })
                    .Append("[dim]Back[/]")
                    .ToList();

                var prompt = new SelectionPrompt<string>()
                    .PageSize(20)
                    .WrapAround(true)
                    .HighlightStyle(new Style(Color.Cyan1))
                    .EnableSearch()
                    .SearchPlaceholderText("[dim]Type to search...[/]")
                    .AddChoices(choices);

                var selection = AnsiConsole.Prompt(prompt);

                if (selection == "[dim]Back[/]")
                {
                    return null;
                }

                var methodName = selection.Split("[dim]")[0];
                return module.Methods.FirstOrDefault(m => m.DisplayName == methodName);
            }
        }
    }
}
