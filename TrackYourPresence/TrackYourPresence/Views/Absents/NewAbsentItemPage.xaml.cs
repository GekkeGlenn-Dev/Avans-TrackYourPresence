using TrackYourPresence.Models;
using TrackYourPresence.ViewModels;
using Xamarin.Forms;

namespace TrackYourPresence.Views
{
    public partial class NewAbsentItemPage : ContentPage
    {
        public AbsentItem AbsentItem { get; set; }

        public NewAbsentItemPage()
        {
            InitializeComponent();
            BindingContext = new NewAbsentItemViewModel();
        }
    }
}