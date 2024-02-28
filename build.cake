#addin nuget:?package=Newtonsoft.Json&version=13.0.3

using System.Net.Http;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

var IOSVERSION = Argument("iosversion", "13.3.1");
var IOS_SERVICERELEASE_VERSION = "0"; // This is combined with the IOSVERSION variable for the NuGet Package version

var target = Argument ("target", "Default");
var NUGET_API_KEY = EnvironmentVariable("NUGET_API_KEY");

Task ("DownloadDeps")
	.Description ("Downloads frameworks")
	.Does (async () => {
		Information ("Downloading all the dependencies please wait...");

		if (!IsRunningOnUnix ()) {
			Error ("*** This can only be run in macOS. ***");
			return;
		}

		var iosUrl = $"https://customers.pspdfkit.com/pspdfkit-ios/{IOSVERSION}.podspec.json";
		var instantUrl = $"https://customers.pspdfkit.com/instant/{IOSVERSION}.podspec.json";

		var iosDlUrl = await ResolveDownloadUrl (iosUrl);
		var instantDlUrl = await ResolveDownloadUrl (instantUrl);

		CreateDirectory("./cache");
		DownloadFile (iosDlUrl, "./cache/ios.zip");
		DownloadFile (instantDlUrl, "./cache/instant.zip");

		UnzipFile ("./cache/ios.zip", "./cache/ios");
		UnzipFile ("./cache/instant.zip", "./cache/ios");

		ProcessXCFramework ("./cache/ios/PSPDFKit.xcframework");
		ProcessXCFramework ("./cache/ios/PSPDFKitUI.xcframework");
		ProcessXCFramework ("./cache/ios/Instant.xcframework");

		CopyDir ("./cache/ios/PSPDFKit.xcframework", "./PSPDFKit.dotnet.iOS.Model/PSPDFKit.xcframework");
		CopyDir ("./cache/ios/PSPDFKitUI.xcframework", "./PSPDFKit.dotnet.iOS.UI/PSPDFKitUI.xcframework");
		CopyDir ("./cache/ios/Instant.xcframework", "./PSPDFKit.dotnet.iOS.Instant/Instant.xcframework");
	}
);

Task ("Build")
	.Description ("Builds PSPDFKit.dotnet.*.*.dll', expects each of the xcframeworks to be available inside each of the './PSPDFKit.dotnet.iOS.*/' Directories\n")
	.Does (() => {
		var iOSFullVersion = IOS_SERVICERELEASE_VERSION == "0" ? IOSVERSION : $"{IOSVERSION}.{IOS_SERVICERELEASE_VERSION}";

		(string p, string xc) [] frameworks = {
			("PSPDFKit.dotnet.iOS.Model", "PSPDFKit.xcframework"),
			("PSPDFKit.dotnet.iOS.UI", "PSPDFKitUI.xcframework"),
			("PSPDFKit.dotnet.iOS.Instant", "Instant.xcframework"),
		};

		foreach (var f in frameworks) {
			if (!DirectoryExists ($"{f.p}/{f.xc}/"))
				throw new Exception ($"Unable to locate '{f.xc}' inside './{f.p}' Directory");
		}

		var msBuildSettings = new DotNetMSBuildSettings ()
			.WithProperty ("AssemblyVersion", iOSFullVersion);

		var dotNetBuildSettings = new DotNetBuildSettings { 
			Configuration = "Release",
//			Verbosity = DotNetVerbosity.Diagnostic,
			MSBuildSettings = msBuildSettings
		};
		DotNetBuild ("./PSPDFKit.dotnet.sln", dotNetBuildSettings);
	});

Task ("Default")
	.Description ("Builds all PSPDFKit dlls.\n")
	.IsDependentOn ("Clean")
	.IsDependentOn ("DownloadDeps")
	.IsDependentOn ("Build")
	.Does (() => {
		Information (@"DONE! You will find the PSPDFKit.*.dll's inside the 'bin\Release' directory of each project folder.");
	}
);

Task ("NuGet")
	.IsDependentOn ("Default")
	.Does (() =>
{
	if(!DirectoryExists ("./nuget/pkgs/"))
		CreateDirectory ("./nuget/pkgs");

	var commit = GetGitShortCommit ();
	Console.WriteLine ($"Commit: {commit}");

	XmlPoke ("Directory.Build.props", "/Project/PropertyGroup/PSVersion", $"{IOSVERSION}.{IOS_SERVICERELEASE_VERSION}+sha.{commit}");

	var dotNetPackSettings = new DotNetPackSettings {
		Configuration = "Release",
		NoRestore = true,
		NoBuild = true,
		OutputDirectory = "./nuget/pkgs",
		//Verbosity = DotNeVerbosity.Diagnostic,
	};

	DotNetPack($"./PSPDFKit.dotnet.sln", dotNetPackSettings);
});

