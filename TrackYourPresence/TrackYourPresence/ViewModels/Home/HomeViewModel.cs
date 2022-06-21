using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TrackYourPresence.Models;
using TrackYourPresence.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TrackYourPresence.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<WorkDay> WorkDays { get; }
        public Command LoadWorkDaysCommand { get; }


        public HomeViewModel()
        {
            WorkDays = new ObservableCollection<WorkDay>();
            Title = "Home";
            LoadWorkDaysCommand = new Command(async () => await ExecuteLoadWorkDaysCommand());
            // OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        // public ICommand OpenWebCommand { get; }

        private async Task ExecuteLoadWorkDaysCommand()
        {
            IsBusy = true;

            try
            {
                WorkDays.Clear();
                var workDays = await WorkDayService.GetCurrentWeekAsync(true);
                workDays.ForEach(workDay => WorkDays.Add(workDay));
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

        public void OnAppearing()
        {
            IsBusy = true;
        }

        async void OnWorkDaySelected(AbsentItem absentItem)
        {
            if (absentItem == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync(
                $"{nameof(WorkDayDetailPage)}?{nameof(AbsentItemDetailViewModel.ItemId)}={absentItem.Uuid}");
        }
    }
}