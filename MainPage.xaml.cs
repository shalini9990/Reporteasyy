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

        private void OnCounterClicked(object sender, EventArgs e)
        {
            RegisterUser();
        }

        private async Task RegisterUser()
        {
            FirebaseAuthLink auth = await provider.CreateUserWithEmailAndPasswordAsync("user@user.com", "I@mUser");
            User u = await provider.GetUserAsync(auth);
            string userAuthID = u.LocalId;

            DBUser newUser = new DBUser
            {
                DBUserName = "Testing",
                DBFullname = "TestingTest",
                DBEmail = "Email",
                DBIdentificationNumber = "1",
                DBPhoneNumber = "2",
                DBUserAuthID = userAuthID
            };

            string uID = await new FirebaseHelper().AddDBUser(newUser);

            DBUser user = await new FirebaseHelper().GetDBUser("Testing");
            await App.Current.MainPage.DisplayAlert("user:", $"UserAuthID: {user.DBUserAuthID}\nUserID: {user.DBUserID}", "OK");
        }

        private async void SignInBtnOnClick(object sender, EventArgs e)
        {
            try
            {
                FirebaseAuthLink auth = await provider.SignInWithEmailAndPasswordAsync("user@user.com", "I@mUser");
                auth = await auth.GetFreshAuthAsync();

                User u = await provider.GetUserAsync(auth);
                string userAuthID = u.LocalId;  // Authentication database userID.
                DBUser rtDBUser = await new FirebaseHelper().GetDBUserByAuthID(userAuthID);
                string rtDBUserID = rtDBUser.DBUserID;

                Preferences.Set("AuthUserID", userAuthID);
                Preferences.Set("RTDBUserID", rtDBUserID);

                Preferences.Get("AuthUserID", "NoAuthUserID");

                // Go to next page.
                await Navigation.PushAsync(new makereport());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }


}
