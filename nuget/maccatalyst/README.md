# Nutrient.NET (Mac Catalyst)

The Nutrient SDK is a framework that allows you to view, annotate, sign, and fill PDF forms on iOS, Android, Windows, macOS, and Web.

Nutrient Instant adds real-time collaboration features to seamlessly share, edit, and annotate PDF documents.

## Integration

Check your `.csproj` file to determine which integration method to follow:

- **Single platform** (e.g., `<TargetFramework>net10.0-maccatalyst</TargetFramework>`) - Follow [.NET for Mac Catalyst](#net-for-mac-catalyst)
- **Multiple platforms** (e.g., `<TargetFrameworks>net10.0-android;net10.0-maccatalyst</TargetFrameworks>`) - Follow [.NET MAUI (Mac Catalyst)](#net-maui-mac-catalyst)

### .NET for Mac Catalyst

1. **Add NuGet packages** to your [`.csproj`](https://github.com/PSPDFKit/dotnet-pdf-library-for-ios/blob/master/DotNetMacCatalystSample/DotNetMacCatalystSample.csproj) file:

```xml
<ItemGroup>
  <PackageReference Include="Nutrient.dotnet.MacCatalyst.Model" Version="$VERSION$" />
  <PackageReference Include="Nutrient.dotnet.MacCatalyst.UI" Version="$VERSION$" />
</ItemGroup>
```

2. **Display a PDF** in your [`AppDelegate.cs`](https://github.com/PSPDFKit/dotnet-pdf-library-for-ios/blob/master/DotNetMacCatalystSample/AppDelegate.cs)

### .NET MAUI (Mac Catalyst)

1. **Add NuGet packages** conditionally for Mac Catalyst in your [`.csproj`](https://github.com/PSPDFKit/dotnet-pdf-library-for-mobiles/blob/master/combined/dotnet-pdf-library-for-mobiles/dotnet-pdf-library-for-mobiles.csproj) file:

```xml
<Choose>
  <When Condition="'$(TargetFramework)' == 'net10.0-maccatalyst'">
    <ItemGroup>
      <PackageReference Include="Nutrient.dotnet.MacCatalyst.Model" Version="$VERSION$" />
      <PackageReference Include="Nutrient.dotnet.MacCatalyst.UI" Version="$VERSION$" />
    </ItemGroup>
  </When>
</Choose>
```

2. **Create the shared PdfView** in [`Views/PdfView.cs`](https://github.com/PSPDFKit/dotnet-pdf-library-for-mobiles/blob/master/combined/dotnet-pdf-library-for-mobiles/Views/PdfView.cs)

3. **Create the handler interface** in [`Views/IPdfViewHandler.cs`](https://github.com/PSPDFKit/dotnet-pdf-library-for-mobiles/blob/master/combined/dotnet-pdf-library-for-mobiles/Views/IPdfViewHandler.cs)

4. **Create the Mac Catalyst platform handler** in [`Platforms/MacCatalyst/Handlers/PdfViewHandler.cs`](https://github.com/PSPDFKit/dotnet-pdf-library-for-mobiles/blob/master/combined/dotnet-pdf-library-for-mobiles/Platforms/MacCatalyst/Handlers/PdfViewHandler.cs)

5. **Register the handler** in [`MauiProgram.cs`](https://github.com/PSPDFKit/dotnet-pdf-library-for-mobiles/blob/master/combined/dotnet-pdf-library-for-mobiles/MauiProgram.cs)

6. **Use PdfView in a XAML page** - see [`Examples/Playground.xaml`](https://github.com/PSPDFKit/dotnet-pdf-library-for-mobiles/blob/master/combined/dotnet-pdf-library-for-mobiles/Examples/Playground.xaml) and [`Examples/Playground.xaml.cs`](https://github.com/PSPDFKit/dotnet-pdf-library-for-mobiles/blob/master/combined/dotnet-pdf-library-for-mobiles/Examples/Playground.xaml.cs)

## Support

Nutrient offers support via https://support.nutrient.io/hc/en-us/requests/new.

Are you evaluating our SDK? That's great, we're happy to help out! To make sure this is fast, please use a work email and have someone from your company fill out our sales form: https://www.nutrient.io/contact-sales?=sdk

Visit https://www.nutrient.io/guides/ios/dotnet/ for more information on how to setup and use the SDK.

## Examples

Examples are available at
- https://github.com/PSPDFKit/dotnet-pdf-library-for-ios
- https://github.com/PSPDFKit/dotnet-pdf-library-for-mobiles
