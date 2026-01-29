using System;
using System.Linq;
using Den.Dev.Grunt.Zeta.Models;
using Den.Dev.Grunt.Zeta.Services;
using Den.Dev.Grunt.Zeta.UI.Components;
using Spectre.Console;

#pragma warning disable CS8714 // The type cannot be used as type parameter in the generic type or method. Nullability doesn't match 'notnull' constraint.

namespace Den.Dev.Grunt.Zeta.UI.Screens
{
    public class HistoryScreen
    {
        private readonly HistoryService _historyService;
        private readonly ExecutionContext _context;
        private readonly ConsoleLayout _layout;

        public HistoryScreen(HistoryService historyService, ExecutionContext context, ConsoleLayout layout)
        {
            _historyService = historyService;
            _context = context;
            _layout = layout;
        }

        public ApiCallRecord? Show()
        {
            while (true)
            {
                _layout.CheckAndHandleResize();
                _layout.ClearContent();
                _layout.SetBreadcrumbs("History");

                var history = _historyService.History.ToList();

                if (history.Count == 0)
                {
                    AnsiConsole.MarkupLine("[dim]No API calls recorded yet.[/]");
                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine("[dim]Press any key to return...[/]");
                    Console.ReadKey(true);
                    return null;
                }

                var successCount = history.Count(r => r.IsSuccess);
                var failCount = history.Count - successCount;
                var avgDuration = history.Average(r => r.Duration.TotalMilliseconds);

                // Stats as simple line
                AnsiConsole.MarkupLine(
                    $"[dim]Total:[/] [cyan]{history.Count}[/]  " +
                    $"[dim]Success:[/] [green]{successCount}[/]  " +
                    $"[dim]Failed:[/] [red]{failCount}[/]  " +
                    $"[dim]Avg:[/] [yellow]{avgDuration:F0}ms[/]");
                AnsiConsole.WriteLine();

                var prompt = new SelectionPrompt<object?>()
                    .PageSize(18)
                    .WrapAround(true)
                    .HighlightStyle(Theme.Highlight)
                    .EnableSearch()
                    .UseConverter(FormatHistoryChoice)
                    .AddChoices(history.Take(15).Cast<object?>().Append("clear").Append(null));

                var selection = AnsiConsole.Prompt(prompt);

                if (selection == null)
                {
                    return null;
                }

                if (selection is string s && s == "clear")
                {
                    if (AnsiConsole.Confirm("Clear all history?", false))
                    {
                        _historyService.Clear();
                    }
                    continue;
                }

                if (selection is ApiCallRecord record)
                {
                    ShowRecordDetails(record);
                }
            }
        }

        private static string FormatHistoryChoice(object? item)
        {
            if (item == null)
            {
                return "[dim]← Back[/]";
            }

            if (item is string s && s == "clear")
            {
                return "[yellow]Clear history[/]";
            }

            if (item is ApiCallRecord r)
            {
                var indicator = r.IsSuccess ? "[green]●[/]" : "[red]●[/]";
                var ms = r.Duration.TotalMilliseconds;
                return $"{indicator} {r.ModuleName}.[cyan]{r.MethodName}[/]  [dim]•[/]  [dim]{ms:F0}ms[/]";
            }

            return item.ToString() ?? "";
        }

        private void ShowRecordDetails(ApiCallRecord record)
        {
            _layout.ClearContent();
            _layout.SetBreadcrumbs("History", $"{record.ModuleName}.{record.MethodName}");

            var viewer = new ScrollableContentViewer(_layout);
            viewer.ShowResponse(record, verboseDiagnostics: false);
        }
    }
}
