using Firebase.Auth;
using Reporteasyy.Models;
using Reporteasyy.Services;
using Reporteasyy.Views;
using Newtonsoft.Json;

namespace Reporteasyy
{
    public partial class MainPage : ContentPage
    {
        FirebaseAuthProvider provider = new FirebaseAuthProvider(new FirebaseConfig(Settings.FirebaseAuth));

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new signup());
        }

        private async void SignInBtnOnClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new login());
        }
    }


}
