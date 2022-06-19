using TrackYourPresence.ViewModels;
using Xamarin.Forms;

namespace TrackYourPresence.Views
{
    public partial class AbsentItemsPage : ContentPage
    {
        AbsentItemsViewModel _viewModel;

        public AbsentItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new AbsentItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}