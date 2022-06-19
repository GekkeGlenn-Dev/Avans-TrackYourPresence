using TrackYourPresence.ViewModels.WorkDays;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackYourPresence.Views.Workdays
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkDaysPage : ContentPage
    {
        WorkDaysViewModel _viewModel;

        public WorkDaysPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new WorkDaysViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}