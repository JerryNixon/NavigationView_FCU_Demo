using System;

namespace UWP.Controls.NavViewEx
{
    public class NavViewEx : NavigationView
    {
        Frame _frame;

        public Type SettingsPageType { get; set; }

        public NavViewEx()
        {
            Content = _frame = new Frame();
            _frame.Navigated += Frame_Navigated;
            ItemInvoked += NavViewEx_ItemInvoked;
            SystemNavigationManager.GetForCurrentView().BackRequested += ShellPage_BackRequested;
            RegisterPropertyChangedCallback(IsPaneOpenProperty, IsPaneOpenChanged);
            Loaded += (s, e) =>
            {
                if (FindStart() is NavigationViewItem i && i != null)
                    Navigate(i.GetValue(NavProperties.PageTypeProperty) as Type);
            };
        }

        public void Navigate(Type type)
            => Navigate(_frame, type);

        public virtual void Navigate(Frame frame, Type type)
            => frame.Navigate(type);

        public new object SelectedItem
        {
            set
            {
                if (value == SettingsItem)
                {
                    Navigate(SettingsPageType);
                    base.SelectedItem = value;
                    _frame.BackStack.Clear();
                }
                else if (value is NavigationViewItem i && i != null)
                {
                    Navigate(i.GetValue(NavProperties.PageTypeProperty) as Type);
                    base.SelectedItem = value;
                    _frame.BackStack.Clear();
                }
                UpdateBackButton();
                UpdateHeader();
            }
        }

        private NavigationViewItem FindStart()
            => MenuItems.OfType<NavigationViewItem>().SingleOrDefault(x => (bool)x.GetValue(NavProperties.IsStartPageProperty));

        private void IsPaneOpenChanged(DependencyObject sender, DependencyProperty dp)
        {
            foreach (var item in MenuItems.OfType<NavigationViewItemHeader>())
            {
                item.Opacity = IsPaneOpen ? 1 : 0;
            }
        }

        private void NavViewEx_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
            => SelectedItem = (args.IsSettingsInvoked) ? SettingsItem : Find(args.InvokedItem.ToString());

        private void Frame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
            => SelectedItem = (e.SourcePageType == SettingsPageType) ? SettingsItem : Find(e.SourcePageType) ?? base.SelectedItem;

        private void UpdateHeader()
        {
            if (_frame.Content is Page p && p.GetValue(NavProperties.HeaderProperty) is string s && !string.IsNullOrEmpty(s))
            {
                Header = s;
            }
        }

        private void ShellPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (_frame.CanGoBack)
            {
                _frame.GoBack();
            }
        }

        private NavigationViewItem Find(string content)
            => MenuItems.OfType<NavigationViewItem>().SingleOrDefault(x => x.Content.Equals(content));

        private NavigationViewItem Find(Type type)
            => MenuItems.OfType<NavigationViewItem>().SingleOrDefault(x => type.Equals(x.GetValue(NavProperties.PageTypeProperty)));


        private void UpdateBackButton()
            => SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                (_frame.CanGoBack) ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
    }

}
