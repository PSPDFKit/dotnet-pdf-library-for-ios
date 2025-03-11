using System;
using Foundation;
using ObjCRuntime;
using PSPDFKit.Model;
using UIKit;

namespace PSPDFKit.UI {
	[Native]
	public enum PSPDFAnnotationStateManagerStylusMode : ulong {
		FromStylusManager,
		Direct,
		Stylus
	}

	public enum PSPDFAnnotationStringUI {
		[DefaultEnumValue]
		[Field (null)]
		Null,
		[Field ("PSPDFAnnotationStringLink", PSPDFKitGlobal.LibraryPath)]
		Link,
		[Field ("PSPDFAnnotationStringHighlight", PSPDFKitGlobal.LibraryPath)]
		Highlight,
		[Field ("PSPDFAnnotationStringStrikeOut", PSPDFKitGlobal.LibraryPath)]
		StrikeOut,
		[Field ("PSPDFAnnotationStringUnderline", PSPDFKitGlobal.LibraryPath)]
		Underline,
		[Field ("PSPDFAnnotationStringSquiggly", PSPDFKitGlobal.LibraryPath)]
		Squiggly,
		[Field ("PSPDFAnnotationStringNote", PSPDFKitGlobal.LibraryPath)]
		Note,
		[Field ("PSPDFAnnotationStringFreeText", PSPDFKitGlobal.LibraryPath)]
		FreeText,
		[Field ("PSPDFAnnotationStringInk", PSPDFKitGlobal.LibraryPath)]
		Ink,
		[Field ("PSPDFAnnotationStringSquare", PSPDFKitGlobal.LibraryPath)]
		Square,
		[Field ("PSPDFAnnotationStringCircle", PSPDFKitGlobal.LibraryPath)]
		Circle,
		[Field ("PSPDFAnnotationStringLine", PSPDFKitGlobal.LibraryPath)]
		Line,
		[Field ("PSPDFAnnotationStringPolygon", PSPDFKitGlobal.LibraryPath)]
		Polygon,
		[Field ("PSPDFAnnotationStringPolyLine", PSPDFKitGlobal.LibraryPath)]
		PolyLine,
		[Field ("PSPDFAnnotationStringSignature", PSPDFKitGlobal.LibraryPath)]
		Signature,
		[Field ("PSPDFAnnotationStringStamp", PSPDFKitGlobal.LibraryPath)]
		Stamp,
		[Field ("PSPDFAnnotationStringEraser", PSPDFKitGlobal.LibraryPath)]
		Eraser,
		[Field ("PSPDFAnnotationStringSound", PSPDFKitGlobal.LibraryPath)]
		Sound,
		[Field ("PSPDFAnnotationStringImage", PSPDFKitGlobal.LibraryPath)]
		Image,
		[Field ("PSPDFAnnotationStringRedaction", PSPDFKitGlobal.LibraryPath)]
		Redaction,
		[Field ("PSPDFAnnotationStringWidget", PSPDFKitGlobal.LibraryPath)]
		Widget,
		[Field ("PSPDFAnnotationStringFile", PSPDFKitGlobal.LibraryPath)]
		File,
		[Field ("PSPDFAnnotationStringRichMedia", PSPDFKitGlobal.LibraryPath)]
		RichMedia,
		[Field ("PSPDFAnnotationStringScreen", PSPDFKitGlobal.LibraryPath)]
		Screen,
		[Field ("PSPDFAnnotationStringCaret", PSPDFKitGlobal.LibraryPath)]
		Caret,
		[Field ("PSPDFAnnotationStringPopup", PSPDFKitGlobal.LibraryPath)]
		Popup,
		[Field ("PSPDFAnnotationStringWatermark", PSPDFKitGlobal.LibraryPath)]
		Watermark,
		[Field ("PSPDFAnnotationStringTrapNet", PSPDFKitGlobal.LibraryPath)]
		TrapNet,
		[Field ("PSPDFAnnotationString3D", PSPDFKitGlobal.LibraryPath)]
		_3D,

