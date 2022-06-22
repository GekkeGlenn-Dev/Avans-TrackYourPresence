#nullable enable
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TrackYourPresence.Models;
using Xamarin.Forms;

namespace TrackYourPresence.ViewModels.LeaveOfAbsences
{
    public class NewLeaveOfAbsenceViewModal : BaseViewModel
    {
        private DateTime beginDate = DateTime.Today;
        private string? _totalWorkDays = "1";

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public NewLeaveOfAbsenceViewModal()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public DateTime BeginDate
        {
            get => beginDate;
            set => SetProperty(ref beginDate, value);
        }

        public string TotalWorkDays
        {
            get => _totalWorkDays;
            set => SetProperty(ref _totalWorkDays, value);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var leaveOfAbsence = new LeaveOfAbsence()
            {
                StartDate = BeginDate,
                TotalOfDays = Int32.Parse(TotalWorkDays)
            };

            leaveOfAbsence.EndDate = await CalculateEndDate();
            await LeaveOfAbsenceService.AddItemAsync(leaveOfAbsence);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private bool ValidateSave()
        {
            var totalWorkDaysAsInteger = Int32.Parse(TotalWorkDays);
            var user = AuthenticationService.GetUser().Result;
            return totalWorkDaysAsInteger > 0 && totalWorkDaysAsInteger <= user.VacationDays;
        }

        private async Task<DateTime> CalculateEndDate()
        {
            Debug.WriteLine(beginDate);
            var d = beginDate.AddDays(await GetTotalDays());
            Debug.WriteLine(d);

            return d;
        }

        private async Task<double> GetTotalDays()
        {
            var user = await AuthenticationService.GetUser();
            Debug.WriteLine(user.WorkHours);
            Debug.WriteLine(TotalWorkDays);
            var workDaysPerWeek = user.WorkHours / 8.0;
            Debug.WriteLine(workDaysPerWeek);
            var weeksOfAbsence = Int32.Parse(TotalWorkDays) / workDaysPerWeek;
            Debug.WriteLine(weeksOfAbsence);
            var daysOfAbsence = (weeksOfAbsence - (int) weeksOfAbsence) * workDaysPerWeek;
            Debug.WriteLine(daysOfAbsence);
            Debug.WriteLine(weeksOfAbsence * 7 + daysOfAbsence);
            return weeksOfAbsence * 7 + daysOfAbsence;
        }
    }
}