using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;
using PSPDFKit.Model;
using UIKit;

namespace PSPDFKit.UI {

	public partial class PSPDFPresentationOptions : DictionaryContainer {

		public PSPDFPresentationStyle? Style {
			get => (PSPDFPresentationStyle) (ulong) GetNUIntValue (PSPDFPresentationKeys.StyleKey);
			set => SetNumberValue (PSPDFPresentationKeys.StyleKey, (nuint) (ulong) value);
		}

		public PSPDFPresentationHalfModalStyle? HalfModalStyle {
			get => (PSPDFPresentationHalfModalStyle) (ulong) GetNUIntValue (PSPDFPresentationKeys.HalfModalStyleKey);
			set => SetNumberValue (PSPDFPresentationKeys.HalfModalStyleKey, (nuint) (ulong) value);
		}

		public UIPopoverArrowDirection? PopoverArrowDirections {
			get => (UIPopoverArrowDirection) (ulong) GetNUIntValue (PSPDFPresentationKeys.PopoverArrowDirectionsKey);
			set => SetNumberValue (PSPDFPresentationKeys.PopoverArrowDirectionsKey, (nuint) (ulong) value);
		}
	}

	public partial class PSPDFAnnotationTableViewController {

		public PSPDFAnnotationStringUI [] VisibleAnnotationTypes {
			get => WeakVisibleAnnotationTypes?.ToArray ()?.Select (x => PSPDFAnnotationStringUIExtensions.GetValue (x))?.ToArray ();
			set => WeakVisibleAnnotationTypes = value == null ? null : new NSSet<NSString> (value.Select (x => x.GetConstant ())?.ToArray ());
		}

		public PSPDFAnnotationStringUI [] EditableAnnotationTypes {
			get => WeakEditableAnnotationTypes?.ToArray ()?.Select (x => PSPDFAnnotationStringUIExtensions.GetValue (x))?.ToArray ();
			set => WeakEditableAnnotationTypes = value == null ? null : new NSSet<NSString> (value.Select (x => x.GetConstant ())?.ToArray ());
		}
	}

	public partial class PSPDFAnnotationTableViewController {

		public PSPDFAnnotationStringUI [] EditableAnnotationTypesUI {
			get => WeakEditableAnnotationTypes?.ToArray ()?.Select (x => PSPDFAnnotationStringUIExtensions.GetValue (x))?.ToArray ();
			set => WeakEditableAnnotationTypes = value == null ? null : new NSSet<NSString> (value.Select (x => x.GetConstant ())?.ToArray ());
		}
	}

	public partial class PSPDFAnnotationToolbar {

		public PSPDFAnnotationStringUI [] EditableAnnotationTypes {
			get => WeakEditableAnnotationTypes?.ToArray ()?.Select (x => PSPDFAnnotationStringUIExtensions.GetValue (x))?.ToArray ();
			set => WeakEditableAnnotationTypes = value == null ? null : new NSSet<NSString> (value.Select (x => x.GetConstant ())?.ToArray ());
		}
	}

	public partial class PSPDFConfigurationBuilder {

		public PSPDFAnnotationStringUI [] EditableAnnotationTypes {
			get => WeakEditableAnnotationTypes?.ToArray ()?.Select (x => PSPDFAnnotationStringUIExtensions.GetValue (x))?.ToArray ();
			set => WeakEditableAnnotationTypes = value == null ? null : new NSSet<NSString> (value.Select (x => x.GetConstant ())?.ToArray ());
		}
	}

	public partial class PSPDFConfiguration {

		public PSPDFAnnotationStringUI [] EditableAnnotationTypes {
			get => WeakEditableAnnotationTypes?.ToArray ()?.Select (x => PSPDFAnnotationStringUIExtensions.GetValue (x))?.ToArray ();
		}
	}

	public partial class PSPDFDocumentEditorViewController {

		public Type CellType {
			get => CellClass == null ? null : Class.Lookup (CellClass);
			set => CellClass = value == null ? throw new ArgumentNullException (nameof (value)) : new Class (value);
		}
	}

	public partial class PSPDFDocumentInfoCoordinator {

		public delegate void PSPDFDocumentInfoCoordinatorDidCreateControllerHandler (UIViewController controller, PSPDFDocumentInfoOption option);

