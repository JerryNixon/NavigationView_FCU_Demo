using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace NavViewDemo
{
    //public class NavItemEx
    //{
    //    public string Icon { get; set; }
    //    public string Text { get; set; }
    //}

    //public class NavItemHeaderEx
    //{
    //    public string Text { get; set; }
    //}

    //public class NavItemSeparatorEx { }    
    
    //public class NavConverter : IValueConverter
    //{
    //    public object Convert(object v, Type t, object p, string l)
    //    {
    //        var list = new List<object>();
    //        foreach (var item in (v as IEnumerable<object>))
    //        {
    //            switch (item)
    //            {
    //                case NavItemEx dto:
    //                    list.Add(ToItem(dto));
    //                    break;
    //                case NavItemHeaderEx dto:
    //                    list.Add(ToItem(dto));
    //                    break;
    //                case NavItemSeparatorEx dto:
    //                    list.Add(ToItem(dto));
    //                    break;
    //            }
    //        }
    //        return list;
    //    }

    //    object IValueConverter.ConvertBack(object v, Type t, object p, string l)
    //        => throw new NotImplementedException();

    //    NavigationViewItem ToItem(NavItemEx item)
    //        => new NavigationViewItem
    //        {
    //            Content = item.Text,
    //            Icon = ToFontIcon(item.Icon),
    //        };

    //    FontIcon ToFontIcon(string glyph)
    //        => new FontIcon { Glyph = glyph ?? string.Empty, };

    //    NavigationViewItemHeader ToItem(NavItemHeaderEx item)
    //        => new NavigationViewItemHeader { Content = item.Text, };

    //    NavigationViewItemSeparator ToItem(NavItemSeparatorEx item)
    //        => new NavigationViewItemSeparator { };
    //}
}