Task ("NuGet-Push")
	.Does (() =>
{	
	var iOSFullVersion = IOSVERSION;

 	if (IOS_SERVICERELEASE_VERSION != "0") {
 		iOSFullVersion = $"{IOSVERSION}.{IOS_SERVICERELEASE_VERSION}";
 	}
	
	// Get the path to the packages
	var nugetPkgs = new [] {
		$"./nuget/pkgs/PSPDFKit.dotnet.iOS.Model.{iOSFullVersion}.nupkg",
		$"./nuget/pkgs/PSPDFKit.dotnet.iOS.UI.{iOSFullVersion}.nupkg",
		$"./nuget/pkgs/PSPDFKit.dotnet.iOS.Instant.{iOSFullVersion}.nupkg",
		$"./nuget/pkgs/PSPDFKit.dotnet.MacCatalyst.Model.{iOSFullVersion}.nupkg",
		$"./nuget/pkgs/PSPDFKit.dotnet.MacCatalyst.UI.{iOSFullVersion}.nupkg",
		$"./nuget/pkgs/PSPDFKit.dotnet.MacCatalyst.Instant.{iOSFullVersion}.nupkg",
	};

	foreach (var pkg in nugetPkgs) {
		Console.WriteLine ($"Pushing: {pkg}.");
		NuGetPush (pkg, new NuGetPushSettings {
			Source = "https://api.nuget.org/v3/index.json",
			ApiKey = NUGET_API_KEY
		});
	}
});

Task ("Clean")
	.Description ("Cleans the build.\n")
	.Does (() => {
		var nukeFiles = new [] {
			"./PSPDFKit.dotnet.iOS.Model.dll",
			"./PSPDFKit.dotnet.iOS.UI.dll",
			"./PSPDFKit.dotnet.iOS.Instant.dll",
			"./PSPDFKit.dotnet.MacCatalyst.Model.dll",
			"./PSPDFKit.dotnet.MacCatalyst.UI.dll",
			"./PSPDFKit.dotnet.MacCatalyst.Instant.dll",
		};

		foreach (var file in nukeFiles) {
			Console.WriteLine (file);
			if (FileExists ($"{file}"))
				Nuke ($"{file}");
		}

		var projdirs = new [] {
			"./PSPDFKit.dotnet.iOS.Model",
			"./PSPDFKit.dotnet.iOS.UI",
			"./PSPDFKit.dotnet.iOS.Instant",
			"./PSPDFKit.dotnet.MacCatalyst.Model",
			"./PSPDFKit.dotnet.MacCatalyst.UI",
			"./PSPDFKit.dotnet.MacCatalyst.Instant",
			"./Samples/DotNetiOSSample",
			"./Samples/DotNetMacCatalystSample",
		};

		foreach (var proj in projdirs) {
			Console.WriteLine (proj);
			if (DirectoryExists ($"{proj}/bin/"))
				Nuke ($"{proj}/bin");

			if (DirectoryExists ($"{proj}/obj/"))
				Nuke ($"{proj}/obj");
		}

		var nukedirs = new [] {
			"./tools",
			"./packages",
			"./nuget/pkgs",
			"./cache/ios",
			"./cache",
			"./PSPDFKit.dotnet.iOS.Model/PSPDFKit.xcframework",
			"./PSPDFKit.dotnet.iOS.UI/PSPDFKitUI.xcframework",
			"./PSPDFKit.dotnet.iOS.Instant/Instant.xcframework",
		};

		foreach (var dir in nukedirs) {
			Console.WriteLine (dir);
			if (DirectoryExists ($"{dir}/"))
				Nuke ($"{dir}");
		}
	}
);

static HttpClient client = new HttpClient ();
static async Task<string> ResolveDownloadUrl (string url)
{
	var json = await client.GetStringAsync (url);
	var jobj = JObject.Parse (json);
	return (string) jobj["source"]["http"];
}

void LipoCreate (FilePath binaryPath, params FilePath [] thinBinaries)
{
	var args = string.Join (" ", thinBinaries.Select (i => $"\"{i}\""));
	StartProcess ("lipo", new ProcessSettings {
		Arguments = $"-create -output \"{binaryPath}\" {args}"
	});
}

void CopyDir (FilePath origin, FilePath destination)
{
	StartProcess ("cp", new ProcessSettings {
		Arguments = $"-RP \"{origin}\" \"{destination}\""
	});
}

void UnzipFile (FilePath zipFile, DirectoryPath destination)
{
	StartProcess ("unzip", new ProcessSettings {
		Arguments = $"\"{zipFile}\" -d \"{destination}\""
	});
}

void Nuke (string path)
{
	StartProcess ("rm", new ProcessSettings {
		Arguments = $"-rf \"{path}\""
	});
}

string GetGitShortCommit ()
{
	StartProcess ("git", new ProcessSettings {
		Arguments = "rev-parse --short HEAD",
		RedirectStandardOutput = true
	}, out var lines);
	return lines.FirstOrDefault ();
}

void ProcessXCFramework (string path)
{
	Nuke ($"{path}/xros-arm64");
	Nuke ($"{path}/xros-arm64_x86_64-simulator");
	Nuke ($"{path}/_CodeSignature");

	// Remove the xros from the Info.plist
	var infoPath = $"{path}/Info.plist";
	var doc = XDocument.Load (infoPath);

	var dictEntries = doc.Descendants ("dict").ToList ();
	foreach (var dict in dictEntries) {
		var supportedPlatformElements = dict.Elements ("key")
			.Where (k => k.Value == "SupportedPlatform")
			.Select (k => k.NextNode as XElement);

		foreach (var element in supportedPlatformElements) {
			if (element != null && element.Value == "xros")
				dict.Remove ();
		}
	}

	var settings = new XmlWriterSettings {
		Indent = true,
		IndentChars = "\t",
		NewLineOnAttributes = false,
	};

	using var writer = XmlWriter.Create (infoPath, settings);
	doc.DocumentType.InternalSubset = null;
	doc.Save (writer);
}

RunTarget (target);
