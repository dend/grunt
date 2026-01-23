using System;
using System.Linq;
using Den.Dev.Grunt.Zeta.Models;
using Den.Dev.Grunt.Zeta.Services;
using Den.Dev.Grunt.Zeta.UI.Components;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.UI.Screens
{
    public class HistoryScreen
    {
        private readonly HistoryService _historyService;
        private readonly ExecutionContext _context;

        public HistoryScreen(HistoryService historyService, ExecutionContext context)
        {
            _historyService = historyService;
            _context = context;
        }

        public ApiCallRecord? Show()
        {
            while (true)
            {
                Header.Render(_context, "History");

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

                // Build choices as simple strings
                var displayChoices = history
                    .Take(15)
                    .Select(r =>
                    {
                        var status = r.IsSuccess ? "[green]OK[/]" : "[red]ERR[/]";
                        return $"{status} {r.ModuleName}.{r.MethodName} ({r.Duration.TotalMilliseconds:F0}ms)";
                    })
                    .ToList();

                displayChoices.Add("Clear History");
                displayChoices.Add(".. Back");

                var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select a call to view details")
                        .HighlightStyle(new Style(Color.Cyan1))
                        .PageSize(18)
                        .AddChoices(displayChoices));

                if (selection == ".. Back")
                {
                    return null;
                }

                if (selection == "Clear History")
                {
                    if (AnsiConsole.Confirm("Clear all history?", false))
                    {
                        _historyService.Clear();
                    }
                    continue;
                }

                // Find the record by matching
                var index = displayChoices.IndexOf(selection);
                if (index >= 0 && index < history.Count)
                {
                    ShowRecordDetails(history[index]);
                }
            }
        }

        private void ShowRecordDetails(ApiCallRecord record)
        {
            Header.Render(_context, $"{record.ModuleName}.{record.MethodName}");

            ResponseRenderer.RenderResponse(record);

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[dim]Press any key to return...[/]");
            Console.ReadKey(true);
        }
    }
}
