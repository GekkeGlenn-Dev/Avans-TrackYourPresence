using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TrackYourPresence.Models;
using TrackYourPresence.Views;
using TrackYourPresence.Views.Workdays;
using Xamarin.Forms;

namespace TrackYourPresence.ViewModels.WorkDays
{
    public class WorkDaysViewModel : BaseViewModel
    {
        private WorkDay _selectedWorkDay;
        
        public ObservableCollection<WorkDay> WorkDays { get; }
        public Command LoadWorkDaysCommand { get; }
        public Command AddWorkDayCommand { get; }
        public Command<WorkDay> WorkDayTapped { get; }

        public WorkDaysViewModel()
        {
            Title = "Werkdagen";
            WorkDays = new ObservableCollection<WorkDay>();
            
            LoadWorkDaysCommand = new Command(async () => await ExecuteLoadItemsCommand());
            WorkDayTapped = new Command<WorkDay>(OnWorkDaySelected);
            AddWorkDayCommand = new Command(OnAddWorkDay);
        }
        
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                WorkDays.Clear();
                var items = await WorkDayService.GetItemsAsync(true);
                foreach (var item in items)
                {
                    WorkDays.Add(item);
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
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedWorkDay = null;
        }

        public WorkDay SelectedWorkDay
        {
            get => _selectedWorkDay;
            set
            {
                SetProperty(ref _selectedWorkDay, value);
                OnWorkDaySelected(value);
            }
        }

        private async void OnAddWorkDay(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewWorkDayPage));
        }

        async void OnWorkDaySelected(WorkDay workDay)
        {
            if (workDay == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(WorkDayDetailPage)}?{nameof(WorkDayDetailViewModel.WorkDayUuid)}={workDay.Uuid}");
        }
    }
}