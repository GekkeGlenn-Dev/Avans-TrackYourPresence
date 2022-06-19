using System;
using TrackYourPresence.Models;
using Xamarin.Forms;

namespace TrackYourPresence.ViewModels.WorkDays
{
    public class NewWorkDayViewModel : BaseViewModel
    {
        private DateTime date = DateTime.Today;
        private TimeSpan beginTime;
        private TimeSpan endTime;
        private TimeSpan pauseTime;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public NewWorkDayViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public TimeSpan BeginTime
        {
            get => beginTime;
            set => SetProperty(ref beginTime, value);
        }

        public TimeSpan EndTime
        {
            get => endTime;
            set => SetProperty(ref endTime, value);
        }

        public TimeSpan PauseTime
        {
            get => pauseTime;
            set => SetProperty(ref pauseTime, value);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            WorkDay workDay = new WorkDay(Date);
            workDay.SetBeginTimeFromTimeSpan(BeginTime);
            workDay.SetStopTimeFromTimeSpan(EndTime);
            workDay.SetPauseTimeFromTimeSpan(PauseTime);

            await WorkDayService.AddItemAsync(workDay);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private bool ValidateSave()
        {
            return date != null
                   && beginTime != null
                   && endTime != null
                   && pauseTime != null;
        }
    }
}