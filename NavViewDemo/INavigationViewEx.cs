using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace NavViewDemo
{
    public enum VisibleWhenTargets { Narrow, Wide, Both }

    public interface INavItemHeaderEx
    {
        VisibleWhenTargets VisibleWhen { get; set; }
        object NarrowContent { get; set; }
        object Content { get; set; }
        void SetBaseContent(object content);
    }

    public interface INavItemEx
    {
        bool ClearBackStack { get; set; }
        object PageParameter { get; set; }
        Type PageType { get; set; }
        NavigationTransitionInfo TransitionInfo { get; set; }
    }

    public interface INavigationViewEx
    {
        event TypedEventHandler<INavigationViewEx, INavItemEx> SelectionChanged;
        INavItemEx SelectedItem { get; set; }
        Type SettingsPageType { get; set; }
    }

    [ContentProperty(Name = nameof(Content))]
    public class NavigationViewItemHeaderEx :
        NavigationViewItemHeader,
        INavItemHeaderEx
    {
        public VisibleWhenTargets VisibleWhen { get; set; } = VisibleWhenTargets.Both;

        public void SetBaseContent(object content)
            => base.Content = content;

        public new object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public new static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(object),
                typeof(INavItemHeaderEx), new PropertyMetadata(null));

        public object NarrowContent
        {
            get { return (object)GetValue(NarrowContentProperty); }
            set { SetValue(NarrowContentProperty, value); }
        }
        public static readonly DependencyProperty NarrowContentProperty =
            DependencyProperty.Register(nameof(NarrowContent), typeof(object),
                typeof(NavigationViewItemHeaderEx), new PropertyMetadata(null));
    }

    public class NavigationViewItemEx :
        NavigationViewItem,
        INavItemEx
    {
        public bool ClearBackStack { get; set; } = false;
        public NavigationTransitionInfo TransitionInfo { get; set; }
        public object PageParameter { get; set; }
        public Type PageType { get; set; }
    }

    public class NavigationViewEx :
        NavigationView,
        INavigationViewEx
    {
        FrameEx _frame;

        public NavigationViewEx() : base()
        {
            if (DesignMode.DesignModeEnabled | DesignMode.DesignMode2Enabled)
            {
                return;
            }

            Content = _frame = new FrameEx();
            _frame.Focus(FocusState.Programmatic);

            ItemInvoked += (s, e) =>
            {
                if (e.IsSettingsInvoked)
                {
                    _frame.Navigate(SettingsPageType);
                }
                else
                {
                    var item = MenuItems.OfType<NavigationViewItemEx>().FirstOrDefault(x => x.Content.Equals(e.InvokedItem));
                    if (item != null)
                    {
                        SelectedItem = item;
                    }
                }
            };

            RegisterPropertyChangedCallback(DisplayModeProperty, (s, e) => UpdateHeaders());
            RegisterPropertyChangedCallback(IsPaneOpenProperty, (s, e) => UpdateHeaders());

            _frame.Navigated += (s, e) =>
            {
                var item = MenuItems
                    .OfType<NavigationViewItemEx>()
                    .Where(x => x.PageType.Equals(e.SourcePageType))
                    .Where(x => x.PageParameter == e.Parameter)
                    .FirstOrDefault();
                base.SelectedItem = (item == null && e.SourcePageType.Equals(SettingsPageType)) ? SettingsItem : item;
            };
        }

        private void UpdateHeaders()
        {
            var items = MenuItems
                .OfType<INavItemHeaderEx>()
                .Where(x => x.NarrowContent != null);
            foreach (var item in items)
            {
                item.SetBaseContent(IsPaneOpen ? item.Content : item.NarrowContent);
            }
        }

        public new event TypedEventHandler<INavigationViewEx, INavItemEx> SelectionChanged;

        public Type SettingsPageType { get; set; }

        public new INavItemEx SelectedItem
        {
            get => (INavItemEx)GetValue(SelectedItemProperty) ?? base.SelectedItem as NavigationViewItemEx;
            set
            {
                if (value == null)
                {
                    return;
                }
                else if ((_frame.CurrentSourcePageType?.Equals(value.PageType) ?? false)
                    && (_frame.CurrentPageParameter?.Equals(value.PageParameter) ?? false))
                {
                    SetSelectedItem(value);
                }
                else if (_frame.Navigate(value.PageType, value.PageParameter, value.TransitionInfo))
                {
                    SetSelectedItem(value);
                }
                else
                {
                    // what?
                }
            }
        }

        private void SetSelectedItem(INavItemEx item)
        {
            if (item.ClearBackStack)
            {
                _frame.BackStack.Clear();
                _frame.UpdateShellBackButton();
            }
            SetValue(SelectedItemProperty, base.SelectedItem = item);
            SelectionChanged?.Invoke(this, item);
        }

        public new static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(INavItemEx),
                typeof(NavigationViewEx), new PropertyMetadata(null));

        private class FrameEx : Frame
        {
            public FrameEx()
            {
                Navigated += (s, e) =>
                {
                    CurrentPageParameter = e.Parameter;
                    UpdateShellBackButton();
                };
                SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
                {
                    if (CanGoBack) GoBack();
                };
            }

            public object CurrentPageParameter { get; private set; }

            public void UpdateShellBackButton()
                => SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility
                    = CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
    }
}