![Grunt logo](https://raw.githubusercontent.com/dend/grunt/master/media/grunt-logo.png)

# Grunt - The Halo API Wrapper

Welcome to **Grunt** - the unofficial way to use official undocumented Halo APIs. Here be **a lot of dragons**.

>[!NOTE]
>This is a hobby project that is not intended for production or customer-critical applications. Use at your own risk.

This API enables a developer to:

- Get stats on matches you played.
- Get your personal player stats.
- Track your campaign progress.

And more!

## Available Libraries

Grunt is available in two flavors:

| Library | Package | Status |
|:--------|:--------|:-------|
| **.NET (C#)** | [![NuGet](https://img.shields.io/nuget/v/Den.Dev.Grunt)](https://www.nuget.org/packages/Den.Dev.Grunt) [![NuGet Downloads](https://img.shields.io/nuget/dt/Den.Dev.Grunt)](https://www.nuget.org/packages/Den.Dev.Grunt) | [![Publish NuGet Package](https://github.com/dend/grunt/actions/workflows/package.yml/badge.svg)](https://github.com/dend/grunt/actions/workflows/package.yml) |
| **Node.js (TypeScript)** | [![npm](https://img.shields.io/npm/v/@anthropic/grunt)](https://www.npmjs.com/package/@dendev/grunt) | [![Publish npm Package](https://github.com/dend/grunt/actions/workflows/npm-package.yml/badge.svg)](https://github.com/dend/grunt/actions/workflows/npm-package.yml) |

## Quick Start

### .NET

```bash
dotnet add package Den.Dev.Grunt
```

```csharp
HaloInfiniteClient client = new("<YOUR_SPARTAN_TOKEN>", clearanceToken: "<YOUR_CLEARANCE_TOKEN>");

var matchStats = await client.Stats.GetMatchStatsAsync("21416434-4717-4966-9902-af7097469f74");
Console.WriteLine("Match data retrieved!");
```

### Node.js

```bash
npm install @dendev/grunt
```

```typescript
import { HaloInfiniteClient, isSuccess } from '@dendev/grunt';

const client = new HaloInfiniteClient({
  spartanToken: 'your-spartan-token',
  xuid: 'your-xbox-user-id',
});

const matchStats = await client.stats.getMatchStats('21416434-4717-4966-9902-af7097469f74');
if (isSuccess(matchStats)) {
  console.log('Match data retrieved!');
}
```

## Documentation

| Library | Documentation |
|:--------|:--------------|
| .NET | [API Documentation](https://docs.gruntapi.com) &bull; [Detailed README](src/dotnet/Den.Dev.Grunt/README.md) |
| Node.js | [README & Examples](src/node/README.md) |

## Authentication

Both libraries require a **Spartan token** to access most Halo Infinite API endpoints. There are two ways to obtain one:

### Option 1: Bring Your Own Token

Obtain the token by inspecting network traffic from [Halo Waypoint](https://halowaypoint.com) using your browser's developer tools:

1. Open [halowaypoint.com](https://halowaypoint.com) and sign in
2. Open your browser's Network Inspector (F12 → Network tab)
3. Look for API calls that return JSON data
4. Find the `x-343-authorization-spartan` header - that's your Spartan token
5. Some endpoints also require the `343-clearance` header

![Acquiring the Spartan token from the Halo Waypoint website](https://raw.githubusercontent.com/dend/grunt/master/media/spartan-token.png)

> **Note:** Tokens expire frequently. If you receive `401 Unauthorized` errors, you need a new token.

### Option 2: Full Authentication Flow

Both libraries provide helper methods to programmatically generate Spartan tokens using Azure Active Directory. This requires:

1. [Register an Azure AD application](https://docs.microsoft.com/azure/active-directory/develop/quickstart-register-app)
2. Use `https://localhost` as the redirect URI (for personal use)
3. Follow the authentication examples in the library-specific documentation

## Project Structure

```
grunt/
├── src/
│   ├── dotnet/           # .NET library and tools
│   │   └── Den.Dev.Grunt/
│   │       ├── Den.Dev.Grunt/           # Core library
│   │       ├── Den.Dev.Grunt.Zeta/      # Testing/experimentation
│   │       ├── Den.Dev.Grunt.Librarian/ # Code generator
│   │       ├── Den.Dev.Grunt.Composer/  # Data composition
│   │       └── Den.Dev.Grunt.Auditor/   # API validation
│   └── node/             # TypeScript library
├── docs/                 # API documentation source
└── media/                # Images and assets
```

## Features

Both libraries provide:

- **Type-safe API access** - Strongly-typed request/response models
- **Comprehensive coverage** - Stats, Economy, UGC, GameCMS, and more
- **Authentication helpers** - Built-in support for the Xbox Live → Spartan token flow
- **Caching support** - ETag-based caching to reduce API calls
- **Retry logic** - Automatic retry with backoff for transient failures

### .NET-Specific Tools

| Tool | Description |
|:-----|:------------|
| `Den.Dev.Grunt.Librarian` | Code generator that produces API client modules from endpoint definitions |
| `Den.Dev.Grunt.Auditor` | Validates models against live API responses |
| `Den.Dev.Grunt.Composer` | Data composition and transformation utilities |

## Endpoints

Complete list of endpoints can be obtained by querying the official Halo Infinite API:

```
https://settings.svc.halowaypoint.com/settings/hipc/e2a0a7c6-6efe-42af-9283-c2ab73250c48
```

This endpoint does not require authentication. You can also view an offline version in the library source:
- [.NET endpoints.json](src/dotnet/Den.Dev.Grunt/Den.Dev.Grunt/endpoints.json)
- [Node.js endpoints](src/node/src/endpoints/)

## FAQ

**Q1: Is this in any way endorsed by Halo Studios or Microsoft?**

No. Not at all. This is something I've put together by inspecting network traffic. This project is not funded, supported, or otherwise endorsed by either 343 Industries or Microsoft.

**Q2: Something is broken and my production site that uses your library doesn't work. Can you help?**

Don't use any of this code in production. It's nowhere near stable, and will never be.

**Q3: Some API endpoint is not working anymore or returns an unexpected result. What's up with that?**

[Open an issue](https://github.com/dend/grunt/issues) so that I can investigate.

**Q4: How do I contact the author?**

[Open an issue](https://github.com/dend/grunt/issues) or reach out [on the website](https://den.dev).

**Q5: Can this be used for commercial purposes?**

_Absolutely not_. This project is exploratory in nature. It has no guarantees, implied or otherwise, of your ability to consume the API. It does not give you any permission to use this in commercial projects, and neither does it guarantee API access or stability. If you are looking at building something serious using the Halo API, you need to reach out to [343 Industries](https://www.343industries.com/studio).

## Contributions

Contributions are welcome, but please first [open an issue](https://github.com/dend/grunt/issues) so that we can discuss before writing any code.

## License

MIT License - see [LICENSE](LICENSE) for details.