		// UI
		[Field ("PSPDFAnnotationStringSelectionTool", PSPDFKitGlobal.LibraryPath)]
		SelectionTool,
		[Field ("PSPDFAnnotationStringSavedAnnotations", PSPDFKitGlobal.LibraryPath)]
		SavedAnnotations,
	}

	[Native]
	public enum PSPDFPresentationStyle : ulong {
		None,
		HalfModal
	}

	[Native]
	[Flags]
	public enum PSPDFAppearanceMode : ulong {
		Default = 0,
		Sepia = 1 << 0,
		Night = 1 << 1,
		All = Default | Sepia | Night
	}

	[Native]
	public enum PSPDFBookmarkIndicatorImageType : long {
		Large,
		Medium,
		Small
	}

	[Native]
	public enum PSPDFIdleTimerManagement : ulong {
		Manual,
		ExtendedTime,
		ExtendedTimeExternalScreenDisablesTimer,
		DisablesTimer,
	}

	[Native]
	public enum PSPDFCollectionReusableFilterViewStyle : long {
		None,
		LightBlur,
		DarkBlur,
		ExtraLightBlur
	}

	// TODO: See Helpers/PSPDFActivityType.cs, manually bound due to a generator bug
	//public enum PSPDFActivityType {
	//	[DefaultEnumValue]
	//	[Field (null)]
	//	Null,
	//	[Field ("PSPDFActivityTypeOpenIn", PSPDFKitGlobal.LibraryPath)]
	//	OpenIn,
	//	// TODO: This needs a generator fix
	//	[Field ("UIActivityTypePostToFacebook", Constants.UIKitLibrary)]
	//	PostToFacebook,
	//	[Field ("UIActivityTypePostToTwitter", Constants.UIKitLibrary)]
	//	PostToTwitter,
	//	[Field ("UIActivityTypePostToWeibo", Constants.UIKitLibrary)]
	//	PostToWeibo,
	//	[Field ("UIActivityTypeMessage", Constants.UIKitLibrary)]
	//	Message,
	//	[Field ("UIActivityTypeMail", Constants.UIKitLibrary)]
	//	Mail,
	//	[Field ("UIActivityTypePrint", Constants.UIKitLibrary)]
	//	Print,
	//	[Field ("UIActivityTypeCopyToPasteboard", Constants.UIKitLibrary)]
	//	CopyToPasteboard,
	//	[Field ("UIActivityTypeAssignToContact", Constants.UIKitLibrary)]
	//	AssignToContact,
	//	[Field ("UIActivityTypeSaveToCameraRoll", Constants.UIKitLibrary)]
	//	SaveToCameraRoll,
	//	[Field ("UIActivityTypeAddToReadingList", Constants.UIKitLibrary)]
	//	AddToReadingList,
	//	[Field ("UIActivityTypePostToFlickr", Constants.UIKitLibrary)]
	//	PostToFlickr,
	//	[Field ("UIActivityTypePostToVimeo", Constants.UIKitLibrary)]
	//	PostToVimeo,
	//	[Field ("UIActivityTypePostToTencentWeibo", Constants.UIKitLibrary)]
	//	PostToTencentWeibo,
	//	[Field ("UIActivityTypeAirDrop", Constants.UIKitLibrary)]
	//	AirDrop,
	//	[Field ("UIActivityTypeOpenInIBooks", Constants.UIKitLibrary)]
	//	OpenInIBooks,
	//	[Field ("UIActivityTypeMarkupAsPDF", Constants.UIKitLibrary)]
	//	MarkupAsPdf,
	//}

	[Native]
	public enum PSPDFPageTransition : ulong {
		ScrollPerSpread,
		ScrollContinuous,
		Curl,
	}

	[Native]
	public enum PSPDFPageMode : ulong {
		Single,
		Double,
		Automatic
	}

	[Native]
	public enum PSPDFScrollDirection : ulong {
		Horizontal,
		Vertical
	}

