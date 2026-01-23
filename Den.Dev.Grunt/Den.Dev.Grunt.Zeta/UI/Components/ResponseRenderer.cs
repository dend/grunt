using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Den.Dev.Grunt.Zeta.Models;
using Spectre.Console;
using Spectre.Console.Json;

namespace Den.Dev.Grunt.Zeta.UI.Components
{
    public static class ResponseRenderer
    {
        public static void RenderResponse(ApiCallRecord record)
        {
            RenderResponse(record, verboseDiagnostics: false);
        }

        public static void RenderResponse(ApiCallRecord record, bool verboseDiagnostics)
        {
            // Status line
            var statusIcon = record.IsSuccess ? "[green]●[/]" : "[red]●[/]";
            var statusText = record.IsSuccess ? $"[green]{record.StatusCode} OK[/]" : $"[red]{record.StatusCode} Error[/]";

            AnsiConsole.MarkupLine($"{statusIcon} {statusText}  [dim]•[/]  [yellow]{record.Duration.TotalMilliseconds:F0}ms[/]");
            AnsiConsole.WriteLine();

            // Show request details for errors, or always when verbose diagnostics is enabled
            if (!record.IsSuccess || verboseDiagnostics)
            {
                RenderRequestDetails(record, verboseDiagnostics);
                AnsiConsole.WriteLine();

                // Show response headers panel when verbose and we have headers
                if (verboseDiagnostics && record.ResponseHeaders != null && record.ResponseHeaders.Count > 0)
                {
                    RenderResponseHeaders(record.ResponseHeaders);
                    AnsiConsole.WriteLine();
                }
            }

            if (!string.IsNullOrEmpty(record.ResponseJson))
            {
                RenderJsonPanel(record.ResponseJson, record.IsSuccess);
            }
        }

        private static void RenderRequestDetails(ApiCallRecord record, bool verboseDiagnostics = false)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"[dim]Method:[/]  [white]{record.ModuleName}.{record.MethodName}[/]");
            sb.AppendLine($"[dim]Time:[/]    [white]{record.Timestamp:yyyy-MM-dd HH:mm:ss}[/]");

            // Show HTTP request details when verbose diagnostics is enabled
            if (verboseDiagnostics && !string.IsNullOrEmpty(record.RequestUrl))
            {
                sb.AppendLine();
                sb.AppendLine("[dim]HTTP Request:[/]");
                sb.AppendLine($"  [dim]URL:[/]    [cyan]{Markup.Escape(record.RequestUrl)}[/]");
                sb.AppendLine($"  [dim]Method:[/] [white]{record.RequestMethod ?? "GET"}[/]");

                if (record.RequestHeaders != null && record.RequestHeaders.Count > 0)
                {
                    sb.AppendLine();
                    sb.AppendLine("[dim]Request Headers:[/]");
                    foreach (var header in record.RequestHeaders)
                    {
                        var value = MaskSensitiveHeader(header.Key, header.Value);
                        sb.AppendLine($"  [yellow]{Markup.Escape(header.Key)}:[/] [white]{Markup.Escape(value)}[/]");
                    }
                }

                if (!string.IsNullOrEmpty(record.RequestBody))
                {
                    sb.AppendLine();
                    sb.AppendLine("[dim]Request Body:[/]");
                    sb.AppendLine($"  [white]{Markup.Escape(record.RequestBody)}[/]");
                }
            }

            if (record.ParameterDetails.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("[dim]Parameters:[/]");
                foreach (var param in record.ParameterDetails)
                {
                    var value = string.IsNullOrEmpty(param.Value) ? "[dim](empty)[/]" : $"[cyan]{Markup.Escape(param.Value)}[/]";
                    sb.AppendLine($"  [yellow]{param.Name}[/] [dim]({param.Type}):[/] {value}");
                }
            }

            var panel = new Panel(new Markup(sb.ToString().TrimEnd()))
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Yellow)
                .Header("[yellow]Request[/]")
                .Padding(1, 0, 1, 0);

            AnsiConsole.Write(panel);
        }

        private static string MaskSensitiveHeader(string headerName, string headerValue)
        {
            var sensitiveHeaders = new[] { "x-343-authorization-spartan", "authorization", "x-api-key" };
            if (sensitiveHeaders.Any(h => headerName.Equals(h, StringComparison.OrdinalIgnoreCase)))
            {
                if (headerValue.Length <= 8)
                    return new string('*', headerValue.Length);
                return headerValue.Substring(0, 4) + "..." + headerValue.Substring(headerValue.Length - 4);
            }
            return headerValue;
        }

        private static void RenderResponseHeaders(Dictionary<string, string> headers)
        {
            var sb = new StringBuilder();
            foreach (var header in headers)
            {
                sb.AppendLine($"[yellow]{Markup.Escape(header.Key)}:[/] [white]{Markup.Escape(header.Value)}[/]");
            }

            var panel = new Panel(new Markup(sb.ToString().TrimEnd()))
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Blue)
                .Header("[blue]Response Headers[/]")
                .Padding(1, 0, 1, 0);

            AnsiConsole.Write(panel);
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
