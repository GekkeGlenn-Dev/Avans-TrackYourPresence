using TrackYourPresence.ViewModels.WorkDays;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackYourPresence.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkDayDetailPage : ContentPage
    {
        public WorkDayDetailPage()
        {
            InitializeComponent();
            BindingContext = new WorkDayDetailViewModel();
        }
    }
}