using System;
using System.Diagnostics;
using TrackYourPresence.Models;
using Xamarin.Forms;

namespace TrackYourPresence.ViewModels.LeaveOfAbsences
{
    [QueryProperty(nameof(LeaveOfAbsenceUuid), nameof(LeaveOfAbsenceUuid))]
    public class LeaveOfAbsenceDetailViewModal : BaseViewModel
    {
        private string _leaveOfAbsenceUuid { get; set; }
        private LeaveOfAbsence _leaveOfAbsence { get; set; }

        public DateTime BeginDate => _leaveOfAbsence.StartDate;
        public DateTime EndDate => _leaveOfAbsence.EndDate;
        public int Days => _leaveOfAbsence.TotalOfDays;

        public LeaveOfAbsenceDetailViewModal()
        {
            Title = "Verlof details";
        }

        public string LeaveOfAbsenceUuid
        {
            get => _leaveOfAbsenceUuid;
            set
            {
                _leaveOfAbsenceUuid = value;
                LoadItemId(value);
            }
        }

        private async void LoadItemId(string uuid)
        {
            try
            {
                _leaveOfAbsence = await LeaveOfAbsenceService.GetItemAsync(uuid);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}