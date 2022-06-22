using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TrackYourPresence.Models;
using TrackYourPresence.Views;
using Xamarin.Forms;

namespace TrackYourPresence.ViewModels.Settings
{
    public class SettingsViewModal : BaseViewModel
    {
        public Command LoadSettings { get; }
        public Command SaveCommand { get; }

        private User _user;

        public int WorkHours
        {
            get => _user.WorkHours;
            set => _user.WorkHours = value;
        }

        public int VacationDays
        {
            get => _user.VacationDays;
            set => _user.VacationDays = value;
        }

        public SettingsViewModal()
        {
            Title = "Settings";
            LoadSettings = new Command(async () => await OnLoadSettings());
            SaveCommand = new Command(OnSave);
            
            LoadSettings.Execute(null);
        }


        async Task OnLoadSettings()
        {
            IsBusy = true;

            try
            {
                _user = await AuthenticationService.GetUser();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnSave()
        {
            AuthenticationService.UpdateUser(_user);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }
}