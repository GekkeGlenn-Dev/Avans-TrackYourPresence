using System;
using System.Diagnostics;
using TrackYourPresence.Services;
using Plugin.DeviceInfo;
using TrackYourPresence.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackYourPresence
{
    public partial class App : Application
    {
        public static string DeviceId { get; } = CrossDeviceInfo.Current.Id;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<WorkDayService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}