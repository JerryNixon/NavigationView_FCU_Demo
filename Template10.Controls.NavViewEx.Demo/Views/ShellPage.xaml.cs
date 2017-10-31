﻿using System;
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
    public sealed partial class ShellPage : Page
    {
        public ShellPage()
        {
            this.InitializeComponent();
        }

        private void SettingsAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            MainNavigationView.Navigate(typeof(Views.SettingsPage));
        }
    }
}