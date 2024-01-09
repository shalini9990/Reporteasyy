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
}