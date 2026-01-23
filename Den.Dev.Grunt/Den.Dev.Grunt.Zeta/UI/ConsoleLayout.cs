using System;
using System.Text;
using Den.Dev.Grunt.Zeta.Models;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.UI
{
    public class ConsoleLayout
    {
        private const int HeaderLines = 4;  // Status panel (3 lines) + rule
        private const int FooterLines = 2;  // Rule + breadcrumb line

        private string[] _currentBreadcrumbs = Array.Empty<string>();
        private ExecutionContext? _context;
        private int _lastWidth;
        private int _lastHeight;

        public void Initialize(ExecutionContext context)
        {
            _context = context;
            _lastWidth = Console.WindowWidth;
            _lastHeight = Console.WindowHeight;
            SetupLayout();
        }

        public bool CheckAndHandleResize()
        {
            int currentWidth = Console.WindowWidth;
            int currentHeight = Console.WindowHeight;

            if (currentWidth != _lastWidth || currentHeight != _lastHeight)
            {
                _lastWidth = currentWidth;
                _lastHeight = currentHeight;
                Refresh();
                return true;
            }
            return false;
        }

        public void SetBreadcrumbs(params string[] breadcrumbs)
        {
            _currentBreadcrumbs = breadcrumbs;
            RenderFooter();
        }

        public void Refresh()
        {
            SetupLayout();
        }

        private void SetupLayout()
        {
            var height = Console.WindowHeight;
            var contentStart = HeaderLines + 1;
            var contentEnd = height - FooterLines;

            // Clear screen and reset
            Console.Write("\x1b[2J");
            Console.Write("\x1b[H");

            // Render header at top (rows 1-4)
            RenderHeader();

            // Set scroll region for content area only
            Console.Write($"\x1b[{contentStart};{contentEnd}r");

            // Render footer at bottom
            RenderFooter();

            // Position cursor in content area
            Console.Write($"\x1b[{contentStart};1H");
        }

        private void RenderHeader()
        {
            // Position at top
            Console.Write("\x1b[1;1H");

            if (_context == null) return;

            var gamertag = Markup.Escape(_context.Gamertag ?? "Unknown");
            var xuid = _context.Xuid ?? "N/A";
            var clearance = string.IsNullOrEmpty(_context.ClearanceToken)
                ? "None"
                : _context.ClearanceToken;

            var statusContent = new Markup(
                $"[bold cyan]Grunt Zeta[/] [dim]│[/] " +
                $"[dim]GT:[/] [cyan]{gamertag}[/] [dim]│[/] " +
                $"[dim]XUID:[/] [cyan]{xuid}[/] [dim]│[/] " +
                $"[dim]Clearance:[/] [cyan]{Markup.Escape(clearance)}[/]");

            var panel = new Panel(statusContent)
                .Border(BoxBorder.Rounded)
                .BorderColor(Theme.Muted)
                .Padding(0, 0, 0, 0);

            AnsiConsole.Write(panel);
            AnsiConsole.Write(new Rule().RuleStyle("dim"));
        }

        private void RenderFooter()
        {
            var height = Console.WindowHeight;
            var footerRow = height - FooterLines + 1;

            // Save cursor position
            Console.Write("\x1b[s");

            // Move to footer area and clear BOTH lines
            Console.Write($"\x1b[{footerRow};1H");
            Console.Write("\x1b[K");  // Clear rule line
            Console.Write($"\x1b[{footerRow + 1};1H");
            Console.Write("\x1b[K");  // Clear breadcrumb line
            Console.Write($"\x1b[{footerRow};1H");  // Back to start

            AnsiConsole.Write(new Rule().RuleStyle("dim"));

            if (_currentBreadcrumbs.Length > 0)
            {
                var sb = new StringBuilder();
                for (int i = 0; i < _currentBreadcrumbs.Length; i++)
                {
                    if (i == _currentBreadcrumbs.Length - 1)
                    {
                        sb.Append($"[bold cyan]{Markup.Escape(_currentBreadcrumbs[i])}[/]");
                    }
                    else
                    {
                        sb.Append($"[dim]{Markup.Escape(_currentBreadcrumbs[i])}[/]");
                        sb.Append($"[yellow]{Theme.BreadcrumbSeparator}[/]");
                    }
                }
                AnsiConsole.MarkupLine(sb.ToString());
            }
            else
            {
                AnsiConsole.MarkupLine("[dim]Ready[/]");
            }

            // Restore cursor position
            Console.Write("\x1b[u");
        }

        public void ClearContent()
        {
            var height = Console.WindowHeight;
            var contentStart = HeaderLines + 1;
            var contentEnd = height - FooterLines;

            // Position at content start
            Console.Write($"\x1b[{contentStart};1H");

            // Clear each line in content area
            for (int i = contentStart; i <= contentEnd; i++)
            {
                Console.Write("\x1b[K");
                if (i < contentEnd) Console.WriteLine();
            }

            // Return to content start
            Console.Write($"\x1b[{contentStart};1H");
        }

        public void Dispose()
        {
            // Reset scroll region to full screen
            Console.Write("\x1b[r");
            Console.Clear();
        }
    }
}
