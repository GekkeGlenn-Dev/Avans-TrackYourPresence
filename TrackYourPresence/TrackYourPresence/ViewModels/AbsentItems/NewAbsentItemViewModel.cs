using System;
using TrackYourPresence.Models;
using Xamarin.Forms;

namespace TrackYourPresence.ViewModels
{
    public class NewAbsentItemViewModel : BaseViewModel
    {
        private DateTime date = DateTime.Today;
        private string description;

        public NewAbsentItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(description) 
                   && description.Length >= 15 
                   && description.Length <= 255
                   && date != null;
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            AbsentItem newAbsentItem = new AbsentItem()
            {
                Uuid = Guid.NewGuid().ToString(),
                Date = Date,
                Description = Description
            };

            await DataStore.AddItemAsync(newAbsentItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}