	[Native]
	public enum PSPDFViewMode : ulong {
		Document,
		Thumbnails,
		DocumentEditor,
		ContentEditing,
	}

	[Native]
	public enum PSPDFLinkAction : ulong {
		None,
		AlertView,
		OpenSafari,
		InlineBrowser,
		InlineWebViewController
	}

	[Native]
	public enum PSPDFTextSelectionMode : ulong {
		Regular,
		Simple,
		Automatic,
	}

	[Native]
	public enum PSPDFDrawCreateMode : ulong {
		Separate,
		MergeIfPossible,
		Automatic,
	}

	[Obsolete ("Use 'UIAction.Identifier' or 'UIMenu.Identifier' in the modern menu system instead.")]
	[Native]
	[Flags]
	public enum PSPDFTextSelectionMenuAction : ulong {
		None = 0,
		Search = 1uL << 0,
		Define = 1uL << 1,
		Wikipedia = 1uL << 2,
		Speak = 1uL << 3,
		Share = 1uL << 4,
		Copy = 1uL << 5,
		Markup = 1uL << 6,
		Redact = 1uL << 7,
		CreateLink = 1uL << 8,
		AnnotationCreation = Markup | Redact | CreateLink,
		All = ulong.MaxValue
	}

	[Native]
	public enum PSPDFThumbnailBarMode : ulong {
		None,
		ScrubberBar,
		Scrollable,
		FloatingScrubberBar,
	}

	[Native]
	public enum PSPDFScrubberBarType : ulong {
		Horizontal,
		VerticalLeft,
		VerticalRight
	}

	[Native]
	public enum PSPDFThumbnailGrouping : ulong {
		Automatic,
		Never,
		Always
	}

	[Native]
	public enum PSPDFUserInterfaceViewMode : ulong {
		Always,
		Automatic,
		AutomaticNoFirstLastPage,
		Never
	}

	[Native]
	public enum PSPDFUserInterfaceViewAnimation : ulong {
		None,
		Fade,
		Slide
	}

	[Native]
	public enum PSPDFSearchMode : ulong {
		Modal,
		Inline
	}

	[Native]
	public enum PSPDFRenderStatusViewPosition : ulong {
		Top,
		Centered
	}

	[Native]
	public enum PSPDFTapAction : ulong {
		None,
		Zoom,
		SmartZoom
	}

	[Native]
	public enum PSPDFAdaptiveConditional : ulong {
		No,
		Yes,
		Adaptive
	}

	[Native]
	public enum PSPDFPageBookmarkIndicatorMode : ulong {
		Off,
		AlwaysOn,
		OnWhenBookmarked
	}

	[Native]
	public enum PSPDFSoundAnnotationPlayerStyle : ulong {
		Inline,
		Bottom
	}

	[Native]
	public enum PSPDFSignatureSavingStrategy : ulong {
		AlwaysSave,
		NeverSave,
		SaveIfSelected
	}

	[Native]
	public enum PSPDFSignatureCertificateSelectionMode : ulong {
		Always,
		Never,
		IfAvailable
	}

	[Native]
	public enum PSPDFConfigurationSpreadFitting : ulong {
		Fit,
		Fill,
		Adaptive
	}

	[Native]
	public enum PSPDFControllerState : ulong {
		Empty,
		Loading,
		Default,
		Error,
		Locked
	}

	public enum PSPDFDocumentInfoOption {
		[DefaultEnumValue]
		[Field (null)]
		Null,
		[Field ("PSPDFDocumentInfoOptionOutline", PSPDFKitGlobal.LibraryPath)]
		Outline,
		[Field ("PSPDFDocumentInfoOptionBookmarks", PSPDFKitGlobal.LibraryPath)]
		Bookmarks,
		[Field ("PSPDFDocumentInfoOptionAnnotations", PSPDFKitGlobal.LibraryPath)]
		Annotations,
		[Field ("PSPDFDocumentInfoOptionEmbeddedFiles", PSPDFKitGlobal.LibraryPath)]
		EmbeddedFiles,
		[Field ("PSPDFDocumentInfoOptionDocumentInfo", PSPDFKitGlobal.LibraryPath)]
		DocumentInfo,
		[Field ("PSPDFDocumentInfoOptionSecurity", PSPDFKitGlobal.LibraryPath)]
		Security,
	}

