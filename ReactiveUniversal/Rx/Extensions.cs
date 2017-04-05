using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ReactiveUniversal.Rx {

	public static class Extensions {

		public static IObservable<TSource> Async<TSource>(this IObservable<TSource> source) {
			return source.SubscribeOn(TaskPoolScheduler.Default)
				.ObserveOnDispatcher(CoreDispatcherPriority.Normal);
		}

		public static IDisposable BindTo<TSource>(this IObservable<TSource> source, IObserver<TSource> observer) {
			if (observer == null) {
				throw new ArgumentNullException("observer is null");
			}
			return source.Subscribe(observer);
		}

		public static IDisposable BindNext<TSource>(this IObservable<TSource> source, Action<TSource> callback) {
			if (callback == null) {
				throw new ArgumentNullException("callback is null");
			}
			return source.Subscribe(callback);
		}

		public static void DisposeBy(this IDisposable source, DisposeBag disposes) {
			if (disposes == null) {
				throw new ArgumentNullException("disposes are null");
			}
			disposes.Add(source);
		}

		#region UIElement Extensions

		public static UIBindingObserver<TSource, double> RxOpacity<TSource>(this TSource control) where TSource : UIElement {
			return new UIBindingObserver<TSource, double>(control, (ui, opacity) => {
				ui.Opacity = opacity;
			});
		}

		public static UIBindingObserver<TSource, Visibility> RxVisibility<TSource>(this TSource control) where TSource : UIElement {
			return new UIBindingObserver<TSource, Visibility>(control, (ui, visibility) => {
				ui.Visibility = visibility;
			});
		}

		public static IObservable<TappedRoutedEventArgs> RxTapped<TSource>(this TSource control) where TSource : UIElement {
			return Observable.FromEvent<TappedEventHandler, TappedRoutedEventArgs>(hand => {
				control.Tapped += hand;
			}, hand => {
				control.Tapped -= hand;
			});
		}

		public static IObservable<RightTappedRoutedEventArgs> RxRightTapped<TSource>(this TSource control) where TSource : UIElement {
			return Observable.FromEvent<RightTappedEventHandler, RightTappedRoutedEventArgs>(hand => {
				control.RightTapped += hand;
			}, hand => {
				control.RightTapped -= hand;
			});
		}

		public static IObservable<DoubleTappedRoutedEventArgs> RxDoubleTapped<TSource>(this TSource control) where TSource : UIElement {
			return Observable.FromEvent<DoubleTappedEventHandler, DoubleTappedRoutedEventArgs>(hand => {
				control.DoubleTapped += hand;
			}, hand => {
				control.DoubleTapped -= hand;
			});
		}

		#endregion

		#region FrameworkElement Extensions

		public static UIBindingObserver<TSource, double> RxHeight<TSource>(this TSource control) where TSource : FrameworkElement {
			return new UIBindingObserver<TSource, double>(control, (ui, height) => {
				ui.Height = height;
			});
		}

		public static UIBindingObserver<TSource, double> RxWidth<TSource>(this TSource control) where TSource : FrameworkElement {
			return new UIBindingObserver<TSource, double>(control, (ui, width) => {
				ui.Width = width;
			});
		}

		public static UIBindingObserver<TSource, Thickness> RxMargin<TSource>(this TSource control) where TSource : FrameworkElement {
			return new UIBindingObserver<TSource, Thickness>(control, (ui, ticknes) => {
				ui.Margin = ticknes;
			});
		}

		public static UIBindingObserver<TSource, HorizontalAlignment> RxHorizontalAlignment<TSource>(this TSource control) where TSource : FrameworkElement {
			return new UIBindingObserver<TSource, HorizontalAlignment>(control, (ui, hAlignment) => {
				ui.HorizontalAlignment = hAlignment;
			});
		}

		public static UIBindingObserver<TSource, VerticalAlignment> RxVerticalAlignment<TSource>(this TSource control) where TSource : FrameworkElement {
			return new UIBindingObserver<TSource, VerticalAlignment>(control, (ui, vAlignment) => {
				ui.VerticalAlignment = vAlignment;
			});
		}

		public static IObservable<RoutedEventArgs> RxLoaded<TSource>(this TSource control) where TSource : FrameworkElement {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.Loaded += hand;
			}, hand => {
				control.Loaded -= hand;
			});
		}

		public static IObservable<RoutedEventArgs> RxUnloaded<TSource>(this TSource control) where TSource : FrameworkElement {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.Unloaded += hand;
			}, hand => {
				control.Unloaded -= hand;
			});
		}

		#endregion

		#region Control Extensions

		public static UIBindingObserver<TSource, bool> RxIsEnabled<TSource>(this TSource control) where TSource : Control {
			return new UIBindingObserver<TSource, bool>(control, (ui, enabled) => {
				ui.IsEnabled = enabled;
			});
		}

		public static UIBindingObserver<TSource, Brush> RxBackground<TSource>(this TSource control) where TSource : Control {
			return new UIBindingObserver<TSource, Brush>(control, (ui, background) => {
				ui.Background = background;
			});
		}

		public static UIBindingObserver<TSource, Brush> RxForeground<TSource>(this TSource control) where TSource : Control {
			return new UIBindingObserver<TSource, Brush>(control, (ui, foreground) => {
				ui.Foreground = foreground;
			});
		}

		public static UIBindingObserver<TSource, Brush> RxBorderBrush<TSource>(this TSource control) where TSource : Control {
			return new UIBindingObserver<TSource, Brush>(control, (ui, borderBrush) => {
				ui.BorderBrush = borderBrush;
			});
		}

		public static UIBindingObserver<TSource, Thickness> RxBorderThickness<TSource>(this TSource control) where TSource : Control {
			return new UIBindingObserver<TSource, Thickness>(control, (ui, borderThickness) => {
				ui.BorderThickness = borderThickness;
			});
		}

		public static UIBindingObserver<TSource, double> RxFontSize<TSource>(this TSource control) where TSource : Control {
			return new UIBindingObserver<TSource, double>(control, (ui, fontSize) => {
				ui.FontSize = fontSize;
			});
		}

		public static UIBindingObserver<TSource, FontStyle> RxFontStyle<TSource>(this TSource control) where TSource : Control {
			return new UIBindingObserver<TSource, FontStyle>(control, (ui, fontStyle) => {
				ui.FontStyle = fontStyle;
			});
		}

		public static UIBindingObserver<TSource, Thickness> RxPadding<TSource>(this TSource control) where TSource : Control {
			return new UIBindingObserver<TSource, Thickness>(control, (ui, padding) => {
				ui.Padding = padding;
			});
		}

		public static UIBindingObserver<TSource, bool> RxIsFocusEngaged<TSource>(this TSource control) where TSource: Control {
			return new UIBindingObserver<TSource, bool>(control, (ui, focused) => {
				control.IsFocusEngaged = focused;
			});
		}

		public static IObservable<DependencyPropertyChangedEventArgs> RxIsEnabledChanged<TSource>(this TSource control) where TSource : Control {
			return Observable.FromEvent<DependencyPropertyChangedEventHandler, DependencyPropertyChangedEventArgs>(hand => {
				control.IsEnabledChanged += hand;
			}, hand => {
				control.IsEnabledChanged -= hand;
			});
		}

		public static IObservable<FocusEngagedEventArgs> RxFocusEngaged<TSource>(this TSource control) where TSource: Control {
			return Observable.FromEvent<TypedEventHandler<Control, FocusEngagedEventArgs>, FocusEngagedEventArgs>(hand => {
				control.FocusEngaged += hand;
			}, hand => {
				control.FocusEngaged -= hand;
			});
		}

		public static IObservable<FocusDisengagedEventArgs> RxFocusDisengaged<TSource>(this TSource control) where TSource: Control {
			return Observable.FromEvent<TypedEventHandler<Control, FocusDisengagedEventArgs>, FocusDisengagedEventArgs>(hand => {
				control.FocusDisengaged += hand;
			}, hand => {
				control.FocusDisengaged -= hand;
			});
		}

		public static ControlProperty<bool> RxIsEnabledProperty<TSource>(this TSource control) where TSource: Control {
			var source = control.RxIsEnabledChanged()
				.Select(args => Boolean.Parse((args.NewValue ?? false).ToString()))
				.TakeUntil(control.RxUnloaded());

			var bindingObserver = control.RxIsEnabled();

			return new ControlProperty<bool>(source, bindingObserver);
		}

		public static ControlProperty<bool> RxIsFocusedPropery<TSource>(this TSource control) where TSource: Control {
			var source = control.RxFocusEngaged()
				.Select(e => control.IsFocusEngaged)
				.TakeUntil(control.RxUnloaded());

			var bindingObserver = control.RxIsFocusEngaged();

			return new ControlProperty<bool>(source, bindingObserver);
		}

		#endregion

		#region ButtonBase Extensions

		public static UIBindingObserver<TSource, ClickMode> RxClickMode<TSource>(this TSource control) where TSource: ButtonBase {
			return new UIBindingObserver<TSource, ClickMode>(control, (ui, clickMode) => {
				ui.ClickMode = clickMode;
			});
		}

		public static IObservable<RoutedEventArgs> RxClick<TSource>(this TSource control) where TSource: ButtonBase {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.Click += hand;
			}, hand => {
				control.Click -= hand;
			});
		}

		#endregion

		#region ComboBox Extensions

		public static UIBindingObserver<TSource, bool> RxIsTextSearchEnabled<TSource>(this TSource control) where TSource: ComboBox {
			return new UIBindingObserver<TSource, bool>(control, (ui, enabled) => {
				ui.IsTextSearchEnabled = enabled;
			});
		}

		public static UIBindingObserver<TSource, string> RxPlaceholderText<TSource>(this TSource control) where TSource: ComboBox {
			return new UIBindingObserver<TSource, string>(control, (ui, text) => {
				ui.PlaceholderText = text;
			});
		}

		public static IObservable<object> RxDropDrownClosed<TSource>(this TSource control) where TSource: ComboBox {
			return Observable.FromEvent<EventHandler<object>, object>(hand => {
				control.DropDownClosed += hand;
			}, hand => {
				control.DropDownClosed -= hand;
			});
		}

		public static IObservable<object> RxDropDrownOpened<TSource>(this TSource control) where TSource : ComboBox {
			return Observable.FromEvent<EventHandler<object>, object>(hand => {
				control.DropDownOpened += hand;
			}, hand => {
				control.DropDownOpened -= hand;
			});
		}

		#endregion

		#region Selector Extensions 

		public static UIBindingObserver<TSource, int> RxSelectedIndex<TSource>(this TSource control) where TSource: Selector {
			return new UIBindingObserver<TSource, int>(control, (ui, selectedIndex) => {
				ui.SelectedIndex = selectedIndex;
			});
		}

		public static UIBindingObserver<TSource, object> RxSelectedItem<TSource>(this TSource control) where TSource: Selector {
			return new UIBindingObserver<TSource, object>(control, (ui, selectedItem) => {
				ui.SelectedItem = selectedItem;
			});
		}

		public static UIBindingObserver<TSource, object> RxSelectedValue<TSource>(this TSource control) where TSource: Selector {
			return new UIBindingObserver<TSource, object>(control, (ui, selectedValue) => {
				ui.SelectedValue = selectedValue;
			});
		}

		public static IObservable<SelectionChangedEventArgs> RxSelectionChanged<TSource>(this TSource control) where TSource: Selector {
			return Observable.FromEvent<SelectionChangedEventHandler, SelectionChangedEventArgs>(hand => {
				control.SelectionChanged += hand;
			}, hand => {
				control.SelectionChanged -= hand;
			});
		}

		public static ControlProperty<int> RxSelectedIndexProperty<TSource>(this TSource control) where TSource: Selector {
			var source = control.RxSelectionChanged()
				.Select(e => control.SelectedIndex)
				.TakeUntil(control.RxUnloaded());

			var bindingObserver = control.RxSelectedIndex();

			return new ControlProperty<int>(source, bindingObserver);
		}

		public static ControlProperty<object> RxSelectedItemProperty<TSource>(this TSource control) where TSource : Selector {
			var source = control.RxSelectionChanged()
				.Select(e => control.SelectedItem)
				.TakeUntil(control.RxUnloaded());

			var bindingObserver = control.RxSelectedItem();

			return new ControlProperty<object>(source, bindingObserver);
		}

		public static ControlProperty<object> RxSelectedValueProperty<TSource>(this TSource control) where TSource : Selector {
			var source = control.RxSelectionChanged()
				.Select(e => control.SelectedValue)
				.TakeUntil(control.RxUnloaded());

			var bindingObserver = control.RxSelectedValue();

			return new ControlProperty<object>(source, bindingObserver);
		}

		#endregion

		#region ToggleButton Extensions

		public static UIBindingObserver<TSource, bool?> RxIsChecked<TSource>(this TSource control) where TSource: ToggleButton {
			return new UIBindingObserver<TSource, bool?>(control, (ui, check) => {
				ui.IsChecked = check;
			});
		}

		public static UIBindingObserver<TSource, bool> RxIsThreeState<TSource>(this TSource control) where TSource: ToggleButton {
			return new UIBindingObserver<TSource, bool>(control, (ui, threeState) => {
				ui.IsThreeState = threeState;
			});
		}

		public static IObservable<RoutedEventArgs> RxChecked<TSource>(this TSource control) where TSource: ToggleButton {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.Checked += hand;
			}, hand => {
				control.Checked -= hand;
			});
		}

		public static IObservable<RoutedEventArgs> RxUnchecked<TSource>(this TSource control) where TSource : ToggleButton {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.Unchecked += hand;
			}, hand => {
				control.Unchecked -= hand;
			});
		}

		public static IObservable<RoutedEventArgs> RxIndeterminate<TSource>(this TSource control) where TSource : ToggleButton {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.Indeterminate += hand;
			}, hand => {
				control.Indeterminate -= hand;
			});
		}

		#endregion

		#region RangeBase Extensions

		public static UIBindingObserver<TSource, double> RxValue<TSource>(this TSource control) where TSource : RangeBase {
			return new UIBindingObserver<TSource, double>(control, (ui, value) => {
				ui.Value = value;
			});
		}

		public static UIBindingObserver<TSource, double> RxMinimum<TSource>(this TSource control) where TSource : RangeBase {
			return new UIBindingObserver<TSource, double>(control, (ui, min) => {
				ui.Minimum = min;
			});
		}

		public static UIBindingObserver<TSource, double> RxMaximum<TSource>(this TSource control) where TSource : RangeBase {
			return new UIBindingObserver<TSource, double>(control, (ui, max) => {
				ui.Maximum = max;
			});
		}

		public static IObservable<RangeBaseValueChangedEventArgs> RxValueChanged<TSource>(this TSource control) where TSource : RangeBase {
			return Observable.FromEvent<RangeBaseValueChangedEventHandler, RangeBaseValueChangedEventArgs>(hand => {
				control.ValueChanged += hand;
			}, hand => {
				control.ValueChanged -= hand;
			});
		}

		public static ControlProperty<double> RxValueProperty<TSource>(this TSource control) where TSource : RangeBase {
			var source = control.RxValueChanged()
				.Select(x => x.NewValue)
				.TakeUntil(control.RxUnloaded());

			var bindingObserver = control.RxValue();

			return new ControlProperty<double>(source, bindingObserver);
		}

		#endregion

		#region TextBox Extensions

		public static UIBindingObserver<TSource, string> RxSelectedText<TSource>(this TSource control) where TSource: TextBox {
			return new UIBindingObserver<TSource, string>(control, (ui, selectedText) => {
				ui.SelectedText = selectedText;
			});
		}

		public static UIBindingObserver<TSource, string> RxText<TSource>(this TSource control) where TSource: TextBox {
			return new UIBindingObserver<TSource, string>(control, (ui, text) => {
				ui.Text = text;
			});
		}

		public static UIBindingObserver<TSource, string> RxPlacholderText<TSource>(this TSource control) where TSource : TextBox {
			return new UIBindingObserver<TSource, string>(control, (ui, text) => {
				ui.PlaceholderText = text;
			});
		}

		public static UIBindingObserver<TSource, TextAlignment> RxTextAlignment<TSource>(this TSource control) where TSource: TextBox {
			return new UIBindingObserver<TSource, TextAlignment>(control, (ui, alignment) => {
				ui.TextAlignment = alignment;
			});
		}

		public static UIBindingObserver<TSource, int> RxMaxLength<TSource>(this TSource control) where TSource: TextBox {
			return new UIBindingObserver<TSource, int>(control, (ui, maxLength) => {
				ui.MaxLength = maxLength;
			});
		}

		public static IObservable<TextChangedEventArgs> RxTextChanged<TSource>(this TSource control) where TSource: TextBox {
			return Observable.FromEvent<TextChangedEventHandler, TextChangedEventArgs>(hand => {
				control.TextChanged += hand;
			}, hand => {
				control.TextChanged -= hand;
			});
		}

		public static IObservable<RoutedEventArgs> RxTextSelectionChanged<TSource>(this TSource control) where TSource: TextBox {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.SelectionChanged += hand;
			}, hand => {
				control.SelectionChanged -= hand;
			});
		}

		public static ControlProperty<string> RxTextProperty<TSource>(this TSource control) where TSource: TextBox {
			var source = control.RxTextChanged()
				.Select(e => control.Text)
				.TakeUntil(control.RxUnloaded());

			var bindingObserver = control.RxText();

			return new ControlProperty<string>(source, bindingObserver);
		}

		public static ControlProperty<string> RxTextSelectionProperty<TSource>(this TSource control) where TSource: TextBox {
			var source = control.RxTextSelectionChanged()
				.Select(e => control.SelectedText)
				.TakeUntil(control.RxUnloaded());

			var bindingObserver = control.RxSelectedText();

			return new ControlProperty<string>(source, bindingObserver);
		}

		#endregion

		#region ContentControl Extension

		public static UIBindingObserver<TSource, object> RxContent<TSource>(this TSource control) where TSource: ContentControl {
			return new UIBindingObserver<TSource, object>(control, (ui, content) => {
				control.Content = content;
			});
		}

		#endregion

		#region Frame Extensions

		public static IObservable<NavigationEventArgs> RxNavigated<TSource>(this TSource control) where TSource: Frame {
			return Observable.FromEvent<NavigatedEventHandler, NavigationEventArgs>(hand => {
				control.Navigated += hand;
			}, hand => {
				control.Navigated -= hand;
			});
		}

		public static IObservable<NavigatingCancelEventArgs> RxNavigating<TSource>(this TSource control) where TSource: Frame {
			return Observable.FromEvent<NavigatingCancelEventHandler, NavigatingCancelEventArgs>(hand => {
				control.Navigating += hand;
			}, hand => {
				control.Navigating -= hand;
			});
		}

		#endregion

		#region DatePicker Extensions

		public static UIBindingObserver<TSource, Orientation> RxOrientation<TSource>(this TSource control) where TSource : DatePicker {
			return new UIBindingObserver<TSource, Orientation>(control, (ui, orientation) => {
				ui.Orientation = orientation;
			});
		}

		public static UIBindingObserver<TSource, DateTimeOffset> RxDate<TSource>(this TSource control) where TSource: DatePicker {
			return new UIBindingObserver<TSource, DateTimeOffset>(control, (ui, date) => {
				control.Date = date;
			});
		}

		public static UIBindingObserver<TSource, DateTimeOffset> RxMinYear<TSource>(this TSource control) where TSource : DatePicker {
			return new UIBindingObserver<TSource, DateTimeOffset>(control, (ui, minYear) => {
				control.MinYear = minYear;
			});
		}

		public static UIBindingObserver<TSource, DateTimeOffset> RxMaxYear<TSource>(this TSource control) where TSource : DatePicker {
			return new UIBindingObserver<TSource, DateTimeOffset>(control, (ui, maxYear) => {
				control.MaxYear = maxYear;
			});
		}

		public static UIBindingObserver<TSource, bool> RxDayVisibile<TSource>(this TSource control) where TSource: DatePicker {
			return new UIBindingObserver<TSource, bool>(control, (ui, day) => {
				control.DayVisible = day;
			});
		}

		public static UIBindingObserver<TSource, string> RxDayFormat<TSource>(this TSource control) where TSource: DatePicker {
			return new UIBindingObserver<TSource, string>(control, (ui, dayFormat) => {
				ui.DayFormat = dayFormat;
			});
		}

		public static UIBindingObserver<TSource, bool> RxMonthVisibile<TSource>(this TSource control) where TSource : DatePicker {
			return new UIBindingObserver<TSource, bool>(control, (ui, month) => {
				control.MonthVisible = month;
			});
		}

		public static UIBindingObserver<TSource, string> RxMonthFormat<TSource>(this TSource control) where TSource : DatePicker {
			return new UIBindingObserver<TSource, string>(control, (ui, monthFormat) => {
				ui.MonthFormat = monthFormat;
			});
		}

		public static UIBindingObserver<TSource, bool> RxYearVisibile<TSource>(this TSource control) where TSource : DatePicker {
			return new UIBindingObserver<TSource, bool>(control, (ui, year) => {
				control.YearVisible = year;
			});
		}

		public static UIBindingObserver<TSource, string> RxYearFormat<TSource>(this TSource control) where TSource : DatePicker {
			return new UIBindingObserver<TSource, string>(control, (ui, yearFormat) => {
				ui.YearFormat = yearFormat;
			});
		}

		public static IObservable<DatePickerValueChangedEventArgs> RxDateChanged<TSource>(this TSource control) where TSource: DatePicker {
			return Observable.FromEvent<EventHandler<DatePickerValueChangedEventArgs>, DatePickerValueChangedEventArgs>(hand => {
				control.DateChanged += hand;
			}, hand => {
				control.DateChanged -= hand;
			});
		}

		public static ControlProperty<DateTimeOffset> RxDateProperty<TSource>(this TSource control) where TSource: DatePicker {
			var source = control.RxDateChanged()
				.Select(e => e.NewDate)
				.TakeUntil(control.RxUnloaded());

			var bindingObserver = control.RxDate();

			return new ControlProperty<DateTimeOffset>(source, bindingObserver);
		}

		#endregion

		#region Image Extensions

		public static UIBindingObserver<Image, Thickness> RxNineGrid(this Image control) {
			return new UIBindingObserver<Image, Thickness>(control, (ui, nineGrid) => {
				ui.NineGrid = nineGrid;
			});
		}

		public static UIBindingObserver<Image, ImageSource> RxSource(this Image control) {
			return new UIBindingObserver<Image, ImageSource>(control, (ui, imageSource) => {
				ui.Source = imageSource;
			});
		}

		public static UIBindingObserver<Image, Stretch> RxStrech(this Image control) {
			return new UIBindingObserver<Image, Stretch>(control, (ui, strech) => {
				ui.Stretch = strech;
			});
		}

		public static IObservable<ExceptionRoutedEventArgs> RxImageFailed(this Image control) {
			return Observable.FromEvent<ExceptionRoutedEventHandler, ExceptionRoutedEventArgs>(hand => {
				control.ImageFailed += hand;
			}, hand => {
				control.ImageFailed -= hand;
			});
		}

		public static IObservable<RoutedEventArgs> RxImageOpened(this Image control) {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.ImageOpened += hand;
			}, hand => {
				control.ImageOpened -= hand;
			});
		}

		#endregion

		#region ListViewBase Extensions

		public static UIBindingObserver<TSource, object> RxFooter<TSource>(this TSource control) where TSource : ListViewBase {
			return new UIBindingObserver<TSource, object>(control, (ui, footer) => {
				ui.Footer = footer;
			});
		}

		public static UIBindingObserver<TSource, object> RxHeader<TSource>(this TSource control) where TSource : ListViewBase {
			return new UIBindingObserver<TSource, object>(control, (ui, header) => {
				ui.Header = header;
			});
		}

		public static UIBindingObserver<TSource, bool> RxIsActiveView<TSource>(this TSource control) where TSource : ListViewBase {
			return new UIBindingObserver<TSource, bool>(control, (ui, active) => {
				ui.IsActiveView = active;
			});
		}

		public static UIBindingObserver<TSource, bool> RxIsItemClickEnabled<TSource>(this TSource control) where TSource : ListViewBase {
			return new UIBindingObserver<TSource, bool>(control, (ui, itemClick) => {
				ui.IsItemClickEnabled = itemClick;
			});
		}

		public static UIBindingObserver<TSource, bool> RxIsMultiSelectCheckBoxEnabled<TSource>(this TSource control) where TSource : ListViewBase {
			return new UIBindingObserver<TSource, bool>(control, (ui, multi) => {
				ui.IsMultiSelectCheckBoxEnabled = multi;
			});
		}

		public static UIBindingObserver<TSource, bool> RxIsSwipeEnabled<TSource>(this TSource control) where TSource : ListViewBase {
			return new UIBindingObserver<TSource, bool>(control, (ui, swipe) => {
				ui.IsSwipeEnabled = swipe;
			});
		}

		public static IObservable<ItemClickEventArgs> RxItemClick<TSource>(this TSource control) where TSource: ListViewBase {
			return Observable.FromEvent<ItemClickEventHandler, ItemClickEventArgs>(hand => {
				control.ItemClick += hand;
			}, hand => {
				control.ItemClick -= hand;
			});
		}

		#endregion

		#region ListBox Extensions

		public static UIBindingObserver<TSource, SelectionMode> RxSelectionMode<TSource>(this TSource control) where TSource: ListBox {
			return new UIBindingObserver<TSource, SelectionMode>(control, (ui, selectionMode) => {
				ui.SelectionMode = selectionMode;
			});
		}
		
		#endregion

		#region SelectorItem Extensions

		public static UIBindingObserver<TSource, bool> RxIsSelected<TSource>(this TSource control) where TSource: SelectorItem {
			return new UIBindingObserver<TSource, bool>(control, (ui, selected) => {
				ui.IsSelected = selected;
			});
		}

		#endregion

		#region MediaElement Extensions

		public static UIBindingObserver<MediaElement, AudioCategory> RxAudioCategory(this MediaElement control) {
			return new UIBindingObserver<MediaElement, AudioCategory>(control, (ui, category) => {
				ui.AudioCategory = category;
			});
		}

		public static UIBindingObserver<MediaElement, AudioDeviceType> RxAudioDeviceType(this MediaElement control) {
			return new UIBindingObserver<MediaElement, AudioDeviceType>(control, (ui, deviceType) => {
				ui.AudioDeviceType = deviceType;
			});
		}

		public static UIBindingObserver<MediaElement, int?> RxAudioStreamIndex(this MediaElement control) {
			return new UIBindingObserver<MediaElement, int?>(control, (ui, streamIndex) => {
				ui.AudioStreamIndex = streamIndex;
			});
		}

		public static UIBindingObserver<MediaElement, bool> RxAutoPlay(this MediaElement control) {
			return new UIBindingObserver<MediaElement, bool>(control, (ui, autoPlay) => {
				ui.AutoPlay = autoPlay;
			});
		}

		public static UIBindingObserver<MediaElement, double> RxBalance(this MediaElement control) {
			return new UIBindingObserver<MediaElement, double>(control, (ui, balance) => {
				ui.Balance = balance;
			});
		}

		public static UIBindingObserver<MediaElement, double> RxDefaultPlaybackRate(this MediaElement control) {
			return new UIBindingObserver<MediaElement, double>(control, (ui, playbackRate) => {
				ui.DefaultPlaybackRate = playbackRate;
			});
		}

		public static UIBindingObserver<MediaElement, bool> RxIsFullWindow(this MediaElement control) {
			return new UIBindingObserver<MediaElement, bool>(control, (ui, fullWindow) => {
				ui.IsFullWindow = fullWindow;
			});
		}

		public static UIBindingObserver<MediaElement, bool> RxIsLooping(this MediaElement control) {
			return new UIBindingObserver<MediaElement, bool>(control, (ui, looping) => {
				ui.IsLooping = looping;
			});
		}

		public static UIBindingObserver<MediaElement, bool> RxIsMuted(this MediaElement control) {
			return new UIBindingObserver<MediaElement, bool>(control, (ui, muted) => {
				ui.IsMuted = muted;
			});
		}

		public static UIBindingObserver<MediaElement, double> RxPlaybackRate(this MediaElement control) {
			return new UIBindingObserver<MediaElement, double>(control, (ui, playbackRate) => {
				ui.PlaybackRate = playbackRate;
			});
		}

		public static UIBindingObserver<MediaElement, TimeSpan> RxPosition(this MediaElement control) {
			return new UIBindingObserver<MediaElement, TimeSpan>(control, (ui, position) => {
				control.Position = position;
			});
		}

		public static UIBindingObserver<MediaElement, ImageSource> RxPosterSource(this MediaElement control) {
			return new UIBindingObserver<MediaElement, ImageSource>(control, (ui, source) => {
				ui.PosterSource = source;
			});
		}

		public static UIBindingObserver<MediaElement, Uri> RxSource(this MediaElement control) {
			return new UIBindingObserver<MediaElement, Uri>(control, (ui, uri) => {
				ui.Source = uri;
			});
		}

		public static UIBindingObserver<MediaElement, Stretch> RxStretch(this MediaElement control) {
			return new UIBindingObserver<MediaElement, Stretch>(control, (ui, stretch) => {
				ui.Stretch = stretch;
			});
		}

		public static UIBindingObserver<MediaElement, double> RxVolume(this MediaElement control) {
			return new UIBindingObserver<MediaElement, double>(control, (ui, volume) => {
				ui.Volume = volume;
			});
		}

		public static IObservable<RoutedEventArgs> RxBufferingProgressChanged(this MediaElement control) {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.BufferingProgressChanged += hand;
			}, hand => {
				control.BufferingProgressChanged -= hand;
			});
		}

		public static IObservable<RoutedEventArgs> RxCurrentStateChanged(this MediaElement control) {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.CurrentStateChanged += hand;
			}, hand => {
				control.CurrentStateChanged -= hand;
			});
		}

		public static IObservable<RoutedEventArgs> RxDownloadProgressChanged(this MediaElement control) {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.DownloadProgressChanged += hand;
			}, hand => {
				control.DownloadProgressChanged -= hand;
			});
		}

		public static IObservable<RoutedEventArgs> RxMediaEnded(this MediaElement control) {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.MediaEnded += hand;
			}, hand => {
				control.MediaEnded -= hand;
			});
		}

		public static IObservable<ExceptionRoutedEventArgs> RxMediaFailed(this MediaElement control) {
			return Observable.FromEvent<ExceptionRoutedEventHandler, ExceptionRoutedEventArgs>(hand => {
				control.MediaFailed += hand;
			}, hand => {
				control.MediaFailed -= hand;
			});
		}

		public static IObservable<RoutedEventArgs> RxMediaOpened(this MediaElement control) {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.MediaOpened += hand;
			}, hand => {
				control.MediaOpened -= hand;
			});
		}

		public static IObservable<RateChangedRoutedEventArgs> RxRateChanged(this MediaElement control) {
			return Observable.FromEvent<RateChangedRoutedEventHandler, RateChangedRoutedEventArgs>(hand => {
				control.RateChanged += hand;
			}, hand => {
				control.RateChanged -= hand;
			});
		}

		public static IObservable<RoutedEventArgs> RxSeekCompleted(this MediaElement control) {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.SeekCompleted += hand;
			}, hand => {
				control.SeekCompleted -= hand;
			});
		}

		public static IObservable<RoutedEventArgs> RxVolumeChanged(this MediaElement control) {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(hand => {
				control.VolumeChanged += hand;
			}, hand => {
				control.VolumeChanged -= hand;
			});
		}

		public static ControlProperty<TimeSpan> RxPositionProperty(this MediaElement control) {
			var source = control.RxSeekCompleted()
				.Select(e => control.Position)
				.TakeUntil(control.RxUnloaded());

			var bindingObserver = control.RxPosition();

			return new ControlProperty<TimeSpan>(source, bindingObserver);
		}

		public static ControlProperty<double> RxRateProperty(this MediaElement control) {
			var source = control.RxRateChanged()
				.Select(e => control.PlaybackRate)
				.TakeUntil(control.RxUnloaded());

			var bindingObserver = control.RxPlaybackRate();

			return new ControlProperty<double>(source, bindingObserver);
		}

		public static ControlProperty<double> RxVolumeProperty(this MediaElement control) {
			var source = control.RxVolumeChanged()
				.Select(e => control.Volume)
				.TakeUntil(control.RxUnloaded());

			var bindingObserver = control.RxVolume();

			return new ControlProperty<double>(source, bindingObserver);
		}

		#endregion

		#region PasswordBox Extensions

		public static UIBindingObserver<PasswordBox, string> RxPassword(this PasswordBox control) {
			return new UIBindingObserver<PasswordBox, string>(control, (ui, password) => {
				ui.Password = password;
			});
		}

		public static UIBindingObserver<PasswordBox, string> RxPasswordChar(this PasswordBox control) {
			return new UIBindingObserver<PasswordBox, string>(control, (ui, passwordChar) => {
				ui.PasswordChar = passwordChar;
			});
		}

		public static IObservable<RoutedEventArgs> RxPasswordChanged(this PasswordBox control) {
			return Observable.FromEvent<RoutedEventHandler, RoutedEventArgs>(events => {
				control.PasswordChanged += events;
			}, events => {
				control.PasswordChanged -= events;
			});
		}

		public static ControlProperty<string> RxPasswordProperty(this PasswordBox control) {
			var source = control.RxPasswordChanged()
				.Select(x => control.Password)
				.TakeUntil(control.RxUnloaded());

			var bindingObserver = control.RxPassword();

			return new ControlProperty<string>(source, bindingObserver);
		}

		#endregion

		#region 

		#endregion
	}
}
