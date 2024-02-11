using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;
using PSPDFKit.Model;
using AVFoundation;
using CoreAnimation;
using CoreMedia;
using MessageUI;
using WebKit;

#if !NET
using NativeHandle = System.IntPtr;
#endif

namespace PSPDFKit.UI {

	[BaseType (typeof (UIActivityViewController))]
	interface PSPDFActivityViewController : IPSPDFOverridable {

	}

	interface IPSPDFAnalyticsClient { }

	[Protocol]
	interface PSPDFAnalyticsClient {

		[Abstract]
		[Export ("logEvent:attributes:")]
		void LogEvent (string @event, [NullAllowed] NSDictionary attributes);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFAnalytics : PSPDFAnalyticsClient {

		[Field ("PSPDFAnalyticsEventPrefix", PSPDFKitGlobal.LibraryPath)]
		NSString AnalyticsEventPrefix { get; }

		[Export ("enabled")]
		bool Enabled { get; set; }

		[Export ("addAnalyticsClient:")]
		void AddAnalyticsClient (IPSPDFAnalyticsClient analyticsClient);

		[Export ("removeAnalyticsClient:")]
		void RemoveAnalyticsClient (IPSPDFAnalyticsClient analyticsClient);

		[Export ("logEvent:")]
		void LogEvent (string @event);
	}

	[Static]
	interface PSPDFAnalyticsEventName {

		[Field ("PSPDFAnalyticsEventNameDocumentLoad", PSPDFKitGlobal.LibraryPath)]
		NSString DocumentLoadKey { get; }

		[Field ("PSPDFAnalyticsEventNameSpreadChange", PSPDFKitGlobal.LibraryPath)]
		NSString SpreadChangeKey { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationCreationModeEnter", PSPDFKitGlobal.LibraryPath)]
		NSString AnnotationCreationModeEnterKey { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationCreationModeExit", PSPDFKitGlobal.LibraryPath)]
		NSString AnnotationCreationModeExitKey { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationCreatorDialogShow", PSPDFKitGlobal.LibraryPath)]
		NSString AnnotationCreatorDialogShowKey { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationCreatorDialogCancel", PSPDFKitGlobal.LibraryPath)]
		NSString AnnotationCreatorDialogCancelKey { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationCreatorSet", PSPDFKitGlobal.LibraryPath)]
		NSString AnnotationCreatorSetKey { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationSelect", PSPDFKitGlobal.LibraryPath)]
		NSString AnnotationSelectKey { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationCreate", PSPDFKitGlobal.LibraryPath)]
		NSString AnnotationCreateKey { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationDelete", PSPDFKitGlobal.LibraryPath)]
		NSString AnnotationDeleteKey { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationInspectorShow", PSPDFKitGlobal.LibraryPath)]
		NSString AnnotationInspectorShowKey { get; }

		[Field ("PSPDFAnalyticsEventNameTextSelect", PSPDFKitGlobal.LibraryPath)]
		NSString TextSelectKey { get; }

		[Field ("PSPDFAnalyticsEventNameOutlineOpen", PSPDFKitGlobal.LibraryPath)]
		NSString OutlineOpenKey { get; }

		[Field ("PSPDFAnalyticsEventNameOutlineElementSelect", PSPDFKitGlobal.LibraryPath)]
		NSString OutlineElementSelectKey { get; }

		[Field ("PSPDFAnalyticsEventNameOutlineAnnotationSelect", PSPDFKitGlobal.LibraryPath)]
		NSString OutlineAnnotationSelectKey { get; }

		[Field ("PSPDFAnalyticsEventNameThumbnailGridOpen", PSPDFKitGlobal.LibraryPath)]
		NSString ThumbnailGridOpenKey { get; }

		[Field ("PSPDFAnalyticsEventNameDocumentEditorOpen", PSPDFKitGlobal.LibraryPath)]
		NSString DocumentEditorOpenKey { get; }

		[Field ("PSPDFAnalyticsEventNameDocumentEditorAction", PSPDFKitGlobal.LibraryPath)]
		NSString DocumentEditorActionKey { get; }

		[Field ("PSPDFAnalyticsEventNameBookmarkAdd", PSPDFKitGlobal.LibraryPath)]
		NSString BookmarkAddKey { get; }

		[Field ("PSPDFAnalyticsEventNameBookmarkEdit", PSPDFKitGlobal.LibraryPath)]
		NSString BookmarkEditKey { get; }

		[Field ("PSPDFAnalyticsEventNameBookmarkRemove", PSPDFKitGlobal.LibraryPath)]
		NSString BookmarkRemoveKey { get; }

		[Field ("PSPDFAnalyticsEventNameBookmarkSort", PSPDFKitGlobal.LibraryPath)]
		NSString BookmarkSortKey { get; }

		[Field ("PSPDFAnalyticsEventNameBookmarkRename", PSPDFKitGlobal.LibraryPath)]
		NSString BookmarkRenameKey { get; }

		[Field ("PSPDFAnalyticsEventNameBookmarkSelect", PSPDFKitGlobal.LibraryPath)]
		NSString BookmarkSelectKey { get; }

		[Field ("PSPDFAnalyticsEventNameSearchStart", PSPDFKitGlobal.LibraryPath)]
		NSString SearchStartKey { get; }

		[Field ("PSPDFAnalyticsEventNameSearchResultSelect", PSPDFKitGlobal.LibraryPath)]
		NSString SearchResultSelectKey { get; }

		[Field ("PSPDFAnalyticsEventNameShare", PSPDFKitGlobal.LibraryPath)]
		NSString ShareKey { get; }

		[Field ("PSPDFAnalyticsEventNameToolbarMove", PSPDFKitGlobal.LibraryPath)]
		NSString ToolbarMoveKey { get; }

		[Field ("PSPDFAnalyticsEventNameReaderViewOpen", PSPDFKitGlobal.LibraryPath)]
		NSString ReaderViewOpenKey { get; }
	}

	[Static]
	interface PSPDFAnalyticsEventAttributeName {

		[Field ("PSPDFAnalyticsEventAttributeNameDocumentType", PSPDFKitGlobal.LibraryPath)]
		NSString DocumentTypeKey { get; }

		[Field ("PSPDFAnalyticsEventAttributeNameAnnotationType", PSPDFKitGlobal.LibraryPath)]
		NSString AnnotationTypeKey { get; }

		[Field ("PSPDFAnalyticsEventAttributeNameAction", PSPDFKitGlobal.LibraryPath)]
		NSString ActionKey { get; }

		[Field ("PSPDFAnalyticsEventAttributeNameActivityType", PSPDFKitGlobal.LibraryPath)]
		NSString ActivityTypeKey { get; }
	}

	[Static]
	interface PSPDFAnalyticsEventAttributeValueDocumentTypeStandard {

		[Field ("PSPDFAnalyticsEventAttributeValueDocumentTypeStandard", PSPDFKitGlobal.LibraryPath)]
		NSString StandardKey { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueDocumentTypeImage", PSPDFKitGlobal.LibraryPath)]
		NSString ImageKey { get; }
	}

	[Static]
	interface PSPDFAnalyticsEventAttributeValue {

		[Field ("PSPDFAnalyticsEventAttributeValueActionInsertNewPage", PSPDFKitGlobal.LibraryPath)]
		NSString ActionInsertNewPageKey { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueActionRemoveSelectedPages", PSPDFKitGlobal.LibraryPath)]
		NSString ActionRemoveSelectedPagesKey { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueActionDuplicateSelectedPages", PSPDFKitGlobal.LibraryPath)]
		NSString ActionDuplicateSelectedPagesKey { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueActionRotateSelectedPages", PSPDFKitGlobal.LibraryPath)]
		NSString ActionRotateSelectedPagesKey { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueActionExportSelectedPages", PSPDFKitGlobal.LibraryPath)]
		NSString ActionExportSelectedPagesKey { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueActionSelectAllPages", PSPDFKitGlobal.LibraryPath)]
		NSString ActionSelectAllPagesKey { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueActionUndo", PSPDFKitGlobal.LibraryPath)]
		NSString ActionUndoKey { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueActionRedo", PSPDFKitGlobal.LibraryPath)]
		NSString ActionRedoKey { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueToolbarPosition", PSPDFKitGlobal.LibraryPath)]
		NSString ToolbarPositionKey { get; }

		[Field ("PSPDFAnalyticsEventAttributeValuePreviousSpreadIndex", PSPDFKitGlobal.LibraryPath)]
		NSString PreviousSpreadIndexKey { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueNewSpreadIndex", PSPDFKitGlobal.LibraryPath)]
		NSString NewSpreadIndexKey { get; }
	}

	[BaseType (typeof (PSPDFNonAnimatingTableViewCell))]
	interface PSPDFAnnotationCell : IPSPDFOverridable {

		[Static]
		[Export ("heightForAnnotation:inTableView:")]
		nfloat GetHeight (PSPDFAnnotation annotation, UITableView tableView);

		[Export ("nameLabel")]
		UILabel NameLabel { get; }

		[Export ("dateAndUserLabel")]
		UILabel DateAndUserLabel { set; }

		[Export ("disabledStyle")]
		bool DisabledStyle { [Bind ("isDisabledStyle")] get; set; }

		[NullAllowed, Export ("annotation")]
		PSPDFAnnotation Annotation { get; set; }

		// PSPDFAnnotationCell (SubclassingHooks) Category

		[Static]
		[Export ("dateAndUserStringForAnnotation:")]
		string GetDateAndUserString (PSPDFAnnotation annotation);
	}

	interface IPSPDFAnnotationGridViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationGridViewControllerDelegate {

		[Export ("annotationGridViewControllerDidCancel:")]
		void DidCancel (PSPDFAnnotationGridViewController annotationGridController);

		[Export ("annotationGridViewController:didSelectAnnotationSet:")]
		void DidSelectAnnotationSet (PSPDFAnnotationGridViewController annotationGridController, PSPDFAnnotationSet annotationSet);
	}

	interface IPSPDFAnnotationGridViewControllerDataSource { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationGridViewControllerDataSource {

		[Abstract]
		[Export ("numberOfSectionsInAnnotationGridViewController:")]
		nint GetNumberOfSectionsInAnnotationGridViewController (PSPDFAnnotationGridViewController annotationGridController);

		[Abstract]
		[Export ("annotationGridViewController:numberOfAnnotationsInSection:")]
		nint GetNumberOfAnnotationsInSection (PSPDFAnnotationGridViewController annotationGridController, nint section);

		[Abstract]
		[Export ("annotationGridViewController:annotationSetForIndexPath:")]
		PSPDFAnnotationSet GetAnnotationSetForIndexPath (PSPDFAnnotationGridViewController annotationGridController, NSIndexPath indexPath);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFAnnotationGridViewController : PSPDFStyleable, IUICollectionViewDelegate, IUICollectionViewDataSource {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFAnnotationGridViewControllerDelegate Delegate { get; set; }

		[NullAllowed, Export ("dataSource", ArgumentSemantic.Weak)]
		IPSPDFAnnotationGridViewControllerDataSource DataSource { get; set; }

		[Export ("reloadData")]
		void ReloadData ();

		// PSPDFAnnotationGridViewController (SubclassingHooks) category

		[Export ("close:")]
		void Close ([NullAllowed] NSObject sender);

		[Export ("configureCell:forIndexPath:")]
		void ConfigureCell (PSPDFAnnotationSetCell annotationSetCell, NSIndexPath indexPath);

		[NullAllowed, Export ("collectionView")]
		UICollectionView GetCollectionView { get; }

		[Export ("updatePopoverSize")]
		void UpdatePopoverSize ();
	}

#if !NET
	[Category (true)]
	[BaseType (typeof (PSPDFAnnotationGroupItem))]
	interface PSPDFAnnotationGroupItem_PSPDFPresets {

		[Static]
		[Export ("inkConfigurationBlock")]
		PSPDFAnnotationGroupItemConfigurationHandler GetInkConfigurationHandler ();

		[Static]
		[Export ("lineConfigurationBlock")]
		PSPDFAnnotationGroupItemConfigurationHandler GetLineConfigurationHandler ();

		[Static]
		[Export ("freeTextConfigurationBlock")]
		PSPDFAnnotationGroupItemConfigurationHandler GetFreeTextConfigurationHandler ();

		[Static]
		[Export ("polygonConfigurationBlock")]
		PSPDFAnnotationGroupItemConfigurationHandler GetPolygonConfigurationHandler ();

		[Static]
		[Export ("measurementConfigurationBlock")]
		PSPDFAnnotationGroupItemConfigurationHandler GetMeasurementConfigurationHandler ();
	}
#endif

	interface IPSPDFAnnotationPresenting { }

	[Protocol]
	interface PSPDFAnnotationPresenting {

		[return: NullAllowed]
		[Export ("annotation")]
		PSPDFAnnotation GetAnnotation ();

		[Export ("setAnnotation:")]
		void SetAnnotation ([NullAllowed] PSPDFAnnotation annotation);

		[Export ("zIndex")]
		nuint GetZIndex ();

		[Export ("setZIndex:")]
		void SetZIndex (nuint index);

		[Export ("zoomScale")]
		nfloat GetZoomScale ();

		[Export ("setZoomScale:")]
		void SetZoomScale (nfloat zoomScale);

		[Export ("PDFScale")]
		nfloat GetPdfScale ();

		[Export ("setPDFScale:")]
		void SetPdfScale (nfloat pdfScale);

		[Export ("willShowPageView:")]
		void WillShowPageView (PSPDFPageView pageView);

		[Export ("didHidePageView:")]
		void DidHidePageView (PSPDFPageView pageView);

		[Export ("didChangePageBounds:")]
		void DidChangePageBounds (CGRect bounds);

		[Export ("didTapAtPoint:")]
		void DidTapAtPoint (CGPoint point);

		[Export ("shouldSyncRemovalFromSuperview")]
		bool GetShouldSyncRemovalFromSuperview ();

		[Export ("willRemoveFromSuperview")]
		void WillRemoveFromSuperview ();

		[Export ("pageView")]
		PSPDFPageView GetPageView ();

		[Export ("setPageView:")]
		void SetPageView ([NullAllowed] PSPDFPageView PageView);

		[Export ("configuration")]
		PSPDFConfiguration GetConfiguration ();

		[Export ("setConfiguration:")]
		void SetConfiguration (PSPDFConfiguration configuration);

		[Export ("isSelected")]
		bool GetSelected ();

		[Export ("setSelected:")]
		void SetSelected (bool selected);

		[Export ("strokeWidth")]
		nfloat GetStrokeWidth ();

		[Export ("setStrokeWidth:")]
		void SetStrokeWidth (nfloat strokeWidth);

		[Export ("borderColor")]
		UIColor GetBorderColor ();

		[Export ("setBorderColor:")]
		void SetBorderColor ([NullAllowed] UIColor borderColor);

		[Export ("annotationPlaceholder")]
		PSPDFAnnotationPlaceholder GetAnnotationPlaceholder ();

		[Export ("setAnnotationPlaceholder:")]
		void SetAnnotationPlaceholder ([NullAllowed] PSPDFAnnotationPlaceholder annotationPlaceholder);
	}

	[BaseType (typeof (PSPDFSelectableCollectionViewCell))]
	interface PSPDFAnnotationSetCell : IPSPDFOverridable {

		[NullAllowed, Export ("annotationSet", ArgumentSemantic.Strong)]
		PSPDFAnnotationSet AnnotationSet { get; set; }

		[Export ("edgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets EdgeInsets { get; set; }
	}

	[BaseType (typeof (PSPDFTableViewCell))]
	interface PSPDFAnnotationSetsCell : IUICollectionViewDelegate, IUICollectionViewDataSource {

		[Advice ("Allows 'PSPDFAnnotation' or 'PSPDFAnnotationSet' objects.")]
		[NullAllowed, Export ("annotations", ArgumentSemantic.Copy)]
		NSObject [] Annotations { get; set; }

		[Export ("collectionView")]
		UICollectionView CollectionView { get; }

		[Export ("border")]
		nfloat Border { get; set; }

		[Export ("edgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets EdgeInsets { get; set; }

		[NullAllowed, Export ("collectionViewUpdateBlock", ArgumentSemantic.Copy)]
		Action<PSPDFAnnotationSetsCell> CollectionViewUpdateHandler { get; set; }
	}

	interface IPSPDFAnnotationStateManagerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationStateManagerDelegate {

		[Export ("annotationStateManager:shouldChangeState:to:variant:to:")]
		bool ShouldChangeState (PSPDFAnnotationStateManager manager, [NullAllowed] NSString currentState, [NullAllowed] NSString proposedState, [NullAllowed] NSString currentVariant, [NullAllowed] NSString proposedVariant);

		[Export ("annotationStateManager:didChangeState:to:variant:to:")]
		void DidChangeState (PSPDFAnnotationStateManager manager, [NullAllowed] NSString oldState, [NullAllowed] NSString newState, [NullAllowed] NSString oldVariant, [NullAllowed] NSString newVariant);

		[Export ("annotationStateManager:didChangeUndoState:redoState:")]
		void DidChangeUndoState (PSPDFAnnotationStateManager manager, bool undoEnabled, bool redoEnabled);

		[Export ("annotationStateManagerDidRequestShowingColorPalette:")]
		void DidRequestShowingColorPalette (PSPDFAnnotationStateManager manager);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationStateManager : IPSPDFOverridable, IUIPencilInteractionDelegate {

		[NullAllowed, Export ("pdfController", ArgumentSemantic.Weak)]
		PSPDFViewController PdfController { get; }

		[Export ("addDelegate:")]
		void AddDelegate (IPSPDFAnnotationStateManagerDelegate @delegate);

		[Export ("removeDelegate:")]
		bool RemoveDelegate (IPSPDFAnnotationStateManagerDelegate @delegate);

		[BindAs (typeof (PSPDFAnnotationStringUI))]
		[NullAllowed, Export ("state")]
		NSString State { get; set; }

		[Export ("toggleState:")]
		void ToggleState ([BindAs (typeof (PSPDFAnnotationStringUI))] NSString state);

		[BindAs (typeof (PSPDFAnnotationVariantString))]
		[NullAllowed, Export ("variant")]
		NSString Variant { get; set; }

		[Export ("setState:variant:")]
		void SetState ([NullAllowed] [BindAs (typeof (PSPDFAnnotationStringUI))] NSString state, [NullAllowed] [BindAs (typeof (PSPDFAnnotationVariantString))] NSString variant);

		[Export ("toggleState:variant:")]
		void ToggleState ([NullAllowed] [BindAs (typeof (PSPDFAnnotationStringUI))] NSString state, [NullAllowed] [BindAs (typeof (PSPDFAnnotationVariantString))] NSString variant);

		[Advice ("You can use 'GetStateVariantId' for a more strongly typed access.")]
		[Export ("stateVariantID")]
		NSString StateVariantId { get; }

		[Wrap ("PSPDFKit.Model.PSPDFAnnotationStateVariantIdExtensions.GetValue (StateVariantId)")]
		PSPDFAnnotationStateVariantId GetStateVariantId ();

		[Export ("stylusMode", ArgumentSemantic.Assign)]
		PSPDFAnnotationStateManagerStylusMode StylusMode { get; set; }

		[NullAllowed, Export ("drawColor", ArgumentSemantic.Strong)]
		UIColor DrawColor { get; set; }

		[NullAllowed, Export ("fillColor", ArgumentSemantic.Strong)]
		UIColor FillColor { get; set; }

		[Export ("lineWidth")]
		nfloat LineWidth { get; set; }

		[Export ("lineEnd1", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd1 { get; set; }

		[Export ("lineEnd2", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd2 { get; set; }

		[NullAllowed, Export ("dashArray", ArgumentSemantic.Copy)]
		NSNumber [] DashArray { get; set; }

		[Export ("borderEffect", ArgumentSemantic.Assign)]
		PSPDFAnnotationBorderEffect BorderEffect { get; set; }

		[Export ("borderEffectIntensity")]
		nfloat BorderEffectIntensity { get; set; }

		[Export ("blendMode")]
		CGBlendMode BlendMode { get; set; }

		[NullAllowed, Export ("fontName")]
		string FontName { get; set; }

		[Export ("fontSize")]
		nfloat FontSize { get; set; }

		[Export ("textAlignment", ArgumentSemantic.Assign)]
		UITextAlignment TextAlignment { get; set; }

		[NullAllowed, Export ("outlineColor", ArgumentSemantic.Assign)]
		UIColor OutlineColor { get; set; }

		[NullAllowed, Export ("overlayText")]
		string OverlayText { get; set; }

		[Export ("repeatOverlayText")]
		bool RepeatOverlayText { get; set; }

		[Export ("pencilInteraction")]
		UIPencilInteraction PencilInteraction { get; }

		[Export ("toggleStylePicker:presentationOptions:")]
		[return: NullAllowed]
		PSPDFAnnotationStyleViewController ToggleStylePicker ([NullAllowed] NSObject sender, [NullAllowed] NSDictionary options);

		[Wrap ("ToggleStylePicker (sender, options: presentationOptions?.Dictionary)")]
		PSPDFAnnotationStyleViewController ToggleStylePicker ([NullAllowed] NSObject sender, PSPDFPresentationOptions presentationOptions);

		[Export ("toggleSignatureController:presentationOptions:")]
		[return: NullAllowed]
		UIViewController ToggleSignatureController ([NullAllowed] NSObject sender, [NullAllowed] NSDictionary options);

		[Wrap ("ToggleSignatureController (sender, options: presentationOptions?.Dictionary)")]
		UIViewController ToggleSignatureController ([NullAllowed] NSObject sender, PSPDFPresentationOptions presentationOptions);

		[Export ("toggleStampController:presentationOptions:")]
		[return: NullAllowed]
		UIViewController ToggleStampController ([NullAllowed] NSObject sender, [NullAllowed] NSDictionary options);

		[Wrap ("ToggleStampController (sender, options: presentationOptions?.Dictionary)")]
		UIViewController ToggleStampController ([NullAllowed] NSObject sender, PSPDFPresentationOptions presentationOptions);

		[Export ("toggleSavedAnnotations:presentationOptions:")]
		[return: NullAllowed]
		UIViewController ToggleSavedAnnotations ([NullAllowed] NSObject sender, [NullAllowed] NSDictionary options);

		[Wrap ("ToggleSavedAnnotations (sender, options: presentationOptions?.Dictionary)")]
		UIViewController ToggleSavedAnnotations ([NullAllowed] NSObject sender, PSPDFPresentationOptions presentationOptions);

		[Export ("toggleImagePickerController:presentationOptions:")]
		[return: NullAllowed]
		UIViewController ToggleImagePickerController ([NullAllowed] NSObject sender, [NullAllowed] NSDictionary options);

		[Wrap ("ToggleImagePickerController (sender, options: presentationOptions?.Dictionary)")]
		UIViewController ToggleImagePickerController ([NullAllowed] NSObject sender, PSPDFPresentationOptions presentationOptions);

		// PSPDFAnnotationStateManager (StateHelper) category

		[Export ("isDrawingState:")]
		bool IsDrawingState ([NullAllowed] NSString state);

		[Wrap ("IsDrawingState (state.GetConstant ())")]
		void IsDrawingState (PSPDFAnnotationStringUI state);

		[Export ("isMarkupAnnotationState:")]
		bool IsMarkupAnnotationState ([NullAllowed] NSString state);

		[Wrap ("IsMarkupAnnotationState (state.GetConstant ())")]
		void IsMarkupAnnotationState (PSPDFAnnotationStringUI state);

		[Export ("stateShowsStylePicker:")]
		bool StateShowsStylePicker ([NullAllowed] NSString state);

		[Wrap ("StateShowsStylePicker (state.GetConstant ())")]
		void StateShowsStylePicker (PSPDFAnnotationStringUI state);

		// PSPDFAnnotationStateManager (SubclassingHooks) category

		[Export ("setLastUsedColor:annotationString:")]
		void SetLastUsedColor ([NullAllowed] UIColor lastUsedDrawColor, string annotationString);

		[Export ("lastUsedColorForAnnotationString:")]
		[return: NullAllowed]
		UIColor GetLastUsedColor (NSString annotationString);

		[Wrap ("GetLastUsedColor (annotationString.GetConstant ())")]
		UIColor GetLastUsedColor (PSPDFAnnotationStringUI annotationString);

		[Export ("drawViews")]
		NSDictionary<NSNumber, PSPDFDrawView> DrawViews { get; }
	}

	[Static]
	interface PSPDFPresentationKeys {

		[Field ("PSPDFPresentationOptionPresentationStyle", PSPDFKitGlobal.LibraryPath)]
		NSString StyleKey { get; }

		[Field ("PSPDFPresentationOptionHalfModalStyle", PSPDFKitGlobal.LibraryPath)]
		NSString HalfModalStyleKey { get; }

		[Field ("PSPDFPresentationOptionNonAdaptive", PSPDFKitGlobal.LibraryPath)]
		NSString NonAdaptiveKey { get; }

		[Field ("PSPDFPresentationOptionSourceRectProvider", PSPDFKitGlobal.LibraryPath)]
		NSString RectBlockKey { get; }

		[Field ("PSPDFPresentationOptionContentSize", PSPDFKitGlobal.LibraryPath)]
		NSString ContentSizeKey { get; }

		[Field ("PSPDFPresentationOptionInNavigationController", PSPDFKitGlobal.LibraryPath)]
		NSString InNavigationControllerKey { get; }

		[Field ("PSPDFPresentationOptionCloseButton", PSPDFKitGlobal.LibraryPath)]
		NSString CloseButtonKey { get; }

		[Field ("PSPDFPresentationOptionReuseNavigationController", PSPDFKitGlobal.LibraryPath)]
		NSString ReuseNavigationControllerKey { get; }

		[Field ("PSPDFPresentationOptionPopoverArrowDirections", PSPDFKitGlobal.LibraryPath)]
		NSString PopoverArrowDirectionsKey { get; }

		[Field ("PSPDFPresentationOptionPopoverPassthroughViews", PSPDFKitGlobal.LibraryPath)]
		NSString PopoverPassthroughViewsKey { get; }

		[Field ("PSPDFPresentationOptionPopoverBackgroundColor", PSPDFKitGlobal.LibraryPath)]
		NSString PopoverBackgroundColorKey { get; }

		[Field ("PSPDFPresentationOptionSourceRect", PSPDFKitGlobal.LibraryPath)]
		NSString RectKey { get; }
	}

	[StrongDictionary ("PSPDFPresentationKeys")]
	interface PSPDFPresentationOptions {
		bool NonAdaptive { get; set; }
		CGSize ContentSize { get; set; }
		bool InNavigationController { get; set; }
		bool CloseButton { get; set; }
		bool ReuseNavigationController { get; set; }
		UIView [] PopoverPassthroughViews { get; set; }
		UIColor PopoverBackgroundColor { get; set; }
		CGRect Rect { get; set; }
	}

	interface IPSPDFPresentationActions { }

	[Protocol]
	interface PSPDFPresentationActions {

		[Abstract]
		[Export ("presentViewController:options:animated:sender:completion:")]
		bool PresentViewController (UIViewController viewController, [NullAllowed] NSDictionary options, bool animated, [NullAllowed] NSObject sender, [NullAllowed] Action completion);

		[Abstract]
		[Export ("dismissViewControllerOfClass:animated:completion:")]
		bool DismissViewController ([NullAllowed] Class controllerClass, bool animated, [NullAllowed] Action completion);
	}

	interface IPSPDFAnnotationStyleViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationStyleViewControllerDelegate {

		[Abstract]
		[Export ("annotationStyleController:didChangeProperties:")]
		void DidChangeProperties (PSPDFAnnotationStyleViewController styleController, string [] propertyNames);

		[Export ("annotationStyleController:didBeginChangingProperty:")]
		void DidBeginChangingProperty (PSPDFAnnotationStyleViewController styleController, string property);

		[Export ("annotationStyleController:didEndChangingProperty:affectedProperties:")]
		void DidEndChangingProperty (PSPDFAnnotationStyleViewController styleController, string property, string[] affectedProperties);

		[Export ("annotationStyleController:annotationViewForAnnotation:")]
		[return: NullAllowed]
		IPSPDFAnnotationPresenting GetAnnotationView (PSPDFAnnotationStyleViewController styleController, PSPDFAnnotation annotation);

		[Export ("annotationStyleController:executeZIndexMove:")]
		void ExecuteZIndexMove (PSPDFAnnotationStyleViewController styleController, PSPDFAnnotationZIndexMove zIndexMove);

		[Export ("annotationStyleController:canExecuteZIndexMove:")]
		bool CanExecuteZIndexMove (PSPDFAnnotationStyleViewController styleController, PSPDFAnnotationZIndexMove zIndexMove);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationStyleViewController : PSPDFFontPickerViewControllerDelegate, PSPDFStyleable, IPSPDFOverridable {

		[Export ("initWithAnnotations:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] PSPDFAnnotation [] annotations);

		[NullAllowed, Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFAnnotationStyleViewControllerDelegate Delegate { get; set; }

		[Export ("showPreviewArea")]
		bool ShowPreviewArea { get; set; }

		[Export ("propertiesForAnnotations", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> PropertiesForAnnotations { get; set; }

		[Export ("typesShowingColorPresets", ArgumentSemantic.Assign)]
		PSPDFAnnotationType TypesShowingColorPresets { get; set; }

		[Export ("persistsColorPresetChanges")]
		bool PersistsColorPresetChanges { get; set; }

		[Export ("allowAnnotationZIndexMoves")]
		bool AllowAnnotationZIndexMoves { get; set; }

		// PSPDFAnnotationStyleViewController (SubclassingHooks) category

		[Export ("propertiesForAnnotations:")]
		NSArray<NSString> [] GetProperties (PSPDFAnnotation [] annotations);
	}

	interface IPSPDFAnnotationTableViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationTableViewControllerDelegate {

		[Abstract]
		[Export ("annotationTableViewController:didSelectAnnotation:")]
		void DidSelectAnnotation (PSPDFAnnotationTableViewController annotationController, PSPDFAnnotation annotation);
	}

	[BaseType (typeof (PSPDFSearchableTableViewController))]
	interface PSPDFAnnotationTableViewController : PSPDFDocumentInfoController, PSPDFSegmentImageProviding, PSPDFStyleable, IPSPDFOverridable {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFAnnotationTableViewControllerDelegate Delegate { get; set; }

		[Advice ("You can use 'VisibleAnnotationTypes' for a more strongly typed access.")]
		[NullAllowed, Export ("visibleAnnotationTypes", ArgumentSemantic.Copy)]
		NSSet<NSString> WeakVisibleAnnotationTypes { get; set; }

		[Advice ("You can use 'EditableAnnotationTypes' for a more strongly typed access.")]
		[NullAllowed, Export ("editableAnnotationTypes", ArgumentSemantic.Copy)]
		NSSet<NSString> WeakEditableAnnotationTypes { get; set; }

		[Export ("allowCopy")]
		bool AllowCopy { get; set; }

		[Export ("allowEditing")]
		bool AllowEditing { get; set; }

		[Export ("rightActionButtonItems")]
		UIBarButtonItem [] RightActionButtonItems { get; set; }

		[Export ("leftActionButtonItems")]
		UIBarButtonItem [] leftActionButtonItems { get; set; }

		[Export ("showDeleteAllOption")]
		bool ShowDeleteAllOption { get; set; }

		[Export ("allowAnnotationZIndexMoves")]
		bool AllowAnnotationZIndexMoves { get; set; }

		[Export ("reloadData")]
		void ReloadData ();

		// PSPDFAnnotationTableViewController (SubclassingHooks) Category

		[Export ("annotationsForPageAtIndex:")]
		PSPDFAnnotation [] GetAnnotations (nuint pageIndex);

		[Export ("annotationForIndexPath:inTableView:")]
		[return: NullAllowed]
		PSPDFAnnotation GetAnnotation (NSIndexPath indexPath, UITableView tableView);

		[Export ("deleteAllAction:")]
		void DeleteAllAction (NSObject sender);

		[Export ("clearOrDeleteAllAnnotations")]
		void ClearOrDeleteAllAnnotations ();

		[NullAllowed, Export ("viewForTableViewFooter")]
		UIView ViewForTableViewFooter { get; }

		[Export ("updateBarButtonItems")]
		void UpdateBarButtonItems ();
	}

	[BaseType (typeof (PSPDFFlexibleToolbar))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationToolbar : PSPDFAnnotationStateManagerDelegate, IPSPDFOverridable {

		[Export ("initWithAnnotationStateManager:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFAnnotationStateManager annotationStateManager);

		[Export ("annotationStateManager", ArgumentSemantic.Strong)]
		PSPDFAnnotationStateManager AnnotationStateManager { get; set; }

		[Advice ("You can use 'EditableAnnotationTypes' for a more strongly typed access.")]
		[NullAllowed, Export ("editableAnnotationTypes", ArgumentSemantic.Copy)]
		NSSet<NSString> WeakEditableAnnotationTypes { get; set; }

		[NullAllowed, Export ("configurations", ArgumentSemantic.Copy)]
		PSPDFAnnotationToolbarConfiguration [] Configurations { get; set; }

		[Export ("annotationGroups")]
		PSPDFAnnotationGroup [] AnnotationGroups { get; }

		[return: NullAllowed]
		[Export ("buttonWithType:variant:createFromGroup:")]
		UIButton GetButton ([BindAs (typeof (PSPDFAnnotationStringUI))] NSString type, [NullAllowed] [BindAs (typeof (PSPDFAnnotationVariantString))] NSString variant, bool createFromGroup);

		[NullAllowed, Export ("additionalButtons", ArgumentSemantic.Copy)]
		UIButton [] AdditionalButtons { get; set; }

		[Export ("collapseUndoButtonsForCompactSizes")]
		bool CollapseUndoButtonsForCompactSizes { get; set; }

		[Export ("showsApplePencilButtonAutomatically")]
		bool ShowsApplePencilButtonAutomatically { get; set; }

		[Export ("showingApplePencilButton")]
		bool ShowingApplePencilButton { [Bind ("isShowingApplePencilButton")] get; set; }

		[Export ("setShowingApplePencilButton:animated:")]
		void SetShowingApplePencilButton (bool showingApplePencilButton, bool animated);

		[Export ("saveAfterToolbarHiding")]
		bool SaveAfterToolbarHiding { get; set; }

		// PSPDFAnnotationToolbar (SubclassingHooks) Category

		[NullAllowed, Export ("doneButton")]
		UIButton DoneButton { get; }

		[NullAllowed, Export ("applePencilButton")]
		PSPDFToolbarButton ApplePencilButton { get; }

		[NullAllowed, Export ("undoButton")]
		UIButton UndoButton { get; }

		[NullAllowed, Export ("redoButton")]
		UIButton RedoButton { get; }

		[NullAllowed, Export ("undoRedoButton")]
		PSPDFToolbarDualButton UndoRedoButton { get; }

		[NullAllowed, Export ("strokeColorButton")]
		PSPDFColorButton StrokeColorButton { get; }

		[Export ("done:")]
		void Done ([NullAllowed] NSObject sender);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFlexibleToolbarController : PSPDFFlexibleToolbarContainerDelegate {

		[Export ("initWithToolbar:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFFlexibleToolbar toolbar);

		[Export ("toolbar")]
		PSPDFFlexibleToolbar Toolbar { get; }

		[NullAllowed, Export ("flexibleToolbarContainer")]
		PSPDFFlexibleToolbarContainer FlexibleToolbarContainer { get; }

		[Export ("toolbarVisible")]
		bool ToolbarVisible { [Bind ("isToolbarVisible")] get; }

		[Export ("toggleToolbarAnimated:completion:")]
		void ToggleToolbar (bool animated, [NullAllowed] Action<bool> completion);

		[Export ("showToolbarAnimated:completion:")]
		bool ShowToolbar (bool animated, [NullAllowed] Action<bool> completion);

		[Export ("hideToolbarAnimated:completion:")]
		bool HideToolbar (bool animated, [NullAllowed] Action<bool> completion);

		[Export ("updateHostView:container:viewController:")]
		void UpdateHostView ([NullAllowed] UIView hostView, [NullAllowed] NSObject container, [NullAllowed] UIViewController viewController);

		[NullAllowed, Export ("hostView")]
		UIView HostView { get; }

		[NullAllowed, Export ("hostToolbar")]
		IPSPDFSystemBar HostToolbar { get; }

		[NullAllowed, Export ("hostViewController", ArgumentSemantic.Weak)]
		UIViewController HostViewController { get; }
	}

	interface PSPDFAnnotationToolbarControllerVisibilityDidChangeNotificationEventArgs {

		[Export ("PSPDFAnnotationToolbarControllerVisibilityAnimatedKey")]
		bool VisibilityAnimated { get; set; }
	}

	[BaseType (typeof (PSPDFFlexibleToolbarController))]
	interface PSPDFAnnotationToolbarController : IPSPDFOverridable {

		[Field ("PSPDFAnnotationToolbarControllerVisibilityDidChangeNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFAnnotationToolbarControllerVisibilityDidChangeNotificationEventArgs))]
		NSString VisibilityDidChangeNotification { get; }

		[Export ("initWithAnnotationToolbar:")]
		NativeHandle Constructor (PSPDFAnnotationToolbar annotationToolbar);

		[Export ("annotationToolbar")]
		PSPDFAnnotationToolbar AnnotationToolbar { get; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFFlexibleToolbarContainerDelegate Delegate { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFAnnotationView : PSPDFAnnotationPresenting {

		// Inlined from PSPDFAnnotationPresenting
		//[NullAllowed, Export ("annotation", ArgumentSemantic.Strong)]
		//PSPDFAnnotation Annotation { get; set; }

		//[NullAllowed, Export ("pageView", ArgumentSemantic.Weak)]
		//PSPDFPageView PageView { get; set; }

		//[Export ("zoomScale")]
		//nfloat ZoomScale { get; set; }

		// PSPDFAnnotationView (SubclassingHooks) Category

		[Export ("annotationChangedNotification:")]
		[Advice ("Requires base call if override.")]
		void AnnotationChangedNotification (NSNotification notification);

		[Export ("shouldAnimatedAnnotationChanges")]
		bool ShouldAnimatedAnnotationChanges { get; set; }

		[Export ("updateFrame")]
		[Advice ("Requires base call if override.")]
		void UpdateFrame ();
	}

	interface IPSPDFAppearanceModeManagerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFAppearanceModeManagerDelegate {

		[Export ("appearanceManager:renderOptionsForMode:")]
		PSPDFRenderOptions GetRenderOptions (PSPDFAppearanceModeManager manager, PSPDFAppearanceMode mode);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFAppearanceModeManager : IPSPDFOverridable {

		[Field ("PSPDFAppearanceModeChangedNotification", PSPDFKitGlobal.LibraryPath)]
		NSString AppearanceModeChangedNotification { get; }

		[Export ("appearanceMode", ArgumentSemantic.Assign)]
		PSPDFAppearanceMode AppearanceMode { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFAppearanceModeManagerDelegate Delegate { get; set; }
	}

	interface IPSPDFApplication { }

	[Protocol]
	interface PSPDFApplication {

		[Abstract]
		[Export ("canOpenURL:")]
		bool CanOpenUrl (NSUrl url);

		[Abstract]
		[Export ("openURL:options:completionHandler:")]
		void OpenUrl (NSUrl url, [NullAllowed] NSDictionary options, [NullAllowed] Action<bool> completionHandler);

		[Abstract]
		[Export ("networkIndicatorManager")]
		IPSPDFNetworkActivityIndicatorManager NetworkIndicatorManager { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDefaultApplication : PSPDFApplication {

	}

	[BaseType (typeof (PSPDFDefaultApplication))]
	[DisableDefaultCtor]
	interface PSPDFExtensionApplication : PSPDFApplication {

		[Export ("initWithExtensionContext:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSExtensionContext extensionContext);

		[Export ("networkIndicatorManager"), New]
		new IPSPDFNetworkActivityIndicatorManager NetworkIndicatorManager { get; }
	}

	interface IPSPDFAvoidingScrollViewDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFAvoidingScrollViewDelegate : IUIScrollViewDelegate {

		[Export ("scrollViewShouldAvoidKeyboard:")]
		bool ShouldAvoidKeyboard (PSPDFAvoidingScrollView scrollView);
	}

	[BaseType (typeof (UIScrollView))]
	interface PSPDFAvoidingScrollView {

		[Export ("avoidingKeyboard")]
		bool AvoidingKeyboard { [Bind ("isAvoidingKeyboard")] get; }

		[Export ("isHalfModalVisible")]
		bool IsHalfModalVisible { get; }

		[Export ("firstResponderIsTextInput")]
		bool FirstResponderIsTextInput { get; }

		[Export ("enableAvoidance")]
		bool EnableAvoidance { get; set; }
	}

	[BaseType (typeof (PSPDFStyleButton))]
	interface PSPDFBackForwardButton : IPSPDFOverridable {

		[Static]
		[Export ("backButton")]
		PSPDFBackForwardButton BackButton { get; }

		[Static]
		[Export ("forwardButton")]
		PSPDFBackForwardButton ForwardButton { get; }
	}

	[BaseType (typeof (UITableViewController))]
	interface PSPDFBaseTableViewController {

		[Export ("viewWillAppear:"), New]
		[Advice ("Requires base call if override.")]
		void ViewWillAppear (bool animated);

		[Export ("viewDidAppear:"), New]
		[Advice ("Requires base call if override.")]
		void ViewDidAppear (bool animated);

		[Export ("viewWillDisappear:"), New]
		[Advice ("Requires base call if override.")]
		void ViewWillDisappear (bool animated);

		[Export ("viewDidDisappear:"), New]
		[Advice ("Requires base call if override.")]
		void ViewDidDisappear (bool animated);

		[Export ("viewWillLayoutSubviews"), New]
		[Advice ("Requires base call if override.")]
		void ViewWillLayoutSubviews ();
	}

	[BaseType (typeof (UIViewController))]
	interface PSPDFBaseViewController {

		// PSPDFBaseViewController (SubclassingHooks)

		[Export ("contentSizeDidChange")]
		[Advice ("Requires base call if override.")]
		void ContentSizeDidChange ();

		// PSPDFBaseViewController (SubclassingWarnings)

		[Export ("viewWillAppear:"), New]
		[Advice ("Requires base call if override.")]
		void ViewWillAppear (bool animated);

		[Export ("viewDidAppear:"), New]
		[Advice ("Requires base call if override.")]
		void ViewDidAppear (bool animated);

		[Export ("viewWillDisappear:"), New]
		[Advice ("Requires base call if override.")]
		void ViewWillDisappear (bool animated);

		[Export ("viewDidDisappear:"), New]
		[Advice ("Requires base call if override.")]
		void ViewDidDisappear (bool animated);

		[Export ("viewWillLayoutSubviews"), New]
		[Advice ("Requires base call if override.")]
		void ViewWillLayoutSubviews ();

		[Export ("viewDidLayoutSubviews"), New]
		[Advice ("Requires base call if override.")]
		void ViewDidLayoutSubviews ();

		[Export ("didReceiveMemoryWarning"), New]
		[Advice ("Requires base call if override.")]
		void DidReceiveMemoryWarning ();
	}

	interface IPSPDFBookmarkTableViewCellDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFBookmarkTableViewCellDelegate {

		[Abstract]
		[Export ("bookmarkCell:didUpdateBookmarkString:")]
		void DidUpdateBookmarkString (PSPDFBookmarkCell cell, string bookmarkString);
	}

	[BaseType (typeof (PSPDFThumbnailTextCell))]
	interface PSPDFBookmarkCell : IUITextFieldDelegate, IPSPDFOverridable {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFBookmarkTableViewCellDelegate Delegate { get; set; }

		[Export ("isCurrentPage")]
		bool IsCurrentPage { get; set; }
	}

	[BaseType (typeof (UIButton))]
	interface PSPDFBookmarkIndicatorButton : IPSPDFOverridable {

		[Export ("imageType", ArgumentSemantic.Assign)]
		PSPDFBookmarkIndicatorImageType ImageType { get; set; }

		[Export ("normalTintColor", ArgumentSemantic.Strong)]
		UIColor NormalTintColor { get; set; }

		[Export ("selectedTintColor", ArgumentSemantic.Strong)]
		UIColor SelectedTintColor { get; set; }
	}

	interface IPSPDFBookmarkViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFBookmarkViewControllerDelegate {

		[Abstract]
		[Export ("currentPageForBookmarkViewController:")]
		nuint GetCurrentPage (PSPDFBookmarkViewController bookmarkController);

		[Abstract]
		[Export ("bookmarkViewController:didSelectBookmark:")]
		void DidSelectBookmark (PSPDFBookmarkViewController bookmarkController, PSPDFBookmark bookmark);
	}

	[BaseType (typeof (PSPDFStatefulTableViewController))]
	interface PSPDFBookmarkViewController : PSPDFBookmarkTableViewCellDelegate, PSPDFDocumentInfoController, PSPDFSegmentImageProviding, PSPDFStyleable, IPSPDFOverridable {

		[Export ("allowCopy")]
		bool AllowCopy { get; set; }

		[Export ("allowEditing")]
		bool AllowEditing { get; set; }

		[Export ("shouldStartEditingBookmarkNameWhenAdding")]
		bool ShouldStartEditingBookmarkNameWhenAdding { get; set; }

		[Export ("allowMultipleBookmarksPerPage")]
		bool AllowMultipleBookmarksPerPage { get; set; }

		[Export ("sortOrder", ArgumentSemantic.Assign)]
		PSPDFBookmarkManagerSortOrder SortOrder { get; set; }

		[Export ("rightActionButtonItems")]
		UIBarButtonItem [] RightActionButtonItems { get; set; }

		[Export ("leftActionButtonItems")]
		UIBarButtonItem [] LeftActionButtonItems { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFBookmarkViewControllerDelegate Delegate { get; set; }

		// PSPDFBookmarkViewController (SubclassingHooks) Category

		[Export ("configureCell:withBookmark:forRowAtIndexPath:inTableView:")]
		void ConfigureCell (PSPDFBookmarkCell bookmarkCell, PSPDFBookmark bookmark, NSIndexPath indexPath, UITableView tableView);

		[Export ("updateBookmarkViewAnimated:")]
		void UpdateBookmarkView (bool animated);

		[Export ("updateBarButtonItems")]
		void UpdateBarButtonItems ();

		[Export ("addBookmarkAction:")]
		void AddBookmarkAction ([NullAllowed] NSObject sender);

		[Export ("doneAction:")]
		void DoneAction ([NullAllowed] NSObject sender);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFBrightnessManager {

		[Export ("idleTimerManagement", ArgumentSemantic.Assign)]
		PSPDFIdleTimerManagement IdleTimerManagement { get; set; }

		[Export ("extendedIdleTime")]
		double ExtendedIdleTime { get; set; }
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFBrightnessViewController : IPSPDFOverridable {

		[NullAllowed, Export ("appearanceModeManager", ArgumentSemantic.Strong)]
		PSPDFAppearanceModeManager AppearanceModeManager { get; set; }

		[Export ("allowedAppearanceModes", ArgumentSemantic.Assign)]
		PSPDFAppearanceMode AllowedAppearanceModes { get; set; }
	}

	delegate void PSPDFButtonActionHandler (PSPDFButton arg0);

	[BaseType (typeof (UIButton))]
	interface PSPDFButton {

		[Export ("touchAreaInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets TouchAreaInsets { get; set; }

		[Export ("positionImageOnRight")]
		bool PositionImageOnRight { get; set; }

		[Export ("actionBlock", ArgumentSemantic.Copy)]
		PSPDFButtonActionHandler ActionHandler { get; set; }

		[Export ("setActionBlock:forControlEvents:")]
		void SetAction ([NullAllowed] PSPDFButtonActionHandler actionHandler, UIControlEvent controlEvents);
	}

	[BaseType (typeof (PSPDFFormElementView))]
	interface PSPDFButtonFormElementView : IPSPDFOverridable {

	}

	[BaseType (typeof (PSPDFStatefulTableViewController))]
	[DisableDefaultCtor]
	interface PSPDFCertificatePickerViewController : IPSPDFOverridable {

		[Export ("initWithRegisteredSigners:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] PSPDFSigner [] registeredSigners);

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFCertificatePickerViewControllerDelegate Delegate { get; set; }
	}

	interface IPSPDFCertificatePickerViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFCertificatePickerViewControllerDelegate {

		[Export ("certificatePickerViewController:didPickSigner:")]
		void DidPickSigner (PSPDFCertificatePickerViewController controller, [NullAllowed] PSPDFSigner signer);
	}

	[BaseType (typeof (PSPDFFormElementView))]
	interface PSPDFChoiceFormElementView : IPSPDFOverridable {

		[Export ("prepareChoiceFormEditorController")]
		NSObject PrepareChoiceFormEditorController ();
	}

	[BaseType (typeof (UICollectionReusableView))]
	interface PSPDFCollectionReusableFilterView {

		[Static]
		[Wrap ("(float) UILayoutPriority.DefaultHigh - 10f")]
		UILayoutPriority CenterPriority { get; }

		[Static]
		[Wrap ("8")]
		nfloat DefaultMargin { get; }

		[NullAllowed, Export ("filterElement", ArgumentSemantic.Strong)]
		UISegmentedControl FilterElement { get; set; }

		[Export ("filterElementOffset", ArgumentSemantic.Assign)]
		CGPoint FilterElementOffset { get; set; }

		[Export ("minimumFilterMargin", ArgumentSemantic.Assign)]
		UIEdgeInsets MinimumFilterMargin { get; set; }

		[Export ("backgroundStyle", ArgumentSemantic.Assign)]
		PSPDFCollectionReusableFilterViewStyle BackgroundStyle { get; set; }
	}

	[BaseType (typeof (PSPDFButton))]
	interface PSPDFColorButton : IPSPDFOverridable {

		[Export ("shape", ArgumentSemantic.Assign)]
		PSPDFColorButtonShape Shape { get; set; }

		[Export ("contentInset", ArgumentSemantic.Assign)]
		UIEdgeInsets ContentInset { get; set; }

		[NullAllowed, Export ("color", ArgumentSemantic.Strong)]
		UIColor Color { get; set; }

		[NullAllowed, Export ("innerBorderColor", ArgumentSemantic.Strong)]
		UIColor InnerBorderColor { get; set; }

		[Export ("innerBorderWidth")]
		nfloat InnerBorderWidth { get; set; }

		[NullAllowed, Export ("outerBorderColor", ArgumentSemantic.Strong)]
		UIColor OuterBorderColor { get; set; }

		[Export ("outerBorderWidth")]
		nfloat OuterBorderWidth { get; set; }

		[Export ("outerBorderPadding")]
		nfloat OuterBorderPadding { get; set; }
	}

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFConfigurationBuilder {

		[Export ("overrideClass:withClass:")]
		void OverrideClass (Class builtinClass, [NullAllowed] Class subclass);

		[Wrap ("OverrideClass (builtinClass: new Class (builtinType), subclass: subType == null ? null : new Class (subType))")]
		void OverrideClass (Type builtinType, Type subType);

		[Export ("doubleTapAction")]
		PSPDFTapAction DoubleTapAction { get; set; }

		[Export ("formElementZoomEnabled")]
		bool FormElementZoomEnabled { [Bind ("isFormElementZoomEnabled")] get; set; }

		[Export ("scrollOnEdgeTapEnabled")]
		bool ScrollOnEdgeTapEnabled { [Bind ("isScrollOnEdgeTapEnabled")] get; set; }

		[Export ("animateScrollOnEdgeTaps")]
		bool AnimateScrollOnEdgeTaps { [Bind ("animateScrollOnEdgeTaps")] get; set; }

		[Export ("scrollOnEdgeTapMargin")]
		nfloat ScrollOnEdgeTapMargin { get; set; }

		[Export ("textSelectionEnabled")]
		bool TextSelectionEnabled { [Bind ("isTextSelectionEnabled")] get; set; }

		[Export ("imageSelectionEnabled")]
		bool ImageSelectionEnabled { [Bind ("isImageSelectionEnabled")] get; set; }

		[Export ("linkAction")]
		PSPDFLinkAction LinkAction { get; set; }

		[Export ("allowedMenuActions")]
		PSPDFTextSelectionMenuAction AllowedMenuActions { get; set; }

		[Export ("userInterfaceViewMode")]
		PSPDFUserInterfaceViewMode UserInterfaceViewMode { get; set; }

		[Export ("textSelectionMode")]
		PSPDFTextSelectionMode TextSelectionMode { get; set; }

		[Export ("textSelectionShouldSnapToWord")]
		bool TextSelectionShouldSnapToWord { get; set; }

		[Export ("typesShowingColorPresets")]
		PSPDFAnnotationType TypesShowingColorPresets { get; set; }

		[Export ("propertiesForAnnotations")]
		NSDictionary PropertiesForAnnotations { get; set; }

		[Export ("freeTextAccessoryViewEnabled")]
		bool FreeTextAccessoryViewEnabled { [Bind ("isFreeTextAccessoryViewEnabled")] get; set; }

		[Export ("bookmarkSortOrder")]
		PSPDFBookmarkManagerSortOrder BookmarkSortOrder { get; set; }

		[Export ("bookmarkIndicatorMode")]
		PSPDFPageBookmarkIndicatorMode BookmarkIndicatorMode { get; set; }

		[Export ("bookmarkIndicatorInteractionEnabled")]
		bool BookmarkIndicatorInteractionEnabled { get; set; }

		[Export ("allowMultipleBookmarksPerPage")]
		bool AllowMultipleBookmarksPerPage { get; set; }

		[Export ("internalTapGesturesEnabled")]
		bool InternalTapGesturesEnabled { get; set; }

		[Export ("useParentNavigationBar")]
		bool UseParentNavigationBar { get; set; }

		[Export ("userInterfaceViewAnimation")]
		PSPDFUserInterfaceViewAnimation UserInterfaceViewAnimation { get; set; }

		[Export ("thumbnailBarMode")]
		PSPDFThumbnailBarMode ThumbnailBarMode { get; set; }

		[Export ("pageLabelEnabled")]
		bool PageLabelEnabled { [Bind ("isPageLabelEnabled")] get; set; }

		[Export ("documentLabelEnabled")]
		PSPDFAdaptiveConditional DocumentLabelEnabled { get; set; }

		[Export ("shouldHideUserInterfaceOnPageChange")]
		bool ShouldHideUserInterfaceOnPageChange { get; set; }

		[Export ("shouldShowUserInterfaceOnViewWillAppear")]
		bool ShouldShowUserInterfaceOnViewWillAppear { get; set; }

		[Export ("shouldAdjustDocumentInsetsByIncludingHomeIndicatorSafeAreaInsets")]
		bool ShouldAdjustDocumentInsetsByIncludingHomeIndicatorSafeAreaInsets { get; set; }

		[Export ("allowToolbarTitleChange")]
		bool AllowToolbarTitleChange { get; set; }

		[Export ("allowWindowTitleChange")]
		bool AllowWindowTitleChange { get; set; }

		[Export ("renderAnimationEnabled")]
		bool RenderAnimationEnabled { [Bind ("isRenderAnimationEnabled")] get; set; }

		[Export ("renderStatusViewPosition")]
		PSPDFRenderStatusViewPosition RenderStatusViewPosition { get; set; }

		[Export ("pageMode")]
		PSPDFPageMode PageMode { get; set; }

		[Export ("scrubberBarType")]
		PSPDFScrubberBarType ScrubberBarType { get; set; }

		[Export ("hideThumbnailBarForSinglePageDocuments")]
		bool HideThumbnailBarForSinglePageDocuments { get; set; }

		[Export ("thumbnailGrouping")]
		PSPDFThumbnailGrouping ThumbnailGrouping { get; set; }

		[Export ("pageTransition")]
		PSPDFPageTransition PageTransition { get; set; }

		[Export ("scrollDirection")]
		PSPDFScrollDirection ScrollDirection { get; set; }

		[Export ("firstPageAlwaysSingle")]
		bool FirstPageAlwaysSingle { [Bind ("isFirstPageAlwaysSingle")] get; set; }

		[Export ("spreadFitting")]
		PSPDFConfigurationSpreadFitting SpreadFitting { get; set; }

		[Export ("clipToPageBoundaries")]
		bool ClipToPageBoundaries { get; set; }

		[Export ("additionalScrollViewFrameInsets")]
		UIEdgeInsets AdditionalScrollViewFrameInsets { get; set; }

		[Export ("additionalContentInsets")]
		UIEdgeInsets AdditionalContentInsets { get; set; }

		[Export ("minimumZoomScale")]
		float MinimumZoomScale { get; set; }

		[Export ("maximumZoomScale")]
		float MaximumZoomScale { get; set; }

		[Export ("documentViewLayoutDirectionalLock")]
		PSPDFAdaptiveConditional DocumentViewLayoutDirectionalLock { get; set; }

		[Export ("shadowEnabled")]
		bool ShadowEnabled { [Bind ("isShadowEnabled")] get; set; }

		[Export ("shadowOpacity")]
		nfloat ShadowOpacity { get; set; }

		[Export ("shouldHideNavigationBarWithUserInterface")]
		bool ShouldHideNavigationBarWithUserInterface { get; set; }

		[Export ("shouldHideStatusBar")]
		bool ShouldHideStatusBar { get; set; }

		[Export ("shouldHideStatusBarWithUserInterface")]
		bool ShouldHideStatusBarWithUserInterface { get; set; }

		[Export ("shouldShowRedactionInfoButton")]
		bool ShouldShowRedactionInfoButton { get; set; }

		[Export ("redactionUsageHintEnabled")]
		bool RedactionUsageHintEnabled { get; set; }

		[Export ("mainToolbarMode")]
		PSPDFMainToolbarMode MainToolbarMode { get; set; }

		[Export ("backgroundColor")]
		UIColor BackgroundColor { get; set; }

		[Export ("allowedAppearanceModes")]
		PSPDFAppearanceMode AllowedAppearanceModes { get; set; }

		[Export ("thumbnailSize")]
		CGSize ThumbnailSize { get; set; }

		[Export ("thumbnailInteritemSpacing")]
		nfloat ThumbnailInteritemSpacing { get; set; }

		[Export ("thumbnailLineSpacing")]
		nfloat ThumbnailLineSpacing { get; set; }

		[Export ("thumbnailMargin")]
		UIEdgeInsets ThumbnailMargin { get; set; }

		[Export ("annotationAnimationDuration")]
		nfloat AnnotationAnimationDuration { get; set; }

		[Export ("annotationGroupingEnabled")]
		bool AnnotationGroupingEnabled { get; set; }

		[Export ("markupAnnotationMergeBehavior")]
		PSPDFMarkupAnnotationMergeBehavior MarkupAnnotationMergeBehavior { get; set; }

		[Export ("createAnnotationMenuEnabled")]
		bool CreateAnnotationMenuEnabled { [Bind ("isCreateAnnotationMenuEnabled")] get; set; }

		[Export ("createAnnotationMenuGroups", ArgumentSemantic.Copy)]
		PSPDFAnnotationGroup [] CreateAnnotationMenuGroups { get; set; }

		[Export ("naturalDrawingAnnotationEnabled")]
		bool NaturalDrawingAnnotationEnabled { get; set; }

		[Export ("magicInkReplacementThreshold")]
		nuint MagicInkReplacementThreshold { get; set; }

		[Export ("drawCreateMode")]
		PSPDFDrawCreateMode DrawCreateMode { get; set; }

		[Export ("shouldAskForAnnotationUsername")]
		bool ShouldAskForAnnotationUsername { get; set; }

		[Export ("annotationEntersEditModeAfterSecondTapEnabled")]
		bool AnnotationEntersEditModeAfterSecondTapEnabled { get; set; }

		[Advice ("You can use 'EditableAnnotationTypes' for a more strongly typed access.")]
		[NullAllowed, Export ("editableAnnotationTypes", ArgumentSemantic.Copy)]
		NSSet<NSString> WeakEditableAnnotationTypes { get; set; }

		[Export ("autosaveEnabled")]
		bool AutosaveEnabled { [Bind ("isAutosaveEnabled")] get; set; }

		[Export ("allowBackgroundSaving")]
		bool AllowBackgroundSaving { get; set; }

		[Export ("soundAnnotationTimeLimit")]
		double SoundAnnotationTimeLimit { get; set; }

		[Export ("soundAnnotationRecordingOptions")]
		NSDictionary WeakSoundAnnotationRecordingOptions { get; set; }

		[Wrap ("WeakSoundAnnotationRecordingOptions")]
		AudioSettings SoundAnnotationRecordingOptions { get; set; }

		[Export ("shouldCacheThumbnails")]
		bool ShouldCacheThumbnails { get; set; }

		[Export ("allowAnnotationZIndexMoves")]
		bool AllowAnnotationZIndexMoves { get; set; }

		[Export ("allowRemovingDigitalSignatures")]
		bool AllowRemovingDigitalSignatures { get; set; }

		[Export ("annotationMenuConfiguration")]
		PSPDFAnnotationMenuConfiguration AnnotationMenuConfiguration { get; set; }

		[Export ("shouldScrollToChangedPage")]
		bool ShouldScrollToChangedPage { get; set; }

		[Export ("searchMode")]
		PSPDFSearchMode SearchMode { get; set; }

		[Export ("searchResultZoomScale")]
		nfloat SearchResultZoomScale { get; set; }

		[Export ("signatureSavingStrategy")]
		PSPDFSignatureSavingStrategy SignatureSavingStrategy { get; set; }

		[Export ("signatureCertificateSelectionMode")]
		PSPDFSignatureCertificateSelectionMode SignatureCertificateSelectionMode { get; set; }

		[Export ("signatureBiometricPropertiesOptions")]
		PSPDFSignatureBiometricPropertiesOption SignatureBiometricPropertiesOptions { get; set; }

		[Export ("signatureCreationConfiguration")]
		PSPDFSignatureCreationConfiguration SignatureCreationConfiguration { get; set; }

		[Export ("naturalSignatureDrawingEnabled")]
		bool NaturalSignatureDrawingEnabled { get; set; }

		[Export ("signatureStore")]
		IPSPDFSignatureStore SignatureStore { get; set; }

		[Export ("galleryConfiguration")]
		PSPDFGalleryConfiguration GalleryConfiguration { get; set; }

		[Export ("showBackActionButton")]
		bool ShowBackActionButton { get; set; }

		[Export ("showForwardActionButton")]
		bool ShowForwardActionButton { get; set; }

		[Export ("showBackForwardActionButtonLabels")]
		bool ShowBackForwardActionButtonLabels { get; set; }

		[Export ("soundAnnotationPlayerStyle")]
		PSPDFSoundAnnotationPlayerStyle SoundAnnotationPlayerStyle { get; set; }

		[Export ("dragAndDropConfiguration")]
		PSPDFDragAndDropConfiguration DragAndDropConfiguration { get; set; }

		[Export ("documentEditorConfiguration")]
		PSPDFDocumentEditorConfiguration DocumentEditorConfiguration { get; set; }

		[Export ("sharingConfigurations")]
		PSPDFDocumentSharingConfiguration [] SharingConfigurations { get; set; }

		[BindAs (typeof (PSPDFDocumentSharingDestination))]
		[NullAllowed, Export ("selectedSharingDestination")]
		NSString SelectedSharingDestination { get; set; }

		[Export ("settingsOptions")]
		PSPDFSettingsOptions SettingsOptions { get; set; }

		[Export ("contentMenuConfiguration")]
		PSPDFContentMenuConfiguration ContentMenuConfiguration { get; set; }
	}

	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFConfiguration : IPSPDFOverridable {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		NativeHandle Constructor (PSPDFConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFConfigurationBuilder> builderHandler);

		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFConfiguration GetUpdatedConfiguration ([NullAllowed] Action<PSPDFConfigurationBuilder> builderHandler);

		[Export ("pageMode")]
		PSPDFPageMode PageMode { get; }

		[Export ("pageTransition")]
		PSPDFPageTransition PageTransition { get; }

		[Export ("firstPageAlwaysSingle")]
		bool FirstPageAlwaysSingle { [Bind ("isFirstPageAlwaysSingle")] get; }

		[Export ("spreadFitting")]
		PSPDFConfigurationSpreadFitting SpreadFitting { get; }

		[Export ("clipToPageBoundaries")]
		bool ClipToPageBoundaries { get; }

		[Export ("additionalScrollViewFrameInsets")]
		UIEdgeInsets AdditionalScrollViewFrameInsets { get; }

		[Export ("additionalContentInsets")]
		UIEdgeInsets AdditionalContentInsets { get; }

		[Export ("shadowEnabled")]
		bool ShadowEnabled { [Bind ("isShadowEnabled")] get; }

		[Export ("shadowOpacity")]
		nfloat ShadowOpacity { get; }

		[Export ("backgroundColor")]
		UIColor BackgroundColor { get; }

		[Export ("allowedAppearanceModes")]
		PSPDFAppearanceMode AllowedAppearanceModes { get; }

		[Export ("scrollDirection")]
		PSPDFScrollDirection ScrollDirection { get; }

		[Export ("minimumZoomScale")]
		float MinimumZoomScale { get; }

		[Export ("maximumZoomScale")]
		float MaximumZoomScale { get; }

		[Export ("documentViewLayoutDirectionalLock")]
		PSPDFAdaptiveConditional DocumentViewLayoutDirectionalLock { get; }

		[Export ("renderAnimationEnabled")]
		bool RenderAnimationEnabled { [Bind ("isRenderAnimationEnabled")] get; }

		[Export ("renderStatusViewPosition")]
		PSPDFRenderStatusViewPosition RenderStatusViewPosition { get; }

		[Export ("doubleTapAction")]
		PSPDFTapAction DoubleTapAction { get; }

		[Export ("formElementZoomEnabled")]
		bool FormElementZoomEnabled { [Bind ("isFormElementZoomEnabled")] get; }

		[Export ("scrollOnEdgeTapEnabled")]
		bool ScrollOnEdgeTapEnabled { [Bind ("isScrollOnEdgeTapEnabled")] get; }

		[Export ("animateScrollOnEdgeTaps")]
		bool AnimateScrollOnEdgeTaps { [Bind ("animateScrollOnEdgeTaps")] get; }

		[Export ("scrollOnEdgeTapMargin")]
		nfloat ScrollOnEdgeTapMargin { get; }

		[Export ("linkAction")]
		PSPDFLinkAction LinkAction { get; }

		[Export ("allowedMenuActions")]
		PSPDFTextSelectionMenuAction AllowedMenuActions { get; }

		[Export ("textSelectionEnabled")]
		bool TextSelectionEnabled { [Bind ("isTextSelectionEnabled")] get; }

		[Export ("imageSelectionEnabled")]
		bool ImageSelectionEnabled { [Bind ("isImageSelectionEnabled")] get; }

		[Export ("textSelectionMode")]
		PSPDFTextSelectionMode TextSelectionMode { get; }

		[Export ("textSelectionShouldSnapToWord")]
		bool TextSelectionShouldSnapToWord { get; }

		[Advice ("You can use 'EditableAnnotationTypes' for a more strongly typed access.")]
		[NullAllowed, Export ("editableAnnotationTypes", ArgumentSemantic.Copy)]
		NSSet<NSString> WeakEditableAnnotationTypes { get; }

		[Export ("typesShowingColorPresets")]
		PSPDFAnnotationType TypesShowingColorPresets { get; }

		[Export ("propertiesForAnnotations")]
		NSDictionary PropertiesForAnnotations { get; }

		[Export ("freeTextAccessoryViewEnabled")]
		bool FreeTextAccessoryViewEnabled { [Bind ("isFreeTextAccessoryViewEnabled")] get; }

		[Export ("bookmarkSortOrder")]
		PSPDFBookmarkManagerSortOrder BookmarkSortOrder { get; }

		[Export ("bookmarkIndicatorMode")]
		PSPDFPageBookmarkIndicatorMode BookmarkIndicatorMode { get; }

		[Export ("bookmarkIndicatorInteractionEnabled")]
		bool BookmarkIndicatorInteractionEnabled { get; }

		[Export ("allowMultipleBookmarksPerPage")]
		bool AllowMultipleBookmarksPerPage { get; }

		[Export ("userInterfaceViewMode")]
		PSPDFUserInterfaceViewMode UserInterfaceViewMode { get; }

		[Export ("userInterfaceViewAnimation")]
		PSPDFUserInterfaceViewAnimation UserInterfaceViewAnimation { get; }

		[Export ("halfModalStyle")]
		PSPDFPresentationHalfModalStyle HalfModalStyle { get; }

		[Export ("pageLabelEnabled")]
		bool PageLabelEnabled { [Bind ("isPageLabelEnabled")] get; }

		[Export ("documentLabelEnabled")]
		PSPDFAdaptiveConditional DocumentLabelEnabled { get; }

		[Export ("shouldHideUserInterfaceOnPageChange")]
		bool ShouldHideUserInterfaceOnPageChange { get; }

		[Export ("shouldShowUserInterfaceOnViewWillAppear")]
		bool ShouldShowUserInterfaceOnViewWillAppear { get; }

		[Export ("shouldAdjustDocumentInsetsByIncludingHomeIndicatorSafeAreaInsets")]
		bool ShouldAdjustDocumentInsetsByIncludingHomeIndicatorSafeAreaInsets { get; }

		[Export ("allowToolbarTitleChange")]
		bool AllowToolbarTitleChange { get; }

		[Export ("allowWindowTitleChange")]
		bool AllowWindowTitleChange { get; }

		[Export ("shouldHideNavigationBarWithUserInterface")]
		bool ShouldHideNavigationBarWithUserInterface { get; }

		[Export ("shouldHideStatusBar")]
		bool ShouldHideStatusBar { get; }

		[Export ("shouldHideStatusBarWithUserInterface")]
		bool ShouldHideStatusBarWithUserInterface { get; }

		[Export ("shouldShowRedactionInfoButton")]
		bool ShouldShowRedactionInfoButton { get; }

		[Export ("redactionUsageHintEnabled")]
		bool RedactionUsageHintEnabled { get; }

		[Export ("mainToolbarMode")]
		PSPDFMainToolbarMode MainToolbarMode { get; }

		[Export ("showBackActionButton")]
		bool ShowBackActionButton { get; }

		[Export ("showForwardActionButton")]
		bool ShowForwardActionButton { get; }

		[Export ("showBackForwardActionButtonLabels")]
		bool ShowBackForwardActionButtonLabels { get; }

		[Export ("thumbnailBarMode")]
		PSPDFThumbnailBarMode ThumbnailBarMode { get; }

		[Export ("scrubberBarType")]
		PSPDFScrubberBarType ScrubberBarType { get; }

		[Export ("hideThumbnailBarForSinglePageDocuments")]
		bool HideThumbnailBarForSinglePageDocuments { get; }

		[Export ("thumbnailGrouping")]
		PSPDFThumbnailGrouping ThumbnailGrouping { get; }

		[Export ("thumbnailSize")]
		CGSize ThumbnailSize { get; }

		[Export ("thumbnailInteritemSpacing")]
		nfloat ThumbnailInteritemSpacing { get; }

		[Export ("thumbnailLineSpacing")]
		nfloat ThumbnailLineSpacing { get; }

		[Export ("thumbnailMargin")]
		UIEdgeInsets ThumbnailMargin { get; }

		[Export ("annotationAnimationDuration")]
		nfloat AnnotationAnimationDuration { get; }

		[Export ("markupAnnotationMergeBehavior")]
		PSPDFMarkupAnnotationMergeBehavior MarkupAnnotationMergeBehavior { get; }

		[Export ("createAnnotationMenuEnabled")]
		bool CreateAnnotationMenuEnabled { [Bind ("isCreateAnnotationMenuEnabled")] get; }

		[Export ("createAnnotationMenuGroups", ArgumentSemantic.Copy)]
		PSPDFAnnotationGroup [] CreateAnnotationMenuGroups { get; }

		[Export ("naturalDrawingAnnotationEnabled")]
		bool NaturalDrawingAnnotationEnabled { get; }

		[Export ("magicInkReplacementThreshold")]
		nuint MagicInkReplacementThreshold { get; }

		[Export ("drawCreateMode")]
		PSPDFDrawCreateMode DrawCreateMode { get; }

		[Export ("shouldAskForAnnotationUsername")]
		bool ShouldAskForAnnotationUsername { get; }

		[Export ("annotationEntersEditModeAfterSecondTapEnabled")]
		bool AnnotationEntersEditModeAfterSecondTapEnabled { get; }

		[Export ("shouldScrollToChangedPage")]
		bool ShouldScrollToChangedPage { get; }

		[Export ("soundAnnotationPlayerStyle")]
		PSPDFSoundAnnotationPlayerStyle SoundAnnotationPlayerStyle { get; }

		[Export ("autosaveEnabled")]
		bool AutosaveEnabled { [Bind ("isAutosaveEnabled")] get; }

		[Export ("allowBackgroundSaving")]
		bool AllowBackgroundSaving { get; }

		[Export ("soundAnnotationTimeLimit")]
		double SoundAnnotationTimeLimit { get; }

		[Export ("soundAnnotationRecordingOptions")]
		NSDictionary WeakSoundAnnotationRecordingOptions { get; }

		[Wrap ("WeakSoundAnnotationRecordingOptions")]
		AudioSettings SoundAnnotationRecordingOptions { get; }

		[Export ("searchMode")]
		PSPDFSearchMode SearchMode { get; }

		[Export ("searchResultZoomScale")]
		nfloat SearchResultZoomScale { get; }

		[Export ("signatureSavingStrategy")]
		PSPDFSignatureSavingStrategy SignatureSavingStrategy { get; }

		[Export ("signatureCertificateSelectionMode")]
		PSPDFSignatureCertificateSelectionMode SignatureCertificateSelectionMode { get; }

		[Export ("signatureBiometricPropertiesOptions")]
		PSPDFSignatureBiometricPropertiesOption SignatureBiometricPropertiesOptions { get; }

		[Export ("signatureCreationConfiguration")]
		PSPDFSignatureCreationConfiguration SignatureCreationConfiguration { get; }

		[Export ("naturalSignatureDrawingEnabled")]
		bool NaturalSignatureDrawingEnabled { get; }

		[Export ("signatureStore")]
		IPSPDFSignatureStore SignatureStore { get; }

		[Export ("sharingConfigurations")]
		PSPDFDocumentSharingConfiguration [] SharingConfigurations { get; }

		[BindAs (typeof (PSPDFDocumentSharingDestination))]
		[NullAllowed, Export ("selectedSharingDestination")]
		NSString SelectedSharingDestination { get; }

		[Export ("settingsOptions")]
		PSPDFSettingsOptions SettingsOptions { get; }

		[Export ("internalTapGesturesEnabled")]
		bool InternalTapGesturesEnabled { get; }

		[Export ("useParentNavigationBar")]
		bool UseParentNavigationBar { get; }

		[Export ("shouldCacheThumbnails")]
		bool ShouldCacheThumbnails { get; }

		[Export ("allowAnnotationZIndexMoves")]
		bool AllowAnnotationZIndexMoves { get; }

		[Export ("allowRemovingDigitalSignatures")]
		bool AllowRemovingDigitalSignatures { get; }

		[Export ("galleryConfiguration")]
		PSPDFGalleryConfiguration GalleryConfiguration { get; }

		[Export ("dragAndDropConfiguration")]
		PSPDFDragAndDropConfiguration DragAndDropConfiguration { get; }

		[Static]
		[Export ("imageConfiguration")]
		PSPDFConfiguration ImageConfiguration { get; }

		[Export ("documentEditorConfiguration")]
		PSPDFDocumentEditorConfiguration DocumentEditorConfiguration { get; }

		[Export ("annotationMenuConfiguration")]
		PSPDFAnnotationMenuConfiguration AnnotationMenuConfiguration { get; }

		[Export ("contentMenuConfiguration")]
		PSPDFContentMenuConfiguration ContentMenuConfiguration { get; }
	}

	interface IPSPDFDocumentInfoCoordinatorDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentInfoCoordinatorDelegate {

		[Export ("documentInfoCoordinator:infoViewControllerDidCancelDocumentInfoUpdate:")]
		void DidCancelDocumentInfoUpdate (PSPDFDocumentInfoCoordinator infoCoordinator, PSPDFDocumentInfoViewController infoViewController);

		[Export ("documentInfoCoordinator:infoViewControllerDidCommitUpdates:")]
		void DidCommitUpdates (PSPDFDocumentInfoCoordinator infoCoordinator, PSPDFDocumentInfoViewController infoViewController);
	}

	interface IPSPDFContainerViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFContainerViewControllerDelegate {

		[Export ("containerViewController:didUpdateSelectedIndex:")]
		void DidUpdateSelectedIndex (PSPDFContainerViewController controller, nuint selectedIndex);

		[Export ("containerViewController:shouldUseTitleForViewController:")]
		bool ShouldUseTitle (PSPDFContainerViewController controller, UIViewController viewController);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFContainerViewController : PSPDFStyleable, IPSPDFOverridable {

		[Export ("initWithControllers:titles:")]
		NativeHandle Constructor ([NullAllowed] UIViewController [] controllers, [NullAllowed] string [] titles);

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFContainerViewControllerDelegate Delegate { get; set; }

		[Export ("addViewController:")]
		void AddViewController (UIViewController controller);

		[Export ("removeViewController:")]
		void RemoveViewController (UIViewController controller);

		[Export ("viewControllers", ArgumentSemantic.Copy)]
		UIViewController [] ViewControllers { get; }

		[Export ("visibleViewControllerIndex")]
		nuint VisibleViewControllerIndex { get; set; }

		[Export ("setVisibleViewControllerIndex:animated:")]
		void SetVisibleViewControllerIndex (nuint visibleViewControllerIndex, bool animated);

		[Export ("shouldAnimateChanges")]
		bool ShouldAnimateChanges { get; set; }

		[NullAllowed, Export ("lastVisibleViewControllerTitleKey", ArgumentSemantic.Strong)]
		string LastVisibleViewControllerTitleKey { get; set; }

		// PSPDFContainerViewController (SubclassingHooks) Category

		[NullAllowed, Export ("filterSegment")]
		UISegmentedControl FilterSegment { get; set; }
	}

	[BaseType (typeof (PSPDFStackViewLayout))]
	interface PSPDFContinuousScrollingLayout {

		[Export ("fillAlongsideTransverseAxis")]
		bool FillAlongsideTransverseAxis { get; set; }
	}

	interface IPSPDFPageControls { }

	[Protocol]
	interface PSPDFPageControls {

		[Abstract]
		[Export ("setPageIndex:animated:")]
		void SetPageIndex (nuint pageIndex, bool animated);

		[Abstract]
		[Export ("setViewMode:animated:")]
		void SetViewMode (PSPDFViewMode viewMode, bool animated);

		[Abstract]
		[Export ("executePDFAction:targetRect:pageIndex:animated:actionContainer:")]
		bool ExecutePdfAction ([NullAllowed] PSPDFAction action, CGRect targetRect, nuint pageIndex, bool animated, [NullAllowed] NSObject actionContainer);

		[Abstract]
		[Export ("navigateForwardAnimated:")]
		void NavigateForward (bool animated);

		[Abstract]
		[Export ("navigateBackAnimated:")]
		void NavigateBack (bool animated);

		[Abstract]
		[Export ("searchForString:options:sender:animated:")]
		void SearchForString ([NullAllowed] string searchText, [NullAllowed] NSDictionary options, [NullAllowed] NSObject sender, bool animated);

		[Abstract]
		[Export ("presentDocumentInfoViewControllerWithOptions:sender:animated:completion:")]
		[return: NullAllowed]
		UIViewController PresentDocumentInfoViewController ([NullAllowed] NSDictionary options, [NullAllowed] NSObject sender, bool animated, [NullAllowed] Action completion);

		[Abstract]
		[Export ("presentPreviewControllerForURL:title:options:sender:animated:completion:")]
		void PresentPreviewController (NSUrl fileUrl, [NullAllowed] string title, [NullAllowed] NSDictionary options, [NullAllowed] NSObject sender, bool animated, [NullAllowed] Action completion);

		[Abstract]
		[Export ("reloadData")]
		void ReloadData ();

		[Abstract]
		[Export ("printButtonPressed:")]
		void PrintButtonPressed ([NullAllowed] NSObject sender);
	}

	[Protocol]
	interface PSPDFUserInterfaceControls {

		[Abstract]
		[Export ("shouldShowControls")]
		bool ShouldShowControls { get; }

		[Abstract]
		[Export ("hideControlsAnimated:")]
		bool HideControls (bool animated);

		[Abstract]
		[Export ("hideControlsAndPageElementsAnimated:")]
		bool HideControlsAndPageElements (bool animated);

		[Abstract]
		[Export ("toggleControlsAnimated:")]
		bool ToggleControls (bool animated);

		[Abstract]
		[Export ("showControlsAnimated:")]
		bool ShowControls (bool animated);

		[Obsolete]
		[Abstract]
		[Export ("showMenuIfSelectedWithOption:animated:")]
		void ShowMenuIfSelected (PSPDFContextMenuOption contextMenuOption, bool animated);
	}

	interface IPSPDFControlDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFControlDelegate : PSPDFPresentationActions, PSPDFPageControls, PSPDFUserInterfaceControls, PSPDFErrorHandler {

	}

	interface IPSPDFControllerStateHandling { }

	[Protocol]
	interface PSPDFControllerStateHandling {

		[Abstract]
		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; set; }

		[Abstract]
		[Export ("setControllerState:error:animated:")]
		void SetControllerState (PSPDFControllerState state, [NullAllowed] NSError error, bool animated);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFDigitalSignatureCoordinator :IPSPDFOverridable {

		// PSPDFDigitalSignatureCoordinator (SubclassingHooks) Category

		[Export ("pathForDigitallySignedDocumentFromOriginalDocument:suggestedFileName:")]
		string GetPathForDigitallySignedDocument (PSPDFDocument originalDocument, string fileName);

		[Export ("presentSignedDocument:showingPageIndex:withPresentationContext:")]
		void PresentSignedDocument (PSPDFDocument signedDocument, nuint pageIndex, IPSPDFPresentationContext presentationContext);

		[Export ("configureSignatureAppearanceWithBuilder:document:signature:")]
		void ConfigureSignatureAppearance (PSPDFSignatureAppearanceBuilder builder, PSPDFDocument document, PSPDFSignatureContainer signature);
	}

	[BaseType (typeof (PSPDFPageCell))]
	interface PSPDFDocumentEditorCell : IPSPDFOverridable {

	}

	[BaseType (typeof (PSPDFFlexibleToolbar))]
	interface PSPDFDocumentEditorToolbar : IPSPDFOverridable {

		[Export ("addPageButton")]
		PSPDFToolbarButton AddPageButton { get; }

		[Export ("deletePagesButton")]
		PSPDFToolbarButton DeletePagesButton { get; }

		[Export ("duplicatePagesButton")]
		PSPDFToolbarButton DuplicatePagesButton { get; }

		[Export ("rotatePagesButton")]
		PSPDFToolbarButton RotatePagesButton { get; }

		[Export ("exportPagesButton")]
		PSPDFToolbarButton ExportPagesButton { get; }

		[Export ("selectAllPagesButton")]
		PSPDFToolbarButton SelectAllPagesButton { get; }

		[Export ("copyPagesButton")]
		PSPDFToolbarButton CopyPagesButton { get; }

		[Export ("cutPagesButton")]
		PSPDFToolbarButton CutPagesButton { get; }

		[Export ("pastePagesButton")]
		PSPDFToolbarButton PastePagesButton { get; }

		[Export ("undoButton")]
		PSPDFToolbarButton UndoButton { get; }

		[Export ("redoButton")]
		PSPDFToolbarButton RedoButton { get; }

		[Export ("doneButton")]
		PSPDFToolbarButton DoneButton { get; }

		[Export ("allPagesSelected")]
		bool AllPagesSelected { get; set; }

		// PSPDFDocumentEditorToolbar (SubclassingHooks) Category

		[Export ("buttonsForWidth:")]
		PSPDFToolbarButton [] GetButtons (nfloat width);
	}

	interface IPSPDFDocumentEditorToolbarControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentEditorToolbarControllerDelegate : PSPDFFlexibleToolbarContainerDelegate {

		[Abstract]
		[Export ("documentEditorToolbarController:didSelectPages:")]
		void DidSelectPages (PSPDFDocumentEditorToolbarController controller, NSIndexSet pages);

		[Export ("documentEditorToolbarController:indexForNewPageWithConfiguration:")]
		nuint GetIndexForNewPage (PSPDFDocumentEditorToolbarController controller, PSPDFNewPageConfiguration configuration);
	}

	interface PSPDFDocumentEditorToolbarControllerVisibilityDidChangeNotificationEventArgs {

		[Export ("PSPDFDocumentEditorToolbarControllerVisibilityAnimatedKey")]
		bool Animated { get; }
	}

	delegate void PSPDFDocumentEditorToolbarControllerToggleSaveCompletionHandler (bool cancelled);

	[BaseType (typeof (PSPDFFlexibleToolbarController))]
	interface PSPDFDocumentEditorToolbarController : IPSPDFDocumentEditorDelegate, PSPDFNewPageViewControllerDelegate, PSPDFNewPageViewControllerDataSource, PSPDFSaveViewControllerDelegate, IPSPDFOverridable {

		[Field ("PSPDFDocumentEditorToolbarControllerVisibilityDidChangeNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFDocumentEditorToolbarControllerVisibilityDidChangeNotificationEventArgs))]
		NSString VisibilityDidChangeNotification { get; }

		[Export ("initWithDocumentEditorToolbar:")]
		NativeHandle Constructor (PSPDFDocumentEditorToolbar documentEditorToolbar);

		[Export ("documentEditorToolbar")]
		PSPDFDocumentEditorToolbar DocumentEditorToolbar { get; }

		[NullAllowed, Export ("documentEditor", ArgumentSemantic.Strong)]
		PSPDFDocumentEditor DocumentEditor { get; set; }

		[Export ("selectedPages", ArgumentSemantic.Copy)]
		NSIndexSet SelectedPages { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFDocumentEditorToolbarControllerDelegate Delegate { get; set; }

		[NullAllowed, Export ("presentationContext", ArgumentSemantic.Weak)]
		IPSPDFPresentationContext PresentationContext { get; set; }

		[Export ("documentEditorConfiguration")]
		PSPDFDocumentEditorConfiguration DocumentEditorConfiguration { get; }

		[Export ("toggleNewPageController:presentationOptions:")]
		[return: NullAllowed]
		PSPDFNewPageViewController ToggleNewPageController ([NullAllowed] NSObject sender, [NullAllowed] NSDictionary options);

		[Export ("copySelectedPagesToPasteboard")]
		void CopySelectedPagesToPasteboard ();

		[Export ("pastePagesFromPasteboard")]
		void PastePagesFromPasteboard ();

		[Wrap ("ToggleNewPageController (sender, presentationOptions?.Dictionary)")]
		PSPDFNewPageViewController ToggleNewPageController (NSObject sender, PSPDFPresentationOptions presentationOptions);

		[Async]
		[Export ("toggleSavingConfirmationViewController:presentationOptions:completionHandler:")]
		[return: NullAllowed]
		UIViewController ToggleSavingConfirmationViewController ([NullAllowed] NSObject sender, [NullAllowed] NSDictionary options, [NullAllowed] PSPDFDocumentEditorToolbarControllerToggleSaveCompletionHandler completionHandler);

		[Async]
		[Wrap ("ToggleSavingConfirmationViewController (sender, presentationOptions?.Dictionary, completionHandler)")]
		UIViewController ToggleSavingConfirmationViewController (NSObject sender, PSPDFPresentationOptions presentationOptions, PSPDFDocumentEditorToolbarControllerToggleSaveCompletionHandler completionHandler);

		[Export ("toggleSaveController:presentationOptions:completionHandler:")]
		[return: NullAllowed]
		PSPDFSaveViewController ToggleSaveController ([NullAllowed] NSObject sender, [NullAllowed] NSDictionary options, [NullAllowed] PSPDFDocumentEditorToolbarControllerToggleSaveCompletionHandler completionHandler);

		[Async]
		[Wrap ("ToggleSaveController (sender, presentationOptions?.Dictionary, completionHandler)")]
		PSPDFSaveViewController ToggleSaveController (NSObject sender, PSPDFPresentationOptions presentationOptions, PSPDFDocumentEditorToolbarControllerToggleSaveCompletionHandler completionHandler);

		// PSPDFDocumentEditorToolbarController (SubclassingHooks) Category

		[Export ("savingConfirmationControllerForSender:completionHandler:")]
		UIViewController SavingConfirmationController (NSObject sender, PSPDFDocumentEditorToolbarControllerToggleSaveCompletionHandler completionHandler);
	}

	[BaseType (typeof (UICollectionViewController))]
	interface PSPDFDocumentEditorViewController : PSPDFViewModePresenter, IPSPDFDocumentEditorDelegate, PSPDFFlexibleToolbarContainerDelegate, IPSPDFOverridable {

		[Export ("cellClass", ArgumentSemantic.Strong)]
		new Class CellClass { get; set; }

		[NullAllowed, Export ("documentEditor", ArgumentSemantic.Strong)]
		PSPDFDocumentEditor DocumentEditor { get; set; }

		[Export ("toolbarController")]
		PSPDFDocumentEditorToolbarController ToolbarController { get; }

		[Export ("editorInteractiveCapabilities", ArgumentSemantic.Assign)]
		PSPDFDocumentEditorInteractiveCapabilities EditorInteractiveCapabilities { get; set; }
	}

	interface IPSPDFDocumentInfoController { }

	[Protocol]
	interface PSPDFDocumentInfoController {

		//TODO: inline
		// Hack: This must be manually bound to any class implementing this potocol
		//[Abstract]
		//[Export ("initWithDocument:")]
		//NativeHandle Constructor ([NullAllowed] PSPDFDocument document);

		[Abstract]
		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; set; }

		[return: NullAllowed]
		[Export ("container")]
		PSPDFContainerViewController GetContainer ();

		[Export ("setContainer:")]
		void SetContainer ([NullAllowed] PSPDFContainerViewController container);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentInfoCoordinator : IPSPDFOverridable {

		[Export ("documentInfoViewController")]
		UIViewController GetDocumentInfoViewController ();

		[Async]
		[Export ("presentToViewController:options:sender:animated:completion:")]
		[return: NullAllowed]
		UIViewController PresentToViewController (IPSPDFPresentationActions targetController, [NullAllowed] NSDictionary options, [NullAllowed] NSObject sender, bool animated, [NullAllowed] Action completion);

		[Export ("available")]
		bool Available { [Bind ("isAvailable")] get; }

		[NullAllowed, Export ("document", ArgumentSemantic.Strong)]
		PSPDFDocument Document { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFDocumentInfoCoordinatorDelegate Delegate { get; set; }

		[Protected]
		[Export ("availableControllerOptions", ArgumentSemantic.Copy)]
		NSString [] _AvailableControllerOptions { get; set; }

		[Protected]
		[NullAllowed, Export ("didCreateControllerBlock", ArgumentSemantic.Copy)]
		Action<UIViewController, NSString> _DidCreateControllerHandler { get; set; }

		// PSPDFDocumentInfoCoordinator (SubclassingHooks) Category

		[Protected]
		[Export ("controllerForOption:")]
		[return: NullAllowed]
		UIViewController GetController (NSString option);

		[Wrap ("GetController (infoOption.GetConstant ())")]
		UIViewController GetController (PSPDFDocumentInfoOption infoOption);

		[Protected]
		[Export ("isOptionAvailable:")]
		bool IsOptionAvailable (NSString option);

		[Wrap ("IsOptionAvailable (infoOption.GetConstant ())")]
		bool IsOptionAvailable (PSPDFDocumentInfoOption infoOption);
	}

	[BaseType (typeof (PSPDFTableViewCell))]
	interface PSPDFDocumentPickerCell {

		[Export ("configureWithDocument:useDocumentTitle:detailText:pageIndex:previewImage:")]
		void Configure (PSPDFDocument document, bool useDocumentTitle, [NullAllowed] NSAttributedString detailText, nuint pageIndex, UIImage previewImage);

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; set; }

		[Export ("pageIndex")]
		nuint PageIndex { get; set; }

		[NullAllowed, Export ("pagePreviewImage", ArgumentSemantic.Strong)]
		UIImage PagePreviewImage { get; set; }

		[Export ("setPagePreviewImage:animated:")]
		void SetPagePreviewImage ([NullAllowed] UIImage pagePreviewImage, bool animated);

		[Export ("pageImageView", ArgumentSemantic.Strong)]
		UIImageView PageImageView { get; set; }

		[Export ("titleLabel", ArgumentSemantic.Strong)]
		UILabel TitleLabel { get; set; }

		[Export ("detailLabel", ArgumentSemantic.Strong)]
		UILabel DetailLabel { get; set; }

		// PSPDFDocumentPickerCell (SubclassingHooks) Category

		[Static]
		[Export ("titleLabelFont", ArgumentSemantic.Strong)]
		UIFont TitleLabelFont { get; }

		[Static]
		[Export ("detailLabelFont", ArgumentSemantic.Strong)]
		UILabel DetailLabelFont { get; }
	}

	interface IPSPDFDocumentPickerControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentPickerControllerDelegate {

		[Abstract]
		[Export ("documentPickerController:didSelectDocument:pageIndex:searchString:")]
		void DidSelectDocument (PSPDFDocumentPickerController controller, PSPDFDocument document, nuint pageIndex, [NullAllowed] string searchString);

		[Export ("documentPickerControllerWillBeginSearch:")]
		void WillBeginSearch (PSPDFDocumentPickerController controller);

		[Export ("documentPickerControllerDidBeginSearch:")]
		void DidBeginSearch (PSPDFDocumentPickerController controller);

		[Export ("documentPickerControllerWillEndSearch:")]
		void WillEndSearch (PSPDFDocumentPickerController controller);

		[Export ("documentPickerControllerDidEndSearch:")]
		void DidEndSearch (PSPDFDocumentPickerController controller);
	}

	[BaseType (typeof (PSPDFSearchableTableViewController))]
#if __MACCATALYST__
	interface PSPDFDocumentPickerController {
#else
	interface PSPDFDocumentPickerController : IUISearchDisplayDelegate, IUISearchBarDelegate {
#endif
		[Static]
		[Export ("documentsFromDirectory:includeSubdirectories:")]
		PSPDFDocument [] GetDocuments ([NullAllowed] string directoryName, bool includeSubdirectories);

		[Export ("initWithDirectory:includeSubdirectories:library:")]
		NativeHandle Constructor ([NullAllowed] string directory, bool includeSubdirectories, [NullAllowed] PSPDFLibrary library);

		[Export ("initWithDocuments:library:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocument [] documents, [NullAllowed] PSPDFLibrary library);

		[Export ("enqueueDocumentsIfRequired")]
		void EnqueueDocumentsIfRequired ();

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFDocumentPickerControllerDelegate Delegate { get; set; }

		[Export ("documents", ArgumentSemantic.Copy)]
		PSPDFDocument [] Documents { get; }

		[NullAllowed, Export ("directory")]
		string Directory { get; }

		[Export ("useDocumentTitles")]
		bool UseDocumentTitles { get; set; }

		[Export ("showSectionIndexes")]
		bool ShowSectionIndexes { get; set; }

		[Export ("alwaysShowDocuments")]
		bool AlwaysShowDocuments { get; set; }

		[Export ("fullTextSearchEnabled")]
		bool FullTextSearchEnabled { get; set; }

		[Export ("fullTextSearchExactWordMatch")]
		bool FullTextSearchExactWordMatch { get; set; }

		[Export ("isSearchingIndex")]
		bool IsSearchingIndex { get; }

		[Export ("showSearchPageResults")]
		bool ShowSearchPageResults { get; set; }

		[Export ("showSearchPreviewText")]
		bool ShowSearchPreviewText { get; set; }

		[Export ("maximumNumberOfSearchResultsDisplayed")]
		nuint MaximumNumberOfSearchResultsDisplayed { get; set; }

		[Export ("maximumNumberOfSearchResultsPerDocument")]
		nuint MaximumNumberOfSearchResultsPerDocument { get; set; }

		[Export ("maximumNumberOfSearchPreviewLines")]
		nuint MaximumNumberOfSearchPreviewLines { get; set; }

		[NullAllowed, Export ("library")]
		PSPDFLibrary Library { get; }

		// PSPDFDocumentPickerController (SubclassingHooks) Category

		[Export ("updateStatusCell:")]
		void UpdateStatusCell (PSPDFDocumentPickerIndexStatusCell cell);
	}

	[BaseType (typeof (PSPDFSpinnerCell))]
	interface PSPDFDocumentPickerIndexStatusCell {

	}

	interface IPSPDFDocumentSharingViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentSharingViewControllerDelegate {

		[Export ("documentSharingViewController:didFinishSharingWithConfiguration:userInfo:error:")]
		void DidFinishSharing (PSPDFDocumentSharingViewController shareController, PSPDFDocumentSharingConfiguration configuration, NSDictionary userInfo, [NullAllowed] NSError error);

		[Export ("documentSharingViewController:didFinishGeneratingFiles:withConfiguration:error:")]
		void DidFinishGeneratingFiles (PSPDFDocumentSharingViewController shareController, PSPDFFile [] files, PSPDFDocumentSharingConfiguration configuration, [NullAllowed] NSError error);

		[Export ("documentSharingViewController:didCancelSharingAtStep:withConfiguration:")]
		void DidCancelSharingAtStep (PSPDFDocumentSharingViewController shareController, PSPDFDocumentSharingStep sharingStep, PSPDFDocumentSharingConfiguration configuration);

		[Export ("documentSharingViewControllerWasDismissed")]
		void DocumentSharingViewControllerWasDismissed ();

		[Export ("documentSharingViewController:preparationProgress:")]
		void PreparationProgress (PSPDFDocumentSharingViewController shareController, nfloat progress);

		[Export ("documentSharingViewController:filenameForGeneratedFileForDocument:destination:")]
		[return: NullAllowed]
		string GetFileNameForGeneratedFile (PSPDFDocumentSharingViewController shareController, PSPDFDocument sharingDocument, NSString destination);

		[Export ("documentSharingViewController:shouldProcessForSharingWithState:")]
		bool ShouldProcessForSharingWithState (PSPDFDocumentSharingViewController shareController, PSPDFDocumentSharingConfiguration sharingConfiguration);

		[Export ("documentSharingViewController:shouldShareFiles:toDestination:")]
		bool ShouldShareFiles (PSPDFDocumentSharingViewController shareController, PSPDFFile [] files, NSString destination);

		[Export ("documentSharingViewController:shouldSaveDocument:withOptions:")]
		bool ShouldSaveDocument (PSPDFDocumentSharingViewController shareController, PSPDFDocument document,  NSDictionary options);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	[DisableDefaultCtor]
	interface PSPDFDocumentSharingViewController : PSPDFStyleable, IPSPDFOverridable {

		[Export ("initWithDocuments:")]
		NativeHandle Constructor (PSPDFDocument [] documents);

		[Export ("initWithDocuments:sharingConfigurations:")]
		NativeHandle Constructor (PSPDFDocument [] documents, [NullAllowed] PSPDFDocumentSharingConfiguration [] sharingConfigurations);

		[Export ("documents")]
		PSPDFDocument [] Documents { get; }

		[Export ("sharingConfigurations", ArgumentSemantic.Copy)]
		PSPDFDocumentSharingConfiguration [] SharingConfigurations { get; set; }

		[Export ("selectedAnnotationOption", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingAnnotationOptions SelectedAnnotationOption { get; set; }

		[Export ("selectedPageSelectionOption", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingPagesOptions SelectedPageSelectionOption { get; set; }

		[Export ("selectedFileFormatOption", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingFileFormatOptions SelectedFileFormatOption { get; set; }

		[Export ("selectedDestination")]
		string SelectedDestination { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFDocumentSharingViewControllerDelegate Delegate { get; set; }

		[NullAllowed, Export ("visiblePagesDataSource", ArgumentSemantic.Weak)]
		IPSPDFVisiblePagesDataSource VisiblePagesDataSource { get; set; }

		[Export ("shareablePageRange", ArgumentSemantic.Assign)]
		NSRange ShareablePageRange { get; set; }

		[Export ("checkIfControllerHasOptionsAvailableAndCallDelegateIfNot")]
		bool CheckIfControllerHasOptionsAvailableAndCallDelegateIfNot { get; }

		[Export ("commitButtonTitle")]
		string CommitButtonTitle { get; set; }

		[Export ("commitWithCurrentConfiguration")]
		void CommitWithCurrentConfiguration ();

		[Export ("cancelDocumentPreparationIfAny")]
		void CancelDocumentPreparationIfAny ();

		[Export ("presentFromViewController:sender:")]
		void PresentFromViewController (IPSPDFPresentationActions viewController, [NullAllowed] NSObject sender);

		[Export ("currentSharingConfigurationForDestination:")]
		[return: NullAllowed]
		PSPDFDocumentSharingConfiguration CurrentSharingConfigurationForDestination (string destination);

		// PSPDFDocumentSharingViewController (SubclassingHooks) Category

		[NullAllowed, Export ("documentSecurityOptions")]
		PSPDFDocumentSecurityOptions DocumentSecurityOptions { get; }

		[Export ("printInfo")]
		UIPrintInfo PrintInfo { get; }

		[Export ("activityViewControllerForSharingItems:sender:")]
		[return: NullAllowed]
		UIActivityViewController ActivityViewControllerForSharingItems (NSObject [] activityItems, NSObject sender);

		[Export ("configureMailComposeViewController:")]
		void ConfigureMailComposeViewController (MFMailComposeViewController mailComposeViewController);

		[Export ("configureProcessorConfigurationOptions:")]
		void ConfigureProcessorConfigurationOptions (PSPDFProcessorConfiguration processorConfiguration);

		[Export ("addAttachmentData:mimeType:fileName:")]
		void AddAttachmentData (NSData attachment, string mimeType, string filename);

		[Export ("titleForDestination:")]
		string GetTitleForDestination (string destination);

		[Export ("temporaryDirectoryForSharingToDestination:")]
		[return: NullAllowed]
		string GetTemporaryDirectoryForSharingToDestination (string destination);

		[return: NullAllowed]
		[Export ("titleForAnnotationOptions:")]
		string GetTitleForAnnotationOptions (PSPDFDocumentSharingAnnotationOptions option);

		[Export ("subtitleForAnnotationsOptions:sharingConfiguration:")]
		[return: NullAllowed]
		string GetSubtitleForAnnotationsOptions (PSPDFDocumentSharingAnnotationOptions option, PSPDFDocumentSharingConfiguration sharingConfiguration);
	}

	interface IPSPDFDocumentViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentViewControllerDelegate {

		[Export ("documentViewController:didChangeSpreadIndex:")]
		void DidChangeSpreadIndex (PSPDFDocumentViewController documentViewController, nint oldSpreadIndex);

		[Export ("documentViewController:didChangeContinuousSpreadIndex:")]
		void DidChangeContinuousSpreadIndex (PSPDFDocumentViewController documentViewController, nfloat oldContinuousSpreadIndex);

		[Export ("documentViewController:didUpdateZoomScale:forSpreadAtIndex:")]
		void DidUpdateZoomScale (PSPDFDocumentViewController documentViewController, nfloat zoomScale, nint spreadIndex);

		[Export ("documentViewController:willBeginDisplayingSpreadView:forSpreadAtIndex:")]
		void WillBeginDisplayingSpreadView (PSPDFDocumentViewController documentViewController, PSPDFSpreadView spreadView, nint spreadIndex);

		[Export ("documentViewController:didEndDisplayingSpreadView:forSpreadAtIndex:")]
		void DidEndDisplayingSpreadView (PSPDFDocumentViewController documentViewController, PSPDFSpreadView spreadView, nint spreadIndex);

		[Export ("documentViewController:didConfigureSpreadView:forSpreadAtIndex:")]
		void DidConfigureSpreadView (PSPDFDocumentViewController documentViewController, PSPDFSpreadView spreadView, nint spreadIndex);

		[Export ("documentViewController:didCleanupSpreadView:forSpreadAtIndex:")]
		void DidCleanupSpreadView (PSPDFDocumentViewController documentViewController, PSPDFSpreadView spreadView, nint spreadIndex);

		[Export ("documentViewController:configureScrollView:")]
		void ConfigureScrollView (PSPDFDocumentViewController documentViewController, UIScrollView scrollView);

		[Export ("documentViewController:configureZoomView:forSpreadAtIndex:")]
		void ConfigureZoomView (PSPDFDocumentViewController documentViewController, UIScrollView zoomView, nint spreadIndex);
	}

	interface PSPDFDocumentViewControllerSpreadViewEventArgs {

		[Export ("PSPDFDocumentViewControllerSpreadViewKey")]
		PSPDFSpreadView SpreadView { get; set; }
	}

	[BaseType (typeof (UIViewController))]
	[DisableDefaultCtor]
	interface PSPDFDocumentViewController : IPSPDFOverridable {

		[Field ("PSPDFDocumentViewControllerWillBeginDisplayingSpreadViewNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFAnnotationToolbarControllerVisibilityDidChangeNotificationEventArgs))]
		NSString WillBeginDisplayingSpreadViewNotification { get; }

		[Field ("PSPDFDocumentViewControllerDidEndDisplayingSpreadViewNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFAnnotationToolbarControllerVisibilityDidChangeNotificationEventArgs))]
		NSString DidEndDisplayingSpreadViewNotification { get; }

		[Field ("PSPDFDocumentViewControllerDidConfigureSpreadViewNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFAnnotationToolbarControllerVisibilityDidChangeNotificationEventArgs))]
		NSString DidConfigureSpreadViewNotification { get; }

		[Field ("PSPDFDocumentViewControllerDidCleanupSpreadViewNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFAnnotationToolbarControllerVisibilityDidChangeNotificationEventArgs))]
		NSString DidCleanupSpreadViewNotification { get; }

		[Field ("PSPDFDocumentViewControllerSpreadIndexDidChangeNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString SpreadIndexDidChangeNotification { get; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFDocumentViewControllerDelegate Delegate { get; set; }

		[Export ("layout", ArgumentSemantic.Strong)]
		PSPDFDocumentViewLayout Layout { get; set; }

		[Export ("spreadIndex")]
		nint SpreadIndex { get; set; }

		[Export ("setSpreadIndex:animated:")]
		void SetSpreadIndex (nint spreadIndex, bool animated);

		[Export ("scrollToSpreadAtIndex:scrollPosition:animated:")]
		void ScrollToSpread (nint spreadIndex, PSPDFSpreadScrollPosition scrollPosition, bool animated);

		[Export ("continuousSpreadIndex")]
		nfloat ContinuousSpreadIndex { get; set; }

		[Export ("setContinuousSpreadIndex:animated:")]
		void SetContinuousSpreadIndex (nfloat continuousSpreadIndex, bool animated);

		[Export ("scrollToNextSpreadAnimated:")]
		bool ScrollToNextSpread (bool animated);

		[Export ("scrollToPreviousSpreadAnimated:")]
		bool ScrollToPreviousSpread (bool animated);

		[Export ("scrollToNextViewportAnimated:")]
		bool ScrollToNextViewport (bool animated);

		[Export ("scrollToPreviousViewportAnimated:")]
		bool ScrollToPreviousViewport (bool animated);

		[Export ("zoomToPDFRect:forPageAtIndex:animated:")]
		void ZoomTo (CGRect pdfRect, nint pageIndex, bool animated);

		[Export ("scrollEnabled")]
		bool ScrollEnabled { [Bind ("isScrollEnabled")] get; set; }

		[Export ("zoomEnabled")]
		bool ZoomEnabled { [Bind ("isZoomEnabled")] get; set; }

		[Export ("alwaysBounce")]
		bool AlwaysBounce { get; set; }

		[Export ("showsScrollIndicator")]
		bool ShowsScrollIndicator { get; set; }

		[Export ("visibleSpreadViews", ArgumentSemantic.Copy)]
		PSPDFSpreadView [] VisibleSpreadViews { get; }

		// PSPDFDocumentViewController (PageViews) Category

		[Export ("visiblePageViewAtPoint:")]
		[return: NullAllowed]
		PSPDFPageView GetVisiblePageView (CGPoint atPoint);
	}

	[BaseType (typeof (UICollectionViewLayoutInvalidationContext))]
	interface PSPDFDocumentViewLayoutInvalidationContext {

	}

	[BaseType (typeof (UICollectionViewLayout))]
	interface PSPDFDocumentViewLayout {

		[Export ("document")]
		PSPDFDocument Document { get; }

		[Export ("spreadBasedZooming")]
		bool SpreadBasedZooming { get; set; }

		[Export ("scrollViewFrameInsets")]
		UIEdgeInsets ScrollViewFrameInsets { get; }

		[Export ("additionalScrollViewFrameInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets AdditionalScrollViewFrameInsets { get; set; }

		[Export ("continuousSpreadIndexForContentOffset:")]
		nfloat GetContinuousSpreadIndex (CGPoint contentOffset);

		[Export ("contentOffsetForContinuousSpreadIndex:")]
		CGPoint GetContentOffset (nfloat continuousSpreadIndex);

		[Export ("numberOfSpreads")]
		nint NumberOfSpreads { get; }

		[Export ("spreadIndexForPageAtIndex:")]
		nint GetSpreadIndex (nint pageIndex);

		[Export ("pageRangeForSpreadAtIndex:")]
		NSRange GetPageRange (nint spreadIndex);

		[Export ("spreadMode")]
		PSPDFDocumentViewLayoutSpreadMode SpreadMode { get; }

		[Export ("invalidateConfiguration")]
		void InvalidateConfiguration ();

		// PSPDFDocumentViewLayout (Subclassing)

		[Export ("viewport")]
		CGRect Viewport { get; }

		[Export ("shouldInvalidateLayoutForViewportChange:")]
		bool ShouldInvalidateLayout (CGRect newViewport);

		[Export ("invalidationContextForViewportChange:")]
		UICollectionViewLayoutInvalidationContext GetInvalidationContext (CGRect newViewport);

		[Export ("invalidateLayoutWithContext:"), New]
		[Advice ("Requires base call if override.")]
		void InvalidateLayout (UICollectionViewLayoutInvalidationContext context);

		// PSPDFDocumentViewLayout (Convenience)

		[Static]
		[Export ("scrollPerSpreadLayout")]
		PSPDFScrollPerSpreadLayout ScrollPerSpreadLayout { get; }

		[Static]
		[Export ("continuousScrollingLayout")]
		PSPDFContinuousScrollingLayout ContinuousScrollingLayout { get; }

		[Static]
		[Export ("pageCurlLayout")]
		PSPDFDocumentViewLayout PageCurlLayout { get; }
	}

#if !NET
	[Category (true)]
	[BaseType (typeof (NSIndexPath))]
	interface NSIndexPath_PSPDFDocumentViewLayout {

		[Static]
		[Export ("pspdf_indexPathForSpreadAtIndex:")]
		NSIndexPath GetPsPdfIndexPathForSpread (nint spreadIndex);

		[Export ("pspdf_spreadIndex")]
		nint GetPsPdfSpreadIndex ();
	}
#endif

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFDragAndDropConfigurationBuilder {

		[Export ("allowedDragTypes")]
		PSPDFDragType AllowedDragTypes { get; set; }

		[Export ("acceptedDropTypes")]
		PSPDFDropType AcceptedDropTypes { get; set; }

		[Export ("allowedDropTargets")]
		PSPDFDropTarget AllowedDropTargets { get; set; }

		[Export ("allowDraggingToExternalApps")]
		bool AllowDraggingToExternalApps { get; set; }
	}

	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFDragAndDropConfiguration {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFDragAndDropConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		NativeHandle Constructor (PSPDFDragAndDropConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFDragAndDropConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFDragAndDropConfigurationBuilder> builderHandler);

		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFDragAndDropConfiguration GetUpdatedConfiguration ([NullAllowed] Action<PSPDFDragAndDropConfigurationBuilder> builderHandler);

		[Export ("allowedDragTypes")]
		PSPDFDragType AllowedDragTypes { get; }

		[Export ("acceptedDropTypes")]
		PSPDFDropType AcceptedDropTypes { get; }

		[Export ("allowedDropTargets")]
		PSPDFDropTarget AllowedDropTargets { get; }

		[Export ("allowDraggingToExternalApps")]
		bool AllowDraggingToExternalApps { get; }
	}

	[BaseType (typeof (PSPDFContainerView))]
	interface PSPDFDrawView : PSPDFAnnotationPresenting, IPSPDFOverridable {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFDrawViewDelegate Delegate { get; set; }

		[Export ("annotationType", ArgumentSemantic.Assign)]
		PSPDFAnnotationType AnnotationType { get; set; }

		[BindAs (typeof (PSPDFAnnotationVariantString))]
		[NullAllowed, Export ("annotationVariant")]
		NSString AnnotationVariant { get; set; }

		[Export ("inputMode", ArgumentSemantic.Assign)]
		PSPDFDrawViewInputMode InputMode { get; set; }

		[Export ("drawGestureRecognizer")]
		UIGestureRecognizer DrawGestureRecognizer { get; }

		[Export ("drawCreateMode", ArgumentSemantic.Assign)]
		PSPDFDrawCreateMode DrawCreateMode { get; set; }

		[Export ("naturalDrawingEnabled")]
		bool NaturalDrawingEnabled { get; set; }

		[Export ("predictiveTouchesEnabled")]
		bool PredictiveTouchesEnabled { get; set; }

		[Export ("scale")]
		nfloat Scale { get; set; }

		[NullAllowed, Export ("strokeColor", ArgumentSemantic.Strong)]
		UIColor StrokeColor { get; set; }

		[NullAllowed, Export ("fillColor", ArgumentSemantic.Strong)]
		UIColor FillColor { get; set; }

		[Export ("lineWidth")]
		nfloat LineWidth { get; set; }

		[Export ("lineEnd1", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd1 { get; set; }

		[Export ("lineEnd2", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd2 { get; set; }

		[NullAllowed, Export ("dashArray", ArgumentSemantic.Copy)]
		NSNumber[] DashArray { get; set; }

		[Export ("borderEffect", ArgumentSemantic.Assign)]
		PSPDFAnnotationBorderEffect BorderEffect { get; set; }

		[Export ("borderEffectIntensity")]
		nfloat BorderEffectIntensity { get; set; }

		[Export ("blendMode", ArgumentSemantic.Assign)]
		CGBlendMode BlendMode { get; set; }

		[NullAllowed, Export ("guideBorderColor", ArgumentSemantic.Strong)]
		UIColor GuideBorderColor { get; set; }

		[Export ("annotations")]
		PSPDFAnnotation [] Annotations { get; }

		[Export ("updateForAnnotations:")]
		void UpdateForAnnotations (PSPDFInkAnnotation[] annotations);

		[Export ("clear")]
		void Clear ();

		[Export ("pointSequences")]
		NSArray<NSValue> [] PointSequences { get; }

		[Export ("pressureList")]
		NSNumber [] PressureList { get; }

		[Export ("timePoints")]
		NSNumber [] TimePoints { get; }

		[Export ("touchRadii")]
		NSNumber [] TouchRadii { get; }

		[Export ("inputMethod")]
		PSPDFDrawInputMethod InputMethod { get; }

		[Export ("startDrawingAtPoint:")]
		void StartDrawing (PSPDFDrawingPoint location);

		[Export ("continueDrawingAtPoints:predictedPoints:")]
		void ContinueDrawing (NSValue [] locations, NSValue [] predictedLocations);

		[Export ("endDrawing")]
		void EndDrawing ();

		[Export ("cancelDrawing")]
		void CancelDrawing ();

		[Export ("guideSnapAllowance")]
		nfloat GuideSnapAllowance { get; set; }

		[Export ("eraseAt:")]
		void EraseAt (NSValue[] locations);

		[Export ("endErase")]
		void EndErase ();

		[Export ("cancelErase")]
		void CancelErase ();
	}

	[BaseType (typeof (PSPDFNonAnimatingTableViewCell))]
	interface PSPDFEmbeddedFileCell : IPSPDFOverridable {

	}

	interface IPSPDFEmbeddedFilesViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFEmbeddedFilesViewControllerDelegate {

		[Abstract]
		[Export ("embeddedFilesController:didSelectFile:sender:")]
		void DidSelectFile (PSPDFEmbeddedFilesViewController embeddedFilesController, PSPDFEmbeddedFile embeddedFile, [NullAllowed] NSObject sender);
	}

	[BaseType (typeof (PSPDFStatefulTableViewController))]
	interface PSPDFEmbeddedFilesViewController : PSPDFDocumentInfoController, PSPDFSegmentImageProviding, IPSPDFOverridable {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFEmbeddedFilesViewControllerDelegate Delegate { get; set; }
	}

	interface IPSPDFEraseOverlayDataSource { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFEraseOverlayDataSource {

		[Abstract]
		[Export ("zoomScaleForEraseOverlay:")]
		nfloat GetZoomScale (PSPDFEraseOverlay overlay);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFEraseOverlay : IPSPDFOverridable {

		[Export ("circleVisible")]
		bool CircleVisible { get; set; }

		[Export ("circleLineWidth")]
		nfloat CircleLineWidth { get; set; }

		[Export ("circleRadius")]
		nfloat CircleRadius { get; set; }

		[Export ("circleColor", ArgumentSemantic.Strong)]
		UIColor CircleColor { get; set; }

		[Export ("shapeLayer")]
		CAShapeLayer ShapeLayer { get; }

		[Export ("touchPosition", ArgumentSemantic.Assign)]
		CGPoint TouchPosition { get; set; }

		[Export ("tracking")]
		bool Tracking { get; set; }

		[Export ("setTracking:animated:")]
		void SetTracking (bool tracking, bool animated);

		[NullAllowed, Export ("dataSource", ArgumentSemantic.Weak)]
		IPSPDFEraseOverlayDataSource DataSource { get; set; }
	}

	interface IPSPDFErrorHandler { }

	[Protocol]
	interface PSPDFErrorHandler {

		[Abstract]
		[Export ("handleError:title:message:")]
		void HandleError ([NullAllowed] NSError error, [NullAllowed] string title, [NullAllowed] string message);
	}

	interface IPSPDFExternalURLHandler { }

	[Protocol]
	interface PSPDFExternalURLHandler {

		[Abstract]
		[Export ("handleExternalURL:completionBlock:")]
		bool handleExternalUrl (NSUrl url, [NullAllowed] Action<bool> completionHandler);
	}

	interface IPSPDFFlexibleToolbarDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFFlexibleToolbarDelegate {

		[Export ("flexibleToolbarWillShow:")]
		void WillShow (PSPDFFlexibleToolbar toolbar);

		[Export ("flexibleToolbarDidShow:")]
		void DidShow (PSPDFFlexibleToolbar toolbar);

		[Export ("flexibleToolbarWillHide:")]
		void WillHide (PSPDFFlexibleToolbar toolbar);

		[Export ("flexibleToolbarDidHide:")]
		void DidHide (PSPDFFlexibleToolbar toolbar);

		[Export ("flexibleToolbar:didChangePosition:")]
		void DidChangePosition (PSPDFFlexibleToolbar toolbar, PSPDFFlexibleToolbarPosition position);
	}

	delegate void PSPDFToolbarCompletionHandler (bool finished);

	[BaseType (typeof (PSPDFToolbar))]
	interface PSPDFFlexibleToolbar {

		[Wrap ("position == PSPDFFlexibleToolbarPosition.Right ? PSPDFToolbarGroupButtonIndicatorPosition.BottomLeft : PSPDFToolbarGroupButtonIndicatorPosition.BottomRight")]
		PSPDFToolbarGroupButtonIndicatorPosition GetGroupIndicatorPositionForToolbarPosition (PSPDFFlexibleToolbarPosition position);

		[Wrap ("position == PSPDFFlexibleToolbarPosition.InTopBar")]
		bool IsHorizontalPosition (PSPDFFlexibleToolbarPosition position);

		[Wrap ("position == PSPDFFlexibleToolbarPosition.Left || position == PSPDFFlexibleToolbarPosition.Right")]
		bool IsVerticalPosition (PSPDFFlexibleToolbarPosition position);

		[Export ("supportedToolbarPositions", ArgumentSemantic.Assign)]
		PSPDFFlexibleToolbarPosition SupportedToolbarPositions { get; set; }

		[Export ("toolbarPosition", ArgumentSemantic.Assign)]
		PSPDFFlexibleToolbarPosition ToolbarPosition { get; set; }

		[Export ("setToolbarPosition:animated:")]
		void SetToolbarPosition (PSPDFFlexibleToolbarPosition toolbarPosition, bool animated);

		[NullAllowed, Export ("toolbarDelegate", ArgumentSemantic.Weak)]
		IPSPDFFlexibleToolbarDelegate ToolbarDelegate { get; set; }

		[Export ("dragEnabled")]
		bool DragEnabled { [Bind ("isDragEnabled")] get; set; }

		[NullAllowed, Export ("selectedButton", ArgumentSemantic.Strong)]
		UIButton SelectedButton { get; set; }

		[Export ("setSelectedButton:animated:")]
		void SetSelectedButton ([NullAllowed] UIButton button, bool animated);

		[Async]
		[Export ("showToolbarAnimated:completion:")]
		void ShowToolbarAnimated (bool animated, [NullAllowed] PSPDFToolbarCompletionHandler completion);

		[Async]
		[Export ("hideToolbarAnimated:completion:")]
		void HideToolbarAnimated (bool animated, [NullAllowed] PSPDFToolbarCompletionHandler completionBlock);

		[Export ("dragView")]
		PSPDFFlexibleToolbarDragView DragView { get; }

		[Export ("selectedTintColor", ArgumentSemantic.Strong)]
		UIColor SelectedTintColor { get; set; }

		[Export ("selectedBackgroundColor", ArgumentSemantic.Strong)]
		UIColor SelectedBackgroundColor { get; set; }

		[Export ("borderedToolbarPositions", ArgumentSemantic.Assign)]
		PSPDFFlexibleToolbarPosition BorderedToolbarPositions { get; set; }

		[Export ("shadowedToolbarPositions", ArgumentSemantic.Assign)]
		PSPDFFlexibleToolbarPosition ShadowedToolbarPositions { get; set; }

		[Export ("roundedToolbarPositions", ArgumentSemantic.Assign)]
		PSPDFFlexibleToolbarPosition RoundedToolbarPositions { get; set; }

		[Export ("matchUIBarAppearance:")]
		void MatchUIBarAppearance (IPSPDFSystemBar navigationBarOrToolbar);

		[Export ("preferredSizeFitting:forToolbarPosition:")]
		CGSize GetPreferredSizeFitting (CGSize availableSize, PSPDFFlexibleToolbarPosition position);

		[Obsolete ("Use 'UIButton.Menu' to add context menus to buttons instead.")]
		[Export ("showMenuWithItems:target:animated:")]
		void ShowMenu (PSPDFMenuItem [] menuItems, UIView target, bool animated);

		[Obsolete ("Use 'UIButton.Menu' to add context menus to buttons instead.")]
		[Export ("showMenuForCollapsedButtons:fromButton:animated:")]
		void ShowMenuForCollapsedButtons (UIButton [] buttons, UIButton sourceButton, bool animated);

		[Obsolete ("Use 'UIButton.Menu' to add context menus to buttons instead.")]
		[Export ("menuItemForButton:")]
		PSPDFMenuItem GetMenuItem (UIButton button);
	}

	interface IPSPDFFlexibleToolbarContainerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFFlexibleToolbarContainerDelegate {

		[Export ("flexibleToolbarContainerWillShow:")]
		void WillShow (PSPDFFlexibleToolbarContainer container);

		[Export ("flexibleToolbarContainerDidShow:")]
		void DidShow (PSPDFFlexibleToolbarContainer container);

		[Export ("flexibleToolbarContainerWillHide:")]
		void WillHide (PSPDFFlexibleToolbarContainer container);

		[Export ("flexibleToolbarContainerDidHide:")]
		void DidHide (PSPDFFlexibleToolbarContainer container);

		[Export ("flexibleToolbarContainerContentRect:forToolbarPosition:")]
		CGRect GetFlexibleToolbarContainerContentRect (PSPDFFlexibleToolbarContainer container, PSPDFFlexibleToolbarPosition position);

		[Export ("flexibleToolbarContainerWillStartDragging:")]
		void WillStartDragging (PSPDFFlexibleToolbarContainer container);

		[Export ("flexibleToolbarContainerDidEndDragging:withPosition:")]
		void DidEndDragging (PSPDFFlexibleToolbarContainer container, PSPDFFlexibleToolbarPosition position);
	}

	interface IPSPDFSystemBar { }

	[Protocol]
	interface PSPDFSystemBar {

	}

	[BaseType (typeof (UIView))]
	interface PSPDFFlexibleToolbarContainer {

		[NullAllowed, Export ("flexibleToolbar", ArgumentSemantic.Strong)]
		PSPDFFlexibleToolbar FlexibleToolbar { get; set; }

		[NullAllowed, Export ("overlaidBar", ArgumentSemantic.Weak)]
		IPSPDFSystemBar OverlaidBar { get; set; }

		[Export ("dragging")]
		bool Dragging { get; }

		[NullAllowed, Export ("containerDelegate", ArgumentSemantic.Weak)]
		IPSPDFFlexibleToolbarContainerDelegate ContainerDelegate { get; set; }

		[Export ("anchorViewBackgroundColor", ArgumentSemantic.Strong)]
		UIColor AnchorViewBackgroundColor { get; set; }

		[Async]
		[Export ("showAnimated:completion:")]
		void Show (bool animated, [NullAllowed] PSPDFToolbarCompletionHandler completion);

		[Async]
		[Export ("hideAnimated:completion:")]
		void Hide (bool animated, [NullAllowed] PSPDFToolbarCompletionHandler completion);

		[Async]
		[Export ("hideAndRemoveAnimated:completion:")]
		void HideAndRemove (bool animated, [NullAllowed] PSPDFToolbarCompletionHandler completion);

		// PSPDFFlexibleToolbarContainer (SubclassingHooks) Category

		[Export ("rectForToolbarPosition:")]
		CGRect GetRect (PSPDFFlexibleToolbarPosition toolbarPosition);

		[Export ("animateToolbarPositionChangeFrom:to:")]
		void AnimateToolbarPositionChange (PSPDFFlexibleToolbarPosition currentPosition, PSPDFFlexibleToolbarPosition newPosition);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFFlexibleToolbarDragView {

		[Export ("barColor", ArgumentSemantic.Strong)]
		UIColor BarColor { get; set; }

		[Export ("inverted")]
		bool Inverted { get; set; }

		[Export ("setInverted:animated:")]
		void SetInverted (bool inverted, bool animated);
	}

	interface IPSPDFFontPickerViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFFontPickerViewControllerDelegate {

		[Abstract]
		[Export ("fontPickerViewController:didSelectFont:")]
		void DidSelectFont (PSPDFFontPickerViewController fontPickerViewController, [NullAllowed] UIFont selectedFont);
	}

	[BaseType (typeof (PSPDFPickerTableViewController))]
	interface PSPDFFontPickerViewController : IPSPDFOverridable {

		[Export ("initWithFontFamilyDescriptors:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] UIFontDescriptor [] fontFamilyDescriptors);

		[Export ("fontFamilyDescriptors", ArgumentSemantic.Copy)]
		UIFontDescriptor [] FontFamilyDescriptors { get; }

		[NullAllowed, Export ("selectedFont", ArgumentSemantic.Strong)]
		UIFont SelectedFont { get; set; }

		[NullAllowed, Export ("highlightedFontFamilyDescriptors", ArgumentSemantic.Strong)]
		UIFontDescriptor [] HighlightedFontFamilyDescriptors { get; set; }

		[Export ("searchEnabled")]
		bool SearchEnabled { get; set; }

		[Export ("showDownloadableFonts")]
		bool ShowDownloadableFonts { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFFontPickerViewControllerDelegate Delegate { get; set; }

		// UIFontDescriptor (Blacklisting) Category

		[Static]
		[Export ("pspdf_defaultBlacklist")]
		string [] DefaultFontBlacklist { get; [Bind ("setPSPDFDefaultBlacklist:")] set; }
	}

	[BaseType (typeof (PSPDFHostingAnnotationView))]
	interface PSPDFFormElementView : PSPDFFormInputAccessoryViewDelegate {

	}

	[BaseType (typeof (UIView))]
	interface PSPDFFormInputAccessoryView : IPSPDFOverridable {

		[Export ("initWithFrame:")]
		NativeHandle Constructor (CGRect frame);

		[Export ("displayDoneButton")]
		bool DisplayDoneButton { get; set; }

		[Export ("displayClearButton")]
		bool DisplayClearButton { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFFormInputAccessoryViewDelegate Delegate { get; set; }

		[Export ("updateToolbar")]
		void UpdateToolbar ();

		// PSPDFFormInputAccessoryView (SubclassingHooks) Category

		[Export ("nextButton")]
		UIBarButtonItem NextButton { get; }

		[Export ("prevButton")]
		UIBarButtonItem PrevButton { get; }

		[Export ("doneButton")]
		UIBarButtonItem DoneButton { get; }

		[Export ("clearButton")]
		UIBarButtonItem ClearButton { get; }

		[Export ("nextButtonPressed:")]
		void NextButtonPressed ([NullAllowed] NSObject sender);

		[Export ("prevButtonPressed:")]
		void PrevButtonPressed ([NullAllowed] NSObject sender);

		[Export ("doneButtonPressed:")]
		void DoneButtonPressed ([NullAllowed] NSObject sender);

		[Export ("clearButtonPressed:")]
		void ClearButtonPressed ([NullAllowed] NSObject sender);
	}

	interface IPSPDFFormInputAccessoryViewDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFFormInputAccessoryViewDelegate {

		[Abstract]
		[Export ("doneButtonPressedOnFormInputView:")]
		void DoneButtonPressed (PSPDFFormInputAccessoryView inputView);

		[Abstract]
		[Export ("previousButtonPressedOnFormInputView:")]
		void PreviousButtonPressed (PSPDFFormInputAccessoryView inputView);

		[Abstract]
		[Export ("nextButtonPressedOnFormInputView:")]
		void NextButtonPressed (PSPDFFormInputAccessoryView inputView);

		[Abstract]
		[Export ("clearButtonPressedOnFormInputView:")]
		void ClearButtonPressed (PSPDFFormInputAccessoryView inputView);

		[Abstract]
		[Export ("formInputViewShouldEnablePreviousButton:")]
		bool ShouldEnablePreviousButton (PSPDFFormInputAccessoryView inputView);

		[Abstract]
		[Export ("formInputViewShouldEnableNextButton:")]
		bool ShouldEnableNextButton (PSPDFFormInputAccessoryView inputView);

		[Abstract]
		[Export ("formInputViewShouldEnableClearButton:")]
		bool ShouldEnableClearButton (PSPDFFormInputAccessoryView inputView);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFormRequest {

		[Export ("initWithFormat:values:request:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFSubmitFormActionFormat format, NSDictionary<NSString, NSObject> values, NSUrlRequest request);

		[Export ("submissionFormat")]
		PSPDFSubmitFormActionFormat SubmissionFormat { get; }

		[Export ("formValues")]
		NSDictionary<NSString, NSObject> FormValues { get; }

		[Export ("request")]
		NSUrlRequest Request { get; }
	}

	interface IPSPDFFormSubmissionDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFFormSubmissionDelegate {

		[Export ("formSubmissionController:shouldSubmitFormRequest:")]
		bool ShouldSubmitFormRequest (NSObject /* PSPDFFormSubmissionController */ formSubmissionController, PSPDFFormRequest formRequest);

		[Export ("formSubmissionController:willSubmitFormValues:")]
		void WillSubmitFormValues (NSObject /* PSPDFFormSubmissionController */ formSubmissionController, PSPDFFormRequest formRequest);

		[Export ("formSubmissionController:didReceiveResponseData:")]
		void DidReceiveResponseData (NSObject /* PSPDFFormSubmissionController */ formSubmissionController, NSData responseData);

		[Export ("formSubmissionController:didFailWithError:")]
		void DidFail (NSObject /* PSPDFFormSubmissionController */ formSubmissionController, NSError error);

		[Export ("formSubmissionControllerShouldPresentResponseInWebView:")]
		bool ShouldPresentResponseInWebView (NSObject /* PSPDFFormSubmissionController */ formSubmissionController);
	}

	interface IPSPDFFreeTextAccessoryViewDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFFreeTextAccessoryViewDelegate {

		[Export ("doneButtonPressedOnFreeTextAccessoryView:")]
		void DoneButtonPressed (PSPDFFreeTextAccessoryView inputView);

		[Export ("clearButtonPressedOnFreeTextAccessoryView:")]
		void ClearButtonPressed (PSPDFFreeTextAccessoryView inputView);

		[Export ("freeTextAccessoryViewDidRequestInspector:")]
		[return: NullAllowed]
		PSPDFAnnotationStyleViewController DidRequestInspector (PSPDFFreeTextAccessoryView inputView);

		[Export ("freeTextAccessoryView:shouldChangeProperty:")]
		bool ShouldChangeProperty (PSPDFFreeTextAccessoryView styleController, string propertyName);

		[Export ("freeTextAccessoryView:didChangeProperty:")]
		void DidChangeProperty (PSPDFFreeTextAccessoryView styleController, string propertyName);
	}

	[BaseType (typeof (PSPDFToolbar))]
	interface PSPDFFreeTextAccessoryView : PSPDFFontPickerViewControllerDelegate, PSPDFAnnotationStyleViewControllerDelegate, IPSPDFOverridable {

		[Field ("PSPDFFreeTextAccessoryViewDidPressClearButtonNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString DidPressClearButtonNotification { get; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFFreeTextAccessoryViewDelegate Delegate { get; set; }

		[NullAllowed, Export ("presentationContext", ArgumentSemantic.Weak)]
		IPSPDFPresentationContext PresentationContext { get; set; }

		[Export ("annotation", ArgumentSemantic.Strong)]
		PSPDFFreeTextAnnotation Annotation { get; set; }

		[Export ("propertiesForAnnotations", ArgumentSemantic.Copy)]
		NSDictionary PropertiesForAnnotations { get; set; }

		[Export ("borderVisible")]
		bool BorderVisible { [Bind ("isBorderVisible")] get; set; }

		[Export ("separatorColor", ArgumentSemantic.Strong)]
		UIColor SeparatorColor { get; set; }

		// PSPDFFreeTextAccessoryView (SubclassingHooks) Category

		[Export ("buttonsForWidth:")]
		PSPDFToolbarButton [] GetButtons (nfloat width);

		[Export ("dismissPresentedViewControllersAnimated:")]
		[Advice ("Requires base call if override.")]
		void DismissPresentedViewControllers (bool animated);

		[Export ("fontNameButton")]
		PSPDFToolbarButton FontNameButton { get; }

		[Export ("fontSizeButton")]
		PSPDFToolbarButton FontSizeButton { get; }

		[Export ("increaseFontSizeButton")]
		PSPDFToolbarButton IncreaseFontSizeButton { get; }

		[Export ("decreaseFontSizeButton")]
		PSPDFToolbarButton DecreaseFontSizeButton { get; }

		[Export ("leftAlignButton")]
		PSPDFToolbarSelectableButton LeftAlignButton { get; }

		[Export ("centerAlignButton")]
		PSPDFToolbarSelectableButton CenterAlignButton { get; }

		[Export ("rightAlignButton")]
		PSPDFToolbarSelectableButton RightAlignButton { get; }

		[Export ("colorButton")]
		PSPDFToolbarButton ColorButton { get; }

		[Export ("clearButton")]
		PSPDFToolbarButton ClearButton { get; }

		[Export ("doneButton")]
		PSPDFToolbarButton DoneButton { get; }
	}

	[BaseType (typeof (PSPDFHostingAnnotationView))]
	interface PSPDFFreeTextAnnotationView : IUITextViewDelegate, IPSPDFOverridable {

		[Export ("beginEditing")]
		bool BeginEditing ();

		[Export ("endEditing")]
		void EndEditing ();

		[NullAllowed, Export ("textView")]
		UITextView TextView { get; }

		[NullAllowed, Export ("resizableView", ArgumentSemantic.Weak)]
		PSPDFResizableView ResizableView { get; set; }

		// PSPDFFreeTextAnnotationView (SubclassingHooks) Category

		[NullAllowed, Export ("textViewForEditing")]
		UITextView TextViewForEditing { get; }
	}

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFGalleryConfigurationBuilder {

		[Export ("maximumConcurrentDownloads")]
		nuint MaximumConcurrentDownloads { get; set; }

		[Export ("maximumPrefetchDownloads")]
		nuint MaximumPrefetchDownloads { get; set; }

		[Export ("displayModeUserInteractionEnabled")]
		bool DisplayModeUserInteractionEnabled { get; set; }

		[Export ("fullscreenDismissPanThreshold")]
		nfloat FullscreenDismissPanThreshold { get; set; }

		[Export ("fullscreenZoomEnabled")]
		bool FullscreenZoomEnabled { get; set; }

		[Export ("maximumFullscreenZoomScale")]
		nfloat MaximumFullscreenZoomScale { get; set; }

		[Export ("minimumFullscreenZoomScale")]
		nfloat MinimumFullscreenZoomScale { get; set; }

		[Export ("loopEnabled")]
		bool LoopEnabled { get; set; }

		[Export ("loopHUDEnabled")]
		bool LoopHudEnabled { get; set; }

		[Export ("usesExternalPlaybackWhileExternalScreenIsActive")]
		bool UsesExternalPlaybackWhileExternalScreenIsActive { get; set; }

		[Export ("allowPlayingMultipleInstances")]
		bool AllowPlayingMultipleInstances { get; set; }
	}

	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFGalleryConfiguration {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFGalleryConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		NativeHandle Constructor (PSPDFGalleryConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFGalleryConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFGalleryConfigurationBuilder> builderHandler);

		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFGalleryConfiguration GetUpdatedConfiguration ([NullAllowed] Action<PSPDFGalleryConfigurationBuilder> builderHandler);

		[Export ("maximumConcurrentDownloads")]
		nuint MaximumConcurrentDownloads { get; }

		[Export ("maximumPrefetchDownloads")]
		nuint MaximumPrefetchDownloads { get; }

		[Export ("displayModeUserInteractionEnabled")]
		bool DisplayModeUserInteractionEnabled { get; }

		[Export ("fullscreenDismissPanThreshold")]
		nfloat FullscreenDismissPanThreshold { get; }

		[Export ("fullscreenZoomEnabled")]
		bool FullscreenZoomEnabled { [Bind ("isFullscreenZoomEnabled")] get; }

		[Export ("maximumFullscreenZoomScale")]
		nfloat MaximumFullscreenZoomScale { get; }

		[Export ("minimumFullscreenZoomScale")]
		nfloat MinimumFullscreenZoomScale { get; }

		[Export ("loopEnabled")]
		bool LoopEnabled { [Bind ("isLoopEnabled")] get; }

		[Export ("loopHUDEnabled")]
		bool LoopHudEnabled { [Bind ("isLoopHUDEnabled")] get; }

		[Export ("usesExternalPlaybackWhileExternalScreenIsActive")]
		bool UsesExternalPlaybackWhileExternalScreenIsActive { get; }

		[Export ("allowPlayingMultipleInstances")]
		bool AllowPlayingMultipleInstances { get; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryEmbeddedBackgroundView : IPSPDFOverridable {

	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryFullscreenBackgroundView : IPSPDFOverridable {

	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryContainerView : IPSPDFOverridable {

		[Export ("initWithFrame:")]
		NativeHandle Constructor (CGRect frame);

		[Export ("contentState", ArgumentSemantic.Assign)]
		PSPDFGalleryContainerViewContentState ContentState { get; set; }

		[Export ("presentationMode", ArgumentSemantic.Assign)]
		PSPDFGalleryContainerViewPresentationMode PresentationMode { get; set; }

		[Export ("galleryView", ArgumentSemantic.Strong)]
		PSPDFGalleryView GalleryView { get; set; }

		[Export ("loadingView", ArgumentSemantic.Strong)]
		IPSPDFGalleryContentViewLoading LoadingView { get; set; }

		[Export ("backgroundView", ArgumentSemantic.Strong)]
		PSPDFGalleryEmbeddedBackgroundView BackgroundView { get; set; }

		[Export ("fullscreenBackgroundView", ArgumentSemantic.Strong)]
		PSPDFGalleryFullscreenBackgroundView FullscreenBackgroundView { get; set; }

		[Export ("statusHUDView", ArgumentSemantic.Strong)]
		PSPDFStatusHUDView StatusHudView { get; set; }

		[Export ("contentContainerView")]
		UIView ContentContainerView { get; }

		[Export ("presentStatusHUDWithTimeout:animated:")]
		void PresentStatusHud (double timeout, bool animated);

		[Export ("dismissStatusHUDAnimated:")]
		void DismissStatusHud (bool animated);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryContentCaptionView : PSPDFGalleryContentViewCaption {

		// Inlined from PSPDFGalleryContentViewCaption protocol
		//[NullAllowed, Export ("caption")]
		//string Caption { get; set; }

		[Export ("label")]
		UILabel Label { get; }

		[Export ("contentInset", ArgumentSemantic.Assign)]
		UIEdgeInsets ContentInset { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryContentView : INativeObject {

		[Export ("initWithReuseIdentifier:")]
		NativeHandle Constructor ([NullAllowed] string reuseIdentifier);

		[Export ("contentView")]
		UIView ContentView { get; }

		[Export ("loadingView")]
		IPSPDFGalleryContentViewLoading LoadingView { get; }

		[Export ("captionView")]
		IPSPDFGalleryContentViewCaption CaptionView { get; }

		[Export ("errorView")]
		IPSPDFGalleryContentViewError ErrorView { get; }

		[NullAllowed, Export ("reuseIdentifier")]
		string ReuseIdentifier { get; set; }

		[NullAllowed, Export ("content", ArgumentSemantic.Strong)]
		PSPDFGalleryItem Content { get; set; }

		[Export ("shouldHideCaption")]
		bool ShouldHideCaption { get; set; }

		// PSPDFGalleryContentView (SubclassingHooks) Category

		[Static]
		[Export ("contentViewClass")]
		Class ContentViewClass { get; }

		[Static]
		[Export ("loadingViewClass")]
		Class LoadingViewClass { get; }

		[Static]
		[Export ("captionViewClass")]
		Class CaptionViewClass { get; }

		[Static]
		[Export ("errorViewClass")]
		Class ErrorViewClass { get; }

		[Export ("contentViewFrame")]
		CGRect ContentViewFrame { get; }

		[Export ("loadingViewFrame")]
		CGRect LoadingViewFrame { get; }

		[Export ("captionViewFrame")]
		CGRect CaptionViewFrame { get; }

		[Export ("errorViewFrame")]
		CGRect ErrorViewFrame { get; }

		[Export ("updateContentView")]
		void UpdateContentView ();

		[Export ("updateCaptionView")]
		[Advice ("Requires base call if override.")]
		void UpdateCaptionView ();

		[Export ("updateErrorView")]
		[Advice ("Requires base call if override.")]
		void UpdateErrorView ();

		[Export ("updateLoadingView")]
		[Advice ("Requires base call if override.")]
		void UpdateLoadingView ();

		[Export ("prepareForReuse")]
		[Advice ("Requires base call if override.")]
		void PrepareForReuse ();

		[Export ("contentDidChange")]
		[Advice ("Requires base call if override.")]
		void ContentDidChange ();

		[Export ("updateSubviewVisibility")]
		[Advice ("Requires base call if override.")]
		void UpdateSubviewVisibility ();
	}

	interface IPSPDFGalleryContentViewLoading { }

	[Protocol]
	interface PSPDFGalleryContentViewLoading {

		[Abstract]
		[Export ("progress")]
		nfloat Progress { get; set; }

		[Export ("hasUnspecifiedProgress")]
		bool HasUnspecifiedProgress { get; set; }
	}

	interface IPSPDFGalleryContentViewError { }

	[Protocol]
	interface PSPDFGalleryContentViewError {

		[Abstract]
		[NullAllowed, Export ("error", ArgumentSemantic.Strong)]
		NSError Error { get; set; }
	}

	interface IPSPDFGalleryContentViewCaption { }

	[Protocol]
	interface PSPDFGalleryContentViewCaption {

		[Abstract]
		[NullAllowed, Export ("caption")]
		string Caption { get; set; }
	}

	[BaseType (typeof (PSPDFGalleryContentView))]
	interface PSPDFGalleryImageContentView {

		[Export ("zoomEnabled")]
		bool ZoomEnabled { [Bind ("isZoomEnabled")] get; set; }

		[Export ("maximumZoomScale")]
		nfloat MaximumZoomScale { get; set; }

		[Export ("minimumZoomScale")]
		nfloat MinimumZoomScale { get; set; }

		[Export ("zoomScale")]
		nfloat ZoomScale { get; set; }

		[Export ("setZoomScale:animated:")]
		void SetZoomScale (nfloat zoomScale, bool animated);

		[NullAllowed, Export ("content", ArgumentSemantic.Strong), New]
		PSPDFGalleryImageItem Content { get; set; }

		[Export ("contentView"), New]
		UIImageView ContentView { get; }
	}

	[BaseType (typeof (PSPDFGalleryItem))]
	interface PSPDFGalleryImageItem {

		[NullAllowed, Export ("content"), New]
		UIImage Content { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFGalleryItem {

		[Export ("contentURL")]
		NSUrl ContentUrl { get; }

		[NullAllowed, Export ("caption")]
		string Caption { get; }

		[Advice ("Use 'ItemOptions' for a strongly typed access.")]
		[NullAllowed, Export ("options")]
		NSDictionary Options { get; }

		[Wrap ("Options")]
		PSPDFGalleryItemOptions ItemOptions { get; }

		[Export ("contentState")]
		PSPDFGalleryItemContentState ContentState { get; }

		[NullAllowed, Export ("content")]
		NSObject Content { get; }

		[Export ("validContent")]
		bool ValidContent { [Bind ("hasValidContent")] get; }

		[NullAllowed, Export ("error")]
		NSError Error { get; }

		[Export ("progress")]
		nfloat Progress { get; }

		[Static]
		[Export ("itemsFromJSONData:error:")]
		[return: NullAllowed]
		PSPDFGalleryItem [] GetItemsFromJson (NSData jsonData, [NullAllowed] out NSError error);

		[Static]
		[Export ("itemFromLinkAnnotation:error:")]
		[return: NullAllowed]
		PSPDFGalleryItem GetItemFromLinkAnnotation (PSPDFLinkAnnotation annotation, [NullAllowed] out NSError error);

		[Export ("initWithDictionary:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSDictionary dictionary);

		[Wrap ("this (options?.Dictionary)")]
		NativeHandle Constructor (PSPDFGalleryItemOptions options);

		[Export ("initWithContentURL:caption:options:")]
		NativeHandle Constructor (NSUrl contentUrl, [NullAllowed] string caption, [NullAllowed] NSDictionary options);

		[Wrap ("this (contentUrl,caption, options?.Dictionary)")]
		NativeHandle Constructor (NSUrl contentUrl, string caption, PSPDFGalleryItemOptions options);

		[Export ("controlsEnabled")]
		bool ControlsEnabled { get; set; }

		[Export ("fullscreenEnabled")]
		bool FullscreenEnabled { [Bind ("isFullscreenEnabled")] get; set; }
	}

	[Static]
	interface PSPDFGalleryItemKeys {

		[Field ("PSPDFGalleryItemPropertyType", PSPDFKitGlobal.LibraryPath)]
		NSString TypeKey { get; }

		[Field ("PSPDFGalleryItemPropertyContentURL", PSPDFKitGlobal.LibraryPath)]
		NSString ContentUrlKey { get; }

		[Field ("PSPDFGalleryItemPropertyCaption", PSPDFKitGlobal.LibraryPath)]
		NSString CaptionKey { get; }

		[Field ("PSPDFGalleryItemPropertyOptions", PSPDFKitGlobal.LibraryPath)]
		NSString OptionsKey { get; }
	}

	[StrongDictionary ("PSPDFGalleryItemKeys")]
	interface PSPDFGalleryItemOptions {
		string Type { get; set; }
		string ContentUrl { get; set; }
		string Caption { get; set; }

		[StrongDictionary]
		PSPDFGalleryOptions Options { get; set; }
	}

	[Static]
	interface PSPDFGalleryOptionKeys {

		[Field ("PSPDFGalleryOptionAutoplay", PSPDFKitGlobal.LibraryPath)]
		NSString AutoplayKey { get; }

		[Field ("PSPDFGalleryOptionControls", PSPDFKitGlobal.LibraryPath)]
		NSString ControlsKey { get; }

		[Field ("PSPDFGalleryOptionLoop", PSPDFKitGlobal.LibraryPath)]
		NSString LoopKey { get; }

		[Field ("PSPDFGalleryOptionFullscreen", PSPDFKitGlobal.LibraryPath)]
		NSString FullscreenKey { get; }
	}

	[StrongDictionary ("PSPDFGalleryOptionKeys")]
	interface PSPDFGalleryOptions {
		bool Autoplay { get; set; }
		bool Controls { get; set; }
		bool Loop { get; set; }
		bool Fullscreen { get; set; }
	}

	delegate void PSPDFGalleryManifestCompletionHandler ([NullAllowed] PSPDFGalleryItem [] items, [NullAllowed] NSError error);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFGalleryManifest {

		[Export ("initWithLinkAnnotation:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFLinkAnnotation linkAnnotation);

		[Export ("linkAnnotation")]
		PSPDFLinkAnnotation LinkAnnotation { get; }

		[Export ("loadItemsWithCompletionBlock:")]
		void LoadItems ([NullAllowed] PSPDFGalleryManifestCompletionHandler completionHandler);

		[Export ("cancel")]
		void Cancel ();

		[Export ("loading")]
		bool Loading { [Bind ("isLoading")] get; }
	}

	[BaseType (typeof (PSPDFGalleryItem))]
	interface PSPDFGalleryUnknownItem {

	}

	[BaseType (typeof (PSPDFGalleryContentView))]
	interface PSPDFGalleryVideoContentView {

	}

	[BaseType (typeof (PSPDFGalleryItem))]
	interface PSPDFGalleryVideoItem {

		[Export ("autoplayEnabled")]
		bool AutoplayEnabled { get; set; }

		[Export ("loopEnabled")]
		bool LoopEnabled { get; set; }

		[Export ("preferredVideoQualities", ArgumentSemantic.Copy)]
		NSNumber [] PreferredVideoQualities { get; set; }

		[Export ("seekTime")]
		double SeekTime { get; set; }

		[NullAllowed, Export ("startTime", ArgumentSemantic.Strong)]
		NSNumber StartTime { get; set; }

		[NullAllowed, Export ("endTime", ArgumentSemantic.Strong)]
		NSNumber EndTime { get; set; }

		[Export ("playableRange")]
		CMTimeRange PlayableRange { get; }

		[Export ("coverMode", ArgumentSemantic.Assign)]
		PSPDFGalleryVideoItemCoverMode CoverMode { get; set; }

		[NullAllowed, Export ("coverImageURL", ArgumentSemantic.Copy)]
		NSUrl CoverImageUrl { get; set; }

		[NullAllowed, Export ("coverPreviewCaptureTime", ArgumentSemantic.Strong)]
		NSNumber CoverPreviewCaptureTime { get; set; }

		[NullAllowed, Export ("content"), New]
		NSUrl Content { get; }
	}

	[BaseType (typeof (UIScrollView))]
	interface PSPDFGalleryView : IPSPDFOverridable {

		[NullAllowed, Export ("dataSource", ArgumentSemantic.Weak)]
		IPSPDFGalleryViewDataSource DataSource { get; set; }

		[Export ("contentPadding")]
		nfloat ContentPadding { get; set; }

		[NullAllowed, Export ("delegate"), New]
		IPSPDFGalleryViewDelegate Delegate { get; set; }

		[Export ("reload")]
		void Reload ();

		[Export ("contentViewForItemAtIndex:")]
		[return: NullAllowed]
		PSPDFGalleryContentView GetContentView (nuint itemIndex);

		[Export ("itemIndexForContentView:")]
		nuint GetItemIndex (PSPDFGalleryContentView contentView);

		[Export ("dequeueReusableContentViewWithIdentifier:")]
		[return: NullAllowed]
		PSPDFGalleryContentView DequeueReusableContentView (string identifier);

		[Export ("currentItemIndex")]
		nuint CurrentItemIndex { get; set; }

		[Export ("setCurrentItemIndex:animated:")]
		void SetCurrentItemIndex (nuint currentItemIndex, bool animated);

		[Export ("activeContentViews")]
		NSSet<PSPDFGalleryContentView> ActiveContentViews { get; }

		[Export ("loopEnabled")]
		bool LoopEnabled { [Bind ("isLoopEnabled")] get; set; }
	}

	interface IPSPDFGalleryViewDataSource { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryViewDataSource {

		[Abstract]
		[Export ("numberOfItemsInGalleryView:")]
		nuint GetNumberOfItems (PSPDFGalleryView galleryView);

		[Abstract]
		[Export ("galleryView:contentViewForItemAtIndex:")]
		PSPDFGalleryContentView GetContentView (PSPDFGalleryView galleryView, nuint index);
	}

	interface IPSPDFGalleryViewDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryViewDelegate : IUIScrollViewDelegate {

		[Export ("galleryView:willScrollToItemAtIndex:")]
		void WillScrollToItem (PSPDFGalleryView galleryView, nuint index);

		[Export ("galleryView:didScrollToItemAtIndex:")]
		void DidScrollToItem (PSPDFGalleryView galleryView, nuint index);

		[Export ("galleryView:willReuseContentView:")]
		void WillReuseContentView (PSPDFGalleryView galleryView, PSPDFGalleryContentView contentView);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	[DisableDefaultCtor]
	interface PSPDFGalleryViewController : IPSPDFOverridable, PSPDFMultimediaViewController {

		[Export ("initWithLinkAnnotation:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFLinkAnnotation linkAnnotation);

		[Export ("configuration", ArgumentSemantic.Strong)]
		PSPDFGalleryConfiguration Configuration { get; set; }

		[Export ("state")]
		PSPDFGalleryViewControllerState State { get; }

		[NullAllowed, Export ("items", ArgumentSemantic.Copy)]
		PSPDFGalleryItem [] Items { get; }

		[Export ("linkAnnotation")]
		PSPDFLinkAnnotation LinkAnnotation { get; }

		// Inlined from PSPDFMultimediaViewController
		//[Export ("fullscreen")]
		//bool Fullscreen { [Bind ("isFullscreen")] get; set; }

		//[Export ("visible")]
		//bool Visible { [Bind ("isVisible")] get; set; }

		//[Export ("setFullscreen:animated:")]
		//void SetFullscreen (bool fullscreen, bool animated);

		//[Export ("zoomScale")]
		//nfloat ZoomScale { get; set; }

		[Export ("singleTapGestureRecognizer")]
		UITapGestureRecognizer SingleTapGestureRecognizer { get; }

		[Export ("doubleTapGestureRecognizer")]
		UITapGestureRecognizer DoubleTapGestureRecognizer { get; }

		[Export ("panGestureRecognizer")]
		UIPanGestureRecognizer PanGestureRecognizer { get; }
	}

	[BaseType (typeof (PSPDFGalleryContentView))]
	interface PSPDFGalleryWebContentView {

		[NullAllowed, Export ("webView")]
		WKWebView WebView { get; }
	}

	[BaseType (typeof (PSPDFGalleryItem))]
	interface PSPDFGalleryWebItem {

	}

	[BaseType (typeof (PSPDFAnnotationView))]
	interface PSPDFHostingAnnotationView : IPSPDFRenderTaskDelegate, IPSPDFOverridable {

		[NullAllowed, Export ("annotationImageView")]
		UIImageView AnnotationImageView { get; }
	}

	interface IPSPDFImagePickerControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFImagePickerControllerDelegate {

		[Export ("imagePickerController:didSelectImage:")]
		void DidSelectImage (PSPDFImagePickerController picker, UIImage image);

		[Export ("imagePickerController:didFinishWithImage:andInfo:")]
		void DidFinishWithImage (PSPDFImagePickerController picker, UIImage image, NSDictionary info);

		[Export ("imagePickerControllerCancelled:")]
		void ImagePickerControllerCancelled (PSPDFImagePickerController picker);
	}

	[BaseType (typeof (UIImagePickerController))]
	interface PSPDFImagePickerController : IPSPDFOverridable {

		[NullAllowed, Export ("imageDelegate", ArgumentSemantic.Weak)]
		IPSPDFImagePickerControllerDelegate ImageDelegate { get; set; }

		[Export ("shouldShowImageEditor")]
		bool ShouldShowImageEditor { get; set; }

		[Export ("allowedImageQualities", ArgumentSemantic.Assign)]
		PSPDFImageQuality AllowedImageQualities { get; set; }

		// PSPDFImagePickerController (SubclassingHooks)

		[Export ("availableImagePickerSourceTypes")]
		NSNumber[] AvailableImagePickerSourceTypes { get; }
	}

	interface IPSPDFInlineSearchManagerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFInlineSearchManagerDelegate : IPSPDFTextSearchDelegate {

		[Export ("inlineSearchManager:didFocusSearchResult:")]
		void DidFocusSearchResult (PSPDFInlineSearchManager manager, PSPDFSearchResult searchResult);

		[Export ("inlineSearchManagerDidClearAllSearchResults:")]
		void DidClearAllSearchResults (PSPDFInlineSearchManager manager);

		[Export ("inlineSearchManagerSearchWillAppear:")]
		void SearchWillAppear (PSPDFInlineSearchManager manager);

		[Export ("inlineSearchManagerSearchDidAppear:")]
		void SearchDidAppear (PSPDFInlineSearchManager manager);

		[Export ("inlineSearchManagerSearchWillDisappear:")]
		void SearchWillDisappear (PSPDFInlineSearchManager manager);

		[Export ("inlineSearchManagerSearchDidDisappear:")]
		void SearchDidDisappear (PSPDFInlineSearchManager manager);

		[Abstract]
		[Export ("inlineSearchManagerContainerView:")]
		UIView GetContainerView (PSPDFInlineSearchManager manager);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFInlineSearchManager : IPSPDFOverridable {

		[Export ("initWithPresentationContext:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IPSPDFPresentationContext presentationContext);

		[Export ("presentInlineSearchWithSearchText:animated:")]
		void PresentInlineSearch ([NullAllowed] string text, bool animated);

		[Export ("hideInlineSearchAnimated:")]
		bool HideInlineSearch (bool animated);

		[Export ("hideKeyboard")]
		void HideKeyboard ();

		[Export ("searchVisible")]
		bool SearchVisible { [Bind ("isSearchVisible")] get; }

		[NullAllowed, Export ("presentationContext", ArgumentSemantic.Weak)]
		IPSPDFPresentationContext PresentationContext { get; }

		[NullAllowed, Export ("textSearch")]
		PSPDFTextSearch TextSearch { get; }

		[Export ("searchText")]
		string SearchText { get; }

		[Export ("searchResults", ArgumentSemantic.Copy)]
		PSPDFSearchResult [] SearchResults { get; }

		[Export ("searchStatus")]
		PSPDFSearchStatus SearchStatus { get; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFInlineSearchManagerDelegate Delegate { get; set; }

		[NullAllowed, Export ("document", ArgumentSemantic.Strong)]
		PSPDFDocument Document { get; set; }

		[Export ("maximumNumberOfSearchResultsDisplayed")]
		nuint MaximumNumberOfSearchResultsDisplayed { get; set; }

		[Export ("searchableAnnotationTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType SearchableAnnotationTypes { get; set; }

		[Export ("beingPresented")]
		bool BeingPresented { [Bind ("isBeingPresented")] get; }

		[Export ("beingDismissed")]
		bool BeingDismissed { [Bind ("isBeingDismissed")] get; }

		[Export ("searchResultsLabelDistance")]
		nfloat SearchResultsLabelDistance { get; set; }
	}

	[Category]
	[BaseType (typeof (PSPDFKitGlobal))]
	interface PSPDFKit_Analytics {

		[Export ("analytics")]
		PSPDFAnalytics GetAnalytics ();
	}

	[Category]
	[BaseType (typeof (PSPDFKitGlobal))]
	interface PSPDFKit_Services {

		[Export ("application")]
		IPSPDFApplication GetApplication ();

		[Export ("setApplication:")]
		void SetApplication (IPSPDFApplication application);

		[Export ("speechController")]
		PSPDFSpeechController GetSpeechController ();

		[return: NullAllowed]
		[Export ("applePencilManager")]
		PSPDFApplePencilManager GetApplePencilManager ();

		[Export ("screenController")]
		PSPDFScreenController GetScreenController ();

		[Export ("brightnessManager")]
		PSPDFBrightnessManager GetBrightnessManager ();
	}

	[BaseType (typeof (UIView))]
	interface PSPDFLabelView {

		[Export ("label")]
		UILabel Label { get; }

		[Export ("labelMargin")]
		nfloat LabelMargin { get; set; }

		[Export ("labelStyle", ArgumentSemantic.Assign)]
		PSPDFLabelStyle LabelStyle { get; set; }

		[Export ("blurEffectStyle", ArgumentSemantic.Assign)]
		UIBlurEffectStyle BlurEffectStyle { get; set; }

		[Export ("textColor", ArgumentSemantic.Strong)]
		UIColor TextColor { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFLinkAnnotationBaseView : PSPDFAnnotationPresenting {

		[Export ("initWithFrame:")]
		NativeHandle Constructor (CGRect frame);

		[Export ("linkAnnotation")]
		PSPDFLinkAnnotation LinkAnnotation { get; }

		// Inlined from PSPDFAnnotationPresenting
		//[Export ("zIndex")]
		//nuint ZIndex { get; set; }

		[Export ("contentView")]
		UIView ContentView { get; }

		// Inlined from PSPDFAnnotationPresenting
		//[NullAllowed, Export ("pageView", ArgumentSemantic.Weak)]
		//PSPDFPageView PageView { get; set; }

		// PSPDFLinkAnnotationBaseView (SubclassingHooks)

		[Export ("prepareForReuse")]
		[Advice ("Requires base call if override.")]
		void PrepareForReuse ();

		[Export ("populateContentView")]
		void PopulateContentView ();

		[Export ("setContentViewVisible:animated:")]
		void SetContentViewVisible (bool visible, bool animated);

		[Export ("contentViewVisible")]
		bool ContentViewVisible { [Bind ("isContentViewVisible")] get; }
	}

	[BaseType (typeof (PSPDFLinkAnnotationBaseView))]
	interface PSPDFLinkAnnotationView : IPSPDFOverridable {

		[Export ("initWithFrame:")]
		NativeHandle Constructor (CGRect frame);

		[NullAllowed, Export ("borderColor", ArgumentSemantic.Strong)]
		UIColor BorderColor { get; set; }

		[Export ("cornerRadius")]
		nfloat CornerRadius { get; set; }

		[Export ("strokeWidth")]
		nfloat StrokeWidth { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFMediaPlayerController {

		[Field ("PSPDFMediaPlayerControllerPlaybackDidStartNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString PlaybackDidStartNotification { get; }

		[Field ("PSPDFMediaPlayerControllerPlaybackDidPauseNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString PlaybackDidPauseNotification { get; }

		[Field ("PSPDFMediaPlayerControllerPlaybackDidFinishNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString PlaybackDidFinishNotification { get; }

		[Export ("initWithContentURL:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSUrl contentUrl);

		[Export ("contentURL", ArgumentSemantic.Copy)]
		NSUrl ContentUrl { get; }

		[NullAllowed, Export ("contentError")]
		NSError ContentError { get; }

		[Export ("play")]
		void Play ();

		[Export ("pause")]
		void Pause ();

		[Static]
		[Export ("pauseAllInstances")]
		void PauseAllInstances ();

		[Export ("seekToTime:")]
		void Seek (CMTime time);

		[Export ("didFinishPlaying")]
		bool DidFinishPlaying { get; }

		[Export ("playing")]
		bool Playing { [Bind ("isPlaying")] get; }

		[Export ("externalPlaybackActive")]
		bool ExternalPlaybackActive { [Bind ("isExternalPlaybackActive")] get; }

		[Export ("contentState")]
		PSPDFMediaPlayerControllerContentState ContentState { get; }

		[Export ("coverMode", ArgumentSemantic.Assign)]
		PSPDFMediaPlayerCoverMode CoverMode { get; set; }

		[NullAllowed, Export ("coverImageURL", ArgumentSemantic.Strong)]
		NSUrl CoverImageUrl { get; set; }

		[Export ("coverImagePreviewCaptureTime", ArgumentSemantic.Assign)]
		CMTime CoverImagePreviewCaptureTime { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFMediaPlayerControllerDelegate Delegate { get; set; }

		[Export ("shouldHideToolbar")]
		bool ShouldHideToolbar { get; set; }

		[Export ("setShouldHideToolbar:animated:")]
		void SetShouldHideToolbar (bool shouldHideToolbar, bool animated);

		[Export ("didStartPlaying")]
		bool DidStartPlaying { get; }

		[Export ("tapGestureRecognizer")]
		UITapGestureRecognizer TapGestureRecognizer { get; }

		[Export ("loopEnabled")]
		bool LoopEnabled { get; set; }

		[Export ("usesExternalPlaybackWhileExternalScreenIsActive")]
		bool UsesExternalPlaybackWhileExternalScreenIsActive { get; set; }

		[Export ("controlStyle", ArgumentSemantic.Assign)]
		PSPDFMediaPlayerControlStyle ControlStyle { get; set; }

		[Export ("playableRange", ArgumentSemantic.Assign)]
		CMTimeRange PlayableRange { get; set; }

		// PSPDFMediaPlayerController (Advanced) Category

		[NullAllowed, Export ("internalPlayer")]
		AVPlayer InternalPlayer { get; }
	}

	interface IPSPDFMediaPlayerControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFMediaPlayerControllerDelegate {

		[Export ("mediaPlayerControllerShouldPauseOtherInstances:")]
		bool ShouldPauseOtherInstances (PSPDFMediaPlayerController controller);

		[Export ("mediaPlayerControllerDidStartPlaying:")]
		void DidStartPlaying (PSPDFMediaPlayerController controller);

		[Export ("mediaPlayerControllerDidPause:")]
		void DidPause (PSPDFMediaPlayerController controller);

		[Export ("mediaPlayerControllerDidFinishPlaying:")]
		void DidFinishPlaying (PSPDFMediaPlayerController controller);

		[Export ("mediaPlayerController:didSeekToTime:")]
		void DidSeekToTime (PSPDFMediaPlayerController controller, CMTime seekTime);

		[Export ("mediaPlayerController:didHideToolbar:")]
		void DidHideToolbar (PSPDFMediaPlayerController controller, bool hidden);

		[Export ("mediaPlayerController:contentStateDidChange:")]
		void ContentStateDidChange (PSPDFMediaPlayerController controller, PSPDFMediaPlayerControllerContentState contentState);

		[Export ("mediaPlayerController:externalPlaybackActiveDidChange:")]
		void ExternalPlaybackActiveDidChange (PSPDFMediaPlayerController controller, bool externalPlaybackActive);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFMediaPlayerCoverView {

		[NullAllowed, Export ("playButtonColor", ArgumentSemantic.Strong)]
		UIColor PlayButtonColor { get; set; }

		[NullAllowed, Export ("playButtonImage", ArgumentSemantic.Strong)]
		UIImage PlayButtonImage { get; set; }
	}

	[Obsolete ("Use the modern menu system instead.")]
	[BaseType (typeof (UIMenuItem))]
	interface PSPDFMenuItem {

		[Export ("initWithTitle:block:")]
		NativeHandle Constructor (string title, Action action);

		[Export ("initWithTitle:block:identifier:")]
		NativeHandle Constructor (string title, Action action, [NullAllowed] string identifier);

		[Export ("initWithTitle:image:block:identifier:")]
		NativeHandle Constructor (string title, [NullAllowed] UIImage image, Action action, [NullAllowed] string identifier);

		[Export ("initWithTitle:image:block:identifier:allowImageColors:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string title, [NullAllowed] UIImage image, Action action, [NullAllowed] string identifier, bool allowImageColors);

		[Export ("enabled")]
		bool Enabled { [Bind ("isEnabled")] get; set; }

		[Export ("shouldInvokeAutomatically")]
		bool ShouldInvokeAutomatically { get; set; }

		[NullAllowed, Export ("identifier")]
		string Identifier { get; }

		[NullAllowed, Export ("ps_image", ArgumentSemantic.Strong)]
		UIImage PsImage { get; set; }

		[Export ("actionBlock", ArgumentSemantic.Copy)]
		Action ActionHandler { get; set; }

		[Static]
		[Export ("installMenuHandlerForObject:")]
		void InstallMenuHandler (NSObject @object);

		// PSPDFMenuItem (PSPDFAnalytics) Category

		[Export ("performBlock")]
		void PerformHandler ();
	}

	interface IPSPDFMultiDocumentListControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFMultiDocumentListControllerDelegate {

		[Export ("multiDocumentListController:didSelectDocumentAtIndex:")]
		void DidSelectDocument (PSPDFMultiDocumentListController multiDocumentListController, nuint idx);

		[Export ("multiDocumentListControllerDidCancel:")]
		void DidCancel (PSPDFMultiDocumentListController multiDocumentListController);
	}

	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFMultiDocumentListController {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFMultiDocumentListControllerDelegate Delegate { get; set; }

		[NullAllowed, Export ("tabbedViewController", ArgumentSemantic.Weak)]
		PSPDFTabbedViewController TabbedViewController { get; set; }
	}

	interface IPSPDFMultiDocumentViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFMultiDocumentViewControllerDelegate {

		[Export ("multiPDFController:willChangeDocuments:")]
		void WillChangeDocuments (PSPDFMultiDocumentViewController multiPdfController, PSPDFDocument [] newDocuments);

		[Export ("multiPDFController:didChangeDocuments:")]
		void DidChangeDocuments (PSPDFMultiDocumentViewController multiPdfController, PSPDFDocument [] oldDocuments);

		[Export ("multiPDFController:willChangeVisibleDocument:")]
		void WillChangeVisibleDocument (PSPDFMultiDocumentViewController multiPdfController, [NullAllowed] PSPDFDocument newDocument);

		[Export ("multiPDFController:didChangeVisibleDocument:")]
		void DidChangeVisibleDocument (PSPDFMultiDocumentViewController multiPdfController, [NullAllowed] PSPDFDocument oldDocument);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFMultiDocumentViewController : PSPDFConflictResolutionManagerDelegate {

		[Export ("initWithPDFViewController:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] PSPDFViewController pdfController);

		[NullAllowed, Export ("visibleDocument", ArgumentSemantic.Strong)]
		PSPDFDocument VisibleDocument { get; set; }

		[Export ("documents", ArgumentSemantic.Copy)]
		PSPDFDocument [] Documents { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFMultiDocumentViewControllerDelegate Delegate { get; set; }

		[Export ("enableAutomaticStatePersistence")]
		bool EnableAutomaticStatePersistence { get; set; }

		[Export ("persistState")]
		void PersistState ();

		[Export ("restoreState")]
		bool RestoreState { get; }

		[Export ("restoreStateAndMergeWithDocuments:")]
		bool RestoreStateAndMerge (PSPDFDocument [] documents);

		[Export ("statePersistenceKey")]
		string StatePersistenceKey { get; set; }

		[Export ("pdfController")]
		PSPDFViewController PdfController { get; }

		[Export ("thumbnailViewIncludesAllDocuments")]
		bool ThumbnailViewIncludesAllDocuments { get; set; }

		[Export ("showTitle")]
		bool ShowTitle { get; set; }

		// PSPDFMultiDocumentViewController (SubclassingHooks) Category

		[Export ("commonInitWithPDFController:")]
		[Advice ("Requires base call if override.")]
		void CommonInit ([NullAllowed] PSPDFViewController pdfController);

		[Export ("titleForDocumentAtIndex:")]
		string GetTitleForDocument (nuint idx);

		[Export ("titleDidChangeForDocumentAtIndex:")]
		void TitleDidChangeForDocument (nuint idx);

		[Export ("persistViewStateForCurrentVisibleDocument")]
		void PersistViewStateForCurrentVisibleDocument ();

		[Export ("restoreViewStateForCurrentVisibleDocument")]
		void RestoreViewStateForCurrentVisibleDocument ();
	}

	[BaseType (typeof (PSPDFLinkAnnotationBaseView))]
	interface PSPDFMultimediaAnnotationView : IPSPDFOverridable {

		[Export ("initWithFrame:")]
		NativeHandle Constructor (CGRect frame);

		[Export ("multimediaViewController")]
		IPSPDFMultimediaViewController MultimediaViewController { get; }
	}

	interface IPSPDFMultimediaViewController { }

	[Protocol]
	interface PSPDFMultimediaViewController {

		[Abstract]
		[Export ("fullscreen")]
		bool Fullscreen { [Bind ("isFullscreen")] get; set; }

		[Abstract]
		[Export ("visible")]
		bool Visible { [Bind ("isVisible")] get; set; }

		[Abstract]
		[Export ("setFullscreen:animated:")]
		void SetFullscreen (bool fullscreen, bool animated);

		[Abstract]
		[Export ("zoomScale")]
		nfloat ZoomScale { get; set; }

		[Abstract]
		[NullAllowed, Export ("overrideDelegate", ArgumentSemantic.Weak)]
		NSObject OverrideDelegate { get; set; }

		[Abstract]
		[Export ("performAction:")]
		void PerformAction (PSPDFAction action);

		[Export ("configure:")]
		void Configure (PSPDFConfiguration configuration);
	}

	[BaseType (typeof (UINavigationController))]
	interface PSPDFNavigationController : IUINavigationControllerDelegate {

		[Export ("rotationForwardingEnabled")]
		bool RotationForwardingEnabled { [Bind ("isRotationForwardingEnabled")] get; set; }
	}

	[BaseType (typeof (UINavigationItem))]
	interface PSPDFNavigationItem {

		[Export ("leftBarButtonItemsForViewMode:")]
		[return: NullAllowed]
		UIBarButtonItem [] LeftBarButtonItemsForViewMode (PSPDFViewMode viewMode);

		[Export ("setLeftBarButtonItems:forViewMode:animated:")]
		void SetLeftBarButtonItems (UIBarButtonItem [] barButtonItems, PSPDFViewMode viewMode, bool animated);

		[Export ("setLeftBarButtonItems:animated:"), New]
		void SetLeftBarButtonItems ([NullAllowed] UIBarButtonItem [] items, bool animated);

		[Export ("rightBarButtonItemsForViewMode:")]
		[return: NullAllowed]
		UIBarButtonItem [] GetRightBarButtonItems (PSPDFViewMode viewMode);

		[Export ("setRightBarButtonItems:forViewMode:animated:")]
		void SetRightBarButtonItems (UIBarButtonItem [] barButtonItems, PSPDFViewMode viewMode, bool animated);

		[Export ("setRightBarButtonItems:animated:"), New]
		void SetRightBarButtonItems ([NullAllowed] UIBarButtonItem [] items, bool animated);

		[NullAllowed, Export ("closeBarButtonItem", ArgumentSemantic.Assign)]
		UIBarButtonItem CloseBarButtonItem { get; set; }

		[Export ("titleForViewMode:")]
		[return: NullAllowed]
		string GetTitle (PSPDFViewMode viewMode);

		[Export ("setTitle:forViewMode:")]
		void SetTitle ([NullAllowed] string title, PSPDFViewMode viewMode);

		[Export ("leftItemsSupplementBackButtonForViewMode:")]
		bool GetLeftItemsSupplementBackButton (PSPDFViewMode viewMode);

		[Export ("setLeftItemsSupplementBackButton:forViewMode:")]
		void SetLeftItemsSupplementBackButton (bool leftItemsSupplementBackButton, PSPDFViewMode viewMode);
	}

	interface IPSPDFNetworkActivityIndicatorManager { }

	[Protocol]
	interface PSPDFNetworkActivityIndicatorManager {

		[Abstract]
		[Export ("enabled")]
		bool Enabled { [Bind ("isEnabled")] get; set; }

		[Abstract]
		[Export ("isNetworkActivityIndicatorVisible")]
		bool IsNetworkActivityIndicatorVisible { get; }

		[Abstract]
		[Export ("incrementActivityCount")]
		void IncrementActivityCount ();

		[Abstract]
		[Export ("decrementActivityCount")]
		void DecrementActivityCount ();
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDefaultNetworkActivityIndicatorManager : PSPDFNetworkActivityIndicatorManager {

		[Field ("PSPDFNetworkActivityDidStartNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString ActivityDidStartNotification { get; }

		[Field ("PSPDFNetworkActivityDidFinishNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString ActivityDidFinishNotification { get; }
	}

	interface IPSPDFNewPageViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFNewPageViewControllerDelegate {

		[Abstract]
		[Export ("newPageController:didFinishSelectingConfiguration:pageCount:")]
		void DidFinishSelectingConfiguration (PSPDFNewPageViewController controller, [NullAllowed] PSPDFNewPageConfiguration configuration, nuint pageCount);
	}

	interface IPSPDFNewPageViewControllerDataSource { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFNewPageViewControllerDataSource {

		[Export ("selectedIndexForNewPageViewController:")]
		nuint GetSelectedIndex (PSPDFNewPageViewController controller);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	[DisableDefaultCtor]
	interface PSPDFNewPageViewController : IPSPDFDocumentEditorConfigurationConfigurable, IPSPDFOverridable {

		[Export ("initWithDocumentEditorConfiguration:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocumentEditorConfiguration configuration);

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFNewPageViewControllerDelegate Delegate { get; set; }

		[NullAllowed, Export ("dataSource", ArgumentSemantic.Weak)]
		IPSPDFNewPageViewControllerDataSource DataSource { get; set; }

		[Export ("reloadData")]
		void ReloadData ();

		[Export ("documentEditorConfiguration")]
		new PSPDFDocumentEditorConfiguration DocumentEditorConfiguration { get; }
	}

	[BaseType (typeof (PSPDFAnnotationView))]
	[DisableDefaultCtor]
	interface PSPDFNoteAnnotationView : IPSPDFOverridable {

		[Export ("initWithAnnotation:")]
		NativeHandle Constructor (PSPDFAnnotation noteAnnotation);

		[NullAllowed, Export ("annotationImageView", ArgumentSemantic.Strong)]
		UIImageView AnnotationImageView { get; set; }

		// PSPDFNoteAnnotationView (SubclassingHooks)

		[NullAllowed, Export ("renderNoteImage")]
		UIImage RenderNoteImage { get; }

		[Export ("updateImageAnimated:")]
		IntPtr UpdateImage (bool animated);
	}

	interface IPSPDFNoteAnnotationViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFNoteAnnotationViewControllerDelegate {

		[Export ("noteAnnotationController:didDeleteAnnotation:")]
		void DidDeleteAnnotation (PSPDFNoteAnnotationViewController noteController, PSPDFAnnotation annotation);

		[Export ("noteAnnotationController:didClearContentsForAnnotation:")]
		void DidClearContentsForAnnotation (PSPDFNoteAnnotationViewController noteController, PSPDFAnnotation annotation);

		[Export ("noteAnnotationController:didChangeAnnotation:")]
		void DidChangeAnnotation (PSPDFNoteAnnotationViewController noteController, PSPDFAnnotation annotation);

		[Export ("noteAnnotationController:willDismissWithAnnotation:")]
		void WillDismissWithAnnotation (PSPDFNoteAnnotationViewController noteController, [NullAllowed] PSPDFAnnotation annotation);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFNoteAnnotationViewController : IUITextViewDelegate, PSPDFStyleable, IPSPDFOverridable {

		[Export ("initWithAnnotation:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] PSPDFAnnotation annotation);

		[NullAllowed, Export ("annotation", ArgumentSemantic.Strong)]
		PSPDFAnnotation Annotation { get; set; }

		[NullAllowed, Export ("commentBackgroundColor", ArgumentSemantic.Strong)]
		UIColor CommentBackgroundColor { get; set; }

		[Export ("showsTimestamps")]
		bool ShowsTimestamps { get; set; }

		[Export ("showsAuthorName")]
		bool ShowsAuthorName { get; set; }

		[Export ("showColorAndIconOptions")]
		bool ShowColorAndIconOptions { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFNoteAnnotationViewControllerDelegate Delegate { get; set; }

		// PSPDFNoteAnnotationViewController (SubclassingHooks)

		[Export ("updateTextView:")]
		void UpdateTextView (UITextView textView);

		[Export ("backgroundView")]
		UIView BackgroundView { get; }

		[Export ("setupToolbar")]
		void SetupToolbar ();

		[Export ("updateToolbar")]
		void UpdateToolbar ();
	}

	interface IPSPDFOutlineCellDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFOutlineCellDelegate {

		[Abstract]
		[Export ("outlineCellDidTapDisclosureButton:")]
		void DidTapDisclosureButton (PSPDFOutlineCell outlineCell);
	}

	[BaseType (typeof (PSPDFTableViewCell))]
	interface PSPDFOutlineCell : IPSPDFOverridable {

		[Export ("configureWithOutlineElement:documentProvider:")]
		void Configure (PSPDFOutlineElement outlineElement, [NullAllowed] PSPDFDocumentProvider documentProvider);

		[NullAllowed, Export ("outlineElement")]
		PSPDFOutlineElement OutlineElement { get; }

		[NullAllowed, Export ("pageLabelString")]
		string PageLabelString { get; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFOutlineCellDelegate Delegate { get; set; }

		[Export ("showExpandCollapseButton")]
		bool ShowExpandCollapseButton { get; set; }

		[Export ("showPageLabel")]
		bool ShowPageLabel { get; set; }

		// PSPDFOutlineCell (SubclassingHooks) Category

		[Export ("disclosureButton", ArgumentSemantic.Strong)]
		UIButton DisclosureButton { get; set; }

		[Export ("nameLabel", ArgumentSemantic.Strong)]
		UILabel NameLabel { get; set; }

		[Export ("pageLabel", ArgumentSemantic.Strong)]
		UILabel PageLabel { get; set; }

		[Static]
		[Export ("fontForOutlineElement:")]
		UIFont GetFontForOutlineElement ([NullAllowed] PSPDFOutlineElement outlineElement);

		[Static]
		[Export ("pageLabelFontForOutlineElement:")]
		UIFont GetPageLabelFont ([NullAllowed] PSPDFOutlineElement outlineElement);

		[Export ("updateDisclosureButton")]
		void UpdateDisclosureButton ();

		[Export ("expandOrCollapse")]
		void ExpandOrCollapse ();

		[Export ("maximumNumberOfLines")]
		nuint MaximumNumberOfLines { get; set; }

		[Export ("outlineIndentLeftOffset")]
		nfloat OutlineIndentLeftOffset { get; set; }

		[Export ("outlineIndentMultiplier")]
		nfloat OutlineIndentMultiplier { get; set; }
	}

	interface IPSPDFOutlineViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFOutlineViewControllerDelegate {

		[Abstract]
		[Export ("outlineController:didTapAtElement:")]
		bool DidTapAtElement (PSPDFOutlineViewController outlineController, PSPDFOutlineElement outlineElement);
	}

	[BaseType (typeof (PSPDFSearchableTableViewController))]
	interface PSPDFOutlineViewController : PSPDFDocumentInfoController,

#if !__MACCATALYST__
		IUISearchDisplayDelegate,
#endif
		PSPDFStyleable, IPSPDFOverridable {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFOutlineViewControllerDelegate Delegate { get; set; }

		[Export ("allowCopy")]
		bool AllowCopy { get; set; }

		[Export ("showPageLabels")]
		bool ShowPageLabels { get; set; }

		[Export ("maximumNumberOfLines")]
		nuint MaximumNumberOfLines { get; set; }

		[Export ("outlineIndentLeftOffset")]
		nfloat OutlineIndentLeftOffset { get; set; }

		[Export ("outlineIndentMultiplier")]
		nfloat OutlineIndentMultiplier { get; set; }

		// PSPDFOutlineViewController (SubclassingHooks) Category

		[Export ("shouldExpandCollapseOnRowSelection")]
		bool ShouldExpandCollapseOnRowSelection { get; }

		[Export ("outlineCellDidTapDisclosureButton:")]
		bool DidTapDisclosureButton (PSPDFOutlineCell searchController);

		[Export ("searchController")]
		UISearchController SearchController { get; }
	}

	interface IPSPDFPageCellImageRequestToken { }

	[Protocol]
	interface PSPDFPageCellImageRequestToken {

		[Abstract]
		[Export ("expectedSize")]
		CGSize ExpectedSize { get; }

		[return: NullAllowed]
		[Export ("placeholderImage")]
		UIImage GetPlaceholderImage ();

		[Export ("cancel")]
		void Cancel ();
	}

	interface IPSPDFPageCellImageLoading { }

	[Protocol]
	interface PSPDFPageCellImageLoading {

		[Abstract]
		[Export ("requestImageForPageAtIndex:availableSize:completionHandler:")]
		IPSPDFPageCellImageRequestToken RequestImageForPage (nuint pageIndex, CGSize size, Action<UIImage, NSError> completionHandler);
	}

	[BaseType (typeof (UICollectionViewCell))]
	interface PSPDFPageCell {

		[Export ("pageIndex")]
		nuint PageIndex { get; set; }

		[Export ("edgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets EdgeInsets { get; set; }

		[Export ("shadowEnabled")]
		bool ShadowEnabled { [Bind ("isShadowEnabled")] get; set; }

		[Export ("pageLabelEnabled")]
		bool PageLabelEnabled { [Bind ("isPageLabelEnabled")] get; set; }

		[Export ("setNeedsUpdateImage")]
		void SetNeedsUpdateImage ();

		[NullAllowed, Export ("imageLoader", ArgumentSemantic.Strong)]
		IPSPDFPageCellImageLoading ImageLoader { get; set; }

		// PSPDFPageCell (Subviews) Category

		[Export ("pageLabel")]
		UILabel PageLabel { get; }

		[Export ("imageView")]
		UIImageView ImageView { get; }

		// PSPDFPageCell (SubclassingHooks) Category

		[Export ("updatePageLabel")]
		void UpdatePageLabel ();

		[NullAllowed, Export ("image")]
		UIImage Image { get; }

		[return: NullAllowed]
		[Export ("pathShadowForView:")]
		UIBezierPath GetPathShadow (UIView view);

		[Export ("contentRectForBounds:")]
		CGRect GetContentRect (CGRect bounds);

		[Export ("imageRectForContentRect:")]
		CGRect GetImageRect (CGRect contentRect);
	}

	interface IPSPDFPageLabelViewDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFPageLabelViewDelegate {

		[Abstract]
		[Export ("pageLabelView:didPressThumbnailGridButton:")]
		void DidPressThumbnailGridButton (PSPDFPageLabelView pageLabelView, UIButton sender);
	}

	[BaseType (typeof (PSPDFLabelView))]
	interface PSPDFPageLabelView : IPSPDFOverridable {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFPageLabelViewDelegate Delegate { get; set; }

		[Export ("showThumbnailGridButton")]
		bool ShowThumbnailGridButton { get; set; }

		[Export ("thumbnailGridButton", ArgumentSemantic.Strong)]
		UIButton ThumbnailGridButton { get; set; }

		[Export ("thumbnailButtonColor", ArgumentSemantic.Strong)]
		UIColor ThumbnailButtonColor { get; set; }

		[NullAllowed, Export ("labelFormatter", ArgumentSemantic.Copy)]
		PSPDFPageLabelFormatter LabelFormatter { get; set; }
	}

	[BaseType (typeof (PSPDFRelayTouchesView))]
	interface PSPDFAnnotationContainerView : IPSPDFOverridable {

	}

	interface PSPDFAnnotationCreateActionDidInsertNotificationEventArgs {

		[Obsolete]
		[Export ("PSPDFAnnotationCreateActionInsertedAnnotationsKey")]
		PSPDFAnnotation [] InsertedAnnotations { get; }
	}

	[BaseType (typeof (UIView))]
	[DisableDefaultCtor]
	interface PSPDFPageView : IPSPDFRenderTaskDelegate, PSPDFResizableViewDelegate, IPSPDFOverridable, PSPDFAnnotationStyleViewControllerDelegate, PSPDFFontPickerViewControllerDelegate, PSPDFLinkAnnotationEditingContainerViewControllerDelegate, PSPDFNoteAnnotationViewControllerDelegate, PSPDFSignedFormElementViewControllerDelegate, PSPDFTextSelectionViewDelegate {

		[Field ("PSPDFPageViewSelectedAnnotationsDidChangeNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString SelectedAnnotationsDidChangeNotification { get; }

		[Obsolete]
		[Field ("PSPDFAnnotationCreateActionDidInsertNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFAnnotationCreateActionDidInsertNotificationEventArgs))]
		NSString CreateActionDidInsertNotification { get; }

		[NullAllowed, Export ("presentationContext", ArgumentSemantic.Weak)]
		IPSPDFPresentationContext PresentationContext { get; }

		[Export ("prepareForReuse")]
		[Advice ("Requires base call if override.")]
		void PrepareForReuse ();

		[Advice ("Requires base call if override.")]
		[Export ("updateRenderView")]
		void UpdateRenderView ();

		[Advice ("Requires base call if override.")]
		[Export ("updateView")]
		void UpdateView ();

		[Export ("updateAnnotationViewsAnimated:")]
		void UpdateAnnotationViews (bool animated);

		[Export ("annotationViewForAnnotation:")]
		[return: NullAllowed]
		IPSPDFAnnotationPresenting GetAnnotationView (PSPDFAnnotation annotation);

		[Export ("contentView")]
		UIImageView ContentView { get; }

		[Export ("renderView")]
		UIImageView RenderView { get; }

		[Export ("annotationContainerView")]
		PSPDFAnnotationContainerView AnnotationContainerView { get; }

		[Export ("renderStatusView", ArgumentSemantic.Strong)]
		NSObject /* PSPDFRenderStatusView */ RenderStatusView { get; set; }

		[Export ("renderStatusViewOffset")]
		nfloat RenderStatusViewOffset { get; set; }

		[Export ("PDFScale")]
		nfloat PdfScale { get; }

		[Export ("visibleRect")]
		CGRect VisibleRect { get; }

		[Export ("highlightColor", ArgumentSemantic.Strong)]
		UIColor HighlightColor { get; set; }

		[Export ("placeholderColor", ArgumentSemantic.Strong)]
		UIColor PlaceholderColor { get; set; }

		[Export ("pdfCoordinateSpace")]
		 IUICoordinateSpace PdfCoordinateSpace { get; }

		[Advice ("'dictOptions' parameter comes from 'PSPDFObjectFinderOptions', use overload for a strongly typed dictionary.")]
		[Export ("objectsAtPoint:options:")]
		NSDictionary GetObjects (CGPoint viewPoint, [NullAllowed] NSDictionary dictOptions);

		[Wrap ("new global::PSPDFKit.Model.PSPDFObjectFinderType (GetObjects (viewPoint, options?.Dictionary))")]
		global::PSPDFKit.Model.PSPDFObjectFinderType GetObjects (CGPoint viewPoint, [NullAllowed] PSPDFObjectFinderOptions options);

		[Advice ("'dictOptions' parameter comes from 'PSPDFObjectFinderOptions', use overload for a strongly typed dictionary.")]
		[Export ("objectsAtRect:options:")]
		NSDictionary GetObjects (CGRect viewRect, [NullAllowed] NSDictionary dictOptions);

		[Wrap ("new global::PSPDFKit.Model.PSPDFObjectFinderType (GetObjects (viewRect, options?.Dictionary))")]
		global::PSPDFKit.Model.PSPDFObjectFinderType GetObjects (CGRect viewRect, [NullAllowed] PSPDFObjectFinderOptions options);

		[NullAllowed, Export ("zoomView")]
		UIScrollView ZoomView { get; }

		[Export ("visibleAnnotationViews")]
		IPSPDFAnnotationPresenting [] VisibleAnnotationViews { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[NullAllowed, Export ("pageInfo")]
		PSPDFPageInfo PageInfo { get; }

		[NullAllowed, Export ("annotationSelectionView")]
		PSPDFResizableView AnnotationSelectionView { get; }

		[Export ("selectedAnnotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] SelectedAnnotations { get; set; }

		[Export ("selectAnnotations:presentMenu:animated:")]
		void SelectAnnotations (PSPDFAnnotation[] annotations, bool presentMenu, bool animated);

		[Export ("focusFormElement:toggleValue:animated:")]
		void FocusFormElement (PSPDFFormElement formElement, bool toggleValue, bool animated);

		[Export ("selectionView")]
		PSPDFTextSelectionView SelectionView { get; }

		[Export ("selectGlyphs:presentMenu:animated:")]
		void SelectGlyphs (PSPDFGlyph [] glyphs, bool presentMenu, bool animated);

		[Export ("selectImage:presentMenu:animated:")]
		void SelectImage (PSPDFImageInfo image, bool presentMenu, bool animated);

		[Export ("discardSelectionAnimated:")]
		void DiscardSelection (bool animated);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'select(annotations:presentMenu:animated:)' or 'focus(formElement:toggleValue:animated:)' instead.")]
		[Export ("selectAnnotation:animated:")]
		void SelectAnnotation (PSPDFAnnotation annotation, bool animated);

		// PSPDFPageView (AnnotationViews) Category

		[Export ("setAnnotation:forAnnotationView:")]
		void SetAnnotation (PSPDFAnnotation annotation, IPSPDFAnnotationPresenting annotationView);

		[Export ("annotationForAnnotationView:")]
		[return: NullAllowed]
		PSPDFAnnotation GetAnnotation (IPSPDFAnnotationPresenting annotationView);

		[Export ("addAnnotation:options:animated:")]
		void AddAnnotation (PSPDFAnnotation annotation, [NullAllowed] NSDictionary<NSString, NSNumber> options, bool animated);

		[Export ("removeAnnotation:options:animated:")]
		bool RemoveAnnotation (PSPDFAnnotation annotation, [NullAllowed] NSDictionary<NSString, NSNumber> options, bool animated);

		// PSPDFPageView (SubclassingHooks) Category

		[Export ("updateShadowAnimated:")]
		void UpdateShadow (bool animated);

		[Export ("hitTestRectForPoint:")]
		CGRect GetHitTestRect (CGPoint viewPoint);

		[Export ("didSelectAnnotations:")]
		[Advice ("Requires base call if override.")]
		void DidSelectAnnotations ([NullAllowed] PSPDFAnnotation [] annotations);

		[Export ("didDeselectAnnotations:")]
		[Advice ("Requires base call if override.")]
		void DidDeselectAnnotations (PSPDFAnnotation [] annotations);

		[Export ("rectForAnnotations:")]
		CGRect GetRectForAnnotations (PSPDFAnnotation [] annotations);

		[Export ("renderOptionsWithZoomScale:animated:")]
		PSPDFRenderOptions GetRenderOptions (nfloat zoomScale, bool animated);

		[Export ("centerAnnotation:aroundPDFPoint:")]
		void CenterAnnotation (PSPDFAnnotation annotation, CGPoint pdfPoint);

		[Export ("loadPageAnnotationsAnimated:blockWhileParsing:")]
		void LoadPageAnnotations (bool animated, bool blockWhileParsing);

		[Export ("scaleForPageView")]
		nfloat ScaleForPageView { get; }

		[Export ("annotationsAddedNotification:")]
		[Advice ("Requires base call if override.")]
		void AnnotationsAddedNotification (NSNotification notification);

		[Export ("annotationsRemovedNotification:")]
		[Advice ("Requires base call if override.")]
		void AnnotationsRemovedNotification (NSNotification notification);

		[Export ("annotationChangedNotification:")]
		[Advice ("Requires base call if override.")]
		void AnnotationChangedNotification (NSNotification notification);

		[Export ("shouldScaleAnnotationWhenResizing:usesResizeKnob:")]
		bool ShouldScaleAnnotationWhenResizing (PSPDFAnnotation annotation, bool usesResizeKnob);

		[Export ("updateAnnotationSelectionView")]
		void UpdateAnnotationSelectionView ();

		// PSPDFPageView (AnnotationMenu) Category

		[Obsolete]
		[Export ("textSelectionMenuItemForCreatingAnnotationWithType:")]
		[return: NullAllowed]
		PSPDFMenuItem GetTextSelectionMenuItemForCreatingAnnotation ([BindAs (typeof (PSPDFAnnotationStringUI))] NSString annotationString);

		[Export ("showSignatureControllerAtRect:withTitle:signatureFormElement:options:animated:")]
		void ShowSignatureController (CGRect viewRect, [NullAllowed] string title, [NullAllowed] PSPDFSignatureFormElement signatureFormElement, [NullAllowed] NSDictionary options, bool animated);

		[Export ("showNewSignatureMenuAtRect:signatureFormElement:options:animated:")]
		void ShowNewSignatureMenu (CGRect viewRect, [NullAllowed] PSPDFSignatureFormElement signatureFormElement, [NullAllowed] NSDictionary options, bool animated);

		[Export ("showDigitalSignatureMenuForSignatureField:animated:")]
		bool ShowDigitalSignatureMenu (PSPDFSignatureFormElement signatureField, bool animated);

		[Obsolete]
		[Export ("showMenuIfSelectedAnimated:")]
		void ShowMenuIfSelected (bool animated);

		[Obsolete]
		[Export ("showMenuIfSelectedWithOption:animated:")]
		void ShowMenuIfSelected (PSPDFContextMenuOption contextMenuOption, bool animated);

		[Obsolete]
		[Export ("showMenuForPoint:animated:")]
		void ShowMenuForPoint (CGPoint location, bool animated);

		[Export ("canCreateAnnotationsShowMessage:")]
		bool CanCreateAnnotations (bool showMessage);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'PDFViewController.Interactions.TryToShowAnnotationMenu' instead.")]
		[Export ("showAnnotationMenuAtPoint:animated:")]
		bool ShowAnnotationMenuAtPoint (CGPoint viewPoint, bool animated);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'select(annotations:presentMenu:animated:' instead.")]
		[Export ("showMenuForAnnotations:targetRect:option:animated:")]
		void ShowMenu (PSPDFAnnotation [] annotations, CGRect targetRect, PSPDFContextMenuOption contextMenuOption, bool animated);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'presentColorPicker(for:property:options:animated:completion:)' instead.")]
		[Export ("selectColorForAnnotation:isFillColor:")]
		void SelectColor (PSPDFAnnotation annotation, bool isFillColor);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'canPresentInspector(for:)' instead.")]
		[Export ("useAnnotationInspectorForAnnotations:")]
		bool UseAnnotationInspector (PSPDFAnnotation [] annotations);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'presentInspector(for:options:animated:completion:)' instead.")]
		[Export ("showInspectorForAnnotations:options:animated:")]
		[return: NullAllowed]
		PSPDFAnnotationStyleViewController ShowInspector (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options, bool animated);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'presentComments(for:options:animated:completion:)' instead.")]
		[Export ("showNoteControllerForAnnotation:animated:")]
		void ShowNoteController (PSPDFAnnotation annotation, bool animated);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'presentColorPicker(for:property:options:animated:completion:)' instead.")]
		[Export ("showColorPickerForAnnotation:animated:")]
		void ShowColorPicker (PSPDFAnnotation annotation, bool animated);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'presentFontPicker(for:options:animated:completion:)' instead.")]
		[Export ("showFontPickerForAnnotation:animated:")]
		void ShowFontPicker (PSPDFFreeTextAnnotation annotation, bool animated);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'presentLinkActionSheet(for:options:animated:completion:)' instead.")]
		[Export ("showLinkPreviewActionSheetForAnnotation:fromRect:animated:")]
		bool ShowLinkPreviewActionSheet (PSPDFLinkAnnotation annotation, CGRect viewRect, bool animated);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'PDFConfiguration.CreateAnnotationMenuGroups' or 'PDFViewControllerDelegate.PdfViewController(_:menuForCreatingAnnotationAt:onPageView:appearance:suggestedMenu:)' instead.")]
		[Export ("menuItemsForNewAnnotationAtPoint:")]
		PSPDFMenuItem [] GetMenuItemsForNewAnnotation (CGPoint point);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'PDFViewControllerDelegate.PdfViewController(_:menuForAnnotations:onPageView:appearance:suggestedMenu:)' instead.")]
		[Export ("menuItemsForAnnotations:")]
		PSPDFMenuItem [] GetMenuItemsForAnnotations (PSPDFAnnotation [] annotations);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'PDFConfiguration.annotationMenuConfiguration.colorChoices' or 'PDFViewControllerDelegate.pdfViewController(_:menuForAnnotations:onPageView:appearance:suggestedMenu:)' instead.")]
		[Export ("colorMenuItemsForAnnotation:")]
		PSPDFMenuItem [] GetColorMenuItems (PSPDFAnnotation annotation);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'PDFConfiguration.annotationMenuConfiguration.colorChoices' or 'PDFViewControllerDelegate.pdfViewController(_:menuForAnnotations:onPageView:appearance:suggestedMenu:)' instead.")]
		[Export ("fillColorMenuItemsForAnnotation:")]
		PSPDFMenuItem [] GetFillColorMenuItems (PSPDFAnnotation annotation);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'PDFConfiguration.annotationMenuConfiguration.alphaChoices' or 'PDFViewControllerDelegate.pdfViewController(_:menuForAnnotations:onPageView:appearance:suggestedMenu:)' instead.")]
		[Export ("opacityMenuItemForAnnotation:withColor:")]
		PSPDFMenuItem GetOpacityMenuItem (PSPDFAnnotation annotation, [NullAllowed] UIColor color);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'PDFConfiguration.AnnotationMenuConfiguration.ColorChoices' instead.")]
		[Export ("defaultColorOptionsForAnnotationType:")]
		UIColor [] GetDefaultColorOptions (PSPDFAnnotationType annotationType);

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'PDFConfiguration.annotationMenuConfiguration.FontSizeChoices' instead.")]
		[Export ("availableFontSizes")]
		NSNumber [] AvailableFontSizes { get; }

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'PDFConfiguration.annotationMenuConfiguration.LineWidthChoices' instead.")]
		[Export ("availableLineWidths")]
		NSNumber [] AvailableLineWidths { get; }

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'PDFViewControllerDelegate.pdfViewController(_:menuForAnnotations:onPageView:appearance:suggestedMenu:)' instead.")]
		[Export ("shouldMoveStyleMenuEntriesIntoSubmenu")]
		bool ShouldMoveStyleMenuEntriesIntoSubmenu { get; }

		[Obsolete ("Deprecated in PSPDFKit 12 for iOS. Use 'PresentationOption.PopoverPassthroughViews' instead.")]
		[Export ("passthroughViewsForPopoverController")]
		UIView [] PassthroughViewsForPopoverController { get; }
	}

	interface IPSPDFPresentationContext { }

	[Protocol]
	interface PSPDFPresentationContext : IPSPDFOverridable, PSPDFVisiblePagesDataSource, PSPDFErrorHandler {

		[Abstract]
		[Export ("configuration", ArgumentSemantic.Copy)]
		PSPDFConfiguration Configuration { get; }

		[Abstract]
		[Export ("pspdfkit")]
		PSPDFKitGlobal PsPdfKit { get; }

		[Abstract]
		[Export ("displayingViewController")]
		UIViewController DisplayingViewController { get; }

		[Abstract]
		[Export ("documentViewController")]
		PSPDFDocumentViewController DocumentViewController { get; }

		[Abstract]
		[NullAllowed, Export ("document")]
		PSPDFDocument Document { get; }

		[Abstract]
		[Export ("viewMode")]
		PSPDFViewMode ViewMode { get; }

		[Abstract]
		[Export ("contentRect")]
		CGRect ContentRect { get; }

		[Abstract]
		[Export ("rotationActive")]
		bool RotationActive { [Bind ("isRotationActive")] get; }

		[Abstract]
		[Export ("userInterfaceVisible")]
		bool UserInterfaceVisible { [Bind ("isUserInterfaceVisible")] get; }

		[Abstract]
		[Export ("viewWillAppearing")]
		bool ViewWillAppearing { [Bind ("isViewWillAppearing")] get; }

		[Abstract]
		[Export ("reloading")]
		bool Reloading { [Bind ("isReloading")] get; }

		[Abstract]
		[Export ("viewLoaded")]
		bool ViewLoaded { [Bind ("isViewLoaded")] get; }

		[Abstract]
		[Export ("visiblePageViews")]
		PSPDFPageView [] VisiblePageViews { get; }

		[Abstract]
		[Export ("pageViewForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFPageView GetPageView (nuint pageIndex);

		[Abstract]
		[Export ("annotationStateManager")]
		PSPDFAnnotationStateManager AnnotationStateManager { get; }

		[Abstract]
		[Export ("annotationToolbarController")]
		PSPDFAnnotationToolbarController AnnotationToolbarController { get; }

		[Abstract]
		[NullAllowed, Export ("actionDelegate")]
		IPSPDFControlDelegate ActionDelegate { get; }

		[Abstract]
		[Export ("pdfController")]
		PSPDFViewController PdfController { get; }
	}

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFPrintConfigurationBuilder {

		[Export ("printMode")]
		PSPDFPrintMode PrintMode { get; set; }

		[NullAllowed, Export ("defaultPrinter")]
		UIPrinter DefaultPrinter { get; set; }
	}

	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFPrintConfiguration {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFPrintConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		NativeHandle Constructor (PSPDFPrintConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFPrintConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFPrintConfigurationBuilder> builderHandler);

		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFPrintConfiguration GetUpdatedConfiguration ([NullAllowed] Action<PSPDFPrintConfigurationBuilder> builderHandler);

		[Export ("printMode")]
		PSPDFPrintMode PrintMode { get; }

		[NullAllowed, Export ("defaultPrinter")]
		UIPrinter DefaultPrinter { get; }
	}

	[BaseType (typeof (PSPDFContainerView))]
	interface PSPDFRelayTouchesView {

		[Export ("initWithFrame:")]
		NativeHandle Constructor (CGRect frame);
	}

	interface IPSPDFResizableViewDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFResizableViewDelegate {

		[Export ("resizableViewDidBeginEditing:")]
		void DidBeginEditing (PSPDFResizableView resizableView);

		[Export ("resizableViewChangedFrame:outerKnobType:isInitialChange:")]
		void ChangedFrame (PSPDFResizableView resizableView, PSPDFResizableViewOuterKnob outerKnobType, bool isInitialChange);

		[Export ("resizableView:adjustedProperty:ofAnnotation:")]
		void AdjustedProperty (PSPDFResizableView resizableView, string propertyName, PSPDFAnnotation annotation);

		[Export ("resizableViewDidEndEditing:didChangeFrame:")]
		void DidEndEditing (PSPDFResizableView resizableView, bool didChangeFrame);
	}

	interface IPSPDFKnobView { }

	[Protocol]
	interface PSPDFKnobView {

		[Abstract]
		[Export ("type", ArgumentSemantic.Assign)]
		PSPDFKnobType Type { get; set; }

		[Abstract]
		[Export ("knobSize")]
		CGSize KnobSize { get; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFResizableView : IPSPDFOverridable {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFResizableViewDelegate Delegate { get; set; }

		[Export ("mode", ArgumentSemantic.Assign)]
		PSPDFResizableViewMode Mode { get; set; }

		[NullAllowed, Export ("trackedViews", ArgumentSemantic.Copy)]
		NSSet<UIView> TrackedViews { get; set; }

		[Export ("trackedAnnotations")]
		NSSet<PSPDFAnnotation> TrackedAnnotations { get; }

		[Export ("zoomScale")]
		nfloat ZoomScale { get; set; }

		[Export ("innerEdgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets InnerEdgeInsets { get; set; }

		[Export ("outerEdgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets OuterEdgeInsets { get; set; }

		[Export ("allowEditing")]
		bool AllowEditing { get; set; }

		[Export ("allowResizing")]
		bool AllowResizing { get; set; }

		[Export ("allowAdjusting")]
		bool AllowAdjusting { get; set; }

		[Export ("allowRotating")]
		bool AllowRotating { get; set; }

		[Export ("contentRotation")]
		nuint ContentRotation { get; }

		[Export ("enableResizingGuides")]
		bool EnableResizingGuides { get; set; }

		[Export ("showBoundingBox")]
		bool ShowBoundingBox { get; set; }

		[Export ("guideSnapAllowance")]
		nfloat GuideSnapAllowance { get; set; }

		[Export ("minWidth")]
		nfloat MinWidth { get; set; }

		[Export ("minHeight")]
		nfloat MinHeight { get; set; }

		[Export ("selectionBorderWidth")]
		nfloat SelectionBorderWidth { get; set; }

		[NullAllowed, Export ("guideBorderColor", ArgumentSemantic.Strong)]
		UIColor GuideBorderColor { get; set; }

		[Export ("cornerRadius")]
		nfloat CornerRadius { get; set; }

		[Export ("outerKnobOfType:")]
		[return: NullAllowed]
		IPSPDFKnobView GetOuterKnob (PSPDFResizableViewOuterKnob knobType);

		[Export ("rotationKnob")]
		IPSPDFKnobView RotationKnob { get; }

		// PSPDFResizableView (SubclassingHooks) Category

		[Export ("forwardTouchesFromGestureRecognizer:")]
		void ForwardTouchesFrom (UIGestureRecognizer gestureRecognizer);

		[Export ("centerPointForOuterKnob:inFrame:")]
		CGPoint GetCenterPointForOuterKnob (PSPDFResizableViewOuterKnob knobType, CGRect frame);

		[Export ("centerPointForRotationKnobInFrame:")]
		CGPoint GetCenterPointForRotationKnob (CGRect frame);

		[Export ("newKnobViewForType:")]
		IPSPDFKnobView GetNewKnobView (PSPDFKnobType type);

		[Export ("updateKnobsAnimated:")]
		void UpdateKnobs (bool animated);
	}

	[BaseType (typeof (UILabel))]
	interface PSPDFRoundedLabel {

		[Export ("cornerRadius")]
		nfloat CornerRadius { get; set; }

		[NullAllowed, Export ("rectColor", ArgumentSemantic.Strong)]
		UIColor RectColor { get; set; }
	}

	interface IPSPDFAnnotationSetStore { }

	[Protocol]
	interface PSPDFAnnotationSetStore {

		[Abstract]
		[Export ("fetchAnnotationSetsWithError:")]
		[return: NullAllowed]
		PSPDFAnnotationSet[] FetchAnnotationSets ([NullAllowed] out NSError error);

		[Abstract]
		[Export ("setAnnotationSets:error:")]
		bool SetAnnotationSets (PSPDFAnnotationSet[] newValue, [NullAllowed] out NSError error);

		[Obsolete ("Use 'FetchAnnotationSets' and 'SetAnnotationSets' instead.")]
		[Abstract]
		[Export ("annotationSets", ArgumentSemantic.Copy)]
		PSPDFAnnotationSet [] AnnotationSets { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFKeychainAnnotationSetsStore : PSPDFAnnotationSetStore {

	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFPersistentAnnotationSetStore : PSPDFAnnotationSetStore {

		[Export ("initWithDirectoryURL:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSUrl directoryUrl);

		[Export ("directoryURL", ArgumentSemantic.Copy)]
		NSUrl DirectoryUrl { get; }

		[Export ("moveAnnotationSetsFromStore:")]
		void MoveAnnotationSetsFromStore (IPSPDFAnnotationSetStore otherStore);
	}

	[BaseType (typeof (PSPDFAnnotationGridViewController))]
	interface PSPDFSavedAnnotationsViewController : PSPDFAnnotationGridViewControllerDataSource, PSPDFStyleable, IPSPDFOverridable {

		[Static]
		[Export ("sharedAnnotationStore")]
		IPSPDFAnnotationSetStore SharedAnnotationStore { get; }

		[Export ("annotationStore", ArgumentSemantic.Strong)]
		IPSPDFAnnotationSetStore AnnotationStore { get; set; }
	}

	interface IPSPDFSaveViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFSaveViewControllerDelegate {

		[Abstract]
		[Export ("saveViewController:saveFileToURL:completionHandler:")]
		void SaveFile (PSPDFSaveViewController controller, NSUrl url, Action<NSUrl, NSError> completionHandler);

		[Abstract]
		[Export ("saveViewController:didFinishWithURL:")]
		void DidFinish (PSPDFSaveViewController controller, NSUrl url);

		[Abstract]
		[Export ("saveViewControllerDidCancel:")]
		void DidCancel (PSPDFSaveViewController controller);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	[DisableDefaultCtor]
	interface PSPDFSaveViewController : IPSPDFOverridable {

		[Export ("initWithSaveDirectories:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDirectory [] saveDirectories);

		[Export ("saveDirectories")]
		PSPDFDirectory [] SaveDirectories { get; }

		[Export ("selectedSaveDirectory", ArgumentSemantic.Assign)]
		PSPDFDirectory SelectedSaveDirectory { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFSaveViewControllerDelegate Delegate { get; set; }

		[Export ("showDirectoryPicker")]
		bool ShowDirectoryPicker { get; set; }

		[NullAllowed, Export ("fileName")]
		string FileName { get; set; }

		[NullAllowed, Export ("fullFilePath")]
		string FullFilePath { get; }
	}

	interface IPSPDFScreenControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFScreenControllerDelegate {

		[Export ("screenController:didStartMirroringForScreen:")]
		void DidStartMirroring (PSPDFScreenController screenController, UIScreen screen);

		[Export ("screenController:didStopMirroringForScreen:")]
		void DidStopMirroring (PSPDFScreenController screenController, UIScreen screen);

		[Export ("createPDFViewControllerForMirroring:")]
		PSPDFViewController CreatePdfViewControllerForMirroring (PSPDFScreenController screenController);

		[Export ("screenController:shouldSyncConfigurationTo:")]
		bool ShouldSyncConfiguration (PSPDFScreenController screenController, PSPDFViewController mirroredPdfController);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFScreenController {

		[NullAllowed, Export ("pdfControllerToMirror", ArgumentSemantic.Strong)]
		PSPDFViewController PdfControllerToMirror { get; set; }

		[Export ("mirrorControllerForScreen:")]
		[return: NullAllowed]
		PSPDFViewController MirrorController (UIScreen screen);

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFScreenControllerDelegate Delegate { get; set; }
	}

	[BaseType (typeof (PSPDFStackViewLayout))]
	interface PSPDFScrollPerSpreadLayout {

		[Export ("contentScale", ArgumentSemantic.Assign)]
		PSPDFScrollPerSpreadLayoutContentScale ContentScale { get; set; }
	}

	interface IPSPDFScrubberBarDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFScrubberBarDelegate {

		[Abstract]
		[Export ("scrubberBar:didSelectPageAtIndex:")]
		void DidSelectPage (PSPDFScrubberBar scrubberBar, nuint pageIndex);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFScrubberBar : IPSPDFOverridable {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFScrubberBarDelegate Delegate { get; set; }

		[NullAllowed, Export ("dataSource", ArgumentSemantic.Weak)]
		IPSPDFPresentationContext DataSource { get; set; }

		[Export ("scrubberBarType", ArgumentSemantic.Assign)]
		PSPDFScrubberBarType ScrubberBarType { get; set; }

		[Export ("updateToolbarAnimated:")]
		void UpdateToolbar (bool animated);

		[Export ("updateToolbarForced")]
		void UpdateToolbarForced ();

		[Export ("pageIndex")]
		nuint PageIndex { get; set; }

		[Export ("allowTapsOutsidePageArea")]
		bool AllowTapsOutsidePageArea { get; set; }

		[NullAllowed, Export ("standardAppearance", ArgumentSemantic.Strong)]
		UIToolbarAppearance StandardAppearance { get; set; }

		[NullAllowed, Export ("compactAppearance", ArgumentSemantic.Strong)]
		UIToolbarAppearance CompactAppearance { get; set; }

		[Export ("leftBorderMargin")]
		nfloat LeftBorderMargin { get; set; }

		[Export ("rightBorderMargin")]
		nfloat RightBorderMargin { get; set; }

		[Export ("thumbnailMargin")]
		nfloat ThumbnailMargin { get; set; }

		[Export ("pageMarkerSizeMultiplier")]
		nfloat PageMarkerSizeMultiplier { get; set; }

		[NullAllowed, Export ("thumbnailBorderColor", ArgumentSemantic.Strong)]
		UIColor ThumbnailBorderColor { get; set; }

		[Export ("toolbar")]
		UIToolbar Toolbar { get; }

		// PSPDFScrubberBar (SubclassingHooks) Category

		[Export ("scrubberBarHeight")]
		nfloat ScrubberBarHeight { get; }

		[Export ("scrubberBarThumbSize")]
		CGSize ScrubberBarThumbSize { get; }

		[Export ("emptyThumbnailImageView")]
		UIImageView EmptyThumbnailImageView { get; }
	}

	[BaseType (typeof (PSPDFStatefulTableViewController))]
	interface PSPDFSearchableTableViewController {

		[Export ("searchEnabled")]
		bool SearchEnabled { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFSearchHighlightView : PSPDFAnnotationPresenting, IPSPDFOverridable {

		[Export ("popupAnimation")]
		void PopupAnimation ();

		[NullAllowed, Export ("searchResult", ArgumentSemantic.Strong)]
		PSPDFSearchResult SearchResult { get; set; }

		[NullAllowed, Export ("selectionBackgroundColor", ArgumentSemantic.Strong)]
		UIColor SelectionBackgroundColor { get; set; }

		[Export ("cornerRadiusProportion")]
		nfloat CornerRadiusProportion { get; set; }
	}

	interface IPSPDFSearchHighlightViewManagerDataSource { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFSearchHighlightViewManagerDataSource {

		[Abstract]
		[Export ("shouldHighlightSearchResults")]
		bool ShouldHighlightSearchResults { get; }

		[Abstract]
		[Export ("visiblePageViews")]
		PSPDFPageView [] VisiblePageViews { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSearchHighlightViewManager : IPSPDFOverridable {

		[Export ("initWithDataSource:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IPSPDFSearchHighlightViewManagerDataSource dataSource);

		[NullAllowed, Export ("dataSource", ArgumentSemantic.Weak)]
		IPSPDFSearchHighlightViewManagerDataSource DataSource { get; }

		[Export ("hasVisibleSearchResults")]
		bool HasVisibleSearchResults { get; }

		[Export ("clearHighlightedSearchResultsAnimated:")]
		void ClearHighlightedSearchResults (bool animated);

		[Export ("addHighlightSearchResults:animated:")]
		void AddHighlightSearchResults (PSPDFSearchResult [] searchResults, bool animated);

		[Export ("animateSearchHighlight:")]
		void AnimateSearchHighlight (PSPDFSearchResult searchResult);
	}

	interface IPSPDFSearchResultViewable { }

	[Protocol]
	interface PSPDFSearchResultViewable {

		[Abstract]
		[Export ("configureWithSearchResult:")]
		void ConfigureWithSearchResult (PSPDFSearchResult searchResult);
	}

	interface IPSPDFSearchScopeViewable { }

	[Protocol]
	interface PSPDFSearchScopeViewable {

		[Abstract]
		[Export ("configureWithDocument:scope:pageIndexRange:results:")]
		void ConfigureWithDocument (PSPDFDocument document, PSPDFSearchResultScope scope, NSRange pageIndexRange, nuint results);
	}

	interface IPSPDFSearchStatusViewable { }

	[Protocol]
	interface PSPDFSearchStatusViewable {

		[Abstract]
		[Export ("configureWithSearchStatus:results:pageIndex:pageCount:")]
		void ConfigureWithSearchStatus (PSPDFSearchStatus searchStatus, nuint results, nuint pageIndex, nuint pageCount);
	}

	interface IPSPDFSearchViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFSearchViewControllerDelegate : IPSPDFTextSearchDelegate {

		[Export ("searchViewController:didTapSearchResult:")]
		void DidTapSearchResult (PSPDFSearchViewController searchController, PSPDFSearchResult searchResult);

		[Export ("searchViewControllerDidClearAllSearchResults:")]
		void DidClearAllSearchResults (PSPDFSearchViewController searchController);

		[Export ("searchViewControllerGetVisiblePages:")]
		NSIndexSet GetVisiblePages (PSPDFSearchViewController searchController);

		[Export ("searchViewController:searchRangeForScope:")]
		[return: NullAllowed]
		NSIndexSet GetSearchRange (PSPDFSearchViewController searchController, string scope);

		[Export ("searchViewControllerTextSearchObject:")]
		PSPDFTextSearch GetTextSearchObject (PSPDFSearchViewController searchController);
	}

	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFSearchViewController :

#if !__MACCATALYST__
		IUISearchDisplayDelegate, IUISearchBarDelegate,
#endif
		IPSPDFTextSearchDelegate, PSPDFStyleable, IPSPDFOverridable {

		[Static]
		[Export ("resultCellClass")]
		Class ResultCellClass { get; }

		[Static]
		[Export ("scopeHeaderClass")]
		Class ScopeHeaderClass { get; }

		[Static]
		[Export ("statusFooterClass")]
		Class StatusFooterClass { get; }

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] PSPDFDocument document);

		[NullAllowed, Export ("document", ArgumentSemantic.Strong)]
		PSPDFDocument Document { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFSearchViewControllerDelegate Delegate { get; set; }

		[NullAllowed, Export ("searchText")]
		string SearchText { get; set; }

		[Export ("showsCancelButton")]
		bool ShowsCancelButton { get; set; }

		[Export ("searchBar")]
		UISearchBar SearchBar { get; }

		[Export ("searchStatus")]
		PSPDFSearchStatus SearchStatus { get; }

		[Export ("clearHighlightsWhenClosed")]
		bool ClearHighlightsWhenClosed { get; set; }

		[Export ("maximumNumberOfSearchResultsDisplayed")]
		nuint MaximumNumberOfSearchResultsDisplayed { get; set; }

		[Export ("searchVisiblePagesFirst")]
		bool SearchVisiblePagesFirst { get; set; }

		[Export ("numberOfPreviewTextLines")]
		nuint NumberOfPreviewTextLines { get; set; }

		[Export ("useOutlineForPageNames")]
		bool UseOutlineForPageNames { get; set; }

		[Export ("restoreLastSearchResult")]
		bool RestoreLastSearchResult { get; set; }

		[Export ("searchableAnnotationTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType SearchableAnnotationTypes { get; set; }

		[Export ("searchBarPinning", ArgumentSemantic.Assign)]
		PSPDFSearchBarPinning SearchBarPinning { get; set; }

		[NullAllowed, Export ("textSearch")]
		PSPDFTextSearch TextSearch { get; }

		[Export ("restartSearch")]
		void RestartSearch ();

		// PSPDFSearchViewController (SubclassingHooks) Category

		[Export ("filterContentForSearchText:scope:")]
		void FilterContent (string searchText, [NullAllowed] string scope);

		[Export ("setSearchStatus:updateTable:")]
		void SetSearchStatus (PSPDFSearchStatus searchStatus, bool updateTable);

		[Export ("searchResultForIndexPath:")]
		PSPDFSearchResult GetSearchResult (NSIndexPath indexPath);

		[Export ("createSearchBar")]
		UISearchBar CreateSearchBar { get; }

		[Export ("searchResults")]
		PSPDFSearchResult [] SearchResults { get; }
	}

	[BaseType (typeof (UICollectionViewCell))]
	interface PSPDFSelectableCollectionViewCell {

		[Export ("selectableCellStyle", ArgumentSemantic.Assign)]
		PSPDFSelectableCollectionViewCellStyle SelectableCellStyle { get; set; }

		[NullAllowed, Export ("selectableCellColor", ArgumentSemantic.Strong)]
		UIColor SelectableCellColor { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFSelectionState : INSSecureCoding {

		[Static]
		[Export ("stateForSelectionView:")]
		[return: NullAllowed]
		PSPDFSelectionState FromSelectionView (PSPDFTextSelectionView selectionView);

		[Export ("UID")]
		string Uid { get; }

		[Export ("selectionPageIndex")]
		nuint SelectionPageIndex { get; }

		[Export ("selectedGlyphRange")]
		NSRange SelectedGlyphRange { get; }

		[NullAllowed, Export ("selectedImage")]
		PSPDFImageInfo SelectedImage { get; }

		[Export ("isEqualToSelectionState:")]
		bool IsEqualTo ([NullAllowed] PSPDFSelectionState selectionState);
	}

	interface IPSPDFSelectionViewDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFSelectionViewDelegate {

		[Export ("selectionView:shouldStartSelectionAtPoint:")]
		bool ShouldStartSelection (PSPDFSelectionView selectionView, CGPoint point);

		[Export ("selectionView:updateSelectedRect:")]
		void UpdateSelectedRect (PSPDFSelectionView selectionView, CGRect rect);

		[Export ("selectionView:finishedWithSelectedRect:")]
		void FinishedWithSelectedRect (PSPDFSelectionView selectionView, CGRect rect);

		[Export ("selectionView:cancelledWithSelectedRect:")]
		void CancelledWithSelectedRect (PSPDFSelectionView selectionView, CGRect rect);

		[Export ("selectionView:singleTappedWithGestureRecognizer:")]
		void SingleTappedWithGestureRecognizer (PSPDFSelectionView selectionView, UITapGestureRecognizer gestureRecognizer);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFSelectionView : IPSPDFOverridable {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFSelectionViewDelegate Delegate { get; set; }

		[Export ("selectionAlpha")]
		nfloat SelectionAlpha { get; set; }

		[NullAllowed, Export ("rects", ArgumentSemantic.Copy)]
		NSValue [] Rects { get; set; }

		[Export ("allowedTouchTypes", ArgumentSemantic.Copy)]
		NSNumber [] AllowedTouchTypes { get; set; }

		// PSPDFSelectionView (SubclassingHooks) Category

		[Export ("tapGestureRecognizer")]
		UITapGestureRecognizer TapGestureRecognizer { get; }

		[Export ("panGestureRecognizer")]
		UIPanGestureRecognizer PanGestureRecognizer { get; }
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFSettingsViewController : IPSPDFOverridable {

		[NullAllowed, Export ("pdfViewController", ArgumentSemantic.Weak)]
		PSPDFViewController PdfViewController { get; set; }
	}

	[BaseType (typeof (UITableViewCell))]
	interface PSPDFSignatureCell : IPSPDFOverridable {

		[Export ("certificateLabel")]
		UIControl CertificateLabel { get; }

		[Export ("signatureImageView")]
		UIImageView SignatureImageView { get; }

		[Export ("updateCellForSigner:")]
		void UpdateCell ([NullAllowed] PSPDFSigner signer);
	}

	interface IPSPDFSignatureSelectorViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFSignatureSelectorViewControllerDelegate {

		[Abstract]
		[Export ("signatureSelectorViewController:didSelectSignature:")]
		void DidSelectSignature (PSPDFSignatureSelectorViewController signatureSelectorController, PSPDFSignatureContainer signature);

		[Abstract]
		[Export ("signatureSelectorViewControllerWillCreateNewSignature:")]
		void WillCreateNewSignature (PSPDFSignatureSelectorViewController signatureSelectorController);
	}

	[BaseType (typeof (PSPDFStatefulTableViewController))]
	interface PSPDFSignatureSelectorViewController : PSPDFStyleable, IPSPDFOverridable {

		[NullAllowed, Export ("signatureStore", ArgumentSemantic.Strong)]
		IPSPDFSignatureStore SignatureStore { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFSignatureSelectorViewControllerDelegate Delegate { get; set; }

		// PSPDFSelectionView (SubclassingHooks) Category

		[Export ("addSignatureButtonItem")]
		UIBarButtonItem AddSignatureButtonItem { get; }

		[Export ("doneButtonItem")]
		UIBarButtonItem DoneButtonItem { get; }

		[Export ("doneAction:")]
		void DoneAction ([NullAllowed] NSObject sender);

		[Export ("addSignatureAction:")]
		void AddSignatureAction ([NullAllowed] NSObject sender);
	}

	interface IPSPDFSignatureStore { }

	[Protocol]
	interface PSPDFSignatureStore : INSSecureCoding {

		[Abstract]
		[Export ("addSignature:")]
		void AddSignature (PSPDFSignatureContainer signature);

		[Abstract]
		[Export ("removeSignature:")]
		bool RemoveSignature (PSPDFSignatureContainer signature);

		[Abstract]
		[Export ("signatures", ArgumentSemantic.Copy)]
		PSPDFSignatureContainer [] Signatures { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFKeychainSignatureStore : PSPDFSignatureStore {

		[Field ("PSPDFKeychainSignatureStoreDefaultStoreName", PSPDFKitGlobal.LibraryPath)]
		NSString DefaultStoreName { get; }

		[Export ("initWithStoreName:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] string storeName);

		[Export ("storeName")]
		string StoreName { get; }
	}

	interface IPSPDFSignatureViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFSignatureViewControllerDelegate {

		[Export ("signatureViewControllerDidCancel:")]
		void DidCancel (PSPDFSignatureViewController signatureController);

		[Export ("signatureViewControllerDidFinish:withSigner:shouldSaveSignature:")]
		void DidFinish (PSPDFSignatureViewController signatureController, [NullAllowed] PSPDFSigner signer, bool shouldSaveSignature);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFSignatureViewController : PSPDFStyleable, IPSPDFOverridable {

		[Export ("naturalDrawingEnabled")]
		bool NaturalDrawingEnabled { get; set; }

		[Export ("menuColors", ArgumentSemantic.Copy)]
		UIColor [] MenuColors { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFSignatureViewControllerDelegate Delegate { get; set; }

		[Export ("savingStrategy", ArgumentSemantic.Assign)]
		PSPDFSignatureSavingStrategy SavingStrategy { get; set; }

		[Export ("certificateSelectionMode", ArgumentSemantic.Assign)]
		PSPDFSignatureCertificateSelectionMode CertificateSelectionMode { get; set; }

		[Export ("drawView")]
		PSPDFDrawView DrawView { get; }

		[NullAllowed, Export ("signer")]
		PSPDFSigner Signer { get; }

		// PSPDFSignatureViewController (SubclassingHooks) Category

		[Export ("colorButtonForColor:")]
		PSPDFColorButton ColorButtonForColor (UIColor color);

		[Export ("cancel:")]
		void Cancel ([NullAllowed] NSObject sender);

		[Export ("done:")]
		void Done ([NullAllowed] NSObject sender);

		[Export ("clear:")]
		void Clear ([NullAllowed] NSObject sender);

		[Export ("color:")]
		void Color (PSPDFColorButton sender);
	}

	[BaseType (typeof (PSPDFBaseTableViewController))]
	[DisableDefaultCtor]
	interface PSPDFSignedFormElementViewController : IPSPDFOverridable {

		[Export ("initWithSignatureFormElement:")]
		NativeHandle Constructor (PSPDFSignatureFormElement element);

		[Export ("initWithSignatureFormElement:allowRemovingSignature:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFSignatureFormElement element, bool allowRemovingSignature);

		[Export ("formElement", ArgumentSemantic.Strong)]
		PSPDFSignatureFormElement FormElement { get; }

		[Export ("verifySignatureWithTrustedCertificates:error:")]
		[return: NullAllowed]
		PSPDFSignatureStatus VerifySignature ([NullAllowed] PSPDFX509 [] trustedCertificates, [NullAllowed] out NSError error);

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFSignedFormElementViewControllerDelegate Delegate { get; set; }
	}

	interface IPSPDFSignedFormElementViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFSignedFormElementViewControllerDelegate {

		[Export ("signedFormElementViewController:removedSignatureFromDocument:")]
		void RemovedSignatureFromDocument (PSPDFSignedFormElementViewController controller, PSPDFDocument document);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFSpeechController {

		[Export ("speakText:options:delegate:")]
		void SpeakText (string speechString, [NullAllowed] NSDictionary options, [NullAllowed] IAVSpeechSynthesizerDelegate @delegate);

		[Wrap ("SpeakText (speechString, speechOptions?.Dictionary, @delegate)")]
		void SpeakText (string speechString, PSPDFSpeechControllerOptions speechOptions, IAVSpeechSynthesizerDelegate @delegate);

		[Export ("stopSpeakingForDelegate:")]
		bool StopSpeaking ([NullAllowed] IAVSpeechSynthesizerDelegate @delegate);

		[Export ("speechSynthesizer")]
		AVSpeechSynthesizer SpeechSynthesizer { get; }

		[Export ("selectedLanguage")]
		string SelectedLanguage { get; set; }

		[Export ("languageCodes", ArgumentSemantic.Copy)]
		string [] LanguageCodes { get; }

		[Export ("speakRate")]
		float SpeakRate { get; set; }

		[Export ("pitchMultiplier")]
		float PitchMultiplier { get; set; }
	}

	[Static]
	interface PSPDFSpeechControllerOptionKeys {

		[Field ("PSPDFSpeechControllerOptionAutoDetectLanguage", PSPDFKitGlobal.LibraryPath)]
		NSString AutoDetectLanguageKey { get; }

		[Field ("PSPDFSpeechControllerOptionLanguage", PSPDFKitGlobal.LibraryPath)]
		NSString LanguageKey { get; }

		[Field ("PSPDFSpeechControllerOptionLanguageHint", PSPDFKitGlobal.LibraryPath)]
		NSString LanguageHintKey { get; }
	}

	[StrongDictionary ("PSPDFSpeechControllerOptionKeys")]
	interface PSPDFSpeechControllerOptions {
		bool AutoDetectLanguage { get; set; }
		string Language { get; set; }
		string LanguageHint { get; set; }
	}

	[BaseType (typeof (PSPDFTableViewCell))]
	interface PSPDFSpinnerCell {

		// PSPDFSpinnerCell (SubclassingHooks) Category

		[Export ("spinner")]
		UIActivityIndicatorView Spinner { get; }

		[Export ("alignTextLabel")]
		void AlignTextLabel ();

	}

	[BaseType (typeof (UIView))]
	interface PSPDFSpreadView : IPSPDFOverridable {

		[NullAllowed, Export ("layout")]
		PSPDFDocumentViewLayout Layout { get; }

		[Export ("spreadIndex")]
		nint SpreadIndex { get; }

		[Export ("pageViews", ArgumentSemantic.Copy)]
		PSPDFPageView [] PageViews { get; }

		[Export ("pageViewForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFPageView GetPageViewForPage (nuint pageIndex);
	}

	[BaseType (typeof (PSPDFDocumentViewLayout))]
	interface PSPDFStackViewLayout {

		[Export ("contentInsets")]
		UIEdgeInsets ContentInsets { get; }

		[Export ("additionalContentInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets AdditionalContentInsets { get; set; }

		[Export ("interitemSpacing")]
		nfloat InteritemSpacing { get; set; }

		[Export ("direction", ArgumentSemantic.Assign)]
		PSPDFStackViewLayoutDirection Direction { get; set; }

		// PSPDFStackViewLayout (Subclassing) Category

		[Export ("sizeForSpreadAtIndex:")]
		CGSize GetSizeForSpread (nint spreadIndex);
	}

	[BaseType (typeof (PSPDFAnnotationGridViewController))]
	interface PSPDFStampViewController : PSPDFAnnotationGridViewControllerDataSource, PSPDFTextStampViewControllerDelegate, IPSPDFOverridable {

		[Static]
		[NullAllowed, Export ("defaultStampAnnotations")]
		PSPDFStampAnnotation [] DefaultStampAnnotations { get; set; }

		[Export ("stamps", ArgumentSemantic.Copy)]
		PSPDFStampAnnotation [] Stamps { get; set; }

		[Export ("customStampEnabled")]
		bool CustomStampEnabled { get; set; }

		[Export ("dateStampsEnabled")]
		bool DateStampsEnabled { get; set; }

		// PSPDFStackViewLayout (Subclassing) Category

		[Export ("defaultDateStamps")]
		PSPDFStampAnnotation [] DefaultDateStamps { get; }
	}

	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFStatefulTableViewController : PSPDFStatefulViewControlling {

		// All of the following come from PSPDFStatefulViewControlling interface

		//[NullAllowed, Export ("emptyView", ArgumentSemantic.Strong)]
		//UIView EmptyView { get; set; }

		//[NullAllowed, Export ("loadingView", ArgumentSemantic.Strong)]
		//UIView LoadingView { get; set; }

		//[Export ("controllerState", ArgumentSemantic.Assign)]
		//PSPDFStatefulViewState ControllerState { get; set; }

		//[Export ("setControllerState:animated:")]
		//void SetControllerState (PSPDFStatefulViewState controllerState, bool animated);

		[Export ("preferredContentSize"), New]
		new CGSize PreferredContentSize { get; }
	}

	interface IPSPDFStatefulViewControlling { }

	[Protocol]
	interface PSPDFStatefulViewControlling : IUIContentContainer {

		[Abstract]
		[NullAllowed, Export ("emptyView", ArgumentSemantic.Strong)]
		UIView EmptyView { get; set; }

		[Abstract]
		[NullAllowed, Export ("loadingView", ArgumentSemantic.Strong)]
		UIView LoadingView { get; set; }

		[Abstract]
		[Export ("controllerState", ArgumentSemantic.Assign)]
		PSPDFStatefulViewState ControllerState { get; set; }

		[Export ("setControllerState:animated:")]
		void SetControllerState (PSPDFStatefulViewState controllerState, bool animated);
	}

	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFStaticTableViewController {

	}

	[BaseType (typeof (NSObject))]
	interface PSPDFStatusHUDItem {

		[NullAllowed, Export ("title")]
		string Title { get; set; }

		[NullAllowed, Export ("subtitle")]
		string Subtitle { get; set; }

		[NullAllowed, Export ("text")]
		string Text { get; set; }

		[NullAllowed, Export ("actionTitle")]
		string ActionTitle { get; set; }

		[Export ("progress")]
		nfloat Progress { get; set; }

		[NullAllowed, Export ("view", ArgumentSemantic.Strong)]
		UIView View { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFStatusHUDItemDelegate Delegate { get; set; }

		[Static]
		[Export ("progressWithText:actionTitle:")]
		PSPDFStatusHUDItem CreateProgress ([NullAllowed] string text, [NullAllowed] string title);

		[Static]
		[Export ("progressWithText:")]
		PSPDFStatusHUDItem CreateProgress ([NullAllowed] string text);

		[Static]
		[Export ("indeterminateProgressWithText:actionTitle:")]
		PSPDFStatusHUDItem CreateIndeterminateProgress ([NullAllowed] string text, [NullAllowed] string title);

		[Static]
		[Export ("indeterminateProgressWithText:")]
		PSPDFStatusHUDItem CreateIndeterminateProgress ([NullAllowed] string text);

		[Static]
		[Export ("successWithText:")]
		PSPDFStatusHUDItem CreateSuccess ([NullAllowed] string text);

		[Static]
		[Export ("errorWithText:")]
		PSPDFStatusHUDItem CreateError ([NullAllowed] string text);

		[Static]
		[Export ("itemWithText:image:")]
		PSPDFStatusHUDItem CreateItem ([NullAllowed] string text, [NullAllowed] UIImage image);

		[Export ("setHUDStyle:")]
		void SetHudStyle (PSPDFStatusHUDStyle style);

		[Async]
		[Export ("pushAnimated:onWindow:completion:")]
		void Push (bool animated, [NullAllowed] UIWindow window, [NullAllowed] Action completion);

		[Async]
		[Export ("pushAndPopWithDelay:animated:onWindow:completion:")]
		void PushAndPop (double interval, bool animated, [NullAllowed] UIWindow window, [NullAllowed] Action completion);

		[Async]
		[Export ("popAnimated:completion:")]
		void Pop (bool animated, [NullAllowed] Action completion);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFStatusHUD {

		[Static]
		[Export ("itemsForHUDOnWindow:")]
		[return: NullAllowed]
		PSPDFStatusHUDItem [] GetItems ([NullAllowed] UIWindow window);

		[Static]
		[Async]
		[Export ("popAllItemsAnimated:forHUDOnWindow:completion:")]
		void PopAllItems (bool animated, [NullAllowed] UIWindow window, [NullAllowed] Action completion);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFStatusHUDView : IPSPDFOverridable {

		[Export ("initWithFrame:")]
		NativeHandle Constructor (CGRect frame);

		[NullAllowed, Export ("item", ArgumentSemantic.Strong)]
		PSPDFStatusHUDItem Item { get; set; }
	}

	interface IPSPDFStyleable { }

	[Protocol]
	interface PSPDFStyleable {

		[Export ("barStyle")]
		UIBarStyle GetBarStyle ();

		[Export ("setBarStyle:")]
		void SetBarStyle (UIBarStyle style);

		[Export ("isBarTranslucent")]
		bool GetIsBarTranslucent ();

		[Export ("setIsBarTranslucent:")]
		void SetIsBarTranslucent (bool isBarTranslucent);

		[Export ("forcesStatusBarHidden")]
		bool GetForcesStatusBarHidden ();

		[Export ("setForcesStatusBarHidden:")]
		void SetForcesStatusBarHidden (bool val);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFTabbedBar {

		[Export ("barHeight")]
		nfloat BarHeight { get; set; }

		[Export ("minTabWidth")]
		nfloat MinTabWidth { get; set; }

		[Export ("tabTitleFont", ArgumentSemantic.Strong)]
		UIFont TabTitleFont { get; set; }

		[Export ("closeButtonImage", ArgumentSemantic.Strong)]
		UIImage CloseButtonImage { get; set; }

		[Export ("backgroundView", ArgumentSemantic.Strong)]
		UIView BackgroundView { get; set; }

		[Export ("documentPickerButton")]
		UIButton DocumentPickerButton { get; }

		[Export ("overviewButton")]
		UIButton OverviewButton { get; }

		[Export ("overviewThreshold")]
		nint OverviewThreshold { get; set; }

		[Export ("leftViews", ArgumentSemantic.Copy)]
		UIView [] LeftViews { get; set; }

		[Export ("rightViews", ArgumentSemantic.Copy)]
		UIView [] RightViews { get; set; }
	}

	interface IPSPDFTabbedViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFTabbedViewControllerDelegate : PSPDFMultiDocumentViewControllerDelegate {

		[Export ("tabbedPDFController:shouldChangeVisibleDocument:")]
		bool ShouldChangeVisibleDocument (PSPDFTabbedViewController tabbedPdfController, [NullAllowed] PSPDFDocument newVisibleDocument);

		[Export ("tabbedPDFController:didChangeVisibleDocument:")]
		void DidChangeVisibleDocument (PSPDFTabbedViewController tabbedPdfController, [NullAllowed] PSPDFDocument oldVisibleDocument);

		[Export ("tabbedPDFController:shouldCloseDocument:")]
		bool ShouldCloseDocument (PSPDFTabbedViewController tabbedPdfController, PSPDFDocument document);

		[Export ("tabbedPDFController:didCloseDocument:")]
		void DidCloseDocument (PSPDFTabbedViewController tabbedPdfController, PSPDFDocument document);
	}

	[BaseType (typeof (PSPDFMultiDocumentViewController))]
	interface PSPDFTabbedViewController {

        [Export("initWithPDFViewController:")]
        [DesignatedInitializer]
        NativeHandle Constructor([NullAllowed] PSPDFViewController pdfController);

        [Export ("addDocument:makeVisible:animated:")]
		void AddDocument (PSPDFDocument document, bool shouldMakeDocumentVisible, bool animated);

		[Export ("insertDocumentAfterVisibleDocument:makeVisible:animated:")]
		void InsertDocumentAfterVisibleDocument (PSPDFDocument document, bool shouldMakeDocumentVisible, bool animated);

		[Export ("insertDocument:atIndex:makeVisible:animated:")]
		void InsertDocument (PSPDFDocument document, nuint idx, bool shouldMakeDocumentVisible, bool animated);

		[Export ("removeDocumentAtIndex:animated:")]
		void RemoveDocument (nuint idx, bool animated);

		[Export ("removeDocument:animated:")]
		bool RemoveDocument (PSPDFDocument document, bool animated);

		[Export ("setVisibleDocument:scrollToPosition:animated:")]
		void SetVisibleDocument ([NullAllowed] PSPDFDocument visibleDocument, bool scrollToPosition, bool animated);

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak), New]
		IPSPDFTabbedViewControllerDelegate Delegate { get; set; }

		[Export ("statePersistenceKey"), New]
		string StatePersistenceKey { get; set; }

		[Export ("tabbedBar")]
		PSPDFTabbedBar TabbedBar { get; }

		[NullAllowed, Export ("documentPickerController", ArgumentSemantic.Strong)]
		PSPDFDocumentPickerController DocumentPickerController { get; set; }

		[Export ("barHidingMode", ArgumentSemantic.Assign)]
		PSPDFTabbedViewControllerBarHidingMode BarHidingMode { get; set; }

		[Export ("closeMode", ArgumentSemantic.Assign)]
		PSPDFTabbedViewControllerCloseMode CloseMode { get; set; }

		[Export ("openDocumentActionInNewTab")]
		bool OpenDocumentActionInNewTab { get; set; }

		[Export ("allowDraggingTabsToExternalTabbedBar")]
		bool AllowDraggingTabsToExternalTabbedBar { get; set; }

		[Export ("allowDroppingTabsFromExternalTabbedBar")]
		bool AllowDroppingTabsFromExternalTabbedBar { get; set; }

		[Export ("allowReorderingDocuments")]
		bool AllowReorderingDocuments { get; set; }

		[Export ("updateTabbedBarFrameAnimated:")]
		void UpdateTabbedBarFrame (bool animated);
	}

	[BaseType (typeof (UITableViewCell))]
	interface PSPDFTableViewCell {

		[NullAllowed, Export ("context", ArgumentSemantic.Assign)]
		NSObject Context { get; set; }
	}

	[BaseType (typeof (PSPDFTableViewCell))]
	interface PSPDFNonAnimatingTableViewCell {

	}

	[BaseType (typeof (PSPDFTableViewCell))]
	interface PSPDFNeverAnimatingTableViewCell : IPSPDFOverridable {

	}

	[BaseType (typeof (PSPDFFormElementView))]
	interface PSPDFTextFieldFormElementView : IUITextViewDelegate, IUITextFieldDelegate, IPSPDFOverridable {

		[Export("initWithFrame:")]
		NativeHandle Constructor(CGRect frame);

		[Export ("beginEditing")]
		void BeginEditing ();

		[Export ("endEditing")]
		void EndEditing ();

		[NullAllowed, Export ("textView")]
		UITextView TextView { get; }

		[NullAllowed, Export ("textField")]
		UITextField TextField { get; }

		[Export ("isMultiline")]
		bool IsMultiline { get; }

		[Export ("isPassword")]
		bool IsPassword { get; }

		[Export ("editMode")]
		bool EditMode { get; set; }

		[NullAllowed, Export ("resizableView", ArgumentSemantic.Weak)]
		PSPDFResizableView ResizableView { get; set; }

		// PSPDFTextFieldFormElementView (SubclassingHooks) Category

		[Export ("setTextViewForEditing")]
		UITextView SetTextViewForEditing { get; }
	}

	interface IPSPDFTextSelectionViewDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFTextSelectionViewDelegate {

		[Obsolete]
		[Export ("textSelectionView:updateMenuAnimated:")]
		bool UpdateMenuAnimated (PSPDFTextSelectionView textSelectionView, bool animated);

		[Export ("textSelectionView:shouldSelectText:withGlyphs:atRect:")]
		bool ShouldSelectText (PSPDFTextSelectionView textSelectionView, string text, PSPDFGlyph [] glyphs, CGRect rect);

		[Export ("textSelectionView:didSelectText:withGlyphs:atRect:")]
		void DidSelectText (PSPDFTextSelectionView textSelectionView, string text, PSPDFGlyph [] glyphs, CGRect rect);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFTextSelectionView : IAVSpeechSynthesizerDelegate, IPSPDFOverridable {
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFTextSelectionViewDelegate Delegate { get; set; }

		[NullAllowed, Export ("selectedGlyphs", ArgumentSemantic.Copy)]
		PSPDFGlyph [] SelectedGlyphs { get; set; }

		[NullAllowed, Export ("selectedText")]
		string SelectedText { get; }

		[NullAllowed, Export ("trimmedSelectedText")]
		string TrimmedSelectedText { get; }

		[Export ("sortedGlyphs:")]
		PSPDFGlyph [] GetSortedGlyphs (PSPDFGlyph [] glyphs);

		[Export ("clearCache")]
		void ClearCache ();

		[NullAllowed, Export ("selectedImage", ArgumentSemantic.Assign)]
		PSPDFImageInfo SelectedImage { get; set; }

		[Export ("hasSelection")]
		bool HasSelection { get; }

		[Export ("selectionRects")]
		NSValue [] SelectionRects { get; }

		[Export ("rectForFirstBlock")]
		CGRect RectForFirstBlock { get; }

		[Export ("rectForLastBlock")]
		CGRect RectForLastBlock { get; }

		[Export ("updateSelectionAnimated:")]
		void UpdateSelection (bool animated);

		[Export ("selectionAlpha")]
		nfloat SelectionAlpha { get; set; }

		[Export ("selectionHitTestExtension")]
		nfloat SelectionHitTestExtension { get; set; }

		[Export ("showTextFlowData:animated:")]
		void ShowTextFlowData (bool show, bool animated);

		[Export ("discardSelectionAnimated:")]
		void DiscardSelection (bool animated);

		[Export ("updateMenuAnimated:")]
		bool UpdateMenu (bool animated);
	}

	interface IPSPDFTextStampViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFTextStampViewControllerDelegate {

		[Export ("textStampViewController:didCreateAnnotation:")]
		void DidCreateAnnotation (PSPDFTextStampViewController stampController, PSPDFStampAnnotation stampAnnotation);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFTextStampViewController : IPSPDFOverridable {

		[Export ("initWithStampAnnotation:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] PSPDFStampAnnotation stampAnnotation);

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFTextStampViewControllerDelegate Delegate { get; set; }

		[Export ("stampAnnotation")]
		PSPDFStampAnnotation StampAnnotation { get; }

		[NullAllowed, Export ("defaultStampText")]
		string DefaultStampText { get; set; }
	}

	interface IPSPDFThumbnailBarDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFThumbnailBarDelegate {

		[Export ("thumbnailBar:didSelectPageAtIndex:")]
		void DidSelectPage (PSPDFThumbnailBar thumbnailBar, nuint pageIndex);
	}

	[BaseType (typeof (UICollectionView))]
	interface PSPDFThumbnailBar : IUICollectionViewDataSource, IUICollectionViewDelegate, IPSPDFOverridable {

		[NullAllowed, Export ("thumbnailBarDelegate", ArgumentSemantic.Weak)]
		IPSPDFThumbnailBarDelegate ThumbnailBarDelegate { get; set; }

		[NullAllowed, Export ("thumbnailBarDataSource", ArgumentSemantic.Weak)]
		IPSPDFPresentationContext ThumbnailBarDataSource { get; set; }

		[Export ("scrollToPageAtIndex:animated:")]
		void ScrollToPage (nuint pageIndex, bool animated);

		[Export ("stopScrolling")]
		void StopScrolling ();

		[Export ("reloadDataAndKeepSelection")]
		void ReloadDataAndKeepSelection ();

		[Export ("reloadPagesAtIndexes:animated:")]
		void ReloadPages (NSIndexSet indexes, bool animated);

		[Export ("thumbnailSize", ArgumentSemantic.Assign)]
		CGSize ThumbnailSize { get; set; }

		[Export ("thumbnailBarHeight")]
		nfloat ThumbnailBarHeight { get; set; }

		[Export ("showPageLabels")]
		bool ShowPageLabels { get; set; }
	}

	[BaseType (typeof (UICollectionViewLayoutAttributes))]
	interface PSPDFThumbnailFlowLayoutAttributes {

		[Export ("pageMode", ArgumentSemantic.Assign)]
		PSPDFDocumentViewLayoutPageMode PageMode { get; set; }
	}

	[BaseType (typeof (UICollectionViewLayout))]
	interface PSPDFThumbnailFlowLayout {

		[Export ("sectionInset", ArgumentSemantic.Assign)]
		UIEdgeInsets SectionInset { get; set; }

		[Export ("interitemSpacing")]
		nfloat InteritemSpacing { get; set; }

		[Export ("lineSpacing")]
		nfloat LineSpacing { get; set; }

		[Export ("singleLineMode")]
		bool SingleLineMode { get; set; }

		[Export ("incompleteLineAlignment", ArgumentSemantic.Assign)]
		PSPDFThumbnailFlowLayoutLineAlignment IncompleteLineAlignment { get; set; }

		[Export ("stickyHeaderEnabled")]
		bool StickyHeaderEnabled { get; set; }
	}

	interface IPSPDFCollectionViewDelegateThumbnailFlowLayout { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFCollectionViewDelegateThumbnailFlowLayout {

		[Export ("collectionView:layout:itemSizeAtIndexPath:")]
		CGSize GetItemSizeAtIndexPath (UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath);

		[Export ("collectionView:layout:itemSizeAtIndexPath:completionHandler:")]
		CGSize GetItemSizeAtIndexPath (UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath, Action<CGSize> completionHandler);

		[Export ("collectionView:layout:referenceSizeForHeaderInSection:")]
		CGSize GetReferenceSizeForHeaderInSection (UICollectionView collectionView, UICollectionViewLayout layout, nint section);

		[Export ("collectionView:layout:pageModeForItemAtIndexPath:")]
		PSPDFDocumentViewLayoutPageMode GetPageModeForItemAtIndexPath (UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath);
	}

	[BaseType (typeof (PSPDFPageCell))]
	interface PSPDFThumbnailGridViewCell : IPSPDFOverridable {

		[NullAllowed, Export ("document", ArgumentSemantic.Strong)]
		PSPDFDocument Document { get; set; }

		[NullAllowed, Export ("bookmarkImageColor", ArgumentSemantic.Strong)]
		UIColor BookmarkImageColor { get; set; }

		// PSPDFThumbnailGridViewCell (SubclassingHooks) Category

		[NullAllowed, Export ("bookmarkImageView")]
		UIImageView BookmarkImageView { get; }

		[Export ("updateBookmarkImage")]
		void UpdateBookmarkImage ();
	}

	interface IPSPDFThumbnailViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFThumbnailViewControllerDelegate {

		[Export ("thumbnailViewController:didSelectPageAtIndex:inDocument:")]
		void DidSelectPageAtIndex (PSPDFThumbnailViewController thumbnailViewController, nuint pageIndex, PSPDFDocument document);
	}

	[BaseType (typeof (UICollectionViewController))]
	interface PSPDFThumbnailViewController : IUICollectionViewDataSource, IUICollectionViewDelegate, PSPDFViewModePresenter, PSPDFCollectionViewDelegateThumbnailFlowLayout, IPSPDFOverridable {

		[NullAllowed, Export ("dataSource", ArgumentSemantic.Weak)]
		IPSPDFPresentationContext DataSource { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFThumbnailViewControllerDelegate Delegate { get; set; }

		[Export ("cellForPageAtIndex:document:")]
		[return: NullAllowed]
		UICollectionViewCell GetCellForPage (nuint pageIndex, [NullAllowed] PSPDFDocument document);

		[Export ("stopScrolling")]
		void StopScrolling ();

		[Export ("refreshResultsForCurrentFilter")]
		void RefreshResultsForCurrentFilter ();

		[Export ("filterOptions", ArgumentSemantic.Copy), Protected]
		NSString [] WeakFilterOptions { get; set; }

		[Export ("activeFilter"), Protected]
		NSString WeakActiveFilter { get; set; }

		[Export ("setActiveFilter:animated:")]
		void SetActiveFilter ([BindAs (typeof (PSPDFThumbnailViewFilter))] NSString activeFilter, bool animated);

		[Export ("cellClass", ArgumentSemantic.Strong)]
		new Class CellClass { get; set; }

		[Static]
		[Export ("automaticThumbnailSizeForPageWithSize:referencePageSize:containerSize:interitemSpacing:")]
		CGSize GetAutomaticThumbnailSizeForPage (CGSize pageSize, CGSize referencePageSize, CGSize containerSize, nfloat interitemSpacing);

		// PSPDFThumbnailViewController (SubclassingHooks) Category

		[Export ("configureCell:forIndexPath:")]
		void ConfigureCell (PSPDFThumbnailGridViewCell cell, NSIndexPath indexPath);

		[Export ("pageForIndexPath:")]
		nuint GetPageForIndexPath (NSIndexPath indexPath);

		[return: NullAllowed]
		[Export ("indexPathForPageAtIndex:")]
		NSIndexPath GetindexPathForPage (nuint pageIndex);

		[NullAllowed, Export ("filterSegment")]
		PSPDFThumbnailFilterSegmentedControl FilterSegment { get; }

		[Export ("updateFilterSegment")]
		void UpdateFilterSegment ();

		[Export ("pagesForFilter:groupingResultsBy:result:completion:")]
		[return: NullAllowed]
		NSProgress GetPages ([BindAs (typeof (PSPDFThumbnailViewFilter))] NSString filter, nuint groupSize, Action<NSIndexSet> resultHandler, Action<bool> completionHandler);

		[Export ("emptyContentLabelForFilter:")]
		[return: NullAllowed]
		string GetEmptyContentLabel ([BindAs (typeof (PSPDFThumbnailViewFilter))] NSString filter);

		[Export ("collectionView:viewForSupplementaryElementOfKind:atIndexPath:")]
		[return: NullAllowed]
		UICollectionReusableView GetCollectionView (UICollectionView collectionView, string kind, NSIndexPath indexPath);

		[Export ("refreshLoadingViewWithLoadingState:")]
		void RefreshLoadingView (bool loadingState);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFToolbar {

		[Field ("PSPDFToolbarDefaultFixedDimensionLength", PSPDFKitGlobal.LibraryPath)]
		nfloat DefaultFixedDimensionLength { get; }

		[Export ("buttons", ArgumentSemantic.Copy)]
		UIButton [] Buttons { get; set; }

		[Export ("setButtons:animated:")]
		void SetButtons (UIButton [] buttons, bool animated);

		[NullAllowed, Export ("backgroundView", ArgumentSemantic.Strong)]
		UIView BackgroundView { get; set; }

		[Export ("contentView")]
		UIView ContentView { get; }

		[NullAllowed, Export ("barTintColor", ArgumentSemantic.Strong)]
		UIColor BarTintColor { get; set; }

		[NullAllowed, Export ("standardAppearance", ArgumentSemantic.Copy)]
		UIToolbarAppearance StandardAppearance { get; set; }

		[NullAllowed, Export ("compactAppearance", ArgumentSemantic.Copy)]
		UIToolbarAppearance CompactAppearance { get; set; }

		[Export ("fixedDimension")]
		nfloat FixedDimension { get; set; }

		[Export ("collapsedButtons", ArgumentSemantic.Copy)]
		UIButton [] CollapsedButtons { get; }

		[Export ("collapsedButton")]
		UIButton CollapsedButton { get; }

		// PSPDFToolbar (SubclassingHooks) Category

		[Export ("layoutMainSubviews")]
		void LayoutMainSubviews ();

		[Export ("setCollapsedButtonVisible:")]
		void SetCollapsedButtonVisible (bool visible);

		[Export ("horizontal")]
		bool Horizontal { [Bind ("isHorizontal")] get; }

		[Export ("buttonSpacing")]
		nfloat ButtonSpacing { get; }
	}

	[BaseType (typeof (PSPDFButton))]
	interface PSPDFToolbarButton : IPSPDFOverridable {

		[NullAllowed, Export ("image", ArgumentSemantic.Strong)]
		UIImage Image { get; set; }

		[NullAllowed, Export ("smallSizeImage", ArgumentSemantic.Strong)]
		UIImage SmallSizeImage { get; set; }

		[Export ("styleForHighlightedState:")]
		void StyleForHighlightedState (bool highlighted);

		[NullAllowed, Export ("userInfo", ArgumentSemantic.Strong)]
		NSObject UserInfo { get; set; }

		[Export ("setEnabled:animated:")]
		void SetEnabled (bool enabled, bool animated);

		[Export ("collapsible")]
		bool Collapsible { [Bind ("isCollapsible")] get; set; }

		[Export ("length")]
		nfloat Length { get; set; }

		[Export ("setLengthToFit")]
		void SetLengthToFit ();

		[Export ("flexible")]
		bool Flexible { [Bind ("isFlexible")] get; set; }

		[NullAllowed, Export ("tintColorDidChangeBlock", ArgumentSemantic.Copy)]
		Action<UIColor> TintColorDidChangeHandler { get; set; }
	}

	[BaseType (typeof (PSPDFToolbarButton))]
	interface PSPDFToolbarSpacerButton {

		[Static]
		[Export ("flexibleSpacerButton")]
		PSPDFToolbarSpacerButton Create ();
	}

	[BaseType (typeof (PSPDFToolbarButton))]
	interface PSPDFToolbarTickerButton {

		[Export ("timeInterval")]
		double TimeInterval { get; set; }

		[Export ("accelerate")]
		bool Accelerate { get; set; }
	}

	[BaseType (typeof (PSPDFToolbarSpacerButton))]
	interface PSPDFToolbarSeparatorButton {

		[Export ("hairlineView")]
		UIView HairlineView { get; }
	}

	[BaseType (typeof (PSPDFToolbarButton))]
	interface PSPDFToolbarSelectableButton {

		[Export ("setSelected:animated:")]
		void SetSelected (bool selected, bool animated);

		[Export ("selectedTintColor", ArgumentSemantic.Strong)]
		UIColor SelectedTintColor { get; set; }

		[Export ("selectedBackgroundColor", ArgumentSemantic.Strong)]
		UIColor SelectedBackgroundColor { get; set; }

		[Export ("selectionPadding")]
		nfloat SelectionPadding { get; set; }

		[Export ("highlightsSelection")]
		bool HighlightsSelection { get; set; }
	}

	[BaseType (typeof (PSPDFToolbarButton))]
	interface PSPDFToolbarGroupButton : IPSPDFOverridable {

		[Export ("groupIndicatorPosition", ArgumentSemantic.Assign)]
		PSPDFToolbarGroupButtonIndicatorPosition GroupIndicatorPosition { get; set; }
	}

	[BaseType (typeof (PSPDFToolbarButton))]
	interface PSPDFToolbarDualButton {

		[NullAllowed, Export ("primaryImage", ArgumentSemantic.Strong)]
		UIImage PrimaryImage { get; set; }

		[NullAllowed, Export ("secondaryImage", ArgumentSemantic.Strong)]
		UIImage SecondaryImage { get; set; }

		[Export ("primaryEnabled")]
		bool PrimaryEnabled { get; set; }

		[Export ("secondaryEnabled")]
		bool SecondaryEnabled { get; set; }
	}

	[BaseType (typeof (PSPDFToolbarGroupButton))]
	interface PSPDFToolbarCollapsedButton {

		[NullAllowed, Export ("mimickedButton", ArgumentSemantic.Strong)]
		UIButton MimickedButton { get; set; }
	}

	[BaseType (typeof (PSPDFLabelView))]
	interface PSPDFDocumentLabelView : IPSPDFOverridable {

	}

	[BaseType (typeof (PSPDFRelayTouchesView))]
	interface PSPDFUserInterfaceView : PSPDFThumbnailBarDelegate, PSPDFScrubberBarDelegate, PSPDFPageLabelViewDelegate, IPSPDFOverridable {

		[Export ("initWithFrame:dataSource:")]
		NativeHandle Constructor (CGRect frame, IPSPDFPresentationContext dataSource);

		[NullAllowed, Export ("dataSource", ArgumentSemantic.Weak)]
		IPSPDFPresentationContext DataSource { get; set; }

		[Export ("layoutSubviewsAnimated:")]
		void LayoutSubviews (bool animated);

		[Export ("reloadData")]
		void ReloadData ();

		[Export ("pageLabelInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets PageLabelInsets { get; set; }

		[Export ("documentLabelInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets DocumentLabelInsets { get; set; }

		[Export ("scrubberBarInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets ScrubberBarInsets { get; set; }

		// PSPDFUserInterfaceView (Subviews) Category

		[Export ("documentLabel")]
		PSPDFDocumentLabelView DocumentLabel { get; }

		[Export ("pageLabel")]
		PSPDFPageLabelView PageLabel { get; }

		[Export ("scrubberBar")]
		PSPDFScrubberBar ScrubberBar { get; }

		[Export ("thumbnailBar")]
		PSPDFThumbnailBar ThumbnailBar { get; }

		[Export ("backButton")]
		PSPDFBackForwardButton BackButton { get; }

		[Export ("forwardButton")]
		PSPDFBackForwardButton ForwardButton { get; }

		[Export ("redactionInfoButton")]
		PSPDFStyleButton RedactionInfoButton { get; }

		// PSPDFUserInterfaceView (SubclassingHooks) Category

		[Export ("updateDocumentLabelFrameAnimated:")]
		void UpdateDocumentLabelFrame (bool animated);

		[Export ("updateThumbnailBarFrameAnimated:")]
		void UpdateThumbnailBarFrame (bool animated);

		[Export ("updateScrubberBarFrameAnimated:")]
		void UpdateScrubberBarFrame (bool animated);

		[Export ("updatePageLabelFrameAnimated:")]
		[Advice ("Requires base call.")]
		void UpdatePageLabelFrame (bool animated);
	}

	delegate void PSPDFUsernameHelperCompletionHandler (string userName);

	[BaseType (typeof (NSObject))]
	interface PSPDFUsernameHelper {

		[Field ("PSPDFUsernameHelperDidDismissViewNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString DidDismissViewNotification { get; }

		[NullAllowed]
		[Static]
		[Export ("defaultAnnotationUsername", ArgumentSemantic.Strong)]
		string DefaultAnnotationUsername { get; set; }

		[Static]
		[Export ("isDefaultAnnotationUserNameSet")]
		bool IsDefaultAnnotationUserNameSet { get; }

		[Static]
		[Async]
		[Export ("askForDefaultAnnotationUsernameIfNeeded:completionBlock:")]
		void AskForDefaultAnnotationUsernameIfNeeded (PSPDFViewController pdfViewController, PSPDFUsernameHelperCompletionHandler completion);

		[Async]
		[Export ("askForDefaultAnnotationUsername:suggestedName:completionBlock:")]
		void AskForDefaultAnnotationUsername (UIViewController viewController, [NullAllowed] string suggestedName, PSPDFUsernameHelperCompletionHandler completion);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFViewController : PSPDFPresentationContext, PSPDFControlDelegate, IPSPDFOverridable, IPSPDFTextSearchDelegate, IUIScreenshotServiceDelegate, PSPDFInlineSearchManagerDelegate, PSPDFErrorHandler, PSPDFExternalURLHandler, PSPDFOutlineViewControllerDelegate, PSPDFBookmarkViewControllerDelegate, PSPDFWebViewControllerDelegate, PSPDFSearchViewControllerDelegate, PSPDFAnnotationTableViewControllerDelegate, IPSPDFBackForwardActionListDelegate, PSPDFFlexibleToolbarContainerDelegate, IMFMailComposeViewControllerDelegate, IMFMessageComposeViewControllerDelegate, PSPDFEmbeddedFilesViewControllerDelegate, PSPDFConflictResolutionManagerDelegate, PSPDFDocumentSharingViewControllerDelegate {

		[Export ("initWithDocument:configuration:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] PSPDFDocument document, [NullAllowed] PSPDFConfiguration configuration);

		[Export ("initWithDocument:")]
		NativeHandle Constructor ([NullAllowed] PSPDFDocument document);

		[NullAllowed, Export ("document", ArgumentSemantic.Strong)]
		new PSPDFDocument Document { get; set; }

		[Export ("selectedAnnotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] SelectedAnnotations { get; set; }

		[NullAllowed, Export ("documentViewController")]
		new PSPDFDocumentViewController DocumentViewController { get; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFViewControllerDelegate Delegate { get; set; }

		[NullAllowed, Export ("formSubmissionDelegate", ArgumentSemantic.Weak)]
		IPSPDFFormSubmissionDelegate FormSubmissionDelegate { get; set; }

		[Export ("reloadData")]
		new void ReloadData ();

		[Export ("reloadPagesAtIndexes:animated:")]
		void ReloadPages (NSIndexSet indexes, bool animated);

		[Export ("pageIndex")]
		new nuint PageIndex { get; set; }

		[Export ("setPageIndex:animated:")]
		new void SetPageIndex (nuint pageIndex, bool animated);

		[NullAllowed, Export ("viewState")]
		PSPDFViewState ViewState { get; }

		[Export ("applyViewState:animateIfPossible:")]
		void ApplyViewState (PSPDFViewState viewState, bool animateIfPossible);

		[Export ("searchForString:options:sender:animated:")]
		new void SearchForString ([NullAllowed] string searchText, [NullAllowed] NSDictionary options, [NullAllowed] NSObject sender, bool animated);

		[Export ("cancelSearchAnimated:")]
		void CancelSearch (bool animated);

		[Export ("searchActive")]
		bool SearchActive { [Bind ("isSearchActive")] get; }

		[Export ("searchHighlightViewManager")]
		PSPDFSearchHighlightViewManager SearchHighlightViewManager { get; }

		[Export ("inlineSearchManager")]
		PSPDFInlineSearchManager InlineSearchManager { get; }

		[Export ("appearanceModeManager")]
		PSPDFAppearanceModeManager AppearanceModeManager { get; }

		[Export ("textSearch")]
		PSPDFTextSearch TextSearch { get; }

		[Export ("executePDFAction:targetRect:pageIndex:animated:actionContainer:")]
		new bool ExecutePdfAction ([NullAllowed] PSPDFAction action, CGRect targetRect, nuint pageIndex, bool animated, [NullAllowed] NSObject actionContainer);

		[Export ("backForwardList")]
		PSPDFBackForwardActionList BackForwardList { get; }

		[Export ("userInterfaceView")]
		PSPDFUserInterfaceView UserInterfaceView { get; }

		[Export ("userInterfaceVisible")]
		new bool UserInterfaceVisible { [Bind ("isUserInterfaceVisible")] get; set; }

		[Export ("setUserInterfaceVisible:animated:")]
		bool SetUserInterfaceVisible (bool show, bool animated);

		[Export ("showControlsAnimated:")]
		new bool ShowControls (bool animated);

		[Export ("hideControlsAnimated:")]
		new bool HideControls (bool animated);

		[Export ("hideControlsAndPageElementsAnimated:")]
		new bool HideControlsAndPageElements (bool animated);

		[Export ("toggleControlsAnimated:")]
		new bool ToggleControls (bool animated);

		[Export ("contentView")]
		PSPDFRelayTouchesView ContentView { get; }

		[Export ("navigationBarHidden")]
		bool NavigationBarHidden { [Bind ("isNavigationBarHidden")] get; }

		[Export ("controllerState")]
		PSPDFControllerState ControllerState { get; }

		[NullAllowed, Export ("controllerStateError")]
		NSError ControllerStateError { get; }

		[NullAllowed, Export ("overlayViewController", ArgumentSemantic.Strong)]
		IPSPDFControllerStateHandling OverlayViewController { get; set; }

		[Export ("pageViewForPageAtIndex:")]
		[return: NullAllowed]
		new PSPDFPageView GetPageView (nuint pageIndex);

		[Export ("viewMode", ArgumentSemantic.Assign)]
		new PSPDFViewMode ViewMode { get; set; }

		[Export ("setViewMode:animated:")]
		new void SetViewMode (PSPDFViewMode viewMode, bool animated);

		[Export ("thumbnailController")]
		PSPDFThumbnailViewController ThumbnailController { get; }

		[Export ("documentEditorController")]
		PSPDFDocumentEditorViewController DocumentEditorController { get; }

		[Export ("visiblePageViews")]
		new PSPDFPageView [] VisiblePageViews { get; }

		[Export ("setUpdateSettingsForBoundsChangeBlock:")]
		void SetUpdateSettingsForBoundsChangeHandler (Action<PSPDFViewController> handler);

		[Export ("shouldResetAppearanceModeWhenViewDisappears")]
		bool ShouldResetAppearanceModeWhenViewDisappears { get; set; }

		// PSPDFViewController (Configuration) Category

		[Export ("configuration", ArgumentSemantic.Copy)]
		new PSPDFConfiguration Configuration { get; }

		[Export ("updateConfigurationWithBuilder:")]
		void UpdateConfiguration (Action<PSPDFConfigurationBuilder> builderHandler);

		[Export ("updateConfigurationWithoutReloadingWithBuilder:")]
		void UpdateConfigurationWithoutReloading (Action<PSPDFConfigurationBuilder> builderHandler);

		// PSPDFViewController (Presentation) Category

		[Async]
		[Export ("presentViewController:options:animated:sender:completion:")]
		new bool PresentViewController (UIViewController controller, [NullAllowed] NSDictionary options, bool animated, [NullAllowed] NSObject sender, [NullAllowed] Action completion);

		[Async]
		[Export ("dismissViewControllerOfClass:animated:completion:")]
		new bool DismissViewController ([NullAllowed] Class controllerClass, bool animated, [NullAllowed] Action completion);

		[Async]
		[Export ("presentPDFViewControllerWithDocument:options:sender:animated:configurationBlock:completion:")]
		void PresentPdfViewController (PSPDFDocument document, [NullAllowed] NSDictionary options, [NullAllowed] NSObject sender, bool animated, [NullAllowed] Action<PSPDFViewController> configurationHandler, [NullAllowed] Action completion);

		[Async]
		[Export ("presentPreviewControllerForURL:title:options:sender:animated:completion:")]
		new void PresentPreviewController (NSUrl fileUrl, [NullAllowed] string title, [NullAllowed] NSDictionary options, [NullAllowed] NSObject sender, bool animated, [NullAllowed] Action completion);

		// PSPDFViewController (Annotations) Category

		[Export ("annotationStateManager")]
		new PSPDFAnnotationStateManager AnnotationStateManager { get; }

		// PSPDFViewController (Toolbar) Category

		[Export ("navigationItem"), New]
		PSPDFNavigationItem NavigationItem { get; }

		[Export ("closeButtonItem")]
		UIBarButtonItem CloseButtonItem { get; }

		[Export ("outlineButtonItem")]
		UIBarButtonItem OutlineButtonItem { get; }

		[Export ("searchButtonItem")]
		UIBarButtonItem SearchButtonItem { get; }

		[Export ("readerViewButtonItem")]
		UIBarButtonItem ReaderViewButtonItem { get; }

		[Export ("thumbnailsButtonItem")]
		UIBarButtonItem ThumbnailsButtonItem { get; }

		[Export ("documentEditorButtonItem")]
		UIBarButtonItem DocumentEditorButtonItem { get; }

		[Export ("printButtonItem")]
		UIBarButtonItem PrintButtonItem { get; }

		[Export ("openInButtonItem")]
		UIBarButtonItem OpenInButtonItem { get; }

		[Export ("emailButtonItem")]
		UIBarButtonItem EmailButtonItem { get; }

		[Export ("messageButtonItem")]
		UIBarButtonItem MessageButtonItem { get; }

		[Export ("annotationButtonItem")]
		UIBarButtonItem AnnotationButtonItem { get; }

		[Export ("signatureButtonItem")]
		UIBarButtonItem SignatureButtonItem { get; }

		[Export ("bookmarkButtonItem")]
		UIBarButtonItem BookmarkButtonItem { get; }

		[Export ("brightnessButtonItem")]
		UIBarButtonItem BrightnessButtonItem { get; }

		[Export ("activityButtonItem")]
		UIBarButtonItem ActivityButtonItem { get; }

		[Export ("settingsButtonItem")]
		UIBarButtonItem SettingsButtonItem { get; }

		[Export ("contentEditingButtonItem")]
		UIBarButtonItem ContentEditingButtonItem { get; }

		[Export ("barButtonItemsAlwaysEnabled", ArgumentSemantic.Copy)]
		UIBarButtonItem [] BarButtonItemsAlwaysEnabled { get; set; }

		[Export ("documentInfoCoordinator")]
		PSPDFDocumentInfoCoordinator DocumentInfoCoordinator { get; }

		[NullAllowed, Export ("annotationToolbarController")]
		new PSPDFAnnotationToolbarController AnnotationToolbarController { get; }

		// PSPDFViewController (SubclassingHooks) Category

		[Export ("commonInitWithDocument:configuration:")]
		[Advice ("Requires base call if override.")]
		void CommonInit ([NullAllowed] PSPDFDocument document, PSPDFConfiguration configuration);

		[Export ("documentViewControllerDidLoad")]
		void DocumentViewControllerDidLoad ();

		[Export ("updateToolbarAnimated:")]
		void UpdateToolbar (bool animated);

		[Export ("contentRect")]
		new CGRect ContentRect { get; }

		[Export ("annotationButtonPressed:")]
		void AnnotationButtonPressed ([NullAllowed] NSObject sender);

		[Export ("handleAutosaveRequestForDocument:reason:")]
		void HandleAutosaveRequest (PSPDFDocument document, PSPDFAutosaveReason reason);

		[Export ("interactions")]
		IPSPDFDocumentViewInteractions Interactions { get; }
	}

	interface IPSPDFViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFViewControllerDelegate {

		[Export ("pdfViewController:shouldSaveDocument:withOptions:")]
		bool ShouldSaveDocument (PSPDFViewController pdfController, PSPDFDocument document, out NSDictionary options);

		[Export ("pdfViewController:didSaveDocument:error:")]
		void DidSaveDocument (PSPDFViewController pdfController, PSPDFDocument document, [NullAllowed] NSError error);

		[Export ("pdfViewController:shouldChangeDocument:")]
		bool ShouldChangeDocument (PSPDFViewController pdfController, [NullAllowed] PSPDFDocument document);

		[Export ("pdfViewController:didChangeDocument:")]
		void DidChangeDocument (PSPDFViewController pdfController, [NullAllowed] PSPDFDocument document);

		[Export ("pdfViewController:willScheduleRenderTaskForPageView:")]
		void WillScheduleRenderTask (PSPDFViewController pdfController, PSPDFPageView pageView);

		[Export ("pdfViewController:didFinishRenderTaskForPageView:")]
		void DidFinishRenderTask (PSPDFViewController pdfController, PSPDFPageView pageView);

		[Export ("pdfViewController:didUpdateContentImageForPageView:isPlaceholder:")]
		void DidUpdateContentImage (PSPDFViewController pdfController, PSPDFPageView pageView, bool placeholder);

		[Export ("pdfViewController:willBeginDisplayingPageView:forPageAtIndex:")]
		void WillBeginDisplayingPageView (PSPDFViewController pdfController, PSPDFPageView pageView, nint pageIndex);

		[Export ("pdfViewController:didEndDisplayingPageView:forPageAtIndex:")]
		void DidEndDisplayingPageView (PSPDFViewController pdfController, PSPDFPageView pageView, nint pageIndex);

		[Export ("pdfViewController:didConfigurePageView:forPageAtIndex:")]
		void DidConfigurePageView (PSPDFViewController pdfController, PSPDFPageView pageView, nint pageIndex);

		[Export ("pdfViewController:didCleanupPageView:forPageAtIndex:")]
		void DidCleanupPageView (PSPDFViewController pdfController, PSPDFPageView pageView, nint pageIndex);

		[Export ("pdfViewController:documentForRelativePath:")]
		[return: NullAllowed]
		PSPDFDocument GetDocumentForRelativePath (PSPDFViewController pdfController, string relativePath);

		[Export ("pdfViewController:shouldSelectText:withGlyphs:atRect:onPageView:")]
		bool ShouldSelectText (PSPDFViewController pdfController, string text, PSPDFGlyph [] glyphs, CGRect rect, PSPDFPageView pageView);

		[Export ("pdfViewController:didSelectText:withGlyphs:atRect:onPageView:")]
		void DidSelectText (PSPDFViewController pdfController, string text, PSPDFGlyph [] glyphs, CGRect rect, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldDisplayAnnotation:onPageView:")]
		bool ShouldDisplayAnnotation (PSPDFViewController pdfController, PSPDFAnnotation annotation, PSPDFPageView pageView);

		[Export ("pdfViewController:didTapOnAnnotation:annotationPoint:annotationView:pageView:viewPoint:")]
		bool DidTapOnAnnotation (PSPDFViewController pdfController, PSPDFAnnotation annotation, CGPoint annotationPoint, [NullAllowed] IPSPDFAnnotationPresenting annotationView, PSPDFPageView pageView, CGPoint viewPoint);

		[Export ("pdfViewController:shouldSelectAnnotations:onPageView:")]
		PSPDFAnnotation [] ShouldSelectAnnotations (PSPDFViewController pdfController, PSPDFAnnotation [] annotations, PSPDFPageView pageView);

		[Export ("pdfViewController:didSelectAnnotations:onPageView:")]
		void DidSelectAnnotations (PSPDFViewController pdfController, PSPDFAnnotation [] annotations, PSPDFPageView pageView);

		[Export ("pdfViewController:didDeselectAnnotations:onPageView:")]
		void DidDeselectAnnotations (PSPDFViewController pdfController, PSPDFAnnotation [] annotations, PSPDFPageView pageView);

		[Export ("pdfViewController:annotationView:forAnnotation:onPageView:")]
		[return: NullAllowed]
		IPSPDFAnnotationPresenting GetAnnotationView (PSPDFViewController pdfController, [NullAllowed] IPSPDFAnnotationPresenting annotationView, PSPDFAnnotation annotation, PSPDFPageView pageView);

		[Export ("pdfViewController:willShowAnnotationView:onPageView:")]
		void WillShowAnnotationView (PSPDFViewController pdfController, IPSPDFAnnotationPresenting annotationView, PSPDFPageView pageView);

		[Export ("pdfViewController:didShowAnnotationView:onPageView:")]
		void DidShowAnnotationView (PSPDFViewController pdfController, IPSPDFAnnotationPresenting annotationView, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldShowController:options:animated:")]
		bool ShouldShowController (PSPDFViewController pdfController, UIViewController controller, [NullAllowed] NSDictionary options, bool animated);

		[Export ("pdfViewController:didShowController:options:animated:")]
		void DidShowController (PSPDFViewController pdfController, UIViewController controller, [NullAllowed] NSDictionary options, bool animated);

		[Export ("pdfViewController:didChangeViewMode:")]
		void DidChangeViewMode (PSPDFViewController pdfController, PSPDFViewMode viewMode);

		[Export ("pdfViewControllerWillDismiss:")]
		void PdfViewControllerWillDismiss (PSPDFViewController pdfController);

		[Export ("pdfViewControllerDidDismiss:")]
		void PdfViewControllerDidDismiss (PSPDFViewController pdfController);

		[Export ("pdfViewController:didChangeControllerState:error:")]
		void DidChangeControllerState (PSPDFViewController pdfController, PSPDFControllerState controllerState, [NullAllowed] NSError error);

		[Export ("pdfViewController:shouldShowUserInterface:")]
		bool ShouldShowUserInterface (PSPDFViewController pdfController, bool animated);

		[Export ("pdfViewController:didShowUserInterface:")]
		void DidShowUserInterface (PSPDFViewController pdfController, bool animated);

		[Export ("pdfViewController:shouldHideUserInterface:")]
		bool ShouldHideUserInterface (PSPDFViewController pdfController, bool animated);

		[Export ("pdfViewController:didHideUserInterface:")]
		void DidHideUserInterface (PSPDFViewController pdfController, bool animated);

		[Export ("pdfViewController:shouldExecuteAction:")]
		bool ShouldExecuteAction (PSPDFViewController pdfController, PSPDFAction action);

		[Export ("pdfViewController:didExecuteAction:")]
		void DidExecuteAction (PSPDFViewController pdfController, PSPDFAction action);

		[Export ("pdfViewController:menuForCreatingAnnotationAtPoint:onPageView:appearance:suggestedMenu:")]
		UIMenu GetMenuForCreatingAnnotation (PSPDFViewController sender, CGPoint point, PSPDFPageView pageView, PSPDFEditMenuAppearance appearance, UIMenu suggestedMenu);

		[Export ("pdfViewController:menuForAnnotations:onPageView:appearance:suggestedMenu:")]
		UIMenu GetMenuForAnnotations (PSPDFViewController sender, PSPDFAnnotation[] annotations, PSPDFPageView pageView, PSPDFEditMenuAppearance appearance, UIMenu suggestedMenu);

		[Export ("pdfViewController:menuForText:onPageView:appearance:suggestedMenu:")]
		UIMenu GetMenuForText (PSPDFViewController sender, PSPDFGlyphSequence glyphs, PSPDFPageView pageView, PSPDFEditMenuAppearance appearance, UIMenu suggestedMenu);

		[Export ("pdfViewController:menuForImage:onPageView:appearance:suggestedMenu:")]
		UIMenu GetMenuForImage (PSPDFViewController sender, PSPDFImageInfo image, PSPDFPageView pageView, PSPDFEditMenuAppearance appearance, UIMenu suggestedMenu);

		[Obsolete ("Use 'pdfViewController(_:menuForAnnotations:onPageView:appearance:suggestedMenu:)' or 'pdfViewController(_:menuForCreatingAnnotationAt:onPageView:appearance:suggestedMenu:)' instead.")]
		[Export ("pdfViewController:shouldShowMenuItems:atSuggestedTargetRect:forAnnotations:inRect:onPageView:")]
		PSPDFMenuItem [] ShouldShowMenuItemsForAnnotations (PSPDFViewController pdfController, PSPDFMenuItem [] menuItems, CGRect rect, [NullAllowed] PSPDFAnnotation [] annotations, CGRect annotationRect, PSPDFPageView pageView);

		[Obsolete]
		[Export ("pdfViewController:shouldShowMenuItems:atSuggestedTargetRect:forSelectedText:inRect:onPageView:")]
		PSPDFMenuItem [] ShouldShowMenuItemsForSelectedText (PSPDFViewController pdfController, PSPDFMenuItem [] menuItems, CGRect rect, string selectedText, CGRect textRect, PSPDFPageView pageView);

		[Obsolete]
		[Export ("pdfViewController:shouldShowMenuItems:atSuggestedTargetRect:forSelectedImage:inRect:onPageView:")]
		PSPDFMenuItem [] ShouldShowMenuItemsForSelectedImage (PSPDFViewController pdfController, PSPDFMenuItem [] menuItems, CGRect rect, PSPDFImageInfo selectedImage, CGRect textRect, PSPDFPageView pageView);
	}

	interface IPSPDFViewModePresenter { }

	[Protocol]
	interface PSPDFViewModePresenter {

		[Abstract]
		[Export ("initWithCollectionViewLayout:")]
		NativeHandle Constructor ([NullAllowed] UICollectionViewLayout layout);

		[Abstract]
		[Export ("initWithDocument:")]
		NativeHandle Constructor ([NullAllowed] PSPDFDocument document);

		[Abstract]
		[NullAllowed, Export ("document", ArgumentSemantic.Strong)]
		PSPDFDocument Document { get; set; }

		[Abstract]
		[NullAllowed, Export ("presentationContext", ArgumentSemantic.Weak)]
		IPSPDFPresentationContext PresentationContext { get; set; }

		[Abstract]
		[Export ("cellClass", ArgumentSemantic.Strong)]
		Class CellClass { get; set; }

		[Abstract]
		[Export ("fixedItemSizeEnabled")]
		bool FixedItemSizeEnabled { get; set; }

		[Abstract]
		[Export ("visiblePageIndexes")]
		NSIndexSet VisiblePageIndexes { get; }

		[Abstract]
		[Export ("scrollToPageAtIndex:document:animated:")]
		void ScrollToPage (nuint pageIndex, [NullAllowed] PSPDFDocument document, bool animated);
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFViewState {

		[Export ("initWithPageIndex:viewPort:selectionState:selectedAnnotationNames:")]
		[DesignatedInitializer]
		NativeHandle Constructor (nuint pageIndex, CGRect viewPort, [NullAllowed] PSPDFSelectionState selectionState, [NullAllowed] string [] selectedAnnotationNames);

		[Export ("initWithPageIndex:viewPort:selectionState:")]
		NativeHandle Constructor (nuint pageIndex, CGRect viewPort, [NullAllowed] PSPDFSelectionState selectionState);

		[Export ("initWithPageIndex:selectionState:")]
		NativeHandle Constructor (nuint pageIndex, [NullAllowed] PSPDFSelectionState selectionState);

		[Export ("initWithPageIndex:viewPort:")]
		NativeHandle Constructor (nuint pageIndex, CGRect viewPort);

		[Export ("initWithPageIndex:")]
		NativeHandle Constructor (nuint pageIndex);

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[Export ("viewPort")]
		CGRect ViewPort { get; }

		[Export ("hasViewPort")]
		bool HasViewPort { get; }

		[NullAllowed, Export ("selectionState")]
		PSPDFSelectionState SelectionState { get; }

		[Export ("isEqualToViewState:withAccuracy:")]
		bool IsEqualTo ([NullAllowed] PSPDFViewState other, nfloat leeway);
	}

	interface IPSPDFVisiblePagesDataSource { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFVisiblePagesDataSource {

		[Abstract]
		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[Abstract]
		[Export ("visiblePageIndexes")]
		NSIndexSet VisiblePageIndexes { get; }
	}

	interface IPSPDFWebViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFWebViewControllerDelegate : PSPDFExternalURLHandler {

		[Export ("webViewControllerDidStartLoad:")]
		void DidStartLoad (PSPDFWebViewController controller);

		[Export ("webViewControllerDidFinishLoad:")]
		void DidFinishLoad (PSPDFWebViewController controller);

		[Export ("webViewController:didFailLoadWithError:")]
		void DidFailLoad (PSPDFWebViewController controller, NSError error);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFWebViewController : PSPDFStyleable, IPSPDFOverridable {

		[Static]
		[Export ("modalWebViewWithURL:")]
		UINavigationController GetModalWebView (NSUrl url);

		[Export ("initWithURLRequest:")]
		NativeHandle Constructor (NSUrlRequest request);

		[Export ("initWithURL:")]
		NativeHandle Constructor (NSUrl url);

		[Export ("availableActions", ArgumentSemantic.Assign)]
		PSPDFWebViewControllerAvailableActions AvailableActions { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFWebViewControllerDelegate Delegate { get; set; }

		[Export ("webView")]
		WKWebView WebView { get; }

		[Export ("showProgressIndicator")]
		bool ShowProgressIndicator { get; set; }

		[Export ("useCustomErrorPage")]
		bool UseCustomErrorPage { get; set; }

		[Export ("shouldUpdateTitleFromWebContent")]
		bool ShouldUpdateTitleFromWebContent { get; set; }

		[Export ("excludedActivities", ArgumentSemantic.Copy)]
		NSString [] ExcludedActivities { get; set; }

		[Export ("suppressesIncrementalRendering")]
		bool SuppressesIncrementalRendering { get; set; }

		// PSPDFWebViewController (SubclassingHooks) Category

		[Export ("showHTMLWithError:")]
		void ShowHtml (NSError error);

		[NullAllowed, Export ("createDefaultActivityViewController")]
		UIActivityViewController CreateDefaultActivityViewController { get; }

		[Export ("goBack:")]
		void GoBack ([NullAllowed] NSObject sender);

		[Export ("goForward:")]
		void GoForward ([NullAllowed] NSObject sender);

		[Export ("reload:")]
		void Reload ([NullAllowed] NSObject sender);

		[Export ("stop:")]
		void Stop ([NullAllowed] NSObject sender);

		[Export ("action:")]
		void Action ([NullAllowed] UIBarButtonItem sender);

		[Export ("done:")]
		void Done ([NullAllowed] NSObject sender);
	}

	[Category]
	[BaseType (typeof (UISearchController))]
	interface UISearchController_PSPDFKitAdditions {

		[return: NullAllowed]
		[Export ("pspdf_searchResultsTableView")]
		UITableView GetPsPdfSearchResultsTableView ();

		[Export ("pspdf_installWorkaroundsOn:")]
		void PsPdf_InstallWorkarounds (UIViewController controller);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFContainerView {

		[Export ("initWithFrame:")]
		NativeHandle Constructor (CGRect frame);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFPickerTableViewController : IPSPDFOverridable {

	}

	interface IPSPDFStatusHUDItemDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFStatusHUDItemDelegate {

		[Abstract]
		[Export ("statusHUDItemDidTapActionButton:")]
		void DidTapActionButton ([NullAllowed] PSPDFStatusHUDItem hudItem);
	}

	[Protocol]
	interface PSPDFSegmentImageProviding {

		[Abstract]
		[NullAllowed, Export ("segmentImage")]
		UIImage SegmentImage { get; }
	}

	interface IPSPDFDocumentInfoViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentInfoViewControllerDelegate {

		[Export ("documentInfoViewControllerDidCancelUpdates:")]
		void DidCancelUpdates (PSPDFDocumentInfoViewController infoController);

		[Export ("documentInfoViewControllerDidCommitUpdates:")]
		void DidCommitUpdates (PSPDFDocumentInfoViewController infoController);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	[DisableDefaultCtor]
	interface PSPDFDocumentInfoViewController : PSPDFDocumentInfoController, PSPDFSegmentImageProviding, PSPDFStyleable, IPSPDFOverridable {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFDocumentInfoViewControllerDelegate Delegate { get; set; }

		[NullAllowed, Export ("allowEditing")]
		bool AllowEditing { get; set; }

		[NullAllowed, Export ("rightActionButtonItems", ArgumentSemantic.Copy)]
		UIBarButtonItem [] RightActionButtonItems { get; set; }

		[NullAllowed, Export ("leftActionButtonItems", ArgumentSemantic.Copy)]
		UIBarButtonItem [] LeftActionButtonItems { get; set; }

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] PSPDFDocument document);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	[DisableDefaultCtor]
	interface PSPDFDocumentSecurityViewController : PSPDFDocumentInfoController, PSPDFStyleable, IPSPDFOverridable {

		[NullAllowed, Export ("allowEditing")]
		bool AllowEditing { get; set; }

		[NullAllowed, Export ("rightActionButtonItems", ArgumentSemantic.Copy)]
		UIBarButtonItem [] RightActionButtonItems { get; set; }

		[NullAllowed, Export ("leftActionButtonItems", ArgumentSemantic.Copy)]
		UIBarButtonItem [] LeftActionButtonItems { get; set; }

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] PSPDFDocument document);
	}

	[Static]
	interface PSPDFAnnotationStyleKeys {
		[Field ("PSPDFAnnotationStyleKeyColor", PSPDFKitGlobal.LibraryPath)]
		NSString ColorKey { get; }

		[Field ("PSPDFAnnotationStyleKeyFillColor", PSPDFKitGlobal.LibraryPath)]
		NSString FillColorKey { get; }

		[Field ("PSPDFAnnotationStyleKeyAlpha", PSPDFKitGlobal.LibraryPath)]
		NSString AlphaKey { get; }

		[Field ("PSPDFAnnotationStyleKeyLineWidth", PSPDFKitGlobal.LibraryPath)]
		NSString LineWidthKey { get; }

		[Field ("PSPDFAnnotationStyleKeyDashArray", PSPDFKitGlobal.LibraryPath)]
		NSString DashArrayKey { get; }

		[Field ("PSPDFAnnotationStyleKeyLineEnd", PSPDFKitGlobal.LibraryPath)]
		NSString LineEndKey { get; }

		[Field ("PSPDFAnnotationStyleKeyLineEnd1", PSPDFKitGlobal.LibraryPath)]
		NSString LineEnd1Key { get; }

		[Field ("PSPDFAnnotationStyleKeyLineEnd2", PSPDFKitGlobal.LibraryPath)]
		NSString LineEnd2Key { get; }

		[Field ("PSPDFAnnotationStyleKeyFontName", PSPDFKitGlobal.LibraryPath)]
		NSString FontNameKey { get; }

		[Field ("PSPDFAnnotationStyleKeyFontSize", PSPDFKitGlobal.LibraryPath)]
		NSString FontSizeKey { get; }

		[Field ("PSPDFAnnotationStyleKeyTextAlignment", PSPDFKitGlobal.LibraryPath)]
		NSString TextAlignmentKey { get; }

		[Field ("PSPDFAnnotationStyleKeyBlendMode", PSPDFKitGlobal.LibraryPath)]
		NSString BlendModeKey { get; }

		[Field ("PSPDFAnnotationStyleKeyCalloutAction", PSPDFKitGlobal.LibraryPath)]
		NSString CalloutActionKey { get; }

		[Field ("PSPDFAnnotationStyleKeyColorPreset", PSPDFKitGlobal.LibraryPath)]
		NSString ColorPresetKey { get; }

		[Field ("PSPDFAnnotationStyleKeyOutlineColor", PSPDFKitGlobal.LibraryPath)]
		NSString OutlineColorKey { get; }

		[Field ("PSPDFAnnotationStyleKeyOverlayText", PSPDFKitGlobal.LibraryPath)]
		NSString OverlayTextKey { get; }

		[Field ("PSPDFAnnotationStyleKeyRepeatOverlayText", PSPDFKitGlobal.LibraryPath)]
		NSString RepeatOverlayTextKey { get; }

		[Field ("PSPDFAnnotationStyleKeyMeasurementSnapping", PSPDFKitGlobal.LibraryPath)]
		NSString MeasurementSnapping { get; }

		[Field ("PSPDFAnnotationStyleKeyContents", PSPDFKitGlobal.LibraryPath)]
		NSString Contents { get; }

		[Field ("PSPDFAnnotationStyleKeyMeasurementValueConfiguration", PSPDFKitGlobal.LibraryPath)]
		NSString MeasurementValueConfiguration { get; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFProgressLabelView {

		[Export ("initWithFrame:")]
		NativeHandle Constructor (CGRect frame);

		[Export ("titleColor", ArgumentSemantic.Strong)]
		UIColor TitleColor { get; set; }

		[Export ("progressIndicatorColor", ArgumentSemantic.Strong)]
		UIColor ProgressIndicatorColor { get; set; }
	}

	[BaseType (typeof (PSPDFButton))]
	interface PSPDFStyleButton {

		[Export ("buttonStyle", ArgumentSemantic.Assign)]
		PSPDFButtonStyle ButtonStyle { get; set; }

		[Export ("blurEffectStyle", ArgumentSemantic.Assign)]
		UIBlurEffectStyle BlurEffectStyle { get; set; }
	}

	[Static]
	interface PSPDFDocumentSharingUserInfoKeys {

		[Field ("PSPDFDocumentSharingExportedURL", PSPDFKitGlobal.LibraryPath)]
		NSString ExportedUrlKey { get; }

		[Field ("PSPDFDocumentSharingSelectedDocumentPicker", PSPDFKitGlobal.LibraryPath)]
		NSString SelectedDocumentPickerKey { get; }

		[Field ("PSPDFDocumentSharingSelectedActivityType", PSPDFKitGlobal.LibraryPath)]
		NSString SelectedActivityTypeKey { get; }

		[Field ("PSPDFDocumentSharingPrintInteractionController", PSPDFKitGlobal.LibraryPath)]
		NSString PrintInteractionControllerKey { get; }
	}

	[StrongDictionary ("PSPDFDocumentSharingUserInfoKeys")]
	interface PSPDFDocumentSharingUserInfo {
		NSUrl ExportedUrl { get; set; }
		UIDocumentPickerViewController SelectedDocumentPicker { get; set; }
		NSString SelectedActivityType { get; set; }
		UIPrintInteractionController PrintInteractionController { get; set; }
	}

	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFDocumentSharingConfiguration {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFDocumentSharingConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		NativeHandle Constructor (PSPDFDocumentSharingConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFDocumentSharingConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFDocumentSharingConfigurationBuilder> builderHandler);

		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFDocumentSharingConfiguration GetUpdatedConfiguration ([NullAllowed] Action<PSPDFDocumentSharingConfigurationBuilder> builderHandler);

		[Static]
		[Export ("defaultConfigurationForDestination:")]
		PSPDFDocumentSharingConfiguration GetDefaultConfiguration ([BindAs (typeof (PSPDFDocumentSharingDestination))] NSString destination);

		[Export ("fileFormatOptions")]
		PSPDFDocumentSharingFileFormatOptions FileFormatOptions { get; }

		[Export ("pageSelectionOptions")]
		PSPDFDocumentSharingPagesOptions PageSelectionOptions { get; }

		[Export ("annotationOptions")]
		PSPDFDocumentSharingAnnotationOptions AnnotationOptions { get; }

		[BindAs (typeof (PSPDFDocumentSharingDestination))]
		[Export ("destination")]
		NSString Destination { get; }

		[Export ("printConfiguration")]
		PSPDFPrintConfiguration PrintConfiguration { get; }

		[Export ("applicationActivities", ArgumentSemantic.Copy)]
		UIActivity [] ApplicationActivities { get; }

		[Advice ("You can use 'ExcludedActivityTypes' for a strongly typed access")]
		[Export ("excludedActivityTypes", ArgumentSemantic.Copy)]
		NSString [] WeakExcludedActivityTypes { get; }

		[Export ("pageDescriptionProvider", ArgumentSemantic.Copy)]
		Func<nuint, PSPDFDocument, string> PageDescriptionProvider { get; }

		[Export ("selectedPagesDescriptionProvider", ArgumentSemantic.Copy)]
		Func<PSPDFDocumentSharingPagesOptions, NSIndexSet, PSPDFDocument [], string> SelectedPagesDescriptionProvider { get; }
	}

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFDocumentSharingConfigurationBuilder {

		[Export ("fileFormatOptions")]
		PSPDFDocumentSharingFileFormatOptions FileFormatOptions { get; set; }

		[Export ("pageSelectionOptions")]
		PSPDFDocumentSharingPagesOptions PageSelectionOptions { get; set; }

		[Export ("annotationOptions")]
		PSPDFDocumentSharingAnnotationOptions AnnotationOptions { get; set; }

		[BindAs (typeof (PSPDFDocumentSharingDestination))]
		[Export ("destination")]
		NSString Destination { get; set; }

		[Export ("printConfiguration")]
		PSPDFPrintConfiguration PrintConfiguration { get; set; }

		[Export ("applicationActivities", ArgumentSemantic.Copy)]
		UIActivity [] ApplicationActivities { get; set; }

		[Advice ("You can use 'ExcludedActivityTypes' for a strongly typed access")]
		[Export ("excludedActivityTypes", ArgumentSemantic.Copy)]
		NSString [] WeakExcludedActivityTypes { get; set; }

		[Export ("pageDescriptionProvider", ArgumentSemantic.Copy)]
		Func<nuint, PSPDFDocument, string> PageDescriptionProvider { get; set; }

		[Export ("selectedPagesDescriptionProvider", ArgumentSemantic.Copy)]
		Func<PSPDFDocumentSharingPagesOptions, NSIndexSet, PSPDFDocument [], string> SelectedPagesDescriptionProvider { get; set; }
	}

	interface IPSPDFConflictResolutionManagerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFConflictResolutionManagerDelegate {

		[Abstract]
		[Export ("resolutionManager:requestingFileConflictResolutionForDocument:dataProvider:withResolution:error:")]
		bool RequestingFileConflictResolution (PSPDFConflictResolutionManager manager, PSPDFDocument document, IPSPDFCoordinatedFileDataProviding dataProvider, PSPDFFileConflictResolution resolution, out NSError error);

		[Export ("resolutionManager:shouldPerformAutomaticResolutionForForDocument:dataProvider:conflictType:resolution:")]
		bool ShouldPerformAutomaticResolution (PSPDFConflictResolutionManager manager, PSPDFDocument document, IPSPDFCoordinatedFileDataProviding dataProvider, PSPDFFileConflictType type, ref PSPDFFileConflictResolution resolution);

		[Export ("viewControllerForPresentationForResolutionManager:")]
		UIViewController GetViewControllerForPresentation (PSPDFConflictResolutionManager manager);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFConflictResolutionManager : IPSPDFOverridable {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFConflictResolutionManagerDelegate Delegate { get; set; }

		[Export ("handleUnderlyingFileChangedNotification:")]
		void HandleUnderlyingFileChangedNotification (NSNotification notification);

		// PSPDFConflictResolutionManager (SubclassingHooks) Category

		[return: NullAllowed]
		[Export ("controllerForFileDeletionResolutionOnDocument:dataProvider:")]
		UIViewController GetControllerForFileDeletionResolutionOnDocument (PSPDFDocument document, IPSPDFCoordinatedFileDataProviding dataProvider);

		[return: NullAllowed]
		[Export ("controllerForExternalFileChangeResolutionOnDocument:dataProvider:")]
		UIViewController GetControllerForExternalFileChangeResolutionOnDocument (PSPDFDocument document, IPSPDFCoordinatedFileDataProviding dataProvider);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationPlaceholder {

		[Export ("placeholderState")]
		PSPDFAnnotationPlaceholderState PlaceholderState { get; }

		[Export ("contentIcon")]
		UIImage ContentIcon { get; }

		[NullAllowed, Export ("progress")]
		NSProgress Progress { get; }

		[NullAllowed, Export ("error")]
		NSError Error { get; }

		[NullAllowed, Export ("localizedAction")]
		string LocalizedAction { get; }

		[Export ("resolveActualContent")]
		void ResolveActualContent ();

		[Export ("cancelResolution:")]
		[return: NullAllowed]
		PSPDFAnnotationPlaceholder CancelResolution ([NullAllowed] out NSError error);

		[Export ("replacementForReattemptingResolution:")]
		[return: NullAllowed]
		PSPDFAnnotationPlaceholder GetReplacementForReattemptingResolution ([NullAllowed] out NSError error);
	}

	[BaseType (typeof (PSPDFNonAnimatingTableViewCell))]
	interface PSPDFThumbnailTextCell {

		[Export ("textField")]
		UITextField TextField { get; }

		[Export ("detailLabel")]
		UILabel DetailLabel { get; }

		[Export ("adornmentLabel")]
		UILabel AdornmentLabel { get; }

		[Export ("pageImageView")]
		UIImageView PageImageView { get; }
	}

	interface IPSPDFLinkAnnotationEditingContainerViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFLinkAnnotationEditingContainerViewControllerDelegate {

		[Export ("linkAnnotationEditingContainerViewControllerDidCancel:")]
		void DidCancelCreatingLinkAnnotation (PSPDFLinkAnnotationEditingContainerViewController linkAnnotationEditingContainerViewController);

		[Export ("linkAnnotationEditingContainerViewController:didFinishCreatingLinkAnnotation:")]
		void DidFinishCreatingLinkAnnotation (PSPDFLinkAnnotationEditingContainerViewController linkAnnotationEditingContainerViewController, PSPDFLinkAnnotation linkAnnotation);

		[Export ("linkAnnotationEditingContainerViewController:didFinishEditingLinkAnnotation:")]
		void DidFinishEditingLinkAnnotation (PSPDFLinkAnnotationEditingContainerViewController linkAnnotationEditingContainerViewController, PSPDFLinkAnnotation linkAnnotation);
	}

	[BaseType (typeof (PSPDFContainerViewController))]
	[DisableDefaultCtor]
	interface PSPDFLinkAnnotationEditingContainerViewController {

		[Export ("initWithDocument:pageIndex:selectedRects:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocument document, nuint pageIndex, [BindAs (typeof (CGRect []))] NSValue [] selectedRects);

		[Export ("initWithExistingLinkAnnotation:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFLinkAnnotation linkAnnotation);

		[NullAllowed, Export ("linkDelegate", ArgumentSemantic.Weak)]
		IPSPDFLinkAnnotationEditingContainerViewControllerDelegate LinkDelegate { get; set; }
	}

	interface IPSPDFLinkAnnotationEditingViewControllerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFLinkAnnotationEditingViewControllerDelegate {

		[Abstract]
		[Export ("linkAnnotationEditingViewController:shouldEnableDoneButton:")]
		void ShouldEnableDoneButton (PSPDFLinkAnnotationEditingViewController linkAnnotationEditingViewController, bool doneButtonEnabled);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	[DisableDefaultCtor]
	interface PSPDFLinkAnnotationEditingViewController {

		[Export ("initWithDocument:existingAction:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocument document, [NullAllowed] PSPDFAction action);

		[Export ("document")]
		PSPDFDocument Document { get; }

		[NullAllowed, Export ("existingAction")]
		PSPDFAction ExistingAction { get; }

		[NullAllowed, Export ("linkAction")]
		PSPDFAction LinkAction { get; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFLinkAnnotationEditingViewControllerDelegate Delegate { get; set; }

		[Export ("linkType")]
		PSPDFLinkAnnotationType LinkType { get; }
	}

	[BaseType (typeof (PSPDFLinkAnnotationEditingViewController))]
	interface PSPDFPageLinkAnnotationEditingViewController {

	}

	[BaseType (typeof (PSPDFLinkAnnotationEditingViewController))]
	interface PSPDFWebsiteLinkAnnotationEditingViewController {

	}

	[BaseType (typeof (PSPDFToolbarButton))]
	interface PSPDFToolbarBorderButton {

	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	[DisableDefaultCtor]
	interface PSPDFApplePencilController {

		[Export ("initWithApplePencilManager:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFApplePencilManager applePencilManager);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFApplePencilManager {

		[Field ("PSPDFApplePencilDetectedNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString PencilDetectedNotification { get; }

		[Field ("PSPDFApplePencilEnabledChangedNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString PencilEnabledChangedNotification { get; }

		[Export ("detected")]
		bool Detected { get; set; }

		[Export ("enableOnDetection")]
		bool EnableOnDetection { get; set; }

		[Export ("enabled")]
		bool Enabled { get; set; }
	}

	delegate bool PSPDFSubmissionControllerShouldContinueHandler (PSPDFFormRequest formRequest);
	delegate void PSPDFSubmissionControllerBeforeSubmissionHandler (PSPDFFormRequest formRequest);
	delegate void PSPDFSubmissionControllerCompletionHandler (NSData data, NSUrlResponse arresponseg1);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFormSubmissionController {

		[Field ("PSPDFFormSubmissionControllerDidStartLoadingNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString DidStartLoadingNotification { get; }

		[Field ("PSPDFFormSubmissionControllerDidFinishLoadingNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString DidFinishLoadingNotification { get; }

		[Field ("PSPDFFormSubmissionControllerDidFailToLoadNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString DidFailToLoadNotification { get; }

		[Export ("initWithDocumentProvider:action:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocumentProvider documentProvider, PSPDFSubmitFormAction action);

		[Export ("documentProvider")]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("action")]
		PSPDFSubmitFormAction Action { get; }

		[Export ("formRequest", ArgumentSemantic.Strong)]
		PSPDFFormRequest FormRequest { get; set; }

		[Export ("submissionPathExtension")]
		string SubmissionPathExtension { get; }

		[Export ("submitContinueWhen:beforeSubmission:onCompletion:onError:")]
		void SubmitContinue (PSPDFSubmissionControllerShouldContinueHandler continueHandler, PSPDFSubmissionControllerBeforeSubmissionHandler beforeSubmissionHandler, PSPDFSubmissionControllerCompletionHandler completionHandler, Action<NSError> errorHandler);
	}

	interface IPSPDFInitialStartingPointGesture { }

	[Protocol]
	interface PSPDFInitialStartingPointGesture {

		[Abstract]
		[Export ("initialOrCurrentLocationInView:")]
		CGPoint GetInitialOrCurrentLocationInView ([NullAllowed] UIView view);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	[DisableDefaultCtor]
	interface PSPDFReaderViewController {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocument document);

		[Export ("document")]
		PSPDFDocument Document { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationSelectionContext : INativeObject /* <AnnotationType>
		where AnnotationType : PSPDFAnnotation */ {

		[Export ("annotation")]
		NSObject /*AnnotationType*/ Annotation { get; }

		[Export ("pageView")]
		PSPDFPageView PageView { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationTransformationContext<AnnotationType>
		where AnnotationType : PSPDFAnnotation {

		[Export ("annotation")]
		AnnotationType Annotation { get; }

		[Export ("pageView")]
		PSPDFPageView PageView { get; }

		[Export ("mode")]
		PSPDFAnnotationTransformationMode Mode { get; }
	}

	interface IPSPDFDocumentViewInteractions {}

	[Protocol]
	interface PSPDFDocumentViewInteractions {

		[Abstract]
		[Export ("selectAnnotation")]
		PSPDFInteractionComponent /*<PSPDFAnnotationSelectionContext>*/ SelectAnnotation { get; }

		[Abstract]
		[Export ("tryToSelectAnnotationAtPoint:inCoordinateSpace:")]
		bool TryToSelectAnnotation (CGPoint point, IUICoordinateSpace coordinateSpace);

		[Abstract]
		[Export ("deselectAnnotation")]
		PSPDFInteractionComponent /*<NSNull>*/ DeselectAnnotation { get; }

		[Abstract]
		[Export ("editAnnotation")]
		PSPDFInteractionComponent /*<PSPDFAnnotationSelectionContext>*/ EditAnnotation { get; }

		[Abstract]
		[Export ("tryToEditAnnotationAtPoint:inCoordinateSpace:")]
		bool TryToEditAnnotation (CGPoint point, IUICoordinateSpace coordinateSpace);

		[Abstract]
		[Export ("transformAnnotation")]
		PSPDFInteractionComponent /*<PSPDFAnnotationTransformationContext>*/ TransformAnnotation { get; }

		[Abstract]
		[Export ("openLinkAnnotation")]
		PSPDFInteractionComponent /*<PSPDFAnnotationSelectionContext<PSPDFLinkAnnotation>>*/ OpenLinkAnnotation { get; }

		[Abstract]
		[Export ("tryToOpenLinkAnnotationAtPoint:inCoordinateSpace:")]
		bool TryToOpenLinkAnnotation (CGPoint point, IUICoordinateSpace coordinateSpace);

		[Abstract]
		[Export ("showAnnotationMenu")]
		PSPDFInteractionComponent /*<NSNull>*/ ShowAnnotationMenu { get; }

		[Abstract]
		[Export ("tryToShowAnnotationMenuAtPoint:inCoordinateSpace:")]
		bool TryToShowAnnotationMenu (CGPoint point, IUICoordinateSpace coordinateSpace);

		[Abstract]
		[Export ("fastScroll")]
		PSPDFInteractionComponent /*<PSPDFFastScrollContext>*/ FastScroll { get; }

		[Abstract]
		[Export ("tryToPerformFastScrollAtPoint:inCoordinateSpace:")]
		bool TryToPerformFastScroll (CGPoint point, IUICoordinateSpace coordinateSpace);

		[Abstract]
		[Export ("smartZoom")]
		PSPDFInteractionComponent /*<PSPDFSmartZoomContext>*/ SmartZoom { get; }

		[Abstract]
		[Export ("tryToPerformSmartZoomAtPoint:inCoordinateSpace:")]
		bool TryToPerformSmartZoom (CGPoint point, IUICoordinateSpace coordinateSpace);

		[Abstract]
		[Export ("selectText")]
		PSPDFInteractionComponent /*<NSNull>*/ SelectText { get; }

		[Abstract]
		[Export ("deselectText")]
		PSPDFInteractionComponent /*<NSNull>*/ DeselectText { get; }

		[Abstract]
		[Export ("toggleUserInterface")]
		PSPDFInteractionComponent /*<NSNull>*/ ToggleUserInterface { get; }

		[Abstract]
		[Export ("tryToToggleUserInterfaceAtPoint:inCoordinateSpace:")]
		bool TryToToggleUserInterface (CGPoint point, IUICoordinateSpace coordinateSpace);

		[Abstract]
		[Export ("allInteractions")]
		PSPDFInteractionComponent /*<NSNull>*/ AllInteractions { get; }

		[Abstract]
		[Export ("allAnnotationInteractions")]
		PSPDFInteractionComponent /*<NSNull>*/ AllAnnotationInteractions { get; }

		[Abstract]
		[Export ("allTextInteractions")]
		PSPDFInteractionComponent /*<NSNull>*/ AllTextInteractions { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFastScrollContext : INativeObject {

		[Export ("direction")]
		PSPDFFastScrollDirection Direction { get; }
	}

	// Using context as IntPtr for now instead of NSObject, see "Disable Ink Annotation Interaction" sample.
	delegate bool PSPDFInteractionComponentAddActivationConditionHandler (IntPtr context, CGPoint point, IUICoordinateSpace coordinateSpace);
	delegate void PSPDFInteractionComponentAddActivationCallbackHandler (IntPtr context, CGPoint point, IUICoordinateSpace coordinateSpace);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFInteractionComponent {

		[Export ("enabled")]
		bool Enabled { [Bind ("isEnabled")] get; set; }

		[Export ("addActivationCondition:")]
		void AddActivationCondition (PSPDFInteractionComponentAddActivationConditionHandler conditionHandler);

		[Export ("canActivateAtPoint:inCoordinateSpace:")]
		bool CanActivate (CGPoint point, IUICoordinateSpace coordinateSpace);

		[Export ("addActivationCallback:")]
		void AddActivationCallback (PSPDFInteractionComponentAddActivationCallbackHandler callbackHandler);

		[Export ("containsGestureRecognizer:")]
		bool ContainsGestureRecognizer (UIGestureRecognizer gestureRecognizer);

		[Export ("requireGestureRecognizerToFail:")]
		void RequireGestureRecognizerToFail (UIGestureRecognizer otherGestureRecognizer);

		[Export ("allowSimultaneousRecognitionWithGestureRecognizer:")]
		void AllowSimultaneousRecognition (UIGestureRecognizer otherGestureRecognizer);
	}

	[Category]
	[BaseType (typeof (UIGestureRecognizer))]
	interface UIGestureRecognizer_PSPDFInteractionComponentSupport {

		[Export ("pspdf_requireGestureRecognizersInComponentToFail:")]
		void PSPdfRequireGestureRecognizersInComponentToFail (PSPDFInteractionComponent otherComponent);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSmartZoomContext {

		[Export ("targetRect")]
		CGRect TargetRect { get; }

		[Export ("scrollView")]
		UIScrollView ScrollView { get; }
	}

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFSignatureCreationConfigurationBuilder {

		[Export ("availableModes", ArgumentSemantic.Copy)]
		NSNumber [] AvailableModes { get; set; }

		[Export ("colors", ArgumentSemantic.Copy)]
		UIColor [] Colors { get; set; }

		[Export ("isNaturalDrawingEnabled")]
		bool IsNaturalDrawingEnabled { get; set; }

		[Export ("fonts", ArgumentSemantic.Copy)]
		UIFont [] Fonts { get; set; }
	}

	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFSignatureCreationConfiguration {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFSignatureCreationConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		NativeHandle Constructor (PSPDFSignatureCreationConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFSignatureCreationConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFSignatureCreationConfigurationBuilder> builderHandler);

		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFSignatureCreationConfiguration GetUpdatedConfiguration ([NullAllowed] Action<PSPDFSignatureCreationConfigurationBuilder> builderHandler);

		[Export ("availableModes", ArgumentSemantic.Copy)]
		NSNumber [] AvailableModes { get; }

		[Export ("colors", ArgumentSemantic.Copy)]
		UIColor [] Colors { get; }

		[Export ("isNaturalDrawingEnabled")]
		bool IsNaturalDrawingEnabled { get; }

		[Export ("fonts", ArgumentSemantic.Copy)]
		UIFont [] Fonts { get; }
	}

	[BaseType (typeof (UISegmentedControl))]
	interface PSPDFThumbnailFilterSegmentedControl : IPSPDFOverridable {

	}

	interface IPSPDFDrawViewDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFDrawViewDelegate {

		[Abstract]
		[Export ("drawView:didBeginDrawingInMode:")]
		void DidBeginDrawingInMode (PSPDFDrawView drawView, PSPDFDrawViewInputMode inputMode);

		[Abstract]
		[Export ("drawView:didEndDrawingInMode:finished:")]
		void DidEndDrawingInMode (PSPDFDrawView drawView, PSPDFDrawViewInputMode inputMode, bool didFinish);

		[Abstract]
		[Export ("drawView:wantsToAddAnnotation:")]
		void WantsToAddAnnotation (PSPDFDrawView drawView, PSPDFAnnotation annotation);

		[Abstract]
		[Export ("drawView:wantsToModifyAnnotation:inScope:")]
		void WantsToModifyAnnotation (PSPDFDrawView drawView, PSPDFAnnotation annotation, Action scope);

		[Abstract]
		[Export ("drawView:wantsToDeleteAnnotation:")]
		void WantsToDeleteAnnotation (PSPDFDrawView drawView, PSPDFAnnotation annotation);
	}


	delegate NSNumber [] PSPDFAnnotationMenuConfigurationBuilderChoicesHandler (PSPDFAnnotation annotation, PSPDFPageView pageView, NSNumber [] defaultChoices);
	delegate UIColor [] PSPDFAnnotationMenuConfigurationBuilderColorChoicesHandler (PSPDFAnnotationMenuConfigurationColorProperty property, PSPDFAnnotation annotation, PSPDFPageView pageView, UIColor [] defaultChoices);
	delegate NSNumber [] PSPDFAnnotationMenuConfigurationBuilderFontSizeChoicesHandler (PSPDFFreeTextAnnotation annotation, PSPDFPageView pageView, NSNumber [] defaultChoices);
	delegate NSNumber [] PSPDFAnnotationMenuConfigurationBuilderLineEndChoicesHandler (PSPDFAnnotationMenuConfigurationLineEndProperty property, PSPDFAbstractLineAnnotation annotation, PSPDFPageView pageView, NSNumber [] defaultChoices);

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFAnnotationMenuConfigurationBuilder {

		[Export ("alphaChoices", ArgumentSemantic.Copy)]
		PSPDFAnnotationMenuConfigurationBuilderChoicesHandler AlphaChoices { get; set; }

		[Export ("colorChoices", ArgumentSemantic.Copy)]
		PSPDFAnnotationMenuConfigurationBuilderColorChoicesHandler ColorChoices { get; set; }

		[Export ("fontSizeChoices", ArgumentSemantic.Copy)]
		PSPDFAnnotationMenuConfigurationBuilderFontSizeChoicesHandler FontSizeChoices { get; set; }

		[Export ("lineEndChoices", ArgumentSemantic.Copy)]
		PSPDFAnnotationMenuConfigurationBuilderLineEndChoicesHandler LineEndChoices { get; set; }

		[Export ("lineWidthChoices", ArgumentSemantic.Copy)]
		PSPDFAnnotationMenuConfigurationBuilderChoicesHandler LineWidthChoices { get; set; }
	}

	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFAnnotationMenuConfiguration
	{
		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFAnnotationMenuConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		NativeHandle Constructor (PSPDFAnnotationMenuConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFAnnotationMenuConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFAnnotationMenuConfigurationBuilder> builderHandler);

		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFAnnotationMenuConfiguration GetUpdatedConfiguration ([NullAllowed] Action<PSPDFAnnotationMenuConfigurationBuilder> builderHandler);

		[Export ("alphaChoices")]
		PSPDFAnnotationMenuConfigurationBuilderChoicesHandler AlphaChoices { get; }

		[Export ("colorChoices")]
		PSPDFAnnotationMenuConfigurationBuilderColorChoicesHandler ColorChoices { get; }

		[Export ("fontSizeChoices")]
		PSPDFAnnotationMenuConfigurationBuilderFontSizeChoicesHandler FontSizeChoices { get; }

		[Export ("lineEndChoices")]
		PSPDFAnnotationMenuConfigurationBuilderLineEndChoicesHandler LineEndChoices { get; }

		[Export ("lineWidthChoices")]
		PSPDFAnnotationMenuConfigurationBuilderChoicesHandler LineWidthChoices { get; }
	}

	delegate NSString [] PSPDFContentMenuConfigurationBuilderAnnotationToolChoices (PSPDFGlyphSequence glyphs, PSPDFPageView pageView, PSPDFEditMenuAppearance appearance, NSString [] defaultChoices);

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFContentMenuConfigurationBuilder {

		[Export ("annotationToolChoices")]
		PSPDFContentMenuConfigurationBuilderAnnotationToolChoices AnnotationToolChoices { get; set; }
	}

	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFContentMenuConfiguration {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFContentMenuConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		NativeHandle Constructor (PSPDFContentMenuConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFContentMenuConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFContentMenuConfigurationBuilder> builderHandler);

		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFContentMenuConfiguration GetUpdatedConfiguration ([NullAllowed] Action<PSPDFContentMenuConfigurationBuilder> builderHandler);

		[Export ("annotationToolChoices")]
		PSPDFContentMenuConfigurationBuilderAnnotationToolChoices AnnotationToolChoices { get; }
	}
}
