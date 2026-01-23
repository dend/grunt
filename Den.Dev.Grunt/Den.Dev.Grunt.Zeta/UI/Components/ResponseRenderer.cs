using System;
using Den.Dev.Grunt.Zeta.Models;
using Spectre.Console;
using Spectre.Console.Json;

namespace Den.Dev.Grunt.Zeta.UI.Components
{
    public static class ResponseRenderer
    {
        public static void RenderResponse(ApiCallRecord record)
        {
            // Status line
            var statusIcon = record.IsSuccess ? "[green]●[/]" : "[red]●[/]";
            var statusText = record.IsSuccess ? $"[green]{record.StatusCode} OK[/]" : $"[red]{record.StatusCode} Error[/]";

            AnsiConsole.MarkupLine($"{statusIcon} {statusText}  [dim]•[/]  [yellow]{record.Duration.TotalMilliseconds:F0}ms[/]");
            AnsiConsole.WriteLine();

            if (!string.IsNullOrEmpty(record.ResponseJson))
            {
                RenderJsonPanel(record.ResponseJson, record.IsSuccess);
            }
        }

        public static void RenderJsonPanel(string json, bool isSuccess = true)
        {
            try
            {
                var jsonText = new JsonText(json)
                    .BracesColor(Color.Grey)
                    .BracketColor(Color.Grey)
                    .ColonColor(Color.Grey)
                    .CommaColor(Color.Grey)
                    .StringColor(Color.Green)
                    .NumberColor(Color.Cyan1)
                    .BooleanColor(Color.Yellow)
                    .NullColor(Color.Grey)
                    .MemberColor(Color.White);

                var panel = new Panel(jsonText)
                    .Border(BoxBorder.Rounded)
                    .BorderColor(isSuccess ? Color.Green : Color.Red)
                    .Header("[dim]Response[/]")
                    .Expand();

                AnsiConsole.Write(panel);
            }
            catch (Exception)
            {
                // Fallback for invalid JSON
                var panel = new Panel(Markup.Escape(json))
                    .Border(BoxBorder.Rounded)
                    .BorderColor(Color.Grey)
                    .Header("[dim]Response (Raw)[/]")
                    .Expand();

                AnsiConsole.Write(panel);
            }
        }

        public static void RenderJson(string json)
        {
            try
            {
                var jsonText = new JsonText(json)
                    .BracesColor(Color.Grey)
                    .BracketColor(Color.Grey)
                    .ColonColor(Color.Grey)
                    .CommaColor(Color.Grey)
                    .StringColor(Color.Green)
                    .NumberColor(Color.Cyan1)
                    .BooleanColor(Color.Yellow)
                    .NullColor(Color.Grey)
                    .MemberColor(Color.White);

                AnsiConsole.Write(jsonText);
                AnsiConsole.WriteLine();
            }
            catch (Exception)
            {
                AnsiConsole.WriteLine(json);
            }
        }

        public static void RenderError(string message)
        {
            var panel = new Panel($"[red]{Markup.Escape(message)}[/]")
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Red)
                .Header("[red]Error[/]");

            AnsiConsole.Write(panel);
        }

        public static void RenderSuccess(string message)
        {
            AnsiConsole.MarkupLine($"[green]●[/] {Markup.Escape(message)}");
        }

        public static void RenderInfo(string title, string content)
        {
            var panel = new Panel(content)
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Cyan1)
                .Header($"[cyan]{title}[/]");

            AnsiConsole.Write(panel);
        }
    }
}
