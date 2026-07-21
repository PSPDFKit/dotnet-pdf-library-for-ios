---
name: api-lookup
description: Look up Nutrient iOS SDK API documentation. Use when the user asks about a Nutrient API, class, method, annotation type, configuration option, or how to use a specific SDK feature.
---

# Nutrient iOS API Lookup

Helps users find and understand Nutrient iOS SDK APIs by consulting the official documentation.

## Workflow

### Step 1: Fetch the API Index

Fetch the llms.txt file to find relevant API entries:

```
WebFetch: https://www.nutrient.io/api/ios/llms.txt
```

Search the response for classes or components matching the user's query. The index will list the available frameworks and their classes — use these to understand the current SDK structure.

### Step 2: Fetch Specific API Documentation

Once you identify the relevant class or protocol from the llms.txt index, follow the links provided there to fetch the full documentation page.

If the llms.txt entry provides a direct URL, use it. Otherwise, construct the URL using the base path and framework/class structure as shown in the index.

### Step 3: Translate to .NET Binding Context

The iOS API docs describe the Swift/Objective-C API. When answering, translate to the .NET binding context:

1. **Discover the binding project structure** by listing the `Nutrient.dotnet.iOS.*` directories to find the current binding projects and their files.
2. **Read the relevant `ApiDefinition.cs`** in the matching binding project to confirm the C# type name, method signatures, and any binding attributes.
3. **Check `Enums.cs` and `Structs.cs`** in the same project for enum and struct definitions.
4. **Check `ApiEnhancements.cs`** for any hand-written convenience methods not present in the native API.
5. Apply standard Objective-C-to-C# binding conventions:
   - Objective-C types typically keep their names in C#.
   - Objective-C selectors become C# methods with named parameters.
   - Objective-C properties map to C# properties.
   - Objective-C protocols become C# interfaces or abstract classes.
   - Objective-C enums become C# enums.

### Step 4: Identify Binding Location

Help the user find where the API is bound by searching the binding projects. Use Grep to locate the class or method across `ApiDefinition.cs` files in the `Nutrient.dotnet.iOS.*` and `Nutrient.dotnet.MacCatalyst.*` directories. MacCatalyst projects share binding files from iOS projects via MSBuild links.

### Step 5: Show Code Examples

When providing code examples, write them in C# using the .NET binding types. Note that:
- iOS and MacCatalyst use the same C# API surface but different NuGet packages.
- Some Swift-only APIs are not available in the C# binding. If an API seems missing, it may be Swift-only — check `verify_bindings.py` output or the native headers.

## Notes

- The binding files use conditional compilation (`#if __IOS__` / `#elif __MAC__`) for platform-specific type differences.
- `ApiEnhancements.cs` in each project contains convenience methods not present in the native API.
- For guides and tutorials (not API reference), use: `https://www.nutrient.io/guides/ios/`
