using Reporteasyy.ViewModels;

namespace Reporteasyy.Views;

public partial class makereport : ContentPage
{
	public makereport()
	{
		InitializeComponent();
		BindingContext = new makereportViewModel(Navigation);
	}

	private async void OnUploadFromGalleryClicked(object sender, EventArgs e)
	{
		try
		{
			FileResult file = await MediaPicker.PickPhotoAsync();
			string filePath = null;

			if (file != null)
			{
				filePath = file.FullPath;
			}

			((makereportViewModel)BindingContext).MediaPath = filePath;
		}
		catch(Exception ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");
		}
	}

	private async void viewreport(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new viewreport());
	}
}