	[Native]
	public enum PSPDFDocumentViewLayoutSpreadMode : long {
		Single,
		Double,
		Book
	}

	[Native]
	public enum PSPDFDocumentViewLayoutPageMode : long {
		Single,
		Leading,
		Trailing,
		Center
	}

	[Native]
	[Flags]
	public enum PSPDFDragType : ulong {
		None = 0,
		Text = 1 << 0,
		Image = 1 << 1,
		All = ulong.MaxValue
	}

	[Native]
	[Flags]
	public enum PSPDFDropType : ulong {
		None = 0,
		Text = 1 << 0,
		Image = 1 << 1,
		Pdf = 1 << 2,
		All = ulong.MaxValue
	}

	[Native]
	[Flags]
	public enum PSPDFDropTarget : ulong {
		None = 0,
		ExternalApp = 1 << 0,
		SamePage = 1 << 1,
		OtherPages = 1 << 2,
		All = ulong.MaxValue
	}

	[Native]
	public enum PSPDFDrawViewInputMode : long {
		Draw,
		Erase
	}

	[Flags]
	[Native]
	public enum PSPDFFlexibleToolbarPosition : ulong {
		None = 0,
		InTopBar = 1uL << 0,
		Left = 1uL << 1,
		Right = 1uL << 2,
		Top = 1uL << 3,
		Horizontal = InTopBar | Top,
		Vertical = Left | Right,
		Default = InTopBar | Vertical,
		All = Horizontal | Vertical,
	}

	[Native]
	public enum PSPDFToolbarGroupButtonIndicatorPosition : long {
		None = 0,
		BottomLeft,
		BottomRight
	}

	[Native]
	public enum PSPDFSubmitFormActionFormat : ulong {
		Fdf,
		Xfdf,
		Html,
		Pdf
	}

	[Native]
	public enum PSPDFGalleryContainerViewContentState : ulong {
		Loading,
		Ready,
		Error
	}

	[Native]
	public enum PSPDFGalleryContainerViewPresentationMode : ulong {
		Embedded,
		Fullscreen
	}

	[Native]
	public enum PSPDFGalleryItemContentState : ulong {
		Waiting,
		Loading,
		Ready,
		Error
	}

	[Native]
	public enum PSPDFGalleryVideoItemQuality : ulong {
		Unknown,
		_240p,
		_360p,
		_720p,
		_1080p
	}

	[Native]
	public enum PSPDFGalleryVideoItemCoverMode : ulong {
		None,
		Preview,
		Image,
		Clear
	}

	[Native]
	public enum PSPDFGalleryViewControllerState : ulong {
		Idle,
		Loading,
		Ready,
		Error
	}

		[Flags]
	[Native]
	public enum PSPDFImageQuality : ulong {
		Low = 1uL << 0,
		Medium = 1uL << 1,
		Higher = 1uL << 3,
		Best = 1uL << 2,
		All = ulong.MaxValue,
	}

	[Native]
	public enum PSPDFLabelStyle : ulong {
		Flat,
		Modern
	}

	[Native]
	public enum PSPDFMediaPlayerControlStyle : ulong {
		None,
		Default
	}

	[Native]
	public enum PSPDFMediaPlayerControllerContentState : ulong {
		Idle,
		Loading,
		Ready,
		Error
	}

	[Native]
	public enum PSPDFMediaPlayerCoverMode : ulong {
		Preview,
		Custom,
		Hidden,
		Clear
	}

	[Native]
	public enum PSPDFPrintMode : ulong {
		Interactive,
		ChoosePrinterOnly,
		PrintDirect
	}

