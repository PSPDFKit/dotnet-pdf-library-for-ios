using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CoreGraphics;
using Foundation;
using CoreFoundation;
using ObjCRuntime;
#if __IOS__
using UIKit;
#elif __MAC__
using AppKit;
#endif

namespace PSPDFKit.Model {

	public partial class PSPDFKitGlobal {
#if __IOS__
		public const string LibraryPath = "__Internal";
		public const string DlPath = null;
#elif __MAC__
		public const string LibraryPath = "__Internal";
		public const string DlPath = null;
#endif

		internal const string HybridEnvironmentKey = "com.pspdfkit.hybrid-environment";
		internal const string ProductIdentifier = "DotNetBindingsIOS";

		public NSObject this [NSString settingKey] {
			get => GetObject (settingKey);
			set => SetObject (value, settingKey);
		}

		public static void SetLicenseKey (string? licenseKey)
		{
			var key = CFString.CreateNative (HybridEnvironmentKey);
			var value = CFString.CreateNative (ProductIdentifier);
			try {
				using var options = new NSMutableDictionary ();
				options.LowlevelSetObject (value, key);
				_SetLicenseKey (licenseKey, options);
			} finally {
				CFString.ReleaseNative (key);
				CFString.ReleaseNative (value);
			}
		}

		public static void SetLicenseKey (string? licenseKey, NSMutableDictionary? options)
		{
			if (options is null) {
				SetLicenseKey (licenseKey);
				return;
			}

			var key = CFString.CreateNative (HybridEnvironmentKey);
			var value = CFString.CreateNative (ProductIdentifier);
			try {
				options.LowlevelSetObject (value, key);
				_SetLicenseKey (licenseKey, options);
			} finally {
				CFString.ReleaseNative (key);
				CFString.ReleaseNative (value);
			}
		}
	}

	public partial class PSPDFAESCryptoDataProvider : NSObject {

		public PSPDFAESCryptoDataProvider (NSUrl url, PSPDFAESCryptoPassphraseProvider passphraseProvider, bool useLegacyFileFormatUrl = false)
		{
			Handle = useLegacyFileFormatUrl ? 
				InitWithLegacyFileFormatURL (url, passphraseProvider) :
				InitWithURL (url, passphraseProvider);
		}

