using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TrackYourPresence.Models;
using TrackYourPresence.Services;
using Xamarin.Forms;

namespace TrackYourPresence.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<WorkDay> WorkDays { get; }
        public Command LoadWorkDaysCommand { get; }

        private IWorkDayService workDayService;

        public HomeViewModel(IWorkDayService workDayService)
        {
            this.workDayService = workDayService;
            WorkDays = new ObservableCollection<WorkDay>();
            Title = "Home";
            LoadWorkDaysCommand = new Command(async () => await ExecuteLoadWorkDaysCommand());
            // OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        // public ICommand OpenWebCommand { get; }

        private async Task ExecuteLoadWorkDaysCommand()
        {
            IsBusy = true;

            Debug.WriteLine("-------------START-------------");
            try
            {
                WorkDays.Clear();
                var workDays = await workDayService.GetItemsAsync(true);
                foreach (var workDay in workDays)
                {
                    WorkDays.Add(workDay);
                    Debug.WriteLine(workDay.Date.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            Debug.WriteLine("-------------STOP-------------");
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}