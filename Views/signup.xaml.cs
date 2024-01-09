using Firebase.Auth;
using Reporteasyy.Models;
using Reporteasyy.Services;

namespace Reporteasyy.Views;

public partial class signup : ContentPage
{
	public signup()
	{
		InitializeComponent();
	}

	private async void OnCreateAccountClicked(object sender, EventArgs e)
	{
        FirebaseAuthProvider provider = new FirebaseAuthProvider(new FirebaseConfig(Settings.FirebaseAuth));

		try
		{
            FirebaseAuthLink auth = await provider.CreateUserWithEmailAndPasswordAsync(Email.Text, Password.Text);
            User u = await provider.GetUserAsync(auth);
            string userAuthID = u.LocalId;

            DBUser newUser = new DBUser
            {
                DBUserName = Username.Text,
                DBFullname = FullName.Text,
                DBEmail = Email.Text,
                DBIdentificationNumber = IdentificationNumber.Text,
                DBPhoneNumber = PhoneNumber.Text,
                DBUserAuthID = userAuthID
            };

            string uID = await new FirebaseHelper().AddDBUser(newUser);

            await DisplayAlert("Registered!", $"UserAuthID: {userAuthID}\nUserID: {uID}", "OK");
            await Navigation.PushAsync(new login());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error!", ex.Message, "OK");
        }
    }
}