	[Native]
	public enum PSPDFResizableViewOuterKnob : ulong {
		Unknown,
		TopLeft,
		TopMiddle,
		TopRight,
		MiddleLeft,
		MiddleRight,
		BottomLeft,
		BottomMiddle,
		BottomRight
	}

	[Native]
	public enum PSPDFKnobType : ulong {
		Outer,
		Inner,
		Rotation,
	}

	[Native]
	public enum PSPDFResizableViewMode : ulong {
		Idle,
		Move,
		Resize,
		Adjust,
		Rotate,
	}

	[Native]
	public enum PSPDFScrollPerSpreadLayoutContentScale : long {
		AspectFit = 0,
		AspectFillWidth,
		AspectFillHeight
	}

	[Native]
	public enum PSPDFSearchStatus : long {
		Idle,
		Active,
		Finished,
		FinishedNoText,
		Cancelled
	}

	[Native]
	public enum PSPDFSearchBarPinning : ulong {
		Auto,
		Top,
		None,
		Hidden
	}

	[Native]
	public enum PSPDFSearchResultScope : long {
		PageRange,
		Document
	}

	[Native]
	public enum PSPDFSelectableCollectionViewCellStyle : ulong {
		None,
		Checkmark,
		Border,
		DimmedBackgroundWithCheckmark,
	}

	[Native]
	[Flags]
	public enum PSPDFSettingsOptions : ulong {
		ScrollDirection = 1 << 0,
		PageTransition = 1 << 1,
		Appearance = 1 << 2,
		Brightness = 1 << 3,
		PageMode = 1 << 4,
		SpreadFitting = 1 << 5,
		Default = ScrollDirection | PageTransition | SpreadFitting | Appearance | Brightness,
		All = ulong.MaxValue
	}

	[Native]
	public enum PSPDFStackViewLayoutDirection : long {
		Horizontal,
		Vertical
	}

	[Native]
	public enum PSPDFStatefulViewState : ulong {
		Loading,
		Empty,
		Finished
	}

	[Native]
	public enum PSPDFStatusHUDStyle : ulong {
		None = 0,
		Clear,
		Black
	}

	[Native]
	public enum PSPDFTabbedViewControllerBarHidingMode : long {
		Automatic,
		Show,
		Hide
	}

	[Native]
	public enum PSPDFTabbedViewControllerCloseMode : long {
		OnlySelectedTab,
		AllTabs,
		SizeDependent,
		Disabled
	}

	[Native]
	public enum PSPDFThumbnailFlowLayoutLineAlignment : long {
		Left,
		Center,
		Right,
		PageBinding
	}

	[Native]
	[Flags]
	public enum PSPDFToolbarButtonControlEvents : ulong {
		Tick = 1 << 24,
		TouchUpInsideIfNotTicking = 1 << 25
	}

	[Native]
	[Flags]
	public enum PSPDFWebViewControllerAvailableActions : ulong {
		None = 0,
		OpenInSafari = 1 << 0,
		MailLink = 1 << 1,
		CopyLink = 1 << 2,
		Print = 1 << 3,
		StopReload = 1 << 4,
		Back = 1 << 5,
		Forward = 1 << 6,
		Facebook = 1 << 7,
		Twitter = 1 << 8,
		Message = 1 << 9,
		OpenInChrome = 1 << 10,
		All = 16777215
	}

	[Native]
	public enum PSPDFSpreadScrollPosition : long {
		Start,
		Middle,
		End,
	}

	[Native]
	//[ErrorDomain ("PSPDFGalleryManifestErrorDomain")] TODO: enable once generator bug is fixed
	public enum PSPDFGalleryManifestErrorCode : long {
		NoDataSourceProvided
	}

	public enum PSPDFThumbnailViewFilter {
		[DefaultEnumValue]
		[Field ("PSPDFThumbnailViewFilterShowAll", PSPDFKitGlobal.LibraryPath)]
		ShowAll,
		[Field ("PSPDFThumbnailViewFilterBookmarks", PSPDFKitGlobal.LibraryPath)]
		Bookmarks,
		[Field ("PSPDFThumbnailViewFilterAnnotations", PSPDFKitGlobal.LibraryPath)]
		Annotations,
	}

