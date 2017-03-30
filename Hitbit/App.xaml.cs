using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Hitbit
{
    using Views;
    using Windows.UI;
    using Windows.UI.ViewManagement;

    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (await ApplicationData.Current.LocalFolder.TryGetItemAsync("Hitbit.db3") == null)
            {
                StorageFile databaseFile = await Package.Current.InstalledLocation.GetFileAsync("Hitbit.db3");
                await databaseFile.CopyAsync(ApplicationData.Current.LocalFolder);
            }
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // This just gets in the way.
                //this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(320, 200));

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            if (titleBar != null)
            {
                Color titleBarColor = (Color)App.Current.Resources["SystemChromeMediumColor"];
                titleBar.BackgroundColor = titleBarColor;
                titleBar.ButtonBackgroundColor = titleBarColor;
            }

            if (SystemInformationHelpers.IsTenFootExperience)
            {
                ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);

                this.Resources.MergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri("ms-appx:///Styles/TenFootStylesheet.xaml")
                });
            }

            AppShell shell = Window.Current.Content as AppShell;


            if (shell == null)
            {
                shell = new AppShell();

                shell.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                shell.AppFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }
            }

            Window.Current.Content = shell;

            if (shell.AppFrame.Content == null)
            {

                shell.AppFrame.Navigate(typeof(ReadyPhrase), e.Arguments, new Windows.UI.Xaml.Media.Animation.SuppressNavigationTransitionInfo());
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
