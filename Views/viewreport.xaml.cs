using Reporteasyy.Services;
using Reporteasyy.Models;
using Reporteasyy.ViewModels;

namespace Reporteasyy.Views;

public partial class viewreport : ContentPage
{
	public viewreport()
	{
		InitializeComponent();
		BindingContext = new viewrreportViewModel();
	}

    private async void ConnectWithCommunityClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new community());
    }

    private async void SafetyResourcesTipClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new resources());
    }
}