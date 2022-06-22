using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TrackYourPresence.Models;
using TrackYourPresence.Views;
using Xamarin.Forms;

namespace TrackYourPresence.ViewModels
{
    public class AbsentItemsViewModel : BaseViewModel
    {
        private AbsentItem _selectedAbsentItem;

        public ObservableCollection<AbsentItem> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<AbsentItem> ItemTapped { get; }
        

        public AbsentItemsViewModel()
        {
            Title = "Afwezigheid";
            Items = new ObservableCollection<AbsentItem>();
            
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<AbsentItem>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await AbsentItemService.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
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
            SelectedAbsentItem = null;
        }

        public AbsentItem SelectedAbsentItem
        {
            get => _selectedAbsentItem;
            set
            {
                SetProperty(ref _selectedAbsentItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewAbsentItemPage));
        }

        async void OnItemSelected(AbsentItem absentItem)
        {
            if (absentItem == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(AbsentItemDetailPage)}?{nameof(AbsentItemDetailViewModel.ItemId)}={absentItem.Uuid}");
        }
    }
}