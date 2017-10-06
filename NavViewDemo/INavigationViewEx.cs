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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace NavViewDemo
{
    public enum VisibleWhenTargets { Narrow, Wide, Both }

    public interface INavigationViewItemHeaderEx 
    {
        VisibleWhenTargets VisibleWhen { get; set; }
    }

    public interface INavigationViewItemEx 
    {
        bool ClearBackStack { get; set; }
        object PageParameter { get; set; }
        Type PageType { get; set; }
        NavigationTransitionInfo TransitionInfo { get; set; }
    }

    public interface INavigationViewEx
    {
        event TypedEventHandler<INavigationViewEx, INavigationViewItemEx> SelectionChanged;
        INavigationViewItemEx SelectedItem { get; set; }
        Type SettingsPageType { get; set; }
    }

    public class NavigationViewItemHeaderEx :
        NavigationViewItemHeader,
        INavigationViewItemHeaderEx
    {
        public VisibleWhenTargets VisibleWhen { get; set; } = VisibleWhenTargets.Both;
    }

    public class NavigationViewItemEx :
        NavigationViewItem,
        INavigationViewItemEx
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

            RegisterPropertyChangedCallback(DisplayModeProperty, (s, e) => UpdateHeaderVisibility());
            RegisterPropertyChangedCallback(IsPaneOpenProperty, (s, e) => UpdateHeaderVisibility());

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

        private void UpdateHeaderVisibility()
        {
            var items = MenuItems
                .OfType<INavigationViewItemHeaderEx>()
                .OfType<FrameworkElement>()
                .Select(x => new { Element = x, Interface = x as INavigationViewItemHeaderEx });
            foreach (var item in items)
            {
                switch (item.Interface.VisibleWhen)
                {
                    case VisibleWhenTargets.Narrow when (!IsPaneOpen):
                        item.Element.Visibility = Visibility.Visible;
                        break;
                    case VisibleWhenTargets.Wide when (IsPaneOpen):
                        item.Element.Visibility = Visibility.Visible;
                        break;
                    case VisibleWhenTargets.Both:
                        item.Element.Visibility = Visibility.Visible;
                        break;
                    default:
                        item.Element.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }

        public new event TypedEventHandler<INavigationViewEx, INavigationViewItemEx> SelectionChanged;

        public Type SettingsPageType { get; set; }

        public new INavigationViewItemEx SelectedItem
        {
            get => (INavigationViewItemEx)GetValue(SelectedItemProperty) ?? base.SelectedItem as NavigationViewItemEx;
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

        private void SetSelectedItem(INavigationViewItemEx item)
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
            DependencyProperty.Register(nameof(SelectedItem), typeof(INavigationViewItemEx),
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