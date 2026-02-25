# Nutrient.NET (iOS)

The Nutrient SDK is a framework that allows you to view, annotate, sign, and fill PDF forms on iOS, Android, Windows, macOS, and Web.

Nutrient Instant adds real-time collaboration features to seamlessly share, edit, and annotate PDF documents.

## Integration

Check your `.csproj` file to determine which integration method to follow:

- **Single platform** (e.g., `<TargetFramework>net10.0-ios</TargetFramework>`) - Follow [.NET for iOS](#net-for-ios)
- **Multiple platforms** (e.g., `<TargetFrameworks>net10.0-android;net10.0-ios</TargetFrameworks>`) - Follow [.NET MAUI (iOS)](#net-maui-ios)

### .NET for iOS

1. **Add NuGet packages** to your [`.csproj`](https://github.com/PSPDFKit/dotnet-pdf-library-for-ios/blob/master/DotNetiOSSample/DotNetiOSSample.csproj) file:

```xml
<ItemGroup>
  <PackageReference Include="Nutrient.dotnet.iOS.Model" Version="$VERSION$" />
  <PackageReference Include="Nutrient.dotnet.iOS.UI" Version="$VERSION$" />
</ItemGroup>
```

2. **Display a PDF** in your [`AppDelegate.cs`](https://github.com/PSPDFKit/dotnet-pdf-library-for-ios/blob/master/DotNetiOSSample/AppDelegate.cs)

### .NET MAUI (iOS)

1. **Add NuGet packages** conditionally for iOS in your [`.csproj`](https://github.com/PSPDFKit/dotnet-pdf-library-for-mobiles/blob/master/combined/dotnet-pdf-library-for-mobiles/dotnet-pdf-library-for-mobiles.csproj) file:

```xml
<Choose>
  <When Condition="'$(TargetFramework)' == 'net10.0-ios'">
    <ItemGroup>
      <PackageReference Include="Nutrient.dotnet.iOS.Model" Version="$VERSION$" />
      <PackageReference Include="Nutrient.dotnet.iOS.UI" Version="$VERSION$" />
    </ItemGroup>
  </When>
</Choose>
```

2. **Create the shared PdfView** in [`Views/PdfView.cs`](https://github.com/PSPDFKit/dotnet-pdf-library-for-mobiles/blob/master/combined/dotnet-pdf-library-for-mobiles/Views/PdfView.cs)

3. **Create the handler interface** in [`Views/IPdfViewHandler.cs`](https://github.com/PSPDFKit/dotnet-pdf-library-for-mobiles/blob/master/combined/dotnet-pdf-library-for-mobiles/Views/IPdfViewHandler.cs)

4. **Create the iOS platform handler** in [`Platforms/iOS/Handlers/PdfViewHandler.cs`](https://github.com/PSPDFKit/dotnet-pdf-library-for-mobiles/blob/master/combined/dotnet-pdf-library-for-mobiles/Platforms/iOS/Handlers/PdfViewHandler.cs)

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