using System.ComponentModel;
using TrackYourPresence.ViewModels;
using Xamarin.Forms;

namespace TrackYourPresence.Views
{
    public partial class AbsentItemDetailPage : ContentPage
    {
        public AbsentItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new AbsentItemDetailViewModel();
        }
    }
}