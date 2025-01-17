# PSPDFKit.NET (iOS)

- .NET for iOS, MacCatalyst Bindings for PSPDFKit version 14.4.0

#### PSPDFKit

The [PSPDFKit SDK](https://pspdfkit.com/) is a framework that allows you to view, annotate, sign, and fill PDF forms on iOS, Android, Windows, macOS, and Web.

[PSPDFKit Instant](https://pspdfkit.com/instant) adds real-time collaboration features to seamlessly share, edit, and annotate PDF documents.

#### Related

- PSPDFKit.NET (Android): [PSPDFKit/dotnet-pdf-library-for-android](https://github.com/PSPDFKit/dotnet-pdf-library-for-android)
- PSPDFKit.NET for mobiles (iOS & Android): [PSPDFKit/dotnet-pdf-library-for-mobiles](https://github.com/PSPDFKit/dotnet-pdf-library-for-mobiles)

## Support, Issues and License Questions

PSPDFKit offers support via https://pspdfkit.com/support/request/.

Are you evaluating our SDK? That's great, we're happy to help out!
To make sure this is fast, please use a work email and have someone from your company fill out our sales form: https://pspdfkit.com/sales/

Minimum Requirements
====================

In order to build this binding project you need:

- **Visual Studio for Mac 2022 17.4.3+**
- **.NET for iOS 17.2.8004/8.0.100 or higher +**
- **.NET for MacCatalyst 17.2.8004/8.0.100 or higher +**

Build Instructions
==================

## Step 1 - Get the bindings

1. Clone this repository to your computer.

## Step 2 - Integrating PSPDFKit

### Integrating PSPDFKit via NuGet (Recommended)

1. Right-Click on your project in Visual Studio and select "Manage NuGet Packages..."
2. In the `Browse` section for "nuget.org" search for "PSPDFKit.dotnet":

<img width="70%" src="Images/Add-NuGet-packages.png"/>

3. Select the following two iOS packages: PSPDFKit.dotnet.iOS.Model and PSPDFKit.dotnet.iOS.UI.
4. Tap on "Add Packages" to add the NuGet packages to your project.

Now you are done and can skip to [Using PSPDFKit in your project](https://github.com/PSPDFKit/dotnet-pdf-library-for-ios#step-3---using-pspdfkit-in-your-project)!

### Integrating PSPDFKit by adding the DLLs (Advanced)
#### Downloading required files

To use this C# binding you can only build the binding project on macOS, you will need to obtain the full PSPDFKit xcframework files by doing either `./build.sh` and let the build script download the frameworks and build the bindings or by `./build.sh --target DownloadDeps` which will only download the required frameworks.

### Get your dlls

### Using command line / terminal

We are using [Cake](https://cakebuild.net) as our build system.

1. Run `./build.sh` (macOS) command from the root directory in terminal.
2. All the resulting dlls will be inside the `bin/` folder of each project directory.
3. Go to **Step 3 - Using PSPDFKit in your project**.

### Using Visual Studio for Mac

1. Run `./build.sh --target DownloadDeps` (macOS) command from the root directory in terminal.
2. Open `PSPDFKit.dotnet.sln` located in the root folder.
2. Build the binding projects inside the `iOS` or `Mac` solution folders.
3. Get the dlls from the `bin` folder of each project.
4. Go to **Step 3 - Using PSPDFKit in your project**.

## Step 3 - Using PSPDFKit in your project

If you don't use nuget, add the generated PSPDFKit dlls as a reference to into your own .NET project and add the corresponding using statements depending on the dlls referenced into your project.

```csharp
using PSPDFKit.Model;
using PSPDFKit.UI;
using PSPDFKit.Instant;
```

Also you do need to set your **license key** early on in your `AppDelegate`, before accessing any other PSPDFKit classes. You can get your license key from your [customer portal](https://customers.pspdfkit.com/) if you haven't done so already. Pass `null` to use the trial version.

```csharp
public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
{
	PSPDFKitGlobal.SetLicenseKey (null);
	// ...
}
```

# PSPDFKit Instant

With PSPDFKit Instant, it’s easier than ever to add real-time collaboration features to your PSPDFKit-powered app, allowing your users to seamlessly share, edit, and annotate PDF documents across iOS, Android, and web. With just a few lines of code, PSPDFKit Instant gives your users a massive productivity boost.

For more information about Instant, please have a look at our [website](https://pspdfkit.com/instant/).

# Examples

You can find two basic examples inside the Samples folder, porting the catalog is still in progress.

## How to Run the Example Projects

1. Do `./build.sh --target DownloadDeps` from inside the root folder.
2. Open the `Samples/PSPDFKit.dotnet.Samples.sln` solution in Visual Studio.
3. Select the example project and device you want to run it on (alternatively you can also right-click on the project and select "Build `Project Name`").

<img width="60%" src="Images/Project-setup.png"/>

4. Tap on the triangle on the left to run the project.

## Generating a Stack Trace

If you experience a crash on your end it's very valuable for us to have as much information as possible to provide you with the best support experience.
Such valuable information includes a stack trace of the crash. Here's a quick step-by-step guide, showing how to generate a stack trace in Visual Studio:

#### Device (Recommended)

1. In the Terminal app enter the following command: `touch ~/.mtouch-launch-with-lldb`. This will essentially let you use lldb to debug your application when it launches.
2. Launch your app in debug mode in Visual Studio for Mac.
3. Open the Application Output window in Visual Studio. It will ask you to execute another command in the Terminal.
4. Once lldb is set up in the terminal window, you can simply use it like you would in Xcode.
5. To get a stack trace you need to type `bt all`.

If you want to remove lldb from your debug setup again you can simply run `rm ~/.mtouch-launch-with-lldb` in your Terminal.


#### Simulator

1. Launch your app in debug mode in Visual Studio for Mac.
2. Open your Activity Monitor app.
3. In the Activity Monitor app search for your app name in the search bar. To give an example, if I want to attach to our PSPDFCatalog example app I need to search for "PSPDFCatalog".
4. Double-Click on the process to view the information window.
5. Now you need to identify the PID (Process ID) of your process. The PID is the number included in the round brackets in the window title, e.g. if the title says "PSPDFCatalog (73389)", then 73389 is your PID. Here's an example of how that looks:

<img width="50%" src="Images/pid.png"/>

6. Now you need to open your Terminal app and enter the following command to attach lldb: `lldb -p YOUR-PID aux`. For the example case above this is what the full command would look like: `lldb -p 73389 aux`.
7. Once lldb is set up in the terminal window, you can simply use it like you would in Xcode.
8. To get a stack trace you need to type `bt all`.

# Contributing

Please ensure [you signed our CLA](https://pspdfkit.com/guides/web/current/miscellaneous/contributing/) so we can accept your contributions.
