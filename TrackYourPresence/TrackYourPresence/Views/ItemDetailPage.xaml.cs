using System.ComponentModel;
using TrackYourPresence.ViewModels;
using Xamarin.Forms;

namespace TrackYourPresence.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}