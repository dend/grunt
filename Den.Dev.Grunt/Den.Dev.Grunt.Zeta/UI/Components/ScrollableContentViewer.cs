using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Den.Dev.Grunt.Zeta.Models;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.UI.Components
{
    public class ScrollableContentViewer
    {
        private const int HeaderLines = 4;  // Same as ConsoleLayout
        private const int FooterLines = 2;  // Same as ConsoleLayout
        private const int StatusLineHeight = 1;  // Scroll indicator line

        private readonly ConsoleLayout _layout;

        public ScrollableContentViewer(ConsoleLayout layout)
        {
            _layout = layout;
        }

        public void ShowResponse(ApiCallRecord record, bool verboseDiagnostics)
        {
            // Capture the rendered content to a string
            var content = CaptureRenderedContent(() =>
            {
                ResponseRenderer.RenderResponse(record, verboseDiagnostics);
            });

            ShowScrollableContent(content);
        }

        public void ShowContent(string content)
        {
            ShowScrollableContent(content);
        }

        private string CaptureRenderedContent(Action renderAction)
        {
            var originalOut = Console.Out;
            var stringWriter = new StringWriter();

            try
            {
                // Redirect console output to capture content
                Console.SetOut(stringWriter);

                // We need to capture AnsiConsole output too
                // AnsiConsole writes to Console.Out by default
                var originalAnsiConsole = AnsiConsole.Console;
                AnsiConsole.Console = AnsiConsole.Create(new AnsiConsoleSettings
                {
                    Out = new AnsiConsoleOutput(stringWriter)
                });

                renderAction();

                AnsiConsole.Console = originalAnsiConsole;
            }
            finally
            {
                Console.SetOut(originalOut);
            }

            return stringWriter.ToString();
        }

        private void ShowScrollableContent(string content)
        {
            // Split content into lines, preserving empty lines
            var lines = SplitIntoLines(content);

            int scrollOffset = 0;
            int visibleHeight = CalculateVisibleHeight();

            // If content fits on screen, just show it without scroll controls
            if (lines.Count <= visibleHeight)
            {
                _layout.ClearContent();
                RenderVisibleLines(lines, 0, visibleHeight);
                ShowSimplePrompt();
                return;
            }

            // Interactive scrolling loop
            bool running = true;
            while (running)
            {
                // Check for resize and recalculate if needed
                if (_layout.CheckAndHandleResize())
                {
                    visibleHeight = CalculateVisibleHeight();
                    // Clamp scroll offset to valid range
                    int maxOffset = Math.Max(0, lines.Count - visibleHeight);
                    scrollOffset = Math.Min(scrollOffset, maxOffset);
                }

                _layout.ClearContent();
                RenderVisibleLines(lines, scrollOffset, visibleHeight);
                RenderScrollIndicator(scrollOffset, visibleHeight, lines.Count);

                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        scrollOffset = Math.Max(0, scrollOffset - 1);
                        break;

                    case ConsoleKey.DownArrow:
                        scrollOffset = Math.Min(lines.Count - visibleHeight, scrollOffset + 1);
                        break;

                    case ConsoleKey.PageUp:
                        scrollOffset = Math.Max(0, scrollOffset - visibleHeight);
                        break;

                    case ConsoleKey.PageDown:
                        scrollOffset = Math.Min(lines.Count - visibleHeight, scrollOffset + visibleHeight);
                        break;

                    case ConsoleKey.Home:
                        scrollOffset = 0;
                        break;

                    case ConsoleKey.End:
                        scrollOffset = Math.Max(0, lines.Count - visibleHeight);
                        break;

                    case ConsoleKey.Enter:
                    case ConsoleKey.Escape:
                    case ConsoleKey.Backspace:
                    case ConsoleKey.Q:
                        running = false;
                        break;

                    default:
                        // Any other key also exits
                        running = false;
                        break;
                }
            }
        }

        private List<string> SplitIntoLines(string content)
        {
            var lines = new List<string>();
            var consoleWidth = Console.WindowWidth;

            using var reader = new StringReader(content);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                // Handle lines that are wider than the console
                if (GetVisibleLength(line) > consoleWidth)
                {
                    // Split long lines, but this is tricky with ANSI codes
                    // For simplicity, just add the line as-is and let it wrap naturally
                    lines.Add(line);
                }
                else
                {
                    lines.Add(line);
                }
            }

            return lines;
        }

        private int GetVisibleLength(string text)
        {
            // Strip ANSI escape codes to get actual visible length
            var stripped = System.Text.RegularExpressions.Regex.Replace(text, @"\x1b\[[0-9;]*m", "");
            return stripped.Length;
        }

        private int CalculateVisibleHeight()
        {
            // Total available height minus header, footer, and status line
            return Console.WindowHeight - HeaderLines - FooterLines - StatusLineHeight - 1;
        }

        private void RenderVisibleLines(List<string> lines, int offset, int visibleHeight)
        {
            var endIndex = Math.Min(offset + visibleHeight, lines.Count);

            for (int i = offset; i < endIndex; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }

        private void RenderScrollIndicator(int offset, int visibleHeight, int totalLines)
        {
            int startLine = offset + 1;
            int endLine = Math.Min(offset + visibleHeight, totalLines);

            var indicator = $"[dim]Lines {startLine}-{endLine} of {totalLines} | ↑↓ PgUp/PgDn scroll | any key to continue[/]";

            // Position at the status line area
            Console.WriteLine();
            AnsiConsole.MarkupLine(indicator);
        }

        private void ShowSimplePrompt()
        {
            Console.WriteLine();
            AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
            Console.ReadKey(true);
        }
    }
}
