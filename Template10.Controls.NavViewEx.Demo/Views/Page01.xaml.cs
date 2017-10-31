using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page01 : Page
    {
        public Page01()
        {
            this.InitializeComponent();
        }

        public IEnumerable<object> Items
        {
            get
            {
                var r = new Random((int)DateTime.Now.Ticks);
                yield return new { Title = "First", Items = Enumerable.Range(1, r.Next(5, 16)) };
                yield return new { Title = "Second", Items = Enumerable.Range(1, r.Next(5, 16)) };
                yield return new { Title = "Third", Items = Enumerable.Range(1, r.Next(5, 16)) };
                yield return new { Title = "Fourth", Items = Enumerable.Range(1, r.Next(5, 16)) };
                yield return new { Title = "Fifth", Items = Enumerable.Range(1, r.Next(5, 16)) };
            }
        }
    }
}
