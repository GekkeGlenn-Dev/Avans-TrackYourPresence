using System.Diagnostics;
using TrackYourPresence.Services;
using Plugin.DeviceInfo;
using Xamarin.Forms;

namespace TrackYourPresence
{
    public partial class App : Application
    {
        public static string DeviceId { get; } = CrossDeviceInfo.Current.Id;
        public static string ApiBaseUri { get; } = "https://10.0.2.2:7013";

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

        private void LoginUser()
        {
            var authenticationService = DependencyService.Get<IAuthenticationService>();
            authenticationService.LoginUser();
        }

        public static string GetApiUrl(string path)
        {
            return path.StartsWith("/")
                ? ApiBaseUri + path
                : ApiBaseUri + "/" + path;
        }
    }
}