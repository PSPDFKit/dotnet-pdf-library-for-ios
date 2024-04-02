using System;

using Foundation;
using ObjCRuntime;
using CoreGraphics;
using CoreFoundation;
using CoreImage;

#if __IOS__
using UIKit;
using MessageUI;
using CoreSpotlight;
#elif __MAC__
using AppKit;
using UIBezierPath = AppKit.NSBezierPath;
using UIColor = AppKit.NSColor;
using UIImage = AppKit.NSImage;
using UITextAlignment = AppKit.NSTextAlignment;
using UIFont = AppKit.NSFont;
using UIEdgeInsets = AppKit.NSEdgeInsets;
using UIFontDescriptorSymbolicTraits = AppKit.NSFontSymbolicTraits;
#endif

#if !__WATCH__
using AVFoundation;
#endif

#if !NET
using NativeHandle = System.IntPtr;
#endif

namespace PSPDFKit.Model {

	[Abstract]
	[BaseType (typeof (PSPDFAction))]
	interface PSPDFAbstractFormAction {

		[Export ("fields", ArgumentSemantic.Copy), NullAllowed]
		NSObject [] Fields { get; set; }
	}

	[Abstract]
	[BaseType (typeof (PSPDFAbstractShapeAnnotation))]
	interface PSPDFAbstractLineAnnotation : PSPDFMeasurementAnnotation {

		[Export ("initWithPoints:")]
		NativeHandle Constructor (NSValue [] points);

