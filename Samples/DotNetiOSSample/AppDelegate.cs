using PSPDFKit.Model;
using PSPDFKit.UI;

namespace DotNetiOSSample;

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate {
	public override UIWindow? Window {
		get;
		set;
	}

	public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
	{
		// create a new window instance based on the screen size
		Window = new UIWindow (UIScreen.MainScreen.Bounds);

		// Update to use your document name.
		var document = new PSPDFDocument (NSUrl.FromFilename ("Pdf/PSPDFKit QuickStart Guide.pdf"));

		// The configuration object is optional and allows additional customization.
		var configuration = PSPDFConfiguration.FromConfigurationBuilder ((builder) => {
			builder.PageMode = PSPDFPageMode.Single;
			builder.PageTransition = PSPDFPageTransition.ScrollContinuous;
			builder.ScrollDirection = PSPDFScrollDirection.Vertical;
		});
		var pdfViewController = new PSPDFViewController (document, configuration);

		// Present the PDF view controller within a `UINavigationController` to show built-in toolbar buttons.
		var navController = new UINavigationController (pdfViewController);
		Window.RootViewController = navController;

		// make the window visible
		Window.MakeKeyAndVisible ();

		return true;
	}
}

