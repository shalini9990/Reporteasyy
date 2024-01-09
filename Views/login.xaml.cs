using Firebase.Auth;
using Reporteasyy.Models;
using Reporteasyy.Services;

namespace Reporteasyy.Views;

public partial class login : ContentPage
{
	public login()
	{
		InitializeComponent();
	}

	private async void OnSignInClicked(object sender, EventArgs e)
	{
        FirebaseAuthProvider provider = new FirebaseAuthProvider(new FirebaseConfig(Settings.FirebaseAuth));

        try
        {
            FirebaseAuthLink auth = await provider.SignInWithEmailAndPasswordAsync(Username.Text, Password.Text);
            auth = await auth.GetFreshAuthAsync();

            User u = await provider.GetUserAsync(auth);
            string userAuthID = u.LocalId;  // Authentication database userID.
            DBUser rtDBUser = await new FirebaseHelper().GetDBUserByAuthID(userAuthID);
            string rtDBUserID = rtDBUser.DBUserID;

            Preferences.Set("AuthUserID", userAuthID);
            Preferences.Set("RTDBUserID", rtDBUserID);

            // Go to next page.
            await Navigation.PushAsync(new makereport());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error!", ex.Message, "OK");
        }
    }
}