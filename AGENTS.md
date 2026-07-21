# Nutrient .NET for iOS

Agent instructions for the `dotnet-pdf-library-for-ios` repository. This is an Objective-C-to-C# binding library for the Nutrient iOS PDF SDK.

## Architecture Overview

This project produces **6 NuGet packages** (3 iOS + 3 MacCatalyst) that wrap the native Nutrient iOS SDK xcframeworks, enabling .NET developers to use Nutrient PDF functionality from C#.

### How the Binding Works

1. The native Nutrient iOS SDK is distributed as xcframeworks (PSPDFKit, PSPDFKitUI, Instant).
2. The binding projects use .NET for iOS binding generator to produce C# types from Objective-C headers.
3. Binding definitions are hand-maintained in `ApiDefinition.cs`, `Enums.cs`, `Structs.cs`, etc.
4. MacCatalyst projects **share binding files** from iOS projects via MSBuild `<Link>` metadata -- no duplication.
5. The result is 6 DLLs, each packaged as a separate NuGet.

### Package Structure

Each native iOS framework gets a corresponding iOS and MacCatalyst NuGet package. List the `Nutrient.dotnet.iOS.*` and `Nutrient.dotnet.MacCatalyst.*` directories to discover the current set of packages and what they wrap. Check the `.csproj` files for the dependency chain between packages.

### Directory Structure

```text
ios/
├── build.cake / build.sh / build.ps1      # Cake build script and entry points
├── VERSION                                # Current Nutrient iOS SDK version
├── Directory.Build.props                  # Shared NuGet version (PSVersion)
├── *.sln                                  # Solution files
│
├── Nutrient.dotnet.iOS.*/                 # iOS binding projects (one per framework)
│   ├── ApiDefinition.cs                   # ObjC-to-C# binding definitions
│   ├── Enums.cs / Structs.cs             # Type definitions
│   ├── ApiEnhancements.cs                # Manual C# extensions
│   └── *.xcframework/                    # Downloaded native framework
│
├── Nutrient.dotnet.MacCatalyst.*/         # MacCatalyst binding projects (link iOS files)
│
├── Samples/                               # Sample apps (iOS and Mac Catalyst)
├── cache/                                 # Downloaded xcframeworks (gitignored)
├── nuget/                                 # NuGet packaging assets (pkgs/, readmes, icon)
├── *.py                                   # Binding tooling scripts
└── README.md / LICENSE.md
```

When working in this project, list directory contents to discover the current binding projects and their files — the number of projects and specific filenames may change across versions.

### MacCatalyst File Sharing

MacCatalyst projects contain **no binding source files**. They reference iOS project files via MSBuild `<Link>` metadata. Read any MacCatalyst `.csproj` to see the current linking pattern.

This means you only edit binding files in `Nutrient.dotnet.iOS.*` directories -- MacCatalyst picks them up automatically. The binding files use conditional compilation (`#if __IOS__` / `#elif __MAC__`) for platform-specific type differences.

### Build Pipeline

The build uses [Cake](https://cakebuild.net):

```text
DownloadDeps → Build → [NuGet]
```

- **DownloadDeps**: Downloads xcframeworks from my.nutrient.io (check `build.cake` for the current framework list). Extracts to `cache/ios/`, strips unsupported slices, copies to each binding project directory.
- **Build**: Compiles all 6 projects via `dotnet build` in Release mode.
- **NuGet**: Packs all 6 into `.nupkg` files in `nuget/pkgs/`.
- **Clean**: Removes all build artifacts, downloaded frameworks, bin/, obj/.

### Key Build Commands

```bash
# Download frameworks only
./build.sh --nutrient-version=<VERSION> --target DownloadDeps

# Full build (download + compile)
./build.sh --nutrient-version=<VERSION>

# Build NuGet packages
./build.sh --nutrient-version=<VERSION>
dotnet cake --target=NuGet --nutrient-version=<VERSION>

# Clean everything
dotnet cake build.cake --target=Clean --nutrient-version=<VERSION>
```

**macOS is required** for all builds (iOS and Mac Catalyst compilation needs Xcode).

### Binding File Structure

Each binding project follows the standard .NET for iOS binding structure. The key file types are:

- `ApiDefinition.cs` — Interface definitions with `[BaseType]`, `[Export]` attributes mapping ObjC to C#
- `Enums.cs` / `Structs.cs` — Type definitions
- `ApiEnhancements.cs` — Manual C# extensions and convenience methods

List the contents of any binding project directory to discover the full set of current files.

### Binding Configuration

All binding projects share the same MSBuild settings. Read any `.csproj` to see the current binding configuration. XCFrameworks must be present at build time -- they are not embedded in the NuGet.

### Sample Projects

The `Samples/` directory contains sample apps for iOS and Mac Catalyst demonstrating PDF viewing with the binding projects. Read the sample code to understand current usage patterns. Samples use `<ProjectReference>` to the binding projects.

### Version Management

- `VERSION` contains the Nutrient iOS SDK version. May use 4-part versions for service releases.
- `Directory.Build.props` stores `<PSVersion>` shared across all packages.
- Read `VERSION` and `Directory.Build.props` to check the current state. Version must also be updated in `README.md` and the combined project's `.csproj`.

### Tooling

- **Objective Sharpie** (`brew install objectivesharpie`): Auto-generates ObjC binding definitions. Use as a reference -- don't replace files wholesale.
- **`cleanup_instant_bindings.py`**: Filters unwanted system/internal types from Sharpie output for Instant bindings.
- **`verify_bindings.py`**: Detects APIs in the xcframework headers that aren't yet bridged in the binding files.

### Swift API Limitation

The Nutrient iOS SDK exposes some APIs **only in Swift**. These cannot currently be bound to C# -- .NET does not yet support Swift interop. Track progress at [dotnet/runtime#95638](https://github.com/dotnet/runtime/issues/95638). On each update, check whether a solution has become available.
