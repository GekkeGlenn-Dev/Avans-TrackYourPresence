using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackYourPresence.ViewModels.LeaveOfAbsences;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackYourPresence.Views.LeaveOfAbsences
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeaveOfAbsenceDetailpage : ContentPage
    {
        public LeaveOfAbsenceDetailpage()
        {
            InitializeComponent();
            BindingContext = new LeaveOfAbsenceDetailViewModal();
        }
    }
}