		public PSPDFDocumentInfoCoordinatorDidCreateControllerHandler DidCreateControllerHandler {
			get {
				var handler = _DidCreateControllerHandler;
				if (handler == null)
					return null;
				return (controller, option) => handler (controller, option.GetConstant ());
			}
			set {
				if (value == null)
					_DidCreateControllerHandler = null;
				else
					_DidCreateControllerHandler = (controller, option) => value (controller, PSPDFDocumentInfoOptionExtensions.GetValue (option));
			}
		}

		public PSPDFDocumentInfoOption [] AvailableControllerOptions {
			get => _AvailableControllerOptions?.Select (x => PSPDFDocumentInfoOptionExtensions.GetValue (x))?.ToArray ();
			set => _AvailableControllerOptions = value?.Select (x => x.GetConstant ())?.ToArray ();
		}
	}

	public partial class PSPDFFlexibleToolbarContainer {

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFSystemBarForResponder")]
		static extern IntPtr SystemBarForResponder (IntPtr responder);

		[Advice ("'T' must be 'UIToolbar' or 'UINavigationBar' or 'UIView'.")]
		public static T GetSystemBar<T> (UIResponder responder) where T : UIView
		{
			var t = typeof (T);
			if (t == typeof (UIToolbar) || t == typeof (UINavigationBar) || t == typeof (UIView))
				return Runtime.GetNSObject<T> (SystemBarForResponder (responder.Handle));
			throw new InvalidOperationException ("'T' must be 'UIToolbar' or 'UINavigationBar' or 'UIView'.");
		}
	}

	public partial class PSPDFMenuItem {

		public static bool AllowImagesForMenuItems {
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (PSPDFKitGlobal.DlPath, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFAllowImagesForMenuItems");
				return Convert.ToBoolean (Marshal.ReadByte (ptr));
			}
			set {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (PSPDFKitGlobal.DlPath, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFAllowImagesForMenuItems");
				Marshal.WriteByte (ptr, Convert.ToByte (value));
			}
		}
	}

	public partial class PSPDFThumbnailViewController {

		public PSPDFThumbnailViewFilter [] FilterOptions {
			get => WeakFilterOptions?.Select (x => PSPDFThumbnailViewFilterExtensions.GetValue (x))?.ToArray ();
			set => WeakFilterOptions = value?.Select (x => x.GetConstant ())?.ToArray ();
		}

		public PSPDFThumbnailViewFilter ActiveFilter {
			get => PSPDFThumbnailViewFilterExtensions.GetValue (WeakActiveFilter);
			set => WeakActiveFilter = value.GetConstant ();
		}

		public Type CellClassType {
			get => CellClass == null ? null : Class.Lookup (CellClass);
			set => CellClass = value == null ? throw new ArgumentNullException (nameof (value)) : new Class (value);
		}
	}

	public partial class PSPDFDrawView {

		public void ContinueDrawing (PSPDFDrawingPoint [] locations, PSPDFDrawingPoint [] predictedLocationsPoints)
		{
			ContinueDrawing (locations.Select (l => (null as NSValue).FromPSPDFDrawingPoint (l)).ToArray (), predictedLocationsPoints.Select (p => (null as NSValue).FromPSPDFDrawingPoint (p)).ToArray ());
		}

		public void EraseAt (PSPDFDrawingPoint [] locations)
		{
			EraseAt (locations.Select (l => (null as NSValue).FromPSPDFDrawingPoint (l)).ToArray ());
		}
	}

	public partial class PSPDFDocumentSharingConfigurationBuilder {

		public PSPDFActivityType [] ExcludedActivityTypes {
			get => WeakExcludedActivityTypes?.Select (x => PSPDFActivityTypeExtensions.GetValue (x))?.ToArray ();
			set => WeakExcludedActivityTypes = value?.Select (x => x.GetConstant ())?.ToArray ();
		}
	}

	public partial class PSPDFDocumentSharingConfiguration {

		public PSPDFActivityType [] ExcludedActivityTypes {
			get => WeakExcludedActivityTypes?.Select (x => PSPDFActivityTypeExtensions.GetValue (x))?.ToArray ();
		}
	}

	public static class DictionaryContainerHelpers {

