using Reporteasyy.ViewModels;

namespace Reporteasyy.Views;

public partial class makereport : ContentPage
{
	public makereport()
	{
		InitializeComponent();
		BindingContext = new makereportViewModel(Navigation);
	}

	private void OnCapturePhotoClicked(object sender, EventArgs e)
	{

	}

	private void OnUploadFromGalleryClicked(object sender, EventArgs e)
	{

	}

	private async void viewreport(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new viewreport());
	}
}