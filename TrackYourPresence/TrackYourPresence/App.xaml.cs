using System;
using System.Diagnostics;
using TrackYourPresence.Services;
using Plugin.DeviceInfo;
using TrackYourPresence.Models;
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
            RegisterServices();

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

        private void RegisterServices()
        {
            DependencyService.Register<AuthenticationService>();
            DependencyService.Register<AbsentItemService>();
            DependencyService.Register<WorkDayService>();
            DependencyService.Register<LeaveOfAbsenceService>();
        }
    }
}