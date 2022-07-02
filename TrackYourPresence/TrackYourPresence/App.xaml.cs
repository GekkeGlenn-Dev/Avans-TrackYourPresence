using System.Diagnostics;
using TrackYourPresence.Services;
using Plugin.DeviceInfo;
using Xamarin.Forms;

namespace TrackYourPresence
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            RegisterServices();
            LoginUser();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            LoginUser();
        }

        protected override void OnSleep() { }

        protected override void OnResume() { }

        private void RegisterServices()
        {
            DependencyService.Register<AuthenticationService>();
            DependencyService.Register<AbsentItemService>();
            DependencyService.Register<WorkDayService>();
            DependencyService.Register<LeaveOfAbsenceService>();
        }

        private void LoginUser()
        {
            var authenticationService = DependencyService.Get<IAuthenticationService>();
            authenticationService.LoginUser();
        }
    }
}