		// helper to avoid the (common pattern)
		// 	var p = x == null ? null : x.Dictionary;
		static public NSDictionary GetDictionary (this DictionaryContainer self)
		{
			return self == null ? null : self.Dictionary;
		}
	}

#if NET
public unsafe static partial class NSIndexPath_PSPDFDocumentViewLayout  {

		[DllImport (Constants.ObjectiveCLibrary, EntryPoint="objc_msgSend")]
		public extern static IntPtr IntPtr_objc_msgSend (IntPtr receiever, IntPtr selector);

		[DllImport (Constants.ObjectiveCLibrary, EntryPoint="objc_msgSend")]
		public extern static IntPtr IntPtr_objc_msgSend_IntPtr (IntPtr receiever, IntPtr selector, IntPtr arg1);

		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		static readonly IntPtr class_ptr = Class.GetHandle ("NSIndexPath");

		[Export ("pspdf_indexPathForSpreadAtIndex:")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static NSIndexPath GetPsPdfIndexPathForSpread (this NSIndexPath This, nint spreadIndex)
		{
			return  Runtime.GetNSObject<NSIndexPath> (IntPtr_objc_msgSend_IntPtr (class_ptr, Selector.GetHandle ("pspdf_indexPathForSpreadAtIndex:"), (IntPtr) spreadIndex))!;
		}

		[Export ("pspdf_spreadIndex")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static nint GetPsPdfSpreadIndex (this NSIndexPath This)
		{
			return (nint) IntPtr_objc_msgSend (This.Handle, Selector.GetHandle ("pspdf_spreadIndex"));
		}
	} /* class NSIndexPath_PSPDFDocumentViewLayout */

	public unsafe static partial class PSPDFAnnotationGroupItem_PSPDFPresets  {

		[DllImport (Constants.ObjectiveCLibrary, EntryPoint="objc_msgSend")]
		public extern static IntPtr IntPtr_objc_msgSend (IntPtr receiever, IntPtr selector);

		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		static readonly IntPtr class_ptr = Class.GetHandle ("PSPDFAnnotationGroupItem");

		[return: DelegateProxy (typeof (SDPSPDFAnnotationGroupItemConfigurationHandler))]
		[Export ("freeTextConfigurationBlock")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public unsafe static global::PSPDFKit.Model.PSPDFAnnotationGroupItemConfigurationHandler GetFreeTextConfigurationHandler (this global::PSPDFKit.Model.PSPDFAnnotationGroupItem This)
		{
			IntPtr ret;
			ret = IntPtr_objc_msgSend (class_ptr, Selector.GetHandle ("freeTextConfigurationBlock"));
			return NIDPSPDFAnnotationGroupItemConfigurationHandler.Create (ret)!;
		}
		[return: DelegateProxy (typeof (SDPSPDFAnnotationGroupItemConfigurationHandler))]
		[Export ("inkConfigurationBlock")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public unsafe static global::PSPDFKit.Model.PSPDFAnnotationGroupItemConfigurationHandler GetInkConfigurationHandler (this global::PSPDFKit.Model.PSPDFAnnotationGroupItem This)
		{
			IntPtr ret;
			ret = IntPtr_objc_msgSend (class_ptr, Selector.GetHandle ("inkConfigurationBlock"));
			return NIDPSPDFAnnotationGroupItemConfigurationHandler.Create (ret)!;
		}
		[return: DelegateProxy (typeof (SDPSPDFAnnotationGroupItemConfigurationHandler))]
		[Export ("lineConfigurationBlock")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public unsafe static global::PSPDFKit.Model.PSPDFAnnotationGroupItemConfigurationHandler GetLineConfigurationHandler (this global::PSPDFKit.Model.PSPDFAnnotationGroupItem This)
		{
			IntPtr ret;
			ret = IntPtr_objc_msgSend (class_ptr, Selector.GetHandle ("lineConfigurationBlock"));
			return NIDPSPDFAnnotationGroupItemConfigurationHandler.Create (ret)!;
		}
		[return: DelegateProxy (typeof (SDPSPDFAnnotationGroupItemConfigurationHandler))]
		[Export ("measurementConfigurationBlock")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public unsafe static global::PSPDFKit.Model.PSPDFAnnotationGroupItemConfigurationHandler GetMeasurementConfigurationHandler (this global::PSPDFKit.Model.PSPDFAnnotationGroupItem This)
		{
			IntPtr ret;
			ret = IntPtr_objc_msgSend (class_ptr, Selector.GetHandle ("measurementConfigurationBlock"));
			return NIDPSPDFAnnotationGroupItemConfigurationHandler.Create (ret)!;
		}
		[return: DelegateProxy (typeof (SDPSPDFAnnotationGroupItemConfigurationHandler))]
		[Export ("polygonConfigurationBlock")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public unsafe static global::PSPDFKit.Model.PSPDFAnnotationGroupItemConfigurationHandler GetPolygonConfigurationHandler (this global::PSPDFKit.Model.PSPDFAnnotationGroupItem This)
		{
			IntPtr ret;
			ret = IntPtr_objc_msgSend (class_ptr, Selector.GetHandle ("polygonConfigurationBlock"));
			return NIDPSPDFAnnotationGroupItemConfigurationHandler.Create (ret)!;
		}

		internal sealed class NIDPSPDFAnnotationGroupItemConfigurationHandler : TrampolineBlockBase {
			DPSPDFAnnotationGroupItemConfigurationHandler invoker;
			[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
			public unsafe NIDPSPDFAnnotationGroupItemConfigurationHandler (BlockLiteral *block) : base (block)
			{
				invoker = block->GetDelegateForBlock<DPSPDFAnnotationGroupItemConfigurationHandler> ();
			}

			[Preserve (Conditional=true)]
			[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
			public unsafe static global::PSPDFKit.Model.PSPDFAnnotationGroupItemConfigurationHandler? Create (IntPtr block)
			{
				if (block == IntPtr.Zero)
					return null;
				var del = (global::PSPDFKit.Model.PSPDFAnnotationGroupItemConfigurationHandler) GetExistingManagedDelegate (block);
				return del ?? new NIDPSPDFAnnotationGroupItemConfigurationHandler ((BlockLiteral *) block).Invoke;
			}

			[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
			unsafe global::UIKit.UIImage Invoke (global::PSPDFKit.Model.PSPDFAnnotationGroupItem item, NSObject container, global::UIKit.UIColor tintColor)
			{
				var item__handle__ = item.GetHandle ();
				var container__handle__ = container.GetHandle ();
				var tintColor__handle__ = tintColor.GetHandle ();
				var ret =  Runtime.GetNSObject<global::UIKit.UIImage> (invoker (BlockPointer, item__handle__, container__handle__, tintColor__handle__))!;
				return ret!;
			}
		} /* class NIDPSPDFAnnotationGroupItemConfigurationHandler */

		[UnmanagedFunctionPointerAttribute (CallingConvention.Cdecl)]
		[UserDelegateType (typeof (global::PSPDFKit.Model.PSPDFAnnotationGroupItemConfigurationHandler))]
		internal delegate IntPtr DPSPDFAnnotationGroupItemConfigurationHandler (IntPtr block, IntPtr item, IntPtr container, IntPtr tintColor);
		//
		// This class bridges native block invocations that call into C#
		//
		static internal class SDPSPDFAnnotationGroupItemConfigurationHandler {
			static internal readonly DPSPDFAnnotationGroupItemConfigurationHandler Handler = Invoke;
			[MonoPInvokeCallback (typeof (DPSPDFAnnotationGroupItemConfigurationHandler))]
			static unsafe IntPtr Invoke (IntPtr block, IntPtr item, IntPtr container, IntPtr tintColor)
			{
				var descriptor = (BlockLiteral*) block;
				var del = (global::PSPDFKit.Model.PSPDFAnnotationGroupItemConfigurationHandler) (descriptor->Target);
				UIImage retval = del (Runtime.GetNSObject<PSPDFKit.Model.PSPDFAnnotationGroupItem> (item)!, Runtime.GetNSObject<NSObject> (container)!, Runtime.GetNSObject<UIColor> (tintColor)!);
				return retval.GetHandle ();
			}
		} /* class SDPSPDFAnnotationGroupItemConfigurationHandler */
	} /* class PSPDFAnnotationGroupItem_PSPDFPresets */
#endif
}
