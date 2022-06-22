using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TrackYourPresence.Models;
using TrackYourPresence.Services;
using Xamarin.Forms;

namespace TrackYourPresence.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IAbsentItemService AbsentItemService => DependencyService.Get<IAbsentItemService>();
        public IWorkDayService WorkDayService => DependencyService.Get<IWorkDayService>();
        public ILeaveOfAbsenceService LeaveOfAbsenceService => DependencyService.Get<ILeaveOfAbsenceService>();
        public IAuthenticationService AuthenticationService => DependencyService.Get<IAuthenticationService>();

        bool isBusy = false;
        string title = String.Empty;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
