using TrackYourPresence.Models;
using TrackYourPresence.ViewModels.WorkDays;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackYourPresence.Views.Workdays
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewWorkDayPage : ContentPage
    {
        public WorkDay WorkDay { get; set; }

        public NewWorkDayPage()
        {
            InitializeComponent();
            BindingContext = new NewWorkDayViewModel();
        }
    }
}