		public static nuint DefaultPBKDFNumberOfRounds {
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (PSPDFKitGlobal.DlPath, 0);
				return Dlfcn.GetNUInt (RTLD_MAIN_ONLY, "PSPDFDefaultPBKDFNumberOfRounds");
			}
		}
	}

	public partial class PSPDFAESCryptoInputStream {
		public nint Read (byte[] buffer)
		{
			unsafe {
				fixed (byte* ptr = buffer)
					return _Read ((IntPtr) ptr, (nuint) buffer.Length);
			}
		}
	}

	public partial class PSPDFAnnotation : PSPDFModel {

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFStringFromAnnotationType")]
		private static extern IntPtr _StringFromAnnotationType (nuint annotationType);

		public static string GetString (PSPDFAnnotationType annotationType)
		{
			return (string) Runtime.GetNSObject<NSString> (_StringFromAnnotationType ((nuint) (ulong) annotationType));
		}

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFAnnotationTypeFromString")]
		private static extern nuint _AnnotationTypeFromString (IntPtr theString);

		public static PSPDFAnnotationType GetAnnotationType (NSString theString)
		{
			return (PSPDFAnnotationType)(ulong) _AnnotationTypeFromString (theString.Handle);
		}
	}

	public partial class PSPDFAnnotationGroupItem {
		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFAnnotationStateVariantIDMake")]
		private static extern IntPtr _VariantIdentifier (IntPtr state, IntPtr variant);

		public static NSString GetStateVariantIdentifier (NSString state, NSString variant)
		{
			return Runtime.GetNSObject<NSString> (_VariantIdentifier (
				state == null ? IntPtr.Zero : state.Handle,
				variant == null ? IntPtr.Zero : variant.Handle));
		}

		public static PSPDFAnnotationStateVariantId GetStateVariantIdentifier (PSPDFAnnotationString annotationType, PSPDFAnnotationVariantString annotationVariant)
		{
			return PSPDFAnnotationStateVariantIdExtensions.GetValue (GetStateVariantIdentifier (annotationType.GetConstant (), annotationVariant.GetConstant ()));
		}

		public static PSPDFAnnotationStateVariantId GetStateVariantIdentifier (PSPDFAnnotationString annotationType)
		{
			return PSPDFAnnotationStateVariantIdExtensions.GetValue (GetStateVariantIdentifier (annotationType.GetConstant (), null));
		}

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFAnnotationStateFromStateVariantIdentifier")]
		private static extern IntPtr _StateFromStateVariantIdentifier (IntPtr state);

		public static NSString GetStateFromStateVariantIdentifier (NSString stateVariantIdentifier)
		{
			if (stateVariantIdentifier == null)
				throw new ArgumentNullException (nameof (stateVariantIdentifier));

			return Runtime.GetNSObject<NSString> (_StateFromStateVariantIdentifier (stateVariantIdentifier.Handle));
		}

		public static PSPDFAnnotationString GetStateFromStateVariantIdentifier (PSPDFAnnotationString stateVariantIdentifier)
		{
			return PSPDFAnnotationStringExtensions.GetValue (stateVariantIdentifier.GetConstant ());
		}
	}

	public partial class PSPDFGlyph : NSObject {

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFRectsFromGlyphs")]
		static extern IntPtr _RectsFromGlyphs (IntPtr glyphs, CGRect boundingBox);

		public static NSValue [] GetRectsFromGlyphs (PSPDFGlyph [] glyphs, CGRect boundingBox)
		{
			if (glyphs == null)
				return NSArray.ArrayFromHandle<NSValue> (_RectsFromGlyphs (IntPtr.Zero, boundingBox));

			var objs = new List<NSObject> ();

			foreach (var glyph in glyphs)
				objs.Add (glyph);

			NSArray arry = NSArray.FromNSObjects (objs.ToArray ());
			return NSArray.ArrayFromHandle<NSValue> (_RectsFromGlyphs (arry.Handle, boundingBox));
		}

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFBoundingBoxFromGlyphs")]
		static extern CGRect _BoundingBoxFromGlyphs (IntPtr glyphs);

		public static CGRect GetBoundingBoxFromGlyphs (PSPDFGlyph [] glyphs)
		{
			var objs = new List<NSObject> ();

			foreach (var glyph in glyphs)
				objs.Add (glyph);

			NSArray arry = NSArray.FromNSObjects (objs.ToArray ());
			return _BoundingBoxFromGlyphs (arry.Handle);
		}

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFStringFromGlyphs")]
		static extern IntPtr _StringFromGlyphs (IntPtr glyphs);

		public static string GetStringFromGlyphs (PSPDFGlyph [] glyphs)
		{
			var objs = new List<NSObject> ();

			foreach (var glyph in glyphs)
				objs.Add (glyph);

			NSArray arr = NSArray.FromNSObjects (objs.ToArray ());
			return (string) Runtime.GetNSObject<NSString> (_StringFromGlyphs (arr.Handle));
		}

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFRangeFromGlyphs")]
		static extern NSRange _RangeFromGlyphs (IntPtr glyphs);

		public static NSRange GetRangeFromGlyphs (PSPDFGlyph [] glyphs)
		{
			var objs = new List<NSObject> ();

			foreach (var glyph in glyphs)
				objs.Add (glyph);

			NSArray arr = NSArray.FromNSObjects (objs.ToArray ());
			return _RangeFromGlyphs (arr.Handle);
		}

		public static NSRange InvalidGlyphRange {
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (PSPDFKitGlobal.DlPath, 0);
				var ptr = Dlfcn.GetIntPtr (RTLD_MAIN_ONLY, "PSPDFInvalidGlyphRange");
				return (NSRange) Marshal.PtrToStructure (ptr, typeof (NSRange));
			}
		}
	}

	public partial class PSPDFInkAnnotation {

		public PSPDFInkAnnotation (NSArray<NSValue> [] lines) : base (NSObjectFlag.Empty)
		{
			var objs = NSArray.FromNSObjects (lines);
			Handle = InitWithLines (objs.Handle);
		}

		public PSPDFInkAnnotation (List<PSPDFDrawingPoint []> lines) : base (NSObjectFlag.Empty)
		{
			var arr = GetArrayFromList (lines);
			Handle = InitWithLines (arr.Handle);
		}

		[DllImport (Constants.ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
		static extern void void_objc_msgSend_IntPtr (IntPtr receiver, IntPtr selector, IntPtr arg1);

		public void SetLines (List<PSPDFDrawingPoint []> lines)
		{
			var arr = GetArrayFromList (lines);
			void_objc_msgSend_IntPtr (Handle, Selector.GetHandle ("setLines:"), arr.Handle);
		}

		public List<PSPDFDrawingPoint []> GetLines ()
		{
			NSArray<NSValue> [] lines = Lines;
			if (lines == null)
				return null;

			var ret = new List<PSPDFDrawingPoint []> (lines.Length);
			for (int i = 0; i < lines.Length; i++) {
				var arr = lines [i];
				var count = arr.Count;
				var points = new PSPDFDrawingPoint [count];
				for (nuint j = 0; j < count; j++)
					points [j] = arr.GetItem<NSValue> (j).GetPSPDFDrawingPoint ();
				ret [i] = points;
			}
			return ret;
		}

		static NSMutableArray GetArrayFromList (List<PSPDFDrawingPoint []> lines) {
			if (lines == null || lines.Count == 0)
				return new NSMutableArray ();
			
			var mainArray = new NSMutableArray ((nuint) lines.Count);
			for (nuint i = 0; i < mainArray.Count; i++) {
				var points = lines [(int) i];
				var innerArr = new NSMutableArray ((nuint) points.Length);
				for (int j = 0; j < points.Length; j++)
					innerArr.Insert ((null as NSValue).FromPSPDFDrawingPoint (points[j]), j);
				mainArray.Insert (innerArr, (nint) i);
			}

			return mainArray;
		}

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFBoundingBoxFromLines")]
		static extern CGRect _BoundingBoxFromLines (IntPtr lines, nfloat lineWidth);

		public static CGRect GetBoundingBoxFromLines (NSArray lines, nfloat lineWidth) => 
			_BoundingBoxFromLines (lines.Handle, lineWidth);

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFConvertViewLinesToPDFLines")]
		static extern IntPtr _ConvertViewLinesToPDFLines (IntPtr lines, IntPtr pageInfo, CGRect viewBounds);

		public static NSArray<NSValue> [] ConvertViewLinesToPdfLines (NSArray<NSValue> [] lines, PSPDFPageInfo pageInfo, CGRect viewBounds)
		{
			var arr = NSArray.FromNSObjects (lines);
			var ret = _ConvertViewLinesToPDFLines (arr.Handle, pageInfo.Handle, viewBounds);
			return NSArray.ArrayFromHandle<NSArray<NSValue>> (ret);
		}

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFConvertViewLineToPDFLines")]
		static extern IntPtr _ConvertViewLineToPDFLines (IntPtr line, IntPtr pageInfo, CGRect bounds);

		public static NSValue [] ConvertViewLineToPdfLines (NSValue [] line, PSPDFPageInfo pageInfo, CGRect bounds)
		{
			var arr = NSArray.FromNSObjects (line);
			var ret = _ConvertViewLineToPDFLines (arr.Handle, pageInfo.Handle, bounds);
			return NSArray.ArrayFromHandle<NSValue> (ret);
		}

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFConvertPDFLinesToViewLines")]
		static extern IntPtr _ConvertPDFLinesToViewLines (IntPtr lines, IntPtr pageInfo, CGRect viewBounds);

		public static NSArray<NSValue> [] ConvertPdfLinesToViewLines (NSArray<NSValue> [] lines, PSPDFPageInfo pageInfo, CGRect viewBounds)
		{
			var arr = NSArray.FromNSObjects (lines);
			var ret = _ConvertPDFLinesToViewLines (arr.Handle, pageInfo.Handle, viewBounds);
			return NSArray.ArrayFromHandle<NSArray<NSValue>> (ret);
		}
	}

	public partial class PSPDFLibrary : NSObject {

		public static nuint LibraryVersion {
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (PSPDFKitGlobal.DlPath, 0);
				return Dlfcn.GetNUInt (RTLD_MAIN_ONLY, "PSPDFLibraryVersion");
			}
		}
	}

	public partial class PSPDFLibraryOptions : DictionaryContainer {

		public NSRange PreviewRange {
			get => WeakPreviewRange.RangeValue;
			set => WeakPreviewRange = NSValue.FromRange (value);
		}
	}

	public static class PSPDFLocalization {

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFLocalize")]
		static extern IntPtr _Localize (IntPtr stringToken);

		public static string Localize (string stringToken)
		{
			var ret = _Localize (new NSString (stringToken).Handle);
			var str = Runtime.GetNSObject<NSString> (ret);
			return (string) str;
		}

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFLocalizeWithoutFallback")]
		static extern IntPtr _LocalizeWithoutFallback (IntPtr stringToken);

		public static string LocalizeWithoutFallback (string stringToken)
		{
			var ret = _LocalizeWithoutFallback (new NSString (stringToken).Handle);
			var str = Runtime.GetNSObject<NSString> (ret);
			return (string) str;
		}

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFSetLocalizationDictionary")]
		static extern void _SetLocalizationDictionary (IntPtr localizationDict);

		[Advice ("Set on the main thread only.")]
		public static void SetLocalizationDictionary (NSDictionary<NSString, NSDictionary<NSString, NSString>> localizationDict) =>
			_SetLocalizationDictionary (localizationDict == null ? IntPtr.Zero : localizationDict.Handle);

		// Manually bind block

		delegate IntPtr SetLocalizationBlockHandler (IntPtr block, IntPtr strToLocalize);
		static readonly SetLocalizationBlockHandler callback = TrampolineSetLocalizationBlockHandler;

#if __IOS__
		[MonoPInvokeCallback (typeof (SetLocalizationBlockHandler))]
#endif
		static unsafe IntPtr TrampolineSetLocalizationBlockHandler (IntPtr block, IntPtr strToLocalize)
		{
			var descriptor = (BlockLiteral*) block;
			var func = (PSPDFLocalizationHandler) (descriptor->Target);
			var retval = func (NSString.FromHandle (strToLocalize));
			return NSString.CreateNative (retval, autorelease: true);
		}

		public delegate string PSPDFLocalizationHandler (string stringToLocalize);

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFSetLocalizationBlock")]
		static unsafe extern void _SetLocalizationHandler (void* localizationDict);

		public static void SetLocalizationHandler (PSPDFLocalizationHandler localizationHandler)
		{
			if (localizationHandler == null)
				throw new ArgumentNullException (nameof (localizationHandler));

			unsafe {
				BlockLiteral* block_ptr_handler;
				BlockLiteral block_handler;
				block_handler = new BlockLiteral ();
				block_ptr_handler = &block_handler;
				block_handler.SetupBlock (callback, localizationHandler);

				_SetLocalizationHandler ((void*) block_ptr_handler);
				block_ptr_handler->CleanupBlock ();
			}
		}
	}

	public partial class PSPDFPageInfo : NSObject {

		public static readonly nuint PageNull = nuint.MaxValue;

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFConvertViewPointToPDFPoint")]
		static extern CGPoint _ConvertViewPointToPdfPoint (CGPoint viewPoint, IntPtr pageInfo, CGRect bounds);

		public static CGPoint ConvertViewPointToPdfPoint (CGPoint viewPoint, PSPDFPageInfo pageInfo, CGRect viewBounds) => _ConvertViewPointToPdfPoint (viewPoint, pageInfo.Handle, viewBounds);

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFConvertPDFPointToViewPoint")]
		static extern CGPoint _ConvertPdfPointToViewPoint (CGPoint pdfPoint, IntPtr pageInfo, CGRect bounds);

		public static CGPoint ConvertPdfPointToViewPoint (CGPoint pdfPoint, PSPDFPageInfo pageInfo, CGRect viewBounds) => _ConvertPdfPointToViewPoint (pdfPoint, pageInfo.Handle, viewBounds);

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFConvertPDFRectToViewRect")]
		static extern CGRect _ConvertPdfRectToViewRect (CGRect pdfRect, IntPtr pageInfo, CGRect viewBounds);

		public static CGRect ConvertPdfRectToViewRect (CGRect pdfRect, PSPDFPageInfo pageInfo, CGRect viewBounds) => _ConvertPdfRectToViewRect (pdfRect, pageInfo.Handle, viewBounds);

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFConvertViewRectToPDFRect")]
		static extern CGRect _ConvertViewRectToPdfRect (CGRect viewRect, IntPtr pageInfo, CGRect viewBounds);

		public static CGRect ConvertViewRectToPdfRect (CGRect viewRect, PSPDFPageInfo pageInfo, CGRect viewBounds) => _ConvertViewRectToPdfRect (viewRect, pageInfo.Handle, viewBounds);
	}

	public partial class PSPDFProcessor : NSObject {

		public static CGRect PaperSizeA4 {
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (PSPDFKitGlobal.DlPath, 0);
				return Dlfcn.GetCGRect (RTLD_MAIN_ONLY, "PSPDFPaperSizeA4");
			}
		}

		public static CGRect PaperSizeLetter {
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (PSPDFKitGlobal.DlPath, 0);
				return Dlfcn.GetCGRect (RTLD_MAIN_ONLY, "PSPDFPaperSizeLetter");
			}
		}
	}

	public partial class PSPDFProcessorGenerationOptions : DictionaryContainer {
#if __IOS__
		public PSPDFAnnotationType? AnnotationTypes {
			get => (PSPDFAnnotationType) (ulong) GetNUIntValue (PSPDFProcessorGenerationOptionsKeys.AnnotationTypesKey);
			set => SetNumberValue (PSPDFProcessorGenerationOptionsKeys.AnnotationTypesKey, (nuint) (ulong) value);
		}

		public int? KeyLength {
			get => GetInt32Value (PSPDFProcessorGenerationOptionsKeys.KeyLengthKey);
			set {
				if (value > 128 || value < 40 || value % 8 != 0)
					throw new InvalidOperationException ($"{nameof (value)} must be divisible by 8 and in the range of 40 to 128");

				SetNumberValue (PSPDFProcessorGenerationOptionsKeys.KeyLengthKey, value);
			}
		}

		public UIEdgeInsets? PageBorderMargin {
			get => (Dictionary [PSPDFProcessorGenerationOptionsKeys.PageBorderMarginKey] as NSValue)?.UIEdgeInsetsValue;
			set => SetNativeValue (PSPDFProcessorGenerationOptionsKeys.PageBorderMarginKey, value == null || !value.HasValue ? null : NSValue.FromUIEdgeInsets (value.Value));
		}
#endif
	}

	public partial class PSPDFDocumentAnnotationWriteOptions : DictionaryContainer {

		public PSPDFAnnotationType? GenerateAppearanceStreamForTypes {
			get => (PSPDFAnnotationType) (ulong) GetNUIntValue (PSPDFDocument.WriteOptionsGenerateAppearanceStreamForTypeKey);
			set => SetNumberValue (PSPDFDocument.WriteOptionsGenerateAppearanceStreamForTypeKey, (nuint) (ulong) value);
		}
	}

	public partial class PSPDFPageTemplate {
		public PSPDFPageTemplate (PSPDFDocument document, nuint sourcePageIndex, bool useTiledPattern = false)
		{
			if (useTiledPattern)
				InitializeHandle (InitWithTiledPattern (document, sourcePageIndex));
			else
				InitializeHandle (InitWithDocument (document, sourcePageIndex));
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
	public unsafe static partial class NSValue_PSPDFModel {
		[DllImport (Constants.ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
		public extern static PSPDFDrawingPoint PSPDFDrawingPoint_objc_msgSend (IntPtr receiver, IntPtr selector);

		[DllImport (Constants.ObjectiveCLibrary, EntryPoint = "objc_msgSend_stret")]
		public extern static void PSPDFDrawingPoint_objc_msgSend_stret (out PSPDFDrawingPoint retval, IntPtr receiver, IntPtr selector);

		[DllImport (Constants.ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
		public extern static IntPtr IntPtr_objc_msgSend_PSPDFDrawingPoint (IntPtr receiver, IntPtr selector, PSPDFDrawingPoint arg1);

		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		static readonly IntPtr class_ptr = Class.GetHandle ("NSValue");

		[Export ("pspdf_valueWithDrawingPoint:")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static NSValue FromPSPDFDrawingPoint (this NSValue This, PSPDFDrawingPoint point)
		{
			return Runtime.GetNSObject<NSValue> (IntPtr_objc_msgSend_PSPDFDrawingPoint (class_ptr, Selector.GetHandle ("pspdf_valueWithDrawingPoint:"), point))!;
		}

		[Export ("pspdf_drawingPointValue")]
		[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
		public static PSPDFDrawingPoint GetPSPDFDrawingPoint (this NSValue This)
		{
			PSPDFDrawingPoint ret;
			if (ObjCRuntime.Runtime.IsARM64CallingConvention) {
				ret = PSPDFDrawingPoint_objc_msgSend (This.Handle, Selector.GetHandle ("pspdf_drawingPointValue"));
			} else {
				PSPDFDrawingPoint_objc_msgSend_stret (out ret, This.Handle, Selector.GetHandle ("pspdf_drawingPointValue"));
			}
			return ret!;
		}
	} /* class NSValue_PSPDFModel */
#endif
}
