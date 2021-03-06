using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackYourPresence.ViewModels.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrackYourPresence.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = new SettingsViewModal();
        }
    }
}