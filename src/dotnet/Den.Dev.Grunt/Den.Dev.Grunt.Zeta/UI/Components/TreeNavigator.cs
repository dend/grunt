using System;
using System.Collections.Generic;
using System.Linq;
using Den.Dev.Grunt.Zeta.Models;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.UI.Components
{
    public static class TreeNavigator
    {
        public static T? Select<T>(
            string title,
            IReadOnlyList<T> items,
            Func<T, string> displaySelector,
            Func<T, int>? countSelector = null,
            bool allowBack = true) where T : class
        {
            if (items.Count == 0)
            {
                AnsiConsole.MarkupLine("[dim]No items available.[/]");
                WaitForKey();
                return null;
            }

            var choices = new List<(string Display, T? Item, bool IsBack)>();

            foreach (var item in items)
            {
                var display = displaySelector(item);
                var count = countSelector?.Invoke(item);
                var displayText = count.HasValue
                    ? $"{display} [dim]({count})[/]"
                    : display;
                choices.Add((displayText, item, false));
            }

            if (allowBack)
            {
                choices.Add(("[dim]← Back[/]", default, true));
            }

            var prompt = new SelectionPrompt<(string Display, T? Item, bool IsBack)>()
                .Title($"[dim]{title}[/]")
                .HighlightStyle(new Style(Theme.Accent))
                .PageSize(20)
                .UseConverter(c => c.Display)
                .AddChoices(choices);

            var selection = AnsiConsole.Prompt(prompt);

            if (selection.IsBack)
            {
                return null;
            }

            return selection.Item;
        }

        public static void DisplayApiTree(IReadOnlyList<ModuleMetadata> modules, string rootLabel)
        {
            var tree = new Tree($"[cyan]{rootLabel}[/]")
                .Style(Style.Parse("dim"))
                .Guide(TreeGuide.Line);

            foreach (var module in modules)
            {
                var moduleNode = tree.AddNode($"[white]{module.DisplayName}[/] [dim]({module.Methods.Count})[/]");

                foreach (var method in module.Methods.Take(5))
                {
                    var paramList = method.Parameters.Length == 0
                        ? ""
                        : $"[dim]({string.Join(", ", method.Parameters.Select(p => p.Name))})[/]";
                    moduleNode.AddNode($"[cyan]{method.DisplayName}[/]{paramList}");
                }

                if (module.Methods.Count > 5)
                {
                    moduleNode.AddNode($"[dim]... and {module.Methods.Count - 5} more[/]");
                }
            }

            AnsiConsole.Write(tree);
            AnsiConsole.WriteLine();
        }

        public static int SelectIndex(
            IReadOnlyList<string> items,
            bool allowBack = true,
            string? title = null)
        {
            var choices = items.Select((item, index) => (Display: item, Index: index, IsBack: false)).ToList();

            if (allowBack)
            {
                choices.Add(("[dim]← Back[/]", -1, true));
            }

            var prompt = new SelectionPrompt<(string Display, int Index, bool IsBack)>()
                .HighlightStyle(new Style(Theme.Accent))
                .PageSize(20)
                .UseConverter(c => c.Display)
                .AddChoices(choices);

            if (!string.IsNullOrEmpty(title))
            {
                prompt.Title($"[dim]{title}[/]");
            }

            var selection = AnsiConsole.Prompt(prompt);
            return selection.Index;
        }

        public static void WaitForKey(string message = "Press any key to continue...")
        {
            AnsiConsole.MarkupLine($"[dim]{message}[/]");
            Console.ReadKey(true);
        }

        public static bool WaitForKeyOrEsc(string message = "Press any key to continue, ESC to go back...")
        {
            AnsiConsole.MarkupLine($"[dim]{message}[/]");
            var key = Console.ReadKey(true);
            return key.Key == ConsoleKey.Escape;
        }
    }
}
