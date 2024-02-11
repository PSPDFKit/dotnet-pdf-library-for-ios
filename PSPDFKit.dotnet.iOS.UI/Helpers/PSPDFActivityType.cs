//
// Manually fixed code for PSPDFActivityType
//
// Auto-generated from generator.cs, do not edit
//
// We keep references to objects, so warning 414 is expected

#pragma warning disable 414

using System;
using Foundation;
using ObjCRuntime;

namespace PSPDFKit.UI {
	public enum PSPDFActivityType : int {
		Null = 0,
		PostToFacebook = 1,
		PostToTwitter = 2,
		PostToWeibo = 3,
		Message = 4,
		Mail = 5,
		Print = 6,
		CopyToPasteboard = 7,
		AssignToContact = 8,
		SaveToCameraRoll = 9,
		AddToReadingList = 10,
		PostToFlickr = 11,
		PostToVimeo = 12,
		PostToTencentWeibo = 13,
		AirDrop = 14,
		OpenInIBooks = 15,
		MarkupAsPdf = 16,
	}

	[BindingImpl (BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
	static public partial class PSPDFActivityTypeExtensions {

		static readonly IntPtr UIKitHandle = Dlfcn.dlopen (Constants.UIKitLibrary, 0);
		static IntPtr[] values = new IntPtr[16];

		[Field ("UIActivityTypePostToFacebook", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypePostToFacebook {
			get {
				fixed (IntPtr* storage = &values[0])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypePostToFacebook", storage);
			}
		}

		[Field ("UIActivityTypePostToTwitter", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypePostToTwitter {
			get {
				fixed (IntPtr* storage = &values[1])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypePostToTwitter", storage);
			}
		}

		[Field ("UIActivityTypePostToWeibo", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypePostToWeibo {
			get {
				fixed (IntPtr* storage = &values[2])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypePostToWeibo", storage);
			}
		}

		[Field ("UIActivityTypeMessage", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypeMessage {
			get {
				fixed (IntPtr* storage = &values[3])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypeMessage", storage);
			}
		}

		[Field ("UIActivityTypeMail", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypeMail {
			get {
				fixed (IntPtr* storage = &values[4])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypeMail", storage);
			}
		}

		[Field ("UIActivityTypePrint", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypePrint {
			get {
				fixed (IntPtr* storage = &values[5])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypePrint", storage);
			}
		}

		[Field ("UIActivityTypeCopyToPasteboard", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypeCopyToPasteboard {
			get {
				fixed (IntPtr* storage = &values[6])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypeCopyToPasteboard", storage);
			}
		}

		[Field ("UIActivityTypeAssignToContact", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypeAssignToContact {
			get {
				fixed (IntPtr* storage = &values[7])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypeAssignToContact", storage);
			}
		}

		[Field ("UIActivityTypeSaveToCameraRoll", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypeSaveToCameraRoll {
			get {
				fixed (IntPtr* storage = &values[8])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypeSaveToCameraRoll", storage);
			}
		}

		[Field ("UIActivityTypeAddToReadingList", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypeAddToReadingList {
			get {
				fixed (IntPtr* storage = &values[9])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypeAddToReadingList", storage);
			}
		}

		[Field ("UIActivityTypePostToFlickr", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypePostToFlickr {
			get {
				fixed (IntPtr* storage = &values[10])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypePostToFlickr", storage);
			}
		}

		[Field ("UIActivityTypePostToVimeo", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypePostToVimeo {
			get {
				fixed (IntPtr* storage = &values[11])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypePostToVimeo", storage);
			}
		}

		[Field ("UIActivityTypePostToTencentWeibo", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypePostToTencentWeibo {
			get {
				fixed (IntPtr* storage = &values[12])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypePostToTencentWeibo", storage);
			}
		}

		[Field ("UIActivityTypeAirDrop", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypeAirDrop {
			get {
				fixed (IntPtr* storage = &values[13])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypeAirDrop", storage);
			}
		}

		[Field ("UIActivityTypeOpenInIBooks", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypeOpenInIBooks {
			get {
				fixed (IntPtr* storage = &values[14])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypeOpenInIBooks", storage);
			}
		}

		[Field ("UIActivityTypeMarkupAsPDF", "/System/Library/Frameworks/UIKit.framework/UIKit")]
		internal unsafe static IntPtr UIActivityTypeMarkupAsPDF {
			get {
				fixed (IntPtr* storage = &values[15])
					return Dlfcn.CachePointer (UIKitHandle, "UIActivityTypeMarkupAsPDF", storage);
			}
		}

		public static NSString GetConstant (this PSPDFActivityType self)
		{
			IntPtr ptr = IntPtr.Zero;
			switch ((int) self) {
			case 1: // PSPDFActivityType.PostToFacebook
				ptr = UIActivityTypePostToFacebook;
				break;
			case 2: // PSPDFActivityType.PostToTwitter
				ptr = UIActivityTypePostToTwitter;
				break;
			case 3: // PSPDFActivityType.PostToWeibo
				ptr = UIActivityTypePostToWeibo;
				break;
			case 4: // PSPDFActivityType.Message
				ptr = UIActivityTypeMessage;
				break;
			case 5: // PSPDFActivityType.Mail
				ptr = UIActivityTypeMail;
				break;
			case 6: // PSPDFActivityType.Print
				ptr = UIActivityTypePrint;
				break;
			case 7: // PSPDFActivityType.CopyToPasteboard
				ptr = UIActivityTypeCopyToPasteboard;
				break;
			case 8: // PSPDFActivityType.AssignToContact
				ptr = UIActivityTypeAssignToContact;
				break;
			case 9: // PSPDFActivityType.SaveToCameraRoll
				ptr = UIActivityTypeSaveToCameraRoll;
				break;
			case 10: // PSPDFActivityType.AddToReadingList
				ptr = UIActivityTypeAddToReadingList;
				break;
			case 11: // PSPDFActivityType.PostToFlickr
				ptr = UIActivityTypePostToFlickr;
				break;
			case 12: // PSPDFActivityType.PostToVimeo
				ptr = UIActivityTypePostToVimeo;
				break;
			case 13: // PSPDFActivityType.PostToTencentWeibo
				ptr = UIActivityTypePostToTencentWeibo;
				break;
			case 14: // PSPDFActivityType.AirDrop
				ptr = UIActivityTypeAirDrop;
				break;
			case 15: // PSPDFActivityType.OpenInIBooks
				ptr = UIActivityTypeOpenInIBooks;
				break;
			case 16: // PSPDFActivityType.MarkupAsPdf
				ptr = UIActivityTypeMarkupAsPDF;
				break;
			}
			return (NSString) Runtime.GetNSObject (ptr);
		}

		public static PSPDFActivityType GetValue (NSString constant)
		{
			if (constant == null)
				return PSPDFActivityType.Null;
			if (constant.IsEqualTo (UIActivityTypePostToFacebook))
				return PSPDFActivityType.PostToFacebook;
			if (constant.IsEqualTo (UIActivityTypePostToTwitter))
				return PSPDFActivityType.PostToTwitter;
			if (constant.IsEqualTo (UIActivityTypePostToWeibo))
				return PSPDFActivityType.PostToWeibo;
			if (constant.IsEqualTo (UIActivityTypeMessage))
				return PSPDFActivityType.Message;
			if (constant.IsEqualTo (UIActivityTypeMail))
				return PSPDFActivityType.Mail;
			if (constant.IsEqualTo (UIActivityTypePrint))
				return PSPDFActivityType.Print;
			if (constant.IsEqualTo (UIActivityTypeCopyToPasteboard))
				return PSPDFActivityType.CopyToPasteboard;
			if (constant.IsEqualTo (UIActivityTypeAssignToContact))
				return PSPDFActivityType.AssignToContact;
			if (constant.IsEqualTo (UIActivityTypeSaveToCameraRoll))
				return PSPDFActivityType.SaveToCameraRoll;
			if (constant.IsEqualTo (UIActivityTypeAddToReadingList))
				return PSPDFActivityType.AddToReadingList;
			if (constant.IsEqualTo (UIActivityTypePostToFlickr))
				return PSPDFActivityType.PostToFlickr;
			if (constant.IsEqualTo (UIActivityTypePostToVimeo))
				return PSPDFActivityType.PostToVimeo;
			if (constant.IsEqualTo (UIActivityTypePostToTencentWeibo))
				return PSPDFActivityType.PostToTencentWeibo;
			if (constant.IsEqualTo (UIActivityTypeAirDrop))
				return PSPDFActivityType.AirDrop;
			if (constant.IsEqualTo (UIActivityTypeOpenInIBooks))
				return PSPDFActivityType.OpenInIBooks;
			if (constant.IsEqualTo (UIActivityTypeMarkupAsPDF))
				return PSPDFActivityType.MarkupAsPdf;
			return PSPDFActivityType.Null;
		}
	}
}
