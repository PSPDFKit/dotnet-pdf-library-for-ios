---
name: setup-sample
description: Help users set up and run the iOS or MacCatalyst sample projects. Use when asked to run the sample, set up the project, or get started with the Nutrient .NET for iOS SDK.
---

# Set Up and Run the iOS / MacCatalyst Sample Projects

Guides the user through setting up and running the `DotNetiOSSample` or `DotNetMacCatalystSample` projects from this repository.

## Important Rules

- **Always check prerequisites first** before attempting to build.
- **Prefer the NuGet approach** unless the user explicitly wants to build from source.
- **Stop on errors** and help the user diagnose before proceeding.
- **macOS is required** for building iOS and MacCatalyst projects.

## Available Samples

| Project | Target | Location |
|---------|--------|----------|
| DotNetiOSSample | iOS (iPhone/iPad/Simulator) | `Samples/DotNetiOSSample/` |
| DotNetMacCatalystSample | Mac Catalyst (macOS) | `Samples/DotNetMacCatalystSample/` |

Both can be opened together via the solution file: `Samples/Nutrient.dotnet.Samples.sln`

## Workflow

### Step 1: Check Prerequisites

Verify the development environment is ready:

```bash
# Check .NET SDK is installed
dotnet --version

# Check iOS workload is installed
dotnet workload list | grep ios

# Check MacCatalyst workload is installed (for Mac Catalyst sample)
dotnet workload list | grep maccatalyst

# Check Xcode is installed and selected
xcode-select -p
xcrun --show-sdk-version
```

**Required:**
- macOS
- .NET SDK 9.0+
- .NET for iOS workload 17.2.8004+ (for iOS sample)
- .NET for MacCatalyst workload 17.2.8004+ (for MacCatalyst sample)
- Xcode with matching SDK

If workloads are missing:
```bash
dotnet workload install ios
dotnet workload install maccatalyst
```

### Step 2: Choose Integration Path

Ask the user which approach they prefer:

**Option A: NuGet Package (Recommended)** - Easiest setup, uses pre-built packages from nuget.org.

**Option B: Build from Source (Advanced)** - Downloads xcframeworks and builds binding projects locally.

Also ask which sample they want to run: **iOS** or **MacCatalyst**.

### Step 3A: NuGet Approach

1. Read the current version from `VERSION` file.

2. Modify the sample project to use NuGet instead of ProjectReference.

   **For iOS sample** (`Samples/DotNetiOSSample/DotNetiOSSample.csproj`), replace:
   ```xml
   <ProjectReference Include="..\..\Nutrient.dotnet.iOS.Model\Nutrient.dotnet.iOS.Model.csproj" />
   <ProjectReference Include="..\..\Nutrient.dotnet.iOS.UI\Nutrient.dotnet.iOS.UI.csproj" />
   ```
   with:
   ```xml
   <PackageReference Include="Nutrient.dotnet.iOS.Model" Version="VERSION_FROM_FILE" />
   <PackageReference Include="Nutrient.dotnet.iOS.UI" Version="VERSION_FROM_FILE" />
   ```

   **For MacCatalyst sample** (`Samples/DotNetMacCatalystSample/DotNetMacCatalystSample.csproj`), replace the `ProjectReference` entries similarly with:
   ```xml
   <PackageReference Include="Nutrient.dotnet.MacCatalyst.Model" Version="VERSION_FROM_FILE" />
   <PackageReference Include="Nutrient.dotnet.MacCatalyst.UI" Version="VERSION_FROM_FILE" />
   ```

3. Restore and build:
   ```bash
   cd Samples/DotNetiOSSample    # or DotNetMacCatalystSample
   dotnet restore
   dotnet build
   ```

4. To run on the iOS Simulator:
   ```bash
   cd Samples/DotNetiOSSample
   dotnet build -t:Run
   ```

   To run the MacCatalyst sample:
   ```bash
   cd Samples/DotNetMacCatalystSample
   dotnet build -t:Run
   ```

5. **Remind the user** to revert the `.csproj` changes before committing:
   ```bash
   git checkout -- Samples/DotNetiOSSample/DotNetiOSSample.csproj
   git checkout -- Samples/DotNetMacCatalystSample/DotNetMacCatalystSample.csproj
   ```

### Step 3B: Build from Source Approach

1. Read the current version from `VERSION` file.

2. Download the xcframeworks from the repository root:
   ```bash
   ./build.sh --nutrient-version=VERSION_FROM_FILE --target DownloadDeps
   ```
   This downloads and extracts PSPDFKit.xcframework, PSPDFKitUI.xcframework, and Instant.xcframework.

3. Build all binding projects:
   ```bash
   ./build.sh --nutrient-version=VERSION_FROM_FILE
   ```
   Allow up to 10 minutes for the full build.

4. Build and run the sample (it already uses `<ProjectReference>`):

   **iOS sample:**
   ```bash
   cd Samples/DotNetiOSSample
   dotnet build -t:Run
   ```

   **MacCatalyst sample:**
   ```bash
   cd Samples/DotNetMacCatalystSample
   dotnet build -t:Run
   ```

   Alternatively, open `Samples/Nutrient.dotnet.Samples.sln` in Visual Studio or Rider and run from there.

### Step 4: Configure License Key

The sample does not set a license key by default, which means it runs in trial mode. If the user has a license key:

1. Open the `AppDelegate.cs` for the chosen sample.
2. Add the license key setup in `FinishedLaunching`, before any other Nutrient usage:
   ```csharp
   PSPDFKitGlobal.SetLicenseKey("YOUR_LICENSE_KEY");
   ```

License keys are available from https://my.nutrient.io/.

### Step 5: Verify the App Runs

**iOS Sample:**
- The app should launch and display the bundled "PSPDFKit QuickStart Guide.pdf".
- It shows a PDF viewer with navigation controls, annotations toolbar, and page thumbnails.

**MacCatalyst Sample:**
- The app should launch as a native macOS window displaying the same PDF.

If the build succeeds but the app fails to launch, clean and rebuild:
```bash
cd Samples/DotNetiOSSample  # or DotNetMacCatalystSample
rm -rf bin obj
dotnet build
```

## Troubleshooting

### Build fails with missing workload
```bash
dotnet workload install ios
dotnet workload install maccatalyst
```

### "Could not resolve reference" errors
The xcframeworks may not be downloaded. Run from the repository root:
```bash
./build.sh --nutrient-version=VERSION_FROM_FILE --target DownloadDeps
```

### MacCatalyst build fails but iOS succeeds
Verify the xcframework contains Mac Catalyst slices:
```bash
ls Nutrient.dotnet.iOS.Model/PSPDFKit.xcframework/ | grep maccatalyst
```

### Simulator not found
List available simulators:
```bash
xcrun simctl list devices available | grep iPhone
```

## Key Files

| File | Purpose |
|------|---------|
| `VERSION` | Current Nutrient iOS SDK version |
| `Samples/Nutrient.dotnet.Samples.sln` | Solution with all sample projects |
| `Samples/DotNetiOSSample/DotNetiOSSample.csproj` | iOS sample project file |
| `Samples/DotNetiOSSample/AppDelegate.cs` | iOS app delegate with license key setup |
| `Samples/DotNetMacCatalystSample/DotNetMacCatalystSample.csproj` | MacCatalyst sample project file |
| `Samples/Pdf/PSPDFKit QuickStart Guide.pdf` | Bundled demo PDF document |
| `README.md` | Full integration documentation |
