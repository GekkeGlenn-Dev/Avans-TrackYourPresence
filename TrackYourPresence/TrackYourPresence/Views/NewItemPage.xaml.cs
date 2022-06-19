using TrackYourPresence.Models;
using TrackYourPresence.ViewModels;
using Xamarin.Forms;

namespace TrackYourPresence.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}