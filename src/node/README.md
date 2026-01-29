# @dendev/grunt

Unofficial TypeScript client library for the Halo Infinite API.

This is the TypeScript implementation of the Grunt library, providing type-safe access to Halo Infinite and Halo Waypoint APIs. For the .NET version, see the [dotnet folder](../dotnet/).

## Installation

```bash
npm install @dendev/grunt
```

## Quick Start

```typescript
import {
  HaloInfiniteClient,
  MatchType,
  LifecycleMode,
  isSuccess,
} from '@dendev/grunt';

// Create a client with your Spartan token
const client = new HaloInfiniteClient({
  spartanToken: 'your-spartan-token',
  xuid: '2533274855333605', // Your Xbox User ID
});

// Get match history
const history = await client.stats.getMatchHistory(
  '2533274855333605',
  0,    // start index
  25,   // count (max 25)
  MatchType.All
);

if (isSuccess(history)) {
  console.log(`Found ${history.result.resultCount} matches`);
  for (const match of history.result.results ?? []) {
    console.log(`Match: ${match.matchId}`);
  }
}
```

## Authentication

To use authenticated endpoints, you need a Spartan token. The authentication flow is:

1. Authenticate with Xbox Live to get an XSTS token
2. Exchange the XSTS token for a Spartan token using `HaloAuthenticationClient`

```typescript
import { HaloAuthenticationClient, HaloInfiniteClient } from '@dendev/grunt';

// Create auth client
const authClient = new HaloAuthenticationClient();

// Exchange XSTS token for Spartan token
// (You need to obtain the XSTS token through Xbox Live authentication first)
const spartanToken = await authClient.getSpartanToken(xstsToken);

if (spartanToken) {
  // Create the API client with the Spartan token
  const client = new HaloInfiniteClient({
    spartanToken: spartanToken.token!,
    xuid: 'your-xuid',
  });
}
```

The XSTS token must be obtained using the Halo Waypoint relying party:
```typescript
const relyingParty = HaloAuthenticationClient.getRelyingParty();
// Returns: 'https://prod.xsts.halowaypoint.com/'
```

## API Overview

### HaloInfiniteClient

The main client for Halo Infinite APIs, with 12 specialized modules:

| Module | Description |
|--------|-------------|
| `stats` | Match history, service records, match stats |
| `skill` | CSR (Competitive Skill Rank) queries |
| `economy` | Inventory, stores, customization, currency |
| `gameCms` | Item definitions, challenges, medals, career ranks |
| `ugc` | User-generated content authoring |
| `ugcDiscovery` | Search and browse user content |
| `academy` | Bot customization, drills |
| `lobby` | QoS servers, lobby presence |
| `settings` | Clearance levels, feature flags |
| `configuration` | API endpoint discovery |
| `banProcessor` | Ban status queries |
| `textModeration` | Text moderation keys |

### WaypointClient

Client for Halo Waypoint APIs:

| Module | Description |
|--------|-------------|
| `profile` | User profiles and settings |
| `redemption` | Code redemption |
| `content` | News articles |
| `comms` | Notifications |

## Usage Examples

### Get Player Service Record

```typescript
const record = await client.stats.getPlayerServiceRecordByXuid(
  '2533274855333605',
  LifecycleMode.Matchmade
);

if (isSuccess(record)) {
  const stats = record.result.stats?.coreStats;
  console.log(`K/D: ${stats?.kills}/${stats?.deaths}`);
}
```

### Get Match Details

```typescript
const match = await client.stats.getMatchStats('match-guid-here');

if (isSuccess(match)) {
  console.log(`Map: ${match.result.matchInfo?.mapVariant?.publicName}`);
  console.log(`Players: ${match.result.players?.length}`);
}
```

### Get Player CSR

```typescript
const csr = await client.skill.getPlaylistCsr(
  'playlist-guid',
  ['2533274855333605']
);

if (isSuccess(csr)) {
  const playerCsr = csr.result.value?.[0];
  console.log(`CSR: ${playerCsr?.csr?.value} (${playerCsr?.csr?.tier})`);
}
```

