PSPDFKit .NET Bindings for iOS

The PSPDFKit SDK is the leading framework for displaying, annotating and editing PDFs on iOS, macOS, Android, Windows, Electron and the Web.

PSPDFKit Instant adds real-time collaboration features to seamlessly share, edit, and annotate PDF documents.


## Additional Required Setup (Please Read!)

You can try PSPDFKit in a few simple steps and get the library up and running in your app with little to no effort.

1. Get the license key from your [customer portal](https://customers.pspdfkit.com/customers/sign_in) or use 'null' for the evaluation version.

2. Set your license key

public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
{
	PSPDFKitGlobal.SetLicenseKey (null, null);
	// ...
}

3. Now you can start using the SDK.


## Examples

You can find several example projects in https://github.com/PSPDFKit/Xamarin-iOS, including a catalog, Xamarin.Forms, and macOS.

Visit https://pspdfkit.com/guides/ios/current/other-languages/xamarin/ for more information.