	[Native]
	public enum PSPDFMarkupAnnotationMergeBehavior : ulong {
		Never = 0,
		IfColorMatches,
	}

	[Native]
	[Flags]
	public enum PSPDFDocumentEditorInteractiveCapabilities : ulong {
		None = 0,
		ImportPdfs = 1 << 0,
		ImportImages = 1 << 1,
		ExportPages = 1 << 2,
		All = ulong.MaxValue
	}

	[Native]
	public enum PSPDFButtonStyle : ulong {
		Flat,
		Modern,
	}

	[Native]
	public enum PSPDFDocumentSharingStep : ulong {
		Configuration,
		Destination,
		FileGeneration,
	}

	[Flags]
	[Native]
	public enum PSPDFDocumentSharingFileFormatOptions : ulong {
		Pdf = 1uL << 0,
		Original = 1uL << 1,
		Image = 1uL << 2,
	}

	[Flags]
	[Native]
	public enum PSPDFDocumentSharingPagesOptions : ulong {
		Current = 1uL << 0,
		Range = 1uL << 1,
		All = 1uL << 2,
		Annotated = 1uL << 3,
	}

	[Flags]
	[Native]
	public enum PSPDFDocumentSharingAnnotationOptions : ulong {
		Embed = 1uL << 0,
		Flatten = 1uL << 1,
		FlattenForPrint = 1uL << 2,
		Summary = 1uL << 3,
		Remove = 1uL << 4,
	}

	public enum PSPDFDocumentSharingDestination {
		[DefaultEnumValue]
		[Field ("PSPDFDocumentSharingDestinationPrint", PSPDFKitGlobal.LibraryPath)]
		Print,
		[Field ("PSPDFDocumentSharingDestinationExport", PSPDFKitGlobal.LibraryPath)]
		Export,
		[Field ("PSPDFDocumentSharingDestinationActivity", PSPDFKitGlobal.LibraryPath)]
		Activity,
		[Field ("PSPDFDocumentSharingDestinationMessages", PSPDFKitGlobal.LibraryPath)]
		Messages,
		[Field ("PSPDFDocumentSharingDestinationEmail", PSPDFKitGlobal.LibraryPath)]
		Email,
		[Field ("PSPDFDocumentSharingDestinationOtherApplication", PSPDFKitGlobal.LibraryPath)]
		OtherApplication,
	}

	[Native]
	public enum PSPDFAnnotationPlaceholderState : long {
		Idle,
		Progressing,
		Failed,
		Cancelled,
		Completed,
	}

	[Native]
	public enum PSPDFPresentationHalfModalStyle : ulong {
		Card,
		System,
	}

	[Obsolete]
	[Native]
	public enum PSPDFContextMenuOption : long {
		MenuOnly,
		AllowPopovers,
		PopoversOnly,
	}

	[Native]
	public enum PSPDFAutosaveReason : long {
		Disappearing,
		MovingToBackground,
		Terminating,
		ResolvingFileConflict,
	}

	[Native]
	public enum PSPDFColorButtonShape : long {
		RoundedRect = 0,
		Ellipse,
	}

	[Native]
	public enum PSPDFAnnotationTransformationMode : long {
		Move,
		Rotate,
		Resize,
		Adjust,
	}

	[Native]
	public enum PSPDFFastScrollDirection : long {
		Backward = -1,
		Forward = 1,
	}

	[Native]
	public enum PSPDFAnnotationMenuConfigurationColorProperty : ulong {
		Color,
		FillColor,
	}

	[Native]
	public enum PSPDFAnnotationMenuConfigurationLineEndProperty : ulong {
		LineEnd1,
		LineEnd2,
	}

	[Native]
	public enum PSPDFEditMenuAppearance : long {
		HorizontalBar,
		ContextMenu,
	}

	[Native]
	public enum PSPDFMainToolbarMode : long {
		NavigationBar,
		Ornament,
	}
}