### Get Player Inventory

```typescript
const inventory = await client.economy.getInventoryItems('2533274855333605');

if (isSuccess(inventory)) {
  console.log(`Items owned: ${inventory.result.items?.length}`);
}
```

### Search UGC Maps

```typescript
import { AssetKind } from '@dendev/grunt';

const maps = await client.ugcDiscovery.search({
  assetKinds: [AssetKind.Map],
  term: 'blood gulch',
  count: 10,
});

if (isSuccess(maps)) {
  for (const map of maps.result.results ?? []) {
    console.log(`${map.publicName} by ${map.admin}`);
  }
}
```

### Get News Articles (No Auth Required)

```typescript
import { WaypointClient, isSuccess } from '@dendev/grunt';

const client = new WaypointClient(); // No auth needed

const articles = await client.content.getArticles(1, 10);

if (isSuccess(articles)) {
  for (const article of articles.result.articles ?? []) {
    console.log(article.title);
  }
}
```

## Result Handling

All API methods return `HaloApiResult<T>` which contains:
- `result`: The response data (or `null` on failure)
- `response`: Raw response info (status code, headers, etc.)

Use the helper functions to check results:

```typescript
import {
  isSuccess,      // 2xx status with data
  isNotModified,  // 304 (cached response valid)
  isClientError,  // 4xx errors
  isServerError,  // 5xx errors
} from '@dendev/grunt';

const result = await client.stats.getMatchStats('match-id');

if (isSuccess(result)) {
  // result.result is guaranteed non-null here
  console.log(result.result.matchId);
} else if (isClientError(result)) {
  console.error(`Client error: ${result.response.code}`);
} else if (isServerError(result)) {
  console.error(`Server error: ${result.response.code}`);
}
```

## Configuration Options

### HaloInfiniteClient Options

```typescript
const client = new HaloInfiniteClient({
  // Required
  spartanToken: 'your-spartan-token',

  // Optional
  xuid: '2533274855333605',        // Your Xbox User ID
  clearanceToken: 'flight-id',     // For flighted/preview content
  includeRawResponses: true,       // Include full request/response in results
  userAgent: 'MyApp/1.0',          // Custom User-Agent header
  cacheTtlMs: 3600000,             // Cache TTL (default: 60 minutes)
  maxRetries: 3,                   // Retry attempts (default: 3)
});
```

## Building from Source

### Prerequisites

- Node.js 18.0.0 or higher
- npm

### Install Dependencies

```bash
npm install
```

### Build

```bash
npm run build
```

This creates the `dist/` folder with:
- `index.js` - CommonJS build
- `index.mjs` - ES Module build
- `index.d.ts` - TypeScript declarations

### Development

```bash
# Watch mode (rebuild on changes)
npm run dev

# Type check without emitting
npm run typecheck

# Run tests
npm run test
```

## Features

- **Type-safe**: Full TypeScript support with comprehensive type definitions
- **Caching**: Built-in ETag-based caching with configurable TTL
- **Retry Logic**: Automatic retry with exponential backoff for transient failures
- **Lazy Loading**: Modules are initialized on first access to minimize memory usage
- **Minimal Dependencies**: Only one runtime dependency (`lru-cache`)
- **Universal**: Works in Node.js and modern browsers (uses native `fetch`)

## API Reference

For detailed API documentation, refer to the TypeScript type definitions included with the package, or explore the source code in the `src/` directory.

The API mirrors the [.NET Grunt library](../dotnet/) structure, so its documentation can also serve as a reference.

## Disclaimer

This is an unofficial library and is not affiliated with Microsoft, 343 Industries, or Xbox Game Studios. Use at your own risk. The Halo Infinite API is not officially documented and may change without notice.

## License

MIT License - see [LICENSE](LICENSE) for details.

## Credits

- Original Grunt project by [Den Delimarsky](https://den.dev)
- TypeScript implementation maintains API compatibility with the .NET version
