using System;
using System.Runtime.InteropServices;
using CoreGraphics;
using Foundation;
using ObjCRuntime;

namespace PSPDFKit.Model {
	[StructLayout (LayoutKind.Sequential)]
	public struct PSPDFDrawingPoint {

		public CGPoint Location;
		public nfloat Intensity;

		public PSPDFDrawingPoint (CGPoint location, nfloat intensity)
		{
			Location = location;
			Intensity = intensity;
		}

		public static PSPDFDrawingPoint Zero {
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFDrawingPointZero");
				var zero = (PSPDFDrawingPoint) Marshal.PtrToStructure (ptr, typeof (PSPDFDrawingPoint));

				return zero;
			}
		}

		public static PSPDFDrawingPoint Null {
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFDrawingPointNull");
				var ret = (PSPDFDrawingPoint) Marshal.PtrToStructure (ptr, typeof (PSPDFDrawingPoint));

				return ret;
			}
		}

		[DllImport (PSPDFKitLibraryPath.LibraryPath, EntryPoint = "PSPDFDrawingPointIsFinite")]
		[return: MarshalAs (UnmanagedType.I1)]
		static extern bool _IsFinite (PSPDFDrawingPoint point);

		public static bool IsFinite (PSPDFDrawingPoint point) => _IsFinite (point);

		[DllImport (PSPDFKitLibraryPath.LibraryPath, EntryPoint = "PSPDFDrawingPointIsEqualToPoint")]
		[return: MarshalAs (UnmanagedType.I1)]
		static extern bool _IsEqualTo (PSPDFDrawingPoint point, PSPDFDrawingPoint otherPoint);

		public bool IsEqualTo (PSPDFDrawingPoint otherPoint)
		{
			return _IsEqualTo (this, otherPoint);
		}

		[DllImport (PSPDFKitLibraryPath.LibraryPath, EntryPoint = "PSPDFDrawingPointToString")]
		static extern IntPtr _PointToString (PSPDFDrawingPoint point);

		public override string ToString ()
		{
			return (string) Runtime.GetNSObject<NSString> (_PointToString (this));
		}

		[DllImport (PSPDFKitLibraryPath.LibraryPath, EntryPoint = "PSPDFDrawingPointFromString")]
		private static extern PSPDFDrawingPoint _FromString (IntPtr str);

		public static PSPDFDrawingPoint FromString (string str)
		{
			return _FromString (str == null ? IntPtr.Zero : new NSString (str).Handle);
		}
	}
}
