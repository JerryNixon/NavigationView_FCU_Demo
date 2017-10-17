using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using static NavViewDemo.NavItemEx;

namespace NavViewDemo
{
    public class NavItemEx
    {
        public enum ItemTypes { Header, Item, Seperator }
        public ItemTypes ItemType { get; set; } = ItemTypes.Item;

        public string Icon { get; set; }
        public string Text { get; set; }

        public Type PageType { get; set; }
        public object Parameter { get; set; }
    }

    public class NavViewEx : NavigationView
    {
        public NavViewEx()
        {
            Content = new Frame();
            base.MenuItemsSource = new ObservableCollection<object>();
            ItemInvoked += NavViewEx_ItemInvoked;
        }

        public Type SettingsPageType { get; set; }

        private void NavViewEx_ItemInvoked(NavigationView sender, 
            NavigationViewItemInvokedEventArgs args)
        {
            var frame = Content as Frame;
            if (args.IsSettingsInvoked)
            {
                frame.Navigate(SettingsPageType);
            }
            else
            {
                var item = MenuItems
                    .OfType<NavigationViewItem>()
                    .Single(x => x.Content.Equals(args.InvokedItem));
                var page = item.GetValue(NavigationViewAttached.PageTypeProperty) as Type;
                var param = item.GetValue(NavigationViewAttached.ParameterProperty);
                frame.Navigate(page, param);
            }

        }

        public new ObservableCollection<object> MenuItemsSource
        {
            get => base.MenuItemsSource as ObservableCollection<object>;
            set
            {
                MenuItemsSource.Clear();
                foreach (var item in value.OfType<NavItemEx>())
                {
                    switch (item.ItemType)
                    {
                        case ItemTypes.Header:
                            MenuItemsSource.Add(item.ToHeader());
                            break;
                        case ItemTypes.Item:
                            MenuItemsSource.Add(item.ToItem());
                            break;
                        case ItemTypes.Seperator:
                            MenuItemsSource.Add(item.ToSeparator());
                            break;
                    }
                }
            }
        }
    }

    public class NavigationViewAttached : DependencyObject
    {
        public static Type GetPageType(NavigationViewItem obj)
            => (Type)obj.GetValue(PageTypeProperty);
        public static void SetPageType(NavigationViewItem obj, Type value)
            => obj.SetValue(PageTypeProperty, value);
        public static readonly DependencyProperty PageTypeProperty =
            DependencyProperty.RegisterAttached("PageType", typeof(Type),
                typeof(NavigationViewAttached), new PropertyMetadata(null));

        public static object GetParameter(DependencyObject obj)
            => obj.GetValue(ParameterProperty);
        public static void SetParameter(DependencyObject obj, object value)
            => obj.SetValue(ParameterProperty, value);
        public static readonly DependencyProperty ParameterProperty =
            DependencyProperty.RegisterAttached("Parameter", typeof(object),
                typeof(NavigationViewAttached), new PropertyMetadata(null));

        public static object GetCurrentParameter(Frame obj)
            => obj.GetValue(CurrentParameterProperty);
        public static void SetCurrentParameter(Frame obj, object value)
            => obj.SetValue(CurrentParameterProperty, value);
        public static readonly DependencyProperty CurrentParameterProperty =
            DependencyProperty.RegisterAttached("CurrentParameter", typeof(object),
                typeof(NavigationViewAttached), new PropertyMetadata(null));
    }

    public static class NavigationViewExtensions
    {
        public static NavigationViewItem ToItem(this NavItemEx item)
        {
            var viewItem = new NavigationViewItem
            {
                Content = item.Text,
                Icon = new FontIcon
                {
                    Glyph = item.Icon,
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                }
            };
            viewItem.SetValue(NavigationViewAttached.PageTypeProperty, item.PageType);
            viewItem.SetValue(NavigationViewAttached.ParameterProperty, item.Parameter);
            return viewItem;
        }

        public static NavigationViewItemHeader ToHeader(this NavItemEx item)
            => new NavigationViewItemHeader();

        public static NavigationViewItemSeparator ToSeparator(this NavItemEx item)
            => new NavigationViewItemSeparator();
    }

    public sealed partial class ShellPage : Page
    {
        public ShellPage()
        {
            InitializeComponent();
        }
    }
}
