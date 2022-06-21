using System;

namespace TrackYourPresence.ViewModels.LeaveOfAbsences
{
    public class NewLeaveOfAbsenceViewModal : BaseViewModel
    {
        private DateTime beginDate = DateTime.Today;
        private string totalWorkDays;

        public NewLeaveOfAbsenceViewModal()
        {
            Title = "Verlof registeren";
        }
        
        public DateTime BeginDate
        {
            get => beginDate;
            set => SetProperty(ref beginDate, value);
        }

        public string TotalWorkDays
        {
            get => totalWorkDays;
            set => SetProperty(ref totalWorkDays, value);
        }
    }
}