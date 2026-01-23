using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Den.Dev.Grunt.Zeta.Models;
using Den.Dev.Grunt.Zeta.Registry;
using Den.Dev.Grunt.Zeta.Services;
using Den.Dev.Grunt.Zeta.UI;
using Den.Dev.Grunt.Zeta.UI.Components;
using Den.Dev.Grunt.Zeta.UI.Screens;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta
{
    public class App
    {
        private readonly AuthenticationService _authService;
        private readonly HistoryService _historyService;
        private readonly SettingsService _settingsService;
        private readonly ApiMethodRegistry _registry;
        private ApiExecutionService? _executionService;
        private ExecutionContext? _context;
        private ConsoleLayout? _layout;

        public App()
        {
            _authService = new AuthenticationService();
            _historyService = new HistoryService();
            _historyService.Load();
            _settingsService = new SettingsService();
            _registry = new ApiMethodRegistry();
        }

        public async Task RunAsync()
        {
            Console.Title = "Grunt Zeta";

            Header.RenderSimple("Halo Infinite API Testing Tool");
            AnsiConsole.WriteLine();

            _context = await _authService.AuthenticateAsync();
            if (_context == null || !_context.IsAuthenticated)
            {
                AnsiConsole.MarkupLine("[red]●[/] Authentication failed.");
                return;
            }

            // Load and apply saved settings
            var settings = _settingsService.Load();
            _context.VerboseDiagnosticsEnabled = settings.VerboseDiagnosticsEnabled;
            if (_context.HaloClient != null)
                _context.HaloClient.IncludeRawResponses = settings.VerboseDiagnosticsEnabled;
            if (_context.WaypointClient != null)
                _context.WaypointClient.IncludeRawResponses = settings.VerboseDiagnosticsEnabled;

            _executionService = new ApiExecutionService(_historyService);

            AnsiConsole.Status()
                .AutoRefresh(true)
                .Spinner(Theme.Spinner)
                .Start("[yellow]Discovering API methods[/]", ctx =>
                {
                    ctx.Status("[bold blue]Scanning Halo Infinite modules[/]");
                    _registry.DiscoverMethods(_context.HaloClient!, _context.WaypointClient!);
                });

            var totalMethods = _registry.HaloModules.Sum(m => m.Methods.Count) +
                               _registry.WaypointModules.Sum(m => m.Methods.Count);

            AnsiConsole.MarkupLine($"[green]●[/] Discovered [cyan]{totalMethods}[/] API methods");
            System.Threading.Thread.Sleep(500);

            _layout = new ConsoleLayout();
            _layout.Initialize(_context);

            try
            {
                await MainLoopAsync();
            }
            finally
            {
                _layout.Dispose();
            }
        }

        private async Task MainLoopAsync()
        {
            var mainMenuScreen = new MainMenuScreen(_context!, _layout!);
            var apiBrowserScreen = new ApiBrowserScreen(_context!, _layout!);
            var historyScreen = new HistoryScreen(_historyService, _context!, _layout!);
            var sessionInfoScreen = new SessionInfoScreen(_context!, _layout!);
            var settingsScreen = new SettingsScreen(_context!, _settingsService, _layout!);

            while (true)
            {
                _layout!.ClearContent();
                _layout!.SetBreadcrumbs("Main Menu");
                var choice = mainMenuScreen.Show();

                switch (choice)
                {
                    case MainMenuChoice.HaloInfinite:
                        await BrowseApiAsync(apiBrowserScreen, _registry.HaloModules, "Halo Infinite API");
                        break;

                    case MainMenuChoice.Waypoint:
                        await BrowseApiAsync(apiBrowserScreen, _registry.WaypointModules, "Waypoint API");
                        break;

                    case MainMenuChoice.History:
                        _layout!.SetBreadcrumbs("History");
                        historyScreen.Show();
                        break;

                    case MainMenuChoice.SessionInfo:
                        _layout!.SetBreadcrumbs("Session Info");
                        sessionInfoScreen.Show();
                        break;

                    case MainMenuChoice.Settings:
                        _layout!.SetBreadcrumbs("Settings");
                        settingsScreen.Show();
                        break;

                    case MainMenuChoice.Exit:
                        return;
                }
            }
        }

        private async Task BrowseApiAsync(
            ApiBrowserScreen browser,
            IReadOnlyList<ModuleMetadata> modules,
            string apiName)
        {
            _layout!.SetBreadcrumbs(apiName);

            while (true)
            {
                var (module, method) = browser.Browse(modules, apiName);
                if (module == null || method == null)
                {
                    return;
                }

                await ExecuteMethodAsync(apiName, module, method);
            }
        }

        private async Task ExecuteMethodAsync(string apiName, ModuleMetadata module, MethodMetadata method)
        {
            _layout!.ClearContent();
            _layout!.SetBreadcrumbs(apiName, module.DisplayName, method.DisplayName);

            // Method title
            AnsiConsole.MarkupLine($"[bold cyan]{Markup.Escape(method.DisplayName)}[/]");
            AnsiConsole.WriteLine();

            // Parameter table (if any)
            if (method.Parameters.Length > 0)
            {
                var table = new Table()
                    .Border(TableBorder.Rounded)
                    .AddColumn("[dim]Parameter[/]")
                    .AddColumn("[dim]Type[/]")
                    .AddColumn("[dim]Status[/]");

                foreach (var p in method.Parameters)
                {
                    table.AddRow(
                        $"[cyan]{p.Name}[/]",
                        $"[dim]{p.ParameterType.Name}[/]",
                        p.IsOptional ? "[yellow]Optional[/]" : "[green]Required[/]");
                }
                AnsiConsole.Write(table);
                AnsiConsole.WriteLine();
            }
            else
            {
                AnsiConsole.MarkupLine("[dim]No parameters required[/]");
                AnsiConsole.WriteLine();
            }

            // Cancellable parameter collection
            var result = ParameterForm.CollectParametersWithCancel(method.Parameters, _context!.Xuid);

            if (result.WasCancelled)
            {
                AnsiConsole.MarkupLine("[yellow]Cancelled[/]");
                System.Threading.Thread.Sleep(300);
                return; // Back to method selection
            }

            AnsiConsole.WriteLine();
            if (!AnsiConsole.Confirm("Execute API call?", true))
            {
                return;
            }

            ApiCallRecord? record = null;

            await AnsiConsole.Status()
                .AutoRefresh(true)
                .Spinner(Theme.Spinner)
                .StartAsync($"[yellow]Executing API call[/]", async ctx =>
                {
                    ctx.Status($"[bold blue]Calling {method.DisplayName}[/]");
                    record = await _executionService!.ExecuteMethodAsync(module, method, result.Values);
                });

            _layout!.ClearContent();
            _layout!.SetBreadcrumbs(apiName, module.DisplayName, method.DisplayName);

            ResponseRenderer.RenderResponse(record!, _context!.VerboseDiagnosticsEnabled);

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
            Console.ReadKey(true);
        }
    }
}
