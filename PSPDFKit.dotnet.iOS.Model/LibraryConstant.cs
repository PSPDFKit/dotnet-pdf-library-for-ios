using System;
using System.Runtime.InteropServices;
using ObjCRuntime;

namespace PSPDFKit.Model {
	public partial class PSPDFKitLibraryPath {
#if __IOS__
		internal const string LibraryPath = "__Internal";
#elif __MAC__
		public const string LibraryPath = "__Internal";
#endif
	}
}