		[Export ("lineEnd1", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd1 { get; set; }

		[Export ("lineEnd2", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd2 { get; set; }

		[Export ("bezierPath", ArgumentSemantic.Copy), NullAllowed]
		UIBezierPath BezierPath { get; }

		[Export ("setBoundingBox:transformPoints:")]
		void SetBoundingBox (CGRect boundingBox, bool transformPoints);

		[Export ("recalculateBoundingBox")]
		void RecalculateBoundingBox ();
	}

	[Abstract]
	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFAbstractShapeAnnotation : PSPDFMeasurementAnnotation {

		[Export ("pointSequences", ArgumentSemantic.Strong)]
		NSArray<NSValue> [] PointSequences { get; set; }
	}

	[Static]
	interface PSPDFActionOptionsKeys {

		[Field ("PSPDFActionOptionModalKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString ModalKey { get; }

		[Field ("PSPDFActionOptionAutoplayKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AutoplayKey { get; }

		[Field ("PSPDFActionOptionControlsKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString ControlsKey { get; }

		[Field ("PSPDFActionOptionLoopKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString LoopKey { get; }

		[Field ("PSPDFActionOptionFullscreenKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString FullscreenKey { get; }

		[Field ("PSPDFActionOptionOffsetKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString OffsetKey { get; }

		[Field ("PSPDFActionOptionSizeKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString SizeKey { get; }

		[Field ("PSPDFActionOptionPopoverKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString PopoverKey { get; }

		[Field ("PSPDFActionOptionCoverKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString CoverKey { get; }

		[Field ("PSPDFActionOptionPageKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString PageKey { get; }

		[Field ("PSPDFActionOptionButtonKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString ButtonKey { get; }

		[Field ("PSPDFActionOptionCloseButtonKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString CloseButtonKey { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAction : INativeObject {

		[Field ("PSPDFActionTypeTransformerName", PSPDFKitLibraryPath.LibraryPath)]
		NSString TypeTransformerName { get; }

		[Static]
		[Export ("actionClassForType:")]
		Class GetActionClass (PSPDFActionType actionType);

		[Export ("type", ArgumentSemantic.Assign)]
		PSPDFActionType Type { get; }

		[Export ("parentAction", ArgumentSemantic.Weak)]
		PSPDFAction ParentAction { get; }

		[Export ("subActions", ArgumentSemantic.Strong)]
		PSPDFAction [] SubActions { get; set; }

		[Export ("options"), NullAllowed]
		NSDictionary<NSString, NSObject> Options { get; }

		[Export ("localizedDescriptionWithDocumentProvider:")]
		string GetLocalizedDescription ([NullAllowed] PSPDFDocumentProvider documentProvider);

		[Export ("localizedActionType")]
		string LocalizedActionType { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFAESCryptoDataProvider : PSPDFFileDataProviding {

		[Export ("initWithURL:passphraseProvider:salt:rounds:")]
		NativeHandle Constructor (NSUrl url, PSPDFAESCryptoPassphraseProvider passphraseProvider, string salt, nuint rounds);

		[Export ("initWithURL:passphraseDataProvider:salt:rounds:")]
		NativeHandle Constructor (NSUrl url, Func<NSData> passphraseDataProvider, NSData saltData, nuint rounds);

		[Export ("initWithURL:passphraseProvider:")]
		[Internal]
		IntPtr InitWithURL (NSUrl url, PSPDFAESCryptoPassphraseProvider passphraseProvider);

		[Export ("initWithLegacyFileFormatURL:passphraseProvider:")]
		[Internal]
		IntPtr InitWithLegacyFileFormatURL (NSUrl url, PSPDFAESCryptoPassphraseProvider passphraseProvider);

		[Export ("initWithURL:binaryKeyProvider:")]
		NativeHandle Constructor (NSUrl url, Func<NSData> binaryKeyProvider);

		[Export ("fileURL")]
		new NSUrl FileUrl { get; }

		[Export ("createDataSinkWithOptions:error:")]
		new IPSPDFDataSink CreateDataSink (PSPDFDataSinkOptions options, [NullAllowed] out NSError error);

		[Export ("replaceContentsWithDataSink:error:")]
		new bool ReplaceContents (IPSPDFDataSink replacementDataSink, [NullAllowed] out NSError error);

		[Export ("deleteDataWithError:")]
		new bool DeleteData ([NullAllowed] out NSError error);

		[Sealed, Export ("useDiskCache")]
		bool UseDiskCache { get; }
	}

	[BaseType (typeof (PSPDFCryptoInputStream))]
	interface PSPDFAESCryptoInputStream {

		[Export ("initWithInputStream:passphrase:")]
		NativeHandle Constructor (NSInputStream stream, string passphrase);

		[Internal]
		[Export ("read:maxLength:")]
		nint _Read (IntPtr buffer, nuint len);

		[return: NullAllowed]
		[Export ("closeWithData"), New]
		NSData Close ();
	}

	[BaseType (typeof (PSPDFCryptoOutputStream))]
	interface PSPDFAESCryptoOutputStream {

		[Export ("initWithOutputStream:passphrase:")]
		NativeHandle Constructor (NSOutputStream stream, string passphrase);

		[Export ("close"), New]
		void Close ();
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotation : INativeObject {

		[Export ("isDeletable")]
		bool IsDeletable { get; }

		[Export ("fixedSize")]
		CGSize FixedSize { get; }

		[Export ("wantsSelectionBorder")]
		bool WantsSelectionBorder { get; }

		[Export ("requiresPopupAnnotation")]
		bool RequiresPopupAnnotation { get; }

		[Export ("readOnly")]
		bool ReadOnly { [Bind ("isReadOnly")] get; }

		[Export ("locked")]
		bool Locked { [Bind ("isLocked")] get; }

		[Export ("contentsLocked")]
		bool ContentsLocked { [Bind ("isContentsLocked")] get; }

		[Export ("movable")]
		bool Movable { [Bind ("isMovable")] get; }

		[Export ("resizable")]
		bool Resizable { [Bind ("isResizable")] get; }

		[Export ("shouldMaintainAspectRatio")]
		bool ShouldMaintainAspectRatio { get; }

		[Export ("minimumSize")]
		CGSize MinimumSize { get; }

		[Export ("hitTest:minDiameter:")]
		bool HitTest (CGPoint point, nfloat minDiameter);

		[Export ("boundingBoxForPageRect:")]
		CGRect GetBoundingBox (CGRect pageRect);

		[Export ("type")]
		PSPDFAnnotationType Type { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; set; }

		[Export ("absolutePageIndex")]
		nuint AbsolutePageIndex { get; set; }

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("dirty")]
		bool Dirty { [Bind ("isDirty")] get; set; }

		[Export ("overlay")]
		bool Overlay { [Bind ("isOverlay")] get; set; }

		[Export ("editable")]
		bool Editable { [Bind ("isEditable")] get; set; }

		[Export ("deleted")]
		bool Deleted { [Bind ("isDeleted")] get; set; }

		[BindAs (typeof (PSPDFAnnotationString))]
		[Export ("typeString")]
		NSString TypeString { get; }

		[BindAs (typeof (PSPDFAnnotationVariantString))]
		[NullAllowed, Export ("variant")]
		NSString Variant { get; }

		[Export ("alpha")]
		nfloat Alpha { get; set; }

		[NullAllowed, Export ("color", ArgumentSemantic.Strong)]
		UIColor Color { get; set; }

		[NullAllowed, Export ("borderColor", ArgumentSemantic.Strong)]
		UIColor BorderColor { get; set; }

		[NullAllowed, Export ("fillColor", ArgumentSemantic.Strong)]
		UIColor FillColor { get; set; }

		[Export ("blendMode")]
		CGBlendMode BlendMode { get; set; }

		[NullAllowed, Export ("contents")]
		string Contents { get; set; }

		[NullAllowed, Export ("subject")]
		string Subject { get; set; }

		[NullAllowed, Export ("additionalActions", ArgumentSemantic.Copy)]
		NSDictionary<NSNumber, PSPDFAction> AdditionalActions { get; set; }

		[NullAllowed, Export ("value", ArgumentSemantic.Copy)]
		NSObject Value { get; set; }

		[Export ("flags", ArgumentSemantic.Assign)]
		PSPDFAnnotationFlags Flags { get; set; }

		[Export ("hidden")]
		bool Hidden { [Bind ("isHidden")] get; set; }

		[NullAllowed, Export ("name")]
		string Name { get; set; }

		[NullAllowed, Export ("user")]
		string User { get; set; }

		[NullAllowed, Export ("group")]
		string Group { get; set; }

		[Export ("uuid")]
		string Uuid { get; }

		[NullAllowed, Export ("creationDate", ArgumentSemantic.Strong)]
		NSDate CreationDate { get; set; }

		[NullAllowed, Export ("lastModified", ArgumentSemantic.Strong)]
		NSDate LastModified { get; set; }

		[Export ("lineWidth")]
		nfloat LineWidth { get; set; }

		[Export ("borderStyle", ArgumentSemantic.Assign)]
		PSPDFAnnotationBorderStyle BorderStyle { get; set; }

		[NullAllowed, Export ("dashArray", ArgumentSemantic.Copy)]
		NSNumber [] DashArray { get; set; }

		[Export ("borderEffect", ArgumentSemantic.Assign)]
		PSPDFAnnotationBorderEffect BorderEffect { get; set; }

		[Export ("borderEffectIntensity")]
		nfloat BorderEffectIntensity { get; set; }

		[Export ("boundingBox", ArgumentSemantic.Assign)]
		CGRect BoundingBox { get; set; }

		[BindAs (typeof (CGRect []))]
		[NullAllowed, Export ("rects", ArgumentSemantic.Copy)]
		NSValue [] Rects { get; set; }

		[BindAs (typeof (CGPoint []))]
		[NullAllowed, Export ("points", ArgumentSemantic.Copy)]
		NSValue [] Points { get; set; }

		[Export ("objectNumber")]
		nint ObjectNumber { get; }

		[Export ("localizedDescription")]
		string LocalizedDescription { get; }

		[NullAllowed, Export ("annotationIcon")]
		UIImage AnnotationIcon { get; }

		[Export ("reply")]
		bool Reply { [Bind ("isReply")] get; }

		[NullAllowed, Export ("inReplyToAnnotation", ArgumentSemantic.Strong)]
		PSPDFAnnotation InReplyToAnnotation { get; set; }

		[NullAllowed, Export ("customData", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> CustomData { get; set; }

		[Export ("isEqualToAnnotation:")]
		bool IsEqualTo (PSPDFAnnotation otherAnnotation);

		// PSPDFAnnotation (AppearanceStream) Category

		[Export ("hasAppearanceStream")]
		bool HasAppearanceStream { get; }

		[Export ("clearAppearanceStream")]
		void ClearAppearanceStream ();

		[NullAllowed, Export ("appearanceStreamGenerator", ArgumentSemantic.Strong)]
		IPSPDFAppearanceStreamGenerating AppearanceStreamGenerator { get; set; }

		[Export ("maybeRenderCustomAppearanceStreamWithContext:options:")]
		bool MaybeRenderCustomAppearanceStream (CGContext context, PSPDFRenderOptions renderOptions);

		[Static]
		[Export ("propertyKeysForUndo")]
		NSSet<NSString> PropertyKeysForUndo { get; }

		[Export ("setUndoneValue:forKeyPath:")]
		void SetUndoneValue ([NullAllowed] NSObject value, NSString keyPath);

		// PSPDFAnnotation (Drawing) Category

		[Export ("lockAndRenderInContext:options:")]
		void LockAndRender (CGContext context, [NullAllowed] PSPDFRenderOptions renderOptions);

		[Export ("drawInContext:options:")]
		void DrawInContext (CGContext context, [NullAllowed] PSPDFRenderOptions renderOptions);

		[Export ("imageWithSize:options:")]
		[return: NullAllowed]
		UIImage GetImage (CGSize size, [NullAllowed] PSPDFRenderOptions renderOptions);

		[Export ("noteIconPoint")]
		CGPoint NoteIconPoint { get; }

		[Export ("shouldDrawNoteIconIfNeeded")]
		bool ShouldDrawNoteIconIfNeeded { get; }

		// PSPDFAnnotation (Advanced) Category

		[Export ("shouldUpdatePropertiesOnBoundsChange")]
		bool ShouldUpdatePropertiesOnBoundsChange { get; }

		[Export ("shouldUpdateOptionalPropertiesOnBoundsChange")]
		bool ShouldUpdateOptionalPropertiesOnBoundsChange { get; }

		[Export ("updatePropertiesWithTransform:isSizeChange:meanScale:")]
		void UpdatePropertiesWithTransform (CGAffineTransform transform, bool isSizeChange, nfloat meanScale);

		[Export ("updateOptionalPropertiesWithTransform:isSizeChange:meanScale:")]
		void UpdateOptionalPropertiesWithTransform (CGAffineTransform transform, bool isSizeChange, nfloat meanScale);

		[Export ("setBoundingBox:transform:includeOptional:")]
		void SetBoundingBox (CGRect boundingBox, bool transform, bool optionalProperties);

		[Export ("copyToClipboard")]
		void CopyToClipboard ();

		// PSPDFAnnotation (Fonts) Category

		[Field ("PSPDFFontSizeName", PSPDFKitLibraryPath.LibraryPath)]
		NSString FontSizeNameKey { get; }

		[Field ("PSPDFVerticalAlignmentName", PSPDFKitLibraryPath.LibraryPath)]
		NSString VerticalAlignmentNameKey { get; }

		[Field ("PSPDFOriginalFontNameAttributeName", PSPDFKitLibraryPath.LibraryPath)]
		NSString OriginalFontNameAttributeNameKey { get; }

		[NullAllowed, Export ("fontAttributes", ArgumentSemantic.Copy)]
		NSDictionary FontAttributes { get; set; }

		[NullAllowed, Export ("fontName")]
		string FontName { get; set; }

		[Export ("fontSize")]
		nfloat FontSize { get; set; }

		[Export ("textAlignment", ArgumentSemantic.Assign)]
		UITextAlignment TextAlignment { get; set; }

		[Export ("verticalTextAlignment", ArgumentSemantic.Assign)]
		PSPDFVerticalAlignment VerticalTextAlignment { get; set; }

		[Export ("defaultFontSize")]
		nfloat DefaultFontSize { get; }

		[Export ("defaultFontName")]
		string DefaultFontName { get; }

		[Export ("defaultFont")]
		UIFont DefaultFont { get; }

		[NullAllowed, Export ("attributedString")]
		NSAttributedString AttributedString { get; }

		[Export ("attributedStringWithContents:options:")]
		[return: NullAllowed]
		NSAttributedString GetAttributedString ([NullAllowed] string contents, [NullAllowed] PSPDFRenderOptions renderOptions);

		[Export ("attributedStringWithContents:")]
		[return: NullAllowed]
		NSAttributedString GetAttributedString ([NullAllowed] string contents);

		// PSPDFAnnotation (InstantJSON) Category

		[Static]
		[Export ("annotationFromInstantJSON:documentProvider:error:")]
		[return: NullAllowed]
		PSPDFAnnotation FromInstantJson (NSData instantJson, PSPDFDocumentProvider documentProvider, [NullAllowed] out NSError error);

		[Export ("generateInstantJSONWithError:")]
		[return: NullAllowed]
		NSData GenerateInstantJson ([NullAllowed] out NSError error);

		[Export ("generateInstantJSONWithVersion:error:")]
		[return: NullAllowed]
		NSData GenerateInstantJson (PSPDFInstantJsonVersion version, [NullAllowed] out NSError error);

		[Export ("hasBinaryInstantJSONAttachment")]
		bool HasBinaryInstantJsonAttachment { get; }

		[Export ("writeBinaryInstantJSONAttachmentToDataSink:error:")]
		[return: NullAllowed]
		string WriteBinaryInstantJsonAttachment (IPSPDFDataSink dataSink, [NullAllowed] out NSError error);

		[Export ("attachBinaryInstantJSONAttachmentFromDataProvider:error:")]
		bool AttachBinaryInstantJsonAttachment (IPSPDFDataProviding dataProvider, [NullAllowed] out NSError error);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationAppearanceStream {

		[Static]
		[Export ("appearanceStreamWithImage:")]
		PSPDFAnnotationAppearanceStream FromImage (UIImage image);

		[Static]
		[Export ("appearanceStreamWithFileURL:")]
		PSPDFAnnotationAppearanceStream FromImage (NSUrl imageFileUrl);

		[NullAllowed, Export ("image")]
		UIImage Image { get; }

		[NullAllowed, Export ("imageFileURL")]
		NSUrl ImageFileUrl { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotationGroup {

		[Static]
		[Export ("groupWithItems:")]
		PSPDFAnnotationGroup FromItems (PSPDFAnnotationGroupItem [] items);

		[Static]
		[Export ("groupWithItems:choice:")]
		PSPDFAnnotationGroup FromItems (PSPDFAnnotationGroupItem [] items, nuint choice);

		[Export ("items", ArgumentSemantic.Copy)]
		PSPDFAnnotationGroupItem [] Items { get; }

		[Export ("editableItems")]
		PSPDFAnnotationGroupItem [] EditableItems { get; }

		[Export ("choice")]
		nuint Choice { get; set; }

		[Export ("updateChoiceToItemWithType:variant:")]
		bool UpdateChoiceToItem ([BindAs (typeof (PSPDFAnnotationString))] NSString type, [BindAs (typeof (PSPDFAnnotationVariantString))] NSString variant);
	}

	delegate UIImage PSPDFAnnotationGroupItemConfigurationHandler (PSPDFAnnotationGroupItem item, [NullAllowed] NSObject container, UIColor tintColor);

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationGroupItem {

		[Static]
		[Export ("itemWithType:")]
		PSPDFAnnotationGroupItem FromType ([BindAs (typeof (PSPDFAnnotationString))] NSString annotationType);

		[Static]
		[Export ("itemWithType:variant:")]
		PSPDFAnnotationGroupItem FromType ([BindAs (typeof (PSPDFAnnotationString))] NSString annotationType, [NullAllowed] [BindAs (typeof (PSPDFAnnotationVariantString))] NSString annotationVariant);

		[Static]
		[Export ("itemWithType:variant:configurationBlock:")]
		PSPDFAnnotationGroupItem FromType ([BindAs (typeof (PSPDFAnnotationString))] NSString annotationType, [NullAllowed] [BindAs (typeof (PSPDFAnnotationVariantString))] NSString annotationVariant, PSPDFAnnotationGroupItemConfigurationHandler handler);

		[Static]
		[Export ("defaultConfigurationBlock")]
		PSPDFAnnotationGroupItemConfigurationHandler DefaultConfigurationHandler { get; }

		[BindAs (typeof (PSPDFAnnotationString))]
		[Export ("type")]
		NSString Type { get; }

		[BindAs (typeof (PSPDFAnnotationVariantString))]
		[NullAllowed, Export ("variant")]
		NSString Variant { get; }

		[Export ("configurationBlock", ArgumentSemantic.Copy)]
		PSPDFAnnotationGroupItemConfigurationHandler ConfigurationHandler { get; }
	}

	interface PSPDFAnnotationChangedNotificationEventArgs {

		[Export ("PSPDFAnnotationChangedNotificationIgnoreUpdateKey")]
		bool IgnoreUpdate { get; set; }

		[Export ("PSPDFAnnotationChangedNotificationKeyPathKey")]
		string [] KeyPaths { get; set; }
	}

	[Static]
	interface PSPDFAnnotationOptionsKeys {

		[Field ("PSPDFAnnotationOptionSuppressNotifications", PSPDFKitLibraryPath.LibraryPath)]
		NSString SuppressNotificationsKey { get; }

		[Field ("PSPDFAnnotationOptionAnimateView", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnimateViewKey { get; }
	}

	[StrongDictionary ("PSPDFAnnotationOptionsKeys")]
	interface PSPDFAnnotationOptions {
		bool SuppressNotifications { get; set; }
		bool AnimateView { get; set; }
	}

	delegate bool PSPDFAnnotationManagerUpdateAnnotationsHandler (IPSPDFAnnotationUpdate annotationUpdate, ref NSError updateError);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationManager : PSPDFAnnotationProviderChangeNotifier, PSPDFOverridable {

		[Field ("PSPDFAnnotationsAddedNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString AnnotationsAddedNotification { get; }

		[Field ("PSPDFAnnotationsRemovedNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString AnnotationsRemovedNotification { get; }

		[Field ("PSPDFAnnotationChangedNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification (typeof (PSPDFAnnotationChangedNotificationEventArgs))]
		NSString AnnotationChangedNotification { get; }

		[Export ("initWithDocumentProvider:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("annotationProviders", ArgumentSemantic.Copy)]
		IPSPDFAnnotationProvider [] AnnotationProviders { get; set; }

		[Export ("fileAnnotationProvider"), NullAllowed]
		PSPDFFileAnnotationProvider FileAnnotationProvider { get; }

		[Export ("annotationsForPageAtIndex:type:")]
		[return: NullAllowed]
		PSPDFAnnotation [] GetAnnotations (nuint pageIndex, PSPDFAnnotationType type);

		[Export ("allAnnotationsOfType:")]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> GetAllAnnotations (PSPDFAnnotationType annotationType);

		[Export ("hasLoadedAnnotationsForPageAtIndex:")]
		bool HasLoadedAnnotationsForPage (nuint pageIndex);

		[Export ("annotationViewClassForAnnotation:")]
		[return: NullAllowed]
		Class GetAnnotationViewClass (PSPDFAnnotation annotation);

		[Wrap ("Class.Lookup (GetAnnotationViewClass (annotation))")]
		Type GetAnnotationViewType (PSPDFAnnotation annotation);

		[Export ("addAnnotations:options:")]
		bool AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Export ("insertAnnotation:atZIndex:options:error:")]
		bool InsertAnnotation (PSPDFAnnotation annotation, nuint destinationIndex, [NullAllowed] NSDictionary options, [NullAllowed] out NSError error);

		[Wrap ("InsertAnnotation (annotation, destinationIndex, annotationOptions?.Dictionary, out error)")]
		bool InsertAnnotation (PSPDFAnnotation annotation, nuint destinationIndex, [NullAllowed] PSPDFAnnotationOptions annotationOptions, [NullAllowed] out NSError error);

		[Export ("updateAnnotationsOnPageAtIndex:error:withUpdateBlock:")]
		bool UpdateAnnotations (nuint pageIndex, [NullAllowed] out NSError error, PSPDFAnnotationManagerUpdateAnnotationsHandler updateHandler);

		[Export ("canMoveAnnotation:error:")]
		bool CanMoveAnnotation (PSPDFAnnotation annotation, [NullAllowed] out NSError error);

		[Export ("canExecuteZIndexMove:forAnnotation:")]
		bool CanExecuteZIndexMove (PSPDFAnnotationZIndexMove zIndexMove, PSPDFAnnotation annotation);

		[Wrap ("AddAnnotations (annotations, options: annotationOptions?.Dictionary)")]
		bool AddAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationOptions annotationOptions);

		[Export ("removeAnnotations:options:")]
		bool RemoveAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Wrap ("RemoveAnnotations (annotations, options: annotationOptions?.Dictionary)")]
		bool RemoveAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationOptions annotationOptions);

		[Export ("didChangeAnnotation:keyPaths:options:")]
		void DidChangeAnnotation (PSPDFAnnotation annotation, string [] keyPaths, [NullAllowed] NSDictionary options);

		[Export ("saveAnnotationsWithOptions:error:")]
		bool SaveAnnotations ([NullAllowed] NSDictionary options, [NullAllowed] out NSError error);

		[Export ("shouldSaveAnnotations")]
		bool ShouldSaveAnnotations { get; }

		[Export ("updateAnnotations:animated:")]
		new void UpdateAnnotations (PSPDFAnnotation [] annotations, bool animated);

		[Export ("annotationsIncludingGroupsFromAnnotations:")]
		PSPDFAnnotation [] GetAnnotationsIncludingGroups (PSPDFAnnotation [] annotations);

		[Export ("protocolStrings", ArgumentSemantic.Copy)]
		string [] GetProtocolStrings { get; set; }

		[Static]
		[Export ("fileTypeTranslationTable")]
		NSDictionary<NSString, NSNumber> FileTypeTranslationTable { get; }

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		// PSPDFAnnotationManager (SubclassingHooks) Category

		[NullAllowed, Export ("dirtyAnnotations")]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> DirtyAnnotations { get; }

		[Static]
		[Export ("mediaFileTypes")]
		string [] GetMediaFileTypes ();

		[Export ("defaultAnnotationViewClassForAnnotation:")]
		[return: NullAllowed]
		Class GetDefaultAnnotationViewClass (PSPDFAnnotation annotation);

		[Wrap ("Class.Lookup (GetDefaultAnnotationViewClass (annotation))")]
		Type GetDefaultAnnotationViewType (PSPDFAnnotation annotation);
	}

	interface IPSPDFAnnotationProvider {}

	[Protocol]
	interface PSPDFAnnotationProvider {

		[Abstract]
		[Export ("annotationsForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFAnnotation [] GetAnnotations (nuint pageIndex);

		[Export ("hasLoadedAnnotationsForPageAtIndex:")]
		bool HasLoadedAnnotationsForPage (nuint pageIndex);

		[return: NullAllowed]
		[Export ("annotationViewClassForAnnotation:")]
		Class GetAnnotationViewClass (PSPDFAnnotation annotation);

		[Export ("addAnnotations:options:")]
		[return: NullAllowed]
		PSPDFAnnotation [] AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Export ("insertAnnotation:atZIndex:options:error:")]
		bool InsertAnnotation (PSPDFAnnotation annotation, nuint destinationIndex, [NullAllowed] NSDictionary options, [NullAllowed] out NSError error);

		[Export ("allowAnnotationZIndexMoves")]
		bool AllowAnnotationZIndexMoves ();

		[Export ("removeAnnotations:options:")]
		[return: NullAllowed]
		PSPDFAnnotation [] RemoveAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Export ("saveAnnotationsWithOptions:error:")]
		bool SaveAnnotations ([NullAllowed] NSDictionary options, [NullAllowed] out NSError error);

		[Export ("shouldSaveAnnotations")]
		bool ShouldSaveAnnotations ();

		[return: NullAllowed]
		[Export ("dirtyAnnotations")]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> GetDirtyAnnotations ();

		[Export ("didChangeAnnotation:keyPaths:options:")]
		void DidChangeAnnotation (PSPDFAnnotation annotation, string [] keyPaths, [NullAllowed] NSDictionary options);

		[Export ("providerDelegate")]
		IPSPDFAnnotationProviderChangeNotifier GetProviderDelegate ();

		[Export ("setProviderDelegate:")]
		void SetProviderDelegate ([NullAllowed] IPSPDFAnnotationProviderChangeNotifier providerDelegate);

		[Export ("delegate")]
		IPSPDFAnnotationProviderDelegate GetDelegate ();

		[Export ("setDelegate:")]
		void SetDelegate ([NullAllowed] IPSPDFAnnotationProviderDelegate provider);
	}

	interface IPSPDFAnnotationUpdate { }

	[Protocol]
	interface PSPDFAnnotationUpdate {

		[Abstract]
		[Export ("annotations")]
		PSPDFAnnotation [] Annotations { get; }

		[Abstract]
		[Export ("moveAnnotationAtZIndex:toZIndex:error:")]
		bool MoveAnnotation (nuint sourceZIndex, nuint destinationZIndex, [NullAllowed] out NSError error);

		[Abstract]
		[Export ("executeZIndexMove:forAnnotationAtZIndex:error:")]
		bool ExecuteZIndexMove (PSPDFAnnotationZIndexMove zIndexMove, nuint soureZIndex, [NullAllowed] out NSError error);
	}

	interface IPSPDFAnnotationProviderDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationProviderDelegate {

		[Export ("annotationProvider:didSaveAnnotations:")]
		void DidSaveAnnotations (IPSPDFAnnotationProvider annotationProvider, PSPDFAnnotation [] annotations);

		[Export ("annotationProvider:failedToSaveAnnotations:error:")]
		void FailedToSaveAnnotations (IPSPDFAnnotationProvider annotationProvider, PSPDFAnnotation [] annotations, NSError error);
	}

	interface IPSPDFAnnotationProviderChangeNotifier { }

	[Protocol]
	interface PSPDFAnnotationProviderChangeNotifier {

		[Abstract]
		[Export ("updateAnnotations:animated:")]
		void UpdateAnnotations (PSPDFAnnotation [] annotations, bool animated);

		[Abstract]
		[NullAllowed, Export ("parentDocumentProvider")]
		PSPDFDocumentProvider ParentDocumentProvider { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationSet {

		[Export ("initWithAnnotations:copyAnnotations:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFAnnotation [] annotations, bool shouldCopyAnnotations);

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; }

		[Export ("drawInContext:options:")]
		void DrawInContext (CGContext context, [NullAllowed] PSPDFRenderOptions contextOptions);

		[Export ("boundingBox", ArgumentSemantic.Assign)]
		CGRect BoundingBox { get; set; }

		[Export ("copyToClipboard")]
		void CopyToClipboard ();

		[Static]
		[Export ("unarchiveFromClipboard")]
		[return: NullAllowed]
		PSPDFAnnotationSet UnarchiveFromClipboard ();
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationStyle {

		[Export ("initWithName:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string styleName);

		[Export ("styleName")]
		string StyleName { get; set; }

		[NullAllowed, Export ("styleDictionary", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> StyleDictionary { get; set; }

		[Export ("setStyle:forKey:")]
		void SetStyle ([NullAllowed] NSObject style, string key);

		[Export ("applyStyleToAnnotation:")]
		void ApplyStyleToAnnotation (PSPDFAnnotation annotation);
	}

	interface IPSPDFAnnotationStyleManager {}

	[Protocol]
	interface PSPDFAnnotationStyleManager {

		[Abstract]
		[Export ("shouldUpdateDefaultsForAnnotationChanges")]
		bool ShouldUpdateDefaultsForAnnotationChanges { get; set; }

		[Abstract]
		[Export ("setupDefaultStylesIfNeeded")]
		void SetupDefaultStylesIfNeeded ();

		[Abstract]
		[NullAllowed, Export ("styleKeys", ArgumentSemantic.Copy)]
		NSSet<NSString> StyleKeys { get; set; }

		[Abstract]
		[Export ("stylesForKey:")]
		[return: NullAllowed]
		PSPDFAnnotationStyle [] GetStyles (NSString key);

		[Abstract]
		[Export ("addStyle:forKey:")]
		void AddStyle (PSPDFAnnotationStyle style, NSString key);

		[Abstract]
		[Export ("removeStyle:forKey:")]
		void RemoveStyle (PSPDFAnnotationStyle style, NSString key);

		[Abstract]
		[Export ("lastUsedStyleForKey:")]
		[return: NullAllowed]
		PSPDFAnnotationStyle GetLastUsedStyle (NSString key);

		[Abstract]
		[Export ("lastUsedProperty:forKey:")]
		[return: NullAllowed]
		NSObject GetLastUsedProperty (NSString styleProperty, NSString key);

		[Abstract]
		[Export ("setLastUsedValue:forProperty:forKey:")]
		void SetLastUsedValue ([NullAllowed] NSObject value, NSString styleProperty, NSString key);

		[Abstract]
		[Export ("defaultPresetsForKey:type:")]
		[return: NullAllowed]
		IPSPDFStylePreset [] GetDefaultPresets (NSString key, NSString type);

		[Abstract]
		[Export ("setDefaultPresets:forKey:type:")]
		void SetDefaultPresets ([NullAllowed] IPSPDFStylePreset [] presets, NSString key, NSString type);

		[Abstract]
		[Export ("presetsForKey:type:")]
		[return: NullAllowed]
		IPSPDFStylePreset [] GetPresets (NSString key, NSString type);

		[Abstract]
		[Export ("setPresets:forKey:type:")]
		void SetPresets ([NullAllowed] IPSPDFStylePreset [] presets, NSString key, NSString type);

		[Abstract]
		[Export ("isPresetModifiedAtIndex:forKey:type:")]
		bool IsPresetModified (nuint index, NSString key, NSString type);

		[Abstract]
		[Export ("resetPresetAtIndex:forKey:type:")]
		bool ResetPreset (nuint idx, NSString key, NSString type);
	}

	[Static]
	interface PSPDFStyleManagerKeys {

		[Field ("PSPDFAnnotationStyleTypeLastUsed", PSPDFKitLibraryPath.LibraryPath)]
		NSString LastUsedStylesKey { get; }

		[Field ("PSPDFAnnotationStyleTypeGeneric", PSPDFKitLibraryPath.LibraryPath)]
		NSString GenericStylesKey { get; }

		[Field ("PSPDFAnnotationStyleTypeColorPreset", PSPDFKitLibraryPath.LibraryPath)]
		NSString ColorPresetKey { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDefaultAnnotationStyleManager : PSPDFAnnotationStyleManager {

		// PSPDFDefaultAnnotationStyleManager (Defaults) Category

		[Export ("setupDefaultStyles")]
		void SetupDefaultStyles ();

		[Export ("defaultColorPresetsForKey:")]
		PSPDFColorPreset [] GetDefaultColorPresets (string key);

		[Export ("defaultBorderPresetsForKey:")]
		NSObject [] GetDefaultBorderPresets (string key);

		[Static]
		[Export ("highlightYellow")]
		UIColor HighlightYellow { get; }

		[Static]
		[Export ("drawingBlue")]
		UIColor DrawingBlue { get; }
	}

	delegate void PSPDFAnnotationSummarizerProgressHandler (nuint currentIndex, nfloat percentage);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationSummarizer {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocument document);

		[Export ("document")]
		PSPDFDocument Document { get; }

		[return: NullAllowed]
		[Export ("annotationSummaryForPages:")]
		NSAttributedString GetAnnotationSummary (NSIndexSet pages);

		[return: NullAllowed]
		[Export ("annotationSummaryForPages:progress:")]
		NSAttributedString GetAnnotationSummary (NSIndexSet pages, [NullAllowed] PSPDFAnnotationSummarizerProgressHandler progressHandler);

		[Export ("cancelSummaryGeneration")]
		void CancelSummaryGeneration ();

#if __IOS__
		// PSPDFAnnotationSummarizer (FileSupport) Category

		[Async]
		[Export ("temporaryPDFFileURLForPages:completionBlock:")]
		void GenerateTemporaryPdfFileUrl (NSIndexSet pages, Action<NSUrl, NSError> completionHandler);

		[Async]
		[Export ("temporaryPDFFileURLForPages:progress:completionBlock:")]
		void GenerateTemporaryPdfFileUrl (NSIndexSet pages, [NullAllowed] PSPDFAnnotationSummarizerProgressHandler progressHandler, Action<NSUrl, NSError> completionHandler);
#endif
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotationToolbarConfiguration {

		[Export ("initWithAnnotationGroups:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFAnnotationGroup [] annotationGroups);

		[Export ("annotationGroups")]
		PSPDFAnnotationGroup [] AnnotationGroups { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAppearanceCharacteristics {

		[NullAllowed, Export ("normalIcon", ArgumentSemantic.Strong)]
		UIImage NormalIcon { get; set; }
	}

	interface IPSPDFAppearanceStreamGenerating {}

	[Protocol]
	interface PSPDFAppearanceStreamGenerating {

		[Abstract]
		[Export ("dataProviderForAnnotation:options:")]
		[return: NullAllowed]
		IPSPDFDataProviding GetDataProvider (PSPDFAnnotation annotation, [NullAllowed] NSDictionary<NSString, NSObject> options);
	}

	[Static]
	interface PSPDFAppearanceStreamGenerationOptionsKeys {

		[Field ("PSPDFAppearanceGenerationFlatten", PSPDFKitLibraryPath.LibraryPath)]
		NSString FlattenKey { get; }

		[Field ("PSPDFAppearanceGenerationPrint", PSPDFKitLibraryPath.LibraryPath)]
		NSString PrintKey { get; }
	}

	interface IPSPDFApplicationPolicy {}

	[Protocol]
	interface PSPDFApplicationPolicy {

		[Abstract]
		[Export ("hasPermissionForEvent:isUserAction:")]
		bool HasPermissionForEvent (NSString @event, bool isUserAction);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDefaultApplicationPolicy : PSPDFApplicationPolicy {

	}

	[Static]
	interface PSPDFPolicyEvents {

		[Field ("PSPDFPolicyEventOpenIn", PSPDFKitLibraryPath.LibraryPath)]
		NSString OpenIn { get; }

		[Field ("PSPDFPolicyEventPrint", PSPDFKitLibraryPath.LibraryPath)]
		NSString Print { get; }

		[Field ("PSPDFPolicyEventEmail", PSPDFKitLibraryPath.LibraryPath)]
		NSString Email { get; }

		[Field ("PSPDFPolicyEventMessage", PSPDFKitLibraryPath.LibraryPath)]
		NSString Message { get; }

		[Field ("PSPDFPolicyEventQuickLook", PSPDFKitLibraryPath.LibraryPath)]
		NSString QuickLook { get; }

		[Field ("PSPDFPolicyEventAudioRecording", PSPDFKitLibraryPath.LibraryPath)]
		NSString AudioRecording { get; }

		[Field ("PSPDFPolicyEventCamera", PSPDFKitLibraryPath.LibraryPath)]
		NSString EventCamera { get; }

		[Field ("PSPDFPolicyEventPhotoLibrary", PSPDFKitLibraryPath.LibraryPath)]
		NSString PhotoLibrary { get; }

		[Field ("PSPDFPolicyEventPasteboard", PSPDFKitLibraryPath.LibraryPath)]
		NSString Pasteboard { get; }

		[Field ("PSPDFPolicyEventSubmitForm", PSPDFKitLibraryPath.LibraryPath)]
		NSString SubmitForm { get; }

		[Field ("PSPDFPolicyEventNetwork", PSPDFKitLibraryPath.LibraryPath)]
		NSString Network { get; }
	}

	[Abstract]
	[BaseType (typeof (PSPDFLinkAnnotation))]
	interface PSPDFAssetAnnotation {

		[NullAllowed, Export ("assetName")]
		string AssetName { get; }

		[Export ("fileURLWithError:")]
		[return: NullAllowed]
		NSUrl GetFileUrl ([NullAllowed] out NSError error);
	}

	interface IPSPDFBackForwardActionListDelegate {}

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFBackForwardActionListDelegate {

		[Abstract]
		[Export ("backForwardList:requestedBackActionExecution:animated:")]
		void RequestedBackActionExecution (PSPDFBackForwardActionList list, PSPDFAction[] actions, bool animated);

		[Abstract]
		[Export ("backForwardList:requestedForwardActionExecution:animated:")]
		void RequestedForwardActionExecution (PSPDFBackForwardActionList list, PSPDFAction[] actions, bool animated);

		[Export ("backForwardListDidUpdate:")]
		void BackForwardListDidUpdate (PSPDFBackForwardActionList list);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFBackForwardActionList : PSPDFOverridable {

		[Export ("initWithDelegate:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] IPSPDFBackForwardActionListDelegate @delegate);

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFBackForwardActionListDelegate Delegate { get; set; }

		[Export ("registerAction:")]
		void RegisterAction (PSPDFAction action);

		[Export ("requestBackAnimated:")]
		void RequestBack (bool animated);

		[Export ("requestBackToAction:animated:")]
		void RequestBack (PSPDFAction action, bool animated);

		[Export ("requestForwardAnimated:")]
		void RequestForward (bool animated);

		[Export ("requestForwardToAction:animated:")]
		void RequestForward (PSPDFAction action, bool animated);

		[Export ("resetBackList")]
		void ResetBackList ();

		[Export ("resetForwardList")]
		void ResetForwardList ();

		[Export ("reset")]
		void Reset ();

		[Export ("backListCap")]
		nuint BackListCap { get; set; }

		[NullAllowed, Export ("backAction")]
		PSPDFAction BackAction { get; }

		[NullAllowed, Export ("forwardAction")]
		PSPDFAction ForwardAction { get; }

		[Export ("backList", ArgumentSemantic.Copy)]
		PSPDFAction [] BackList { get; }

		[Export ("forwardList", ArgumentSemantic.Copy)]
		PSPDFAction [] ForwardList { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFBaseConfiguration {

		[Static]
		[Export ("defaultConfiguration")]
		PSPDFBaseConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFBaseConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFBaseConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFBaseConfigurationBuilder> builderHandler);

		// Allow subclasses to expose the right one.
		//[Export ("configurationUpdatedWithBuilder:")]
		//PSPDFBaseConfiguration GetUpdatedConfiguration (Action<PSPDFBaseConfigurationBuilder> builderHandler);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFBaseConfigurationBuilder {

		[Static]
		[Export ("configurationTargetClass")]
		Class ConfigurationTargetClass { get; }

		[Static]
		[Wrap ("Class.Lookup (ConfigurationTargetClass)")]
		Type ConfigurationTargetType { get; }

		[Export ("build")]
		PSPDFBaseConfiguration Build { get; }

		[Export ("reset")]
		void Reset ();
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFBookmark : INSCopying, INSMutableCopying, PSPDFOverridable {

		[Export ("initWithAction:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFAction action);

		[Export ("action", ArgumentSemantic.Copy)]
		PSPDFAction Action { get; }

		[NullAllowed, Export ("name")]
		string Name { get; }

		[Export ("displayName")]
		string DisplayName { get; }

		// PSPDFBookmark (GoToAction) Category

		[Export ("initWithPageIndex:")]
		NativeHandle Constructor (uint pageIndex);

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		uint PageIndex { get; }

		// ProviderSupport (PSPDFBookmark) Category

		[Export ("identifier")]
		string Identifier { get; }

		[Export ("sortKey"), NullAllowed]
		NSNumber SortKey { get; }

		[Export ("initWithIdentifier:action:name:sortKey:")]
		NativeHandle Constructor (string identifier, PSPDFAction action, [NullAllowed] string name, [NullAllowed] NSNumber sortKey);
	}

	[BaseType (typeof (PSPDFBookmark))]
	interface PSPDFMutableBookmark {

		[Export ("initWithAction:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFAction action);

		[Export ("action", ArgumentSemantic.Copy), New]
		PSPDFAction Action { get; set; }

		[Export ("name"), NullAllowed, New]
		string Name { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFBookmarkManager {

		[Notification]
		[Field ("PSPDFBookmarksChangedNotification", PSPDFKitLibraryPath.LibraryPath)]
		NSString BookmarksChangedNotification { get; }

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocument document);

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("bookmarks", ArgumentSemantic.Copy)]
		PSPDFBookmark [] Bookmarks { get; }

		[Export ("bookmarksWithSortOrder:")]
		PSPDFBookmark [] GetBookmarks (PSPDFBookmarkManagerSortOrder sortOrder);

		[Export ("addBookmark:")]
		void AddBookmark (PSPDFBookmark bookmark);

		[Export ("removeBookmark:")]
		void RemoveBookmark (PSPDFBookmark bookmark);

		[Export ("moveBookmarkAtIndex:toIndex:")]
		void MoveBookmark (nuint sourceIndex, nuint destinationIndex);

		[Export ("performBlock:")]
		void PerformAction (Action action);

		[Export ("performBlockAndWait:")]
		void PerformActionAndWait (Action action);

		[Export ("provider", ArgumentSemantic.Copy)]
		IPSPDFBookmarkProvider [] Provider { get; set; }

		// PSPDFBookmarkManager (GoToAction) Category

		[Export ("addBookmarkForPageAtIndex:")]
		PSPDFBookmark AddBookmarkForPage (uint pageIndex);

		[Export ("removeBookmarksForPageAtIndex:")]
		void RemoveBookmarksForPage (uint pageIndex);

		[Export ("bookmarkForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFBookmark GetBookmarkForPage (uint pageIndex);
	}

	interface IPSPDFBookmarkProvider { }

	[Protocol]
	interface PSPDFBookmarkProvider {

		[Abstract]
		[Export ("bookmarks")]
		PSPDFBookmark [] Bookmarks { get; }

		[Abstract]
		[Export ("addBookmark:")]
		bool AddBookmark (PSPDFBookmark bookmark);

		[Abstract]
		[Export ("removeBookmark:")]
		bool RemoveBookmark (PSPDFBookmark bookmark);

		[Abstract]
		[Export ("save")]
		void Save ();
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFButtonFormElement {

		[Export ("selected")]
		bool Selected { [Bind ("isSelected")] get; }

		[NullAllowed, Export ("options", ArgumentSemantic.Copy)]
		PSPDFFormOption [] Options { get; }

		[NullAllowed, Export ("onState")]
		string OnState { get; }

		[NullAllowed, Export ("buttonFormField")]
		PSPDFButtonFormField ButtonFormField { get; }

		[Export ("select")]
		void Select ();

		[Export ("deselect")]
		void Deselect ();

		[Export ("toggleButtonSelectionState")]
		bool ToggleButtonSelectionState ();

	}

	[BaseType (typeof (PSPDFFormField))]
	[DisableDefaultCtor]
	interface PSPDFButtonFormField {

		[Static]
		[Export ("insertedButtonFieldWithType:fullyQualifiedName:documentProvider:formElements:buttonValues:error:")]
		[return: NullAllowed]
		PSPDFButtonFormField CreateInsertedButtonField (PSPDFFormFieldType type, string fullyQualifiedName, PSPDFDocumentProvider documentProvider, PSPDFFormElement [] formElements, string [] buttonValues, [NullAllowed] out NSError error);

		[Export ("isPushButton")]
		bool IsPushButton { get; }

		[Export ("isCheckBox")]
		bool IsCheckBox { get; }

		[Export ("isRadioButton")]
		bool IsRadioButton { get; }

		[Export ("selectedAnnotationObjectNumbers", ArgumentSemantic.Strong)]
		NSNumber [] SelectedAnnotationObjectNumbers { get; set; }

		[Export ("options", ArgumentSemantic.Copy)]
		PSPDFFormOption [] Options { get; set; }

		[Export ("onStateForButton:")]
		[return: NullAllowed]
		string OnStateForButton (PSPDFWidgetAnnotation annotation);

		[Export ("toggleButton:")]
		void ToggleButton (PSPDFWidgetAnnotation annotation);

		[Export ("isSelected:")]
		bool IsSelected (PSPDFWidgetAnnotation annotation);

		[Export ("selectButton:")]
		void SelectButton (PSPDFWidgetAnnotation annotation);

		[Export ("deselectButton:")]
		void DeselectButton (PSPDFWidgetAnnotation annotation);

		[Export ("valueForButton:")]
		[return: NullAllowed]
		string ValueForButton (PSPDFWidgetAnnotation annotation);
	}

	delegate void PSPDFCacheDocumentImageRenderingCompletionHandler (UIImage image, PSPDFDocument document, nuint page, CGSize size);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFCache {

		[Export ("memoryCache")]
		PSPDFMemoryCache MemoryCache { get; }

		[Export ("diskCache")]
		PSPDFDiskCache DiskCache { get; }

		[Export ("cacheStatusForRequest:imageSizeMatching:")]
		PSPDFCacheStatus GetCacheStatus (PSPDFRenderRequest request, PSPDFCacheImageSizeMatching imageSizeMatching);

		[Export ("imageForRequest:imageSizeMatching:error:")]
		[return: NullAllowed]
		UIImage GetImage (PSPDFRenderRequest request, PSPDFCacheImageSizeMatching imageSizeMatching, out NSError error);

		[Export ("saveImage:forRequest:")]
		void SaveImage (UIImage image, PSPDFRenderRequest request);

		[Export ("cacheDocument:withPageSizes:")]
		void CacheDocument ([NullAllowed] PSPDFDocument document, NSValue [] sizes);

		[Export ("cacheDocument:withPageSizes:imageRenderingCompletionBlock:")]
		void CacheDocument ([NullAllowed] PSPDFDocument document, NSValue [] sizes, [NullAllowed] PSPDFCacheDocumentImageRenderingCompletionHandler pageCompletionHandler);

		[Export ("stopCachingDocument:")]
		void StopCachingDocument ([NullAllowed] PSPDFDocument document);

		[Export ("invalidateImagesFromDocument:pageIndex:")]
		void InvalidateImages ([NullAllowed] PSPDFDocument document, nuint pageIndex);

		[Export ("removeImagesForDocumentWithUID:pageIndex:")]
		void RemoveImages (string documentUid, nuint pageIndex);

		[Export ("invalidateImagesFromDocument:indexes:")]
		void InvalidateImages ([NullAllowed] PSPDFDocument document, NSIndexSet indexes);

		[Export ("removeImagesForDocumentWithUID:pageIndexes:")]
		void RemoveImages (string documentUid, NSIndexSet indexes);

		[Export ("removeCacheForDocument:")]
		void RemoveCache ([NullAllowed] PSPDFDocument document);

		[Export ("removeImagesForDocumentWithUID:")]
		void RemoveImages (string documentUid);

		[Export ("clearCache")]
		void ClearCache ();
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFCaretAnnotation : PSPDFOverridable {

	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFChoiceFormElement {

		[NullAllowed, Export ("options", ArgumentSemantic.Copy)]
		PSPDFFormOption [] Options { get; }

		[NullAllowed, Export ("selectedIndices", ArgumentSemantic.Copy)]
		NSIndexSet SelectedIndices { get; set; }

		[NullAllowed, Export ("selectedOptions", ArgumentSemantic.Copy)]
		PSPDFFormOption [] SelectedOptions { get; }

		[Export ("customSelection")]
		bool CustomSelection { get; }

		[Export ("topIndex")]
		nuint TopIndex { get; }

		[NullAllowed, Export ("customText")]
		string CustomText { get; set; }

		[NullAllowed, Export ("choiceFormField")]
		PSPDFChoiceFormField ChoiceFormField { get; }
	}

	[BaseType (typeof (PSPDFFormField))]
	[DisableDefaultCtor]
	interface PSPDFChoiceFormField {

		[Static]
		[Export ("insertedChoiceFieldWithType:fullyQualifiedName:documentProvider:formElement:error:")]
		[return: NullAllowed]
		PSPDFChoiceFormField CreateInsertedChoiceField (PSPDFFormFieldType type, string fullyQualifiedName, PSPDFDocumentProvider documentProvider, PSPDFFormElement formElement, [NullAllowed] out NSError error);

		[Export ("isCombo")]
		bool IsCombo { get; }

		[Export ("isEdit")]
		bool IsEdit { get; set; }

		[Export ("isMultiSelect")]
		bool IsMultiSelect { get; set; }

		[Export ("commitOnSelChange")]
		bool CommitOnSelChange { get; set; }

		[Export ("doNotSpellCheck")]
		bool DoNotSpellCheck { get; set; }

		[Export ("options", ArgumentSemantic.Copy)]
		PSPDFFormOption [] Options { get; set; }

		[NullAllowed, Export ("selectedIndices", ArgumentSemantic.Copy)]
		NSIndexSet SelectedIndices { get; set; }

		[Export ("customSelection")]
		bool CustomSelection { get; }

		[NullAllowed, Export ("customText")]
		string CustomText { get; set; }

		[Export ("clearOptionsCache")]
		void ClearOptionsCache ();
	}

	[BaseType (typeof (PSPDFAbstractShapeAnnotation))]
	interface PSPDFCircleAnnotation : PSPDFOverridable {

		[Export ("bezierPath")]
		UIBezierPath BezierPath { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFColorPreset : PSPDFStylePreset {

		[Static]
		[Export ("presetWithColor:")]
		PSPDFColorPreset FromColor ([NullAllowed] UIColor color);

		[Static]
		[Export ("presetWithColor:fillColor:alpha:")]
		PSPDFColorPreset FromColor ([NullAllowed] UIColor color, [NullAllowed] UIColor fillColor, nfloat alpha);

		[NullAllowed, Export ("color")]
		UIColor Color { get; }

		[NullAllowed, Export ("colorWithAlpha")]
		UIColor ColorWithAlpha { get; }

		[NullAllowed, Export ("fillColor")]
		UIColor FillColor { get; }

		[NullAllowed, Export ("fillColorWithAlpha")]
		UIColor FillColorWithAlpha { get; }

		[Export ("alpha")]
		nfloat Alpha { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFContainerAnnotationProvider : PSPDFAnnotationProvider {

		[Export ("initWithDocumentProvider:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocumentProvider documentProvider);

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		// PSPDFContainerAnnotationProvider (SubclassingHooks) Category

		[Static]
		[Export ("shouldTrackDeletions")]
		bool ShouldTrackDeletions { get; }

		[Export ("clearNeedsSaveFlag")]
		void ClearNeedsSaveFlag ();

		[Export ("annotationCache")]
		NSMutableDictionary<NSNumber, NSArray<PSPDFAnnotation>> AnnotationCache { get; }

		[Export ("willInsertAnnotations:")]
		void WillInsertAnnotations (PSPDFAnnotation [] annotations);

		[Export ("performBlockForReading:")]
		void PerformActionForReading (Action action);

		[Export ("performBlockForWriting:")]
		void PerformActionForWriting (Action action);

		[Export ("performBlockForWritingAndWait:")]
		void PerformActionForWritingAndWait (Action action);

		[Export ("setAnnotations:forPageAtIndex:append:")]
		void SetAnnotations (PSPDFAnnotation [] annotations, nuint pageIndex, bool append);

		[Export ("setAnnotations:append:")]
		void SetAnnotations (PSPDFAnnotation [] annotations, bool append);

		[Export ("addAnnotations:options:")]
		[Advice ("Requires base call if override.")]
		[return: NullAllowed]
		PSPDFAnnotation[] AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("removeAnnotations:options:")]
		[Advice ("Requires base call if override.")]
		[return: NullAllowed]
		PSPDFAnnotation [] RemoveAnnotations (PSPDFAnnotation[] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("removeAllAnnotationsWithOptions:")]
		void RemoveAllAnnotations (NSDictionary<NSString, NSObject> options);

		[Export ("allAnnotations")]
		PSPDFAnnotation [] AllAnnotations { get; }

		[Export ("annotations")]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> Annotations { get; }

		[Export ("setAnnotationCacheDirect:")]
		void SetAnnotationCacheDirect (NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> annotationCache);
	}

	[BaseType (typeof (PSPDFFileDataProvider))]
	interface PSPDFCoordinatedFileDataProvider : PSPDFCoordinatedFileDataProviding, INSFilePresenter {

		// Inine to avoid compiler warnings

		[Export ("additionalOperationsSupported"), New]
		new PSPDFDataProvidingAdditionalOperations AdditionalOperationsSupported { get; }

		[NullAllowed, Export ("data"), New]
		new NSData Data { get; }

		[NullAllowed, Export ("fileURL"), New]
		new NSUrl FileUrl { get; }

		[Export ("size"), New]
		new ulong Size { get; }

		[Export ("UID"), New]
		new string Uid { get; }

		[NullAllowed, Export ("signature", ArgumentSemantic.Strong), New]
		new NSData Signature { get; set; }
	}

	interface IPSPDFCoordinatedFileDataProviding { }

	[Protocol]
	interface PSPDFCoordinatedFileDataProviding : PSPDFFileDataProviding {

		[Abstract]
		[Export ("filePresenter")]
		INSFilePresenter FilePresenter { get; }

		[Abstract]
		[NullAllowed, Export ("coordinationDelegate", ArgumentSemantic.Weak)]
		IPSPDFFileCoordinationDelegate CoordinationDelegate { get; set; }

		[Export ("isConflictResolutionAvailable")]
		bool IsConflictResolutionAvailable ();

		[Export ("resolveFileConflictWithResolution:error:")]
		bool ResolveFileConflict (PSPDFFileConflictResolution resolution, out NSError error);
	}

	delegate nint PSPDFCryptoInputStreamDecryptionHandler (PSPDFCryptoInputStream superStream, IntPtr buffer, nint len);

	[BaseType (typeof (NSInputStream))]
	interface PSPDFCryptoInputStream {

		[Export ("initWithInputStream:decryptionBlock:")]
		NativeHandle Constructor (NSInputStream stream, [NullAllowed] PSPDFCryptoInputStreamDecryptionHandler decryptionHandler);

		[Export ("decryptionBlock", ArgumentSemantic.Copy)]
		PSPDFCryptoInputStreamDecryptionHandler DecryptionHandler { get; set; }
	}

	delegate NSData PSPDFCryptoOutputStreamEncryptionHandler (PSPDFCryptoOutputStream stream, IntPtr buffer, nuint len);

	[BaseType (typeof (NSOutputStream))]
	interface PSPDFCryptoOutputStream {

		[Export ("initWithOutputStream:encryptionBlock:")]
		NativeHandle Constructor (NSOutputStream stream, PSPDFCryptoOutputStreamEncryptionHandler encryptionHandler);

		[Export ("encryptionBlock", ArgumentSemantic.Copy)]
		PSPDFCryptoOutputStreamEncryptionHandler EncryptionHandler { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFCryptor {

		[Export ("keyFromPassphrase:salt:")]
		NSData GetKey (string passphrase, string salt);

		[Export ("legacyKeyFromPassphrase:salt:")]
		NSData GetLegacyKey (string passphrase, string salt);

		[Export ("encryptFromURL:toURL:key:error:")]
		bool Encrypt (NSUrl sourceUrl, NSUrl targetUrl, NSData key, [NullAllowed] out NSError error);

		[Export ("decryptFromURL:toURL:key:error:")]
		bool Decrypt (NSUrl sourceUrl, NSUrl targetUrl, NSData key, [NullAllowed] out NSError error);

		[Export ("encryptFromURL:toURL:passphrase:error:")]
		bool Encrypt (NSUrl sourceUrl, NSUrl targetUrl, string passphrase, [NullAllowed] out NSError error);

		[Export ("decryptFromURL:toURL:passphrase:error:")]
		bool Decrypt (NSUrl sourceUrl, NSUrl targetUrl, string passphrase, [NullAllowed] out NSError error);

		[NullAllowed, Export ("fileManager", ArgumentSemantic.Strong)]
		IPSPDFFileManager FileManager { get; set; }
	}

	interface IPSPDFDatabaseEncryptionProvider { }

	[Protocol]
	interface PSPDFDatabaseEncryptionProvider {

		[Abstract]
		[Export ("encryptDatabase:withKey:")]
		bool EncryptDatabase (IntPtr db, NSData keyData);

		[Abstract]
		[Export ("reEncryptDatabase:withKey:")]
		unsafe bool ReEncryptDatabase (IntPtr db, NSData keyData);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDataContainerProvider : PSPDFDataProviding {

		[Export ("initWithData:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSData data);

		[Sealed, NullAllowed, Export ("progress")]
		NSProgress Progress { get; }

		[Export ("createDataSinkWithOptions:error:")]
		[return: NullAllowed]
		new IPSPDFDataSink CreateDataSink (PSPDFDataSinkOptions options, [NullAllowed] out NSError error);

		[Export ("replaceContentsWithDataSink:error:")]
		new bool ReplaceContents (IPSPDFDataSink replacementDataSink, out NSError error);

		[Export ("deleteDataWithError:")]
		new bool DeleteData ([NullAllowed] out NSError error);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDataContainerSink : PSPDFDataSink {

		[Export ("initWithData:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] NSData data);

		[Export ("data")]
		NSData Data { get; }
	}

	interface IPSPDFDataProviding { }

	[Protocol]
	interface PSPDFDataProviding : INSSecureCoding {

		[Abstract]
		[Export ("data:")]
		[return: NullAllowed]
		NSData GetData ([NullAllowed] out NSError error);

		[Abstract]
		[Export ("size")]
		ulong Size { get; }

		[Abstract]
		[Export ("UID")]
		string Uid { get; }

		[Abstract]
		[Export ("additionalOperationsSupported")]
		PSPDFDataProvidingAdditionalOperations AdditionalOperationsSupported { get; }

		[Abstract]
		[Export ("readDataWithSize:atOffset:error:")]
		[return: NullAllowed]
		NSData ReadData (ulong size, ulong offset, [NullAllowed] out NSError error);

		[Abstract]
		[NullAllowed, Export ("signature", ArgumentSemantic.Strong)]
		NSData Signature { get; set; }

		[return: NullAllowed]
		[Export ("progress")]
		NSProgress GetProgress ();

		[return: NullAllowed]
		[Export ("error")]
		NSError GetError ();

		[Export ("createDataSinkWithOptions:error:")]
		[return: NullAllowed]
		IPSPDFDataSink CreateDataSink (PSPDFDataSinkOptions options, [NullAllowed] out NSError error);

		[Export ("replaceContentsWithDataSink:error:")]
		bool ReplaceContents (IPSPDFDataSink replacementDataSink, out NSError error);

		[Export ("canWrite")]
		bool GetCanWrite ();

		[Export ("deleteDataWithError:")]
		bool DeleteData ([NullAllowed] out NSError error);

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("useDiskCache")]
		bool GetUseDiskCache ();
	}

	interface IPSPDFDataSink { }

	[Protocol]
	interface PSPDFDataSink {

		[Abstract]
		[Export ("isFinished")]
		bool IsFinished { get; }

		[Abstract]
		[Export ("options")]
		PSPDFDataSinkOptions Options { get; }

		[Abstract]
		[Export ("writeData:")]
		bool WriteData (NSData data);

		[Abstract]
		[Export ("finish")]
		bool Finish ();
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDigitalSignatureReference {

		[Export ("transformMethod")]
		PSPDFDigitalSignatureReferenceTransformMethod TransformMethod { get; }

		[NullAllowed, Export ("transformParams", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> TransformParams { get; }

		[NullAllowed, Export ("digestMethod")]
		string DigestMethod { get; }

		[NullAllowed, Export ("digestValue")]
		string DigestValue { get; }

		[Export ("digestLocation")]
		NSRange DigestLocation { get; }
	}

	delegate NSData PSPDFDiskCacheEncryptionHelper (PSPDFRenderRequest request, NSData data);
	delegate NSData PSPDFDiskCacheDecryptionHelper (PSPDFRenderRequest request, NSData encryptedData);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDiskCache {

		[Export ("allowedDiskSpace")]
		long AllowedDiskSpace { get; set; }

		[Export ("usedDiskSpace")]
		long UsedDiskSpace { get; }

		[Export ("cacheDirectory")]
		string CacheDirectory { get; set; }

		[Export ("fileFormat", ArgumentSemantic.Assign)]
		PSPDFDiskCacheFileFormat FileFormat { get; set; }

		[Export ("compression")]
		nfloat Compression { get; set; }

		[NullAllowed, Export ("encryptionHelper", ArgumentSemantic.Copy)]
		PSPDFDiskCacheEncryptionHelper EncryptionHelper { get; set; }

		[NullAllowed, Export ("decryptionHelper", ArgumentSemantic.Copy)]
		PSPDFDiskCacheDecryptionHelper DecryptionHelper { get; set; }
	}

	[Static]
	interface PSPDFDocumentSaveOptionsKeys {

		[Field ("PSPDFDocumentSaveOptionForceSaving", PSPDFKitLibraryPath.LibraryPath)]
		NSString ForceSavingKey { get; }

		[Field ("PSPDFDocumentSaveOptionStrategy", PSPDFKitLibraryPath.LibraryPath)]
		NSString StrategyKey { get; }

		[Field ("PSPDFDocumentSaveOptionSecurityOptions", PSPDFKitLibraryPath.LibraryPath)]
		NSString SecurityOptionsKey { get; }

		[Field ("PSPDFDocumentSaveOptionApplyRedactions", PSPDFKitLibraryPath.LibraryPath)]
		NSString ApplyRedactionsKey { get; }
	}

	[StrongDictionary ("PSPDFDocumentSaveOptionsKeys")]
	interface PSPDFDocumentSaveOptions {

		bool ForceSaving { get; set; }
		PSPDFDocumentSaveStrategy Strategy { get; set; }
		PSPDFDocumentSecurityOptions SecurityOptions { get; set; }
		bool ApplyRedactions { get; set; }
	}

	[StrongDictionary ("PSPDFDocument")]
	interface PSPDFDocumentAnnotationWriteOptions {

	}

	interface PSPDFDocumentUnderlyingFileChangedNotificationEventArgs {

		[Export ("PSPDFDocumentUnderlyingFileURLKey")]
		NSUrl FileUrl { get; }

		[Export ("PSPDFDocumentUnderlyingDataProvider")]
		NSObject DataProvider { get; }

		[Export ("PSPDFDocumentUnderlyingFileWillBeDeletedKey")]
		bool FileWillBeDeleted { get; }
	}

	delegate void PSPDFDocumentSaveHandler (NSError error, PSPDFAnnotation [] savedAnnotations);
	delegate void PSPDFDocumentGetAnnotationsByDetectingLinkTypesProgressHandler (PSPDFAnnotation [] annotations, nuint page, ref bool stop);

	[BaseType (typeof (NSObject))]
	interface PSPDFDocument : PSPDFDocumentProviderDelegate, PSPDFOverridable, INSCopying, INSSecureCoding, PSPDFFileCoordinationDelegate {

		[Field ("PSPDFDocumentUnderlyingFileChangedNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification (typeof (PSPDFDocumentUnderlyingFileChangedNotificationEventArgs))]
		NSString UnderlyingFileChangedNotification { get; }

		[Export ("initWithURL:")]
		NativeHandle Constructor (NSUrl url);

		[Export ("initWithDataProviders:")]
		NativeHandle Constructor (IPSPDFDataProviding [] dataProviders);

		[Export ("initWithDataProviders:loadCheckpointIfAvailable:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IPSPDFDataProviding [] dataProviders, bool loadCheckpoint);

		[Export ("documentByAppendingDataProviders:")]
		PSPDFDocument GetDocumentByAppendingDataProviders (IPSPDFDataProviding [] dataProviders);

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFDocumentDelegate Delegate { get; set; }

		[Export ("valid")]
		bool Valid { [Bind ("isValid")] get; }

		[NullAllowed, Export ("error")]
		NSError Error { get; }

		[Export ("isEqualToDocument:")]
		bool IsEqualTo (PSPDFDocument otherDocument);

		[Export ("features")]
		PSPDFDocumentFeatures Features { get; }

		[NullAllowed, Export ("fileURL")]
		NSUrl FileUrl { get; }

		[Export ("fileURLs")]
		NSUrl [] FileUrls { get; }

		[Export ("filePresenters")]
		INSFilePresenter [] FilePresenters { get; }

		[Export ("progress")]
		NSProgress Progress { get; }

		[NullAllowed, Export ("originalFile", ArgumentSemantic.Strong)]
		PSPDFFile OriginalFile { get; set; }

		[Export ("pathForPageAtIndex:")]
		[return: NullAllowed]
		NSUrl GetPathForPage (nuint pageIndex);

		[Export ("fileIndexForPageAtIndex:")]
		nint GetFileIndexForPage (nuint pageIndex);

		[Export ("URLForFileIndex:")]
		[return: NullAllowed]
		NSUrl GetUrlForFile (nuint fileIndex);

		[NullAllowed, Export ("fileName")]
		string FileName { get; }

		[Export ("fileNameForPageAtIndex:")]
		string GetFileNameForPage (nuint pageIndex);

		[Export ("deleteFiles:")]
		bool DeleteFiles ([NullAllowed] out NSError error);

		[NullAllowed, Export ("data")]
		NSData Data { get; }

		[Export ("dataArray")]
		NSData [] DataArray { get; }

		[Export ("fileNamesWithDataDictionary")]
		NSDictionary<NSString, NSData> FileNamesWithDataDictionary { get; }

		[Export ("dataProviders")]
		IPSPDFDataProviding [] DataProviders { get; }

		[Export ("documentProviders")]
		PSPDFDocumentProvider [] DocumentProviders { get; }

		[Export ("documentProviderForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFDocumentProvider GetDocumentProviderForPage (nuint pageIndex);

		[Export ("pageOffsetForDocumentProvider:")]
		nuint GetPageOffset (PSPDFDocumentProvider documentProvider);

		[NullAllowed, Export ("documentId", ArgumentSemantic.Copy)]
		NSData DocumentId { get; }

		[NullAllowed, Export ("documentIdString")]
		string DocumentIdString { get; }

		[Export ("UID"), NullAllowed]
		string Uid { get; set; }

		[Export ("pageCount")]
		nuint PageCount { get; }

		[Export ("pageInfoForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFPageInfo GetPageInfo (nuint pageIndex);

		[Export ("saveWithOptions:error:")]
		bool Save ([NullAllowed] NSDictionary options, [NullAllowed] out NSError error);

		[Wrap ("Save (saveOptions?.Dictionary, out error)")]
		bool Save ([NullAllowed] PSPDFDocumentSaveOptions saveOptions, [NullAllowed] out NSError error);

		[Async (ResultTypeName = "PSPDFDocumentSaveHandlerResult")]
		[Export ("saveWithOptions:completionHandler:")]
		void Save ([NullAllowed] NSDictionary options, [NullAllowed] PSPDFDocumentSaveHandler completionHandler);

		[Async (ResultTypeName = "PSPDFDocumentSaveHandlerResult")]
		[Wrap ("Save (saveOptions?.Dictionary, completionHandler)")]
		void Save ([NullAllowed] PSPDFDocumentSaveOptions saveOptions, [NullAllowed] PSPDFDocumentSaveHandler completionHandler);

		[Export ("checkpointer")]
		PSPDFDocumentCheckpointer Checkpointer { get; }

		[NullAllowed, Export ("outline")]
		PSPDFOutlineElement Outline { get; }

		[NullAllowed, Export ("userActivity", ArgumentSemantic.Strong)]
		NSUserActivity UserActivity { get; set; }

		[Export ("undoController")]
		IPSPDFUndoController UndoController { get; }

		// PSPDFDocument (Caching) Category

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("fillCache")]
		void FillCache ();

		[Export ("dataDirectory")]
		string DataDirectory { get; set; }

		[Export ("ensureDataDirectoryExistsWithError:")]
		bool EnsureDataDirectoryExists ([NullAllowed] out NSError error);

		[Export ("useDiskCache")]
		bool UseDiskCache { get; set; }

		// PSPDFDocument (Security) Category

		[Export ("unlockWithPassword:")]
		bool Unlock (string password);

		[Export ("lock")]
		void Lock ();

		[Export ("isEncrypted")]
		bool IsEncrypted { get; }

		[Export ("isLocked")]
		bool IsLocked { get; }

		[Export ("isUnlockedWithFullAccess")]
		bool IsUnlockedWithFullAccess { get; }

		[Export ("permissions")]
		PSPDFDocumentPermissions Permissions { get; }

		[Export ("allowAnnotationChanges")]
		bool AllowAnnotationChanges { get; }

		// PSPDFDocument (Bookmarks) Category

		[Export ("bookmarksEnabled")]
		bool BookmarksEnabled { [Bind ("areBookmarksEnabled")] get; set; }

		[NullAllowed, Export ("bookmarkManager")]
		PSPDFBookmarkManager BookmarkManager { get; }

		[Export ("bookmarks")]
		PSPDFBookmark [] Bookmarks { get; }

		// PSPDFDocument (PageLabels) Category

		[Export ("pageLabelsEnabled")]
		bool PageLabelsEnabled { [Bind ("arePageLabelsEnabled")] get; set; }

		[Export ("pageLabelForPageAtIndex:substituteWithPlainLabel:")]
		[return: NullAllowed]
		string GetPageLabel (nuint pageIndex, bool substitute);

		[Export ("pageForPageLabel:partialMatching:")]
		nuint GetPage (string pageLabel, bool partialMatching);

		// PSPDFDocument (Forms) Category

		[Export ("formsEnabled")]
		bool FormsEnabled { [Bind ("areFormsEnabled")] get; set; }

		[Export ("javaScriptStatus", ArgumentSemantic.Assign)]
		PSPDFJavaScriptStatus JavaScriptStatus { get; set; }

		[Export ("isJavaScriptStatusEnabled")]
		bool IsJavaScriptStatusEnabled { get; }

		[NullAllowed, Export ("formParser")]
		PSPDFFormParser FormParser { get; }

		// PSPDFDocument (EmbeddedFiles) Category

		[Export ("allEmbeddedFiles")]
		PSPDFEmbeddedFile [] AllEmbeddedFiles { get; }

		[Export ("annotationsEnabled")]
		bool AnnotationsEnabled { [Bind ("areAnnotationsEnabled")] get; set; }

		[Export ("addAnnotations:options:")]
		bool AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Wrap ("AddAnnotations (annotations, options: annotationOptions?.Dictionary)")]
		bool AddAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationOptions annotationOptions);

		[Export ("insertAnnotation:atZIndex:options:error:")]
		bool InsertAnnotation (PSPDFAnnotation annotation, nuint destinationIndex, [NullAllowed] NSDictionary options, [NullAllowed] out NSError error);

		[Wrap ("InsertAnnotation (annotation, destinationIndex, annotationOptions?.Dictionary, out error)")]
		bool InsertAnnotation (PSPDFAnnotation annotation, nuint destinationIndex, [NullAllowed] PSPDFAnnotationOptions annotationOptions, [NullAllowed] out NSError error);

		[Export ("removeAnnotations:options:")]
		bool RemoveAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Wrap ("RemoveAnnotations (annotations, options: annotationOptions?.Dictionary)")]
		bool RemoveAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationOptions annotationOptions);

		[Export ("annotationsForPageAtIndex:type:")]
		PSPDFAnnotation [] GetAnnotations (nuint pageIndex, PSPDFAnnotationType type);

		[Export ("allAnnotationsOfType:")]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> GetAllAnnotations (PSPDFAnnotationType annotationType);

		[Export ("containsAnnotations")]
		bool ContainsAnnotations { get; }

		// PSPDFDocument (AnnotationSaving) Category

		[Field ("PSPDFDocumentWillSaveAnnotationsNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString WillSaveAnnotationsNotification { get; }

		[Export ("canEmbedAnnotations")]
		bool CanEmbedAnnotations { get; }

		[Export ("canSaveAnnotations")]
		bool CanSaveAnnotations { get; }

		[Export ("annotationSaveMode", ArgumentSemantic.Assign)]
		PSPDFAnnotationSaveMode AnnotationSaveMode { get; set; }

		[Field ("PSPDFDocumentDefaultAnnotationUsernameKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString DefaultAnnotationUsernameKey { get; }

		[NullAllowed, Export ("defaultAnnotationUsername")]
		string DefaultAnnotationUsername { get; set; }

		[Field ("PSPDFAnnotationWriteOptionGenerateAppearanceStreamForType", PSPDFKitLibraryPath.LibraryPath)]
		NSString WriteOptionsGenerateAppearanceStreamForTypeKey { get; }

		[NullAllowed, Export ("annotationWritingOptions", ArgumentSemantic.Copy)]
		NSDictionary WeakAnnotationWritingOptions { get; set; }

		[Wrap ("WeakAnnotationWritingOptions")]
		PSPDFDocumentAnnotationWriteOptions AnnotationWritingOptions { get; set; }

		[Export ("hasDirtyAnnotations")]
		bool HasDirtyAnnotations { get; }

		// PSPDFDocument (Rendering) Category

		[Export ("imageForPageAtIndex:size:clippedToRect:annotations:options:error:")]
		[return: NullAllowed]
		UIImage GetImage (nuint pageIndex, CGSize size, CGRect clipRect, [NullAllowed] PSPDFAnnotation [] annotations, [NullAllowed] PSPDFRenderOptions options, [NullAllowed] out NSError error);

		[Export ("renderPageAtIndex:context:size:clippedToRect:annotations:options:error:")]
		bool RenderPage (nuint pageIndex, CGContext context, CGSize size, CGRect clipRect, [NullAllowed] PSPDFAnnotation [] annotations, [NullAllowed] PSPDFRenderOptions options, [NullAllowed] out NSError error);

		[Export ("setRenderOptions:type:")]
		void SetRenderOptions ([NullAllowed] PSPDFRenderOptions options, PSPDFRenderType type);

		[Export ("updateRenderOptionsForType:withBlock:")]
		void UpdateRenderOptions (PSPDFRenderType type, Action<PSPDFRenderOptions> handler);

		 [Export ("renderOptionsForType:")]
		PSPDFRenderOptions GetRenderOptions (PSPDFRenderType type);

		[Export ("renderAnnotationTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType RenderAnnotationTypes { get; set; }

		// PSPDFDocument (Metadata) Category

		[NullAllowed, Export ("title")]
		string Title { get; set; }

		[Export ("titleLoaded")]
		bool TitleLoaded { [Bind ("isTitleLoaded")] get; }

#if __IOS__
		[Export ("searchableItemAttributeSetWithThumbnail:")]
		[return: NullAllowed]
		CSSearchableItemAttributeSet GetSearchableItemAttributeSet (bool renderThumbnail);
#endif
		// PSPDFDocument (SubclassingHooks) Category

		[Export ("overrideClass:withClass:")]
		void OverrideClass (Class builtinClass, Class subclass);

		[Wrap ("OverrideClass (builtinType == null ? null : new Class (builtinType), subType == null ? null : new Class (subType))")]
		void OverrideClass (Type builtinType, Type subType);

		[Export ("didCreateDocumentProvider:")]
		PSPDFDocumentProvider DidCreateDocumentProvider (PSPDFDocumentProvider documentProvider);

		[NullAllowed, Export ("didCreateDocumentProviderBlock", ArgumentSemantic.Copy)]
		Action<PSPDFDocumentProvider> DidCreateDocumentProviderHandler { get; set; }

		[return: NullAllowed]
		[Export ("fileNameForIndex:")]
		string GetFileName (nuint fileIndex);

		// PSPDFDocument (Advanced) Category

		[Export ("relativePageIndexForPageAtIndex:")]
		nuint GetRelativePageIndex (nuint pageIndex);

		[Export ("pageBinding", ArgumentSemantic.Assign)]
		PSPDFPageBinding PageBinding { get; set; }

		[Export ("pspdfkit")]
		PSPDFKitGlobal PSPdfKit { get; }

		// PSPDFDocument (DataDetection) Category

		[Export ("autodetectTextLinkTypes", ArgumentSemantic.Assign)]
		PSPDFTextCheckingType AutodetectTextLinkTypes { get; set; }

		[Export ("annotationsByDetectingLinkTypes:forPagesAtIndexes:options:progress:error:")]
		[return: NullAllowed]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> GetAnnotationsByDetectingLinkTypes (PSPDFTextCheckingType linkTypes, NSIndexSet indexes, [NullAllowed] NSDictionary<NSString, NSDictionary<NSNumber, NSArray<PSPDFAnnotation>>> options, [NullAllowed] PSPDFDocumentGetAnnotationsByDetectingLinkTypesProgressHandler progressHandler, [NullAllowed] out NSError error);

		// PSPDFDocument (TextParser) Category

		[Export ("watermarkFilterEnabled")]
		bool WatermarkFilterEnabled { [Bind ("isWatermarkFilterEnabled")] get; set; }

		[Export ("textParserForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFTextParser GetTextParser (nuint pageIndex);

		// PSPDFDocument (ObjectFinder) Category

		[Advice ("'dictOptions' parameter comes from 'PSPDFObjectFinderOptions', use overload for a strongly typed dictionary.")]
		[Export ("objectsAtPDFPoint:pageIndex:options:")]
		NSDictionary GetObjectsAtPdfPoint (CGPoint pdfPoint, nuint pageIndex, [NullAllowed] NSDictionary dictOptions);

		[Wrap ("new PSPDFObjectFinderType (GetObjectsAtPdfPoint (pdfPoint, pageIndex, options?.Dictionary))")]
		PSPDFObjectFinderType GetObjectsAtPdfPoint (CGPoint pdfPoint, nuint pageIndex, [NullAllowed] PSPDFObjectFinderOptions options);

		[Advice ("'dictOptions' parameter comes from 'PSPDFObjectFinderOptions', use overload for a strongly typed dictionary.")]
		[Export ("objectsAtPDFRect:pageIndex:options:")]
		NSDictionary GetObjectsAtPdfRect (CGRect pdfRect, nuint pageIndex, [NullAllowed] NSDictionary dictOptions);

		[Wrap ("new PSPDFObjectFinderType (GetObjectsAtPdfRect (pdfRect, pageIndex, options?.Dictionary))")]
		PSPDFObjectFinderType GetObjectsAtPdfRect (CGRect pdfRect, nuint pageIndex, [NullAllowed] PSPDFObjectFinderOptions options);

		// PSPDFAnnotation (InstantJSON) Category

		[Export ("applyInstantJSONFromDataProvider:toDocumentProvider:lenient:error:")]
		bool ApplyInstantJson (IPSPDFDataProviding dataProvider, PSPDFDocumentProvider documentProvider, bool lenient, [NullAllowed] out NSError error);

		[Export ("generateInstantJSONFromDocumentProvider:error:")]
		[return: NullAllowed]
		NSData GenerateInstantJson (PSPDFDocumentProvider documentProvider, [NullAllowed] out NSError error);

		[Export ("generateInstantJSONFromDocumentProvider:version:error:")]
		[return: NullAllowed]
		NSData GenerateInstantJson (PSPDFDocumentProvider documentProvider, PSPDFInstantJsonVersion version, [NullAllowed] out NSError error);

		// PSPDFDocument (JavaScript)

		[Export ("loadDocumentLevelJavaScriptActionsWithError:")]
		bool LoadDocumentLevelJavaScriptActions ([NullAllowed] out NSError error);

		// PSPDFDocument (ConflictResolution)

		[Export ("resolveFileConflictForDataProvider:withResolution:error:")]
		bool ResolveFileConflict (IPSPDFCoordinatedFileDataProviding dataProvider, PSPDFFileConflictResolution resolution, [NullAllowed] out NSError error);
	}

	[Static]
	interface PSPDFObjectFinderOptionKeys {

		[Field ("PSPDFObjectFinderOptionExtractGlyphs", PSPDFKitLibraryPath.LibraryPath)]
		NSString ExtractGlyphsKey { get; }

		[Field ("PSPDFObjectFinderOptionExtractWords", PSPDFKitLibraryPath.LibraryPath)]
		NSString ExtractWordsKey { get; }

		[Field ("PSPDFObjectFinderOptionExtractText", PSPDFKitLibraryPath.LibraryPath)]
		NSString ExtractTextKey { get; }

		[Field ("PSPDFObjectFinderOptionExtractTextBlocks", PSPDFKitLibraryPath.LibraryPath)]
		NSString ExtractTextBlocksKey { get; }

		[Field ("PSPDFObjectFinderOptionExtractImages", PSPDFKitLibraryPath.LibraryPath)]
		NSString ExtractImagesKey { get; }

		[Field ("PSPDFObjectFinderOptionExtractAnnotations", PSPDFKitLibraryPath.LibraryPath)]
		NSString ExtractAnnotationsKey { get; }

		[Field ("PSPDFObjectFinderOptionIgnoreLargeTextBlocks", PSPDFKitLibraryPath.LibraryPath)]
		NSString IgnoreLargeTextBlocksKey { get; }

		[Field ("PSPDFObjectFinderOptionAnnotationTypes", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnnotationTypesKey { get; }

		[Field ("PSPDFObjectFinderOptionAnnotationPageBounds", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnnotationPageBoundsKey { get; }

		[Field ("PSPDFObjectFinderOptionPageZoomLevel", PSPDFKitLibraryPath.LibraryPath)]
		NSString PageZoomLevelKey { get; }

		[Field ("PSPDFObjectFinderOptionAnnotationIncludedGrouped", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnnotationIncludedGroupedKey { get; }

		[Field ("PSPDFObjectFinderOptionSmartSort", PSPDFKitLibraryPath.LibraryPath)]
		NSString SmartSortKey { get; }

		[Field ("PSPDFObjectFinderOptionMinDiameter", PSPDFKitLibraryPath.LibraryPath)]
		NSString MinDiameterKey { get; }

		[Field ("PSPDFObjectFinderOptionTextFlow", PSPDFKitLibraryPath.LibraryPath)]
		NSString TextFlowKey { get; }

		[Field ("PSPDFObjectFinderOptionFindFirstOnly", PSPDFKitLibraryPath.LibraryPath)]
		NSString FindFirstOnlyKey { get; }

		[Field ("PSPDFObjectFinderOptionTestIntersection", PSPDFKitLibraryPath.LibraryPath)]
		NSString TestIntersectionKey { get; }

		[Field ("PSPDFObjectFinderOptionTestIntersectionFraction", PSPDFKitLibraryPath.LibraryPath)]
		NSString TestIntersectionFractionKey { get; }
	}

	[StrongDictionary ("PSPDFObjectFinderOptionKeys")]
	interface PSPDFObjectFinderOptions {

		bool ExtractGlyphs { get; set; }
		bool ExtractWords { get; set; }
		bool ExtractText { get; set; }
		bool ExtractTextBlocks { get; set; }
		bool ExtractImages { get; set; }
		bool ExtractAnnotations { get; set; }
		bool IgnoreLargeTextBlocks { get; set; }
		PSPDFAnnotationType AnnotationTypes { get; set; }
		CGRect AnnotationPageBounds { get; set; }
		float PageZoomLevel { get; set; }
		bool AnnotationIncludedGrouped { get; set; }
		bool SmartSort { get; set; }
		float MinDiameter { get; set; }
		bool TextFlow { get; set; }
		bool FindFirstOnly { get; set; }
		bool TestIntersection { get; set; }
		double TestIntersectionFraction { get; set; }
	}

	[Static]
	interface PSPDFObjectFinderTypeKeys {

		[Field ("PSPDFObjectFinderTypeGlyphs", PSPDFKitLibraryPath.LibraryPath)]
		NSString GlyphsKey { get; }

		[Field ("PSPDFObjectFinderTypeWords", PSPDFKitLibraryPath.LibraryPath)]
		NSString WordsKey { get; }

		[Field ("PSPDFObjectFinderTypeText", PSPDFKitLibraryPath.LibraryPath)]
		NSString TextKey { get; }

		[Field ("PSPDFObjectFinderTypeTextBlocks", PSPDFKitLibraryPath.LibraryPath)]
		NSString TextBlocksKey { get; }

		[Field ("PSPDFObjectFinderTypeImages", PSPDFKitLibraryPath.LibraryPath)]
		NSString ImagesKey { get; }

		[Field ("PSPDFObjectFinderTypeAnnotations", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnnotationsKey { get; }
	}

	[StrongDictionary ("PSPDFObjectFinderTypeKeys")]
	interface PSPDFObjectFinderType {

		PSPDFGlyph [] Glyphs { get; set; }
		PSPDFWord [] Words { get; set; }
		string Text { get; set; }
		PSPDFTextBlock [] TextBlocks { get; set; }
		PSPDFImageInfo [] Images { get; set; }
		PSPDFAnnotation [] Annotations { get; set; }
	}

	interface PSPDFDocumentCheckpointSavedNotificationEventArgs {

		[Export ("PSPDFDocumentCheckpointSavedNotificationSuccessKey")]
		bool Success { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDocumentCheckpointer {

		[Field ("PSPDFDocumentCheckpointSavedNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification (typeof (PSPDFDocumentCheckpointSavedNotificationEventArgs))]
		NSString SavedNotification { get; }

		[Export ("strategy", ArgumentSemantic.Assign)]
		PSPDFDocumentCheckpointingStrategy Strategy { get; set; }

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("checkpointExists")]
		bool CheckpointExists { get; }

		[Async]
		[Export ("saveCheckpointWithCompletionHandler:")]
		void SaveCheckpoint ([NullAllowed] Action<bool, NSError> completionHandler);

		[Async]
		[Export ("deleteCheckpointWithCompletionHandler:")]
		void DeleteCheckpoint ([NullAllowed] Action<bool, NSError> completionHandler);
	}

	interface IPSPDFDocumentDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentDelegate {

		[Export ("pdfDocument:resolveCustomAnnotationPathToken:")]
		string ResolveCustomAnnotationPathToken (PSPDFDocument document, string pathToken);

		[Export ("pdfDocumentDidSave:")]
		void DidSave (PSPDFDocument document);

		[Export ("pdfDocument:saveDidFailWithError:")]
		void SaveDidFail (PSPDFDocument document, NSError error);
	}

	interface IPSPDFDocumentEditorDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentEditorDelegate {

		[Abstract]
		[Export ("documentEditorRequestsFullReload:")]
		void RequestsFullReload (PSPDFDocumentEditor editor);

		[Export ("documentEditor:didPerformChanges:")]
		void DidPerformChanges (PSPDFDocumentEditor editor, PSPDFEditingChange [] changes);
	}

	delegate void PSPDFDocumentEditorSuccessHandler (bool success, [NullAllowed] NSError error);
	delegate void PSPDFDocumentEditorSaveHandler ([NullAllowed] PSPDFDocument document, [NullAllowed] NSError error);
	delegate void PSPDFDocumentEditorImportHandler ([NullAllowed] PSPDFEditingChange [] changes, [NullAllowed] NSError error);
	delegate void PSPDFDocumentEditorGroupDatesHandler ([BlockCallback] PSPDFDocumentEditorGroupDatesHandlerCompletion response);
	delegate void PSPDFDocumentEditorGroupDatesHandlerCompletion (bool completed);

	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentEditor : PSPDFOverridable {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] PSPDFDocument document);

		[NullAllowed, Export ("document")]
		PSPDFDocument Document { get; }

		[NullAllowed, Export ("securityOptions", ArgumentSemantic.Strong)]
		PSPDFDocumentSecurityOptions SecurityOptions { get; set; }

		[Export ("addDelegate:")]
		void AddDelegate (IPSPDFDocumentEditorDelegate @delegate);

		[Export ("removeDelegate:")]
		bool RemoveDelegate (IPSPDFDocumentEditorDelegate @delegate);

		[Export ("pageCount")]
		nuint PageCount { get; }

		[Export ("pageSizeForPageAtIndex:")]
		CGSize GetPageSize (nuint pageIndex);

		[Export ("addPagesInRange:withConfiguration:")]
		PSPDFEditingChange [] AddPages (NSRange range, PSPDFNewPageConfiguration configuration);

		[Export ("groupUpdates:")]
		void GroupUpdates (PSPDFDocumentEditorGroupDatesHandler updateHandler);

		[Export ("groupUpdates:completion:")]
		void GroupUpdates (PSPDFDocumentEditorGroupDatesHandler updateHandler, [NullAllowed] Action<PSPDFEditingChange []> completionHandler);

		[Export ("movePages:to:")]
		PSPDFEditingChange [] MovePages (NSIndexSet pageIndexes, nuint destination);

		[Export ("removePages:")]
		PSPDFEditingChange [] RemovePages (NSIndexSet pageIndexes);

		[Export ("duplicatePages:")]
		PSPDFEditingChange [] DuplicatePages (NSIndexSet pageIndexes);

		[Export ("rotatePages:rotation:")]
		PSPDFEditingChange [] RotatePages (NSIndexSet pageIndexes, nint rotation);

		[Export ("rotationForPageAtIndex:")]
		nint GetRotationForPage (nuint pageIndex);

		[return: NullAllowed]
		[Export ("undo")]
		PSPDFEditingChange [] Undo ();

		[return: NullAllowed]
		[Export ("redo")]
		PSPDFEditingChange [] Redo ();

		[Export ("canRedo")]
		bool CanRedo { get; }

		[Export ("canUndo")]
		bool CanUndo { get; }

		[Export ("reset")]
		void Reset ();

		[Export ("canSave")]
		bool CanSave { get; }

		[Async]
		[Export ("saveWithCompletionBlock:")]
		void Save ([NullAllowed] PSPDFDocumentEditorSaveHandler handler);

		[Async]
		[Export ("saveToPath:withCompletionBlock:")]
		void Save (string path, [NullAllowed] PSPDFDocumentEditorSaveHandler handler);

		[Async]
		[Export ("saveToDataSink:withCompletionBlock:")]
		void Save (IPSPDFDataSink dataSink, [NullAllowed] PSPDFDocumentEditorSuccessHandler handler);

		[Async]
		[Export ("exportPages:toPath:withCompletionBlock:")]
		void ExportPages (NSIndexSet pageIndexes, string path, [NullAllowed] PSPDFDocumentEditorSaveHandler handler);

		[Async]
		[Export ("exportPages:toDataSink:withCompletionBlock:")]
		void ExportPages (NSIndexSet pageIndexes, IPSPDFDataSink dataSink, [NullAllowed] PSPDFDocumentEditorSuccessHandler handler);

		[Export ("importPagesTo:fromDocument:withCompletionBlock:queue:")]
		NSProgress ImportPages (nuint index, PSPDFDocument sourceDocument, [NullAllowed] PSPDFDocumentEditorImportHandler handler, [NullAllowed] DispatchQueue queue);

		[Export ("imageForPageAtIndex:size:scale:")]
		[return: NullAllowed]
		UIImage GetImage (nuint pageIndex, CGSize size, nfloat scale);
	}

	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFDocumentEditorConfiguration {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFDocumentEditorConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		NativeHandle Constructor (PSPDFDocumentEditorConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFDocumentEditorConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFDocumentEditorConfigurationBuilder> builderHandler);

		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFDocumentEditorConfiguration GetUpdatedConfiguration ([NullAllowed] Action<PSPDFDocumentEditorConfigurationBuilder> builderHandler);

		[Export ("pageTemplates")]
		PSPDFPageTemplate [] PageTemplates { get; }

		[Export ("currentDocumentPageSize")]
		PSPDFPageSize CurrentDocumentPageSize { get; }

		[Export ("pageSizes")]
		PSPDFPageSize [] PageSizes { get; }

		[Export ("currentDocumentDirectory")]
		PSPDFDirectory CurrentDocumentDirectory { get; }

		[Export ("saveDirectories")]
		PSPDFDirectory [] SaveDirectories { get; }

		[Export ("compressions")]
		PSPDFCompression [] Compressions { get; }

		[Export ("selectedTemplate")]
		PSPDFPageTemplate SelectedTemplate { get; }

		[Export ("selectedPageSize")]
		PSPDFPageSize SelectedPageSize { get; }

		[Export ("selectedOrientation")]
		PSPDFDocumentOrientation SelectedOrientation { get; }

		[Export ("selectedColor")]
		UIColor SelectedColor { get; }

		[Export ("selectedImage")]
		UIImage SelectedImage { get; }

		[Export ("selectedImagePageSize")]
		PSPDFPageSize SelectedImagePageSize { get; }

		[Export ("selectedCompression")]
		PSPDFCompression SelectedCompression { get; }

		[Export ("selectedSaveDirectory")]
		PSPDFDirectory SelectedSaveDirectory { get; }

		[Export ("userFacingCompressionEnabled")]
		bool UserFacingCompressionEnabled { get; }

		[Export ("allowExternalFileSource")]
		bool AllowExternalFileSource { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFPageSize {

		[Export ("initWithSize:name:")]
		[DesignatedInitializer]
		NativeHandle Constructor (CGSize size, string name);

		[Export ("size")]
		CGSize Size { get; }

		[Export ("name")]
		string Name { get; }

		[Export ("localizedName")]
		string LocalizedName { get; }

		[Export ("localizedSize")]
		string LocalizedSize { get; }

		[Export ("sizeForOrientation:")]
		CGSize GetSize (PSPDFDocumentOrientation orientation);
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFDirectory {

		[Static]
		[Export ("directoryWithPath:")]
		PSPDFDirectory FromPath (string path);

		[Static]
		[Export ("directoryWithPath:name:")]
		PSPDFDirectory FromPath (string path, [NullAllowed] string name);

		[Export ("initWithPath:name:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string path, [NullAllowed] string name);

		[Export ("path")]
		string Path { get; }

		[NullAllowed, Export ("name")]
		string Name { get; }

		[Export ("localizedName")]
		string LocalizedName { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFCompression {

		[Static]
		[Export ("compression:name:")]
		PSPDFCompression Create (nfloat compression, string name);

		[Export ("initWithCompression:name:")]
		[DesignatedInitializer]
		NativeHandle Constructor (nfloat compression, string name);

		[Export ("compression")]
		nfloat Compression { get; }

		[Export ("name")]
		string Name { get; }

		[Export ("localizedName")]
		string LocalizedName { get; }
	}

	interface IPSPDFDocumentEditorConfigurationConfigurable { }

	[Protocol]
	interface PSPDFDocumentEditorConfigurationConfigurable {

		// This ctor needs to be manually added to any class implementing this protocol
		//[Abstract] // TODO: add this to classes that implement this
		//[Export ("initWithDocumentEditorConfiguration:")]
		//NativeHandle Constructor (PSPDFDocumentEditorConfiguration configuration);

		[Abstract]
		[Export ("documentEditorConfiguration")]
		PSPDFDocumentEditorConfiguration DocumentEditorConfiguration { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDocumentPDFMetadata {

		[Export ("initWithDocument:")]
		NativeHandle Constructor (PSPDFDocument document);

		[Export ("initWithDocumentProvider:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("documentProvider")]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("allInfoKeys")]
		NSString [] AllInfoKeys { get; }

		[Export ("objectForInfoDictionaryKey:")]
		[return: NullAllowed]
		NSObject GetObject (NSString key);

		[Export ("setObject:forInfoDictionaryKey:")]
		void SetObject ([NullAllowed] NSObject @object, NSString key);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentProvider : PSPDFOverridable {

		[Export ("dataProvider")]
		IPSPDFDataProviding DataProvider { get; }

		[NullAllowed, Export ("fileURL")]
		NSUrl FileUrl { get; }

		[Export ("dataRepresentationWithError:")]
		[return: NullAllowed]
		NSData GetDataRepresentation ([NullAllowed] out NSError error);

		[Export ("fileSize")]
		ulong FileSize { get; }

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFDocumentProviderDelegate Delegate { get; set; }

		[Export ("pageInfoForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFPageInfo GetPageInfo (nuint pageIndex);

		[Export ("pageCount")]
		nuint PageCount { get; }

		[Export ("pageOffsetForDocument")]
		nuint PageOffsetForDocument { get; }

		[NullAllowed, Export ("password")]
		string Password { get; }

		[NullAllowed, Export ("contentSignature", ArgumentSemantic.Copy)]
		NSData ContentSignature { get; }

		[Export ("permissions")]
		PSPDFDocumentPermissions Permissions { get; }

		[Export ("permissionsLevel")]
		PSPDFDocumentPermissionsLevel PermissionsLevel { get; }

		[Export ("isEncrypted")]
		bool IsEncrypted { get; }

		[Export ("isEncryptedWithUserPassword")]
		bool IsEncryptedWithUserPassword { get; }

		[Export ("isUnlockedWithFullAccess")]
		bool IsUnlockedWithFullAccess { get; }

		[Export ("isUnlockedWithUserPassword")]
		bool IsUnlockedWithUserPassword { get; }

		[Export ("isLocked")]
		bool IsLocked { get; }

		[Export ("canEmbedAnnotations")]
		bool CanEmbedAnnotations { get; }

		[Export ("allowAnnotationChanges")]
		bool AllowAnnotationChanges { get; }

		[NullAllowed, Export ("fileId", ArgumentSemantic.Copy)]
		NSData FileId { get; }

		[Export ("title"), NullAllowed]
		string Title { get; }

		[Export ("textParserForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFTextParser GetTextParser (nuint pageIndex);

		[Export ("outlineParser")]
		PSPDFOutlineParser OutlineParser { get; }

		[NullAllowed, Export ("formParser")]
		PSPDFFormParser FormParser { get; }

		[Export ("annotationManager")]
		PSPDFAnnotationManager AnnotationManager { get; }

		[Export ("labelParser")]
		PSPDFLabelParser LabelParser { get; }

		[Export ("metadata", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> Metadata { get; }

		[NullAllowed, Export ("XMPMetadata")]
		string XmpMetadata { get; }

		[Export ("setRotationOffset:forPageAtIndex:")]
		void SetRotationOffset (nuint rotation, nuint pageIndex);

		[Export ("resolveNamedDestination:")]
		nuint ResolveNamedDestination (string namedDestination);

		[Export ("hashOfDataInRanges:algorithm:error:")]
		[return: NullAllowed]
		NSData GetHashOfData (NSIndexSet byteRanges, PSPDFSignatureHashAlgorithm hashAlgorithm, [NullAllowed] out NSError error);

		// PSPDFDocumentProvider (SubclassingHooks) Category

		[Advice ("You shouldn't call this method directly, use the high-level save method in 'PSPDFDocument' instead.")]
		[Export ("saveAnnotationsWithOptions:error:")]
		bool SaveAnnotations ([NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] out NSError error);

		[Export ("resolveTokenizedPath:alwaysLocal:")]
		string ResolveTokenizedPath (string path, bool alwaysLocal);

		// PSPDFDocument ()

		[Export ("measurementValueConfigurations")]
		PSPDFMeasurementValueConfiguration [] MeasurementValueConfigurations { get; }

		[Export ("activeMeasurementValueConfiguration")]
		[NullAllowed]
		PSPDFMeasurementValueConfiguration ActiveMeasurementValueConfiguration { get; set; }

		[Export ("addMeasurementValueConfiguration:")]
		bool AddMeasurementValueConfiguration (PSPDFMeasurementValueConfiguration valueConfiguration);

		[Export ("defaultMeasurementValueConfiguration")]
		[NullAllowed]
		PSPDFMeasurementValueConfiguration DefaultMeasurementValueConfiguration { get; set; }
	}

	interface IPSPDFDocumentProviderDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentProviderDelegate {

		[Export ("documentProvider:shouldAppendData:")]
		bool ShouldAppendData (PSPDFDocumentProvider documentProvider, NSData data);

		[Export ("documentProvider:didAppendData:")]
		void DidAppendData (PSPDFDocumentProvider documentProvider, NSData data);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentSecurityOptions {

		[Field ("PSPDFDocumentSecurityOptionsKeyLengthAutomatic", PSPDFKitLibraryPath.LibraryPath)]
		nuint KeyLengthAutomatic { get; }

		[Export ("initWithOwnerPassword:userPassword:error:")]
		NativeHandle Constructor ([NullAllowed] string ownerPassword, [NullAllowed] string userPassword, [NullAllowed] out NSError error);

		[Export ("initWithOwnerPassword:userPassword:keyLength:error:")]
		NativeHandle Constructor ([NullAllowed] string ownerPassword, [NullAllowed] string userPassword, nuint keyLength, [NullAllowed] out NSError error);

		[Export ("initWithOwnerPassword:userPassword:keyLength:permissions:error:")]
		NativeHandle Constructor ([NullAllowed] string ownerPassword, [NullAllowed] string userPassword, nuint keyLength, PSPDFDocumentPermissions documentPermissions, [NullAllowed] out NSError error);

		[Export ("initWithOwnerPassword:userPassword:keyLength:permissions:encryptionAlgorithm:error:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] string ownerPassword, [NullAllowed] string userPassword, nuint keyLength, PSPDFDocumentPermissions documentPermissions, PSPDFDocumentEncryptionAlgorithm encryptionAlgorithm, [NullAllowed] out NSError error);

		[NullAllowed, Export ("ownerPassword")]
		string OwnerPassword { get; }

		[NullAllowed, Export ("userPassword")]
		string UserPassword { get; }

		[Export ("keyLength")]
		nuint KeyLength { get; }

		[Export ("permissions")]
		PSPDFDocumentPermissions Permissions { get; }

		[Export ("encryptionAlgorithm")]
		PSPDFDocumentEncryptionAlgorithm EncryptionAlgorithm { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDocumentXMPMetadata {

		[Export ("initWithDocument:")]
		NativeHandle Constructor (PSPDFDocument document);

		[Export ("initWithDocumentProvider:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("document")]
		PSPDFDocument Document { get; }

		[Export ("documentProvider")]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Advice ("Use 'PSPDFXMPKeys'.")]
		[Export ("stringForXMPKey:namespace:")]
		[return: NullAllowed]
		string GetString (NSString xmpKey, string ns);

		[Advice ("Use 'PSPDFXMPKeys'.")]
		[Export ("setString:forXMPKey:namespace:suggestedNamespacePrefix:")]
		void SetString ([NullAllowed] string @string, NSString xmpKey, string ns, string nsPrefix);
	}

	[Static]
	interface PSPDFXMPKeys {

		[Field ("PSPDFXMPPDFNamespace", PSPDFKitLibraryPath.LibraryPath)]
		NSString PdfNamespaceKey { get; }

		[Field ("PSPDFXMPPDFNamespacePrefix", PSPDFKitLibraryPath.LibraryPath)]
		NSString PdfNamespacePrefixKey { get; }

		[Field ("PSPDFXMPDCNamespace", PSPDFKitLibraryPath.LibraryPath)]
		NSString DCNamespaceKey { get; }

		[Field ("PSPDFXMPDCNamespacePrefix", PSPDFKitLibraryPath.LibraryPath)]
		NSString DCNamespacePrefixKey { get; }
	}

	delegate bool PSPDFDownloadManagerObjectsPassingTestPredicate (IPSPDFRemoteContentObject obj, nuint index, ref bool stop);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDownloadManager {

		[Field ("PSPDFDownloadManagerDidStartLoadingTaskNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString DidStartLoadingTaskNotification { get; }

		[Field ("PSPDFDownloadManagerDidFinishLoadingTaskNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString DidFinishLoadingTaskNotification { get; }

		[Field ("PSPDFDownloadManagerDidFailToLoadTaskNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString DidFailToLoadTaskNotification { get; }

		[Export ("numberOfConcurrentDownloads")]
		nuint NumberOfConcurrentDownloads { get; set; }

		[Export ("enableDynamicNumberOfConcurrentDownloads")]
		bool EnableDynamicNumberOfConcurrentDownloads { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFDownloadManagerDelegate Delegate { get; set; }

		[Export ("shouldFinishLoadingObjectsInBackground")]
		bool ShouldFinishLoadingObjectsInBackground { get; set; }

		[Export ("enqueueObject:")]
		void EnqueueObject (IPSPDFRemoteContentObject @object);

		[Export ("enqueueObject:atFront:")]
		void EnqueueObject (IPSPDFRemoteContentObject @object, bool enqueueAtFront);

		[Export ("enqueueObjects:")]
		void EnqueueObjects (IPSPDFRemoteContentObject [] objects);

		[Export ("enqueueObjects:atFront:")]
		void EnqueueObjects (IPSPDFRemoteContentObject [] objects, bool enqueueAtFront);

		[Export ("cancelObject:")]
		void CancelObject (IPSPDFRemoteContentObject @object);

		[Export ("cancelAllObjects")]
		void CancelAllObjects ();

		[Export ("reachability")]
		PSPDFReachability Reachability { get; }

		[Export ("waitingObjects", ArgumentSemantic.Copy)]
		IPSPDFRemoteContentObject [] WaitingObjects { get; }

		[Export ("loadingObjects", ArgumentSemantic.Copy)]
		IPSPDFRemoteContentObject [] LoadingObjects { get; }

		[Export ("failedObjects", ArgumentSemantic.Copy)]
		IPSPDFRemoteContentObject [] FailedObjects { get; }

		[Export ("objectsPassingTest:")]
		IPSPDFRemoteContentObject [] GetObjectsPassingTest (PSPDFDownloadManagerObjectsPassingTestPredicate predicate);

		[Export ("handlesObject:")]
		bool HandlesObject (IPSPDFRemoteContentObject @object);

		[Export ("stateForObject:")]
		PSPDFDownloadManagerObjectState GetState (IPSPDFRemoteContentObject @object);
	}

	interface IPSPDFDownloadManagerDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFDownloadManagerDelegate {

		[Export ("downloadManager:authenticationChallenge:completionHandler:")]
		void AuthenticationChallenge (PSPDFDownloadManager downloadManager, NSUrlAuthenticationChallenge authenticationChallenge, Action<NSUrlSessionAuthChallengeDisposition, NSUrlCredential> completionHandler);

		[Export ("downloadManager:didChangeObject:")]
		void DidChangeObject (PSPDFDownloadManager downloadManager, IPSPDFRemoteContentObject @object);

		[Export ("downloadManager:reachabilityDidChange:")]
		void ReachabilityDidChange (PSPDFDownloadManager downloadManager, PSPDFReachability reachability);
	}

	interface IPSPDFDownloadManagerPolicy { }

	[Protocol]
	interface PSPDFDownloadManagerPolicy {

		[Abstract]
		[Export ("hasPermissionForNetworkEvent")]
		bool HasPermissionForNetworkEvent { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDrawingContextAppearanceStreamGenerator : PSPDFAppearanceStreamGenerating {

		[Static]
		[Export ("defaultGenerator", ArgumentSemantic.Strong)]
		PSPDFDrawingContextAppearanceStreamGenerator DefaultGenerator { get; }
	}

#if !NET
	[Category (allowStaticMembers: true)]
	[BaseType (typeof (NSValue))]
	interface NSValue_PSPDFModel {

		[Static]
		[Export ("pspdf_valueWithDrawingPoint:")]
		NSValue FromPSPDFDrawingPoint (PSPDFDrawingPoint point);

		[Export ("pspdf_drawingPointValue")]
		PSPDFDrawingPoint GetPSPDFDrawingPoint ();
	}
#endif

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFEditingChange {

		[Export ("initWithOperation:affectedPageIndex:destinationPageIndex:pageReferenceSourceIndex:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFEditingOperation operation, nuint affectedPageIndex, nuint destinationPageIndex, nuint pageReferenceSourceIndex);

		[Export ("operation")]
		PSPDFEditingOperation Operation { get; }

		[Export ("affectedPageIndex")]
		nuint AffectedPageIndex { get; }

		[Export ("destinationPageIndex")]
		nuint DestinationPageIndex { get; }

		[Export ("pageReferenceSourceIndex")]
		nuint PageReferenceSourceIndex { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFEmbeddedFile {
		
		[Export ("initWithFileURL:fileDescription:")]
		NativeHandle Constructor (NSUrl fileUrl, [NullAllowed] string fileDescription);

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("fileName")]
		string FileName { get; }

		[Export ("fileSize")]
		ulong FileSize { get; }

		[NullAllowed, Export ("fileDescription")]
		string FileDescription { get; }

		[NullAllowed, Export ("modificationDate")]
		NSDate ModificationDate { get; }

		[NullAllowed, Export ("fileURL")]
		NSUrl FileUrl { get; }

		[Export ("fileURLWithError:")]
		[return: NullAllowed]
		NSUrl GetFileUrl ([NullAllowed] out NSError error);
	}

	[BaseType (typeof (PSPDFGoToAction))]
	interface PSPDFEmbeddedGoToAction {

		[Export ("initWithRelativePath:targetRelationship:openInNewWindow:pageIndex:")]
		NativeHandle Constructor (string remotePath, PSPDFEmbeddedGoToActionTarget targetRelationship, bool openInNewWindow, nuint pageIndex);

		[Export ("targetRelationship")]
		PSPDFEmbeddedGoToActionTarget TargetRelationship { get; }

		[NullAllowed, Export ("relativePath")]
		string RelativePath { get; }

		[Export ("openInNewWindow")]
		bool OpenInNewWindow { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFFile {

		[Export ("initWithName:URL:data:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string fileName, [NullAllowed] NSUrl fileUrl, [NullAllowed] NSData fileData);

		[Export ("fileName")]
		string FileName { get; }

		[NullAllowed, Export ("fileURL")]
		NSUrl FileUrl { get; }

		[NullAllowed, Export ("fileData")]
		NSData FileData { get; }

		[Export ("mimeType")]
		string MimeType { get; }

		[Export ("fileDataMappedWithError:")]
		[return: NullAllowed]
		NSData GetFileDataMapped ([NullAllowed] out NSError error);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFFileAnnotation : PSPDFOverridable {

		[BindAs (typeof (PSPDFFileIconName))]
		[Export ("iconName")]
		NSString IconName { get; set; }

		[NullAllowed, Export ("embeddedFile", ArgumentSemantic.Strong)]
		PSPDFEmbeddedFile EmbeddedFile { get; set; }
	}

	[BaseType (typeof (PSPDFContainerAnnotationProvider))]
	[DisableDefaultCtor]
	interface PSPDFFileAnnotationProvider : PSPDFOverridable {

		[Export ("initWithDocumentProvider:")]
		NativeHandle Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("initWithDocumentProvider:fileURL:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocumentProvider documentProvider, [NullAllowed] NSUrl annotationFileUrl);

		[Export ("autodetectTextLinkTypes", ArgumentSemantic.Assign)]
		PSPDFTextCheckingType AutodetectTextLinkTypes { get; set; }

		[Export ("annotationsForPageAtIndex:"), New]
		[return: NullAllowed]
		new PSPDFAnnotation [] GetAnnotations (nuint pageIndex);

		[Export ("addAnnotations:options:")]
		[Advice ("Requires base call if override.")]
		[return: NullAllowed]
		new PSPDFAnnotation [] AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Wrap ("AddAnnotations (annotations, options: annotationOptions?.Dictionary)")]
		PSPDFAnnotation [] AddAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationOptions annotationOptions);

		[Export ("removeAnnotations:options:")]
		[Advice ("Requires base call if override.")]
		[return: NullAllowed]
		new PSPDFAnnotation [] RemoveAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Wrap ("RemoveAnnotations (annotations, options: annotationOptions?.Dictionary)")]
		PSPDFAnnotation [] RemoveAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationOptions annotationOptions);

		[Export ("clearCache")]
		void ClearCache ();

		// PSPDFFileAnnotationProvider (Advanced) Category

		[Export ("saveableTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType SaveableTypes { get; set; }

		[Export ("parsableTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType ParsableTypes { get; set; }

		[Export ("annotationsPath")]
		string AnnotationsPath { get; }

		// PSPDFFileAnnotationProvider (SubclassingHooks) Category

		[Export ("parseAnnotationsForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFAnnotation [] ParseAnnotations (nuint pageIndex);

		[Export ("saveAnnotationsWithOptions:error:"), New]
		new bool SaveAnnotations ([NullAllowed] NSDictionary options, [NullAllowed] out NSError error);

		[Export ("loadAnnotationsWithError:")]
		[return: NullAllowed]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> LoadAnnotations ([NullAllowed] out NSError error);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFileAppearanceStreamGenerator : PSPDFAppearanceStreamGenerating {

		[Export ("initWithFileURL:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSUrl fileUrl);

		[Export ("fileURL")]
		NSUrl FileUrl { get; }

		// /we need a generator fix here
		// [NullAllowed, Export ("drawingBlock", ArgumentSemantic.Copy)]
		// Action<CGContext> DrawingHandler { get; set; }
	}

	interface IPSPDFFileCoordinationDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFFileCoordinationDelegate {

		[Abstract]
		[Export ("presentedItemDidChangeForDataProvider:")]
		void PresentedItemDidChange (IPSPDFCoordinatedFileDataProviding dataProvider);

		[Abstract]
		[Export ("accommodatePresentedItemDeletionForDataProvider:completionHandler:")]
		void AccommodatePresentedItemDeletion (IPSPDFCoordinatedFileDataProviding dataProvider, Action<NSError> completionHandler);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFileDataProvider : PSPDFFileDataProviding {

		[Export ("initWithFileURL:progress:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSUrl fileUrl, [NullAllowed] NSProgress progress);

		[Export ("initWithFileURL:")]
		NativeHandle Constructor (NSUrl fileUrl);

		[Sealed, NullAllowed, Export ("progress")]
		NSProgress Progress { get; }

		[Export ("createDataSinkWithOptions:error:")]
		[return: NullAllowed]
		new IPSPDFDataSink CreateDataSink (PSPDFDataSinkOptions options, [NullAllowed] out NSError error);

		[Export ("replaceContentsWithDataSink:error:")]
		new bool ReplaceContents (IPSPDFDataSink replacementDataSink, [NullAllowed] out NSError error);

		[Sealed, Export ("canWrite")]
		bool CanWrite { get; }

		[Export ("deleteDataWithError:")]
		new bool DeleteData ([NullAllowed] out NSError error);

		[Export ("clearCache")]
		new void ClearCache ();
	}

	interface IPSPDFFileDataProviding { }

	[Protocol]
	interface PSPDFFileDataProviding : PSPDFDataProviding {

		[Abstract]
		[NullAllowed, Export ("fileURL")]
		NSUrl FileUrl { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFileDataSink : PSPDFDataSink {

		[Export ("initWithFileURL:options:error:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSUrl fileURL, PSPDFDataSinkOptions options, [NullAllowed] out NSError error);

		[Export ("options")]
		PSPDFDataSinkOptions Options { get; }

		[Export ("fileURL")]
		NSUrl FileUrl { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFileIndexItemDescriptor : INSSecureCoding {

		[Export ("documentPath")]
		string DocumentPath { get; }

		[Export ("documentUID")]
		string DocumentUid { get; }

		[Export ("lastModificationDate")]
		NSDate LastModificationDate { get; }

		[Export ("isEqualToFileIndexItemDescriptor:")]
		bool IsEqualTo (PSPDFFileIndexItemDescriptor other);
	}

	delegate bool PSPDFFileManagerErrorHandler (NSUrl url, NSError error);

	interface IPSPDFFileManager { }

	[Protocol]
	interface PSPDFFileManager {

		[Export ("usesEncryption")]
		[Abstract]
		bool UsesEncryption { get; }

		[Export ("allowsPolicyEvent:")]
		[Abstract]
		bool AllowsPolicyEvent (string policyEvent);

		[Export ("copyFileToUnencryptedLocationIfRequired:policyEvent:error:")]
		[Abstract]
		[return: NullAllowed]
		NSUrl CopyFileToUnencryptedLocationIfRequired ([NullAllowed] NSUrl fileUrl, string policyEvent, out NSError error);

		[Export ("cleanupIfTemporaryFile:")]
		[Abstract]
		bool CleanupIfTemporaryFile (NSUrl url);

		[Export ("createTemporaryWritableDataProviderWithPrefix:")]
		[return: NullAllowed]
		[Abstract]
		IPSPDFDataProviding CreateTemporaryWritableDataProvider ([NullAllowed] string prefix);

		[Export ("libraryDirectory")]
		[Abstract]
		string LibraryDirectory { get; }

		[Export ("applicationSupportDirectory")]
		[Abstract]
		string ApplicationSupportDirectory { get; }

		[Export ("cachesDirectory")]
		[Abstract]
		string CachesDirectory { get; }

		[Export ("documentDirectory")]
		[Abstract]
		string DocumentDirectory { get; }

		[Export ("cachesURL")]
		[Abstract]
		NSUrl CachesUrl { get; }

		[Export ("documentURL")]
		[Abstract]
		NSUrl DocumentUrl { get; }

		[Export ("temporaryDirectoryWithUID:")]
		[Abstract]
		string GetTemporaryDirectory ([NullAllowed] string uid);

		[Export ("cacheDirectoryWithPath:")]
		[Abstract]
		string GetCacheDirectory ([NullAllowed] string path);

		[Export ("unencryptedTemporaryDirectoryWithUID:")]
		[Abstract]
		[return: NullAllowed]
		string GetUnencryptedTemporaryDirectory (string uid);

		[Export ("isNativePath:")]
		[Abstract]
		bool IsNativePath ([NullAllowed] string path);

		[Export ("fileExistsAtPath:")]
		[Abstract]
		bool FileExists ([NullAllowed] string path);

		[Export ("fileExistsAtPath:isDirectory:")]
		[Abstract]
		bool FileExists ([NullAllowed] string path, ref bool isDirectory);

		[Export ("fileExistsAtURL:")]
		[Abstract]
		bool FileExists ([NullAllowed] NSUrl url);

		[Export ("fileExistsAtURL:isDirectory:")]
		[Abstract]
		bool FileExists ([NullAllowed] NSUrl url, ref bool isDirectory);

		[Export ("createFileAtPath:contents:attributes:")]
		[Abstract]
		bool CreateFile (string path, [NullAllowed] NSData data, [NullAllowed] NSDictionary<NSString, NSObject> attributes);

		[Export ("createDirectoryAtPath:withIntermediateDirectories:attributes:error:")]
		[Abstract]
		bool CreateDirectory (string path, bool createIntermediates, [NullAllowed] NSDictionary<NSString, NSObject> attributes, out NSError error);

		[Abstract]
		[Export ("createDirectoryAtURL:withIntermediateDirectories:attributes:error:")]
		bool CreateDirectory (NSUrl url, bool createIntermediates, [NullAllowed] NSDictionary<NSString, NSObject> attributes, out NSError error);

		[Export ("writeData:toFile:options:error:")]
		[Abstract]
		bool WriteData (NSData data, string path, NSDataWritingOptions writeOptionsMask, out NSError error);

		[Export ("writeData:toURL:options:error:")]
		[Abstract]
		bool WriteData (NSData data, NSUrl fileUrl, NSDataWritingOptions writeOptionsMask, out NSError error);

		[Export ("dataWithContentsOfFile:options:error:")]
		[Abstract]
		NSData GetData (string path, NSDataReadingOptions readOptionsMask, out NSError error);

		[Export ("dataWithContentsOfURL:options:error:")]
		[Abstract]
		NSData GetData (NSUrl fileUrl, NSDataReadingOptions readOptionsMask, out NSError error);

		[Export ("copyItemAtURL:toURL:error:")]
		[Abstract]
		bool CopyItem (NSUrl sourceUrl, NSUrl destinationUrl, out NSError error);

		[Export ("moveItemAtURL:toURL:error:")]
		[Abstract]
		bool MoveItem (NSUrl sourceUrl, NSUrl destinationUrl, out NSError error);

		[Export ("replaceItemAtURL:withItemAtURL:backupItemName:options:resultingItemURL:error:")]
		[Abstract]
		bool ReplaceItem (NSUrl originalItemUrl, NSUrl newItemUrl, [NullAllowed] string backupItemName, NSFileManagerItemReplacementOptions options, out NSUrl resultingUrl, out NSError error);

		[Export ("removeItemAtPath:error:")]
		[Abstract]
		bool RemoveItem (string path, out NSError error);

		[Export ("removeItemAtURL:error:")]
		[Abstract]
		bool RemoveItem (NSUrl Url, out NSError error);

		[Export ("attributesOfFileSystemForPath:error:")]
		[Abstract]
		[return: NullAllowed]
		NSDictionary<NSString, NSObject> GetAttributesOfFileSystem (string path, out NSError error);

		[Export ("attributesOfItemAtPath:error:")]
		[Abstract]
		[return: NullAllowed]
		NSDictionary<NSString, NSObject> GetAttributesOfItem ([NullAllowed] string path, out NSError error);

		[Export ("isDeletableFileAtPath:")]
		[Abstract]
		bool IsDeletableFile (string path);

		[Export ("isWritableFileAtPath:")]
		[Abstract]
		bool IsWritableFile (string path);

		[Export ("contentsOfDirectoryAtPath:error:")]
		[Abstract]
		string [] GetContentsOfDirectory (string path, out NSError error);

		[Export ("subpathsOfDirectoryAtPath:error:")]
		[Abstract]
		string [] GetSubpathsOfDirectory (string path, out NSError error);

		[Export ("enumeratorAtPath:")]
		[Abstract]
		NSDirectoryEnumerator GetEnumerator (string path);

		[Export ("enumeratorAtURL:includingPropertiesForKeys:options:errorHandler:")]
		[Abstract]
		NSDirectoryEnumerator GetEnumerator (NSUrl url, string [] keys, NSDirectoryEnumerationOptions options, [NullAllowed] PSPDFFileManagerErrorHandler handler);

		[Export ("destinationOfSymbolicLinkAtPath:error:")]
		[Abstract]
		string GetDestinationOfSymbolicLink (string path, out NSError error);

		[Export ("fileSystemRepresentationForPath:")]
		[Abstract]
		IntPtr GetFileSystemRepresentation (string path);

		[Export ("fileHandleForReadingFromURL:error:withBlock:")]
		[Abstract]
		bool GetFileHandleForReading (NSUrl url, out NSError error, Func<NSFileHandle, bool> reader);

		[Abstract]
		[Export ("setUbiquitous:itemAtURL:destinationURL:error:")]
		bool SetUbiquitous (bool flag, NSUrl url, NSUrl destinationUrl, out NSError error);

		[Abstract]
		[Export ("isUbiquitousItemAtURL:")]
		bool IsUbiquitousItem (NSUrl url);

		[Abstract]
		[Export ("startDownloadingUbiquitousItemAtURL:error:")]
		bool StartDownloadingUbiquitousItem (NSUrl url, out NSError error);

		[Abstract]
		[Export ("evictUbiquitousItemAtURL:error:")]
		bool EvictUbiquitousItem (NSUrl url, out NSError error);

		[Abstract]
		[Export ("URLForUbiquityContainerIdentifier:")]
		[return: NullAllowed]
		NSUrl GetUrlForUbiquityContainerIdentifier ([NullAllowed] string containerIdentifier);

		[Abstract]
		[Export ("URLForPublishingUbiquitousItemAtURL:expirationDate:error:")]
		[return: NullAllowed]
		NSUrl GetUrlForPublishingUbiquitousItem (NSUrl url, out NSDate outDate, out NSError error);

		[Abstract]
		[NullAllowed, Export ("ubiquityIdentityToken", ArgumentSemantic.Copy)]
		NSObject UbiquityIdentityToken { get; }

		[Abstract]
		[Export ("performBlockWithoutCoordination:")]
		void PerformHandlerWithoutCoordination (Action handler);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDefaultFileManager : PSPDFFileManager {

		[Export ("initWithOptions:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] NSDictionary<NSString, NSObject> options);
	}

	[Static]
	interface PSPDFFileManagerOptionKeys {

		[Field ("PSPDFFileManagerOptionCoordinatedAccess", PSPDFKitLibraryPath.LibraryPath)]
		NSString CoordinatedAccessKey { get; }

		[Field ("PSPDFFileManagerOptionFilePresenter", PSPDFKitLibraryPath.LibraryPath)]
		NSString FilePresenterKey { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFilePresenterCoordinator {

		[Static]
		[Export ("sharedCoordinator")]
		PSPDFFilePresenterCoordinator SharedCoordinator { get; }

		[Export ("observeFilePresenter:")]
		void ObserveFilePresenter (INSFilePresenter filePresenter);

		[Export ("unobserveFilePresenter:")]
		void UnobserveFilePresenter (INSFilePresenter filePresenter);

		[Export ("reloadFilePresenter:")]
		void ReloadFilePresenter (INSFilePresenter filePresenter);

		[Export ("observeFilePresenters:")]
		void ObserveFilePresenters ([NullAllowed] INSFilePresenter [] filePresenters);

		[Export ("unobserveFilePresenters:")]
		void UnobserveFilePresenters ([NullAllowed] INSFilePresenter [] filePresenters);
	}

	[BaseType (typeof (PSPDFWidgetAnnotation))]
	interface PSPDFFormElement {

		[NullAllowed, Export ("formField", ArgumentSemantic.Weak)]
		PSPDFFormField FormField { get; }

		[Export ("resettable")]
		bool Resettable { [Bind ("isResettable")] get; }

		[NullAllowed, Export ("defaultValue")]
		NSObject DefaultValue { get; }

		[NullAllowed, Export ("exportValue")]
		NSObject ExportValue { get; }

		[NullAllowed, Export ("highlightColor", ArgumentSemantic.Strong)]
		UIColor HighlightColor { get; set; }

		[NullAllowed, Export ("next", ArgumentSemantic.Weak)]
		PSPDFFormElement Next { get; set; }

		[NullAllowed, Export ("previous", ArgumentSemantic.Weak)]
		PSPDFFormElement Previous { get; set; }

		[Export ("calculationOrderIndex")]
		nuint CalculationOrderIndex { get; }

		[Export ("readOnly"), New]
		bool ReadOnly { [Bind ("isReadOnly")] get; }

		[Export ("required")]
		bool Required { [Bind ("isRequired")] get; }

		[Export ("noExport")]
		bool NoExport { [Bind ("isNoExport")] get; }

		[NullAllowed, Export ("fieldName")]
		string FieldName { get; }

		[NullAllowed, Export ("fullyQualifiedFieldName")]
		string FullyQualifiedFieldName { get; }

		[Export ("formTypeName")]
		string FormTypeName { get; }

		// PSPDFFormElement (Fonts) Category

		[Export ("maxLength")]
		nuint MaxLength { get; set; }

		[Export ("doNotScroll")]
		bool DoNotScroll { get; set; }

		[Export ("isMultiline")]
		bool IsMultiline { get; set; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFFormField : PSPDFOverridable {

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("type")]
		PSPDFFormFieldType Type { get; }

		[NullAllowed, Export ("name")]
		string Name { get; }

		[NullAllowed, Export ("fullyQualifiedName")]
		string FullyQualifiedName { get; }

		[NullAllowed, Export ("mappingName", ArgumentSemantic.Strong)]
		string MappingName { get; set; }

		[NullAllowed, Export ("alternateFieldName", ArgumentSemantic.Strong)]
		string AlternateFieldName { get; set; }

		[Export ("isEditable")]
		bool IsEditable { get; set; }

		[Export ("isReadOnly")]
		bool IsReadOnly { get; set; }

		[Export ("isRequired")]
		bool IsRequired { get; set; }

		[Export ("isNoExport")]
		bool IsNoExport { get; set; }

		[NullAllowed, Export ("defaultValue")]
		NSObject DefaultValue { get; }

		[NullAllowed, Export ("exportValue")]
		NSObject ExportValue { get; }

		[NullAllowed, Export ("value", ArgumentSemantic.Strong)]
		NSObject Value { get; set; }

		[Export ("calculationOrderIndex")]
		nuint CalculationOrderIndex { get; }

		[Export ("dirty")]
		bool Dirty { get; }

		[Export ("annotations")]
		PSPDFFormElement [] Annotations { get; }

		[Export ("nameForAnnotation:")]
		[return: NullAllowed]
		string GetName (PSPDFFormElement annotation);

		[Export ("fullyQualifiedNameForAnnotation:")]
		[return: NullAllowed]
		string GetFullyQualifiedName (PSPDFFormElement annotation);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFormOption : INSSecureCoding {

		[Export ("initWithLabel:value:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string label, string value);

		[Export ("label")]
		string Label { get; }

		[Export ("value")]
		string Value { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFormParser : PSPDFOverridable {

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("forms", ArgumentSemantic.Copy)]
		PSPDFFormElement [] Forms { get; }

		[NullAllowed, Export ("formFields", ArgumentSemantic.Copy)]
		PSPDFFormField [] FormFields { get; }

		[NullAllowed, Export ("dirtyForms")]
		PSPDFFormElement [] DirtyForms { get; }

		[Export ("findAnnotationWithFieldName:")]
		PSPDFFormElement FindAnnotation (string fieldName);

		[Export ("findFieldWithFullFieldName:")]
		PSPDFFormField FindField (string fullFieldName);

		[Export ("removeFormElements:error:")]
		bool RemoveFormElements (PSPDFFormElement [] formElements, [NullAllowed] out NSError error);

		[Export ("removeFormFields:error:")]
		bool RemoveFormFields (PSPDFFormField [] formFields, [NullAllowed] out NSError error);

		[Export ("resetForm:withFlags:error:")]
		bool ResetForm (PSPDFFormField [] formFields, PSPDFResetFormActionFlag resetFormActionFlags, [NullAllowed] out NSError error);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFFreeTextAnnotation : PSPDFRotatable, PSPDFOverridable {

		[Field ("PSPDFFreeTextAnnotationIntentTransformerName", PSPDFKitLibraryPath.LibraryPath)]
		NSString IntentTransformerName { get; }

		[Export ("initWithContents:")]
		NativeHandle Constructor (string contents);

		[Export ("initWithContents:calloutPoint1:")]
		NativeHandle Constructor (string contents, CGPoint point1);

		[Export ("intentType", ArgumentSemantic.Assign)]
		PSPDFFreeTextAnnotationIntent IntentType { get; set; }

		[Export ("point1", ArgumentSemantic.Assign)]
		CGPoint Point1 { get; set; }

		[Export ("kneePoint", ArgumentSemantic.Assign)]
		CGPoint KneePoint { get; set; }

		[Export ("point2", ArgumentSemantic.Assign)]
		CGPoint Point2 { get; set; }

		[Export ("lineEnd", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd { get; set; }

		[Export ("innerRectInset", ArgumentSemantic.Assign)]
		UIEdgeInsets InnerRectInset { get; set; }

		[Export ("sizeToFit")]
		void SizeToFit ();

		[Export ("sizeWithConstraints:")]
		CGSize GetSize (CGSize constraints);

		[Export ("enableVerticalResizing")]
		bool EnableVerticalResizing { get; set; }

		[Export ("enableHorizontalResizing")]
		bool EnableHorizontalResizing { get; set; }

		[Export ("setBoundingBox:transformSize:")]
		void SetBoundingBox (CGRect boundingBox, bool transformSize);

		[Export ("textBoundingBox", ArgumentSemantic.Assign)]
		CGRect TextBoundingBox { get; set; }

		[Export ("setTextBoundingBoxSize:")]
		void SetTextBoundingBoxSize (CGSize size);

		[Export ("convertIntentTypeTo:")]
		[return: NullAllowed]
		NSString [] ConvertIntentType (PSPDFFreeTextAnnotationIntent newIntent);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFGlyph : INSCopying, INSSecureCoding {

		[Export ("frame")]
		CGRect Frame { get; }

		[Export ("content")]
		string Content { get; }

		[Export ("lineBreaker")]
		bool LineBreaker { get; }

		[Export ("wordBreaker")]
		bool WordBreaker { [Bind ("isWordBreaker")] get; }

		[Export ("isWordOrLineBreaker")]
		bool IsWordOrLineBreaker { get; }

		[Export ("generated")]
		bool Generated { [Bind ("isGenerated")] get; }

		[Export ("indexOnPage")]
		nint IndexOnPage { get; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFGoToAction {

		[Export ("initWithPageIndex:")]
		NativeHandle Constructor (nuint pageIndex);

		[Export ("pageIndex")]
		nuint PageIndex { get; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFHideAction {

		[Export ("initWithAssociatedAnnotations:shouldHide:")]
		NativeHandle Constructor (PSPDFAnnotation [] annotations, bool shouldHide);

		[Export ("shouldHide")]
		bool ShouldHide { get; }

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; }
	}

	[BaseType (typeof (PSPDFTextMarkupAnnotation))]
	interface PSPDFHighlightAnnotation : PSPDFOverridable {

		[Static]
		[Export ("textOverlayAnnotationWithGlyphs:")]
		[return: NullAllowed]
		PSPDFHighlightAnnotation FromGlyphs ([NullAllowed] PSPDFGlyph[] glyphs);

		[Static]
		[Export ("textOverlayAnnotationWithRects:boundingBox:pageIndex:")]
		[return: NullAllowed]
		PSPDFHighlightAnnotation FromRects ([BindAs (typeof (CGRect[]))] NSValue[] rects, CGRect boundingBox, nuint pageIndex);
	}

	[BaseType (typeof (PSPDFDocument))]
	interface PSPDFImageDocument {

		[Export ("initWithImageDataProvider:")]
		NativeHandle Constructor (IPSPDFDataProviding imageDataProvider);

		[Export ("initWithImageURL:")]
		NativeHandle Constructor (NSUrl imageUrl);

		[NullAllowed, Export ("imageDataProvider")]
		IPSPDFDataProviding ImageDataProvider { get; }

		[NullAllowed, Export ("imageURL")]
		NSUrl ImageUrl { get; }

		[Export ("waitUntilLoaded")]
		void WaitUntilLoaded ();

		[Export ("imageSaveMode", ArgumentSemantic.Assign)]
		PSPDFImageSaveMode ImageSaveMode { get; set; }

		[Export ("compressionQuality")]
		nfloat CompressionQuality { get; set; }

		[Static]
		[Export ("supportedContentTypes")]
		NSSet<NSString> SupportedContentTypes { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFImageInfo : INSCopying, INSSecureCoding {

		[Export ("index")]
		nuint Index { get; }

		[Export ("pixelSize")]
		CGSize PixelSize { get; }

		[Export ("transform")]
		CGAffineTransform Transform { get; }

		[Export ("vertices")]
		NSValue [] Vertices { get; }

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[Export ("displaySize")]
		CGSize DisplaySize { get; }

		[Export ("horizontalResolution")]
		nfloat HorizontalResolution { get; }

		[Export ("verticalResolution")]
		nfloat VerticalResolution { get; }

		[Export ("hitTest:")]
		bool HitTest (CGPoint point);

		[Export ("boundingBox")]
		CGRect BoundingBox { get; }

		[Export ("imageWithError:")]
		[return: NullAllowed]
		UIImage GetImage ([NullAllowed] out NSError error);

		[Export ("imageInRGBColorSpaceWithError:")]
		[return: NullAllowed]
		UIImage GetImageInRGBColorSpace ([NullAllowed] out NSError error);
	}

	[BaseType (typeof (PSPDFAbstractShapeAnnotation))]
	interface PSPDFInkAnnotation : PSPDFOverridable {

		[Export ("initWithLines:"), Internal]
		IntPtr InitWithLines (IntPtr lines);

		[Advice ("You can use '[Get|Set]Lines' for a more strongly typed access.")]
		[NullAllowed, Export ("lines", ArgumentSemantic.Copy)]
		NSArray<NSValue> [] Lines { get; set; }

		[Export ("bezierPath")]
		UIBezierPath BezierPath { get; }

		[Export ("empty")]
		bool Empty { [Bind ("isEmpty")] get; }

		[Export ("naturalDrawingEnabled")]
		bool NaturalDrawingEnabled { get; set; }

		[Export ("isSignature")]
		bool IsSignature { get; set; }

		[Export ("setBoundingBox:transformLines:")]
		void SetBoundingBox (CGRect boundingBox, bool transformLines);

		[Export ("copyLinesByApplyingTransform:")]
		NSObject [] CopyLines (CGAffineTransform transform);
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFJavaScriptAction {

		[Export ("initWithScript:")]
		NativeHandle Constructor (string script);

		[NullAllowed, Export ("script")]
		string Script { get; }
	}

	delegate string PSPDFKitLogMessageHandler ();
	delegate void PSPDFKitLogHandler (PSPDFLogLevel type, IntPtr strTag, [BlockCallback] PSPDFKitLogMessageHandler message, IntPtr strFile, IntPtr strFunction, nuint line);
	delegate UIImage PSPDFKitImageLoadingHandler (string imageName);

	[BaseType (typeof (NSObject))]
	interface PSPDFKitGlobal {

		[Field ("PSPDFSettingKeyXCallbackURLString", PSPDFKitLibraryPath.LibraryPath)]
		NSString XCallbackUrlStringKey { get; }

		[Field ("PSPDFSettingKeyApplicationPolicy", PSPDFKitLibraryPath.LibraryPath)]
		NSString ApplicationPolicyKey { get; }

		[Field ("PSPDFSettingKeyFileManager", PSPDFKitLibraryPath.LibraryPath)]
		NSString FileManagerKey { get; }

		[Field ("PSPDFSettingKeyCoordinatedFileManager", PSPDFKitLibraryPath.LibraryPath)]
		NSString CoordinatedFileManagerKey { get; }

		[Field ("PSPDFSettingKeyFileCoordinationEnabled", PSPDFKitLibraryPath.LibraryPath)]
		NSString FileCoordinationEnabledKey { get; }

		[Field ("PSPDFSettingKeyLibraryIndexingPriority", PSPDFKitLibraryPath.LibraryPath)]
		NSString LibraryIndexingPriorityKey { get; }

		[Field ("PSPDFSettingKeyDebugMode", PSPDFKitLibraryPath.LibraryPath)]
		NSString DebugModeKey { get; }

		[Field ("PSPDFSettingKeyAdditionalFontDirectories", PSPDFKitLibraryPath.LibraryPath)]
		NSString AdditionalFontDirectoriesKey { get; }

		[Field ("PSPDFSettingKeyHonorDocumentPermissions", PSPDFKitLibraryPath.LibraryPath)]
		NSString HonorDocumentPermissionsKey { get; }

		[Field ("PSPDFSettingKeyLowMemoryMode", PSPDFKitLibraryPath.LibraryPath)]
		NSString LowMemoryModeKey { get; }

		[Static]
		[Export ("sharedInstance")]
		PSPDFKitGlobal SharedInstance { get; }

		[Internal]
		[Static]
		[Export ("setLicenseKey:")]
		void _SetLicenseKey ([NullAllowed] string licenseKey);

		[Internal]
		[Static]
		[Export ("setLicenseKey:options:")]
		void _SetLicenseKey ([NullAllowed] string licenseKey, [NullAllowed] NSDictionary options);

		[Static]
		[Export ("versionString")]
		string VersionString { get; }

		[Static]
		[Export ("versionNumber")]
		string VersionNumber { get; }

		[Static]
		[Export ("compiledAt")]
		NSDate CompiledAt { get; }

		[Static]
		[Export ("buildNumber")]
		nuint BuildNumber { get; }

		[Static]
		[Export ("isFeatureEnabled:")]
		bool IsFeatureEnabled (PSPDFFeatureMask feature);

		[Export ("objectForKeyedSubscript:")]
		[return: NullAllowed]
		NSObject GetObject (NSString key);

		[Export ("boolForKey:")]
		bool GetBool (NSString key);

		[Export ("setObject:forKeyedSubscript:")]
		void SetObject (NSObject @object, NSString key);

		[Export ("cache")]
		PSPDFCache Cache { get; }

		[Export ("fileManager")]
		IPSPDFFileManager FileManager { get; }

		[Export ("renderManager")]
		IPSPDFRenderManager RenderManager { get; }

		[Export ("styleManager")]
		IPSPDFAnnotationStyleManager StyleManager { get; }

		[Export ("signatureManager")]
		PSPDFSignatureManager SignatureManager { get; }

		[Export ("policy")]
		IPSPDFApplicationPolicy Policy { get; }

		[NullAllowed, Export ("library", ArgumentSemantic.Strong)]
		PSPDFLibrary Library { get; set; }

		[NullAllowed, Export ("databaseEncryptionProvider", ArgumentSemantic.Strong)]
		IPSPDFDatabaseEncryptionProvider DatabaseEncryptionProvider { get; set; }

		[Export ("injectDependentProperties:")]
		nint InjectDependentProperties (NSObject @object);

		// PSPDFKit (ImageLoading) Category
		[Static]
		[Export ("imageNamed:")]
		[return: NullAllowed]
		UIImage GetImage (string name);

		[NullAllowed, Export ("imageLoadingHandler", ArgumentSemantic.Strong)]
		PSPDFKitImageLoadingHandler ImageLoadingHandler { get; set; }

		[Export ("logLevel", ArgumentSemantic.Assign)]
		PSPDFLogLevel LogLevel { get; set; }

		[NullAllowed, Export ("logHandler", ArgumentSemantic.Strong)]
		PSPDFKitLogHandler LogHandler { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFLabelParser : PSPDFOverridable {

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("pageLabelForPageAtIndex:")]
		[return: NullAllowed]
		string GetPageLabel (nuint pageIndex);

		[Export ("pageForPageLabel:partialMatching:")]
		nuint GetPage (string pageLabel, bool partialMatching);

		[Export ("labels", ArgumentSemantic.Copy)]
		NSDictionary<NSNumber, NSString> Labels { get; }
	}

	[Static]
	interface PSPDFLibraryOptionsKeys {

		[Field ("PSPDFLibraryOptionMaximumSearchResultsTotal", PSPDFKitLibraryPath.LibraryPath)]
		NSString MaximumSearchResultsTotalKey { get; }

		[Field ("PSPDFLibraryOptionMaximumSearchResultsPerDocument", PSPDFKitLibraryPath.LibraryPath)]
		NSString MaximumSearchResultsPerDocumentKey { get; }

		[Field ("PSPDFLibraryOptionMaximumPreviewResultsTotal", PSPDFKitLibraryPath.LibraryPath)]
		NSString MaximumPreviewResultsTotalKey { get; }

		[Field ("PSPDFLibraryOptionMaximumPreviewResultsPerDocument", PSPDFKitLibraryPath.LibraryPath)]
		NSString MaximumPreviewResultsPerDocumentKey { get; }

		[Field ("PSPDFLibraryOptionMatchExactWordsOnly", PSPDFKitLibraryPath.LibraryPath)]
		NSString MatchExactWordsOnlyKey { get; }

		[Field ("PSPDFLibraryOptionMatchExactPhrasesOnly", PSPDFKitLibraryPath.LibraryPath)]
		NSString MatchExactPhrasesOnlyKey { get; }

		[Field ("PSPDFLibraryOptionExcludeAnnotations", PSPDFKitLibraryPath.LibraryPath)]
		NSString ExcludeAnnotationsKey { get; }

		[Field ("PSPDFLibraryOptionExcludeDocumentText", PSPDFKitLibraryPath.LibraryPath)]
		NSString ExcludeDocumentTextKey { get; }

		[Field ("PSPDFLibraryOptionPreviewRange", PSPDFKitLibraryPath.LibraryPath)]
		NSString PreviewRangeKey { get; }
	}

	[StrongDictionary ("PSPDFLibraryOptionsKeys")]
	interface PSPDFLibraryOptions {
		nuint MaximumSearchResultsTotal { get; set; }
		nuint MaximumSearchResultsPerDocument { get; set; }
		nuint MaximumPreviewResultsTotal { get; set; }
		nuint MaximumPreviewResultsPerDocument { get; set; }
		bool MatchExactWordsOnly { get; set; }
		bool MatchExactPhrasesOnly { get; set; }
		bool ExcludeAnnotations { get; set; }
		bool ExcludeDocumentText { get; set; }

		[Export ("PreviewRangeKey"), Internal]
		NSValue WeakPreviewRange { get; set; }
	}

	interface PSPDFLibraryDidFinishIndexingDocumentNotificationEventArgs {

		[Export ("PSPDFLibraryNotificationUIDKey")]
		string Uid { get; }

		[Export ("PSPDFLibraryNotificationSuccessKey")]
		[ProbePresence]
		bool Success { get; }
	}

	delegate void PSPDFLibraryFindDocumentUidsCompletionHandler (string searchString, NSDictionary<NSString, NSIndexSet> resultSet);
	delegate void PSPDFLibraryFindDocumentUidsPreviewTextHandler (string searchString, NSDictionary<NSString, NSSet<PSPDFLibraryPreviewResult>> resultSet);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFLibrary {

		[Field ("PSPDFLibraryWillStartIndexingDocumentNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString WillStartIndexingDocumentNotification { get; }

		[Field ("PSPDFLibraryDidFinishIndexingDocumentNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification (typeof (PSPDFLibraryDidFinishIndexingDocumentNotificationEventArgs))]
		NSString DidFinishIndexingDocumentNotification { get; }

		[Field ("PSPDFLibraryDidRemoveDocumentNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString DidRemoveDocumentNotification { get; }

		[Field ("PSPDFLibraryDidClearIndexesNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString DidClearIndexesNotification { get; }

		[Static]
		[Export ("libraryWithPath:error:")]
		[return: NullAllowed]
		PSPDFLibrary FromPath (string path, [NullAllowed] out NSError error);

		[Static]
		[Export ("libraryWithPath:tokenizer:error:")]
		[return: NullAllowed]
		PSPDFLibrary FromPath (string path, [NullAllowed] string tokenizer, [NullAllowed] out NSError error);

		[Static]
		[Export ("libraryWithPath:ftsVersion:tokenizer:error:")]
		[return: NullAllowed]
		PSPDFLibrary FromPath (string path, PSPDFLibraryFTSVersion ftsVersion, [NullAllowed] string tokenizer, [NullAllowed] out NSError error);

		[Static]
		[Export ("libraryWithPath:indexingPriority:ftsVersion:tokenizer:error:")]
		[return: NullAllowed]
		PSPDFLibrary FromPath (string path, PSPDFLibraryIndexingPriority priority, PSPDFLibraryFTSVersion ftsVersion, [NullAllowed] string tokenizer, [NullAllowed] out NSError error);

		[Static]
		[Export ("defaultLibraryPath")]
		string DefaultLibraryPath { get; }

		[Export ("path")]
		string Path { get; }

		[Export ("spotlightIndexingType", ArgumentSemantic.Assign)]
		PSPDFLibrarySpotlightIndexingType SpotlightIndexingType { get; set; }

		[Export ("shouldIndexAnnotations")]
		bool ShouldIndexAnnotations { get; set; }

		[NullAllowed, Export ("tokenizer")]
		string Tokenizer { get; }

		[Export ("saveReversedPageText")]
		bool SaveReversedPageText { get; set; }

		[Export ("suspended")]
		bool Suspended { get; set; }

		[Export ("automaticallyPauseLongRunningTasks")]
		bool AutomaticallyPauseLongRunningTasks { get; set; }

		[Export ("maximumContiguousIndexingTime")]
		double MaximumContiguousIndexingTime { get; set; }

		[Export ("automaticPauseDuration")]
		double AutomaticPauseDuration { get; set; }

		[Async (ResultTypeName = "PSPDFLibraryFindDocumentUidsResults")]
		[Export ("documentUIDsMatchingString:options:completionHandler:")]
		void FindDocumentUids (string searchString, [NullAllowed] NSDictionary options, PSPDFLibraryFindDocumentUidsCompletionHandler completionHandler);

		[Async (ResultTypeName = "PSPDFLibraryFindDocumentUidsResults")]
		[Wrap ("FindDocumentUids (searchString, searhcOptions?.Dictionary, completionHandler)")]
		void FindDocumentUids (string searchString, PSPDFLibraryOptions searhcOptions, PSPDFLibraryFindDocumentUidsCompletionHandler completionHandler);

		[Export ("documentUIDsMatchingString:options:completionHandler:previewTextHandler:")]
		void FindDocumentUids (string searchString, [NullAllowed] NSDictionary options, [NullAllowed] PSPDFLibraryFindDocumentUidsCompletionHandler completionHandler, [NullAllowed] PSPDFLibraryFindDocumentUidsPreviewTextHandler previewTextHandler);

		[Wrap ("FindDocumentUids (searchString, searhcOptions?.Dictionary, completionHandler, previewTextHandler)")]
		void FindDocumentUids (string searchString, PSPDFLibraryOptions searhcOptions, [NullAllowed] PSPDFLibraryFindDocumentUidsCompletionHandler completionHandler, [NullAllowed] PSPDFLibraryFindDocumentUidsPreviewTextHandler previewTextHandler);

		[Export ("indexStatusForUID:withProgress:")]
		PSPDFLibraryIndexStatus GetIndexStatus (string uid, out nfloat outProgress);

		[Export ("indexing")]
		bool Indexing { [Bind ("isIndexing")] get; }

		[Export ("queuedUIDs")]
		NSOrderedSet<NSString> QueuedUids { get; }

		[NullAllowed, Export ("indexedUIDs")]
		NSOrderedSet<NSString> IndexedUids { get; }

		[Export ("indexedUIDCount")]
		nint IndexedUidCount { get; }

		[Export ("indexedDocumentWithUID:")]
		[return: NullAllowed]
		PSPDFDocument GetIndexedDocument (string uid);

		[NullAllowed, Export ("dataSource", ArgumentSemantic.Strong)]
		IPSPDFLibraryDataSource DataSource { get; set; }

		[Async]
		[Export ("updateIndexWithCompletionHandler:")]
		void UpdateIndex ([NullAllowed] Action completionHandler);

		[Export ("removeIndexForUID:")]
		void RemoveIndex (string uid);

		[Export ("clearAllIndexes")]
		void ClearAllIndexes ();

		[Async]
		[Export ("fetchSpotlightIndexedDocumentForUserActivity:completionHandler:")]
		void FetchSpotlightIndexedDocument (NSUserActivity userActivity, Action<PSPDFDocument> completionHandler);

		[Export ("cancelAllPreviewTextOperations")]
		void CancelAllPreviewTextOperations ();

		// PSPDFLibrary (EncryptionSupport) Category

		[Static]
		[Export ("encryptedLibraryWithPath:encryptionKeyProvider:error:")]
		PSPDFLibrary CreateEncryptedLibrary (string path, [NullAllowed] Func<NSData> encryptionKeyProvider, [NullAllowed] out NSError error);

		[Static]
		[Export ("encryptedLibraryWithPath:encryptionKeyProvider:tokenizer:error:")]
		PSPDFLibrary CreateEncryptedLibrary (string path, [NullAllowed] Func<NSData> encryptionKeyProvider, [NullAllowed] string tokenizer, [NullAllowed] out NSError error);

		[Static]
		[Export ("encryptedLibraryWithPath:encryptionKeyProvider:ftsVersion:tokenizer:error:")]
		PSPDFLibrary CreateEncryptedLibrary (string path, [NullAllowed] Func<NSData> encryptionKeyProvider, PSPDFLibraryFTSVersion ftsVersion, [NullAllowed] string tokenizer, [NullAllowed] out NSError error);

		[Static]
		[Export ("encryptedLibraryWithPath:encryptionKeyProvider:indexingPriority:ftsVersion:tokenizer:error:")]
		PSPDFLibrary CreateEncryptedLibrary (string path, [NullAllowed] Func<NSData> encryptionKeyProvider, PSPDFLibraryIndexingPriority priority, PSPDFLibraryFTSVersion ftsVersion, [NullAllowed] string tokenizer, [NullAllowed] out NSError error);

		[Export ("encrypted")]
		bool Encrypted { [Bind ("isEncrypted")] get; }
	}

	interface IPSPDFLibraryDataSource { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFLibraryDataSource {

		[Export ("libraryWillBeginIndexing:")]
		void WillBeginIndexing (PSPDFLibrary library);

		[Export ("library:didFinishIndexingDocumentWithUID:success:")]
		void DidFinishIndexingDocument (PSPDFLibrary library, string documentUid, bool success);

		[Export ("library:didRemoveDocumentWithUID:")]
		void DidRemoveDocument (PSPDFLibrary library, string documentUid);

		[Abstract]
		[Export ("uidsOfDocumentsToBeIndexedByLibrary:")]
		string [] GetUidsOfDocumentsToBeIndexed (PSPDFLibrary library);

		[Abstract]
		[Export ("uidsOfDocumentsToBeRemovedFromLibrary:")]
		string [] GetUidsOfDocumentsToBeRemoved (PSPDFLibrary library);

		[Abstract]
		[Export ("library:documentWithUID:")]
		[return: NullAllowed]
		PSPDFDocument GetDocument (PSPDFLibrary library, string uid);
	}

	delegate bool PSPDFLibraryFileSystemDataSourceDocumentHandler (PSPDFDocument document, ref bool stop);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFLibraryFileSystemDataSource : PSPDFLibraryDataSource {

		[Export ("initWithLibrary:documentsDirectoryURL:documentHandler:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFLibrary library, NSUrl url, [NullAllowed] PSPDFLibraryFileSystemDataSourceDocumentHandler documentHandler);

		[NullAllowed, Export ("library", ArgumentSemantic.Weak)]
		PSPDFLibrary Library { get; }

		[Export ("documentsDirectoryURL")]
		NSUrl DocumentsDirectoryUrl { get; }

		[NullAllowed, Export ("documentHandler", ArgumentSemantic.Copy)]
		PSPDFLibraryFileSystemDataSourceDocumentHandler DocumentHandler { get; set; }

		[Export ("directoryEnumerationOptions", ArgumentSemantic.Assign)]
		NSDirectoryEnumerationOptions DirectoryEnumerationOptions { get; set; }

		[Export ("allowedPathExtensions"), NullAllowed]
		NSSet<NSString> AllowedPathExtensions { get; set; }

		[Export ("indexItemDescriptorForDocumentWithUID:")]
		[return: NullAllowed]
		PSPDFFileIndexItemDescriptor GetIndexItemDescriptorForDocument (string documentUid);

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		IPSPDFLibraryFileSystemDataSourceDocumentProvider DocumentProvider { get; set; }

		[Export ("explicitModeEnabled")]
		bool ExplicitModeEnabled { [Bind ("isExplicitModeEnabled")] get; set; }

		[Export ("didAddOrModifyDocumentAtURL:")]
		void DidAddOrModifyDocument (NSUrl url);

		[Export ("didRemoveDocumentAtURL:")]
		void DidRemoveDocument (NSUrl url);
	}

	interface IPSPDFLibraryFileSystemDataSourceDocumentProvider { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFLibraryFileSystemDataSourceDocumentProvider {

		[Abstract]
		[Export ("dataSource:documentWithUID:atURL:")]
		[return: NullAllowed]
		PSPDFDocument GetDocument (PSPDFLibraryFileSystemDataSource dataSource, [NullAllowed] string uid, NSUrl fileUrl);
	}

	[BaseType (typeof (PSPDFSearchResult))]
	interface PSPDFLibraryPreviewResult : INativeObject {

		[Export ("annotationObjectNumber")]
		nint AnnotationObjectNumber { get; }

		[Export ("isAnnotationResult")]
		bool IsAnnotationResult { get; }
	}

	[BaseType (typeof (PSPDFAbstractLineAnnotation))]
	interface PSPDFLineAnnotation : PSPDFOverridable {

		[Export ("initWithPoint1:point2:")]
		NativeHandle Constructor (CGPoint point1, CGPoint point2);

		[Export ("point1", ArgumentSemantic.Assign)]
		CGPoint Point1 { get; set; }

		[Export ("point2", ArgumentSemantic.Assign)]
		CGPoint Point2 { get; set; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFLinkAnnotation : PSPDFOverridable {

		[Export ("initWithLinkAnnotationType:")]
		NativeHandle Constructor (PSPDFLinkAnnotationType linkAnnotationType);

		[Export ("initWithAction:")]
		NativeHandle Constructor (PSPDFAction action);

		[Export ("initWithURL:")]
		NativeHandle Constructor (NSUrl url);

		[Export ("linkType", ArgumentSemantic.Assign)]
		PSPDFLinkAnnotationType LinkType { get; set; }

		[NullAllowed, Export ("action", ArgumentSemantic.Strong)]
		PSPDFAction Action { get; set; }

		[NullAllowed, Export ("URLAction")]
		PSPDFURLAction UrlAction { get; }

		[NullAllowed, Export ("URL", ArgumentSemantic.Copy)]
		NSUrl Url { get; }

		[Export ("showAsLinkView")]
		bool ShowAsLinkView { get; }

		[Export ("multimediaExtension")]
		bool MultimediaExtension { [Bind ("isMultimediaExtension")] get; }

		[Export ("controlsEnabled")]
		bool ControlsEnabled { get; set; }

		[Export ("autoplayEnabled")]
		bool AutoplayEnabled { [Bind ("isAutoplayEnabled")] get; set; }

		[Export ("loopEnabled")]
		bool LoopEnabled { [Bind ("isLoopEnabled")] get; set; }

		[Export ("fullscreenEnabled")]
		bool FullscreenEnabled { [Bind ("isFullscreenEnabled")] get; set; }

		[NullAllowed, Export ("targetString")]
		string TargetString { get; }
	}

	[Category]
	[BaseType (typeof (NSObject))]
	interface NSObject_PSPDFLocalizedAccessibility {

		[Export ("pspdf_accessibility")]
		string GetPSPDFAccessibility ();

		[Export ("setPspdf_accessibility:")]
		void SetPSPDFAccessibility (string str);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFMemoryCache {

		[Export ("count")]
		nuint Count { get; }

		[Export ("numberOfPixels")]
		long NumberOfPixels { get; }

		[Export ("maxNumberOfPixels")]
		long MaxNumberOfPixels { get; set; }

		[Export ("maxNumberOfPixelsUnderStress")]
		long MaxNumberOfPixelsUnderStress { get; set; }
	}

	[Abstract]
	[BaseType (typeof (NSObject))]
	interface PSPDFModel : INSCopying, INSSecureCoding {

		[Static]
		[return: NullAllowed]
		[Export ("modelWithDictionary:error:")]
		PSPDFModel FromDictionary ([NullAllowed] NSDictionary dictionaryValue, [NullAllowed] out NSError error);

		[Export ("initWithDictionary:error:")]
		NativeHandle Constructor ([NullAllowed] NSDictionary dictionaryValue, [NullAllowed] out NSError error);

		[Static]
		[Export ("propertyKeys")]
		NSOrderedSet<NSString> PropertyKeys { get; }

		[Export ("dictionaryValue", ArgumentSemantic.Copy)]
		NSDictionary DictionaryValue { get; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFNamedAction {

		[Field ("PSPDFNamedActionTypeTransformerName", PSPDFKitLibraryPath.LibraryPath)]
		NSString TransformerName { get; }

		[Export ("initWithActionNamedString:")]
		NativeHandle Constructor ([NullAllowed] string actionNameString);

		[Export ("initWithNamedActionType:")]
		NativeHandle Constructor (PSPDFNamedActionType namedActionType);

		[Export ("namedActionType")]
		PSPDFNamedActionType NamedActionType { get; }

		[NullAllowed, Export ("namedAction")]
		string NamedAction { get; }

		[Export ("pageIndexWithCurrentPage:fromDocument:")]
		nuint GetPageIndex (nuint currentPage, PSPDFDocument document);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFNewPageConfiguration {

		[Static]
		[Export ("newPageConfigurationWithPageTemplate:builderBlock:")]
		PSPDFNewPageConfiguration FromPageTemplate (PSPDFPageTemplate pageTemplate, [NullAllowed] Action<PSPDFNewPageConfigurationBuilder> builderBlock);

		[Export ("pageSize")]
		CGSize PageSize { get; }

		[Export ("pageRotation")]
		PSPDFRotation PageRotation { get; }

		[NullAllowed, Export ("backgroundColor")]
		UIColor BackgroundColor { get; }

		[Export ("pageMargins")]
		UIEdgeInsets PageMargins { get; }

		[NullAllowed, Export ("pageTemplate")]
		PSPDFPageTemplate PageTemplate { get; }

		[NullAllowed, Export ("item")]
		PSPDFProcessorItem Item { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFNewPageConfigurationBuilder {

		[Export ("pageSize", ArgumentSemantic.Assign)]
		CGSize PageSize { get; set; }

		[Export ("pageRotation")]
		nuint PageRotation { get; set; }

		[NullAllowed, Export ("backgroundColor", ArgumentSemantic.Strong)]
		UIColor BackgroundColor { get; set; }

		[NullAllowed, Export ("item", ArgumentSemantic.Strong)]
		PSPDFProcessorItem Item { get; set; }

		[Export ("pageMargins", ArgumentSemantic.Assign)]
		UIEdgeInsets PageMargins { get; set; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFNoteAnnotation : PSPDFOverridable {

		[Export ("initWithContents:")]
		NativeHandle Constructor (string contents);

		[Export ("iconName")]
		string IconName { get; set; }

		[Export ("authorStateModel", ArgumentSemantic.Assign)]
		PSPDFAnnotationAuthorStateModel AuthorStateModel { get; set; }

		[Export ("authorState", ArgumentSemantic.Assign)]
		PSPDFAnnotationAuthorState AuthorState { get; set; }

		// PSPDFNoteAnnotation (SubclassingHooks) Category

		[NullAllowed, Export ("renderAnnotationIcon")]
		UIImage RenderAnnotationIcon { get; }

		[Export ("shouldDrawIconAsIs")]
		bool ShouldDrawIconAsIs { get; }

		[Export ("drawImageInContext:boundingBox:options:")]
		void DrawImage (CGContext context, CGRect boundingBox, [NullAllowed] PSPDFRenderOptions options);

		[Export ("boundingBoxIfRenderedAsText")]
		CGRect BoundingBoxIfRenderedAsText { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFOutlineElement {

		[Export ("initWithTitle:color:fontTraits:action:children:level:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] string title, [NullAllowed] UIColor color, UIFontDescriptorSymbolicTraits fontTraits, [NullAllowed] PSPDFAction action, [NullAllowed] PSPDFOutlineElement [] children, nuint level);

		[Export ("flattenedChildren")]
		PSPDFOutlineElement [] FlattenedChildren { get; }

		[Export ("allFlattenedChildren")]
		PSPDFOutlineElement [] AllFlattenedChildren { get; }

		[NullAllowed, Export ("parent", ArgumentSemantic.Weak)]
		PSPDFOutlineElement Parent { get; }

		[NullAllowed, Export ("title")]
		string Title { get; }

		[NullAllowed, Export ("action")]
		PSPDFAction Action { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[NullAllowed, Export ("color")]
		UIColor Color { get; }

		[Export ("fontTraits")]
		UIFontDescriptorSymbolicTraits FontTraits { get; }

		[NullAllowed, Export ("children", ArgumentSemantic.Copy)]
		PSPDFOutlineElement [] Children { get; }

		[Export ("level")]
		nuint Level { get; }

		[Export ("expanded")]
		bool Expanded { [Bind ("isExpanded")] get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFOutlineParser : PSPDFOverridable {

		[Export ("outlineElementForPageAtIndex:exactPageOnly:")]
		[return: NullAllowed]
		PSPDFOutlineElement GetOutlineElement (nuint pageIndex, bool exactPageOnly);

		[Export ("outline", ArgumentSemantic.Strong)]
		PSPDFOutlineElement Outline { get; set; }

		[Export ("outlineParsed")]
		bool OutlineParsed { [Bind ("isOutlineParsed")] get; }

		[Export ("outlineAvailable")]
		bool OutlineAvailable { [Bind ("isOutlineAvailable")] get; }

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }
	}

	interface IPSPDFOverridable { }

	[Protocol]
	interface PSPDFOverridable {

	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFPageInfo : INSCopying, INSSecureCoding {

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("size")]
		CGSize Size { get; }

		[Export ("savedRotation")]
		PSPDFRotation SavedRotation { get; }

		[Export ("rotationOffset")]
		PSPDFRotation RotationOffset { get; }

		[Export ("transform")]
		CGAffineTransform Transform { get; }

		[Export ("mediaBox")]
		CGRect MediaBox { get; }

		[Export ("cropBox")]
		CGRect CropBox { get; }

		[Export ("trimBox")]
		CGRect TrimBox { get; }

		[Export ("bleedBox")]
		CGRect BleedBox { get; }

		[NullAllowed, Export ("additionalActions", ArgumentSemantic.Copy)]
		NSDictionary<NSNumber, PSPDFAction> AdditionalActions { get; }

		[Export ("allowAnnotationCreation")]
		bool AllowAnnotationCreation { get; }
	}

	delegate void PSPDFPKCS12UnlockHandler ([NullAllowed] PSPDFX509 x509, [NullAllowed] PSPDFPrivateKey pk, [NullAllowed] NSError error);
	delegate void PSPDFPKCS12UnlockCertHandler ([NullAllowed] PSPDFX509 [] certificateChain, [NullAllowed] PSPDFPrivateKey pk, [NullAllowed] NSError error);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFPKCS12 : INSSecureCoding {

		[Export ("initWithData:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSData data);

		[Export ("data")]
		NSData Data { get; }

		[Async (ResultTypeName = "PSPDFPKCS12UnlockHandlerResult")]
		[Export ("unlockWithPassword:done:")]
		void Unlock (string password, [NullAllowed] PSPDFPKCS12UnlockHandler done);

		[Async (ResultTypeName = "PSPDFPKCS12UnlockCertHandlerResult")]
		[Export ("unlockCertificateChainWithPassword:done:")]
		void UnlockCertificateChain (string password, [NullAllowed] PSPDFPKCS12UnlockCertHandler done);
	}

	delegate void PSPDFPKCS12SignerSignFormElementCompletionHandler (bool success, PSPDFDocument document, NSError error);
	delegate void PSPDFPKCS12SignerSignFormElementDataSinkCompletionHandler (bool success, IPSPDFDataSink dataSink, NSError error);

	[BaseType (typeof (PSPDFSigner))]
	[DisableDefaultCtor]
	interface PSPDFPKCS12Signer {

		[Export ("initWithDisplayName:PKCS12:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string displayName, PSPDFPKCS12 p12);

		[Export ("p12")]
		PSPDFPKCS12 P12 { get; }

		[Async (ResultTypeName = "PSPDFPKCS12SignerSignFormElementCompletionHandlerResult")]
		[Export ("signFormElement:usingPassword:writeTo:completion:")]
		void SignFormElement (PSPDFSignatureFormElement element, string password, string path, [NullAllowed] PSPDFPKCS12SignerSignFormElementCompletionHandler completionHandler);

		[Async (ResultTypeName = "PSPDFPKCS12SignerSignFormElementCompletionHandlerResult")]
		[Export ("signFormElement:usingPassword:writeTo:appearance:biometricProperties:completion:")]
		void SignFormElement (PSPDFSignatureFormElement element, string password, string path, [NullAllowed] PSPDFSignatureAppearance signatureAppearance, [NullAllowed] PSPDFSignatureBiometricProperties biometricProperties, [NullAllowed] PSPDFPKCS12SignerSignFormElementCompletionHandler completionHandler);

		[Async (ResultTypeName = "PSPDFPKCS12SignerSignFormElementDataSinkCompletionHandlerResult")]
		[Export ("signFormElement:usingPassword:writeToDataSink:completionBlock:")]
		void SignFormElement (PSPDFSignatureFormElement element, string password, IPSPDFDataSink dataSink, [NullAllowed] PSPDFPKCS12SignerSignFormElementDataSinkCompletionHandler completionHandler);

		[Async (ResultTypeName = "PSPDFPKCS12SignerSignFormElementCompletionHandlerResult")]
		[Export ("signFormElement:usingPassword:writeTo:completionBlock:")]
		void SignFormElement2 (PSPDFSignatureFormElement element, string password, string path, [NullAllowed] PSPDFPKCS12SignerSignFormElementCompletionHandler completionHandler);
	}

	[BaseType (typeof (PSPDFAbstractLineAnnotation))]
	interface PSPDFPolygonAnnotation : PSPDFOverridable {

		[Export ("initWithPoints:intentType:")]
		NativeHandle Constructor (NSValue [] points, PSPDFPolygonAnnotationIntent intentType);

		[Export ("intentType", ArgumentSemantic.Assign)]
		PSPDFPolygonAnnotationIntent IntentType { get; set; }
	}

	[BaseType (typeof (PSPDFAbstractLineAnnotation))]
	interface PSPDFPolyLineAnnotation : PSPDFOverridable {

		[Export ("initWithPoints:")]
		NativeHandle Constructor (NSValue [] points);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFPopupAnnotation : PSPDFOverridable {

		[Export ("open")]
		bool Open { [Bind ("isOpen")] get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFPrivateKey {

		[Static]
		[Export ("createFromRawPrivateKey:encoding:")]
		[return: NullAllowed]
		PSPDFPrivateKey Create (NSData rawPrivateKey, PSPDFPrivateKeyEncoding encoding);

		[Export ("signatureEncryptionAlgorithm")]
		PSPDFSignatureEncryptionAlgorithm SignatureEncryptionAlgorithm { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFProcessor {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFProcessorDelegate Delegate { get; set; }

		[Export ("initWithConfiguration:securityOptions:")]
		NativeHandle Constructor (PSPDFProcessorConfiguration configuration, [NullAllowed] PSPDFDocumentSecurityOptions securityOptions);

		[Export ("writeToFileURL:error:")]
		bool WriteToFile (NSUrl fileUrl, [NullAllowed] out NSError error);

		[Export ("dataWithError:")]
		[return: NullAllowed]
		NSData GetData ([NullAllowed] out NSError error);

		[Export ("outputToDataSink:error:")]
		bool OutputToDataSink (IPSPDFDataSink outputDataSink, [NullAllowed] out NSError error);

		[Export ("cancel")]
		void Cancel ();

#if !__MACCATALYST__

		[Static]
		[Export ("generatePDFFromURL:options:completionBlock:")]
		PSPDFUrlConversionOperation GeneratePdf (NSUrl inputUrl, [NullAllowed] NSDictionary dicOptions, [NullAllowed] Action<NSData, NSError> completion); // TODO: PSPDFProcessorGenerationOptions ?

		[Static]
		[Export ("generatePDFFromURL:outputFileURL:options:completionBlock:")]
		PSPDFUrlConversionOperation GeneratePdf (NSUrl inputUrl, NSUrl outputFileUrl, [NullAllowed] NSDictionary dicOptions, [NullAllowed] Action<NSUrl, NSError> completion);

		[Static]
		[Export ("generatePDFFromHTMLString:options:completionBlock:")]
		PSPDFHtmlConversionOperation GeneratePdf (string htmlString, [NullAllowed] NSDictionary dicOptions, [NullAllowed] Action<NSData, NSError> completion);

		[Static]
		[Export ("generatePDFFromHTMLString:outputFileURL:options:completionBlock:")]
		PSPDFHtmlConversionOperation GeneratePdf (string htmlString, NSUrl outputFileUrl, [NullAllowed] NSDictionary dicOptions, [NullAllowed] Action<NSUrl, NSError> completion);

		[Static]
		[Export ("generatePDFFromAttributedString:options:completionBlock:")]
		PSPDFAttributedStringConversionOperation GeneratePdf (NSAttributedString attributedString, [NullAllowed] NSDictionary dicOptions, [NullAllowed] Action<NSData, NSError> completion);

		[Static]
		[Export ("generatePDFFromAttributedString:outputFileURL:options:completionBlock:")]
		PSPDFAttributedStringConversionOperation GeneratePdf (NSAttributedString attributedString, NSUrl outputFileUrl, [NullAllowed] NSDictionary dicOptions, [NullAllowed] Action<NSUrl, NSError> completion);

		[Static]
		[Export ("generatePDFFromURL:serverURL:JWT:completionBlock:")]
		[return: NullAllowed]
		PSPDFOfficeConversionOperation GeneratePdf (NSUrl inputUrl, NSUrl serverUrl, string jwt, [NullAllowed] Action<NSData, NSError> completion);

		[Static]
		[Export ("generatePDFFromURL:serverURL:JWT:outputFileURL:completionBlock:")]
		[return: NullAllowed]
		PSPDFOfficeConversionOperation GeneratePdf (NSUrl inputUrl, NSUrl serverUrl, string jwt, NSUrl outputFileUrl, [NullAllowed] Action<NSUrl, NSError> completion);

		[Static]
		[Export ("cancelAllConversionOperations")]
		void CancelAllConversionOperations ();
#endif
	}

	interface IPSPDFProcessorDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFProcessorDelegate {

		[Export ("processor:didProcessPage:totalPages:")]
		void DidProcessPage (PSPDFProcessor processor, nuint currentPage, nuint totalPages);

		[Export ("processorCancelled:")]
		void ProcessorCancelled (PSPDFProcessor processor);
	}

#if __IOS__
	[Static]
	interface PSPDFProcessorGenerationOptionsKeys {

		[Field ("PSPDFProcessorAnnotationTypesKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnnotationTypesKey { get; }

		[Field ("PSPDFProcessorAnnotationDictKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnnotationDictKey { get; }

		[Field ("PSPDFProcessorAnnotationAsDictionaryKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnnotationAsDictionaryKey { get; }

		[Field ("PSPDFProcessorUserPasswordKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString UserPasswordKey { get; }

		[Field ("PSPDFProcessorOwnerPasswordKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString OwnerPasswordKey { get; }

		[Field ("PSPDFProcessorKeyLengthKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString KeyLengthKey { get; }

		[Field ("PSPDFProcessorPageRectKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString PageRectKey { get; }

		[Field ("PSPDFProcessorNumberOfPagesKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString NumberOfPagesKey { get; }

		[Field ("PSPDFProcessorPageBorderMarginKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString PageBorderMarginKey { get; }

		[Field ("PSPDFProcessorAdditionalDelayKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AdditionalDelayKey { get; }

		[Field ("PSPDFProcessorStripEmptyPagesKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString StripEmptyPagesKey { get; }

		[Field ("PSPDFProcessorDocumentTitleKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString DocumentTitleKey { get; }

		// Some other useful options
		[Field ("kCGPDFContextAllowsCopying", Constants.CoreGraphicsLibrary)]
		NSString AllowsCopyingKey { get; }

		[Field ("kCGPDFContextAllowsPrinting", Constants.CoreGraphicsLibrary)]
		NSString AllowsPrintingKey { get; }

		[Field ("kCGPDFContextKeywords", Constants.CoreGraphicsLibrary)]
		NSString KeywordsKey { get; }

		[Field ("kCGPDFContextAuthor", Constants.CoreGraphicsLibrary)]
		NSString AuthorKey { get; }

		[Field ("kCGPDFContextSubject", Constants.CoreGraphicsLibrary)]
		NSString SubjectKey { get; }
	}

	[StrongDictionary ("PSPDFProcessorGenerationOptionsKeys")]
	interface PSPDFProcessorGenerationOptions {
		NSDictionary AnnotationDict { get; set; }
		bool AnnotationAsDictionary { get; set; }
		string UserPassword { get; set; }
		string OwnerPassword { get; set; }
		CGRect PageRect { get; set; }
		int NumberOfPages { get; set; }
		double AdditionalDelay { get; set; }
		bool StripEmptyPages { get; set; }
		string DocumentTitle { get; set; }
		bool AllowsCopying { get; set; }
		bool AllowsPrinting { get; set; }
		string [] Keywords { get; set; }
		string Author { get; set; }
		string Subject { get; set; }
	}
#endif

	[BaseType (typeof (NSObject))]
	interface PSPDFProcessorConfiguration : INSCopying {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] PSPDFDocument document);

		[NullAllowed, Export ("document")]
		PSPDFDocument Document { get; }

		[Export ("pageCount")]
		nint PageCount { get; }

		[Export ("formFieldNameMappings", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSString> FormFieldNameMappings { get; set; }

		[Export ("formMappingNameMappings", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSString> FormMappingNameMappings { get; set; }

		[Export ("movePages:toDestinationIndex:")]
		void MovePages (NSIndexSet indexes, nuint destinationPageIndex);

		[Export ("removePages:")]
		void RemovePages (NSIndexSet indexes);

		[Export ("setShouldStripBlankPagesOnGenerate:")]
		void SetShouldStripBlankPagesOnGenerate (bool shouldStrip);

		[Export ("includeOnlyIndexes:")]
		void IncludeOnlyIndexes (NSIndexSet indexes);

		[Export ("rotatePage:by:")]
		void RotatePage (nuint pageIndex, nuint degrees);

		[Export ("scalePage:toSize:")]
		void ScalePage (nuint pageIndex, CGSize size);

		[Export ("scalePage:toSizeInMillimeter:")]
		void ScalePageUsingMillimeterSize (nuint pageIndex, CGSize mmSize);

		[Export ("changeCropBoxForPageAtIndex:toRect:")]
		void ChangeCropBoxForPageAtIndex (nuint pageIndex, CGRect rect);

		[Export ("changeMediaBoxForPageAtIndex:toRect:")]
		void ChangeMediaBox (nuint pageIndex, CGRect rect);

		[Export ("addNewPageAtIndex:configuration:")]
		void AddNewPage (nuint destinationPageIndex, [NullAllowed] PSPDFNewPageConfiguration newPageConfiguration);

		[Export ("modifyAnnotationsOfTypes:change:")]
		void ModifyAnnotations (PSPDFAnnotationType annotationTypes, PSPDFAnnotationChange annotationChange);

		[Export ("modifyAnnotations:change:error:")]
		bool ModifyAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationChange annotationChange, [NullAllowed] out NSError error);

		[Export ("modifyFormsOfType:change:")]
		void ModifyForms (PSPDFFormFieldType formFieldType, PSPDFAnnotationChange annotationChange);

		[Export ("mergeItem:onPage:")]
		void MergeItem (PSPDFProcessorItem item, nuint destinationPageIndex);

		[Export ("drawOnAllCurrentPages:")]
		void DrawOnAllCurrentPages (PSPDFRenderDrawHandler drawHandler);

		[Export ("changeStrokeColorOnPageAtIndex:toColor:")]
		void ChangeStrokeColorOnPage (nuint pageIndex, UIColor color);

		[Export ("mergeAutoRotatedPageFromDocument:password:sourcePageIndex:destinationPageIndex:transform:blendMode:")]
		void MergeAutoRotatedPage (PSPDFDocument sourceDocument, [NullAllowed] string password, nuint sourcePageIndex, nuint destinationPageIndex, CGAffineTransform transform, CGBlendMode blendMode);

		[Export ("applyRedactions")]
		void ApplyRedactions ();

		[Export ("applyRedactionsOnPageAtIndex:")]
		void ApplyRedactionsOnPage (nuint pageIndex);

		// PSPDFProcessorConfiguration (Metadata) Category

		[Export ("metadata")]
		NSDictionary<NSString, NSString> Metadata { get; }

		[Export ("updateMetadata:")]
		void UpdateMetadata (NSDictionary<NSString, NSString> metadata);

		[Export ("clearMetadata")]
		void ClearMetadata ();
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFProcessorItem {

		[Static]
		[Export ("processorItemWithImage:jpegCompressionQuality:builderBlock:")]
		PSPDFProcessorItem FromImage (UIImage image, nfloat jpegCompressionQuality, [NullAllowed] Action<PSPDFProcessorItemBuilder> builder);

		[Static]
		[Export ("processorItemWithItemURL:builderBlock:")]
		PSPDFProcessorItem FromItemUrl (NSUrl itemUrl, [NullAllowed] Action<PSPDFProcessorItemBuilder> builder);

		[NullAllowed, Export ("image")]
		UIImage Image { get; }

		[NullAllowed, Export ("itemURL")]
		NSUrl ItemUrl { get; }

		[Export ("transform")]
		CGAffineTransform Transform { get; }

		[Export ("jpegCompressionQuality")]
		nfloat JpegCompressionQuality { get; }

		[Export ("alignment")]
		PSPDFRectAlignment Alignment { get; }

		[Export ("shouldUseAlignment")]
		bool ShouldUseAlignment { get; }

		[Export ("zPosition")]
		PSPDFItemZPosition ZPosition { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFProcessorItemBuilder {

		[Export ("transform", ArgumentSemantic.Assign)]
		CGAffineTransform Transform { get; set; }

		[Export ("alignment", ArgumentSemantic.Assign)]
		PSPDFRectAlignment Alignment { get; set; }

		[Export ("shouldUseAlignment")]
		bool ShouldUseAlignment { get; set; }

		[Export ("zPosition", ArgumentSemantic.Assign)]
		PSPDFItemZPosition ZPosition { get; set; }
	}

	delegate void PSPDFRemoteContentObjectAuthenticationHandlerCallback (NSUrlSessionAuthChallengeDisposition disposition, NSUrlCredential credential);
	delegate void PSPDFRemoteContentObjectAuthenticationHandler (NSUrlAuthenticationChallenge challenge, [BlockCallback] PSPDFRemoteContentObjectAuthenticationHandlerCallback callback);
	delegate NSObject PSPDFRemoteContentObjectTransformerHandler (NSUrl location);

	interface IPSPDFRemoteContentObject { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFRemoteContentObject {

		[Abstract]
		[NullAllowed, Export ("URLRequestForRemoteContent")]
		NSUrlRequest UrlRequestForRemoteContent { get; }

		[Abstract]
		[NullAllowed, Export ("remoteContent", ArgumentSemantic.Strong)]
		NSObject RemoteContent { get; set; }

		[Export ("isLoadingRemoteContent")]
		bool GetLoadingRemoteContent ();

		[Export ("setLoadingRemoteContent:")]
		void SetLoadingRemoteContent (bool isLoadingRemoteContent);

		[Export ("remoteContentProgress")]
		nfloat GetRemoteContentProgress ();

		[Export ("setRemoteContentProgress:")]
		void SetRemoteContentProgress (nfloat remoteContentProgress);

		[Export ("remoteContentError")]
		NSError GetRemoteContentError ();

		[Export ("setRemoteContentError:")]
		void SetRemoteContentError ([NullAllowed] NSError remoteContentError);

		[Export ("shouldCacheRemoteContent")]
		bool ShouldCacheRemoteContent ();

		[Export ("shouldRetryLoadingRemoteContentOnConnectionFailure")]
		bool ShouldRetryLoadingRemoteContentOnConnectionFailure ();

		[Export ("remoteContentAuthenticationChallengeBlock")]
		PSPDFRemoteContentObjectAuthenticationHandler GetRemoteContentAuthenticationChallengeHandler ();

		[Export ("remoteContentTransformerBlock")]
		PSPDFRemoteContentObjectTransformerHandler GetRemoteContentTransformerHandler ();

		[Export ("hasRemoteContent")]
		bool HasRemoteContent ();

		[Export ("completionBlock")]
		Action<PSPDFRemoteContentObject> GetCompletionHandler ();

		[Export ("setCompletionBlock:")]
		void SetCompletionHandler (Action<PSPDFRemoteContentObject> completionHandler);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFRemoteFileObject : PSPDFRemoteContentObject {

		[Export ("initWithRemoteURL:targetURL:fileManager:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSUrl remoteURL, NSUrl targetFileURL, IPSPDFFileManager fileManager);

		[Export ("remoteURL", ArgumentSemantic.Copy)]
		NSUrl RemoteUrl { get; }

		[Export ("targetURL", ArgumentSemantic.Copy)]
		NSUrl TargetUrl { get; }

		// They come from PSPDFRemoteContentObject protocol no need to bind
		//[NullAllowed, Export ("remoteContent", ArgumentSemantic.Strong)]
		//new NSUrl RemoteContent { get; set; }

		//[Export ("loadingRemoteContent")]
		//bool LoadingRemoteContent { [Bind ("isLoadingRemoteContent")] get; set; }

		//[Export ("remoteContentProgress")]
		//nfloat RemoteContentProgress { get; set; }

		//[NullAllowed, Export ("remoteContentError", ArgumentSemantic.Strong)]
		//NSError RemoteContentError { get; set; }

		//[NullAllowed, Export ("completionBlock", ArgumentSemantic.Copy)]
		//Action<PSPDFRemoteContentObject> CompletionHandler { get; set; }
	}

	[BaseType (typeof (PSPDFGoToAction))]
	interface PSPDFRemoteGoToAction {

		[Export ("initWithRelativePath:pageIndex:")]
		NativeHandle Constructor ([NullAllowed] string remotePath, nuint pageIndex);

		[Export ("initWithRelativePath:namedDestination:")]
		NativeHandle Constructor ([NullAllowed] string remotePath, string namedDestination);

		[NullAllowed, Export ("relativePath")]
		string RelativePath { get; }

		[NullAllowed, Export ("namedDestination")]
		string NamedDestination { get; }
	}

	interface PSPDFRenderManagerRenderResultDidChangeNotificationEventArgs {
		[Export ("PSPDFRenderManagerRenderResultChangedDocumentKey")]
		PSPDFDocument Document { get; }

		[Export ("PSPDFRenderManagerRenderResultChangedPagesKey")]
		NSIndexSet Pages { get; }

		[Export ("PSPDFPageRendererPageInfoKey")]
		PSPDFPageInfo PageInfo { get; }
	}

	interface IPSPDFRenderManager { }

	[Protocol]
	interface PSPDFRenderManager {

		[Abstract]
		[Export ("setupGraphicsContext:rectangle:pageInfo:")]
		void SetupGraphicsContext (CGContext context, CGRect displayRectangle, PSPDFPageInfo pageInfo);

		[Abstract]
		[Export ("renderQueue")]
		PSPDFRenderQueue RenderQueue { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFRenderQueue {

		[Export ("scheduleTask:")]
		void Schedule (PSPDFRenderTask task);

		[Export ("scheduleTasks:")]
		void Schedule (PSPDFRenderTask [] tasks);

		// PSPDFRenderQueue (Debugging) Category

		[Advice ("You should not call this method in production code. This method should only be used for debugging.")]
		[Export ("cancelAllTasks")]
		void CancelAllTasks ();
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFRenderRequest : INSCopying, INSMutableCopying {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocument document);

		[Export ("document")]
		PSPDFDocument Document { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("pageIndex")]
		nuint PageIndex { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("imageSize")]
		CGSize ImageSize { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("pdfRect")]
		CGRect PdfRect { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("imageScale")]
		nfloat ImageScale { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[NullAllowed, Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("options", ArgumentSemantic.Copy)]
		PSPDFRenderOptions Options { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("userInfo", ArgumentSemantic.Copy)]
		NSDictionary UserInfo { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("cachePolicy")]
		PSPDFRenderRequestCachePolicy CachePolicy { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("isEqualRenderRequest:")]
		bool IsEqualTo (PSPDFRenderRequest renderRequest);
	}

	[BaseType (typeof (PSPDFRenderRequest))]
	interface PSPDFMutableRenderRequest {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocument document);

		[Export ("pageIndex"), Override]
		nuint PageIndex { get; set; }

		[Export ("imageSize", ArgumentSemantic.Assign), Override]
		CGSize ImageSize { get; set; }

		[Export ("pdfRect", ArgumentSemantic.Assign), Override]
		CGRect PdfRect { get; set; }

		[Export ("imageScale"), Override]
		nfloat ImageScale { get; set; }

		[NullAllowed, Export ("annotations", ArgumentSemantic.Copy), Override]
		PSPDFAnnotation [] Annotations { get; set; }

		[Export ("options", ArgumentSemantic.Copy), Override]
		PSPDFRenderOptions Options { get; set; }

		[Export ("cachePolicy", ArgumentSemantic.Assign), Override]
		PSPDFRenderRequestCachePolicy CachePolicy { get; set; }

		[Export ("userInfo", ArgumentSemantic.Copy), Override]
		NSDictionary UserInfo { get; set; }
	}

	interface IPSPDFRenderTaskDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFRenderTaskDelegate {

		[Export ("renderTaskDidFinish:")]
		void DidFinish (PSPDFRenderTask task);

		[Export ("renderTask:didFailWithError:")]
		void DidFail (PSPDFRenderTask task, NSError error);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFRenderTask {

		[Export ("initWithRequest:error:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFRenderRequest request, [NullAllowed] out NSError error);

		[Export ("request")]
		PSPDFRenderRequest Request { get; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFRenderTaskDelegate Delegate { get; set; }

		[NullAllowed, Export ("completionHandler", ArgumentSemantic.Copy)]
		Action<UIImage, NSError> CompletionHandler { get; set; }

		[Export ("priority", ArgumentSemantic.Assign)]
		PSPDFRenderQueuePriority Priority { get; set; }

		[NullAllowed, Export ("image")]
		UIImage Image { get; }

		[Export ("cancelled")]
		bool Cancelled { [Bind ("isCancelled")] get; }

		[Export ("completed")]
		bool Completed { [Bind ("isCompleted")] get; }

		[Export ("cancel")]
		void Cancel ();

		[Static]
		[Async]
		[Export ("groupTasks:completionHandler:")]
		void GroupTasks (PSPDFRenderTask [] tasks, Action completionHandler);
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFRenditionAction {

		[Export ("initWithActionType:javaScript:annotation:")]
		NativeHandle Constructor (PSPDFRenditionActionType actionType, [NullAllowed] string javaScript, [NullAllowed] PSPDFScreenAnnotation annotation);

		[Export ("actionType")]
		PSPDFRenditionActionType ActionType { get; }

		[NullAllowed, Export ("annotation", ArgumentSemantic.Weak)]
		PSPDFScreenAnnotation Annotation { get; }

		[NullAllowed, Export ("javaScript")]
		string JavaScript { get; }
	}

	[BaseType (typeof (PSPDFAbstractFormAction))]
	interface PSPDFResetFormAction {

		[Export ("initWithFlags:")]
		NativeHandle Constructor (PSPDFResetFormActionFlag flags);

		[Export ("flags")]
		PSPDFResetFormActionFlag Flags { get; }
	}

	[BaseType (typeof (PSPDFAssetAnnotation))]
	interface PSPDFRichMediaAnnotation : PSPDFOverridable {

	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFRichMediaExecuteAction {

		[Export ("initWithCommand:argument:annotation:")]
		NativeHandle Constructor ([NullAllowed] string command, [NullAllowed] NSObject argument, [NullAllowed] PSPDFRichMediaAnnotation annotation);

		[NullAllowed, Export ("command")]
		string Command { get; }

		[NullAllowed, Export ("argument", ArgumentSemantic.Copy)]
		NSObject Argument { get; }

		[NullAllowed, Export ("annotation", ArgumentSemantic.Weak)]
		PSPDFRichMediaAnnotation Annotation { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFRSAKey {

		[Export ("publicKeyScheme")]
		string PublicKeyScheme { get; }

		[Export ("keyLength")]
		nint KeyLength { get; }
	}

	[BaseType (typeof (PSPDFAssetAnnotation))]
	interface PSPDFScreenAnnotation : PSPDFOverridable {

		[Export ("mediaScreenWindowType")]
		PSPDFMediaScreenWindowType MediaScreenWindowType { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFSearchResult {

		[Export ("initWithDocumentUID:pageIndex:range:previewText:rangeInPreviewText:selection:annotation:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string documentUid, nuint pageIndex, NSRange range, string previewText, NSRange rangeInPreviewText, [NullAllowed] PSPDFTextBlock selection, [NullAllowed] PSPDFAnnotation annotation);

		[Export ("initWithDocument:pageIndex:range:previewText:rangeInPreviewText:selection:annotation:")]
		NativeHandle Constructor (PSPDFDocument document, nuint pageIndex, NSRange range, string previewText, NSRange rangeInPreviewText, [NullAllowed] PSPDFTextBlock selection, [NullAllowed] PSPDFAnnotation annotation);

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[Export ("previewText")]
		string PreviewText { get; }

		[Export ("rangeInPreviewText")]
		NSRange RangeInPreviewText { get; }

		[Export ("range")]
		NSRange Range { get; }

		[NullAllowed, Export ("selection")]
		PSPDFTextBlock Selection { get; }

		[NullAllowed, Export ("annotation", ArgumentSemantic.Weak)]
		PSPDFAnnotation Annotation { get; }

		[Export ("documentUID")]
		string DocumentUid { get; }

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }
	}

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFSignatureAppearanceBuilder {

		[Export ("appearanceMode")]
		PSPDFSignatureAppearanceMode AppearanceMode { get; set; }

		[Export ("showSignerName")]
		bool ShowSignerName { get; set; }

		[Export ("showSigningDate")]
		bool ShowSigningDate { get; set; }

		[Export ("showSignatureReason")]
		bool ShowSignatureReason { get; set; }

		[Export ("showSignatureLocation")]
		bool ShowSignatureLocation { get; set; }

		[NullAllowed, Export ("signatureGraphic")]
		PSPDFAnnotationAppearanceStream SignatureGraphic { get; set; }

		[NullAllowed, Export ("signatureWatermark")]
		PSPDFAnnotationAppearanceStream SignatureWatermark { get; set; }

		[Export ("reuseExistingAppearance")]
		bool ReuseExistingAppearance { get; set; }

		[Export ("showWatermark")]
		bool ShowWatermark { get; set; }
	}

	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFSignatureAppearance {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFSignatureAppearance DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		NativeHandle Constructor (PSPDFSignatureAppearanceBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFSignatureAppearance FromConfigurationBuilder ([NullAllowed] Action<PSPDFSignatureAppearanceBuilder> builderHandler);

		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFSignatureAppearance GetUpdatedConfiguration ([NullAllowed] Action<PSPDFSignatureAppearanceBuilder> builderHandler);

		[Export ("appearanceMode")]
		PSPDFSignatureAppearanceMode AppearanceMode { get; }

		[Export ("showSignerName")]
		bool ShowSignerName { get; }

		[Export ("showSigningDate")]
		bool ShowSigningDate { get; }

		[Export ("showSignatureReason")]
		bool ShowSignatureReason { get; }

		[Export ("showSignatureLocation")]
		bool ShowSignatureLocation { get; }

		[NullAllowed, Export ("signatureGraphic")]
		PSPDFAnnotationAppearanceStream SignatureGraphic { get; }

		[NullAllowed, Export ("signatureWatermark")]
		PSPDFAnnotationAppearanceStream SignatureWatermark { get; }

		[Export ("reuseExistingAppearance")]
		bool ReuseExistingAppearance { get; }

		[Export ("showWatermark")]
		bool ShowWatermark { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSignatureBiometricProperties : INSSecureCoding {

		[Export ("initWithPressureList:timePointsList:touchRadius:inputMethod:")]
		NativeHandle Constructor ([NullAllowed] NSNumber [] pressureList, [NullAllowed] NSNumber [] timePointsList, [NullAllowed] NSNumber touchRadius, PSPDFDrawInputMethod inputMethod);

		[NullAllowed, Export ("pressureList")]
		NSNumber [] PressureList { get; }

		[NullAllowed, Export ("timePointsList")]
		NSNumber [] TimePointsList { get; }

		[NullAllowed, Export ("touchRadius")]
		NSNumber TouchRadius { get; }

		[Export ("inputMethod")]
		PSPDFDrawInputMethod InputMethod { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSignatureContainer : INSSecureCoding, PSPDFOverridable {

		[Export ("initWithSignatureAnnotation:signer:biometricProperties:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFAnnotation signatureAnnotation, [NullAllowed] PSPDFSigner signer, [NullAllowed] PSPDFSignatureBiometricProperties biometricProperties);

		[Export ("signatureAnnotation")]
		PSPDFAnnotation SignatureAnnotation { get; }

		[NullAllowed, Export ("signer")]
		PSPDFSigner Signer { get; }

		[NullAllowed, Export ("biometricProperties")]
		PSPDFSignatureBiometricProperties BiometricProperties { get; }
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFSignatureFormElement {

		[Export ("isSigned")]
		bool IsSigned { get; }

		[NullAllowed, Export ("signatureInfo")]
		PSPDFSignatureInfo SignatureInfo { get; }

		[NullAllowed, Export ("overlappingSignatureAnnotation")]
		PSPDFAnnotation OverlappingSignatureAnnotation { get; }

		[Export ("signatureBiometricProperties:")]
		[return: NullAllowed]
		PSPDFSignatureBiometricProperties GetSignatureBiometricProperties (PSPDFPrivateKey privateKey);

		[Export ("removeSignatureWithError:")]
		bool RemoveSignature (out NSError error);
	}

	[BaseType (typeof (PSPDFFormField))]
	[DisableDefaultCtor]
	interface PSPDFSignatureFormField {

		[Static]
		[Export ("insertedSignatureFieldWithFullyQualifiedName:documentProvider:formElement:error:")]
		[return: NullAllowed]
		PSPDFSignatureFormField Create (string fullyQualifiedName, PSPDFDocumentProvider documentProvider, PSPDFFormElement formElement, [NullAllowed] out NSError error);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSignatureInfo {

		[Export ("placeholderBytes")]
		nuint PlaceholderBytes { get; }

		[NullAllowed, Export ("contents", ArgumentSemantic.Copy)]
		NSData Contents { get; }

		[NullAllowed, Export ("byteRanges", ArgumentSemantic.Copy)]
		NSIndexSet ByteRanges { get; }

		[NullAllowed, Export ("filter")]
		string Filter { get; }

		[NullAllowed, Export ("subFilter")]
		string SubFilter { get; }

		[NullAllowed, Export ("name")]
		string Name { get; }

		[NullAllowed, Export ("creationDate", ArgumentSemantic.Copy)]
		NSDate CreationDate { get; }

		[NullAllowed, Export ("location")]
		string Location { get; }

		[NullAllowed, Export ("reason")]
		string Reason { get; }

		[NullAllowed, Export ("propBuild", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuild PropBuild { get; }

		[NullAllowed, Export ("references", ArgumentSemantic.Copy)]
		PSPDFDigitalSignatureReference [] References { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFSignatureManager {

		[Export ("registeredSigners")]
		PSPDFSigner [] RegisteredSigners { get; }

		[Export ("registerSigner:")]
		void RegisterSigner (PSPDFSigner signer);

		[Export ("clearRegisteredSigners")]
		void ClearRegisteredSigners ();

		[Export ("trustedCertificates")]
		PSPDFX509 [] TrustedCertificates { get; }

		[Export ("addTrustedCertificate:")]
		void AddTrustedCertificate (PSPDFX509 x509);

		[Export ("clearTrustedCertificates")]
		void ClearTrustedCertificates ();
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSignaturePropBuild {

		[NullAllowed, Export ("filter", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuildEntry Filter { get; }

		[NullAllowed, Export ("pubSec", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuildEntry PubSec { get; }

		[NullAllowed, Export ("app", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuildEntry App { get; }

		[NullAllowed, Export ("sigQ", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuildEntry SigQ { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSignaturePropBuildEntry {

		[NullAllowed, Export ("name")]
		string Name { get; }

		[NullAllowed, Export ("date")]
		string Date { get; }

		[Export ("revisionNumber")]
		nint RevisionNumber { get; }

		[NullAllowed, Export ("operatingSystem")]
		string OperatingSystem { get; }

		[Export ("preRelease")]
		bool PreRelease { get; }

		[Export ("nonEmbeddedFontNoWarning")]
		bool NonEmbeddedFontNoWarning { get; }

		[Export ("trustedMode")]
		bool TrustedMode { get; }

		[Export ("minimumVersion")]
		nint MinimumVersion { get; }

		[NullAllowed, Export ("textRevision")]
		string TextRevision { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSignatureStatus {

		[Export ("initWithSigner:signingDate:wasModified:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] string signer, [NullAllowed] NSDate date, bool wasModified);

		[NullAllowed, Export ("signer")]
		string Signer { get; }

		[NullAllowed, Export ("signingDate", ArgumentSemantic.Copy)]
		NSDate SigningDate { get; }

		[Export ("coversEntireDocument")]
		bool CoversEntireDocument { get; }

		[Export ("isValid")]
		bool IsValid { get; }

		[Export ("problems")]
		string [] Problems { get; }

		[Export ("severity", ArgumentSemantic.Assign)]
		PSPDFSignatureStatusSeverity Severity { get; set; }

		[Export ("signatureIntegrityStatus", ArgumentSemantic.Assign)]
		PSPDFSignatureIntegrityStatus SignatureIntegrityStatus { get; }

		[Export ("summary")]
		string Summary { get; }

		[Export ("signatureType")]
		PSPDFSignatureType SignatureType { get; }

		[Export ("padesSignatureLevel")]
		PSPDFPadesSignatureLevel PadesSignatureLevel { get; }

		[Export ("validFrom")]
		[NullAllowed]
		NSDate ValidFrom { get; }

		[Export ("validUntil")]
		[NullAllowed]
		NSDate ValidUntil { get; }

		[Export ("serialNumber")]
		[NullAllowed]
		NSData SerialNumber { get; }

		[Export ("subjectDistinguishedName")]
		[NullAllowed]
		string SubjectDistinguishedName { get; }

		[Export ("issuerCommonName")]
		[NullAllowed]
		string IssuerCommonName { get; }

		[Export ("publicKey")]
		[NullAllowed]
		PSPDFRSAKey PublicKey { get; }

		[Export ("hashAlgorithm")]
		PSPDFSignatureHashAlgorithm HashAlgorithm { get; }

		[Export ("encryptionAlgorithm")]
		PSPDFSignatureEncryptionAlgorithm EncryptionAlgorithm { get; }

		[Export ("isSelfSigned")]
		bool IsSelfSigned { get; }

		[Export ("isLongTermValidationEnabled")]
		bool IsLongTermValidationEnabled { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSignatureValidator {

		[Export ("initWithSignatureFormElement:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFSignatureFormElement formElement);

		[Export ("signatureFormElement")]
		PSPDFSignatureFormElement SignatureFormElement { get; }

		[Export ("verifySignatureWithTrustedCertificates:error:")]
		[return: NullAllowed]
		PSPDFSignatureStatus VerifySignature ([NullAllowed] PSPDFX509 [] trustedCertificates, [NullAllowed] out NSError error);
	}

	delegate void PSPDFSignerSignFormElementHandler (bool success, PSPDFDocument document, [NullAllowed] NSError error);
	delegate void PSPDFSignerSignFormElementSinkHandler (bool success, IPSPDFDataSink document, [NullAllowed] NSError error);
	delegate void PSPDFSignatureCreationHandler (bool success, [NullAllowed] IPSPDFDataSink document, [NullAllowed] NSError error);

	[BaseType (typeof (NSObject))]
	interface PSPDFSigner : PSPDFDocumentSignerDelegate, PSPDFDocumentSignerDataSource, PSPDFExternalSignature, INSSecureCoding {

		[Export ("filter")]
		string Filter { get; }

		[Export ("signatureType", ArgumentSemantic.Assign)]
		PSPDFSignatureType SignatureType { get; set; }

		[Export ("displayName")]
		string DisplayName { get; }

		[NullAllowed, Export ("signersName")]
		string SignersName { get; set; }

		[Export ("reason")]
		string Reason { get; }

		[Export ("location")]
		string Location { get; }

		[NullAllowed, Export ("privateKey", ArgumentSemantic.Assign)]
		PSPDFPrivateKey PrivateKey { get; set; }

		[NullAllowed, Export ("dataSource", ArgumentSemantic.Weak)]
		IPSPDFDocumentSignerDataSource DataSource { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFDocumentSignerDelegate Delegate { get; set; }

#if __IOS__
		[Async]
		[Export ("requestSigningCertificate:completionBlock:")]
		void RequestSigningCertificate (NSObject sourceController, [NullAllowed] Action<PSPDFX509, NSError> completion);
#endif

		[Export ("prepareFormElement:toBeSignedWithAppearance:contents:writingToDataSink:completion:")]
		void PrepareFormElement (PSPDFSignatureFormElement element, PSPDFSignatureAppearance signatureAppearance, IPSPDFSignatureContents contents, IPSPDFDataSink dataSink, PSPDFSignatureCreationHandler completionBlock);

		[Export ("embedSignatureInFormElement:withContents:writingToDataSink:completion:")]
		void EmbedSignature (PSPDFSignatureFormElement element, IPSPDFSignatureContents contents, IPSPDFDataSink dataSink, PSPDFSignatureCreationHandler completionBlock);

		[Export ("signData:privateKey:hashAlgorithm:")]
		NSData SignData (NSData data, PSPDFPrivateKey privateKey, PSPDFSignatureHashAlgorithm hashAlgorithm);

		[Async (ResultTypeName = "PSPDFSignerSignFormElementSinkResult")]
		[Export ("signFormElement:withCertificate:writeToDataSink:completionBlock:")]
		[Advice ("Requires base call if override.")]
		void SignFormElement (PSPDFSignatureFormElement element, PSPDFX509 certificate, IPSPDFDataSink dataSink, [NullAllowed] PSPDFSignerSignFormElementSinkHandler completion);

		[Async (ResultTypeName = "PSPDFSignerSignFormElementCertSinkResult")]
		[Export ("signFormElement:withCertificateChain:writeToDataSink:completionBlock:")]
		[Advice ("Requires base call if override.")]
		void SignFormElement (PSPDFSignatureFormElement element, PSPDFX509 [] certificateChain, IPSPDFDataSink dataSink, [NullAllowed] PSPDFSignerSignFormElementSinkHandler completion);

		[Async (ResultTypeName = "PSPDFSignerSignFormElementResult")]
		[Export ("signFormElement:withCertificate:writeTo:completionBlock:")]
		[Advice ("Requires base call if override.")]
		void SignFormElement (PSPDFSignatureFormElement element, PSPDFX509 certificate, string path, [NullAllowed] PSPDFSignerSignFormElementHandler completion);

		[Async (ResultTypeName = "PSPDFSignerSignFormElementCertResult")]
		[Export ("signFormElement:withCertificateChain:writeTo:completionBlock:")]
		[Advice ("Requires base call if override.")]
		void SignFormElement (PSPDFSignatureFormElement element, PSPDFX509 [] certificateChain, string path, [NullAllowed] PSPDFSignerSignFormElementHandler completion);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFSoundAnnotation : PSPDFOverridable {

		[Export ("controller")]
		PSPDFSoundAnnotationController Controller { get; }

		[NullAllowed, Export ("iconName")]
		string IconName { get; set; }

		[Static]
		[Export ("recordingAnnotationAvailable")]
		bool RecordingAnnotationAvailable { get; }

		[Export ("canRecord")]
		bool CanRecord { get; }

		[Export ("bits")]
		nuint Bits { get; }

		[Export ("rate")]
		nuint Rate { get; }

		[Export ("channels")]
		nuint Channels { get; }

		[NullAllowed, Export ("encoding")]
		string Encoding { get; }

		[NullAllowed, Export ("soundURL", ArgumentSemantic.Copy)]
		NSUrl SoundUrl { get; }

		[Export ("loadAttributesFromAudioFile:")]
		bool LoadAttributesFromAudioFile ([NullAllowed] out NSError error);

		[NullAllowed, Export ("soundData")]
		NSData SoundData { get; }

		[Export ("initWithURL:error:")]
		NativeHandle Constructor (NSUrl soundUrl, [NullAllowed] out NSError error);

		[Export ("initWithRecorderOptions:")]
		NativeHandle Constructor ([NullAllowed] NSDictionary recorderOptions);

		[Static]
		[Export ("audioFormatIDFromEncoding:")]
		uint GetAudioFormatId ([NullAllowed] string fromEncoding);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSoundAnnotationController {

		[Field ("PSPDFSoundAnnotationChangedStateNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString ChangedStateNotification { get; }

		[Field ("PSPDFSoundAnnotationStopAllNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString StopAllNotification { get; }

		[Static]
		[Export ("stopRecordingOrPlaybackForAllExcept:")]
		void StopRecordingOrPlaybackForAllExcept ([NullAllowed] NSObject except);

		[Async]
		[Static]
		[Export ("requestRecordPermission:")]
		void RequestRecordPermission ([NullAllowed] Action<bool> handler);

		[Export ("initWithSoundAnnotation:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFSoundAnnotation annotation);

		[NullAllowed, Export ("annotation", ArgumentSemantic.Weak)]
		PSPDFSoundAnnotation Annotation { get; }

		[Export ("state")]
		PSPDFSoundAnnotationState State { get; }

		[Export ("playbackDuration")]
		double PlaybackDuration { get; }

		[Export ("startPlayback:")]
		bool StartPlayback ([NullAllowed] out NSError error);

		[Export ("pause")]
		void Pause ();

		[Export ("stop:")]
		bool Stop ([NullAllowed] out NSError error);

#if !__WATCH__

		[NullAllowed, Export ("audioPlayer")]
		AVAudioPlayer AudioPlayer { get; }
#endif

		[Export ("startRecording:")]
		bool StartRecording ([NullAllowed] out NSError error);

		[Export ("discardRecording")]
		void DiscardRecording ();
	}

	[BaseType (typeof (PSPDFAbstractShapeAnnotation))]
	interface PSPDFSquareAnnotation : PSPDFOverridable {

		[Export ("bezierPath")]
		UIBezierPath BezierPath { get; }
	}

	[BaseType (typeof (PSPDFTextMarkupAnnotation))]
	interface PSPDFSquigglyAnnotation : PSPDFOverridable {

		[Static]
		[Export ("textOverlayAnnotationWithGlyphs:")]
		[return: NullAllowed]
		PSPDFSquigglyAnnotation FromGlyphs ([NullAllowed] PSPDFGlyph [] glyphs);

		[Static]
		[Export ("textOverlayAnnotationWithRects:boundingBox:pageIndex:")]
		[return: NullAllowed]
		PSPDFSquigglyAnnotation FromRects ([BindAs (typeof (CGRect[]))] NSValue[] rects, CGRect boundingBox, nuint pageIndex);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFStampAnnotation : PSPDFRotatable, PSPDFOverridable {

		[Static]
		[Export ("colorForStampType:")]
		UIColor GetStampColor ([NullAllowed] [BindAs (typeof (PSPDFStampType?))] NSString stampType);

		[Export ("initWithStampType:")]
		NativeHandle Constructor ([NullAllowed] [BindAs (typeof (PSPDFStampType?))] NSString stampType);

		[Export ("initWithTitle:")]
		NativeHandle Constructor ([NullAllowed] string title);

		[Export ("initWithImage:")]
		NativeHandle Constructor ([NullAllowed] UIImage image);

		[BindAs (typeof (PSPDFStampType?))]
		[NullAllowed, Export ("stampType")]
		NSString StampType { get; set; }

		[NullAllowed, Export ("title")]
		string Title { get; set; }

		[NullAllowed, Export ("subtitle")]
		string Subtitle { get; set; }

		[Export ("rotation")]
		nuint Rotation { get; }

		[NullAllowed, Export ("image", ArgumentSemantic.Strong)]
		UIImage Image { get; set; }

		[Export ("loadImageWithTransform:error:")]
		[return: NullAllowed]
		UIImage LoadImage ([NullAllowed] CGAffineTransform transform, [NullAllowed] out NSError error);

		[Export ("imageTransform", ArgumentSemantic.Assign)]
		CGAffineTransform ImageTransform { get; set; }

		[Export ("isSignature")]
		bool IsSignature { get; set; }

		[Export ("sizeThatFits:")]
		CGSize GetSizeThatFits (CGSize size);

		[Export ("sizeToFit")]
		void SizeToFit ();
	}

	[BaseType (typeof (PSPDFTextMarkupAnnotation))]
	interface PSPDFStrikeOutAnnotation : PSPDFOverridable {

		[Static]
		[Export ("textOverlayAnnotationWithGlyphs:")]
		[return: NullAllowed]
		PSPDFStrikeOutAnnotation FromGlyphs ([NullAllowed] PSPDFGlyph[] glyphs);

		[Static]
		[Export ("textOverlayAnnotationWithRects:boundingBox:pageIndex:")]
		[return: NullAllowed]
		PSPDFStrikeOutAnnotation FromRects ([BindAs (typeof (CGRect[]))] NSValue[] rects, CGRect boundingBox, nuint pageIndex);
	}

	[BaseType (typeof (PSPDFAbstractFormAction))]
	interface PSPDFSubmitFormAction {

		[Export ("initWithURL:flags:")]
		NativeHandle Constructor ([NullAllowed] NSUrl url, PSPDFSubmitFormActionFlag flags);

		[NullAllowed, Export ("URL", ArgumentSemantic.Copy)]
		NSUrl Url { get; }

		[Export ("flags")]
		PSPDFSubmitFormActionFlag Flags { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFTextBlock : INSCopying, INSSecureCoding {

		[Export ("initWithGlyphs:frame:")]
		NativeHandle Constructor (PSPDFGlyph[] glyphs, CGRect frame);

		[Export ("initWithRange:text:frame:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSRange textRange, string text, CGRect frame);

		[Export ("frame")]
		CGRect Frame { get; }

		[Export ("range")]
		NSRange Range { get; }

		[Export ("words", ArgumentSemantic.Copy)]
		PSPDFWord [] Words { get; }

		[Export ("content")]
		string Content { get; }

		[Export ("isEqualToTextBlock:")]
		bool IsEqualTo (PSPDFTextBlock otherBlock);
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFTextFieldFormElement {

		[Export ("multiline")]
		bool Multiline { [Bind ("isMultiline")] get; }

		[Export ("password")]
		bool Password { [Bind ("isPassword")] get; }

		[NullAllowed, Export ("formattedContents")]
		string FormattedContents { get; }

		[Export ("inputFormat")]
		PSPDFTextInputFormat InputFormat { get; }

		[NullAllowed, Export ("textFormField")]
		PSPDFTextFormField TextFormField { get; }
	}

	[BaseType (typeof (PSPDFFormField))]
	[DisableDefaultCtor]
	interface PSPDFTextFormField {

		[Static]
		[Export ("insertedTextFieldWithFullyQualifiedName:documentProvider:formElement:error:")]
		[return: NullAllowed]
		PSPDFTextFormField Create (string fullyQualifiedName, PSPDFDocumentProvider documentProvider, PSPDFFormElement formElement, [NullAllowed] out NSError error);

		[Export ("isMultiLine")]
		bool IsMultiLine { get; set; }

		[Export ("isPassword")]
		bool IsPassword { get; set; }

		[Export ("isComb")]
		bool IsComb { get; set; }

		[Export ("doNotScroll")]
		bool DoNotScroll { get; set; }

		[Export ("isRichText")]
		bool IsRichText { get; set; }

		[Export ("doNotSpellCheck")]
		bool DoNotSpellCheck { get; set; }

		[Export ("fileSelect")]
		bool FileSelect { get; set; }

		[NullAllowed, Export ("text")]
		string Text { get; set; }

		[NullAllowed, Export ("richText")]
		string RichText { get; set; }

		[Export ("maxLength")]
		nuint MaxLength { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFTextParser : PSPDFOverridable {

		[Export ("text")]
		string Text { get; }

		[Export ("glyphs", ArgumentSemantic.Copy)]
		PSPDFGlyph [] Glyphs { get; }

		[Export ("words", ArgumentSemantic.Copy)]
		PSPDFWord [] Words { get; }

		[Export ("textBlocks", ArgumentSemantic.Copy)]
		PSPDFTextBlock [] TextBlocks { get; }

		[Export ("images", ArgumentSemantic.Copy)]
		PSPDFImageInfo [] Images { get; }

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[Export ("textWithGlyphs:")]
		string GetText (PSPDFGlyph [] glyphs);

		[Export ("glyphsInRange:")]
		PSPDFGlyph [] GetGlyphs (NSRange range);
	}

	interface IPSPDFTextSearchDelegate { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFTextSearchDelegate {

		[Export ("willStartSearch:term:isFullSearch:")]
		void WillStartSearch (PSPDFTextSearch textSearch, string searchTerm, bool isFullSearch);

		[Export ("didUpdateSearch:term:newSearchResults:pageIndex:")]
		void DidUpdateSearch (PSPDFTextSearch textSearch, string searchTerm, PSPDFSearchResult [] searchResults, nuint pageIndex);

		[Export ("didFailSearch:withError:")]
		void DidFailSearch (PSPDFTextSearch textSearch, NSError error);

		[Export ("didFinishSearch:term:searchResults:isFullSearch:pageTextFound:")]
		void DidFinishSearch (PSPDFTextSearch textSearch, string searchTerm, PSPDFSearchResult [] searchResults, bool isFullSearch, bool pageTextFound);

		[Export ("didCancelSearch:term:isFullSearch:")]
		void DidCancelSearch (PSPDFTextSearch textSearch, string searchTerm, bool isFullSearch);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFTextSearch : INSCopying, PSPDFOverridable {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocument document);

		[Export ("searchForString:")]
		void Search (string searchTerm);

		[Export ("searchForString:inRanges:rangesOnly:cancelOperations:")]
		void Search (string searchTerm, [NullAllowed] NSIndexSet ranges, bool rangesOnly, bool cancelOperations);

		[Export ("cancelAllOperations")]
		void CancelAllOperations ();

		[Export ("cancelAllOperationsAndWait")]
		void CancelAllOperationsAndWait ();

		[Export ("comparisonOptions")]
		PSPDFTextComparisonOptions ComparisonOptions { get; set; }

		[Export ("previewRange", ArgumentSemantic.Assign)]
		NSRange PreviewRange { get; set; }

		[Export ("searchableAnnotationTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType SearchableAnnotationTypes { get; set; }

		[Export ("maximumNumberOfSearchResults")]
		nuint MaximumNumberOfSearchResults { get; set; }

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFTextSearchDelegate Delegate { get; set; }

		// PSPDFTextSearch (SubclassingHooks) Category

		[Export ("searchQueue")]
		NSOperationQueue SearchQueue { get; }
	}

	[BaseType (typeof (PSPDFTextMarkupAnnotation))]
	interface PSPDFUnderlineAnnotation : PSPDFOverridable {

		[Static]
		[Export ("textOverlayAnnotationWithGlyphs:")]
		[return: NullAllowed]
		PSPDFUnderlineAnnotation FromGlyphs ([NullAllowed] PSPDFGlyph[] glyphs);

		[Static]
		[Export ("textOverlayAnnotationWithRects:boundingBox:pageIndex:")]
		[return: NullAllowed]
		PSPDFUnderlineAnnotation FromRects ([BindAs (typeof (CGRect[]))] NSValue[] rects, CGRect boundingBox, nuint pageIndex);
	}

	interface IPSPDFUndoController { }

	[Protocol]
	interface PSPDFUndoController {

		[Abstract]
		[Export ("undoManager", ArgumentSemantic.Strong)]
		NSUndoManager UndoManager { get; }

		[Abstract]
		[Export ("recordCommandNamed:inScope:")]
		void RecordCommandNamed ([NullAllowed] string name, Action<IPSPDFUndoRecorder> scope);

		[Abstract]
		[Export ("recordCommandNamed:changingAnnotations:inScope:")]
		void RecordCommandNamedChangingAnnotations ([NullAllowed] string name, PSPDFAnnotation[] annotations, Action scope);

		[Abstract]
		[Export ("recordCommandNamed:addingAnnotations:inScope:")]
		void RecordCommandNamedAddingAnnotations ([NullAllowed] string name, PSPDFAnnotation[] annotations, Action scope);

		[Abstract]
		[Export ("recordCommandNamed:removingAnnotations:inScope:")]
		void RecordCommandNamedRemovingAnnotations ([NullAllowed] string name, PSPDFAnnotation[] annotations, Action scope);

		[Abstract]
		[Export ("beginRecordingCommandNamed:")]
		IPSPDFDetachedUndoRecorder BeginRecordingCommandNamed ([NullAllowed] string name);

		[Abstract]
		[Export ("beginRecordingCommandNamed:changingAnnotations:")]
		IPSPDFPendingUndoRecorder BeginRecordingCommandNamed ([NullAllowed] string name, PSPDFAnnotation[] annotations);
	}

	interface IPSPDFPendingUndoRecorder { }

	[Protocol]
	interface PSPDFPendingUndoRecorder {

		[Abstract]
		[Export ("commit")]
		void Commit ();
	}
	
	interface IPSPDFUndoRecorder { }

	[Protocol]
	interface PSPDFUndoRecorder {

		[Abstract]
		[Export ("recordChangingAnnotations:inScope:")]
		void RecordChangingAnnotations (PSPDFAnnotation[] annotations, Action scope);

		[Abstract]
		[Export ("recordAddingAnnotations:inScope:")]
		void RecordAddingAnnotations (PSPDFAnnotation[] annotations, Action scope);

		[Abstract]
		[Export ("recordRemovingAnnotations:inScope:")]
		void RecordRemovingAnnotations (PSPDFAnnotation[] annotations, Action scope);
	}

	interface IPSPDFDetachedUndoRecorder { }

	[Protocol]
	interface PSPDFDetachedUndoRecorder : PSPDFPendingUndoRecorder, PSPDFUndoRecorder {

		[Abstract]
		[Export ("beginRecordingChangingAnnotations:")]
		IPSPDFPendingUndoRecorder BeginRecordingChangingAnnotations (PSPDFAnnotation[] annotations);
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFURLAction {

		[Export ("initWithURLString:")]
		NativeHandle Constructor (string urlString);

		[Export ("initWithURL:options:")]
		NativeHandle Constructor ([NullAllowed] NSUrl url, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[NullAllowed, Export ("URL", ArgumentSemantic.Copy)]
		NSUrl Url { get; }

		[NullAllowed, Export ("unmodifiedURL", ArgumentSemantic.Copy)]
		NSUrl UnmodifiedUrl { get; }

		[NullAllowed, Export ("invalidURLString", ArgumentSemantic.Copy)]
		string InvalidUrlString { get; }

		[Export ("updateURLWithAnnotationManager:")]
		bool UpdateUrl (PSPDFAnnotationManager annotationManager);

		[Export ("isPSPDFPrefixed")]
		bool IsPSPDFPrefixed { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; set; }

		[Export ("modal")]
		bool Modal { [Bind ("isModal")] get; set; }

		[Export ("popover")]
		bool Popover { [Bind ("isPopover")] get; set; }

		[Export ("button")]
		bool Button { [Bind ("isButton")] get; set; }

		[Export ("size", ArgumentSemantic.Assign)]
		CGSize Size { get; set; }

		[Export ("offset")]
		nfloat Offset { get; set; }

		[Export ("prefixedURLStringWithAnnotationManager:")]
		[return: NullAllowed]
		string GetPrefixedUrlString (PSPDFAnnotationManager annotationManager);

		[Export ("emailURL")]
		bool EmailUrl { [Bind ("isEmailURL")] get; }

		[Export ("localPDFURL")]
		bool LocalPdfUrl { [Bind ("isLocalPDFURL")] get; }

#if __IOS__
		[Export ("configureMailComposeViewController:")]
		bool ConfigureMailComposeViewController (MFMailComposeViewController mailComposeViewController);
#endif
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFWidgetAnnotation : PSPDFOverridable {

		[NullAllowed, Export ("action", ArgumentSemantic.Strong)]
		PSPDFAction Action { get; set; }

		[NullAllowed, Export ("borderColor", ArgumentSemantic.Strong), New]
		UIColor BorderColor { get; set; }

		[Export ("widgetRotation")]
		nint WidgetRotation { get; set; }

		[NullAllowed, Export ("appearanceCharacteristics", ArgumentSemantic.Strong)]
		PSPDFAppearanceCharacteristics AppearanceCharacteristics { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFWord : INSCopying, INSSecureCoding {

		[Export ("initWithGlyphs:frame:")]
		NativeHandle Constructor (PSPDFGlyph [] wordGlyphs, CGRect frame);

		[Export ("initWithRange:text:frame:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSRange textRange, string text, CGRect frame);

		[Export ("stringValue")]
		string StringValue { get; }

		[Export ("frame", ArgumentSemantic.Assign)]
		CGRect Frame { get; set; }

		[Export ("range")]
		NSRange Range { get; }

		[Export ("lineBreaker")]
		bool LineBreaker { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFX509 {

		[Static]
		[Export ("adobeCA")]
		PSPDFX509 AdobeCA ();

		[Static]
		[Export ("certificatesFromPKCS7Data:error:")]
		[return: NullAllowed]
		PSPDFX509 [] GetCertificates (NSData pkcs7Data, [NullAllowed] out NSError error);

		[Export ("publicKey")]
		PSPDFRSAKey PublicKey { get; }

		[NullAllowed, Export ("commonName")]
		string CommonName { get; }
	}

	[BaseType (typeof (PSPDFContainerAnnotationProvider))]
	interface PSPDFXFDFAnnotationProvider {

		[Export ("initWithDocumentProvider:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("initWithDocumentProvider:fileURL:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocumentProvider documentProvider, NSUrl xfdfFileUrl);

		[Export ("initWithDocumentProvider:dataProvider:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocumentProvider documentProvider, IPSPDFDataProviding dataProvider);

		[NullAllowed, Export ("fileURL", ArgumentSemantic.Copy)]
		NSUrl FileUrl { get; }

		[NullAllowed, Export ("dataProvider")]
		IPSPDFDataProviding DataProvider { get; }

		[Export ("loadAllAnnotations")]
		void LoadAllAnnotations ();

		[Export ("ignorePageRotation")]
		bool IgnorePageRotation { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFXFDFParser {

		[Export ("initWithDataProvider:documentProvider:")]
		[DesignatedInitializer]
		NativeHandle Constructor (IPSPDFDataProviding dataProvider, PSPDFDocumentProvider documentProvider);

		[Export ("parseWithError:")]
		[return: NullAllowed]
		PSPDFAnnotation [] Parse ([NullAllowed] out NSError error);

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; }

		[Export ("parsingEnded")]
		bool ParsingEnded { get; }

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("dataProvider")]
		IPSPDFDataProviding DataProvider { get; }

		[Export ("ignorePageRotation")]
		bool IgnorePageRotation { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFXFDFWriter {

		[Export ("writeAnnotations:toDataSink:documentProvider:error:")]
		bool WriteAnnotations (PSPDFAnnotation [] annotations, IPSPDFDataSink dataSink, PSPDFDocumentProvider documentProvider, [NullAllowed] out NSError error);

		[Export ("ignorePageRotation")]
		bool IgnorePageRotation { get; set; }
	}

	[BaseType (typeof (NSFormatter))]
	interface PSPDFPageLabelFormatter {

		[Static]
		[Export ("localizedStringFromPageRange:document:")]
		string GetLocalizedString (NSRange pageRange, PSPDFDocument document);

		[NullAllowed, Export ("document")]
		PSPDFDocument Document { get; set; }

		[Export ("stringFromRange:")]
		string GetString (NSRange pageRange);
	}

	interface IPSPDFDocumentFeaturesSource { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentFeaturesSource {

		[Abstract]
		[NullAllowed, Export ("features", ArgumentSemantic.Weak)]
		PSPDFDocumentFeatures Features { get; set; }

		[Export ("canModify")]
		bool GetCanModify ();

		[Export ("canEditBookmarks")]
		bool GetCanEditBookmarks ();

		[Export ("canPrint")]
		bool GetCanPrint ();

		[Export ("canShowAnnotationReplies")]
		bool GetCanShowAnnotationReplies ();

		[Export ("canShowAnnotationReviews")]
		bool GetCanShowAnnotationReviews ();

		[Export ("canUseDocumentEditor")]
		bool GetCanUseDocumentEditor ();

		[Export ("canFillForms")]
		bool GetCanFillForms ();

		[Export ("canEditAnnotations")]
		bool GetCanEditAnnotations ();

		[Export ("canExtractTextAndImages")]
		bool GetCanExtractTextAndImages ();
	}

	interface IPSPDFDocumentFeaturesObserver { }

	[Protocol]
	interface PSPDFDocumentFeaturesObserver {

		[Abstract]
		[Export ("bindToObjectLifetime:")]
		void BindToObjectLifetime (NSObject @object);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDocumentFeatures : PSPDFDocumentFeaturesSource {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFDocument document);

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("addSources:")]
		void AddSources (IPSPDFDocumentFeaturesSource [] sources);

		[Export ("removeSources:")]
		void RemoveSources (IPSPDFDocumentFeaturesSource [] sources);

		[Export ("updateFeatures")]
		void UpdateFeatures ();

		[Export ("addObserverForFeature:updateHandler:")]
		IPSPDFDocumentFeaturesObserver AddObserver (Selector feature, Action<bool> handler);

		[Export ("removeObserver:")]
		void RemoveObserver (IPSPDFDocumentFeaturesObserver observer);

		[Export ("traceFeature:")]
		void TraceFeature (Selector selector);
	}

	interface IPSPDFExternalSignature { }

	[Protocol]
	interface PSPDFExternalSignature {

		[Abstract]
		[Export ("signData:hashAlgorithm:")]
		NSData SignData (NSData data, PSPDFSignatureHashAlgorithm hashAlgorithm);

		[Abstract]
		[Export ("encryptionAlgorithm")]
		PSPDFSignatureEncryptionAlgorithm EncryptionAlgorithm { get; }
	}

	delegate string PSPDFAESCryptoPassphraseProvider ();

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFAESCryptoDataSink : PSPDFDataSink {

		[Export ("initWithUID:passphraseProvider:options:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string uid, PSPDFAESCryptoPassphraseProvider passphraseProvider, PSPDFDataSinkOptions options);

		[Export ("initWithURL:passphraseProvider:options:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSUrl fileUrl, PSPDFAESCryptoPassphraseProvider passphraseProvider, PSPDFDataSinkOptions options);
	}

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFDocumentEditorConfigurationBuilder {

		[Export ("pageTemplates", ArgumentSemantic.Copy)]
		PSPDFPageTemplate [] PageTemplates { get; set; }

		[NullAllowed, Export ("currentDocumentPageSize", ArgumentSemantic.Strong)]
		PSPDFPageSize CurrentDocumentPageSize { get; set; }

		[Export ("pageSizes", ArgumentSemantic.Strong)]
		PSPDFPageSize [] PageSizes { get; set; }

		[NullAllowed, Export ("currentDocumentDirectory", ArgumentSemantic.Strong)]
		PSPDFDirectory CurrentDocumentDirectory { get; set; }

		[Export ("saveDirectories", ArgumentSemantic.Strong)]
		PSPDFDirectory [] SaveDirectories { get; set; }

		[Export ("compressions", ArgumentSemantic.Strong)]
		PSPDFCompression [] Compressions { get; set; }

		[NullAllowed, Export ("selectedTemplate", ArgumentSemantic.Strong)]
		PSPDFPageTemplate SelectedTemplate { get; set; }

		[NullAllowed, Export ("selectedPageSize", ArgumentSemantic.Strong)]
		PSPDFPageSize SelectedPageSize { get; set; }

		[Export ("selectedOrientation", ArgumentSemantic.Assign)]
		PSPDFDocumentOrientation SelectedOrientation { get; set; }

		[NullAllowed, Export ("selectedColor", ArgumentSemantic.Strong)]
		UIColor SelectedColor { get; set; }

		[NullAllowed, Export ("selectedImage", ArgumentSemantic.Strong)]
		UIImage SelectedImage { get; set; }

		[NullAllowed, Export ("selectedImagePageSize", ArgumentSemantic.Strong)]
		PSPDFPageSize SelectedImagePageSize { get; set; }

		[NullAllowed, Export ("selectedCompression", ArgumentSemantic.Strong)]
		PSPDFCompression SelectedCompression { get; set; }

		[NullAllowed, Export ("selectedSaveDirectory", ArgumentSemantic.Strong)]
		PSPDFDirectory SelectedSaveDirectory { get; set; }

		[Export ("userFacingCompressionEnabled")]
		bool UserFacingCompressionEnabled { get; set; }

		[Export ("allowExternalFileSource")]
		bool AllowExternalFileSource { get; set; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFPageTemplate {
		
		[Internal]
		[Export ("initWithDocument:sourcePageIndex:")]
		IntPtr InitWithDocument (PSPDFDocument document, nuint sourcePageIndex);

		[Internal]
		[Export ("initWithTiledPatternFromDocument:sourcePageIndex:")]
		IntPtr InitWithTiledPattern (PSPDFDocument document, nuint sourcePageIndex);

		[Export ("initWithPageType:identifier:")]
		NativeHandle Constructor (PSPDFNewPageType pageType, [BindAs (typeof (PSPDFTemplateIdentifier))] [NullAllowed] NSString identifier);

		[Async]
		[Export ("renderThumbnailWithSize:completion:")]
		void RenderThumbnail (CGSize size, Action<UIImage, NSError> completion);

		[Export ("pageType")]
		PSPDFNewPageType PageType { get; }

		[BindAs (typeof (PSPDFTemplateIdentifier))]
		[NullAllowed, Export ("identifier")]
		NSString Identifier { get; }

		[NullAllowed, Export ("templateName")]
		string TemplateName { get; set; }

		[Export ("localizedName")]
		string LocalizedName { get; }

		[NullAllowed, Export ("resourceURL")]
		NSUrl ResourceUrl { get; }

		[NullAllowed, Export ("sourceDocument")]
		PSPDFDocument SourceDocument { get; }

		[Export ("sourcePageIndex")]
		nuint SourcePageIndex { get; }

		// PSPDFPageTemplate (Convenience)

		[Static]
		[Export ("defaultTemplates")]
		PSPDFPageTemplate [] DefaultTemplates { get; }

		[Static]
		[Export ("blankTemplate")]
		PSPDFPageTemplate BlankTemplate { get; }

		[Static]
		[Export ("imagePickerTemplate")]
		PSPDFPageTemplate ImagePickerTemplate { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFRetainExistingAppearanceStreamGenerator : PSPDFAppearanceStreamGenerating {

	}

	[Protocol]
	interface PSPDFRotatable {

		[Abstract]
		[Export ("rotation")]
		nuint Rotation { get; }

		[Abstract]
		[Export ("setRotation:updateBoundingBox:")]
		void UpdateBoundingBox (nuint rotation, bool shouldUpdateBoundingBoxToMaintainContentSize);
	}

	interface IPSPDFDocumentSignerDataSource { }

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentSignerDataSource {

		[Export ("documentSigner:signatureAppearance:")]
		PSPDFSignatureAppearance GetSignatureAppearance (PSPDFSigner signer, string formFieldFqn);

		[Export ("documentSigner:signatureEstimatedSize:")]
		int GetSignatureEstimatedSize (PSPDFSigner signer, string formFieldFqn);

		[Export ("documentSigner:signatureBiometricProperties:")]
		PSPDFSignatureBiometricProperties GetSignatureBiometricProperties (PSPDFSigner signer, string formFieldFqn);

		[Export ("documentSigner:signatureHashAlgorithm:")]
		PSPDFSignatureHashAlgorithm GetSignatureHashAlgorithm (PSPDFSigner signer, string formFieldFqn);

		[Export ("documentSigner:signatureEncryptionAlgorithm:")]
		PSPDFSignatureEncryptionAlgorithm GetSignatureEncryptionAlgorithm (PSPDFSigner signer, string formFieldFqn);
	}

	interface IPSPDFDocumentSignerDelegate { }
	delegate void PSPDFDocumentSignDataCompletionHandler (bool status, [NullAllowed] NSData signedData);

#if NET
	[Protocol, Model]
#else
	[Protocol, Model (AutoGeneratedName = true)]
#endif
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentSignerDelegate {

		[Abstract]
		[Export ("documentSigner:signData:hashAlgorithm:completion:")]
		void SignData (PSPDFSigner signer, NSData data, PSPDFSignatureHashAlgorithm hashAlgorithm, PSPDFDocumentSignDataCompletionHandler completion);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFTextMarkupAnnotation {

		[Static]
		[Export ("textOverlayAnnotationWithGlyphs:")]
		[return: NullAllowed]
		PSPDFTextMarkupAnnotation GetTextOverlayAnnotation ([NullAllowed] PSPDFGlyph[] glyphs);

		[Static]
		[Export ("textOverlayAnnotationWithRects:boundingBox:pageIndex:")]
		[return: NullAllowed]
		PSPDFTextMarkupAnnotation GetTextOverlayAnnotation ([BindAs (typeof (CGRect []))] NSValue [] rects, CGRect boundingBox, nuint pageIndex);

		[Export ("markedUpString")]
		string MarkedUpString { get; }

		[Static]
		[Export ("defaultColor")]
		UIColor DefaultColor { get; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFRedactionAnnotation : PSPDFOverridable {

		[NullAllowed, Export ("outlineColor", ArgumentSemantic.Assign)]
		UIColor OutlineColor { get; set; }

		[NullAllowed, Export ("overlayText")]
		string OverlayText { get; set; }

		[Export ("repeatOverlayText")]
		bool RepeatOverlayText { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFPKCS7 {

		[Export ("initWithDigest:type:privateKey:certificate:hashAlgorithm:encryptionAlgorithm:")]
		NativeHandle Constructor (NSData digest, PSPDFSignatureType signatureType, PSPDFPrivateKey privateKey, PSPDFX509 certificate, PSPDFSignatureHashAlgorithm hashAlgorithm, PSPDFSignatureEncryptionAlgorithm encryptionAlgorithm);

		[Export ("data")]
		NSData Data { get; }
	}

	interface IPSPDFSignatureContents { }

	[Protocol]
	interface PSPDFSignatureContents {

		[Abstract]
		[Export ("signData:")]
		NSData SignData (NSData dataToSign);
	}

	interface IPSPDFStylePreset { }

	[Protocol]
	interface PSPDFStylePreset : INSSecureCoding {

	}

	[BaseType (typeof (NSOperation))]
	[DisableDefaultCtor]
	interface PSPDFConversionOperation {

		[NullAllowed, Export ("outputURL", ArgumentSemantic.Copy)]
		NSUrl OutputUrl { get; }

		[NullAllowed, Export ("outputData")]
		NSData OutputData { get; }

		[NullAllowed, Export ("error")]
		NSError Error { get; }
	}

#if !__MACCATALYST__
	[BaseType (typeof (PSPDFConversionOperation), Name = "PSPDFURLConversionOperation")]
	[DisableDefaultCtor]
	interface PSPDFUrlConversionOperation {

		[Export ("inputURL", ArgumentSemantic.Copy)]
		NSUrl InputUrl { get; }
	}

	[BaseType (typeof (PSPDFConversionOperation), Name = "PSPDFHTMLConversionOperation")]
	[DisableDefaultCtor]
	interface PSPDFHtmlConversionOperation {

		[Export ("HTMLString")]
		string HtmlString { get; }
	}

	[BaseType (typeof(PSPDFConversionOperation))]
	[DisableDefaultCtor]
	interface PSPDFAttributedStringConversionOperation {

		[Export ("attributedString", ArgumentSemantic.Copy)]
		NSAttributedString AttributedString { get; }
	}
#endif

	delegate void PSPDFRenderDrawHandler (/*CGContext*/ IntPtr context, nuint page, CGRect cropBox, PSPDFRenderOptions options);

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFRenderOptions {

		[Export ("preserveAspectRatio")]
		bool PreserveAspectRatio { get; set; }

		[Export ("ignoreDisplaySettings")]
		bool IgnoreDisplaySettings { get; set; }

		[Export ("pageColor", ArgumentSemantic.Strong)]
		UIColor PageColor { get; set; }

		[Export ("invertRenderColor")]
		bool InvertRenderColor { get; set; }

		[Export ("filters", ArgumentSemantic.Assign)]
		PSPDFRenderFilter Filters { get; set; }

		[Export ("interpolationQuality", ArgumentSemantic.Assign)]
		CGInterpolationQuality InterpolationQuality { get; set; }

		[Export ("skipPageContent")]
		bool SkipPageContent { get; set; }

		[Export ("overlayAnnotations")]
		bool OverlayAnnotations { get; set; }

		[NullAllowed, Export ("skipAnnotationArray", ArgumentSemantic.Strong)]
		PSPDFAnnotation [] SkipAnnotationArray { get; set; }

		[Export ("ignorePageClip")]
		bool IgnorePageClip { get; set; }

		[Export ("allowAntialiasing")]
		bool AllowAntialiasing { get; set; }

		[NullAllowed, Export ("backgroundFill", ArgumentSemantic.Strong)]
		UIColor BackgroundFill { get; set; }

		[Export ("renderTextUsingCoreGraphics")]
		bool RenderTextUsingCoreGraphics { get; set; }

		[NullAllowed, Export ("interactiveFormFillColor", ArgumentSemantic.Strong)]
		UIColor InteractiveFormFillColor { get; set; }

		[NullAllowed, Export ("requiredFormBorderColor", ArgumentSemantic.Strong)]
		UIColor RequiredFormBorderColor { get; set; }

		[NullAllowed, Export ("drawBlock", ArgumentSemantic.Strong)]
		PSPDFRenderDrawHandler DrawHandler { get; set; }

		[Export ("drawSignHereOverlay")]
		bool DrawSignHereOverlay { get; set; }

		[Export ("drawRedactionsAsRedacted")]
		bool DrawRedactionsAsRedacted { get; set; }

		[NullAllowed, Export ("additionalCIFilters", ArgumentSemantic.Strong)]
		CIFilter [] AdditionalCIFilters { get; set; }

		[Export ("drawFlattened")]
		bool DrawFlattened { get; set; }

		[Export ("drawForPrinting")]
		bool DrawForPrinting { get; set; }

		[Export ("centered")]
		bool Centered { get; set; }

		[Export ("margin", ArgumentSemantic.Assign)]
		UIEdgeInsets Margin { get; set; }
	}

	[Static]
	interface PSPDFAnnotationStyleKey {

		[Field ("PSPDFAnnotationStyleKeyColor", PSPDFKitLibraryPath.LibraryPath)]
		NSString Color { get; }

		[Field ("PSPDFAnnotationStyleKeyFillColor", PSPDFKitLibraryPath.LibraryPath)]
		NSString FillColor { get; }

		[Field ("PSPDFAnnotationStyleKeyAlpha", PSPDFKitLibraryPath.LibraryPath)]
		NSString Alpha { get; }

		[Field ("PSPDFAnnotationStyleKeyLineWidth", PSPDFKitLibraryPath.LibraryPath)]
		NSString LineWidth { get; }

		[Field ("PSPDFAnnotationStyleKeyDashArray", PSPDFKitLibraryPath.LibraryPath)]
		NSString DashArray { get; }

		[Field ("PSPDFAnnotationStyleKeyLineEnd", PSPDFKitLibraryPath.LibraryPath)]
		NSString LineEnd { get; }

		[Field ("PSPDFAnnotationStyleKeyLineEnd1", PSPDFKitLibraryPath.LibraryPath)]
		NSString LineEnd1 { get; }

		[Field ("PSPDFAnnotationStyleKeyLineEnd2", PSPDFKitLibraryPath.LibraryPath)]
		NSString LineEnd2 { get; }

		[Field ("PSPDFAnnotationStyleKeyFontName", PSPDFKitLibraryPath.LibraryPath)]
		NSString FontName { get; }

		[Field ("PSPDFAnnotationStyleKeyFontSize", PSPDFKitLibraryPath.LibraryPath)]
		NSString FontSize { get; }

		[Field ("PSPDFAnnotationStyleKeyTextAlignment", PSPDFKitLibraryPath.LibraryPath)]
		NSString TextAlignment { get; }

		[Field ("PSPDFAnnotationStyleKeyBlendMode", PSPDFKitLibraryPath.LibraryPath)]
		NSString BlendMode { get; }

		[Field ("PSPDFAnnotationStyleKeyCalloutAction", PSPDFKitLibraryPath.LibraryPath)]
		NSString CalloutAction { get; }

		[Field ("PSPDFAnnotationStyleKeyColorPreset", PSPDFKitLibraryPath.LibraryPath)]
		NSString ColorPreset { get; }

		[Field ("PSPDFAnnotationStyleKeyOutlineColor", PSPDFKitLibraryPath.LibraryPath)]
		NSString OutlineColor { get; }

		[Field ("PSPDFAnnotationStyleKeyOverlayText", PSPDFKitLibraryPath.LibraryPath)]
		NSString OverlayText { get; }

		[Field ("PSPDFAnnotationStyleKeyRepeatOverlayText", PSPDFKitLibraryPath.LibraryPath)]
		NSString RepeatOverlayText { get; }

		[Field ("PSPDFAnnotationStyleKeyMeasurementSnapping", PSPDFKitLibraryPath.LibraryPath)]
		NSString MeasurementSnapping { get; }

		[Field ("PSPDFAnnotationStyleKeyContents", PSPDFKitLibraryPath.LibraryPath)]
		NSString Contents { get; }

		[Field ("PSPDFAnnotationStyleKeyMeasurementValueConfiguration", PSPDFKitLibraryPath.LibraryPath)]
		NSString MeasurementValueConfiguration { get; }
	}

	[BaseType (typeof (PSPDFConversionOperation))]
	[DisableDefaultCtor]
	interface PSPDFOfficeConversionOperation : INSProgressReporting {

		[Export ("inputURL")]
		NSUrl InputUrl { get; }

		[Export ("serverURL")]
		NSUrl ServerUrl { get; }

		[Export ("JWT")]
		string Jwt { get; }

		[Export ("progress")]
		NSProgress Progress { get; }
	}

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFComparisonConfigurationBuilder {

		[NullAllowed, Export ("oldDocumentStrokeColor", ArgumentSemantic.Strong)]
		UIColor OldDocumentStrokeColor { get; set; }

		[NullAllowed, Export ("newDocumentStrokeColor", ArgumentSemantic.Strong)]
		UIColor NewDocumentStrokeColor { get; set; }

		[Export ("blendMode", ArgumentSemantic.Assign)]
		CGBlendMode BlendMode { get; set; }

		[NullAllowed, Export ("workingDirectory", ArgumentSemantic.Strong)]
		NSUrl WorkingDirectory { get; set; }
	}

	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFComparisonConfiguration {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFComparisonConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		NativeHandle Constructor (PSPDFComparisonConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFComparisonConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFComparisonConfigurationBuilder> builderHandler);

		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFComparisonConfiguration GetUpdatedConfiguration ([NullAllowed] Action<PSPDFComparisonConfigurationBuilder> builderHandler);

		[NullAllowed, Export ("oldDocumentStrokeColor")]
		UIColor OldDocumentStrokeColor { get; }

		[NullAllowed, Export ("newDocumentStrokeColor")]
		UIColor NewDocumentStrokeColor { get; }

		[Export ("blendMode")]
		CGBlendMode BlendMode { get; }

		[NullAllowed, Export ("workingDirectory")]
		NSUrl WorkingDirectory { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFComparisonProcessor {

		[Export ("initWithConfiguration:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFComparisonConfiguration configuration);

		[Export ("comparisonDocumentWithOldDocument:pageIndex:newDocument:pageIndex:transform:error:")]
		[return: NullAllowed]
		PSPDFDocument GetComparisonDocument (PSPDFDocument oldDocument, nuint oldDocumentPageIndex, PSPDFDocument newDocument, nuint newDocumentPageIndex, CGAffineTransform transform, out NSError error);

		[Export ("comparisonDocumentWithOldDocument:pageIndex:points:newDocument:pageIndex:points:error:")]
		[return: NullAllowed]
		PSPDFDocument GetComparisonDocument (PSPDFDocument oldDocument, nuint oldDocumentPageIndex, [BindAs (typeof (CGPoint []))] NSValue [] oldDocumentPoints, PSPDFDocument newDocument, nuint newDocumentPageIndex, [BindAs (typeof (CGPoint []))] NSValue [] newDocumentPoints, out NSError error);
	}

	interface IPSPDFMeasurementAnnotation {}

	[Protocol]
	interface PSPDFMeasurementAnnotation {

		[Abstract]
		[Export ("isMeasurement")]
		bool IsMeasurement { get; }

		[Abstract]
		[NullAllowed, Export ("measurementInfo")]
		PSPDFMeasurementInfo MeasurementInfo { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFMeasurementScale {

		[Export ("unitFrom")]
		PSPDFUnitFrom UnitFrom { get; }

		[Export ("unitTo")]
		PSPDFUnitTo UnitTo { get; }

		[Export ("from")]
		double From { get; }

		[Export ("to")]
		double To { get; }

		[Export ("initWithFrom:unitFrom:to:unitTo:")]
		[DesignatedInitializer]
		NativeHandle Constructor (double from, PSPDFUnitFrom unitFrom, double to, PSPDFUnitTo unitTo);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFMeasurementCalibration {

		[Export ("unitTo")]
		PSPDFUnitTo UnitTo { get; }

		[Export ("value")]
		double Value { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFMeasurementInfo {

		[Export ("scale")]
		PSPDFMeasurementScale Scale { get; }

		[Export ("mode")]
		PSPDFMeasurementMode Mode { get; }

		[Export ("precision")]
		PSPDFMeasurementPrecision Precision { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFGlyphSequence : INSCopying, INSSecureCoding {

		[Export ("initWithGlyphs:")]
		[DesignatedInitializer]
		NativeHandle Constructor (PSPDFGlyph [] glyphs);

		[Export ("glyphs", ArgumentSemantic.Copy)]
		PSPDFGlyph [] Glyphs { get; }

		[Export ("range")]
		NSRange Range { get; }

		[Export ("text")]
		string Text { get; }

		[Export ("boundingBox")]
		CGRect BoundingBox { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFMeasurementValueConfiguration {

		[Export ("name"), NullAllowed]
		string Name { get; }

		[Export ("scale")]
		PSPDFMeasurementScale Scale { get; }

		[Export ("precision")]
		PSPDFMeasurementPrecision Precision { get; }

		[Export ("initWithName:scale:precision:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string name, PSPDFMeasurementScale scale, PSPDFMeasurementPrecision precision);
	}
}
