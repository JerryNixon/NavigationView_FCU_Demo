using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace Demo
{
    sealed partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Window.Current.Content = new Views.ShellPage();
            Window.Current.Activate();
        }
    }
}
