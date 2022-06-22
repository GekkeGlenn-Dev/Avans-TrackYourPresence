using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TrackYourPresence.Models;
using TrackYourPresence.Views.LeaveOfAbsences;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TrackYourPresence.ViewModels.LeaveOfAbsences
{
    public class LeaveOfAbsencesViewModal : BaseViewModel
    {
        public ObservableCollection<LeaveOfAbsence> LeaveOfAbsences { get; }
        private LeaveOfAbsence _selectedLeaveOfAbsence;

        public Command<LeaveOfAbsence> LeaveOfAbsenceTapped { get; }
        public Command LoadLeaveOfAbsenceCommand { get; }
        public Command AddLeaveOfAbsenceCommand { get; }

        public LeaveOfAbsencesViewModal()
        {
            Title = "Verlof Registratie";
            LeaveOfAbsences = new();

            LoadLeaveOfAbsenceCommand = new(async () => await ExecuteLoadLeaveOfAbsenceCommand());
            LeaveOfAbsenceTapped = new(OnLeaveOfAbsenceSelected);
            AddLeaveOfAbsenceCommand = new(OnAddLeaveOfAbsence);
        }

        async Task ExecuteLoadLeaveOfAbsenceCommand()
        {
            IsBusy = true;

            try
            {
                LeaveOfAbsences.Clear();
                var items = await LeaveOfAbsenceService.GetItemsAsync(true);
                items.ForEach(item => LeaveOfAbsences.Add(item));
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
            SelectedLeaveOfAbsence = null;
        }

        public LeaveOfAbsence SelectedLeaveOfAbsence
        {
            get => _selectedLeaveOfAbsence;
            set
            {
                SetProperty(ref _selectedLeaveOfAbsence, value);
                OnLeaveOfAbsenceSelected(value);
            }
        }

        private async void OnAddLeaveOfAbsence(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewLeaveOfAbsencePage));
        }

        async void OnLeaveOfAbsenceSelected(LeaveOfAbsence leaveOfAbsence)
        {
            if (leaveOfAbsence == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(LeaveOfAbsenceDetailpage)}?{nameof(LeaveOfAbsenceDetailViewModal.LeaveOfAbsenceUuid)}={leaveOfAbsence.Uuid}");
        }
    }
}