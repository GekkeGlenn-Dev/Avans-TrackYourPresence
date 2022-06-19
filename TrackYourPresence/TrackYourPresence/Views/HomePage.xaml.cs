using TrackYourPresence.Services;
using TrackYourPresence.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackYourPresence.Views
{
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel _viewModel;
        
        public HomePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new HomeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}