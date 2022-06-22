using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace TrackYourPresence.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class AbsentItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private DateTime date;
        private string description;
        public string Uuid { get; set; }

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

        public string ItemId
        {
            get => itemId;
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string uuid)
        {
            try
            {
                var item = await AbsentItemService.GetItemAsync(uuid);
                Uuid = item.Uuid.ToString();
                Date = item.Date;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
