# Grunt Zeta

A console-based interactive tool for exploring and testing Halo Infinite APIs.

## Overview

Zeta provides a terminal UI for browsing and executing API methods from the Halo Infinite and Waypoint clients. It automatically discovers available API methods, collects parameters, executes calls, and displays formatted JSON responses.

## Features

- **API Browser**: Navigate through Halo Infinite and Waypoint API modules and methods
- **Parameter Collection**: Interactive prompts for required and optional parameters
- **Response Viewer**: Syntax-highlighted JSON output with status codes
- **Request History**: Browse and review past API calls
- **Session Info**: View current authentication state and tokens
- **Verbose Diagnostics**: Inspect raw HTTP request/response details

## Data Storage

All persistent data is stored in the local application data folder:

| File | Path | Description |
|------|------|-------------|
| Settings | `%LOCALAPPDATA%\Den.Dev\Grunt.Zeta\settings.json` | Application preferences |
| History | `%LOCALAPPDATA%\Den.Dev\Grunt.Zeta\history.json` | API call history (up to 100 records) |

### History Persistence

API call history is automatically saved after each request and restored when the application starts. Each record includes:

- Timestamp and duration
- Module and method names
- Parameter values
- Response JSON and status code
- HTTP diagnostic data (when verbose mode is enabled)

History is capped at 100 records. Older entries are removed when the limit is reached.

## Verbose Diagnostics

Enable verbose diagnostics from the **Settings** menu when you need to debug API issues. When enabled, each API response displays additional HTTP details:

- Request URL and HTTP method
- Request headers
- Request body (for POST/PUT requests)
- Response headers

### When to Use Verbose Mode

- **Debugging authentication issues**: Inspect authorization headers and token formats
- **Troubleshooting failed requests**: View the exact URL and parameters sent
- **Understanding API behavior**: See response headers like rate limits or cache directives
- **Reporting bugs**: Capture full request/response details for issue reports

Verbose mode adds overhead to history storage since raw HTTP data is captured. Disable it during normal use.

## Usage

```
dotnet run --project Den.Dev.Grunt.Zeta
```

The tool will authenticate via the configured credential flow, discover available API methods, and present the main menu.
