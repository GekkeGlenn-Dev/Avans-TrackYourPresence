using System;
using System.Diagnostics;
using TrackYourPresence.Models;
using TrackYourPresence.Services;
using Xamarin.Forms;

namespace TrackYourPresence.ViewModels.WorkDays
{
    [QueryProperty(nameof(WorkDayUuid), nameof(WorkDayUuid))]
    public class WorkDayDetailViewModel : BaseViewModel
    {
        private string _workDayUuid;
        private WorkDay _workDay;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public WorkDayDetailViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string WorkDayUuid
        {
            get => _workDayUuid;
            set
            {
                _workDayUuid = value;
                LoadWorkDay(value);
            }
        }

        public DateTime Date
        {
            get => _workDay.Date;
            set => _workDay.Date = value;
        }

        public TimeSpan BeginTime
        {
            get => WorkDay.GetTimeSpanFromDateTime(_workDay.StartTime);
            set => _workDay.SetBeginTimeFromTimeSpan(value);
        }

        public TimeSpan EndTime
        {
            get => WorkDay.GetTimeSpanFromDateTime(_workDay.StopTime);
            set => _workDay.SetStopTimeFromTimeSpan(value);
        }

        public TimeSpan PauseTime
        {
            get => WorkDay.GetTimeSpanFromDateTime(_workDay.PauseTime);
            set => _workDay.SetPauseTimeFromTimeSpan(value);
        }

        private async void LoadWorkDay(string workdayId)
        {
            try
            {
                _workDay = await WorkDayService.GetItemAsync(workdayId);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Workday");
            }
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            await WorkDayService.UpdateItemAsync(_workDay);
            await Shell.Current.GoToAsync("..");
        }
    }
}