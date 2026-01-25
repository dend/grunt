# CLAUDE.md

This file provides guidance for Claude Code when working on this project.

## Project Overview

Den.Dev.Grunt is a C# wrapper library for the Halo Infinite API. It provides strongly-typed access to various Halo Infinite services including stats, economy, game CMS, UGC, and more.

## Adding New API Endpoints

When implementing a new API endpoint, the following files must be created or modified:

### 1. Response Model

Create a model class in `Den.Dev.Grunt/Models/HaloInfinite/`:

```csharp
// <copyright file="ModelName.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Description of what this model represents.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ModelName
    {
        /// <summary>
        /// Gets or sets the property description.
        /// </summary>
        public int PropertyName { get; set; }
    }
}
```

### 2. Module Method

Add the method to the appropriate module in `Den.Dev.Grunt/Core/Modules/`:

```csharp
/// <summary>
/// Description of what this method does.
/// </summary>
/// <include file='../../APIDocsExamples/HaloInfinite/EndpointName.xml' path='example'/>
/// <param name="player">The player identifier in the format "xuid(PLAYER_XUID_HERE)".</param>
/// <returns>Description of the return value.</returns>
public async Task<HaloApiResultContainer<ModelName, RawResponseContainer>> MethodName(string player)
{
    return await this.GetAsync<ModelName>($"/hi/endpoint/path");
}
```

### 3. Response Type Mapping

Add the endpoint-to-model mapping in `Den.Dev.Grunt.Librarian/response-types.json`:

```json
"Module_MethodName": "ModelName",
```

Keep entries alphabetically sorted within their module group.

### 4. XML Example File

Create an example response file in `Den.Dev.Grunt/APIDocsExamples/HaloInfinite/`:

Filename format: `Module_MethodName.xml`

```xml
<example>
	Here is an example response from the API:
	<code>
    {
        "PropertyName": 0
    }
	</code>
</example>
```

### 5. Reference the XML Example

Add the `<include>` tag in the method's XML documentation (see step 2). The path is relative from the module file location:

```csharp
/// <include file='../../APIDocsExamples/HaloInfinite/Module_MethodName.xml' path='example'/>
```

## Gap Analysis

The Librarian tool can analyze API coverage:

```bash
cd Den.Dev.Grunt.Librarian
dotnet run -- --analyze-gaps -r response-types.json -m ../Den.Dev.Grunt/Core/Modules
```

This shows:
- Endpoints missing response type mappings
- Endpoints not yet implemented in modules

## Build Verification

After adding a new endpoint, verify the build:

```bash
cd Den.Dev.Grunt
dotnet build
```

Ensure there are 0 warnings and 0 errors.
